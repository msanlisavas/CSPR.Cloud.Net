using CSPR.Cloud.Net.Clients;
using CSPR.Cloud.Net.Enums;
using CSPR.Cloud.Net.Extensions;
using CSPR.Cloud.Net.Objects.Config;
using CSPR.Cloud.Net.Parameters.Filtering.Account;
using CSPR.Cloud.Net.Parameters.Filtering.Block;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Account;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Block;
using CSPR.Cloud.Net.Parameters.Sorting.Account;
using CSPR.Cloud.Net.Parameters.Sorting.Block;
using CSPR.Cloud.Net.Parameters.Wrapper.Accounts;
using CSPR.Cloud.Net.Parameters.Wrapper.Block;

namespace CSPR.Cloud.Net.Tests
{
    public class CSPRCloudNetTests
    {
        private readonly CasperCloudRestClient _restClient;
        private readonly string _testPublicKey = "0106ca7c39cd272dbf21a86eeb3b36b7c26e2e9b94af64292419f7862936bca2ca";
        private readonly string _test2PublicKey = "012d58e05b2057a84115709e0a6ccf000c6a83b4e8dfa389a680c1ab001864f1f2";
        private readonly string _testAccountHash = "fa12d2dd5547714f8c2754d418aa8c9d59dc88780350cb4254d622e2d4ef7e69";
        private readonly string _test2AccountHash = "68bae9382be8706fa9533f33562eb1d58a879e42ccd1e8daf7368b17850304dc";
        private readonly string _testBlockHash = "bc1a9a481fa3f8b9e83c7cfa0ea87906c214345e20a5d76a5305dfb033d0510e";
        private readonly ulong _testBlockHeight = 2550824;
        public CSPRCloudNetTests()
        {
            _restClient = new CasperCloudRestClient(new CasperCloudClientConfig("55f79117-fc4d-4d60-9956-65423f39a06a")); // test key
        }


