using CSPR.Cloud.Net.Clients;
using CSPR.Cloud.Net.Enums;
using CSPR.Cloud.Net.Extensions;
using CSPR.Cloud.Net.Objects.Config;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Account;
using CSPR.Cloud.Net.Parameters.Sorting.Account;
using CSPR.Cloud.Net.Parameters.Wrapper.Accounts;

namespace CSPR.Cloud.Net.Tests
{
    public class CSPRCloudNetTests
    {
        private readonly CasperCloudRestClient _restClient;
        private readonly string _testPublicKey = "012d58e05b2057a84115709e0a6ccf000c6a83b4e8dfa389a680c1ab001864f1f2";
        private readonly string _testAccountHash = "68bae9382be8706fa9533f33562eb1d58a879e42ccd1e8daf7368b17850304dc";
        public CSPRCloudNetTests()
        {
            _restClient = new CasperCloudRestClient(new CasperCloudClientConfig("55f79117-fc4d-4d60-9956-65423f39a06a"));
        }


        [Fact]
        public async Task GetAccountAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetAccountAsync(_testPublicKey);
            Assert.Equal(_testPublicKey, result.PublicKey);
        }
        [Fact]
        public async Task GetAccountAsync_ReturnsExpectedDataWithOptionalParameter()
        {
            var result = await _restClient.Testnet.GetAccountAsync(_testPublicKey, auctionStatus: true);
            Assert.Equal(AuctionStatus.ActiveValidator.GetEnumMemberValue(), result.AuctionStatus);
        }
        [Fact]
        public async Task GetAccountsAsync_ReturnsOptionalParametersData()
        {
            var parameters = new AccountsRequestParameters
            {
                OptionalParameters = new AccountsOptionalParameters
                {
                    AuctionStatus = true,
                    DelegatedBalance = true,
                    UndelegatingBalance = true

                },
            };
            var result = await _restClient.Testnet.GetAccountsAsync(parameters);
            Assert.True(result.ItemCount > 0);
        }

        [Fact]
        public async Task GetAccountsAsync_ReturnsOrderByBalanceDescendingData()
        {
            var parameters = new AccountsRequestParameters
            {
                OptionalParameters = new AccountsOptionalParameters
                {
                    AuctionStatus = true,
                    DelegatedBalance = true,
                    UndelegatingBalance = true

                },
                Sorting = new AccountsSorting
                {
                    OrderBy = new List<string>
                    {
                        "balance"
                    },
                    SortType = SortType.Descending
                }
            };
            var result = await _restClient.Testnet.GetAccountsAsync(parameters);
            Assert.True(result.ItemCount > 0 && result.Data[0].Balance >= result.Data[1].Balance);
        }
        [Fact]
        public async Task GetAccountsAsync_ReturnsOrderByBalanceAscendingData()
        {
            var parameters = new AccountsRequestParameters
            {
                OptionalParameters = new AccountsOptionalParameters
                {
                    AuctionStatus = true,
                    DelegatedBalance = true,
                    UndelegatingBalance = true

                },
                Sorting = new AccountsSorting
                {
                    OrderBy = new List<string>
                    {
                        "balance"
                    },
                    SortType = SortType.Ascending
                }
            };
            var result = await _restClient.Testnet.GetAccountsAsync(parameters);
            Assert.True(result.ItemCount > 0 && result.Data[0].Balance == null);
        }
        [Fact]
        public async Task GetAccountsAsync_ReturnsAccountHashesData()
        {
            var parameters = new AccountsRequestParameters
            {

                QueryParameters = new AccountsQueryParameters
                {
                    AccountHashes = new List<string>
                    {
                        _testAccountHash
                    }
                }
            };
            var result = await _restClient.Testnet.GetAccountsAsync(parameters);
            Assert.True(result.Data[0].PublicKey == _testPublicKey);
        }
        [Fact]
        public async Task GetAccountsAsync_ThrowsException_WithWrongOrderByParameters()
        {
            var parameters = new AccountsRequestParameters
            {
                OptionalParameters = new AccountsOptionalParameters
                {
                    AuctionStatus = true,
                    DelegatedBalance = true,
                    UndelegatingBalance = true

                },
                Sorting = new AccountsSorting
                {
                    OrderBy = new List<string>
                    {
                        "wrongparameter"
                    },
                    SortType = SortType.Ascending
                }
            };
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _restClient.Testnet.GetAccountsAsync(parameters);
            });
        }
    }
}