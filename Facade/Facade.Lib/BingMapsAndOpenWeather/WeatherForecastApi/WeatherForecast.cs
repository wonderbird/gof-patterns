using System.Collections.Generic;

namespace kata_gof_pattern_facade_windforecast.BingMapsAndOpenWeather.WeatherForecastApi
{
    public class WeatherForecast
    {
        public List<WeatherForecastForMoment> daily { get; set; } = new List<WeatherForecastForMoment>();
    }
}