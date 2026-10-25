# Glasgow

## BOM

- Docker
- .NET 10
- .NET Aspire

## WebUI (Blazor WASM)

```powershell
dotnet new blazorwasm --framework net10.0 --no-https --use-program-main --output src/WebUI --name Glasgow.WebUI
```

## Abstractions (Orleans Grains)

```powershell
dotnet new classlib --framework net10.0 --output src/Abstractions --name Glasgow.Abstractions
dotnet add src/Abstractions package Microsoft.Orleans.Sdk
```

## WebApi

```powershell
dotnet new webapi --framework net10.0 --no-https --use-controllers --use-program-main --output src/WebApi --name Glasgow.WebApi
dotnet add src/WebApi reference src/Abstractions
dotnet add src/WebApi reference src/WebUI
dotnet add src/WebApi package Microsoft.AspNetCore.Components.WebAssembly.Server
dotnet add src/WebApi package Microsoft.Orleans.Client
dotnet add src/WebApi package Swashbuckle.AspNetCore.SwaggerUI
```

```csharp
public sealed class Program
{

}
```

## Services

```powershell
dotnet new classlib --framework net10.0 --output src/Services --name Glasgow.Services
dotnet add src/Services reference src/Abstractions
```

## AppServer (Orleans Silo)

```powershell
dotnet new web --framework net10.0 --no-https --use-program-main --output src/AppServer --name Glasgow.AppServer
dotnet add src/AppServer reference src/Services
dotnet add src/AppServer package Microsoft.Orleans.Dashboard
dotnet add src/AppServer package Microsoft.Orleans.Server
```

## AppHost (.NET Aspire)

```powershell
dotnet new aspire-apphost --framework net10.0 --no-https --output src/AppHost --name Glasgow.AppHost
dotnet add src/AppHost reference src/AppServer
dotnet add src/AppHost reference src/WebApi
```

## Sln

```powershell
dotnet new sln --name Glasgow
dotnet sln add src/AppHost
```

## CLI

```powershell
dotnet new console --framework net10.0 --use-program-main --output src/Cli --name Glasgow.Cli
dotnet add src/Cli package Microsoft.Extensions.Configuration.CommandLine
```

## OpenApi

```powershell
dotnet tool install --global Microsoft.OpenApi.Kiota
kiota generate --language CSharp --openapi http://localhost:5080/openapi/v1.json --output src/Cli --namespace-name Glasgow.Cli
```

Run the CLI while the WebApi is available at `http://localhost:5080`:

```powershell
dotnet run --project src/Cli
```

To use a different WebApi URL, pass the `--base-url` option:

```powershell
dotnet run --project src/Cli -- --base-url http://localhost:5080
```

## Tests

### Unit Tests

```powershell
dotnet new mstest --framework net10.0 --output tests/Services.UnitTests --name Glasgow.Services.UnitTests
dotnet add tests/Services.UnitTests reference src/Services
dotnet add tests/Services.UnitTests package Shouldly
dotnet sln add tests/Services.UnitTests
```

### Functional Tests

```powershell
dotnet new mstest --framework net10.0 --output tests/Services.FunctionalTests --name Glasgow.Services.FunctionalTests
dotnet add tests/Services.FunctionalTests reference src/Services
dotnet add tests/Services.FunctionalTests package Microsoft.Orleans.TestingHost
dotnet add tests/Services.FunctionalTests package Shouldly
dotnet sln add tests/Services.FunctionalTests
```

- Microsoft.Orleans.TestingHost
- Aspire.Hosting.Testing