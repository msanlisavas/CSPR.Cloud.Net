# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

CSPR.Cloud.Net is a .NET client library for interacting with the CSPR Cloud API, providing access to Casper blockchain data for both Mainnet and Testnet environments. The library targets .NET Standard 2.0 and 2.1 for broad compatibility.

## Build and Test Commands

```bash
# Build the solution
dotnet build

# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --logger "console;verbosity=detailed"

# Build in Release mode
dotnet build -c Release

# Pack NuGet package
dotnet pack CSPR.Cloud.Net/CSPR.Cloud.Net.csproj -c Release
```

## Architecture Overview

### Core Components

1. **Client Structure**: The main entry point is `CasperCloudRestClient` which provides two endpoint properties:
   - `Mainnet`: Access to mainnet API endpoints
   - `Testnet`: Access to testnet API endpoints

2. **Endpoint Organization**: Each endpoint category (Account, Block, Validator, etc.) is accessed through the respective property on Mainnet/Testnet objects. Example: `restClient.Testnet.Account.GetAccountAsync()`

3. **Parameter System**: The library uses a three-part parameter system for API requests:
   - **FilterParameters**: Filter results based on specific criteria
   - **SortingParameters**: Control result ordering
   - **OptionalParameters**: Include additional data in responses

   These are wrapped in request-specific parameter classes like `ValidatorsRequestParameters`.

4. **Response Types**: Responses follow a consistent pattern with base types:
   - `Response<T>`: Single item responses
   - `ListResponse<T>`: List responses
   - `PaginatedResponse<T>`: Paginated list responses
   - `ContractResponse<T>`: Contract-specific responses

### Directory Structure

- `CSPR.Cloud.Net/`: Main library project
  - `Clients/`: REST and Socket client implementations
  - `Clients/Api/`: Endpoint definitions and routing
  - `Objects/`: Data models for API responses
  - `Parameters/`: Request parameter definitions
    - `Filtering/`: Filter parameter classes
    - `Sorting/`: Sorting parameter classes
    - `OptionalParameters/`: Optional inclusion parameters
    - `Wrapper/`: Combined parameter wrapper classes
  - `Enums/`: Enumeration types
  - `Errors/`: Custom exception types
  - `Extensions/`: Extension methods
  - `Helpers/`: Utility functions

- `CSPR.Cloud.Net.Tests/`: xUnit test project

## Testing Approach

Tests use xUnit and follow the pattern:
- Test method naming: `MethodName_StateUnderTest_ExpectedBehavior`
- All test methods in `CSPRCloudNetTests.cs` use a shared test API key
- Tests cover both parameterized and non-parameterized endpoint calls
- Tests validate response data structure and optional parameter inclusion

## API Key Configuration

The library requires an API key for authentication. Initialize the client with:
```csharp
var config = new CasperCloudClientConfig("your-api-key");
var restClient = new CasperCloudRestClient(config);
```

## Common Development Patterns

1. **Adding New Endpoints**:
   - Define the endpoint URL in `Endpoints.cs`
   - Create data models in appropriate `Objects/` subdirectory
   - Add parameter classes if needed
   - Implement the method in the appropriate endpoint class

2. **Error Handling**: The client maps HTTP status codes to custom exceptions:
   - 400 → `InvalidParamException`
   - 401 → `UnauthorizedException`
   - 403 → `AccessDeniedException`
   - 404 → `NotFoundException`
   - 409 → `DuplicateEntryException`
   - 500 → `InternalServerErrorException`

3. **Async Pattern**: All API methods follow the async/await pattern and end with `Async` suffix.