using System.Collections.Generic;

namespace Facade.Lib.AccuWeather.WeatherForecastApi
{
    public class WeatherForecast
    {
        public List<DailyForecast> DailyForecasts { get; set; } = new List<DailyForecast>();
    }
}