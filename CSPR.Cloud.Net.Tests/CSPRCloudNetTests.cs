using CSPR.Cloud.Net.Clients;
using CSPR.Cloud.Net.Enums;
using CSPR.Cloud.Net.Extensions;
using CSPR.Cloud.Net.Objects.Config;
using CSPR.Cloud.Net.Parameters.Filtering.Account;
using CSPR.Cloud.Net.Parameters.Filtering.Bidder;
using CSPR.Cloud.Net.Parameters.Filtering.Block;
using CSPR.Cloud.Net.Parameters.Filtering.CentralizedAccountInfo;
using CSPR.Cloud.Net.Parameters.Filtering.Contract;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Account;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Bidder;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Block;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Contract;
using CSPR.Cloud.Net.Parameters.Sorting.Account;
using CSPR.Cloud.Net.Parameters.Sorting.Block;
using CSPR.Cloud.Net.Parameters.Sorting.CentralizedAccountInfo;
using CSPR.Cloud.Net.Parameters.Sorting.Contract;
using CSPR.Cloud.Net.Parameters.Wrapper.Accounts;
using CSPR.Cloud.Net.Parameters.Wrapper.Bidder;
using CSPR.Cloud.Net.Parameters.Wrapper.Block;
using CSPR.Cloud.Net.Parameters.Wrapper.CentralizedAccountInfo;
using CSPR.Cloud.Net.Parameters.Wrapper.Contract;

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
        private readonly string _testBidderPublicKey = "017d9aa0b86413d7ff9a9169182c53f0bacaa80d34c211adab007ed4876af17077";
        private readonly string _testCentralizedAccountHash = "fa12d2dd5547714f8c2754d418aa8c9d59dc88780350cb4254d622e2d4ef7e69";
        private readonly string _testContractHash = "d950b6fb1e487e054dff551ad1acd0106802bb482bf1e88630b6a1eec2de8ed9";
        private readonly string _testContractPackageHash = "dbb3284da4e20be62aeb332c653bfa715c7fa1ef6a73393cd36804b382f10d4e";
        private readonly string _testContractDeployHash = "94f35bd289abca1f91f34b56b101852d3dffc1f50567d9062a7df4d176070f0f";
        private readonly string _testOwnerPublicKey = "01dfe2a285f7841e4dc7fb65e960bfcbee6be271e8f32dfd90ee545de5e43384fb";
        private readonly string _testDelegatorPublicKey = "018afa98ca4be12d613617f7339a2d576950a2f9a92102ca4d6508ee31b54d2c02";
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
        // GetBidderAsync tests
        [Fact]
        public async Task GetBidderAsync_ReturnsExpectedData()
        {
            var parameters = new BidderRequestParameters
            {
                FilterParameters = new BidderFilterParameters
                {
                    EraId = "10935"
                }
            };
            var result = await _restClient.Testnet.GetBidderAsync(_testBidderPublicKey, parameters);
            Assert.Equal(_testBidderPublicKey, result.PublicKey);
        }
        [Fact]
        public async Task GetBidderAsync_ReturnsExpectedDataWithFilterParameters()
        {
            var parameters = new BidderRequestParameters
            {
                FilterParameters = new BidderFilterParameters
                {
                    EraId = "10935"
                }
            };
            var result = await _restClient.Testnet.GetBidderAsync(_testBidderPublicKey, parameters);
            Assert.Equal(_testBidderPublicKey, result.PublicKey);
        }
        [Fact]
        public async Task GetBidderAsync_ReturnsExpectedDataWithOptionalParameters()
        {
            var parameters = new BidderRequestParameters
            {
                FilterParameters = new BidderFilterParameters
                {
                    EraId = "10935"
                },
                OptionalParameters = new BidderOptionalParameters
                {
                    AccountInfo = true,
                    AveragePerformance = true,
                    CentralizedAccountInfo = true,
                }
            };
            var result = await _restClient.Testnet.GetBidderAsync(_testBidderPublicKey, parameters);

        }
        // Get BiddersAsync tests
        [Fact]
        public async Task GetBiddersAsync_ReturnsExpectedData()
        {
            var parameters = new BiddersRequestParameters
            {
                FilterParameters = new BiddersFilterParameters
                {
                    EraId = "10935",
                    IsActive = true,
                    PublicKey = new List<string>
                    {
                        _testBidderPublicKey
                    }
                }
            };
            var result = await _restClient.Testnet.GetBiddersAsync(parameters);
            Assert.True(result.ItemCount > 0);
        }
        // Get CentralizedAccountInfoAsync Tests
        [Fact]
        public async Task GetCentralizedAccountInfoAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetCentralizedAccountInfoAsync(_testCentralizedAccountHash);
            Assert.Equal(_testCentralizedAccountHash, result.AccountHash);
        }
        [Fact]
        public async Task GetCentralizedAccountInfosAsync_ReturnsExpectedData()
        {
            var parameters = new CentralizedAccountInfoRequestParameters
            {
                FilterParameters = new CentralizedAccountInfoFilterParameters
                {
                    AccountHashes = new List<string>
                    {
                        _testCentralizedAccountHash
                    }

                },
                SortingParameters = new CentralizedAccountInfoSortingParameters
                {
                    OrderByAccountHash = true,
                    SortType = SortType.Descending
                }
            };
            var result = await _restClient.Testnet.GetCentralizedAccountInfosAsync(parameters);
            Assert.True(result.ItemCount > 0);
        }
        // Get ContractAsync Tests
        [Fact]
        public async Task GetContractAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetContractAsync(_testContractHash);
            Assert.True(result.ContractHash == _testContractHash);
        }
        [Fact]
        public async Task GetContractAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new ContractRequestParameters
            {
                OptionalParameters = new ContractOptionalParameters
                {
                    ContractPackage = true
                }
            };
            var result = await _restClient.Testnet.GetContractAsync(_testContractHash, parameters);
            Assert.True(result.ContractPackage != null);
        }
        [Fact]
        public async Task GetContractsAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetContractsAsync();
            Assert.True(result.ItemCount > 0);
        }
        [Fact]
        public async Task GetContractsAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new ContractsRequestParameters
            {
                OptionalParameters = new ContractOptionalParameters
                {
                    ContractPackage = true
                }
            };
            var result = await _restClient.Testnet.GetContractsAsync(parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].ContractPackage != null);
        }
        [Fact]
        public async Task GetContractsAsync_WithFilterParametersContractPackageHash_ReturnsExpectedData()
        {
            var parameters = new ContractsRequestParameters
            {
                FilterParameters = new ContractsFilterParameters
                {
                    ContractPackageHash = _testContractPackageHash
                }
            };
            var result = await _restClient.Testnet.GetContractsAsync(parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].ContractPackageHash == _testContractPackageHash);
        }
        [Fact]
        public async Task GetContractsAsync_WithFilterParametersDeployHash_ReturnsExpectedData()
        {
            var parameters = new ContractsRequestParameters
            {
                FilterParameters = new ContractsFilterParameters
                {
                    DeployHash = _testContractDeployHash
                }
            };
            var result = await _restClient.Testnet.GetContractsAsync(parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].DeployHash == _testContractDeployHash);
        }
        [Fact]
        public async Task GetContractsAsync_WithSortingParameters_ReturnsExpectedData()
        {
            var parameters = new ContractsRequestParameters
            {
                SortingParameters = new ContractsSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Ascending
                },
                PageNumber = 1,
                PageSize = 100

            };
            var result = await _restClient.Testnet.GetContractsAsync(parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp < result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp < result.Data[2].Timestamp);
            Assert.True(result.Data[2].Timestamp < result.Data[3].Timestamp);
        }
        // Get ContractPackageAsync Tests
        [Fact]
        public async Task GetContractByContractPackageAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetContractsByContractPackageAsync(_testContractPackageHash);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].ContractPackageHash == _testContractPackageHash);
        }
        // Get ContractTypesAsync Tests
        [Fact]
        public async Task GetContractTypesAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetContractTypesAsync();
            Assert.True(result.Count > 0);
        }
        // GetContractEntryPointsAsync Tests
        [Fact]
        public async Task GetContractEntryPointsAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetContractEntryPointsAsync(_testContractHash);
            Assert.True(result.ItemCount > 0);
            var count = result.Data.Count();
            var equalCount = result.Data.Where(x => x.ContractHash == _testContractHash).Count();
            Assert.True(count == equalCount);
        }
        // GetContractEntryPointCostsAsync Tests
        [Fact]
        public async Task GetContractEntryPointCostsAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetContractEntryPointCostsAsync(_testContractHash, "activate_bid");
            Assert.True(result != null);
        }
        // GetContractPackageAsync Tests
        [Fact]
        public async Task GetContractPackageAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetContractPackageAsync(_testContractPackageHash);
            Assert.True(result.Data.ContractPackageHash == _testContractPackageHash);
        }
        // GetContractPackagesAsync Tests
        [Fact]
        public async Task GetContractPackagesAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetContractPackagesAsync();
            Assert.True(result.ItemCount > 0);
        }
        [Fact]
        public async Task GetContractPackagesAsync_WithFilterParameters_ReturnsExpectedData()
        {
            var parameters = new ContractPackageRequestParameters
            {
                FilterParameters = new ContractPackageFilterParameters
                {
                    OwnerPublicKey = _testOwnerPublicKey
                }
            };
            var result = await _restClient.Testnet.GetContractPackagesAsync(parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].OwnerPublicKey == _testOwnerPublicKey);
        }
        [Fact]
        public async Task GetContractPackagesAsync_WithSortingParameters_ReturnsExpectedData()
        {
            var parameters = new ContractPackageRequestParameters
            {
                SortingParameters = new ContractPackageSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Ascending
                },
                PageNumber = 1,
                PageSize = 10

            };
            var result = await _restClient.Testnet.GetContractPackagesAsync(parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp < result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp < result.Data[2].Timestamp);
            Assert.True(result.Data[2].Timestamp < result.Data[3].Timestamp);
            Assert.True(result.Data[3].Timestamp < result.Data[4].Timestamp);
            Assert.True(result.Data[4].Timestamp < result.Data[5].Timestamp);
            Assert.True(result.Data[5].Timestamp < result.Data[6].Timestamp);
            Assert.True(result.Data[6].Timestamp < result.Data[7].Timestamp);
            Assert.True(result.Data[7].Timestamp < result.Data[8].Timestamp);
            Assert.True(result.Data[8].Timestamp < result.Data[9].Timestamp);


        }
        [Fact]
        public async Task GetContractPackagesAsync_WithDeploysNumber_ReturnsExpectedData()
        {
            var parameters = new ContractPackageRequestParameters
            {
                PageNumber = 1,
                PageSize = 10,
                OptionalParameters = new ContractPackageOptionalParameters
                {
                    DeploysNumber = 63
                }

            };
            var result = await _restClient.Testnet.GetContractPackagesAsync(parameters);
            Assert.True(result.ItemCount > 0);
            var isDeployNumbersLowerThan = result.Data.Where(x => x.DeploysNumber < 63).Count();
            Assert.True(isDeployNumbersLowerThan == 0);
        }
        // GetAccountContractPackagesAsync Tests
        [Fact]
        public async Task GetAccountContractPackagesAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetAccountContractPackagesAsync(_testOwnerPublicKey);
            Assert.True(result.ItemCount > 0);
            var count = result.Data.Count();
            var equalCount = result.Data.Where(x => x.OwnerPublicKey == _testOwnerPublicKey).Count();
            Assert.True(count == equalCount);
        }
        // GetAccountDelegationsAsync Tests
        [Fact]
        public async Task GetAccountDelegationsAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetAccountDelegationsAsync(_testDelegatorPublicKey);
            Assert.True(result.ItemCount > 0);
            var IsAllDelegatorPublicKeyEqual = result.Data.Where(x => x.PublicKey == _testDelegatorPublicKey).Count() == 10;
            Assert.True(IsAllDelegatorPublicKeyEqual);
        }

        // GetValidatorDelegationsAsync Tests
        [Fact]
        public async Task GetValidatorDelegationsAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetValidatorDelegationsAsync(_test2PublicKey);
            Assert.True(result.ItemCount > 0);
            var IsAllDelegatorPublicKeyEqual = result.Data.Where(x => x.ValidatorPublicKey == _test2PublicKey).Count() == 10;
            Assert.True(IsAllDelegatorPublicKeyEqual);
        }
    }
}