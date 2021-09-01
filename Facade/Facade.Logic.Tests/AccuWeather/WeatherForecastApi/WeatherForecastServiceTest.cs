using System;
using System.Net;
using Facade.Logic.AccuWeather.WeatherForecastApi;
using Xunit;

namespace Facade.Logic.Tests.AccuWeather.WeatherForecastApi
{
    public class WeatherForecastServiceTest
    {
        private readonly string ApiKey = Environment.GetEnvironmentVariable("ACCUWEATHER_APIKEY");

        [Fact]
        public void GetWeatherForecast__ReturnsWindForecast()
        {
            // https://www.latlong.net/convert-address-to-lat-long.html
            // Roermond NL
            var locationKey = "248715";
            var weatherForecastService = new WeatherForecastService();
            var forecast = weatherForecastService.GetWeatherForecast(locationKey, ApiKey, "de-de", true, true);

            Assert.True(!double.IsNaN(forecast.DailyForecasts[0].Day.Wind.Speed.Value));
            Assert.Equal("km/h", forecast.DailyForecasts[0].Day.Wind.Speed.Unit);
        }

        [Fact]
        public void GetWeatherForecast_InvalidApiKey_ThrowsWebException()
        {
            var weatherForecastService = new WeatherForecastService();
            Assert.Throws<WebException>(() =>
                weatherForecastService.GetWeatherForecast("", "INVALID API KEY", "", false, false));
        }
    }
}