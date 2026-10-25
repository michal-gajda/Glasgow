namespace Glasgow.Services.UnitTests;

using Microsoft.Extensions.Time.Testing;

[TestClass]
public sealed class WeatherForecastServiceTests
{
    [TestMethod]
    public async Task GetWeatherForecastAsync_ReturnsFiveForecastsForTheNextFiveDays()
    {
        var utcNow = new DateTimeOffset(2026, 10, 25, 23, 30, 0, TimeSpan.Zero);
        var timeProvider = new FakeTimeProvider(utcNow);
        var service = new WeatherForecastService(timeProvider);

        var forecasts = (await service.GetWeatherForecastAsync()).ToArray();

        forecasts.Length.ShouldBe(5);
        forecasts.Select(forecast => forecast.Date).ShouldBe(
        [
            new DateOnly(2026, 10, 26),
            new DateOnly(2026, 10, 27),
            new DateOnly(2026, 10, 28),
            new DateOnly(2026, 10, 29),
            new DateOnly(2026, 10, 30),
        ]);
    }

    [TestMethod]
    public async Task GetWeatherForecastAsync_ReturnsValidTemperaturesAndSummaries()
    {
        var utcNow = new DateTimeOffset(2026, 10, 25, 0, 0, 0, TimeSpan.Zero);
        var timeProvider = new FakeTimeProvider(utcNow);
        var service = new WeatherForecastService(timeProvider);

        var forecasts = await service.GetWeatherForecastAsync();

        foreach (var forecast in forecasts)
        {
            forecast.TemperatureC.ShouldBeInRange(-20, 54);
            forecast.Summary.ShouldNotBeNullOrWhiteSpace();
        }
    }
}
