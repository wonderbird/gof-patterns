using System.Collections.Generic;

namespace Facade.Lib.BingMapsAndOpenWeather.WeatherForecastApi
{
    public class WeatherForecast
    {
        public List<WeatherForecastForMoment> daily { get; set; } = new();
    }
}