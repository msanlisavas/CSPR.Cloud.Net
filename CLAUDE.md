# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

CSPR.Cloud.Net is a .NET client library for interacting with the CSPR Cloud API, providing access to Casper blockchain data for both Mainnet and Testnet environments. The library targets .NET Standard 2.0 and 2.1 for broad compatibility.

Current release: **v2.0.0** — tracks CSPR Cloud API through v2.9.0. See README Release Notes for breaking changes (notably: balance/stake fields typed as `string` to avoid uint64 overflow).

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

1. **Clients**: Two top-level clients, both shaped as `Mainnet` / `Testnet` endpoint properties.
   - `CasperCloudRestClient` — the REST client. Entry point: `restClient.Testnet.Account.GetAccountAsync(...)`.
   - `CasperCloudSocketClient` — the WebSocket streaming client (10 channels). Entry point: `socketClient.Testnet.Block.SubscribeAsync(...)`. Supports optional auto-reconnect via `StreamReconnectPolicy` and a `Persistent-Session` header for replaying queued messages across reconnects (paid tiers).

2. **Endpoint Organization**: Each endpoint category (Account, Block, Validator, etc.) is accessed through the respective property on `Mainnet` / `Testnet`. Both return an interface (`INetworkEndpoint` for REST, `INetworkSocketEndpoint` for streaming) so runtime network selection works: `var network = useTestnet ? client.Testnet : client.Mainnet;`.

3. **Parameter System**: The library uses a three-part parameter system for API requests:
   - **FilterParameters**: Filter results based on specific criteria
   - **SortingParameters**: Control result ordering
   - **OptionalParameters**: Include additional data in responses (bool flags, plus a few function includers typed as `int?` that take a currency id — e.g. `TokenMarketData`, `Rate`)

   These are wrapped in request-specific parameter classes like `ValidatorsRequestParameters`.

4. **Response Types**: Responses follow a consistent pattern with base types:
   - `Response<T>`: Single item responses
   - `ListResponse<T>`: List responses
   - `PaginatedResponse<T>`: Paginated list responses
   - `ContractResponse<T>`: Contract-specific responses

5. **Wire-level conventions**:
   - Large integer amounts (balances, stakes, delegation amounts, reward amounts) are typed as `string` on the models — the server emits them as JSON strings to avoid uint64 overflow. Use `BigInteger.Parse` for numeric work.
   - `SupplyData.Timestamp` is a Unix-seconds `long?`, not a `DateTime?` — the Supply endpoint is the outlier here.
   - Contract-package market data (`CoingeckoData`, `FriendlymarketData`, `CsprtradeData`, `TokenMarketData`) is exposed as raw `JObject` / `List<JObject>` because the shape varies by includer arguments.

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
  - `CSPRCloudNetTests.cs` — integration tests against the live REST API using a shared test key. Long-running (minutes).
  - `CSPRCloudNetSocketTests.cs` — URL-construction and deserialization unit tests for the streaming client plus a small set of live-socket integration tests per channel.
  - `CSPRCloudNetRestUrlTests.cs` — fast URL-construction and deserialization unit tests for the REST client (sub-100ms). New in v2.0.0. Add new-endpoint / new-filter / new-includer / new-property tests here rather than in the integration file.

- `.github/workflows/`: CI (build on push/PR) + publish-on-version-bump (NuGet push + auto-tag + GitHub release draft, triggered when `CSPR.Cloud.Net.csproj` `<Version>` changes on master).

- `docs/audit-2026-04-22.md`: point-in-time audit of the SDK surface against the CSPR Cloud docs (v2.9.0 snapshot). Useful as the starting point for any future gap-closing release.

## Testing Approach

Tests use xUnit. Two flavors:
- **Integration tests** (`CSPRCloudNetTests.cs`, and the "live testnet" subset of `CSPRCloudNetSocketTests.cs`) hit the real API. Use a shared test API key, take minutes to run, and are the authoritative check that the SDK models match the live wire.
- **Unit tests** (`CSPRCloudNetRestUrlTests.cs` and most of `CSPRCloudNetSocketTests.cs`) validate URL construction and JSON deserialization against captured sample payloads. Run in milliseconds — fine to run on every save.

Test method naming convention: `MethodName_StateUnderTest_ExpectedBehavior`.

When adding new endpoints/filters/includers/properties, write the unit test first in `CSPRCloudNetRestUrlTests.cs` before touching the integration file. If the wire format changed for an existing field, the integration suite will surface it.

## API Key Configuration

The library requires an API key for authentication. Initialize the client with:
```csharp
var config = new CasperCloudClientConfig("your-api-key");
var restClient = new CasperCloudRestClient(config);
```

## Common Development Patterns

1. **Adding a new REST endpoint**:
   - Add the URL template to `Clients/Api/Endpoints.cs` `BaseUrls`
   - Add a URL-builder method in the right `Endpoints.<Category>` static class (matches an existing peer endpoint's shape — filter + sort + pagination + optional)
   - Create/update the wrapper in `Parameters/Wrapper/<Category>/`, filter in `Parameters/Filtering/<Category>/`, sort in `Parameters/Sorting/<Category>/`, optional in `Parameters/OptionalParameters/<Category>/`
   - Add the response model in `Objects/<Category>/`
   - Add an `…Async` method on the matching inner class inside `Clients/CasperCloudRestClient.cs`
   - Add a URL-construction unit test and a deserialization test to `CSPR.Cloud.Net.Tests/CSPRCloudNetRestUrlTests.cs`

2. **Adding a streaming channel**: mirror the same pattern under `Clients/Socket/`, `Objects/Socket/`, `Parameters/Socket/`. Use `CasperCloudSocketClient`'s existing channel classes (BlockStream, etc.) as templates.

3. **Error Handling**: The client maps HTTP status codes to custom exceptions:
   - 400 → `InvalidParamException`
   - 401 → `UnauthorizedException`
   - 403 → `AccessDeniedException`
   - 404 → `NotFoundException`
   - 409 → `DuplicateEntryException`
   - 500 → `InternalServerErrorException`

4. **Async Pattern**: All API methods follow the async/await pattern and end with `Async` suffix.

## Release process

1. Make your changes with unit tests in `CSPRCloudNetRestUrlTests.cs`.
2. Bump `<Version>` in `CSPR.Cloud.Net/CSPR.Cloud.Net.csproj` and update `<PackageReleaseNotes>`.
3. Commit and push to master. The `publish.yml` workflow detects the version bump, packs, pushes to NuGet (using the `NUGET_API_KEY` secret under the `Production` environment), tags `v{version}`, and creates the GitHub release draft. No local tag needed.
4. Version unchanged = workflow no-ops.