        [Fact]
        public async Task GetAccountAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetAccountAsync(_testPublicKey);
            Assert.Equal(_testPublicKey, result.PublicKey);
        }
        [Fact]
        public async Task GetAccountAsync_ReturnsExpectedDataWithOptionalParameterAuctionStatusTrue()
        {
            var parameters = new AccountsOptionalParameters
            {
                AuctionStatus = true,
            };
            var result = await _restClient.Testnet.GetAccountAsync(_testPublicKey, parameters);

            Assert.Equal(AuctionStatus.ActiveValidator.GetEnumMemberValue(), result.AuctionStatus);
            Assert.True(result.StakedBalance == null && result.AuctionStatus != null && result.DelegatedBalance == null && result.UndelegatedBalance == null);
        }
        [Fact]
        public async Task GetAccountAsync_ReturnsExpectedDataWithOptionalParameterStakedBalanceTrue()
        {
            var parameters = new AccountsOptionalParameters
            {
                StakedBalance = true,
            };
            var result = await _restClient.Testnet.GetAccountAsync(_testPublicKey, parameters);

            Assert.True(result.StakedBalance > 0 && result.AuctionStatus == null && result.DelegatedBalance == null && result.UndelegatedBalance == null);
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
                Sorting = new AccountsSortingParameters
                {

                    OrderByTotalBalance = true,
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
                Sorting = new AccountsSortingParameters
                {
                    OrderByBalance = true,
                    SortType = SortType.Ascending
                },
            };
            var result = await _restClient.Testnet.GetAccountsAsync(parameters);
            Assert.True(result.ItemCount > 0 && result.Data[0].Balance == null);
        }
        [Fact]
        public async Task GetAccountsAsync_ReturnsAccountHashesData()
        {
            var parameters = new AccountsRequestParameters
            {
                QueryParameters = new AccountsFilterParameters
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
        public async Task GetAccountsAsync_ReturnsMultipleAccountHashesData()
        {
            var parameters = new AccountsRequestParameters
            {
                QueryParameters = new AccountsFilterParameters
                {
                    AccountHashes = new List<string>
                    {
                        _testAccountHash,
                        _test2AccountHash
                    }
                },
                Sorting = new AccountsSortingParameters
                {
                    OrderByBalance = true,
                    OrderByTotalBalance = false,
                    SortType = SortType.Ascending
                }
            };
            var result = await _restClient.Testnet.GetAccountsAsync(parameters);
            Assert.True(result.Data[0].PublicKey == _test2PublicKey && result.Data[1].PublicKey == _testPublicKey);
        }
        [Fact]
        public async Task GetBlockAsync_ReturnsBlockDataUsingBlockHash()
        {
            var result = await _restClient.Testnet.GetBlockAsync(_testBlockHash);
            Assert.Equal(_testBlockHeight, result.BlockHeight);
            Assert.Equal(_testBlockHash, result.BlockHash);

        }
        [Fact]
        public async Task GetBlockAsync_ReturnsBlockDataUsingBlockHeight()
        {
            var result = await _restClient.Testnet.GetBlockAsync(_testBlockHeight.ToString());
            Assert.Equal(_testBlockHeight, result.BlockHeight);
            Assert.Equal(_testBlockHash, result.BlockHash);

        }
        [Fact]
        public async Task GetBlockAsync_ReturnsBlockDataUsingBlockHashAndProposerAccountInfo()
        {
            var parameters = new BlockOptionalParameters
            {
                ProposerAccountInfo = true
            };
            var result = await _restClient.Testnet.GetBlockAsync(_testBlockHash, parameters);
            Assert.True(result.ProposerAccountInfo != null);

        }
        [Fact]
        public async Task GetBlockAsync_ReturnsBlockDataUsingBlockHashAndProposerAccountInfoIsFalse()
        {
            var parameters = new BlockOptionalParameters
            {
                ProposerAccountInfo = false
            };
            var result = await _restClient.Testnet.GetBlockAsync(_testBlockHash, parameters);
            Assert.True(result.ProposerAccountInfo == null);

        }
        [Fact]
        public async Task GetBlocksAsync_ReturnsExpectedData()
        {

            var result = await _restClient.Testnet.GetBlocksAsync();
            Assert.True(result.ItemCount > 0);

        }
        [Fact]
        public async Task GetBlocksAsync_ReturnsExpectedDataOrderByBlockHeightAsc()
        {
            var parameters = new BlockRequestParameters
            {
                Sorting = new BlockSortingParameters
                {
                    OrderByBlockHeight = true,
                    SortType = SortType.Ascending
                },
                //FilterParameters = new BlockFilterParameters
                //{
                //    ProposerPublicKey = ""
                //},
                //OptionalParameters = new BlockOptionalParameters
                //{
                //    ProposerAccountInfo = true
                //},
                //PageNumber = 1,
                //PageSize = 10

            };
            var result = await _restClient.Testnet.GetBlocksAsync(parameters);
            Assert.True(result.Data[0].BlockHeight < result.Data[1].BlockHeight);

        }
        [Fact]
        public async Task GetBlocksAsync_ReturnsExpectedDataOrderByTimestampAsc()
        {
            var parameters = new BlockRequestParameters
            {
                Sorting = new BlockSortingParameters
                {
                    OrderByBlockHeight = false,
                    OrderByTimestamp = true,
                    SortType = SortType.Ascending
                },
                //FilterParameters = new BlockFilterParameters
                //{
                //    ProposerPublicKey = ""
                //},
                //OptionalParameters = new BlockOptionalParameters
                //{
                //    ProposerAccountInfo = true
                //},
                //PageNumber = 1,
                //PageSize = 10

            };
            var result = await _restClient.Testnet.GetBlocksAsync(parameters);
            Assert.True(result.Data[0].Timestamp < result.Data[1].Timestamp);

        }
        [Fact]
        public async Task GetBlocksAsync_ReturnsExpectedDataWithProposerPublicKey()
        {
            var parameters = new BlockRequestParameters
            {
                FilterParameters = new BlockFilterParameters
                {
                    ProposerPublicKey = _testPublicKey
                },
                //OptionalParameters = new BlockOptionalParameters
                //{
                //    ProposerAccountInfo = true
                //},
                //PageNumber = 1,
                //PageSize = 10

            };
            var result = await _restClient.Testnet.GetBlocksAsync(parameters);
            Assert.True(result.Data[0].ProposerPublicKey == _testPublicKey);

        }
        [Fact]
        public async Task GetBlocksAsync_ReturnsExpectedDataWithProposerAccountInfo()
        {
            var parameters = new BlockRequestParameters
            {
                OptionalParameters = new BlockOptionalParameters
                {
                    ProposerAccountInfo = true
                },
                //PageNumber = 1,
                //PageSize = 10

            };
            var result = await _restClient.Testnet.GetBlocksAsync(parameters);
            Assert.True(result.Data[0].ProposerAccountInfo != null);

        }
        [Fact]
        public async Task GetBlocksAsync_ReturnsExpectedDataWithPaginatedInfo()
        {
            var parameters = new BlockRequestParameters
            {
                PageNumber = 1,
                PageSize = 250

            };
            var result = await _restClient.Testnet.GetBlocksAsync(parameters);
            Assert.True(result.Data.Count == 250);

        }

        [Fact]
        public async Task GetValidatorBlocksAsync_ReturnsExpectedData()
        {

            var result = await _restClient.Testnet.GetValidatorBlocksAsync(_testPublicKey);
            Assert.True(result.ItemCount > 0);

        }
        [Fact]
        public async Task GetValidatorBlocksAsync_ReturnsExpectedDataOrderByBlockHeightAsc()
        {
            var parameters = new BlockRequestParameters
            {
                Sorting = new BlockSortingParameters
                {
                    OrderByBlockHeight = true,
                    SortType = SortType.Ascending
                },
                //FilterParameters = new BlockFilterParameters
                //{
                //    ProposerPublicKey = ""
                //},
                //OptionalParameters = new BlockOptionalParameters
                //{
                //    ProposerAccountInfo = true
                //},
                //PageNumber = 1,
                //PageSize = 10

            };
            var result = await _restClient.Testnet.GetValidatorBlocksAsync(_testPublicKey, parameters);
            Assert.True(result.Data[0].BlockHeight < result.Data[1].BlockHeight && result.Data[0].ProposerPublicKey == _testPublicKey);

        }
        [Fact]
        public async Task GetValidatorBlocksAsync_ReturnsExpectedDataOrderByTimestampAsc()
        {
            var parameters = new BlockRequestParameters
            {
                Sorting = new BlockSortingParameters
                {
                    OrderByBlockHeight = false,
                    OrderByTimestamp = true,
                    SortType = SortType.Ascending
                },
                //FilterParameters = new BlockFilterParameters
                //{
                //    ProposerPublicKey = ""
                //},
                //OptionalParameters = new BlockOptionalParameters
                //{
                //    ProposerAccountInfo = true
                //},
                //PageNumber = 1,
                //PageSize = 10

            };
            var result = await _restClient.Testnet.GetValidatorBlocksAsync(_testPublicKey, parameters);
            Assert.True(result.Data[0].Timestamp < result.Data[1].Timestamp && result.Data[0].ProposerPublicKey == _testPublicKey);

        }

        [Fact]
        public async Task GetValidatorBlocksAsync_ReturnsExpectedDataWithProposerAccountInfo()
        {
            var parameters = new BlockRequestParameters
            {
                OptionalParameters = new BlockOptionalParameters
                {
                    ProposerAccountInfo = true
                },
                //PageNumber = 1,
                //PageSize = 10

            };
            var result = await _restClient.Testnet.GetValidatorBlocksAsync(_testPublicKey, parameters);
            Assert.True(result.Data[0].ProposerAccountInfo != null);

        }
        [Fact]
        public async Task GetValidatorBlocksAsync_ReturnsExpectedDataWithPaginatedInfo()
        {
            var parameters = new BlockRequestParameters
            {
                PageNumber = 1,
                PageSize = 250

            };
            var result = await _restClient.Testnet.GetValidatorBlocksAsync(_testPublicKey, parameters);
            Assert.True(result.Data.Count == 250 && result.Data[0].ProposerPublicKey == _testPublicKey);

        }
    }
}