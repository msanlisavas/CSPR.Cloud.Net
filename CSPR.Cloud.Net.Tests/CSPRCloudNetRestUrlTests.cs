using CSPR.Cloud.Net.Clients.Api;
using CSPR.Cloud.Net.Objects.Account;
using CSPR.Cloud.Net.Objects.Auction;
using CSPR.Cloud.Net.Objects.Bidder;
using CSPR.Cloud.Net.Objects.Block;
using CSPR.Cloud.Net.Objects.Contract;
using CSPR.Cloud.Net.Objects.Delegate;
using CSPR.Cloud.Net.Objects.Deploy;
using CSPR.Cloud.Net.Objects.Nft;
using CSPR.Cloud.Net.Objects.Supply;
using CSPR.Cloud.Net.Objects.Transfer;
using CSPR.Cloud.Net.Objects.Validator;
using CSPR.Cloud.Net.Parameters.Filtering.Delegate;
using CSPR.Cloud.Net.Parameters.Filtering.Deploy;
using CSPR.Cloud.Net.Parameters.Filtering.Ft;
using CSPR.Cloud.Net.Parameters.Filtering.Nft;
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
using CSPR.Cloud.Net.Parameters.Wrapper.Accounts;
using CSPR.Cloud.Net.Parameters.Wrapper.Bidder;
using CSPR.Cloud.Net.Parameters.Wrapper.Block;
using CSPR.Cloud.Net.Parameters.Wrapper.Contract;
using CSPR.Cloud.Net.Parameters.Wrapper.Delegate;
using CSPR.Cloud.Net.Parameters.Wrapper.Deploy;
using CSPR.Cloud.Net.Parameters.Wrapper.Ft;
using CSPR.Cloud.Net.Parameters.Wrapper.Nft;
using CSPR.Cloud.Net.Parameters.Wrapper.Validator;
using Newtonsoft.Json;
using Xunit;

namespace CSPR.Cloud.Net.Tests
{
    /// <summary>
    /// Unit tests that exercise URL construction and JSON deserialization without hitting the network.
    /// Integration tests live in <see cref="CSPRCloudNetTests"/>; this file stays fast so the CI loop
    /// can validate wire-level changes (new endpoints, new filters, new includers, new response
    /// fields) in milliseconds.
    /// </summary>
    public class CSPRCloudNetRestUrlTests
    {
        private const string TestnetBase = "https://api.testnet.cspr.cloud";
        private const string PublicKey = "0106ca7c39cd272dbf21a86eeb3b36b7c26e2e9b94af64292419f7862936bca2ca";

        // =================================================================================
        // Batch A — new endpoints
        // =================================================================================

        [Fact]
        public void GetAccountUndelegations_NoParams_UsesUndelegationsPath()
        {
            var url = Endpoints.Delegate.GetAccountUndelegations(TestnetBase, PublicKey);
            Assert.Equal($"{TestnetBase}/accounts/{PublicKey}/undelegations", url);
        }

        [Fact]
        public void GetAccountUndelegations_WithPagination_AppendsPageAndPageSize()
        {
            var parameters = new DelegationRequestParameters { PageNumber = 2, PageSize = 25 };
            var url = Endpoints.Delegate.GetAccountUndelegations(TestnetBase, PublicKey, parameters);
            Assert.Contains("page=2", url);
            Assert.Contains("page_size=25", url);
        }

        [Fact]
        public void GetNFTs_NoParams_UsesUnscopedNftTokensPath()
        {
            var url = Endpoints.NFT.GetNFTs(TestnetBase);
            Assert.Equal($"{TestnetBase}/nft-tokens", url);
        }

        [Fact]
        public void GetNFTs_WithContractPackageHashFilter_AppendsFilter()
        {
            var parameters = new NFTsRequestParameters
            {
                FilterParameters = new NFTsFilterParameters
                {
                    ContractPackageHash = "f1127fd171695283b190a6253c5485cd830b5c1c365ea5661aead15e00dc1425"
                }
            };
            var url = Endpoints.NFT.GetNFTs(TestnetBase, parameters);
            Assert.Contains("contract_package_hash=f1127fd171695283b190a6253c5485cd830b5c1c365ea5661aead15e00dc1425", url);
        }

