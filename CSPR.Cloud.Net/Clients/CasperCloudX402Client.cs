using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSPR.Cloud.Net.Clients.Api;
using CSPR.Cloud.Net.Errors;
using CSPR.Cloud.Net.Objects.Config;
using CSPR.Cloud.Net.Objects.X402;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CSPR.Cloud.Net.Clients
{
    /// <summary>
    /// Client for the CSPR.cloud x402 Facilitator API (default host
    /// <c>https://x402-facilitator.cspr.cloud</c>): verify and settle x402 v2 payment payloads
    /// signed per CEP-3009, and query scheme/network support. Unlike
    /// <see cref="CasperCloudRestClient"/> there is no Mainnet/Testnet facade split — one host
    /// serves every network, and the CAIP-2 network id travels inside the payment objects.
    /// Authentication is the same CSPR.cloud access token, sent raw in the
    /// <c>Authorization</c> header. For a self-hosted facilitator
    /// (make-software/casper-x402), pass its URL as <c>baseUrl</c>.
    /// </summary>
    public class CasperCloudX402Client
    {
        // x402 protocol JSON omits absent optional members rather than sending nulls.
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
        };

        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;
        private readonly ILogger? _logger;

        public CasperCloudX402Client(CasperCloudClientConfig config, HttpClient? httpClient = null,
            ILoggerFactory? loggerFactory = null, string? baseUrl = null)
        {
            _apiKey = config.ApiKey;
            _httpClient = httpClient ?? new HttpClient();
            _logger = loggerFactory?.CreateLogger<CasperCloudX402Client>();
            _baseUrl = string.IsNullOrEmpty(baseUrl)
                ? Endpoints.BaseUrls.X402Facilitator
                : baseUrl!.TrimEnd('/');
        }

        /// <summary>
        /// Retrieves the facilitator's supported (scheme, network) pairs and signer accounts.
        /// Note this does NOT list tokens — the settled asset is the resource server's choice.
        /// For more information, see <see href="https://docs.cspr.cloud/x402-facilitator-api/reference">CSPR Cloud API documentation</see>.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <returns>A task whose result contains the supported kinds, extensions and signers.</returns>
        public async Task<X402SupportedResponse?> GetSupportedAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataAsync<X402SupportedResponse>(
                Endpoints.X402.GetSupported(_baseUrl), cancellationToken);
        }

        /// <summary>
        /// Validates a payment payload against payment requirements WITHOUT committing anything
        /// on-chain. A negative verdict arrives as <c>IsValid == false</c> with
        /// <c>InvalidReason</c>/<c>InvalidMessage</c>, not as an exception.
        /// For more information, see <see href="https://docs.cspr.cloud/x402-facilitator-api/reference">CSPR Cloud API documentation</see>.
        /// </summary>
        /// <param name="request">The payment payload and the requirements it must satisfy.</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <returns>A task whose result contains the verification verdict.</returns>
        public async Task<X402VerifyResponse?> VerifyAsync(
            X402FacilitatorRequest request, CancellationToken cancellationToken = default)
        {
            return await PostDataAsync<X402VerifyResponse>(
                Endpoints.X402.PostVerify(_baseUrl), request, cancellationToken);
        }

        /// <summary>
        /// Re-verifies and settles a payment on-chain via the CEP-3009
        /// <c>transfer_with_authorization</c> entry point. The facilitator answers HTTP 200 even
        /// for a FAILED settlement — always branch on <c>Success</c>, never on the status code.
        /// For more information, see <see href="https://docs.cspr.cloud/x402-facilitator-api/reference">CSPR Cloud API documentation</see>.
        /// </summary>
        /// <param name="request">The payment payload and the requirements it must satisfy.</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <returns>A task whose result contains the settlement outcome.</returns>
        public async Task<X402SettleResponse?> SettleAsync(
            X402FacilitatorRequest request, CancellationToken cancellationToken = default)
        {
            return await PostDataAsync<X402SettleResponse>(
                Endpoints.X402.PostSettle(_baseUrl), request, cancellationToken);
        }

        private async Task<T?> GetDataAsync<T>(string endpoint, CancellationToken cancellationToken) where T : class
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            request.Headers.Add("Authorization", _apiKey);
            request.Headers.Add("Accept", "application/json");

            var response = await _httpClient.SendAsync(request, cancellationToken);
            return await HandleResponseAsync<T>(response);
        }

        private async Task<T?> PostDataAsync<T>(string endpoint, object body, CancellationToken cancellationToken) where T : class
        {
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Headers.Add("Authorization", _apiKey);
            request.Headers.Add("Accept", "application/json");
            request.Content = new StringContent(
                JsonConvert.SerializeObject(body, SerializerSettings), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request, cancellationToken);
            return await HandleResponseAsync<T>(response);
        }

        // Same status mapping as CasperCloudRestClient.HandleResponseAsync, minus the
        // tolerant-rows machinery (x402 responses carry no data row arrays).
        private async Task<T?> HandleResponseAsync<T>(HttpResponseMessage response) where T : class
        {
            var code = response.StatusCode;
            if (code == HttpStatusCode.OK || code == HttpStatusCode.Created)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(content)) return null;
                return JsonConvert.DeserializeObject<T>(content);
            }
            if (code == HttpStatusCode.NotFound) return null;

            var body = await response.Content.ReadAsStringAsync();
            switch (code)
            {
                case HttpStatusCode.BadRequest: throw new InvalidParamException($"Invalid Param Error: {body}", _logger);
                case HttpStatusCode.Unauthorized: throw new UnauthorizedException($"Unauthorized Error: {body}", _logger);
                case HttpStatusCode.Forbidden: throw new AccessDeniedException($"Access Denied Error: {body}", _logger);
                case HttpStatusCode.Conflict: throw new DuplicateEntryException($"Duplicate Entry Error: {body}", _logger);
                case (HttpStatusCode)429: throw new RateLimitException($"Rate Limit Error: {body}", _logger);
            }
            if ((int)code >= 500) throw new InternalServerErrorException($"Server Error ({(int)code}): {body}", _logger);
            throw new HttpRequestException($"Unexpected response status {(int)code} ({code}): {body}");
        }
    }
}
