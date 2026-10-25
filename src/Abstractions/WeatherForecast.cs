namespace Glasgow;

[GenerateSerializer]
[Alias("Glasgow.WeatherForecast")]
public sealed record class WeatherForecast
{
    [Id(0)] public required DateOnly Date { get; init; }
    [Id(1)] public required int TemperatureC { get; init; }
    [Id(2)] public string? Summary { get; init; } = null;
}
