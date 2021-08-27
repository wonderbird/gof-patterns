using System.Collections.Generic;

namespace kata_gof_pattern_facade_windforecast.AccuWeather.WeatherForecastApi
{
    public class WeatherForecast
    {
        public List<DailyForecast> DailyForecasts { get; set; } = new List<DailyForecast>();
    }
}