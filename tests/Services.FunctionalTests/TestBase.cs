namespace Glasgow.Services.FunctionalTests;

public abstract class TestBase
{
    protected IClusterClient Client => ClusterSetup.Cluster.Client;
    protected IGrainFactory GrainFactory => ClusterSetup.Cluster.Client;
}
