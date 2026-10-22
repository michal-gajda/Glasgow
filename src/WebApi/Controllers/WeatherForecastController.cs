namespace Glasgow.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public sealed class WeatherForecastController(IGrainFactory grainFactory) : ControllerBase
{
    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        var service = grainFactory.GetGrain<IWeatherForecastService>("weather-forecast");

        return await service.GetWeatherForecastAsync();
    }
}