        [Fact]
        public void GetNFTs_WithOwnerHashAndBlockHeightRange_AppendsAll()
        {
            var parameters = new NFTsRequestParameters
            {
                FilterParameters = new NFTsFilterParameters
                {
                    OwnerHash = "102803f72be160c63422103853da0e6630ef99dbd667b37323e4112853dd4b1a",
                    FromBlockHeight = "7000000",
                    ToBlockHeight = "7600000"
                },
                OptionalParameters = new NFTOptionalParameters { ContractPackage = true }
            };
            var url = Endpoints.NFT.GetNFTs(TestnetBase, parameters);
            Assert.Contains("owner_hash=102803f72be160c63422103853da0e6630ef99dbd667b37323e4112853dd4b1a", url);
            Assert.Contains("from_block_height=7000000", url);
            Assert.Contains("to_block_height=7600000", url);
            Assert.Contains("includes=contract_package", url);
        }

        // =================================================================================
        // Batch A — deserialization checks for new response models
        // =================================================================================

        [Fact]
        public void UndelegationData_Deserializes_AllFields()
        {
            const string json = @"{
                ""public_key"": ""018afa98ca4be12d613617f7339a2d576950a2f9a92102ca4d6508ee31b54d2c02"",
                ""validator_public_key"": ""0106ca7c39cd272dbf21a86eeb3b36b7c26e2e9b94af64292419f7862936bca2ca"",
                ""amount"": ""500000000000"",
                ""bonding_purse"": ""uref-917e1aeba7e7d3a322e89069e88b6e0877a59d781840c0ce8223e990e0342d42-007"",
                ""delegator_identifier"": ""018afa98ca4be12d613617f7339a2d576950a2f9a92102ca4d6508ee31b54d2c02"",
                ""delegator_identifier_type_id"": 0,
                ""era_of_creation"": 21940,
                ""timestamp"": ""2026-04-22T10:00:00Z""
            }";

            var data = JsonConvert.DeserializeObject<UndelegationData>(json);
            Assert.NotNull(data);
            Assert.Equal("018afa98ca4be12d613617f7339a2d576950a2f9a92102ca4d6508ee31b54d2c02", data.PublicKey);
            Assert.Equal("500000000000", data.Amount);
            Assert.Equal(0, data.DelegatorIdentifierTypeId);
            Assert.Equal(21940UL, data.EraOfCreation);
            Assert.NotNull(data.Timestamp);
        }

        [Fact]
        public void UndelegationData_Deserializes_PurseDelegator()
        {
            const string json = @"{
                ""public_key"": null,
                ""validator_public_key"": ""0106ca7c39cd272dbf21a86eeb3b36b7c26e2e9b94af64292419f7862936bca2ca"",
                ""amount"": ""100000000000"",
                ""bonding_purse"": ""uref-2372be06c70aa026df441b070cb8aaf91c6453e445d0ea2e31177ef5a05c2a53-007"",
                ""delegator_identifier"": ""uref-fc3f0684d19865a5c020536e499dd3d9cf1c08201b85474762e86de7e85c0d34-007"",
                ""delegator_identifier_type_id"": 1
            }";

