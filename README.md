# CSPR.Cloud.Net

## Release Notes
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

## To do list
- Implementation of Streaming API

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
### 5. Dependency Injection Example

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

