using System.Collections.Generic;

namespace Facade.Logic.BingMapsAndOpenWeather.WeatherForecastApi
{
    public class WeatherForecast
    {
        public List<WeatherForecastForMoment> daily { get; set; } = new();
    }
}