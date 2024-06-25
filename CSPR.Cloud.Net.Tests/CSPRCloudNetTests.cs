using CSPR.Cloud.Net.Clients;
using CSPR.Cloud.Net.Enums;
using CSPR.Cloud.Net.Extensions;
using CSPR.Cloud.Net.Objects.Config;
using CSPR.Cloud.Net.Parameters.Filtering.Account;
using CSPR.Cloud.Net.Parameters.Filtering.Bidder;
using CSPR.Cloud.Net.Parameters.Filtering.Block;
using CSPR.Cloud.Net.Parameters.Filtering.CentralizedAccountInfo;
using CSPR.Cloud.Net.Parameters.Filtering.Contract;
using CSPR.Cloud.Net.Parameters.Filtering.Deploy;
using CSPR.Cloud.Net.Parameters.Filtering.Ft;
using CSPR.Cloud.Net.Parameters.Filtering.Nft;
using CSPR.Cloud.Net.Parameters.Filtering.Rate;
using CSPR.Cloud.Net.Parameters.Filtering.Transfer;
using CSPR.Cloud.Net.Parameters.Filtering.Validator;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Account;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Bidder;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Block;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Contract;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Delegate;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Deploy;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Ft;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Nft;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Transfer;
using CSPR.Cloud.Net.Parameters.OptionalParameters.Validator;
using CSPR.Cloud.Net.Parameters.Sorting.Account;
using CSPR.Cloud.Net.Parameters.Sorting.Block;
using CSPR.Cloud.Net.Parameters.Sorting.CentralizedAccountInfo;
using CSPR.Cloud.Net.Parameters.Sorting.Contract;
using CSPR.Cloud.Net.Parameters.Sorting.Delegate;
using CSPR.Cloud.Net.Parameters.Sorting.Deploy;
using CSPR.Cloud.Net.Parameters.Sorting.Ft;
using CSPR.Cloud.Net.Parameters.Sorting.Nft;
using CSPR.Cloud.Net.Parameters.Sorting.Rate;
using CSPR.Cloud.Net.Parameters.Sorting.Transfer;
using CSPR.Cloud.Net.Parameters.Sorting.Validator;
using CSPR.Cloud.Net.Parameters.Wrapper.Accounts;
using CSPR.Cloud.Net.Parameters.Wrapper.Bidder;
using CSPR.Cloud.Net.Parameters.Wrapper.Block;
using CSPR.Cloud.Net.Parameters.Wrapper.CentralizedAccountInfo;
using CSPR.Cloud.Net.Parameters.Wrapper.Contract;
using CSPR.Cloud.Net.Parameters.Wrapper.Delegate;
using CSPR.Cloud.Net.Parameters.Wrapper.Deploy;
using CSPR.Cloud.Net.Parameters.Wrapper.Ft;
using CSPR.Cloud.Net.Parameters.Wrapper.Nft;
using CSPR.Cloud.Net.Parameters.Wrapper.Rate;
using CSPR.Cloud.Net.Parameters.Wrapper.Transfer;
using CSPR.Cloud.Net.Parameters.Wrapper.Validator;
using System.Numerics;

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
        private readonly string _testDeployHash = "88461218a5e972fcda1d764d7cc4edb2e0c3a538123b97890d484f43c55935f5";
        private readonly string _testDeployHash2 = "ffeee7e097a17702b11a68a870b5c0e7b0d3a2207228a5316dfaf2b4896dbca9";
        private readonly string _callerPublicKey = "018afa98ca4be12d613617f7339a2d576950a2f9a92102ca4d6508ee31b54d2c02";
        private readonly string _testFtTokenContractPackageHash = "de04671ba6226ecbb4c4e09c256459d2dec2d7dab305b5e57825894c07607069";
        private readonly string _testTokenIdOfContractPackage = "395dc1a096e8dd8e1fda68bdd9cc94093974f58af63c0e054a075880e51060e0";
        private readonly string _testAccountHashWithNft = "9c36101703214b13bc355dfb2fc7cfba0b553c780d7407943dfaadd8fb69de66";
        private readonly string _testAccountHashWithAlotOfNfts = "0188ed5156681e57c66d2f3f5baa38126607774a6cba86369fa89970426242413a";
        private readonly string _testContractPackageNFT = "5341882bae97a7368cdb007faa9f25735d2780d601114f82907fd83af2e9f508";
        private readonly string _testAccountPublicKeyWithNFT = "0131561311ded2e4c2bbb6d2497e231ae554afc86e7b6b9a083a84330830b8cfc5";
        private readonly string _testNFTOwnerHash = "2554a43ce4c90967adf811ca022526a3eab70ab4928bff3a81327e406092cdbc";
        private readonly string _testAccountHashWithNftContractPackageHash = "998af6825d77da15485baf4bb89aeef3f1dfb4a78841d149574b0be694ce4821";


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
            Assert.True(result.Data[0].Timestamp <= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp <= result.Data[2].Timestamp);
            Assert.True(result.Data[2].Timestamp <= result.Data[3].Timestamp);
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
            Assert.True(result.Data[0].Timestamp <= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp <= result.Data[2].Timestamp);
            Assert.True(result.Data[2].Timestamp <= result.Data[3].Timestamp);
            Assert.True(result.Data[3].Timestamp <= result.Data[4].Timestamp);
            Assert.True(result.Data[4].Timestamp <= result.Data[5].Timestamp);
            Assert.True(result.Data[5].Timestamp <= result.Data[6].Timestamp);
            Assert.True(result.Data[6].Timestamp <= result.Data[7].Timestamp);
            Assert.True(result.Data[7].Timestamp <= result.Data[8].Timestamp);
            Assert.True(result.Data[8].Timestamp <= result.Data[9].Timestamp);


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
        [Fact]
        public async Task GetAccountDelegationsAsync_WithSortingParameters_ReturnsExpectedData()
        {
            var parameters = new DelegationRequestParameters
            {
                SortingParameters = new DelegationSortingParameters
                {
                    OrderByStake = true,
                    SortType = SortType.Ascending
                }
            };
            var result = await _restClient.Testnet.GetAccountDelegationsAsync(_testDelegatorPublicKey, parameters);
            Assert.True(result.ItemCount > 0);
            var firstStake = BigInteger.Parse(result.Data[0].Stake);
            var secondStake = BigInteger.Parse(result.Data[1].Stake);
            Assert.True(firstStake <= secondStake);
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
        [Fact]
        public async Task GetValidatorDelegationsAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new DelegationRequestParameters
            {
                OptionalParameters = new DelegationOptionalParameters
                {
                    AccountInfo = true,
                    ValidatorAccountInfo = true,
                    CentralizedAccountInfo = true
                }
            };
            var result = await _restClient.Testnet.GetValidatorDelegationsAsync(_test2PublicKey, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].ValidatorAccountInfo != null);
        }
        // GetAccountDelegatorRewardsAsync Tests
        [Fact]
        public async Task GetAccountDelegatorRewardsAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetAccountDelegatorRewardsAsync(_testDelegatorPublicKey);
            Assert.True(result.ItemCount > 0);
            var IsAllDelegatorPublicKeyEqual = result.Data.Where(x => x.PublicKey == _testDelegatorPublicKey).Count() == 10;
            Assert.True(IsAllDelegatorPublicKeyEqual);
        }
        // Sorting not working here
        [Fact]
        public async Task GetAccountDelegatorRewardsAsync_WithSortingParameters_ReturnsExpectedData()
        {
            var parameters = new AccountDelegatorRewardRequestParameters
            {
                SortingParameters = new AccountDelegatorRewardSortingParameters
                {
                    OrderByEraId = true,
                    SortType = SortType.Descending
                }
            };
            var result = await _restClient.Testnet.GetAccountDelegatorRewardsAsync(_testDelegatorPublicKey, parameters);
            Assert.True(result.ItemCount > 0);
            var firstEra = result.Data[0].EraId;
            var secondEra = result.Data[1].EraId;
            Assert.True(firstEra <= secondEra);
        }
        // Optional Parameters
        [Fact]
        public async Task GetAccountDelegatorRewardsAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new AccountDelegatorRewardRequestParameters
            {
                OptionalParameters = new AccountDelegatorRewardOptionalParameters
                {
                    Rate = 1,
                }
            };
            var result = await _restClient.Testnet.GetAccountDelegatorRewardsAsync(_testDelegatorPublicKey, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Rate != null);
        }
        // GetTotalAccountDelegatorRewardsAsync Tests
        [Fact]
        public async Task GetTotalAccountDelegatorRewardsAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetTotalAccountDelegationRewards(_testDelegatorPublicKey);
            Assert.True(result > 0);
        }
        // GetTotalValidatorDelegationRewards Tests
        [Fact]
        public async Task GetTotalValidatorDelegationRewardsAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetTotalValidatorDelegationRewards(_test2PublicKey);
            Assert.True(result > 0);
        }
        // GetDeployAsync Tests
        [Fact]
        public async Task GetDeployAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetDeployAsync(_testDeployHash);
            Assert.True(result.Data.DeployHash == _testDeployHash);
        }
        [Fact]
        public async Task GetDeployAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new DeployRequestParameters
            {
                OptionalParameters = new DeployOptionalParameters
                {
                    AccountInfo = true,
                    CentralizedAccountInfo = true,
                    ContractPackage = true,
                    Contract = true,
                    ContractEntrypoint = true,
                    Rate = 1,
                    Transfers = true,
                    NFTTokenActions = true,
                    FTTokenActions = true
                }
            };
            var result = await _restClient.Testnet.GetDeployAsync(_testDeployHash2, parameters);
            Assert.True(result.Data.DeployHash == _testDeployHash2);
            Assert.True(result.Data.Contract != null);
            Assert.True(result.Data.ContractEntrypoint != null);
            Assert.True(result.Data.ContractPackage != null);
            Assert.True(result.Data.Rate != null);
            Assert.True(result.Data.NFTTokenAction.Count > 0);

        }
        // GetDeploysAsync Tests
        [Fact]
        public async Task GetDeploysAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetDeploysAsync();
            Assert.True(result.ItemCount > 0);
        }
        [Fact]
        public async Task GetDeploysAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new DeploysRequestParameters
            {
                OptionalParameters = new DeployOptionalParameters
                {
                    AccountInfo = true,
                    CentralizedAccountInfo = true,
                    ContractPackage = true,
                    Contract = true,
                    ContractEntrypoint = true,
                    Rate = 1,
                    Transfers = true,
                    NFTTokenActions = true,
                    FTTokenActions = true
                },
                PageSize = 200
            };
            var result = await _restClient.Testnet.GetDeploysAsync(parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.Contract != null);
            Assert.Contains(result.Data, value => value.ContractEntrypoint != null);
            Assert.Contains(result.Data, value => value.ContractPackage != null);
            Assert.Contains(result.Data, value => value.Rate != null);

        }
        [Fact]
        public async Task GetDeploysAsync_WithCallerPublicKeyFilterParameters_ReturnsExpectedData()
        {
            var parameters = new DeploysRequestParameters
            {
                FilterParameters = new DeploysFilterParameters
                {
                    CallerPublicKey = _callerPublicKey
                }
            };
            var result = await _restClient.Testnet.GetDeploysAsync(parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.CallerPublicKey == _callerPublicKey);
        }
        [Fact]
        public async Task GetDeploysAsync_WithContractHashFilterParameters_ReturnsExpectedData()
        {
            var parameters = new DeploysRequestParameters
            {
                FilterParameters = new DeploysFilterParameters
                {
                    ContractHash = _testContractHash
                }
            };
            var result = await _restClient.Testnet.GetDeploysAsync(parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.ContractHash == _testContractHash);
        }
        [Fact]
        public async Task GetDeploysAsync_WithContractPackageHashFilterParameters_ReturnsExpectedData()
        {
            var parameters = new DeploysRequestParameters
            {
                FilterParameters = new DeploysFilterParameters
                {
                    ContractPackageHash = _testContractPackageHash
                }
            };
            var result = await _restClient.Testnet.GetDeploysAsync(parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.ContractPackageHash == _testContractPackageHash);
        }
        [Fact]
        public async Task GetDeploysAsync_WithBlockHashFilterParameters_ReturnsExpectedData()
        {
            var parameters = new DeploysRequestParameters
            {
                FilterParameters = new DeploysFilterParameters
                {
                    BlockHash = _testBlockHash
                }
            };
            var result = await _restClient.Testnet.GetDeploysAsync(parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.BlockHash == _testBlockHash);
        }
        [Fact]
        public async Task GetDeploysAsync_WithFromBlockHeightFilterParameters_ReturnsExpectedData()
        {
            var parameters = new DeploysRequestParameters
            {
                FilterParameters = new DeploysFilterParameters
                {
                    FromBlockHeight = "3218428"
                }
            };
            var result = await _restClient.Testnet.GetDeploysAsync(parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data.Where(x => x.BlockHeight >= 3218428).Count() == result.Data.Count);
        }
        [Fact]
        public async Task GetDeploysAsync_WithToBlockHeightFilterParameters_ReturnsExpectedData()
        {
            var parameters = new DeploysRequestParameters
            {
                FilterParameters = new DeploysFilterParameters
                {
                    ToBlockHeight = "3218428"
                }
            };
            var result = await _restClient.Testnet.GetDeploysAsync(parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data.Where(x => x.BlockHeight <= 3218428).Count() == result.Data.Count);
        }
        [Fact]
        public async Task GetDeploysAsync_WithASCSortingParameters_ReturnsExpectedData()
        {
            var parameters = new DeploysRequestParameters
            {
                SortingParameters = new DeploysSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Ascending
                },
                PageNumber = 1,
                PageSize = 10

            };
            var result = await _restClient.Testnet.GetDeploysAsync(parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp <= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp <= result.Data[2].Timestamp);
            Assert.True(result.Data[2].Timestamp <= result.Data[3].Timestamp);
            Assert.True(result.Data[3].Timestamp <= result.Data[4].Timestamp);
            Assert.True(result.Data[4].Timestamp <= result.Data[5].Timestamp);
            Assert.True(result.Data[5].Timestamp <= result.Data[6].Timestamp);
            Assert.True(result.Data[6].Timestamp <= result.Data[7].Timestamp);
            Assert.True(result.Data[7].Timestamp <= result.Data[8].Timestamp);
            Assert.True(result.Data[8].Timestamp <= result.Data[9].Timestamp);

        }
        [Fact]
        public async Task GetDeploysAsync_WithDESCSortingParameters_ReturnsExpectedData()
        {
            var parameters = new DeploysRequestParameters
            {
                SortingParameters = new DeploysSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Descending
                },
                PageNumber = 1,
                PageSize = 10

            };
            var result = await _restClient.Testnet.GetDeploysAsync(parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp >= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp >= result.Data[2].Timestamp);
            Assert.True(result.Data[2].Timestamp >= result.Data[3].Timestamp);
            Assert.True(result.Data[3].Timestamp >= result.Data[4].Timestamp);
            Assert.True(result.Data[4].Timestamp >= result.Data[5].Timestamp);
            Assert.True(result.Data[5].Timestamp >= result.Data[6].Timestamp);
            Assert.True(result.Data[6].Timestamp >= result.Data[7].Timestamp);
            Assert.True(result.Data[7].Timestamp >= result.Data[8].Timestamp);
            Assert.True(result.Data[8].Timestamp >= result.Data[9].Timestamp);

        }
        // GetAccountDeploysAsync Tests
        [Fact]
        public async Task GetAccountDeploysAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetAccountDeploysAsync(_callerPublicKey);
            Assert.True(result.ItemCount > 0);
        }
        [Fact]
        public async Task GetAccountDeploysAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new AccountDeploysRequestParameters
            {
                OptionalParameters = new DeployOptionalParameters
                {
                    AccountInfo = true,
                    CentralizedAccountInfo = true,
                    ContractPackage = true,
                    Contract = true,
                    ContractEntrypoint = true,
                    Rate = 1,
                    Transfers = true,
                    NFTTokenActions = true,
                    FTTokenActions = true
                },
                PageSize = 200
            };
            var result = await _restClient.Testnet.GetAccountDeploysAsync("01cde2af414349be59d01206e3ffd6b9f1f49a4292b2c219f8d7d53f55d5e39b17", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.Contract != null);
            Assert.Contains(result.Data, value => value.ContractEntrypoint != null);
            Assert.Contains(result.Data, value => value.ContractPackage != null);
            Assert.Contains(result.Data, value => value.Rate != null);

        }
        [Fact]
        public async Task GetAccountDeploysAsync_WithContractHashFilterParameters_ReturnsExpectedData()
        {
            var parameters = new AccountDeploysRequestParameters
            {
                FilterParameters = new AccountDeploysFilterParameters
                {
                    ContractHash = "d950b6fb1e487e054dff551ad1acd0106802bb482bf1e88630b6a1eec2de8ed9"
                }
            };
            var result = await _restClient.Testnet.GetAccountDeploysAsync("01a35887f3962a6a232e8e11fa7d4567b6866d68850974aad7289ef287676825f6", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.ContractHash == "d950b6fb1e487e054dff551ad1acd0106802bb482bf1e88630b6a1eec2de8ed9");
        }
        [Fact]
        public async Task GetAccountDeploysAsync_WithContractPackageHashFilterParameters_ReturnsExpectedData()
        {
            var parameters = new AccountDeploysRequestParameters
            {
                FilterParameters = new AccountDeploysFilterParameters
                {
                    ContractPackageHash = "dbb3284da4e20be62aeb332c653bfa715c7fa1ef6a73393cd36804b382f10d4e"
                }
            };
            var result = await _restClient.Testnet.GetAccountDeploysAsync("01a35887f3962a6a232e8e11fa7d4567b6866d68850974aad7289ef287676825f6", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.ContractPackageHash == _testContractPackageHash);
        }
        [Fact]
        public async Task GetAccountDeploysAsync_WithFromBlockHeightFilterParameters_ReturnsExpectedData()
        {
            var parameters = new AccountDeploysRequestParameters
            {
                FilterParameters = new AccountDeploysFilterParameters
                {
                    FromBlockHeight = "3218428"
                }
            };
            var result = await _restClient.Testnet.GetAccountDeploysAsync(_callerPublicKey, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data.Where(x => x.BlockHeight >= 3218428 && x.CallerPublicKey == _callerPublicKey).Count() == result.Data.Count);
        }
        [Fact]
        public async Task GetAccountDeploysAsync_WithToBlockHeightFilterParameters_ReturnsExpectedData()
        {
            var parameters = new AccountDeploysRequestParameters
            {
                FilterParameters = new AccountDeploysFilterParameters
                {
                    ToBlockHeight = "3218428"
                }
            };
            var result = await _restClient.Testnet.GetAccountDeploysAsync(_callerPublicKey, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data.Where(x => x.BlockHeight <= 3218428 && x.CallerPublicKey == _callerPublicKey).Count() == result.Data.Count);
        }
        [Fact]
        public async Task GetAccountDeploysAsync_WithASCSortingParameters_ReturnsExpectedData()
        {
            var parameters = new AccountDeploysRequestParameters
            {
                SortingParameters = new DeploysSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Ascending
                },
                PageNumber = 1,
                PageSize = 10

            };
            var result = await _restClient.Testnet.GetAccountDeploysAsync(_callerPublicKey, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp <= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp <= result.Data[2].Timestamp);
            Assert.True(result.Data[2].Timestamp <= result.Data[3].Timestamp);
            Assert.True(result.Data[3].Timestamp <= result.Data[4].Timestamp);
            Assert.True(result.Data[4].Timestamp <= result.Data[5].Timestamp);
            Assert.True(result.Data[5].Timestamp <= result.Data[6].Timestamp);
            Assert.True(result.Data[6].Timestamp <= result.Data[7].Timestamp);
            Assert.True(result.Data[7].Timestamp <= result.Data[8].Timestamp);
            Assert.True(result.Data[8].Timestamp <= result.Data[9].Timestamp);

        }
        [Fact]
        public async Task GetAccountDeploysAsync_WithDESCSortingParameters_ReturnsExpectedData()
        {
            var parameters = new AccountDeploysRequestParameters
            {
                SortingParameters = new DeploysSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Descending
                },
                PageNumber = 1,
                PageSize = 10

            };
            var result = await _restClient.Testnet.GetAccountDeploysAsync(_callerPublicKey, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp >= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp >= result.Data[2].Timestamp);
            Assert.True(result.Data[2].Timestamp >= result.Data[3].Timestamp);
            Assert.True(result.Data[3].Timestamp >= result.Data[4].Timestamp);
            Assert.True(result.Data[4].Timestamp >= result.Data[5].Timestamp);
            Assert.True(result.Data[5].Timestamp >= result.Data[6].Timestamp);
            Assert.True(result.Data[6].Timestamp >= result.Data[7].Timestamp);
            Assert.True(result.Data[7].Timestamp >= result.Data[8].Timestamp);
            Assert.True(result.Data[8].Timestamp >= result.Data[9].Timestamp);

        }
        // GetBlockDeploysAsync Tests
        [Fact]
        public async Task GetBlockDeploysAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetBlockDeploysAsync(_testBlockHash);
            Assert.True(result.ItemCount > 0);
        }
        [Fact]
        public async Task GetBlockDeploysAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new BlockDeploysRequestParameters
            {
                OptionalParameters = new DeployOptionalParameters
                {
                    AccountInfo = true,
                    CentralizedAccountInfo = true,
                    ContractPackage = true,
                    Contract = true,
                    ContractEntrypoint = true,
                    Rate = 1,
                    Transfers = true,
                    NFTTokenActions = true,
                    FTTokenActions = true
                },
                PageSize = 200
            };
            var result = await _restClient.Testnet.GetBlockDeploysAsync(_testBlockHash, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.Contract != null);
            Assert.Contains(result.Data, value => value.ContractEntrypoint != null);
            Assert.Contains(result.Data, value => value.ContractPackage != null);
            Assert.Contains(result.Data, value => value.Rate != null);

        }
        [Fact]
        public async Task GetBlockDeploysAsync_WithContractHashFilterParameters_ReturnsExpectedData()
        {
            var parameters = new BlockDeploysRequestParameters
            {
                FilterParameters = new BlockDeploysFilterParameters
                {
                    ContractHash = "4376c1707d2c844bdc8d24def3ae7252f3e011fb446c9108a4418152938468f2"
                }
            };
            var result = await _restClient.Testnet.GetBlockDeploysAsync("77f57ec996017340dfdd832c24608aad81f71a4f77da6ed4e4f32bab1717e41f", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.ContractHash == "4376c1707d2c844bdc8d24def3ae7252f3e011fb446c9108a4418152938468f2");
        }
        [Fact]
        public async Task GetBlockDeploysAsync_WithContractPackageHashFilterParameters_ReturnsExpectedData()
        {
            var parameters = new BlockDeploysRequestParameters
            {
                FilterParameters = new BlockDeploysFilterParameters
                {
                    ContractPackageHash = "405d23bf75777993474c63c65f1803f3b70387d23ca1c4a391c02927ee69ca83"
                }
            };
            var result = await _restClient.Testnet.GetBlockDeploysAsync("77f57ec996017340dfdd832c24608aad81f71a4f77da6ed4e4f32bab1717e41f", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.ContractPackageHash == "405d23bf75777993474c63c65f1803f3b70387d23ca1c4a391c02927ee69ca83");
        }
        [Fact]
        public async Task GetBlockDeploysAsync_WithCallerPublicKeyFilterParameters_ReturnsExpectedData()
        {
            var parameters = new BlockDeploysRequestParameters
            {
                FilterParameters = new BlockDeploysFilterParameters
                {
                    CallerPublicKey = "01f8c5cb2750adca87490dee7d71344775e479d1d63b5976c664eae4dc5d2c246b"
                }
            };
            var result = await _restClient.Testnet.GetBlockDeploysAsync("77f57ec996017340dfdd832c24608aad81f71a4f77da6ed4e4f32bab1717e41f", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data.Where(x => x.BlockHeight >= 3218428 && x.CallerPublicKey == "01f8c5cb2750adca87490dee7d71344775e479d1d63b5976c664eae4dc5d2c246b").Count() == result.Data.Count);
        }
        [Fact]
        public async Task GetBlockDeploysAsync_WithASCSortingParameters_ReturnsExpectedData()
        {
            var parameters = new BlockDeploysRequestParameters
            {
                SortingParameters = new DeploysSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Ascending
                },
                PageNumber = 1,
                PageSize = 10

            };
            var result = await _restClient.Testnet.GetBlockDeploysAsync("a0c910531685b163b00b131739fb0f68dd84af2dbf51408b39f4acee3327363a", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp <= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp <= result.Data[2].Timestamp);

        }
        [Fact]
        public async Task GetBlockDeploysAsync_WithDESCSortingParameters_ReturnsExpectedData()
        {
            var parameters = new BlockDeploysRequestParameters
            {
                SortingParameters = new DeploysSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Descending
                },
                PageNumber = 1,
                PageSize = 10

            };
            var result = await _restClient.Testnet.GetBlockDeploysAsync("a0c910531685b163b00b131739fb0f68dd84af2dbf51408b39f4acee3327363a", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp >= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp >= result.Data[2].Timestamp);

        }
        // GetDeployExecutionTypesAsync Tests
        [Fact]
        public async Task GetDeployExecutionTypesAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetDeployExecutionTypesAsync();
            Assert.True(result.Data.Count > 0);
        }
        // GetFungibleTokenActions Tests
        [Fact]
        public async Task GetFungibleTokenActionsAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetFungibleTokenActionsAsync();
            Assert.True(result.ItemCount > 0);
        }
        [Fact]
        public async Task GetFungibleTokenActionsAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new FTActionRequestParameters
            {
                OptionalParameters = new FTActionOptionalParameters
                {
                    ContractPackage = true,
                    Deploy = true
                },
                PageSize = 200
            };
            var result = await _restClient.Testnet.GetFungibleTokenActionsAsync(parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.Deploy != null);
            Assert.Contains(result.Data, value => value.ContractPackage != null);

        }
        [Fact]
        public async Task GetFungibleTokenActionsAsync_WithContractPackageHashFilterParameters_ReturnsExpectedData()
        {
            var parameters = new FTActionRequestParameters
            {
                FilterParameters = new FTActionFilterParameters
                {
                    ContractPackageHash = "de04671ba6226ecbb4c4e09c256459d2dec2d7dab305b5e57825894c07607069"
                }
            };
            var result = await _restClient.Testnet.GetFungibleTokenActionsAsync(parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.ContractPackageHash == "de04671ba6226ecbb4c4e09c256459d2dec2d7dab305b5e57825894c07607069");
        }
        [Fact]
        public async Task GetFungibleTokenActionsAsync_WithAccountHashFilterParameters_ReturnsExpectedData()
        {
            var parameters = new FTActionRequestParameters
            {
                FilterParameters = new FTActionFilterParameters
                {
                    AccountHash = "0f758c10859a3dcf2a041ad3505e0e12754e66662e7e1a6d9d76af43395197a2"
                }
            };
            var result = await _restClient.Testnet.GetFungibleTokenActionsAsync(parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.ToHash == "0f758c10859a3dcf2a041ad3505e0e12754e66662e7e1a6d9d76af43395197a2");
        }
        // GetAccountFungibleTokenActions Tests
        [Fact]
        public async Task GetAccountFungibleTokenActionsAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetAccountFungibleTokenActionsAsync("0f758c10859a3dcf2a041ad3505e0e12754e66662e7e1a6d9d76af43395197a2");
            Assert.True(result.ItemCount > 0);
        }
        [Fact]
        public async Task GetAccountFungibleTokenActionsAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new FTAccountActionRequestParameters
            {
                OptionalParameters = new FTActionOptionalParameters
                {
                    ContractPackage = true,
                    Deploy = true
                },
                PageSize = 200
            };
            var result = await _restClient.Testnet.GetAccountFungibleTokenActionsAsync("0f758c10859a3dcf2a041ad3505e0e12754e66662e7e1a6d9d76af43395197a2", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.Deploy != null);
            Assert.Contains(result.Data, value => value.ContractPackage != null);

        }
        [Fact]
        public async Task GetAccountFungibleTokenActionsAsync_WithContractPackageHashFilterParameters_ReturnsExpectedData()
        {
            var parameters = new FTAccountActionRequestParameters
            {
                FilterParameters = new FTAccountActionFilterParameters
                {
                    FromBlockHeight = "3212781"
                }
            };
            var result = await _restClient.Testnet.GetAccountFungibleTokenActionsAsync("0f758c10859a3dcf2a041ad3505e0e12754e66662e7e1a6d9d76af43395197a2", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.ContractPackageHash == "de04671ba6226ecbb4c4e09c256459d2dec2d7dab305b5e57825894c07607069");
        }
        [Fact]
        public async Task GetAccountFungibleTokenActionsAsync_WithContractPackageHashFilterParameters_ShouldntReturnsExpectedData()
        {
            var parameters = new FTAccountActionRequestParameters
            {
                FilterParameters = new FTAccountActionFilterParameters
                {
                    FromBlockHeight = "3212783"
                }
            };
            var result = await _restClient.Testnet.GetAccountFungibleTokenActionsAsync("0f758c10859a3dcf2a041ad3505e0e12754e66662e7e1a6d9d76af43395197a2", parameters);
            Assert.True(result.ItemCount == 0);
        }
        [Fact]
        public async Task GetAccountFungibleTokenActionsAsync_WithAccountHashFilterParameters_ReturnsExpectedData()
        {
            var parameters = new FTAccountActionRequestParameters
            {
                FilterParameters = new FTAccountActionFilterParameters
                {
                    ToBlockHeight = "3224327"
                }
            };
            var result = await _restClient.Testnet.GetAccountFungibleTokenActionsAsync("0f758c10859a3dcf2a041ad3505e0e12754e66662e7e1a6d9d76af43395197a2", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.ToHash == "0f758c10859a3dcf2a041ad3505e0e12754e66662e7e1a6d9d76af43395197a2");
        }
        [Fact]
        public async Task GetAccountFungibleTokenActionsAsync_WithASCSortParameters_ShouldntReturnsExpectedData()
        {
            var parameters = new FTAccountActionRequestParameters
            {
                SortingParameters = new FTAccountActionSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Ascending
                }
            };
            var result = await _restClient.Testnet.GetAccountFungibleTokenActionsAsync("ddfc69e06130a5cb1aaf51254cf913a0ab0c60922f4c33261bbcdbdc8156421a", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp <= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp <= result.Data[2].Timestamp);
        }
        [Fact]
        public async Task GetAccountFungibleTokenActionsAsync_WithDESCSortParameters_ShouldntReturnsExpectedData()
        {
            var parameters = new FTAccountActionRequestParameters
            {
                SortingParameters = new FTAccountActionSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Descending
                }
            };
            var result = await _restClient.Testnet.GetAccountFungibleTokenActionsAsync("ddfc69e06130a5cb1aaf51254cf913a0ab0c60922f4c33261bbcdbdc8156421a", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp >= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp >= result.Data[2].Timestamp);
        }
        // GetContractPackageFungibleTokenActions Tests
        [Fact]
        public async Task GetContractPackageFungibleTokenActionsAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetContractPackageFungibleTokenActionsAsync(_testFtTokenContractPackageHash);
            Assert.True(result.ItemCount > 0);
        }
        [Fact]
        public async Task GetContractPackageFungibleTokenActionsAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new FTContractPackageActionRequestParameters
            {
                OptionalParameters = new FTActionOptionalParameters
                {
                    ContractPackage = true,
                    Deploy = true,
                    FromPublicKey = true,
                    ToPublicKey = true
                },
                PageSize = 200
            };
            var result = await _restClient.Testnet.GetContractPackageFungibleTokenActionsAsync(_testFtTokenContractPackageHash, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.Deploy != null);
            Assert.Contains(result.Data, value => value.ContractPackage != null);

        }
        [Fact]
        public async Task GetContractPackageFungibleTokenActionsAsync_WithContractPackageHashFilterParameters_ReturnsExpectedData()
        {
            var parameters = new FTContractPackageActionRequestParameters
            {
                FilterParameters = new FTContractPackageActionFilterParameters
                {
                    FromBlockHeight = "3212781"
                }
            };
            var result = await _restClient.Testnet.GetContractPackageFungibleTokenActionsAsync(_testFtTokenContractPackageHash, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.ContractPackageHash == _testFtTokenContractPackageHash);
        }
        [Fact]
        public async Task GetContractPackageFungibleTokenActionsAsync_WithFromBlockHeightFilterParameters_ShouldntReturnsExpectedData()
        {
            var parameters = new FTContractPackageActionRequestParameters
            {
                FilterParameters = new FTContractPackageActionFilterParameters
                {
                    FromBlockHeight = "3212783"
                }
            };
            var result = await _restClient.Testnet.GetContractPackageFungibleTokenActionsAsync(_testFtTokenContractPackageHash, parameters);
            Assert.True(result.ItemCount == 0);
        }
        [Fact]
        public async Task GetContractPackageFungibleTokenActionsAsync_WithToBlockHeightFilterParameters_ReturnsExpectedData()
        {
            var parameters = new FTContractPackageActionRequestParameters
            {
                FilterParameters = new FTContractPackageActionFilterParameters
                {
                    ToBlockHeight = "3224327"
                }
            };
            var result = await _restClient.Testnet.GetContractPackageFungibleTokenActionsAsync(_testFtTokenContractPackageHash, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.ToHash == "0f758c10859a3dcf2a041ad3505e0e12754e66662e7e1a6d9d76af43395197a2");
        }
        [Fact]
        public async Task GetContractPackageFungibleTokenActionsAsync_WithASCSortParameters_ShouldntReturnsExpectedData()
        {
            var parameters = new FTContractPackageActionRequestParameters
            {
                SortingParameters = new FTContractPackageActionSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Ascending
                }
            };
            var result = await _restClient.Testnet.GetContractPackageFungibleTokenActionsAsync(_testFtTokenContractPackageHash, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp <= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp <= result.Data[2].Timestamp);
        }
        [Fact]
        public async Task GetContractPackageFungibleTokenActionsAsync_WithDESCSortParameters_ShouldntReturnsExpectedData()
        {
            var parameters = new FTContractPackageActionRequestParameters
            {
                SortingParameters = new FTContractPackageActionSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Descending
                }
            };
            var result = await _restClient.Testnet.GetContractPackageFungibleTokenActionsAsync(_testFtTokenContractPackageHash, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp >= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp >= result.Data[2].Timestamp);
        }
        // GetAccountFungibleTokenOwnershipAsync Tests
        [Fact]
        public async Task GetAccountFungibleTokenOwnershipAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetAccountFungibleTokenOwnershipAsync(_testAccountHash);
            Assert.True(result.ItemCount > 0);
        }
        [Fact]
        public async Task GetAccountFungibleTokenOwnershipAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new FTAccountOwnershipRequestParameters
            {
                OptionalParameters = new FTAccountOwnershipOptionalParameters
                {
                    ContractPackage = true
                },
                PageSize = 200
            };
            var result = await _restClient.Testnet.GetAccountFungibleTokenOwnershipAsync(_testAccountHash, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.OwnerHash == _testAccountHash);
            Assert.True(result.Data[0].ContractPackage != null);

        }
        // GetContractPackageFungibleTokenOwnershipAsync Tests
        [Fact]
        public async Task GetContractPackageFungibleTokenOwnershipAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetContractPackageFungibleTokenOwnershipAsync(_testFtTokenContractPackageHash);
            Assert.True(result.ItemCount > 0);
        }
        // GetNonFungibleToken Tests
        [Fact]
        public async Task GetNonFungibleTokenAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetNonFungibleTokenAsync(_testContractPackageHash, _testTokenIdOfContractPackage);
            Assert.True(result != null);
            Assert.True(result.Data.ContractPackageHash == _testContractPackageHash);
        }
        // GetAccountNfts Tests
        [Fact]
        public async Task GetAccountNftsAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetAccountNFTsAsync(_testAccountHashWithNft);
            Assert.True(result.ItemCount > 0);
        }
        [Fact]
        public async Task GetAccountNftsAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new NFTAccountRequestParameters
            {
                OptionalParameters = new NFTOptionalParameters
                {
                    ContractPackage = true,
                    OwnerPublicKey = true
                },
                PageSize = 200
            };
            var result = await _restClient.Testnet.GetAccountNFTsAsync(_testAccountHashWithNft, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => !string.IsNullOrWhiteSpace(value.OwnerPublicKey));
            Assert.Contains(result.Data, value => value.ContractPackage != null);

        }
        [Fact]
        public async Task GetAccountNftsAsync_WithFromBlockHeightFilterParameters_ReturnsExpectedData()
        {
            var parameters = new NFTAccountRequestParameters
            {
                FilterParameters = new NFTAccountFilterParameters
                {
                    FromBlockHeight = "1349344"
                }
            };
            var result = await _restClient.Testnet.GetAccountNFTsAsync(_testAccountHashWithNft, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.OwnerHash == _testAccountHashWithNft);
        }
        [Fact]
        public async Task GetAccountNftsAsync_WithFromBlockHeightFilterParameters_ShouldntReturnData()
        {
            var parameters = new NFTAccountRequestParameters
            {
                FilterParameters = new NFTAccountFilterParameters
                {
                    FromBlockHeight = "1349345"
                }
            };
            var result = await _restClient.Testnet.GetAccountNFTsAsync(_testAccountHashWithNft, parameters);
            Assert.True(result.ItemCount == 0);
        }
        [Fact]
        public async Task GetAccountNftsAsync_WithToBlockHeightFilterParameters_ReturnsExpectedData()
        {
            var parameters = new NFTAccountRequestParameters
            {
                FilterParameters = new NFTAccountFilterParameters
                {
                    ToBlockHeight = "1349344"
                }
            };
            var result = await _restClient.Testnet.GetAccountNFTsAsync(_testAccountHashWithNft, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.OwnerHash == _testAccountHashWithNft);
        }
        [Fact]
        public async Task GetAccountNftsAsync_WithToBlockHeightFilterParameters_ShouldntReturnData()
        {
            var parameters = new NFTAccountRequestParameters
            {
                FilterParameters = new NFTAccountFilterParameters
                {
                    ToBlockHeight = "1349343"
                }
            };
            var result = await _restClient.Testnet.GetAccountNFTsAsync(_testAccountHashWithNft, parameters);
            Assert.True(result.ItemCount == 0);
        }
        [Fact]
        public async Task GetAccountNftsAsync_WithASCSortParameters_ReturnsExpectedData()
        {
            var parameters = new NFTAccountRequestParameters
            {
                SortingParameters = new NFTAccountSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Ascending
                }
            };
            var result = await _restClient.Testnet.GetAccountNFTsAsync(_testAccountHashWithAlotOfNfts, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp <= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp <= result.Data[2].Timestamp);
            Assert.True(result.Data[2].Timestamp <= result.Data[3].Timestamp);
            Assert.True(result.Data[3].Timestamp <= result.Data[4].Timestamp);
            Assert.True(result.Data[4].Timestamp <= result.Data[5].Timestamp);
            Assert.True(result.Data[5].Timestamp <= result.Data[6].Timestamp);

        }
        [Fact]
        public async Task GetAccountNftsAsync_WithDESCSortParameters_ReturnsExpectedData()
        {
            var parameters = new NFTAccountRequestParameters
            {
                SortingParameters = new NFTAccountSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Descending
                }
            };
            var result = await _restClient.Testnet.GetAccountNFTsAsync(_testAccountHashWithAlotOfNfts, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp >= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp >= result.Data[2].Timestamp);
            Assert.True(result.Data[2].Timestamp >= result.Data[3].Timestamp);
            Assert.True(result.Data[3].Timestamp >= result.Data[4].Timestamp);
            Assert.True(result.Data[4].Timestamp >= result.Data[5].Timestamp);
            Assert.True(result.Data[5].Timestamp >= result.Data[6].Timestamp);

        }
        // GetContractPackageNfts Tests
        [Fact]
        public async Task GetContractPackageNftsAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetContractPackageNFTsAsync(_testContractPackageHash);
            Assert.True(result.ItemCount > 0);
        }
        [Fact]
        public async Task GetContractPackageNftsAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new NFTContractPackageRequestParameters
            {
                OptionalParameters = new NFTContractPackageOptionalParameters
                {
                    OwnerPublicKey = true
                },
                PageSize = 200
            };
            var result = await _restClient.Testnet.GetContractPackageNFTsAsync(_testContractPackageNFT, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => !(String.IsNullOrWhiteSpace(value.OwnerPublicKey)));

        }
        [Fact]
        public async Task GetContractPackageNftsAsync_WithFromBlockHeightFilterParameters_ReturnsExpectedData()
        {
            var parameters = new NFTContractPackageRequestParameters
            {
                FilterParameters = new NFTContractPackageFilterParameters
                {
                    FromBlockHeight = "3000000",
                    ToBlockHeight = "3250000"
                },
                PageSize = 200
            };
            var result = await _restClient.Testnet.GetContractPackageNFTsAsync(_testContractPackageHash, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value =>
                value.BlockHeight >= ulong.Parse(parameters.FilterParameters.FromBlockHeight) &&
                value.BlockHeight <= ulong.Parse(parameters.FilterParameters.ToBlockHeight));

        }
        [Fact]
        public async Task GetContractPackageNftsAsync_WithFromBlockHeightFilterParameters_ShouldntReturnData()
        {
            var parameters = new NFTContractPackageRequestParameters
            {
                FilterParameters = new NFTContractPackageFilterParameters
                {
                    FromBlockHeight = "1349344",
                    ToBlockHeight = "1349500"
                }
            };
            var result = await _restClient.Testnet.GetContractPackageNFTsAsync(_testContractPackageHash, parameters);
            Assert.True(result.ItemCount == 0);
        }
        // GetNftStandard Tests
        [Fact]
        public async Task GetNftStandardAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetNFTStandardsAsync();
            Assert.True(result.Data.Count > 0);
        }
        // Get off-chain NFT metadata statuses Tests
        [Fact]
        public async Task GetOffChainNftMetadataStatusesAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetOffchainNFTMetadataStatusesAsync();
            Assert.True(result.Data.Count > 0);
        }
        // Get contract package NFT actions for a token Tests
        [Fact]
        public async Task GetContractPackageNftActionsAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetContractPackageNFTActionsForATokenAsync(_testContractPackageHash, _testTokenIdOfContractPackage);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].ContractPackageHash == _testContractPackageHash);
            Assert.True(result.Data[0].TokenId == _testTokenIdOfContractPackage);
        }
        [Fact]
        public async Task GetContractPackageNftActionsAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new NFTContractPackageTokenActionsRequestParameters
            {
                OptionalParameters = new NFTContractPackageTokenActionsOptionalParameters
                {
                    ContractPackage = true,
                    Deploy = true,
                    FromPublicKey = true,
                    ToPublicKey = true
                },
                PageSize = 200
            };
            var result = await _restClient.Testnet.GetContractPackageNFTActionsForATokenAsync(_testContractPackageNFT, "200", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.Deploy != null);
            Assert.Contains(result.Data, value => value.ContractPackage != null);
            Assert.Contains(result.Data, value => !string.IsNullOrWhiteSpace(value.FromPublicKey));
            Assert.Contains(result.Data, value => !string.IsNullOrWhiteSpace(value.ToPublicKey));


        }
        [Fact]
        public async Task GetContractPackageNftActionsAsync_WithAscendingSortingParameters_ReturnsExpectedData()
        {
            var parameters = new NFTContractPackageTokenActionsRequestParameters
            {
                SortingParameters = new NFTContractPackageTokenActionsSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Ascending
                },
            };
            var result = await _restClient.Testnet.GetContractPackageNFTActionsForATokenAsync(_testContractPackageNFT, "200", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp <= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp <= result.Data[2].Timestamp);
            Assert.True(result.Data[2].Timestamp <= result.Data[3].Timestamp);
        }
        [Fact]
        public async Task GetContractPackageNftActionsAsync_WithDescendingSortingParameters_ReturnsExpectedData()
        {
            var parameters = new NFTContractPackageTokenActionsRequestParameters
            {
                SortingParameters = new NFTContractPackageTokenActionsSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Descending
                },
            };
            var result = await _restClient.Testnet.GetContractPackageNFTActionsForATokenAsync(_testContractPackageNFT, "200", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp >= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp >= result.Data[2].Timestamp);
            Assert.True(result.Data[2].Timestamp >= result.Data[3].Timestamp);
        }
        // Get account NFT actions by its identifier (public key or account hash) Tests
        [Fact]
        public async Task GetAccountNftActionsAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetAccountNFTActionsAsync(_testAccountHashWithNft);
            Assert.True(result.ItemCount > 0);
        }
        [Fact]
        public async Task GetAccountNftActionsAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new NFTAccountActionsRequestParameters
            {
                OptionalParameters = new NFTAccountActionsOptionalParameters
                {
                    ContractPackage = true,
                    Deploy = true,
                    FromPublicKey = true,
                    ToPublicKey = true
                },
                PageSize = 200
            };
            var result = await _restClient.Testnet.GetAccountNFTActionsAsync(_testAccountPublicKeyWithNFT, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.Deploy != null);
            Assert.Contains(result.Data, value => value.ContractPackage != null);
            Assert.Contains(result.Data, value => !string.IsNullOrWhiteSpace(value.FromPublicKey));
            Assert.Contains(result.Data, value => !string.IsNullOrWhiteSpace(value.ToPublicKey));

        }
        [Fact]
        public async Task GetAccountNftActionsAsync_WithAscendingSortingParameters_ReturnsExpectedData()
        {
            var parameters = new NFTAccountActionsRequestParameters
            {
                SortingParameters = new NFTAccountActionsSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Ascending
                },
            };
            var result = await _restClient.Testnet.GetAccountNFTActionsAsync(_testAccountPublicKeyWithNFT, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp <= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp <= result.Data[2].Timestamp);
            Assert.True(result.Data[2].Timestamp <= result.Data[3].Timestamp);
        }
        [Fact]
        public async Task GetAccountNftActionsAsync_WithDescendingSortingParameters_ReturnsExpectedData()
        {
            var parameters = new NFTAccountActionsRequestParameters
            {
                SortingParameters = new NFTAccountActionsSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Descending
                },
            };
            var result = await _restClient.Testnet.GetAccountNFTActionsAsync(_testAccountPublicKeyWithNFT, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp >= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp >= result.Data[2].Timestamp);
            Assert.True(result.Data[2].Timestamp >= result.Data[3].Timestamp);
        }
        [Fact]
        public async Task GetAccountNftActionsAsync_WithFromBlockHeightFilterParameters_ReturnsExpectedData()
        {
            var parameters = new NFTAccountActionsRequestParameters
            {
                FilterParameters = new NFTAccountActionsFilterParameters
                {
                    FromBlockHeight = "1349344",
                    ToBlockHeight = "1349500"
                }
            };
            var result = await _restClient.Testnet.GetAccountNFTActionsAsync(_testAccountPublicKeyWithNFT, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value =>
                value.BlockHeight >= ulong.Parse(parameters.FilterParameters.FromBlockHeight) &&
                value.BlockHeight <= ulong.Parse(parameters.FilterParameters.ToBlockHeight));

        }
        // Get contract package NFT actions by its hash Tests
        [Fact]
        public async Task GetContractPackageNftActionsByHashAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetContractPackageNFTActionsAsync(_testContractPackageHash);
            Assert.True(result.ItemCount > 0);
        }
        [Fact]
        public async Task GetContractPackageNftActionsByHashAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new NFTContractPackageActionsRequestParameters
            {
                OptionalParameters = new NFTContractPackageActionsOptionalParameters
                {
                    ContractPackage = true,
                    Deploy = true,
                    FromPublicKey = true,
                    ToPublicKey = true
                },
                PageSize = 200
            };
            var result = await _restClient.Testnet.GetContractPackageNFTActionsAsync(_testContractPackageHash, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.Deploy != null);
            Assert.Contains(result.Data, value => value.ContractPackage != null);
            Assert.Contains(result.Data, value => !string.IsNullOrWhiteSpace(value.FromPublicKey));
            Assert.Contains(result.Data, value => !string.IsNullOrWhiteSpace(value.ToPublicKey));

        }
        [Fact]
        public async Task GetContractPackageNftActionsByHashAsync_WithAscendingSortingParameters_ReturnsExpectedData()
        {
            var parameters = new NFTContractPackageActionsRequestParameters
            {
                SortingParameters = new NFTContractPackageActionsSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Ascending
                },
            };
            var result = await _restClient.Testnet.GetContractPackageNFTActionsAsync(_testContractPackageHash, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp <= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp <= result.Data[2].Timestamp);
            Assert.True(result.Data[2].Timestamp <= result.Data[3].Timestamp);
            Assert.True(result.Data[3].Timestamp <= result.Data[4].Timestamp);
            Assert.True(result.Data[4].Timestamp <= result.Data[5].Timestamp);
            Assert.True(result.Data[5].Timestamp <= result.Data[6].Timestamp);
            Assert.True(result.Data[6].Timestamp <= result.Data[7].Timestamp);
            Assert.True(result.Data[7].Timestamp <= result.Data[8].Timestamp);
            Assert.True(result.Data[8].Timestamp <= result.Data[9].Timestamp);
        }
        [Fact]
        public async Task GetContractPackageNftActionsByHashAsync_WithDescendingSortingParameters_ReturnsExpectedData()
        {
            var parameters = new NFTContractPackageActionsRequestParameters
            {
                SortingParameters = new NFTContractPackageActionsSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Descending
                },
            };
            var result = await _restClient.Testnet.GetContractPackageNFTActionsAsync(_testContractPackageHash, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp >= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp >= result.Data[2].Timestamp);
            Assert.True(result.Data[2].Timestamp >= result.Data[3].Timestamp);
            Assert.True(result.Data[3].Timestamp >= result.Data[4].Timestamp);
            Assert.True(result.Data[4].Timestamp >= result.Data[5].Timestamp);
            Assert.True(result.Data[5].Timestamp >= result.Data[6].Timestamp);
            Assert.True(result.Data[6].Timestamp >= result.Data[7].Timestamp);
            Assert.True(result.Data[7].Timestamp >= result.Data[8].Timestamp);
            Assert.True(result.Data[8].Timestamp >= result.Data[9].Timestamp);
        }
        [Fact]
        public async Task GetContractPackageNftActionsByHashAsync_WithFromBlockHeightFilterParameters_ReturnsExpectedData()
        {
            var parameters = new NFTContractPackageActionsRequestParameters
            {
                FilterParameters = new NFTContractPackageActionsFilterParameters
                {
                    FromBlockHeight = "487957",
                    ToBlockHeight = "488669"
                }
            };
            var result = await _restClient.Testnet.GetContractPackageNFTActionsAsync(_testContractPackageHash, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value =>
                value.BlockHeight >= ulong.Parse(parameters.FilterParameters.FromBlockHeight) &&
                value.BlockHeight <= ulong.Parse(parameters.FilterParameters.ToBlockHeight));

        }
        // Get NFT action types Tests
        [Fact]
        public async Task GetNftActionTypesAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetNFTActionTypesAsync();
            Assert.True(result.Data.Count > 0);
        }
        // Get contract package NFT ownerships Tests
        [Fact]
        public async Task GetContractPackageNftOwnershipsAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetContractPackageNFTOwnershipAsync(_testContractPackageNFT);
            Assert.True(result.ItemCount > 0);
        }
        [Fact]
        public async Task GetContractPackageNftOwnershipsAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new NFTContractPackageOwnershipRequestParameters
            {
                OptionalParameters = new NFTContractPackageOwnershipOptionalParameters
                {
                    OwnerPublicKey = true,
                    ContractPackage = true
                },
                PageSize = 200
            };
            var result = await _restClient.Testnet.GetContractPackageNFTOwnershipAsync(_testContractPackageNFT, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => !string.IsNullOrWhiteSpace(value.OwnerPublicKey));
            Assert.Contains(result.Data, value => value.ContractPackage != null);

        }
        [Fact]
        public async Task GetContractPackageNftOwnershipsAsync_WithOwnerHashFilterParameters_ReturnsExpectedData()
        {
            var parameters = new NFTContractPackageOwnershipRequestParameters
            {
                FilterParameters = new NFTContractPackageOwnershipFilterParameters
                {
                    OwnerHash = _testNFTOwnerHash,
                }
            };
            var result = await _restClient.Testnet.GetContractPackageNFTOwnershipAsync(_testContractPackageNFT, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.OwnerHash == _testNFTOwnerHash);

        }
        [Fact]
        public async Task GetContractPackageNftOwnershipsAsync_WithDESCSortingParameters_ReturnsExpectedData()
        {
            var parameters = new NFTContractPackageOwnershipRequestParameters
            {
                SortingParameters = new NFTContractPackageOwnershipSortingParameters
                {
                    OrderByTokensNumber = true,
                    SortType = SortType.Descending
                }
            };
            var result = await _restClient.Testnet.GetContractPackageNFTOwnershipAsync(_testContractPackageHash, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].TokensNumber >= result.Data[1].TokensNumber);
            Assert.True(result.Data[1].TokensNumber >= result.Data[2].TokensNumber);
            Assert.True(result.Data[2].TokensNumber >= result.Data[3].TokensNumber);
            Assert.True(result.Data[3].TokensNumber >= result.Data[4].TokensNumber);
            Assert.True(result.Data[4].TokensNumber >= result.Data[5].TokensNumber);
            Assert.True(result.Data[5].TokensNumber >= result.Data[6].TokensNumber);
            Assert.True(result.Data[6].TokensNumber >= result.Data[7].TokensNumber);
            Assert.True(result.Data[7].TokensNumber >= result.Data[8].TokensNumber);
            Assert.True(result.Data[8].TokensNumber >= result.Data[9].TokensNumber);

        }
        [Fact]
        public async Task GetContractPackageNftOwnershipsAsync_WithASCSortingParameters_ReturnsExpectedData()
        {
            var parameters = new NFTContractPackageOwnershipRequestParameters
            {
                SortingParameters = new NFTContractPackageOwnershipSortingParameters
                {
                    OrderByTokensNumber = true,
                    SortType = SortType.Ascending
                }
            };
            var result = await _restClient.Testnet.GetContractPackageNFTOwnershipAsync(_testContractPackageHash, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].TokensNumber <= result.Data[1].TokensNumber);
            Assert.True(result.Data[1].TokensNumber <= result.Data[2].TokensNumber);
            Assert.True(result.Data[2].TokensNumber <= result.Data[3].TokensNumber);
            Assert.True(result.Data[3].TokensNumber <= result.Data[4].TokensNumber);
            Assert.True(result.Data[4].TokensNumber <= result.Data[5].TokensNumber);
            Assert.True(result.Data[5].TokensNumber <= result.Data[6].TokensNumber);
            Assert.True(result.Data[6].TokensNumber <= result.Data[7].TokensNumber);
            Assert.True(result.Data[7].TokensNumber <= result.Data[8].TokensNumber);
            Assert.True(result.Data[8].TokensNumber <= result.Data[9].TokensNumber);

        }
        // Get account NFT ownerships Tests
        [Fact]
        public async Task GetAccountNftOwnershipsAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetAccountNFTOwnershipAsync(_testAccountHashWithNft);
            Assert.True(result.ItemCount > 0);
        }
        [Fact]
        public async Task GetAccountNftOwnershipsAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new NFTAccountOwnershipRequestParameters
            {
                OptionalParameters = new NFTAccountOwnershipOptionalParameters
                {
                    ContractPackage = true,
                    OwnerPublicKey = true
                },
                PageSize = 200
            };
            var result = await _restClient.Testnet.GetAccountNFTOwnershipAsync(_testAccountHashWithNft, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.OwnerHash == _testAccountHashWithNft);
            Assert.Contains(result.Data, value => !String.IsNullOrWhiteSpace(value.OwnerPublicKey));
            Assert.Contains(result.Data, value => value.ContractPackage != null);

        }
        [Fact]
        public async Task GetAccountNftOwnershipsAsync_WithOwnerHashFilterParameters_ReturnsExpectedData()
        {
            var parameters = new NFTAccountOwnershipRequestParameters
            {
                FilterParameters = new NFTAccountOwnershipFilterParameters
                {
                    ContractPackageHash = _testAccountHashWithNftContractPackageHash,
                }
            };
            var result = await _restClient.Testnet.GetAccountNFTOwnershipAsync(_testAccountHashWithNft, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.ContractPackageHash == _testAccountHashWithNftContractPackageHash);

        }
        [Fact]
        public async Task GetAccountNftOwnershipsAsync_WithDESCSortingParameters_ReturnsExpectedData()
        {
            var parameters = new NFTAccountOwnershipRequestParameters
            {
                SortingParameters = new NFTAccountOwnershipSortingParameters
                {
                    OrderByTokensNumber = true,
                    SortType = SortType.Descending
                }
            };
            var result = await _restClient.Testnet.GetAccountNFTOwnershipAsync(_testAccountHashWithAlotOfNfts, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].TokensNumber >= result.Data[1].TokensNumber);
            Assert.True(result.Data[1].TokensNumber >= result.Data[2].TokensNumber);
            Assert.True(result.Data[2].TokensNumber >= result.Data[3].TokensNumber);
            Assert.True(result.Data[3].TokensNumber >= result.Data[4].TokensNumber);
            Assert.True(result.Data[4].TokensNumber >= result.Data[5].TokensNumber);
            Assert.True(result.Data[5].TokensNumber >= result.Data[6].TokensNumber);
            Assert.True(result.Data[6].TokensNumber >= result.Data[7].TokensNumber);
            Assert.True(result.Data[7].TokensNumber >= result.Data[8].TokensNumber);

        }
        [Fact]
        public async Task GetAccountNftOwnershipsAsync_WithASCSortingParameters_ReturnsExpectedData()
        {
            var parameters = new NFTAccountOwnershipRequestParameters
            {
                SortingParameters = new NFTAccountOwnershipSortingParameters
                {
                    OrderByTokensNumber = true,
                    SortType = SortType.Ascending
                }
            };
            var result = await _restClient.Testnet.GetAccountNFTOwnershipAsync(_testAccountHashWithAlotOfNfts, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].TokensNumber <= result.Data[1].TokensNumber);
            Assert.True(result.Data[1].TokensNumber <= result.Data[2].TokensNumber);
            Assert.True(result.Data[2].TokensNumber <= result.Data[3].TokensNumber);
            Assert.True(result.Data[3].TokensNumber <= result.Data[4].TokensNumber);
            Assert.True(result.Data[4].TokensNumber <= result.Data[5].TokensNumber);
            Assert.True(result.Data[5].TokensNumber <= result.Data[6].TokensNumber);
            Assert.True(result.Data[6].TokensNumber <= result.Data[7].TokensNumber);
            Assert.True(result.Data[7].TokensNumber <= result.Data[8].TokensNumber);
        }
        // Get the current CSPR rate by currency identifier Tests
        [Fact]
        public async Task GetCsprRateByCurrencyIdentifierAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetCurrentCurrencyRateAsync("1");
            Assert.True(result.Data != null);
            Assert.True(result.Data.CurrencyId == 1);
            Assert.True(result.Data.Amount > 0); // hopefully xD
            Assert.True(result.Data.Created > DateTime.UtcNow.AddDays(-1));
        }
        // Get a paginated list of historical currency rates for the given time range Tests
        [Fact]
        public async Task GetHistoricalCurrencyRatesAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetHistoricalCurrencyRatesAsync("1");
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].CurrencyId == 1);
            Assert.True(result.Data[0].Amount > 0); // hopefully xD
            Assert.True(result.Data[0].Created > DateTime.UtcNow.AddDays(-1));
        }
        [Fact]
        public async Task GetHistoricalCurrencyRatesAsync_WithQueryParameters_ReturnsExpectedData()
        {
            var parameters = new RateHistoricalRequestParameters
            {
                PageSize = 200,
                FilterParameters = new RateHistoricalFilterParameters
                {
                    From = DateTime.UtcNow.AddMinutes(-10),
                    To = DateTime.UtcNow
                }
            };
            var result = await _restClient.Testnet.GetHistoricalCurrencyRatesAsync("1", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => value.CurrencyId == 1);
            Assert.Contains(result.Data, value => value.Created >= parameters.FilterParameters.To && value.Created <= parameters.FilterParameters.From);

        }
        [Fact]
        public async Task GetHistoricalCurrencyRatesAsync_WithASCOrdering_ReturnsExpectedData()
        {
            var parameters = new RateHistoricalRequestParameters
            {
                SortingParameters = new RateHistoricalSortingParameters
                {
                    OrderByCreated = true,
                    SortType = SortType.Ascending
                }
            };
            var result = await _restClient.Testnet.GetHistoricalCurrencyRatesAsync("1", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Created <= result.Data[1].Created);
            Assert.True(result.Data[1].Created <= result.Data[2].Created);
            Assert.True(result.Data[2].Created <= result.Data[3].Created);
            Assert.True(result.Data[3].Created <= result.Data[4].Created);
            Assert.True(result.Data[4].Created <= result.Data[5].Created);
            Assert.True(result.Data[5].Created <= result.Data[6].Created);
            Assert.True(result.Data[6].Created <= result.Data[7].Created);
            Assert.True(result.Data[7].Created <= result.Data[8].Created);
            Assert.True(result.Data[8].Created <= result.Data[9].Created);
        }
        [Fact]
        public async Task GetHistoricalCurrencyRatesAsync_WithDESCOrdering_ReturnsExpectedData()
        {
            var parameters = new RateHistoricalRequestParameters
            {
                SortingParameters = new RateHistoricalSortingParameters
                {
                    OrderByCreated = true,
                    SortType = SortType.Descending
                }
            };
            var result = await _restClient.Testnet.GetHistoricalCurrencyRatesAsync("1", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Created >= result.Data[1].Created);
            Assert.True(result.Data[1].Created >= result.Data[2].Created);
            Assert.True(result.Data[2].Created >= result.Data[3].Created);
            Assert.True(result.Data[3].Created >= result.Data[4].Created);
            Assert.True(result.Data[4].Created >= result.Data[5].Created);
            Assert.True(result.Data[5].Created >= result.Data[6].Created);
            Assert.True(result.Data[6].Created >= result.Data[7].Created);
            Assert.True(result.Data[7].Created >= result.Data[8].Created);
            Assert.True(result.Data[8].Created >= result.Data[9].Created);
        }
        // Get a paginated list of currencies Tests
        [Fact]
        public async Task GetCurrenciesAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetCurrenciesAsync();
            Assert.True(result.ItemCount > 0);
        }
        [Fact]
        public async Task GetCurrenciesAsync_WithDESCSortingParameters_ReturnsExpectedData()
        {
            var parameters = new RateCurrenciesRequestParameters
            {
                PageSize = 200,
                SortingParameters = new RateCurrenciesSortingParameters
                {
                    OrderById = true,
                    SortType = SortType.Descending
                }
            };
            var result = await _restClient.Testnet.GetCurrenciesAsync(parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Id >= result.Data[1].Id);
            Assert.True(result.Data[1].Id >= result.Data[2].Id);
            Assert.True(result.Data[2].Id >= result.Data[3].Id);
            Assert.True(result.Data[3].Id >= result.Data[4].Id);
            Assert.True(result.Data[4].Id >= result.Data[5].Id);
            Assert.True(result.Data[5].Id >= result.Data[6].Id);
            Assert.True(result.Data[6].Id >= result.Data[7].Id);
            Assert.True(result.Data[7].Id >= result.Data[8].Id);
            Assert.True(result.Data[8].Id >= result.Data[9].Id);
        }
        [Fact]
        public async Task GetCurrenciesAsync_WithASCOrdering_ReturnsExpectedData()
        {
            var parameters = new RateCurrenciesRequestParameters
            {
                PageSize = 200,
                SortingParameters = new RateCurrenciesSortingParameters
                {
                    OrderById = true,
                    SortType = SortType.Ascending
                }
            };
            var result = await _restClient.Testnet.GetCurrenciesAsync(parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Id <= result.Data[1].Id);
            Assert.True(result.Data[1].Id <= result.Data[2].Id);
            Assert.True(result.Data[2].Id <= result.Data[3].Id);
            Assert.True(result.Data[3].Id <= result.Data[4].Id);
            Assert.True(result.Data[4].Id <= result.Data[5].Id);
            Assert.True(result.Data[5].Id <= result.Data[6].Id);
            Assert.True(result.Data[6].Id <= result.Data[7].Id);
            Assert.True(result.Data[7].Id <= result.Data[8].Id);
            Assert.True(result.Data[8].Id <= result.Data[9].Id);

        }
        // Get the latest CSPR supply Tests
        [Fact]
        public async Task GetCsprSupplyAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.GetSupplyAsync();
            Assert.True(result.Data != null);
            Assert.True(result.Data.Circulating > 0); // hopefully
            Assert.True(result.Data.Total > 0); // hopefully
            Assert.True(result.Data.Timestamp > DateTime.UtcNow.AddDays(-1));
        }

        // Get all account sent and received transfers by the account identifier (public key or account hash) Tests
        [Fact]
        public async Task GetAccountTransfersAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.Transfer.GetAccountTransfersAsync(_testAccountHash);
            Assert.True(result.ItemCount > 0);
        }
        [Fact]
        public async Task GetAccountTransfersAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new TransferAccountRequestParameters
            {
                OptionalParameters = new TransferAccountOptionalParameters
                {
                    ToPublicKey = true,
                    FromPurseAccountInfo = true,
                    FromPurseCentralizedAccountInfo = true,
                    FromPursePublicKey = true,
                    InitiatorPublicKey = true,
                    Rate = 1,
                    ToAccountInfo = true,
                    ToCentralizedAccountInfo = true,
                    ToPurseAccountInfo = true,
                    ToPurseCentralizedAccountInfo = true,
                    ToPursePublicKey = true
                },
                PageSize = 10
            };
            var result = await _restClient.Testnet.Transfer.GetAccountTransfersAsync(_testAccountHash, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => !string.IsNullOrWhiteSpace(value.ToPublicKey));
            Assert.Contains(result.Data, value => !string.IsNullOrWhiteSpace(value.FromPursePublicKey));
            Assert.Contains(result.Data, value => !string.IsNullOrWhiteSpace(value.InitiatorPublicKey));
            Assert.Contains(result.Data, value => value.Rate != null);
            Assert.Contains(result.Data, value => value.ToAccountInfo != null);
            Assert.Contains(result.Data, value => value.ToPurseAccountInfo != null);
            Assert.Contains(result.Data, value => !string.IsNullOrWhiteSpace(value.ToPursePublicKey));


        }
        [Fact]
        public async Task GetAccountTransfersAsync_WithASCOrdering_ReturnsExpectedData()
        {
            var parameters = new TransferAccountRequestParameters
            {
                SortingParameters = new TransferAccountSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Ascending
                }
            };
            var result = await _restClient.Testnet.Transfer.GetAccountTransfersAsync(_testAccountHash, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp <= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp <= result.Data[2].Timestamp);
            Assert.True(result.Data[2].Timestamp <= result.Data[3].Timestamp);
            Assert.True(result.Data[3].Timestamp <= result.Data[4].Timestamp);
            Assert.True(result.Data[4].Timestamp <= result.Data[5].Timestamp);
            Assert.True(result.Data[5].Timestamp <= result.Data[6].Timestamp);
            Assert.True(result.Data[6].Timestamp <= result.Data[7].Timestamp);
            Assert.True(result.Data[7].Timestamp <= result.Data[8].Timestamp);
            Assert.True(result.Data[8].Timestamp <= result.Data[9].Timestamp);
        }
        [Fact]
        public async Task GetAccountTransfersAsync_WithDESCOrdering_ReturnsExpectedData()
        {
            var parameters = new TransferAccountRequestParameters
            {
                SortingParameters = new TransferAccountSortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Descending
                }
            };
            var result = await _restClient.Testnet.Transfer.GetAccountTransfersAsync(_testAccountHash, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp >= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp >= result.Data[2].Timestamp);
            Assert.True(result.Data[2].Timestamp >= result.Data[3].Timestamp);
            Assert.True(result.Data[3].Timestamp >= result.Data[4].Timestamp);
            Assert.True(result.Data[4].Timestamp >= result.Data[5].Timestamp);
            Assert.True(result.Data[5].Timestamp >= result.Data[6].Timestamp);
            Assert.True(result.Data[6].Timestamp >= result.Data[7].Timestamp);
            Assert.True(result.Data[7].Timestamp >= result.Data[8].Timestamp);
            Assert.True(result.Data[8].Timestamp >= result.Data[9].Timestamp);
        }
        [Fact]
        public async Task GetAccountTransfersAsync_WithFromBlockHeightFilterParameters_ReturnsExpectedData()
        {
            var parameters = new TransferAccountRequestParameters
            {
                FilterParameters = new TransferAccountFilterParameters
                {
                    FromBlockHeight = "2349344",
                    ToBlockHeight = "3349500"
                }
            };
            var result = await _restClient.Testnet.Transfer.GetAccountTransfersAsync(_testAccountHash, parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value =>
                value.BlockHeight >= ulong.Parse(parameters.FilterParameters.FromBlockHeight) &&
                value.BlockHeight <= ulong.Parse(parameters.FilterParameters.ToBlockHeight));

        }
        [Fact]
        public async Task GetAccountTransfersAsync_WithFromBlockHeightFilterParameters_ShouldntReturnData()
        {
            var parameters = new TransferAccountRequestParameters
            {
                FilterParameters = new TransferAccountFilterParameters
                {
                    FromBlockHeight = "1349344",
                    ToBlockHeight = "1349500"
                }
            };
            var result = await _restClient.Testnet.Transfer.GetAccountTransfersAsync(_testAccountHash, parameters);
            Assert.True(result.ItemCount == 0);

        }
        // Get transfers by deploy hash Tests
        [Fact]
        public async Task GetTransfersByDeployHashAsync_ReturnsExpectedData()
        {
            var result = await _restClient.Testnet.Transfer.GetDeployTransfersAsync(_testDeployHash);
            Assert.True(result.ItemCount > 0);
        }
        [Fact]
        public async Task GetTransfersByDeployHashAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new TransferDeployRequestParameters
            {
                OptionalParameters = new TransferDeployOptionalParameters
                {
                    ToPublicKey = true,
                    FromPurseAccountInfo = true,
                    FromPurseCentralizedAccountInfo = true,
                    FromPursePublicKey = true,
                    InitiatorPublicKey = true,
                    Rate = 1,
                    ToAccountInfo = true,
                    ToCentralizedAccountInfo = true,
                    ToPurseAccountInfo = true,
                    ToPurseCentralizedAccountInfo = true,
                    ToPursePublicKey = true
                },
                PageSize = 10
            };
            var result = await _restClient.Testnet.Transfer.GetDeployTransfersAsync("da31ebd5faeb5d0e4c33e27e7b258209dac78368dcdcda56bc75026d53a7131b", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.Contains(result.Data, value => !string.IsNullOrWhiteSpace(value.ToPublicKey));
            Assert.Contains(result.Data, value => !string.IsNullOrWhiteSpace(value.FromPursePublicKey));
            Assert.Contains(result.Data, value => !string.IsNullOrWhiteSpace(value.InitiatorPublicKey));
            Assert.Contains(result.Data, value => value.Rate != null);
            Assert.Contains(result.Data, value => !string.IsNullOrWhiteSpace(value.ToPursePublicKey));

        }
        // Couldn't find a deploy with a lot of transfers to test the sorting
        [Fact]
        public async Task GetTransfersByDeployHashAsync_WithASCOrdering_ReturnsExpectedData()
        {
            var parameters = new TransferDeployRequestParameters
            {
                SortingParameters = new TransferDeploySortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Ascending
                }
            };
            var result = await _restClient.Testnet.Transfer.GetDeployTransfersAsync("e3fd35c7dfbdfcad9467111b0a24d1458bc3098c8971a1f4a07e7ac0526f0be5", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp <= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp <= result.Data[2].Timestamp);
        }
        // Couldn't find a deploy with a lot of transfers to test the sorting
        [Fact]
        public async Task GetTransfersByDeployHashAsync_WithDESCOrdering_ReturnsExpectedData()
        {
            var parameters = new TransferDeployRequestParameters
            {
                SortingParameters = new TransferDeploySortingParameters
                {
                    OrderByTimestamp = true,
                    SortType = SortType.Descending
                }
            };
            var result = await _restClient.Testnet.Transfer.GetDeployTransfersAsync("adeb1a8db852a68f733a68a91aa519e95b198642e443ab40348ab1adffe470d0", parameters);
            Assert.True(result.ItemCount > 0);
            Assert.True(result.Data[0].Timestamp >= result.Data[1].Timestamp);
            Assert.True(result.Data[1].Timestamp >= result.Data[2].Timestamp);

        }
        // Get validator by public key Tests
        [Fact]
        public async Task GetValidatorByPublicKeyAsync_ReturnsExpectedData()
        {
            var parameters = new ValidatorRequestParameters
            {
                FilterParameters = new ValidatorFilterParameters
                {
                    EraId = "14027"
                }
            };
            var result = await _restClient.Testnet.Validator.GetValidatorAsync(_test2PublicKey, parameters);
            Assert.True(result.Data != null);
            Assert.True(result.Data.PublicKey == _test2PublicKey);
            Assert.True(result.Data.EraId == uint.Parse(parameters.FilterParameters.EraId));
        }
        [Fact]
        public async Task GetValidatorByPublicKeyAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new ValidatorRequestParameters
            {
                OptionalParameters = new ValidatorOptionalParameters
                {
                    AccountInfo = true,
                    CentralizedAccountInfo = true,
                    AveragePerformance = true,
                },
                FilterParameters = new ValidatorFilterParameters
                {
                    EraId = "14027"
                }
            };
            var result = await _restClient.Testnet.Validator.GetValidatorAsync(_test2PublicKey, parameters);
            Assert.True(result.Data != null);
            Assert.True(result.Data.PublicKey == _test2PublicKey);
            Assert.True(result.Data.EraId == uint.Parse(parameters.FilterParameters.EraId));
            Assert.True(result.Data.TotalStake > 0);
            Assert.True(result.Data.AveragePerformance != null);
            Assert.True(result.Data.AccountInfo != null);
        }
        // Get a paginated list of validators Tests
        [Fact]
        public async Task GetValidatorsAsync_ShouldntThrowException()
        {
            var parameters = new ValidatorsRequestParameters
            {
                FilterParameters = new ValidatorsFilterParameters
                {
                    EraId = "14027",
                    IsActive = false
                }
            };
            var result = await _restClient.Testnet.Validator.GetValidatorsAsync(parameters);

            // Verify that the result is not null
            Assert.NotNull(result);

            // Verify that the result contains expected data
            Assert.NotEmpty(result.Data); // Assuming result.Data is a collection of validators
        }
        [Fact]
        public async Task GetValidatorsAsync_ReturnsExpectedData()
        {
            var parameters = new ValidatorsRequestParameters
            {
                FilterParameters = new ValidatorsFilterParameters
                {
                    EraId = "14027",
                    IsActive = true
                }
            };
            var result = await _restClient.Testnet.Validator.GetValidatorsAsync(parameters);

            // Verify that the result is not null
            Assert.NotNull(result);

            // Verify that the result contains expected data
            Assert.NotEmpty(result.Data); // Assuming result.Data is a collection of validators
        }
        [Fact]
        public async Task GetValidatorsAsync_WithOptionalParameters_ReturnsExpectedData()
        {
            var parameters = new ValidatorsRequestParameters
            {
                OptionalParameters = new ValidatorsOptionalParameters
                {
                    AccountInfo = true,
                    CentralizedAccountInfo = true,
                    AveragePerformance = true,
                },
                FilterParameters = new ValidatorsFilterParameters
                {
                    EraId = "14027",
                    IsActive = true
                }
            };
            var result = await _restClient.Testnet.Validator.GetValidatorsAsync(parameters);

            // Verify that the result is not null
            Assert.NotNull(result);

            // Verify that the result contains expected data
            Assert.NotEmpty(result.Data); // Assuming result.Data is a collection of validators

            // Verify that the result contains expected data
            Assert.Contains(result.Data, value => value.AveragePerformance != null);
            Assert.Contains(result.Data, value => value.AccountInfo != null);
        }
        [Fact]
        public async Task GetValidatorsAsync_WithASCOrdering_OrderByTotalStake_ReturnsExpectedData()
        {
            var parameters = new ValidatorsRequestParameters
            {
                SortingParameters = new ValidatorsSortingParameters
                {
                    OrderByTotalStake = true,
                    SortType = SortType.Ascending
                },
                FilterParameters = new ValidatorsFilterParameters
                {
                    EraId = "14027",
                    IsActive = true
                }
            };
            var result = await _restClient.Testnet.Validator.GetValidatorsAsync(parameters);

            // Verify that the result is not null
            Assert.NotNull(result);

            // Verify that the result contains expected data
            Assert.NotEmpty(result.Data); // Assuming result.Data is a collection of validators

            // Verify that the result is sorted in ascending order
            Assert.True(result.Data[0].TotalStake <= result.Data[1].TotalStake);
            Assert.True(result.Data[1].TotalStake <= result.Data[2].TotalStake);
            Assert.True(result.Data[2].TotalStake <= result.Data[3].TotalStake);
            Assert.True(result.Data[3].TotalStake <= result.Data[4].TotalStake);
            Assert.True(result.Data[4].TotalStake <= result.Data[5].TotalStake);
            Assert.True(result.Data[5].TotalStake <= result.Data[6].TotalStake);
            Assert.True(result.Data[6].TotalStake <= result.Data[7].TotalStake);
            Assert.True(result.Data[7].TotalStake <= result.Data[8].TotalStake);
            Assert.True(result.Data[8].TotalStake <= result.Data[9].TotalStake);
        }
        [Fact]
        public async Task GetValidatorsAsync_WithDESCOrdering_OrderByTotalStake_ReturnsExpectedData()
        {
            var parameters = new ValidatorsRequestParameters
            {
                SortingParameters = new ValidatorsSortingParameters
                {
                    OrderByTotalStake = true,
                    SortType = SortType.Descending
                },
                FilterParameters = new ValidatorsFilterParameters
                {
                    EraId = "14027",
                    IsActive = true
                }
            };
            var result = await _restClient.Testnet.Validator.GetValidatorsAsync(parameters);

            // Verify that the result is not null
            Assert.NotNull(result);

            // Verify that the result contains expected data
            Assert.NotEmpty(result.Data); // Assuming result.Data is a collection of validators

            // Verify that the result is sorted in descending order
            Assert.True(result.Data[0].TotalStake >= result.Data[1].TotalStake);
            Assert.True(result.Data[1].TotalStake >= result.Data[2].TotalStake);
            Assert.True(result.Data[2].TotalStake >= result.Data[3].TotalStake);
            Assert.True(result.Data[3].TotalStake >= result.Data[4].TotalStake);
            Assert.True(result.Data[4].TotalStake >= result.Data[5].TotalStake);
            Assert.True(result.Data[5].TotalStake >= result.Data[6].TotalStake);
            Assert.True(result.Data[6].TotalStake >= result.Data[7].TotalStake);
            Assert.True(result.Data[7].TotalStake >= result.Data[8].TotalStake);
            Assert.True(result.Data[8].TotalStake >= result.Data[9].TotalStake);
        }
        [Fact]
        public async Task GetValidatorsAsync_WithASCOrdering_OrderByRank_ReturnsExpectedData()
        {
            var parameters = new ValidatorsRequestParameters
            {
                SortingParameters = new ValidatorsSortingParameters
                {
                    OrderByRank = true,
                    SortType = SortType.Ascending
                },
                FilterParameters = new ValidatorsFilterParameters
                {
                    EraId = "14027",
                    IsActive = true
                }
            };
            var result = await _restClient.Testnet.Validator.GetValidatorsAsync(parameters);

            // Verify that the result is not null
            Assert.NotNull(result);

            // Verify that the result contains expected data
            Assert.NotEmpty(result.Data); // Assuming result.Data is a collection of validators

            // Verify that the result is sorted in ascending order
            Assert.True(result.Data[0].Rank <= result.Data[1].Rank);
            Assert.True(result.Data[1].Rank <= result.Data[2].Rank);
            Assert.True(result.Data[2].Rank <= result.Data[3].Rank);
            Assert.True(result.Data[3].Rank <= result.Data[4].Rank);
            Assert.True(result.Data[4].Rank <= result.Data[5].Rank);
            Assert.True(result.Data[5].Rank <= result.Data[6].Rank);
            Assert.True(result.Data[6].Rank <= result.Data[7].Rank);
            Assert.True(result.Data[7].Rank <= result.Data[8].Rank);
            Assert.True(result.Data[8].Rank <= result.Data[9].Rank);
        }
        [Fact]
        public async Task GetValidatorsAsync_WithDESCOrdering_OrderByRank_ReturnsExpectedData()
        {
            var parameters = new ValidatorsRequestParameters
            {
                SortingParameters = new ValidatorsSortingParameters
                {
                    OrderByRank = true,
                    SortType = SortType.Descending
                },
                FilterParameters = new ValidatorsFilterParameters
                {
                    EraId = "14027",
                    IsActive = true
                }
            };
            var result = await _restClient.Testnet.Validator.GetValidatorsAsync(parameters);

            // Verify that the result is not null
            Assert.NotNull(result);

            // Verify that the result contains expected data
            Assert.NotEmpty(result.Data); // Assuming result.Data is a collection of validators

            // Verify that the result is sorted in ascending order
            Assert.True(result.Data[0].Rank >= result.Data[1].Rank);
            Assert.True(result.Data[1].Rank >= result.Data[2].Rank);
            Assert.True(result.Data[2].Rank >= result.Data[3].Rank);
            Assert.True(result.Data[3].Rank >= result.Data[4].Rank);
            Assert.True(result.Data[4].Rank >= result.Data[5].Rank);
            Assert.True(result.Data[5].Rank >= result.Data[6].Rank);
            Assert.True(result.Data[6].Rank >= result.Data[7].Rank);
            Assert.True(result.Data[7].Rank >= result.Data[8].Rank);
            Assert.True(result.Data[8].Rank >= result.Data[9].Rank);
        }
        [Fact]
        public async Task GetValidatorsAsync_WithASCOrdering_OrderByFee_ReturnsExpectedData()
        {
            var parameters = new ValidatorsRequestParameters
            {
                SortingParameters = new ValidatorsSortingParameters
                {
                    OrderByFee = true,
                    SortType = SortType.Ascending
                },
                FilterParameters = new ValidatorsFilterParameters
                {
                    EraId = "14027",
                    IsActive = true
                }
            };
            var result = await _restClient.Testnet.Validator.GetValidatorsAsync(parameters);

            // Verify that the result is not null
            Assert.NotNull(result);

            // Verify that the result contains expected data
            Assert.NotEmpty(result.Data); // Assuming result.Data is a collection of validators

            // Verify that the result is sorted in ascending order
            Assert.True(result.Data[0].Fee <= result.Data[1].Fee);
            Assert.True(result.Data[1].Fee <= result.Data[2].Fee);
            Assert.True(result.Data[2].Fee <= result.Data[3].Fee);
            Assert.True(result.Data[3].Fee <= result.Data[4].Fee);
            Assert.True(result.Data[4].Fee <= result.Data[5].Fee);
            Assert.True(result.Data[5].Fee <= result.Data[6].Fee);
            Assert.True(result.Data[6].Fee <= result.Data[7].Fee);
            Assert.True(result.Data[7].Fee <= result.Data[8].Fee);
            Assert.True(result.Data[8].Fee <= result.Data[9].Fee);
        }
        [Fact]
        public async Task GetValidatorsAsync_WithDESCOrdering_OrderByFee_ReturnsExpectedData()
        {
            var parameters = new ValidatorsRequestParameters
            {
                SortingParameters = new ValidatorsSortingParameters
                {
                    OrderByFee = true,
                    SortType = SortType.Descending
                },
                FilterParameters = new ValidatorsFilterParameters
                {
                    EraId = "14027",
                    IsActive = true
                }
            };
            var result = await _restClient.Testnet.Validator.GetValidatorsAsync(parameters);

            // Verify that the result is not null
            Assert.NotNull(result);

            // Verify that the result contains expected data
            Assert.NotEmpty(result.Data); // Assuming result.Data is a collection of validators

            // Verify that the result is sorted in ascending order
            Assert.True(result.Data[0].Fee >= result.Data[1].Fee);
            Assert.True(result.Data[1].Fee >= result.Data[2].Fee);
            Assert.True(result.Data[2].Fee >= result.Data[3].Fee);
            Assert.True(result.Data[3].Fee >= result.Data[4].Fee);
            Assert.True(result.Data[4].Fee >= result.Data[5].Fee);
            Assert.True(result.Data[5].Fee >= result.Data[6].Fee);
            Assert.True(result.Data[6].Fee >= result.Data[7].Fee);
            Assert.True(result.Data[7].Fee >= result.Data[8].Fee);
            Assert.True(result.Data[8].Fee >= result.Data[9].Fee);
        }
        [Fact]
        public async Task GetValidatorsAsync_WithASCOrdering_OrderByDelegatorsNumber_ReturnsExpectedData()
        {
            var parameters = new ValidatorsRequestParameters
            {
                SortingParameters = new ValidatorsSortingParameters
                {
                    OrderByDelegatorsNumber = true,
                    SortType = SortType.Ascending
                },
                FilterParameters = new ValidatorsFilterParameters
                {
                    EraId = "14027",
                    IsActive = true
                }
            };
            var result = await _restClient.Testnet.Validator.GetValidatorsAsync(parameters);

            // Verify that the result is not null
            Assert.NotNull(result);

            // Verify that the result contains expected data
            Assert.NotEmpty(result.Data); // Assuming result.Data is a collection of validators

            // Verify that the result is sorted in ascending order
            Assert.True(result.Data[0].DelegatorsNumber <= result.Data[1].DelegatorsNumber);
            Assert.True(result.Data[1].DelegatorsNumber <= result.Data[2].DelegatorsNumber);
            Assert.True(result.Data[2].DelegatorsNumber <= result.Data[3].DelegatorsNumber);
            Assert.True(result.Data[3].DelegatorsNumber <= result.Data[4].DelegatorsNumber);
            Assert.True(result.Data[4].DelegatorsNumber <= result.Data[5].DelegatorsNumber);
            Assert.True(result.Data[5].DelegatorsNumber <= result.Data[6].DelegatorsNumber);
            Assert.True(result.Data[6].DelegatorsNumber <= result.Data[7].DelegatorsNumber);
            Assert.True(result.Data[7].DelegatorsNumber <= result.Data[8].DelegatorsNumber);
            Assert.True(result.Data[8].DelegatorsNumber <= result.Data[9].DelegatorsNumber);
        }
        [Fact]
        public async Task GetValidatorsAsync_WithDESCOrdering_OrderByDelegatorsNumber_ReturnsExpectedData()
        {
            var parameters = new ValidatorsRequestParameters
            {
                SortingParameters = new ValidatorsSortingParameters
                {
                    OrderByDelegatorsNumber = true,
                    SortType = SortType.Descending
                },
                FilterParameters = new ValidatorsFilterParameters
                {
                    EraId = "14027",
                    IsActive = true
                }
            };
            var result = await _restClient.Testnet.Validator.GetValidatorsAsync(parameters);

            // Verify that the result is not null
            Assert.NotNull(result);

            // Verify that the result contains expected data
            Assert.NotEmpty(result.Data); // Assuming result.Data is a collection of validators

            // Verify that the result is sorted in ascending order
            Assert.True(result.Data[0].DelegatorsNumber >= result.Data[1].DelegatorsNumber);
            Assert.True(result.Data[1].DelegatorsNumber >= result.Data[2].DelegatorsNumber);
            Assert.True(result.Data[2].DelegatorsNumber >= result.Data[3].DelegatorsNumber);
            Assert.True(result.Data[3].DelegatorsNumber >= result.Data[4].DelegatorsNumber);
            Assert.True(result.Data[4].DelegatorsNumber >= result.Data[5].DelegatorsNumber);
            Assert.True(result.Data[5].DelegatorsNumber >= result.Data[6].DelegatorsNumber);
            Assert.True(result.Data[6].DelegatorsNumber >= result.Data[7].DelegatorsNumber);
            Assert.True(result.Data[7].DelegatorsNumber >= result.Data[8].DelegatorsNumber);
            Assert.True(result.Data[8].DelegatorsNumber >= result.Data[9].DelegatorsNumber);
        }
        [Fact]
        public async Task GetValidatorsAsync_WithASCOrdering_OrderBySelfStake_ReturnsExpectedData()
        {
            var parameters = new ValidatorsRequestParameters
            {
                SortingParameters = new ValidatorsSortingParameters
                {
                    OrderBySelfStake = true,
                    SortType = SortType.Ascending
                },
                FilterParameters = new ValidatorsFilterParameters
                {
                    EraId = "14027",
                    IsActive = true
                }
            };
            var result = await _restClient.Testnet.Validator.GetValidatorsAsync(parameters);

            // Verify that the result is not null
            Assert.NotNull(result);

            // Verify that the result contains expected data
            Assert.NotEmpty(result.Data); // Assuming result.Data is a collection of validators

            // Verify that the result is sorted in ascending order
            Assert.True(result.Data[0].SelfStake <= result.Data[1].SelfStake);
            Assert.True(result.Data[1].SelfStake <= result.Data[2].SelfStake);
            Assert.True(result.Data[2].SelfStake <= result.Data[3].SelfStake);
            Assert.True(result.Data[3].SelfStake <= result.Data[4].SelfStake);
            Assert.True(result.Data[4].SelfStake <= result.Data[5].SelfStake);
            Assert.True(result.Data[5].SelfStake <= result.Data[6].SelfStake);
            Assert.True(result.Data[6].SelfStake <= result.Data[7].SelfStake);
            Assert.True(result.Data[7].SelfStake <= result.Data[8].SelfStake);
            Assert.True(result.Data[8].SelfStake <= result.Data[9].SelfStake);
        }
        [Fact]
        public async Task GetValidatorsAsync_WithDESCOrdering_OrderBySelfStake_ReturnsExpectedData()
        {
            var parameters = new ValidatorsRequestParameters
            {
                SortingParameters = new ValidatorsSortingParameters
                {
                    OrderBySelfStake = true,
                    SortType = SortType.Descending
                },
                FilterParameters = new ValidatorsFilterParameters
                {
                    EraId = "14027",
                    IsActive = true
                }
            };
            var result = await _restClient.Testnet.Validator.GetValidatorsAsync(parameters);

            // Verify that the result is not null
            Assert.NotNull(result);

            // Verify that the result contains expected data
            Assert.NotEmpty(result.Data); // Assuming result.Data is a collection of validators

            // Verify that the result is sorted in ascending order
            Assert.True(result.Data[0].SelfStake >= result.Data[1].SelfStake);
            Assert.True(result.Data[1].SelfStake >= result.Data[2].SelfStake);
            Assert.True(result.Data[2].SelfStake >= result.Data[3].SelfStake);
            Assert.True(result.Data[3].SelfStake >= result.Data[4].SelfStake);
            Assert.True(result.Data[4].SelfStake >= result.Data[5].SelfStake);
            Assert.True(result.Data[5].SelfStake >= result.Data[6].SelfStake);
            Assert.True(result.Data[6].SelfStake >= result.Data[7].SelfStake);
            Assert.True(result.Data[7].SelfStake >= result.Data[8].SelfStake);
            Assert.True(result.Data[8].SelfStake >= result.Data[9].SelfStake);
        }
        [Fact]
        public async Task GetValidatorsAsync_WithASCOrdering_OrderByNetworkShare_ReturnsExpectedData()
        {
            var parameters = new ValidatorsRequestParameters
            {
                SortingParameters = new ValidatorsSortingParameters
                {
                    OrderByNetworkShare = true,
                    SortType = SortType.Ascending
                },
                FilterParameters = new ValidatorsFilterParameters
                {
                    EraId = "14027",
                    IsActive = true
                }
            };
            var result = await _restClient.Testnet.Validator.GetValidatorsAsync(parameters);

            // Verify that the result is not null
            Assert.NotNull(result);

            // Verify that the result contains expected data
            Assert.NotEmpty(result.Data); // Assuming result.Data is a collection of validators

            // Verify that the result is sorted in ascending order
            Assert.True(result.Data[0].NetworkShare <= result.Data[1].NetworkShare);
            Assert.True(result.Data[1].NetworkShare <= result.Data[2].NetworkShare);
            Assert.True(result.Data[2].NetworkShare <= result.Data[3].NetworkShare);
            Assert.True(result.Data[3].NetworkShare <= result.Data[4].NetworkShare);
            Assert.True(result.Data[4].NetworkShare <= result.Data[5].NetworkShare);
            Assert.True(result.Data[5].NetworkShare <= result.Data[6].NetworkShare);
            Assert.True(result.Data[6].NetworkShare <= result.Data[7].NetworkShare);
            Assert.True(result.Data[7].NetworkShare <= result.Data[8].NetworkShare);
            Assert.True(result.Data[8].NetworkShare <= result.Data[9].NetworkShare);
        }
        [Fact]
        public async Task GetValidatorsAsync_WithDESCOrdering_OrderByNetworkShare_ReturnsExpectedData()
        {
            var parameters = new ValidatorsRequestParameters
            {
                SortingParameters = new ValidatorsSortingParameters
                {
                    OrderByNetworkShare = true,
                    SortType = SortType.Descending
                },
                FilterParameters = new ValidatorsFilterParameters
                {
                    EraId = "14027",
                    IsActive = true
                }
            };
            var result = await _restClient.Testnet.Validator.GetValidatorsAsync(parameters);

            // Verify that the result is not null
            Assert.NotNull(result);

            // Verify that the result contains expected data
            Assert.NotEmpty(result.Data); // Assuming result.Data is a collection of validators

            // Verify that the result is sorted in ascending order
            Assert.True(result.Data[0].NetworkShare >= result.Data[1].NetworkShare);
            Assert.True(result.Data[1].NetworkShare >= result.Data[2].NetworkShare);
            Assert.True(result.Data[2].NetworkShare >= result.Data[3].NetworkShare);
            Assert.True(result.Data[3].NetworkShare >= result.Data[4].NetworkShare);
            Assert.True(result.Data[4].NetworkShare >= result.Data[5].NetworkShare);
            Assert.True(result.Data[5].NetworkShare >= result.Data[6].NetworkShare);
            Assert.True(result.Data[6].NetworkShare >= result.Data[7].NetworkShare);
            Assert.True(result.Data[7].NetworkShare >= result.Data[8].NetworkShare);
            Assert.True(result.Data[8].NetworkShare >= result.Data[9].NetworkShare);
        }
        [Fact]
        public async Task GetValidatorsAsync_WithPublicKeyList_ReturnsExpectedData()
        {
            var parameters = new ValidatorsRequestParameters
            {
                FilterParameters = new ValidatorsFilterParameters
                {
                    EraId = "14027",
                    PublicKeys = new List<string>
                    {
                        _testPublicKey,
                        _test2PublicKey
                    }
                }
            };
            var result = await _restClient.Testnet.Validator.GetValidatorsAsync(parameters);
            Assert.NotNull(result);
            Assert.NotEmpty(result.Data);
            Assert.Contains(result.Data, value => value.PublicKey == _testPublicKey);
            Assert.Contains(result.Data, value => value.PublicKey == _test2PublicKey);
        }

    }
}