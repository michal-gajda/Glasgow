namespace Glasgow.AppServer;

using Orleans.Dashboard;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public sealed class Program
{
    private Program()
    {
    }

    public static async Task<int> Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseOrleans(siloBuilder =>
        {
            siloBuilder.AddActivityPropagation();
            siloBuilder.AddDashboard();
            siloBuilder.AddMemoryGrainStorageAsDefault();
            siloBuilder.AddMemoryGrainStorage("PubSubStore");
            siloBuilder.AddMemoryStreams("Events");
            siloBuilder.UseInMemoryReminderService();
            siloBuilder.UseLocalhostClustering();
        });

        builder.Services.AddHealthChecks();
        builder.Services.AddSingleton(TimeProvider.System);

        var app = builder.Build();

        app.UseHealthChecks("/healthz");

        app.MapOrleansDashboard();

        await app.RunAsync();

        return Environment.ExitCode;
    }
}
