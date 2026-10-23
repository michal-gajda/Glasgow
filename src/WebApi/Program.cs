namespace Glasgow.WebApi;

public sealed class Program
{
    private Program()
    {
    }

    public static async Task<int> Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseOrleansClient(client =>
        {
            client.AddActivityPropagation();
            client.UseLocalhostClustering();
        });

        builder.Services.AddControllers();
        builder.Services.AddHealthChecks();
        builder.Services.AddOpenApi();

        var app = builder.Build();
    
        app.UseHealthChecks("/healthz");

        app.MapOpenApi();
        app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.yaml", "Glasgow API v1"));

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseAuthorization();

        app.MapControllers();

        app.MapFallbackToFile("index.html");

        await app.RunAsync();

        return Environment.ExitCode;
    }
}
