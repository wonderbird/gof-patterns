using System.Collections.Generic;

namespace Facade.Logic.AccuWeather.WeatherForecastApi
{
    public class WeatherForecast
    {
        public List<DailyForecast> DailyForecasts { get; set; } = new();
    }
}