namespace Glasgow;

[Alias("Glasgow.IWeatherForecastService")]
public interface IWeatherForecastService : IGrainWithStringKey
{
    [Alias("GetWeatherForecastAsync")]
    Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync();
}
