namespace CSPR.Cloud.Net.Clients.Api
{
    public class EndpointBuilder
    {
        private readonly string _baseUrl;

        public EndpointBuilder(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public string GetAccount(string publicKey)
        {
            return Endpoints.Account.GetAccount(_baseUrl, publicKey);
        }
    }
}
