namespace Glasgow.AppHost;

public sealed class Program
{
    private Program()
    {
    }

    public static async Task<int> Main(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        var silo = builder.AddProject<Projects.Glasgow_AppServer>("silo")
            .WithHealthCheck("/healthz")
            ;
        
        builder.AddProject<Projects.Glasgow_WebApi>("api")
            .WithReference(silo)
            ;

        var app = builder.Build();

        await app.RunAsync();

        return Environment.ExitCode;
    }
}
