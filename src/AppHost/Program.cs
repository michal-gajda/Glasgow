namespace Glasgow.AppHost;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public sealed class Program
{
    private Program()
    {
    }

    public static async Task<int> Main(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        var silo = builder.AddProject<Projects.Glasgow_AppServer>("silo")
            .WithHttpHealthCheck("/healthz")
            ;
        
        builder.AddProject<Projects.Glasgow_WebApi>("api")
            .WithHttpHealthCheck("/healthz")
            .WithReference(silo)
            ;

        var app = builder.Build();

        await app.RunAsync();

        return Environment.ExitCode;
    }
}
