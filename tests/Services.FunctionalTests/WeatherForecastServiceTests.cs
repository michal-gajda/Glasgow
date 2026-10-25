namespace Glasgow.Services.FunctionalTests;

[TestClass]
public sealed class WeatherForecastServiceTests : TestBase
{
    [TestMethod]
    public async Task Returns_forecast()
    {
        var service = GrainFactory.GetGrain<IWeatherForecastService>("weather-forecast");

        var result = await service.GetWeatherForecastAsync();

        result.ShouldNotBeEmpty();
    }
}
