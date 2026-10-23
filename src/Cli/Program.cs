namespace Glasgow.Cli;

using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

internal static class Program
{
    private const string DefaultBaseUrl = "http://localhost:5080";

    public static async Task<int> Main(string[] args)
    {
        if (args.Length > 0 &&
            !(args.Length == 1 && args[0].StartsWith("--base-url=", StringComparison.OrdinalIgnoreCase)) &&
            !(args.Length == 2 && args[0].Equals("--base-url", StringComparison.OrdinalIgnoreCase)))
        {
            Console.Error.WriteLine("Usage: Glasgow.Cli [--base-url <url>]");
            return 2;
        }

        IConfigurationRoot configuration;
        try
        {
            configuration = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();
        }
        catch (FormatException exception)
        {
            Console.Error.WriteLine(exception.Message);
            Console.Error.WriteLine("Usage: Glasgow.Cli [--base-url <url>]");
            return 2;
        }

        return await RunAsync(configuration["base-url"]);
    }

    private static async Task<int> RunAsync(string? configuredBaseUrl)
    {
        var baseUrl = (configuredBaseUrl ?? DefaultBaseUrl).TrimEnd('/');
        if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out var baseUri) ||
            (baseUri.Scheme != Uri.UriSchemeHttp && baseUri.Scheme != Uri.UriSchemeHttps))
        {
            Console.Error.WriteLine("The --base-url value must be an absolute HTTP or HTTPS URL.");
            Console.Error.WriteLine("Usage: Glasgow.Cli [--base-url <url>]");
            return 2;
        }

        var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = baseUri.ToString().TrimEnd('/')
        };
        var client = new ApiClient(adapter);
        var forecasts = await client.WeatherForecast.GetAsync();

        if (forecasts is null || forecasts.Count == 0)
        {
            Console.WriteLine("No weather forecasts were returned.");
            return 0;
        }

        foreach (var forecast in forecasts)
        {
            Console.WriteLine($"{forecast.Date}: {forecast.TemperatureC} °C, {forecast.Summary}");
        }

        return Environment.ExitCode;
    }
}
