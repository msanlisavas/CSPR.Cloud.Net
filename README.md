# CSPR.Cloud.Net

[![NuGet Version](https://img.shields.io/nuget/v/CSPR.Cloud.Net?logo=nuget&label=NuGet)](https://www.nuget.org/packages/CSPR.Cloud.Net)
[![NuGet Downloads](https://img.shields.io/nuget/dt/CSPR.Cloud.Net?logo=nuget&label=Downloads)](https://www.nuget.org/packages/CSPR.Cloud.Net)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](./LICENSE.txt)
[![.NET Standard](https://img.shields.io/badge/.NET%20Standard-2.0%20%7C%202.1-512BD4?logo=dotnet)](https://learn.microsoft.com/dotnet/standard/net-standard)

A .NET client library for the CSPR Cloud API — access Casper blockchain data (Mainnet & Testnet) with type-safe methods, filtering, sorting, pagination, and a WebSocket Streaming API.

## Release Notes
### v2.0.0
Catches the SDK up with the CSPR Cloud API through **v2.9.0** (2026-02). Covers every changelog entry from v2.0.3 → v2.9.0 and includes Casper 2.0 fields across Deploy / Validator / Bidder / Block / Supply.

**⚠️ Breaking changes**
- **Balance and stake fields migrated from `ulong?` to `string`** to match the v2.4.3 wire format and avoid uint64 overflow on large accounts.
  - `AccountData`: `Balance`, `DelegatedBalance`, `UndelegatedBalance`, `StakedBalance`, `UndelegatingBalance`
  - `ValidatorData`: `SelfStake`, `DelegatorsStake`, `TotalStake`, `SelfShare`, `NetworkShare` (plus new `BidAmount`, `MinimumDelegationAmount`, `MaximumDelegationAmount`, `PendingUnstakingAmount`)
  - `BidderData`: same set as Validator
  - `AuctionMetricsData.TotalActiveEraStake`
  - `ValidatorRewardData.Amount`
- **`SupplyData.Timestamp`** migrated from `DateTime?` to `long?` (Unix seconds — the endpoint emits it as a number, not ISO-8601).
- Callers comparing these fields must parse with `BigInteger.Parse` / `decimal.Parse` / `long`.

**New endpoints**
```csharp
// Account undelegations (v2.5.1) — pending funds in the 7-era unbonding window
var pending = await client.Testnet.Delegate.GetAccountUndelegationsAsync("01publicKey...");
// Returns PaginatedResponse<UndelegationData> with DelegatorIdentifier + DelegatorIdentifierTypeId

// Unscoped NFT listing with filtering across the whole network
var nfts = await client.Testnet.NFT.GetNFTsAsync(new NFTsRequestParameters
{
    FilterParameters = new NFTsFilterParameters
    {
        ContractPackageHash = "pkg1...",
        OwnerHash = "hash1..."
    }
});
```

**New filters**
- `DeploysFilterParameters.CallerHash` (v2.0.12)
- `ft_action_type_id` on all three FT-action filter classes (v2.0.20)
- `nft_action_id` on both NFT-action filter classes (v2.0.20)
- `owner_hash` on `NFTContractPackageFilterParameters` (v2.0.20)
- `from_era_id` / `to_era_id` on `AccountDelegatorRewardFilterParameters` and the new `ValidatorRewardsFilterParameters` (v2.4.0) — wired into both `GetValidatorRewardsAsync` and `GetValidatorEraRewardsAsync`
- `contract_package_hash` on the new `FTAccountOwnershipFilterParameters` (v2.6.0)
- `GetAccountNFTActionsAsync` URL builder now threads `FilterParameters` (was previously unwired)

**New includers**
- `ContractPackageOptionalParameters`: `TokenMarketData` (function includer, takes a currency id), `CoingeckoData`, `FriendlymarketData`, `CsprtradeData`, `OwnerCsprName`
- CSPR.name sweep across Account, Validator, Bidder, Delegation, DelegatorReward, Deploy caller, Transfer, Block proposer, FT/NFT actions & ownership
- `AccountsOptionalParameters` gains `Rank`

**New response properties**
- `DeployData` (Casper 2.0): `CallerHash`, `VersionId`, `PricingModeId`, `GasPriceLimit`, `IsStandardPayment`, `RuntimeTypeId`, `ConsumedGas`, `RefundAmount`, `CallerCsprName`
- `ValidatorData` / `BidderData`: `BidAmount`, `ReservedSlots`, min/max delegation amounts, `PendingUnstakingAmount`, `EraId`, `DelegatorsNumber`, `DelegatorsStake`, `CsprName`
- `SupplyData`: `TotalAnnualIssuance`, `AnnualEcosystemSustainIssuance`, `AnnualStakingRewardsIssuance`, `AnnualIssuance`
- `ContractPackageData`: `WebsiteUrl`, `OwnerHash`, `CoingeckoId`, `IsContractInfoApproved`, `IsFeatured`, `OwnerCsprName`, plus raw `JObject` payloads for the four market-data includers
- `BlockData`: Casper 2.0 transaction buckets (`AuctionTxnNumber`, `InstallUpgradeTxnNumber`, `SmallTxnNumber`, `MediumTxnNumber`, `LargeTxnNumber`), `GasPrice`, `VersionId`, `ProposerCentralizedAccountInfo`, `ProposerCsprName`
- `TransferData`: `TransferIndex` + four CSPR.name fields (initiator / to / from-purse / to-purse)
- `DelegationData` / `DelegatorRewardData`: `DelegatorIdentifier` + `DelegatorIdentifierTypeId` (v2.1.0 purse delegation support), plus CSPR.name fields
- `ValidatorRewardData`: `Type` (Casper 2.0 RewardTypeID) and string-typed `Amount`

**Streaming**
- Contracts streaming channel (`.Contract`) verified against `streaming.testnet.cspr.cloud/contracts` and unskipped.

**Internal**
- Added `CSPRCloudNetRestUrlTests.cs` with 40 fast unit tests covering URL construction + deserialization of every new field (sub-100ms run time).
- Full surface audit saved to `docs/audit-2026-04-22.md`.

### v1.2.1
Fix: the Streaming API pump now tolerates the plaintext `Ping` keepalive text frames that the CSPR Cloud server interleaves between JSON envelopes. v1.2.0 would terminate the subscription with a `JsonReaderException` on the first keepalive (~5s after connect). Non-JSON frames are now skipped via a new `IsJsonEnvelope` guard.

### v1.2.0
Adds the full **Streaming API** — WebSocket-based real-time subscriptions — via the new `CasperCloudSocketClient`.

**All 10 streaming channels implemented** (Mainnet + Testnet):
- **Account balance** — `updated` events
- **Block** — `created` events
- **Contract** — `created` events
- **Contract package** — `created`, `updated` events
- **Contract-level events** — `emitted` events (with optional `raw_data`)
- **Deploy** — `created` events
- **Fungible token action** — `created` events
- **NFT** — `created`, `updated` events
- **NFT action** — `created` events
- **Transfer** — `created` events

**Other changes:**
- **Auto-reconnect with exponential backoff + jitter** via opt-in `StreamReconnectPolicy` — addresses the CSPR docs' guidance that "WebSocket connections may close during API deployments, necessitating reconnection logic in client applications"
- **`Persistent-Session` header** support for replaying queued messages across reconnects (paid tiers, Beta)
- **`INetworkSocketEndpoint`** interface mirrors the REST `INetworkEndpoint`, so the same runtime-switching pattern works for streams
- **`CasperCloudRestClient.Mainnet`/`Testnet` now typed as `INetworkEndpoint`** — lets `var network = useTestnet ? client.Testnet : client.Mainnet;` compile
- 50 new tests covering URL construction, envelope deserialization, reconnect policy math, retry/veto/fatal/cancellation paths, and live handshake checks against testnet for every stream

**Usage:**
```csharp
var socket = new CasperCloudSocketClient(
    new CasperCloudClientConfig("your-api-key"),
    reconnectPolicy: new StreamReconnectPolicy { Enabled = true });

using var cts = new CancellationTokenSource();
await socket.Testnet.Block.SubscribeAsync(
    parameters: null,
    onMessage: msg => { Console.WriteLine(msg.Data.BlockHash); return Task.CompletedTask; },
    cancellationToken: cts.Token);
```

### v1.1.0
Added 20 new API endpoints covering 8 new categories and 6 missing endpoints in existing categories, aligning with CSPR.cloud API v2.9.0.

**New Categories:**
- **DEX** - Get list of DEXes
- **Swap** - Get paginated list of fungible token trades with filtering, sorting, and optional properties
- **FT Rate** - Get latest and historical fungible token rates by currency
- **FT Daily Rate** - Get latest and historical daily aggregated token rates
- **FT DEX Rate** - Get latest and historical token-to-token exchange rates
- **FT Daily DEX Rate** - Get latest and historical daily token-to-token rates
- **CSPR.name Resolution** - Resolve CSPR.name to account hash
- **Awaiting Deploy** - Create, add signatures to, and retrieve awaiting deploys (multisig workflows)

**New Endpoints in Existing Categories:**
- **Purse Transfers** - Get transfers by purse URef
- **Purse Delegations** - Get delegations by purse URef
- **Purse Delegation Rewards** - Get delegation rewards and totals by purse URef
- **Validator Era Rewards** - Get validator rewards aggregated by era
- **FT Action Types** - Get list of fungible token action types

**Other Changes:**
- Added `PostDataAsync` support for POST endpoints
- Added 20 new unit tests

**Usage Examples:**

```csharp
// Get DEXes
var dexes = await restClient.Testnet.Dex.GetDexesAsync();

// Get swaps with filtering
var swapParams = new SwapRequestParameters { PageSize = 25 };
var swaps = await restClient.Testnet.Swap.GetSwapsAsync(swapParams);

// Get latest FT rate
var filterParams = new FTRateFilterParameters { CurrencyId = "1" };
var rate = await restClient.Testnet.FT.GetFTRateLatestAsync("contract-package-hash", filterParams);

// Get historical FT daily rates
var dailyParams = new FTDailyRateRequestParameters();
dailyParams.FilterParameters.CurrencyId = "1";
var dailyRates = await restClient.Testnet.FT.GetFTDailyRatesAsync("contract-package-hash", dailyParams);

// Resolve CSPR.name
var resolution = await restClient.Testnet.CsprName.GetCsprNameResolutionAsync("cloud.cspr");

// Get validator era rewards
var eraRewards = await restClient.Testnet.Validator.GetValidatorEraRewardsAsync("validator-public-key");

// Get purse transfers
var purseTransfers = await restClient.Testnet.Transfer.GetPurseTransfersAsync("uref-...-007");
```

### v1.0.11
- Added `era_id` and `is_switch_block` filter parameters for the `GetBlocks` endpoint
- You can now filter blocks by era and switch block status:
```csharp
var parameters = new BlockRequestParameters
{
    FilterParameters = new BlockFilterParameters
    {
        EraId = "21180",
        IsSwitchBlock = true
    }
};
var result = await restClient.Testnet.Block.GetBlocksAsync(parameters);
```

### v1.0.10
- Added `INetworkEndpoint` interface allowing easy switching between Mainnet and Testnet endpoints with type safety

### v1.0.9
- Enhanced `GetContractPackageFungibleTokenOwnership` endpoint with sorting by balance and optional parameters

### v1.0.8
- Added `contract_package_hash` and `account_hash` filters to `FTAccountActionFilterParameters`
- Enhanced fungible token action filtering capabilities for `GetAccountFTActionsAsync` methods on both Mainnet and Testnet

### v1.0.7
- Update tests and client for new validator performance data

Updated `CSPRCloudNetTests.cs`:
- Added assertion to check `Score` values are not null.
- Replaced `AverageScore` ordering checks with `Score` ordering checks.

Updated `CasperCloudRestClient.cs`:
- Changed return type of `GetHistoricalValidatorsAveragePerformanceAsync` to `PaginatedResponse<RelativeValidatorPerformanceData>`.
- Updated endpoint call, method signature, and documentation to reflect new return type.

These changes ensure compatibility with updated data structures and API responses, improving the accuracy and reliability of tests and client methods.


### v1.0.6
- Fixed mainnet baseUrl
### v1.0.2
- Fixed an issue where mainnet endpoints were using testnet baseurl.
- Changes:
  - GetAccountInfo endpoint replaced with GetAccountInfoAsync
### v.1.0.0 
- Initial Release

## Supported Frameworks
The library is targeting both `.NET Standard 2.0` and `.NET Standard 2.1` for optimal compatibility

## Get the library
`Package Manager`
 ```
NuGet\Install-Package CSPR.Cloud.Net
 ```
`.NET CLI`
 ```
dotnet add package CSPR.Cloud.Net
 ```
	

## How to Use

The `CasperCloudRestClient` class provides an easy way to interact with the CSPR Cloud API for both Mainnet and Testnet environments. Below are the steps to initialize and use the client in your application.

### Initialization

To create an instance of the `CasperCloudRestClient`, you need to provide your API key. Optionally, you can also pass a custom `HttpClient` and `ILoggerFactory` for logging purposes.



### Step-by-Step Guide

### 1. **Add Dependencies**

   Ensure you have the necessary dependencies in your project. Typically, this includes:
   - `HttpClient`
   - `ILoggerFactory` from Microsoft.Extensions.Logging

### 2. **Create a Configuration Class**

   Create an instance of the `CasperCloudClientConfig` class with your API key.

   ```
   public class CasperCloudClientConfig
   {
       public string ApiKey { get; set; }

       public CasperCloudClientConfig(string apiKey)
       {
           if (string.IsNullOrEmpty(apiKey))
               throw new ArgumentException("API key is required.", nameof(apiKey));

           ApiKey = apiKey;
       }
   }
   ```
### 3. Initialize the Client

Initialize the `CasperCloudRestClient` with your configuration, and optionally pass in a custom `HttpClient` and `ILoggerFactory`.

```
using System.Net.Http;
using Microsoft.Extensions.Logging;

// Configuration with API key
var config = new CasperCloudClientConfig("your-api-key");

// Optional: Custom HttpClient
HttpClient customHttpClient = new HttpClient();

// Optional: LoggerFactory for logging
ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

// Create the CasperCloudRestClient
var restClient = new CasperCloudRestClient(config, customHttpClient, loggerFactory);
```

### 4. Access Endpoints

Use the `Mainnet` and `Testnet` properties to access different endpoints. For example:

```
// Accessing the Account endpoint on Testnet
var accountData = await restClient.Testnet.Account.GetAccountAsync("public-key");

// Accessing the Block endpoint on Mainnet
var blockData = await restClient.Mainnet.Block.GetBlockAsync("block-hash");
```

### 5. Switching Between Networks Using INetworkEndpoint

Both `Mainnet` and `Testnet` implement the `INetworkEndpoint` interface, allowing you to easily switch between networks:

```csharp
// Choose network based on configuration
bool useTestnet = true; // or from configuration
INetworkEndpoint network = useTestnet ? restClient.Testnet : restClient.Mainnet;

// Use the selected network throughout your application
var account = await network.Account.GetAccountAsync("public-key");
var block = await network.Block.GetBlockAsync("block-hash");
```

This is useful when you want to:
- Switch networks based on environment (development/production)
- Pass the network endpoint to services without coupling them to a specific network
- Write reusable code that works with both networks

### 6. Dependency Injection Example

#### appsettings.json
 ```
  "CsprCloud": {
    "ApiKey": "your-api-key"
  }
 ```
#### Startup.cs
 ```
 context.Services.AddSingleton(resolver =>
 {
     var apiKey = configuration.GetSection("CsprCloud:ApiKey").Value;
     return new CasperCloudRestClient(new CasperCloudClientConfig(apiKey));
 });
 ```
#### Then call using di
 ```
 public class IndexModel : PageModel
{
     private readonly CasperCloudRestClient _restClient;
     public IndexModel(CasperCloudRestClient restClient)
     {
          _restClient = restClient;
     }
     public async Task OnGet()
     {
         var result = await _restClient.Mainnet.Account.GetAccountInfoAsync("bb436216f3f56b073fc712c024a01c1291292e9533a03ddabc67ef85360b00bf");
     }
     
}

 ```

## Streaming API (WebSocket)

Use `CasperCloudSocketClient` for real-time subscriptions. The shape mirrors the REST client — pick `Mainnet` or `Testnet`, pick a stream, call `SubscribeAsync` with optional filters and a message handler.

### Quick start
```csharp
using CSPR.Cloud.Net.Clients;
using CSPR.Cloud.Net.Objects.Config;

var socket = new CasperCloudSocketClient(new CasperCloudClientConfig("your-api-key"));

using var cts = new CancellationTokenSource();
await socket.Testnet.Block.SubscribeAsync(
    parameters: null,
    onMessage: msg =>
    {
        Console.WriteLine($"{msg.Action} @ {msg.Data.BlockHash}");
        return Task.CompletedTask;
    },
    cancellationToken: cts.Token);
```

### Available streams
Every stream lives under `socket.Mainnet.{Stream}` and `socket.Testnet.{Stream}`:

| Stream | Actions | Payload type |
|--------|---------|--------------|
| `.AccountBalance` | `updated` | `AccountBalanceStreamData` |
| `.Block` | `created` | `BlockData` |
| `.Contract` | `created` | `ContractData` |
| `.ContractPackage` | `created`, `updated` | `ContractPackageData` |
| `.ContractEvent` | `emitted` | `ContractEventStreamData` |
| `.Deploy` | `created` | `DeployData` |
| `.FTTokenAction` | `created` | `FTTokenActionData` |
| `.NFT` | `created`, `updated` | `NFTTokenData` |
| `.NFTAction` | `created` | `NFTTokenActionData` |
| `.Transfer` | `created` | `TransferData` |

Every message is delivered as a `WebSocketMessage<T>` envelope with `Action`, `Data`, `Extra` (per-stream `JObject`), and `Timestamp`.

### Filtering
Each stream has a parameters class exposing its server-side filters as `List<string>` (comma-joined in the URL). Leave null to skip a filter:

```csharp
await socket.Testnet.Deploy.SubscribeAsync(
    new DeployStreamParameters
    {
        CallerPublicKey = new List<string> { "01abc..." },
        ContractPackageHash = new List<string> { "pkg1..." }
    },
    onMessage: msg => { Handle(msg); return Task.CompletedTask; },
    cancellationToken: ct);
```

The `ContractEvent` stream requires exactly one of `ContractHash` or `ContractPackageHash` — the client throws `ArgumentException` on `BuildUri` if neither is supplied, so you fail fast instead of hitting a 400.

### Switching networks at runtime
`Mainnet` and `Testnet` both return `INetworkSocketEndpoint`, so the same pattern you use for REST works:

```csharp
bool useTestnet = true;
var network = useTestnet ? socket.Testnet : socket.Mainnet;
await network.Transfer.SubscribeAsync(parameters: null, onMessage, cancellationToken: ct);
```

### Auto-reconnect
WebSocket connections may drop during CSPR Cloud deployments. Enable the built-in reconnect policy to survive those transparently:

```csharp
var socket = new CasperCloudSocketClient(
    new CasperCloudClientConfig("your-api-key"),
    persistentSessionId: "my-consumer-id",          // optional; replays queued messages across reconnects (Beta, paid tiers)
    reconnectPolicy: new StreamReconnectPolicy
    {
        Enabled = true,
        InitialDelay = TimeSpan.FromSeconds(1),
        MaxDelay = TimeSpan.FromSeconds(60),
        BackoffMultiplier = 2.0,
        JitterFactor = 0.25,
        MaxRetries = -1                             // -1 = retry forever
    });

await socket.Testnet.Block.SubscribeAsync(
    parameters: null,
    onMessage: msg => { Handle(msg); return Task.CompletedTask; },
    onError: ex => { Log(ex); return Task.CompletedTask; },         // parse errors (envelope keeps flowing)
    onReconnecting: (ex, attempt) =>
    {
        Console.WriteLine($"reconnecting (attempt {attempt}): {ex?.Message}");
        return Task.CompletedTask;
    },
    cancellationToken: ct);
```

The default retry gate retries transport exceptions (`WebSocketException`, `IOException`, `TimeoutException`) and clean server disconnects; it refuses `ArgumentException` / `InvalidOperationException`. Override via `ShouldReconnect = (ex, attempt) => ...` for custom rules.

### Stopping a subscription
`SubscribeAsync` returns a `Task` that only completes when you cancel the `CancellationToken` (or, if reconnect is off, when the server closes the socket). Just cancel the CTS:

```csharp
cts.Cancel(); // the subscription task exits with OperationCanceledException
```


## Using Parameterized Requests on Endpoints

Most of the endpoints require optional request parameters wrapped in a `RequestParameters` class, which includes three possible components:
1. **FilterParameters**: Used to filter the results.
2. **SortingParameters**: Used to sort the results.
3. **OptionalParameters**: Used to include additional data in the results.


### Example Usage

Parameters and properties differ for each request depending on the endpoint.
Here's an example of how to initialize and use parameterized requests for the `Validators` endpoint:

```
// Initialize the request parameters
var parameters = new ValidatorsRequestParameters
{
    // Result is filtered for EraId = 14027 and IsActive = true
    FilterParameters = new ValidatorsFilterParameters
    {
        EraId = "14027",
        IsActive = true
    },
    // Result will include AccountInfo, CentralizedAccountInfo and AveragePerformance Entities
    OptionalParameters = new ValidatorsOptionalParameters
    {
        AccountInfo = true,
        CentralizedAccountInfo = true,
        AveragePerformance = true,
    },
    // Result is sorted by Total Stake because it is set to true and the Sort Direction is Descending
    SortingParameters = new ValidatorsSortingParameters
    {
        OrderByTotalStake = true,
        SortType = SortType.Descending
    }
};

// Execute the request
var result = await _restClient.Testnet.Validator.GetValidatorsAsync(parameters);
```



### Samples

Below are some examples of demonstrating how to use the `CasperCloudRestClient`.

For more detailed examples of all endpoints, please refer to the [CSPRCloudNetTests.cs](https://github.com/msanlisavas/CSPR.Cloud.Net/blob/master/CSPR.Cloud.Net.Tests/CSPRCloudNetTests.cs) file.

```
var config = new CasperCloudClientConfig("your-api-key");
_restClient = new CasperCloudRestClient(config);

// Without parameters
var result = await _restClient.Testnet.Account.GetAccountAsync(_testPublicKey);

// With parameters
var parameters = new AccountInfosRequestParameters
{
    FilterParameters = new AccountInfosFilterParameters
    {
        AccountHashes = new List<string>
        {
            "first-account-hash",
            "second-account-hash"
        }
    }
};
var result = await _restClient.Testnet.Account.GetAccountInfosAsync(parameters);

```

### Contributing
Contributions are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request.

### License
MIT License

**Buy me a coffee?**
---
```
BTC: 1NxUuEQcR4Scw8ge3oto6ykLqBpe9LGikS
ETH: 0x9cda155f73220073a9f024daaa72eb06b5c06c86
CSPR Public Key: 01a0cbbd2f6402c98c745b6d318d15c0b68feef6a17af48ae35e683f05a4e6cbcc
```

### Support my validator on the Casper Network by Delegating
https://cspr.live/delegate-stake?validatorPublicKey=011143c66e8e567bc46e7108736d363dfbbeeb8d0dfbd78aa9728646824999a5e5

