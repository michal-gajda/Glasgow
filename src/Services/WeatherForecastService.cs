namespace Glasgow.Services;

public sealed class WeatherForecastService(TimeProvider timeProvider) : Grain, IWeatherForecastService
{
    private static readonly string[] Summaries =
    [
        "Balmy",
        "Bracing",
        "Chilly",
        "Cool",
        "Freezing",
        "Hot",
        "Mild",
        "Scorching",
        "Sweltering",
        "Warm",
    ];

    public Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync()
    {
        var dateTime = timeProvider.GetUtcNow().Date;
        var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(dateTime.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();

        return Task.FromResult(result.AsEnumerable());
    }
}
