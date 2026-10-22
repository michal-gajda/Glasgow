namespace Glasgow;

public interface IWeatherForecastService : IGrainWithStringKey
{
    Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync();
}