            var data = JsonConvert.DeserializeObject<UndelegationData>(json);
            Assert.NotNull(data);
            Assert.Null(data.PublicKey);
            Assert.Equal(1, data.DelegatorIdentifierTypeId);
            Assert.StartsWith("uref-", data.DelegatorIdentifier);
        }

        // =================================================================================
        // Batch B — new filters
        // =================================================================================

        [Fact]
        public void GetDeploys_WithCallerHashFilter_AppendsCallerHash()
        {
            var parameters = new DeploysRequestParameters
            {
                FilterParameters = new DeploysFilterParameters
                {
                    CallerHash = "9393fb979bbe817d99b3038236b7f0254d2c6d1e52da43415b5dad39d247c918"
                }
            };
            var url = Endpoints.Deploy.GetDeploys(TestnetBase, parameters);
            Assert.Contains("caller_hash=9393fb979bbe817d99b3038236b7f0254d2c6d1e52da43415b5dad39d247c918", url);
        }

        [Fact]
        public void GetFungibleTokenActions_WithActionTypeIdFilter_AppendsFtActionTypeId()
        {
            var parameters = new FTActionRequestParameters
            {
                FilterParameters = new FTActionFilterParameters { FtActionTypeId = "2" }
            };
            var url = Endpoints.FT.GetFungibleTokenActions(TestnetBase, parameters);
            Assert.Contains("ft_action_type_id=2", url);
        }

        [Fact]
        public void GetAccountFungibleTokenActions_WithActionTypeIdFilter_AppendsFtActionTypeId()
        {
            var parameters = new FTAccountActionRequestParameters
            {
                FilterParameters = new FTAccountActionFilterParameters { FtActionTypeId = "5" }
            };
            var url = Endpoints.FT.GetAccountFungibleTokenActions(TestnetBase, PublicKey, parameters);
            Assert.Contains("ft_action_type_id=5", url);
        }

        [Fact]
        public void GetContractPackageFungibleTokenActions_WithActionTypeIdFilter_AppendsFtActionTypeId()
        {
            var parameters = new FTContractPackageActionRequestParameters
            {
                FilterParameters = new FTContractPackageActionFilterParameters { FtActionTypeId = "7" }
            };
            var url = Endpoints.FT.GetContractPackageFungibleTokenActions(TestnetBase, "abcd", parameters);
            Assert.Contains("ft_action_type_id=7", url);
        }

        [Fact]
        public void GetAccountNFTActions_WithNftActionIdFilter_AppendsNftActionId()
        {
            var parameters = new NFTAccountActionsRequestParameters
            {
                FilterParameters = new NFTAccountActionsFilterParameters { NftActionId = "3" }
            };
            var url = Endpoints.NFT.GetAccountNFTActions(TestnetBase, PublicKey, parameters);
            Assert.Contains("nft_action_id=3", url);
        }

        [Fact]
        public void GetContractPackageNFTActions_WithNftActionIdFilter_AppendsNftActionId()
        {
            var parameters = new NFTContractPackageActionsRequestParameters
            {
                FilterParameters = new NFTContractPackageActionsFilterParameters { NftActionId = "1" }
            };
            var url = Endpoints.NFT.GetContractPackageNFTActions(TestnetBase, "abcd", parameters);
            Assert.Contains("nft_action_id=1", url);
        }

        [Fact]
        public void GetContractPackageNFTs_WithOwnerHashFilter_AppendsOwnerHash()
        {
            var parameters = new NFTContractPackageRequestParameters
            {
                FilterParameters = new NFTContractPackageFilterParameters
                {
                    OwnerHash = "102803f72be160c63422103853da0e6630ef99dbd667b37323e4112853dd4b1a"
                }
            };
            var url = Endpoints.NFT.GetContractPackageNFTs(TestnetBase, "abcd", parameters);
            Assert.Contains("owner_hash=102803f72be160c63422103853da0e6630ef99dbd667b37323e4112853dd4b1a", url);
        }

        [Fact]
        public void GetAccountDelegatorRewards_WithEraRangeFilter_AppendsFromAndToEraId()
        {
            var parameters = new AccountDelegatorRewardRequestParameters
            {
                FilterParameters = new AccountDelegatorRewardFilterParameters
                {
                    FromEraId = "21900",
                    ToEraId = "21950"
                }
            };
            var url = Endpoints.Delegate.GetAccountDelegatorRewards(TestnetBase, PublicKey, parameters);
            Assert.Contains("from_era_id=21900", url);
            Assert.Contains("to_era_id=21950", url);
        }

        [Fact]
        public void GetValidatorRewards_WithEraRangeFilter_AppendsFromAndToEraId()
        {
            var parameters = new ValidatorRewardsRequestParameters
            {
                FilterParameters = new ValidatorRewardsFilterParameters
                {
                    FromEraId = "21900",
                    ToEraId = "21950"
                }
            };
            var url = Endpoints.Validator.GetValidatorRewards(TestnetBase, PublicKey, parameters);
            Assert.Contains("from_era_id=21900", url);
            Assert.Contains("to_era_id=21950", url);
        }

        [Fact]
        public void GetValidatorEraRewards_WithEraRangeFilter_AppendsFromAndToEraId()
        {
            var parameters = new ValidatorEraRewardsRequestParameters
            {
                FilterParameters = new ValidatorRewardsFilterParameters
                {
                    FromEraId = "21900",
                    ToEraId = "21950"
                }
            };
            var url = Endpoints.Validator.GetValidatorEraRewards(TestnetBase, PublicKey, parameters);
            Assert.Contains("from_era_id=21900", url);
            Assert.Contains("to_era_id=21950", url);
        }

        [Fact]
        public void GetAccountFungibleTokenOwnership_WithContractPackageHashFilter_AppendsFilter()
        {
            var parameters = new FTAccountOwnershipRequestParameters
            {
                FilterParameters = new FTAccountOwnershipFilterParameters
                {
                    ContractPackageHash = "dbb3284da4e20be62aeb332c653bfa715c7fa1ef6a73393cd36804b382f10d4e"
                }
            };
            var url = Endpoints.FT.GetAccountFungibleTokenOwnership(TestnetBase, PublicKey, parameters);
            Assert.Contains("contract_package_hash=dbb3284da4e20be62aeb332c653bfa715c7fa1ef6a73393cd36804b382f10d4e", url);
        }

        // =================================================================================
        // Batch C — new includers
        // =================================================================================

        [Fact]
        public void GetContractPackage_WithAllNewIncluders_AppendsAll()
        {
            var parameters = new ContractPackageRequestParameters
            {
                OptionalParameters = new ContractPackageOptionalParameters
                {
                    TokenMarketData = 1,
                    CoingeckoData = true,
                    FriendlymarketData = true,
                    CsprtradeData = true,
                    OwnerCsprName = true
                }
            };
            var url = Endpoints.Contract.GetContractPackages(TestnetBase, parameters);
            Assert.Contains("token_market_data(1)", url);
            Assert.Contains("coingecko_data", url);
            Assert.Contains("friendlymarket_data", url);
            Assert.Contains("csprtrade_data", url);
            Assert.Contains("owner_cspr_name", url);
        }

        [Fact]
        public void GetAccount_WithCsprNameAndRank_AppendsBoth()
        {
            var parameters = new AccountsOptionalParameters { CsprName = true, Rank = true };
            var url = Endpoints.Account.GetAccount(TestnetBase, PublicKey, parameters);
            Assert.Contains("cspr_name", url);
            Assert.Contains("rank", url);
        }

        [Fact]
        public void GetValidator_WithCsprName_AppendsCsprName()
        {
            var parameters = new ValidatorRequestParameters
            {
                OptionalParameters = new ValidatorOptionalParameters { CsprName = true }
            };
            var url = Endpoints.Validator.GetValidator(TestnetBase, PublicKey, parameters);
            Assert.Contains("cspr_name", url);
        }

        [Fact]
        public void GetBidder_WithCsprName_AppendsCsprName()
        {
            var parameters = new BidderRequestParameters
            {
                OptionalParameters = new BidderOptionalParameters { CsprName = true }
            };
            var url = Endpoints.Bidder.GetBidder(TestnetBase, PublicKey, parameters);
            Assert.Contains("cspr_name", url);
        }

        [Fact]
        public void GetDeploy_WithCallerCsprName_AppendsCallerCsprName()
        {
            var parameters = new DeployRequestParameters
            {
                OptionalParameters = new DeployOptionalParameters { CallerCsprName = true }
            };
            var url = Endpoints.Deploy.GetDeploy(TestnetBase, "abc", parameters);
            Assert.Contains("caller_cspr_name", url);
        }

        [Fact]
        public void GetAccountDelegations_WithCsprNameIncluders_AppendsAll()
        {
            var parameters = new DelegationRequestParameters
            {
                OptionalParameters = new DelegationOptionalParameters
                {
                    CsprName = true,
                    ValidatorCsprName = true,
                    Bidder = true
                }
            };
            var url = Endpoints.Delegate.GetAccountDelegations(TestnetBase, PublicKey, parameters);
            Assert.Contains("cspr_name", url);
            Assert.Contains("validator_cspr_name", url);
            Assert.Contains("bidder", url);
        }

        [Fact]
        public void GetAccountDelegatorRewards_WithCsprNameIncluders_AppendsAll()
        {
            var parameters = new AccountDelegatorRewardRequestParameters
            {
                OptionalParameters = new AccountDelegatorRewardOptionalParameters
                {
                    CsprName = true,
                    ValidatorCsprName = true,
                    ValidatorAccountInfo = true
                }
            };
            var url = Endpoints.Delegate.GetAccountDelegatorRewards(TestnetBase, PublicKey, parameters);
            Assert.Contains("cspr_name", url);
            Assert.Contains("validator_cspr_name", url);
            Assert.Contains("validator_account_info", url);
        }

        [Fact]
        public void GetBlock_WithProposerCsprNameAndCentralized_AppendsBoth()
        {
            var parameters = new BlockOptionalParameters
            {
                ProposerCsprName = true,
                ProposerCentralizedAccountInfo = true
            };
            var url = Endpoints.Block.GetBlock(TestnetBase, "abcd", parameters);
            Assert.Contains("proposer_cspr_name", url);
            Assert.Contains("proposer_centralized_account_info", url);
        }

        [Fact]
        public void GetValidatorRewards_WithCsprNameIncluder_AppendsCsprName()
        {
            var parameters = new Parameters.Wrapper.Validator.ValidatorRewardsRequestParameters
            {
                OptionalParameters = new ValidatorRewardsOptionalParameters { CsprName = true, AccountInfo = true }
            };
            var url = Endpoints.Validator.GetValidatorRewards(TestnetBase, PublicKey, parameters);
            Assert.Contains("cspr_name", url);
            Assert.Contains("account_info", url);
        }

        [Fact]
        public void GetAccountTransfers_WithCsprNameIncluders_AppendsAll()
        {
            var parameters = new Parameters.Wrapper.Transfer.TransferAccountRequestParameters
            {
                OptionalParameters = new TransferAccountOptionalParameters
                {
                    InitiatorCsprName = true,
                    ToCsprName = true,
                    FromPurseCsprName = true,
                    ToPurseCsprName = true
                }
            };
            var url = Endpoints.Transfer.GetAccountTransfers(TestnetBase, PublicKey, parameters);
            Assert.Contains("initiator_cspr_name", url);
            Assert.Contains("to_cspr_name", url);
            Assert.Contains("from_purse_cspr_name", url);
            Assert.Contains("to_purse_cspr_name", url);
        }

        [Fact]
        public void GetFungibleTokenActions_WithCsprNameIncluders_AppendsAll()
        {
            var parameters = new FTActionRequestParameters
            {
                OptionalParameters = new FTActionOptionalParameters
                {
                    FromCsprName = true,
                    ToCsprName = true
                }
            };
            var url = Endpoints.FT.GetFungibleTokenActions(TestnetBase, parameters);
            Assert.Contains("from_cspr_name", url);
            Assert.Contains("to_cspr_name", url);
        }

        // =================================================================================
        // Batches D + E — response-property additions and string-balance migration
        // =================================================================================

        [Fact]
        public void DeployData_Deserializes_Casper20Fields()
        {
            const string json = @"{
                ""deploy_hash"": ""2819f2250cc4a3dca861677ff71e1bd7d09adc59e828a347a40368f49b0834f9"",
                ""block_hash"": ""c5bdbe64ede507b486fc36c901dbfcdad89b8c97bdb6751052514e5ab8d603cf"",
                ""block_height"": 7608280,
                ""caller_public_key"": ""01e2614eb93fb15758ae9e5b47349920c82d4f6c1aeb40b55f7dd6c862f5796687"",
                ""caller_hash"": ""9393fb979bbe817d99b3038236b7f0254d2c6d1e52da43415b5dad39d247c918"",
                ""consumed_gas"": ""16917010891"",
                ""cost"": ""21000000000"",
                ""entry_point_id"": 2697572,
                ""execution_type_id"": 4,
                ""gas_price_limit"": 1,
                ""is_standard_payment"": true,
                ""payment_amount"": ""21000000000"",
                ""pricing_mode_id"": 0,
                ""refund_amount"": ""3062241831"",
                ""runtime_type_id"": 1,
                ""status"": ""processed"",
                ""timestamp"": ""2026-04-22T19:03:40Z"",
                ""version_id"": 2
            }";

            var data = JsonConvert.DeserializeObject<DeployData>(json);
            Assert.NotNull(data);
            Assert.Equal("9393fb979bbe817d99b3038236b7f0254d2c6d1e52da43415b5dad39d247c918", data.CallerHash);
            Assert.Equal("16917010891", data.ConsumedGas);
            Assert.Equal("3062241831", data.RefundAmount);
            Assert.Equal((byte)1, data.RuntimeTypeId);
            Assert.Equal((byte)2, data.VersionId);
            Assert.True(data.IsStandardPayment);
            Assert.Equal((byte)0, data.PricingModeId);
            Assert.Equal(1UL, data.GasPriceLimit);
        }

        [Fact]
        public void ValidatorData_Deserializes_StringBalancesAndNewFields()
        {
            const string json = @"{
                ""bid_amount"": ""7288505920785157"",
                ""delegators_number"": 172,
                ""delegators_stake"": ""36807400826653794"",
                ""era_id"": 21947,
                ""fee"": 10,
                ""is_active"": true,
                ""maximum_delegation_amount"": ""1000000000000000000"",
                ""minimum_delegation_amount"": ""500000000000"",
                ""network_share"": ""7.16321553696719"",
                ""public_key"": ""0106ca7c39cd272dbf21a86eeb3b36b7c26e2e9b94af64292419f7862936bca2ca"",
                ""rank"": 1,
                ""reserved_slots"": 0,
                ""self_share"": ""16.5285099742932"",
                ""self_stake"": ""7288505920785157"",
                ""total_stake"": ""44095906747438951""
            }";

            var data = JsonConvert.DeserializeObject<ValidatorData>(json);
            Assert.NotNull(data);
            Assert.Equal("7288505920785157", data.BidAmount);
            Assert.Equal("36807400826653794", data.DelegatorsStake);
            Assert.Equal("44095906747438951", data.TotalStake);
            Assert.Equal("7288505920785157", data.SelfStake);
            Assert.Equal("1000000000000000000", data.MaximumDelegationAmount);
            Assert.Equal("500000000000", data.MinimumDelegationAmount);
            Assert.Equal(0u, data.ReservedSlots);
            Assert.Equal(1u, data.Rank);
            Assert.Equal("7.16321553696719", data.NetworkShare);
        }

        [Fact]
        public void SupplyData_Deserializes_AnnualIssuanceBreakdown()
        {
            const string json = @"{
                ""token"": ""CSPR"",
                ""total"": 13435666773,
                ""circulating"": 11011257919,
                ""total_annual_issuance"": 0.003949780196042418,
                ""annual_ecosystem_sustain_issuance"": 0.0009874450490106046,
                ""annual_staking_rewards_issuance"": 0.0029623351470318138,
                ""annual_issuance"": 0.003949780196042418,
                ""timestamp"": 1776884963
            }";

            var data = JsonConvert.DeserializeObject<SupplyData>(json);
            Assert.NotNull(data);
            Assert.Equal(0.003949780196042418, data.TotalAnnualIssuance);
            Assert.Equal(0.0009874450490106046, data.AnnualEcosystemSustainIssuance);
            Assert.Equal(0.0029623351470318138, data.AnnualStakingRewardsIssuance);
            Assert.Equal(0.003949780196042418, data.AnnualIssuance);
            Assert.Equal(1776884963L, data.Timestamp);
        }

        [Fact]
        public void ContractPackageData_Deserializes_NewFields()
        {
            const string json = @"{
                ""coingecko_id"": ""casper-network"",
                ""contract_package_hash"": ""b73a02e1cf51a91cf4a9974cc699c04f2a44aef34d958bec99303660771f0c56"",
                ""is_contract_info_approved"": true,
                ""is_featured"": false,
                ""owner_hash"": ""53280c6857d1d8bbeec6b7c77ff51e28cbd8948962dc46f6bbd6dce38acfb57d"",
                ""owner_public_key"": ""0203c2049f9e36c843be29376f017d0a00d7b08f0369d71d2439488b0f4887ba9b4c"",
                ""website_url"": ""https://example.com"",
                ""timestamp"": ""2026-04-22T15:37:02Z""
            }";

            var data = JsonConvert.DeserializeObject<ContractPackageData>(json);
            Assert.NotNull(data);
            Assert.Equal("casper-network", data.CoingeckoId);
            Assert.True(data.IsContractInfoApproved);
            Assert.False(data.IsFeatured);
            Assert.Equal("53280c6857d1d8bbeec6b7c77ff51e28cbd8948962dc46f6bbd6dce38acfb57d", data.OwnerHash);
            Assert.Equal("https://example.com", data.WebsiteUrl);
        }

        [Fact]
        public void AccountData_Deserializes_StringBalances()
        {
            const string json = @"{
                ""public_key"": ""0106ca7c39cd272dbf21a86eeb3b36b7c26e2e9b94af64292419f7862936bca2ca"",
                ""balance"": ""99999999999999999999"",
                ""staked_balance"": ""7288505920785157"",
                ""delegated_balance"": ""30626909927320739"",
                ""undelegated_balance"": ""0"",
                ""undelegating_balance"": ""0""
            }";

            var data = JsonConvert.DeserializeObject<AccountData>(json);
            Assert.NotNull(data);
            // value exceeds uint64; must not lose precision
            Assert.Equal("99999999999999999999", data.Balance);
            Assert.Equal("7288505920785157", data.StakedBalance);
            Assert.Equal("30626909927320739", data.DelegatedBalance);
        }

        [Fact]
        public void BlockData_Deserializes_Casper20TxnBuckets()
        {
            const string json = @"{
                ""auction_txn_number"": 0,
                ""block_hash"": ""4793ae674cd2a5367b9ce3222b370ebaac54fc7ac118f26b59021c6cd90e67e7"",
                ""block_height"": 7608322,
                ""contract_calls_number"": 0,
                ""era_id"": 21947,
                ""gas_price"": 1,
                ""install_upgrade_txn_number"": 0,
                ""is_switch_block"": false,
                ""large_txn_number"": 0,
                ""medium_txn_number"": 0,
                ""native_transfers_number"": 0,
                ""parent_block_hash"": ""af5ca75cc9f094d86c2fd126439b5f4b7160c8cdf4cb33a57e0e0977593ccaa1"",
                ""proposer_public_key"": ""017d96b9a63abcb61c870a4f55187a0a7ac24096bdb5fc585c12a686a4d892009e"",
                ""small_txn_number"": 0,
                ""state_root_hash"": ""74ac374969a1dc86505a468d80a063bf21371839d2a137162959123bff862a1c"",
                ""timestamp"": ""2026-04-22T19:09:21Z"",
                ""version_id"": 1
            }";

            var data = JsonConvert.DeserializeObject<BlockData>(json);
            Assert.NotNull(data);
            Assert.Equal(1u, data.GasPrice);
            Assert.Equal((byte)1, data.VersionId);
            Assert.Equal(0, (ushort)data.AuctionTxnNumber);
            Assert.Equal(0, (ushort)data.InstallUpgradeTxnNumber);
            Assert.Equal(0, (ushort)data.SmallTxnNumber);
            Assert.Equal(0, (ushort)data.MediumTxnNumber);
            Assert.Equal(0, (ushort)data.LargeTxnNumber);
        }

        [Fact]
        public void AuctionMetricsData_Deserializes_StringTotalActiveStake()
        {
            const string json = @"{
                ""current_era_id"": 21947,
                ""active_validator_number"": 57,
                ""total_bids_number"": 18520,
                ""active_bids_number"": 57,
                ""total_active_era_stake"": ""615588160371180893""
            }";

            var data = JsonConvert.DeserializeObject<AuctionMetricsData>(json);
            Assert.NotNull(data);
            Assert.Equal("615588160371180893", data.TotalActiveEraStake);
        }

        [Fact]
        public void DelegationData_Deserializes_PurseDelegator()
        {
            const string json = @"{
                ""bonding_purse"": ""uref-2372be06c70aa026df441b070cb8aaf91c6453e445d0ea2e31177ef5a05c2a53-007"",
                ""delegator_identifier"": ""uref-fc3f0684d19865a5c020536e499dd3d9cf1c08201b85474762e86de7e85c0d34-007"",
                ""delegator_identifier_type_id"": 1,
                ""stake"": ""5636306374125273"",
                ""validator_public_key"": ""0106ca7c39cd272dbf21a86eeb3b36b7c26e2e9b94af64292419f7862936bca2ca""
            }";

            var data = JsonConvert.DeserializeObject<DelegationData>(json);
            Assert.NotNull(data);
            Assert.StartsWith("uref-", data.DelegatorIdentifier);
            Assert.Equal(1, data.DelegatorIdentifierTypeId);
        }

        [Fact]
        public void ValidatorRewardData_Deserializes_StringAmountAndType()
        {
            const string json = @"{
                ""public_key"": ""0106ca7c39cd272dbf21a86eeb3b36b7c26e2e9b94af64292419f7862936bca2ca"",
                ""era_id"": 21947,
                ""amount"": ""123456789012345678"",
                ""type"": 0,
                ""timestamp"": ""2026-04-22T19:09:21Z""
            }";

            var data = JsonConvert.DeserializeObject<ValidatorRewardData>(json);
            Assert.NotNull(data);
            Assert.Equal("123456789012345678", data.Amount);
            Assert.Equal((byte)0, data.Type);
        }

        [Fact]
        public void BidderData_Deserializes_StringStakesAndNewFields()
        {
            const string json = @"{
                ""bid_amount"": ""7288505920785157"",
                ""delegators_number"": 172,
                ""delegators_stake"": ""36807400826653794"",
                ""era_id"": 21947,
                ""fee"": 10,
                ""is_active"": true,
                ""maximum_delegation_amount"": ""1000000000000000000"",
                ""minimum_delegation_amount"": ""500000000000"",
                ""network_share"": ""7.16"",
                ""public_key"": ""0106ca7c39cd272dbf21a86eeb3b36b7c26e2e9b94af64292419f7862936bca2ca"",
                ""rank"": 1,
                ""reserved_slots"": 0,
                ""self_share"": ""16.52"",
                ""self_stake"": ""7288505920785157"",
                ""total_stake"": ""44095906747438951""
            }";

            var data = JsonConvert.DeserializeObject<BidderData>(json);
            Assert.NotNull(data);
            Assert.Equal("7288505920785157", data.BidAmount);
            Assert.Equal(172UL, data.DelegatorsNumber);
            Assert.Equal(0u, data.ReservedSlots);
            Assert.Equal("44095906747438951", data.TotalStake);
        }

        [Fact]
        public void NFTTokenData_Deserializes_FromLiveApiShape()
        {
            const string json = @"{
                ""block_height"": 7592514,
                ""contract_package_hash"": ""f1127fd171695283b190a6253c5485cd830b5c1c365ea5661aead15e00dc1425"",
                ""is_burned"": false,
                ""owner_hash"": ""102803f72be160c63422103853da0e6630ef99dbd667b37323e4112853dd4b1a"",
                ""owner_type"": 0,
                ""timestamp"": ""2026-04-21T08:01:23Z"",
                ""token_id"": ""12284936144027871778033568559102708798884965623732709450304319273911201749555"",
                ""token_standard_id"": 3,
                ""tracking_id"": ""456946""
            }";

            var data = JsonConvert.DeserializeObject<NFTTokenData>(json);
            Assert.NotNull(data);
            Assert.Equal(7592514UL, data.BlockHeight);
            Assert.Equal("f1127fd171695283b190a6253c5485cd830b5c1c365ea5661aead15e00dc1425", data.ContractPackageHash);
            Assert.False(data.IsBurned);
            Assert.Equal((byte)3, data.TokenStandardId);
        }
    }
}
