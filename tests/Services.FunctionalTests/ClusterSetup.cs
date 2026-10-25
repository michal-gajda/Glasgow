namespace Glasgow.Services.FunctionalTests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans.TestingHost;

[TestClass]
public static class ClusterSetup
{
    public static InProcessTestCluster Cluster { get; private set; } = null!;

    [AssemblyInitialize]
    public static async Task InitializeAsync(TestContext _)
    {
        var builder = new InProcessTestClusterBuilder();
        builder.ConfigureSilo((_, silo) => silo.AddMemoryGrainStorage("Default"));
        Cluster = builder.Build();
        await Cluster.DeployAsync();
    }

    [AssemblyCleanup]
    public static async Task CleanupAsync() => await Cluster.DisposeAsync();
}
