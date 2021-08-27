using System.Collections.Generic;
using System.Linq;
using kata_gof_pattern_facade_windforecast;
using kata_gof_pattern_facade_windforecast.BingMapsAndOpenWeather;
using kata_gof_pattern_facade_windforecast.BingMapsAndOpenWeather.LocationApi;
using kata_gof_pattern_facade_windforecast.BingMapsAndOpenWeather.WeatherForecastApi;
using kata_gof_pattern_facade_windforecast.WindSpeedConverterApi;
using Moq;

namespace Facade.Lib.Tests.WindForcastServiceTest
{
    public class BingMapsAndOpenWeatherTestBuilder : ITestBuilder
    {
        private List<WeatherForecastForMoment> dailyForecasts = new List<WeatherForecastForMoment>();
        
        private Mock<IWeatherForecastService> weatherForecastServiceMock;
        private Mock<ILocationService> locationServiceMock;
        private Mock<IWindSpeedConverterService> windSpeedConverterServiceMock;

        private string location;
        private const double latitude = 50.0;
        private const double longitude = 7.0;

        public void SetupWindspeedForNextDays(params int[] windSpeedForNextDays)
        {
            var epochDatesForForecast = EpochDateListGenerator.EpochDatesForNextDays(windSpeedForNextDays.Length);

            dailyForecasts = epochDatesForForecast
                .Zip(windSpeedForNextDays)
                .Select(epochDateAndWindSpeed => new WeatherForecastForMoment
                {
                    dt = epochDateAndWindSpeed.First,
                    wind_speed = epochDateAndWindSpeed.Second
                })
                .ToList();
        }

        public void SetupMocks(string location)
        {
            this.location = location;

            SetupWeatherForecastMock();
            SetupLocationServiceMock();
            SetupWindSpeedConverterServiceMock();
        }

        private void SetupWeatherForecastMock()
        {
            var weatherForecast = new WeatherForecast
            {
                daily = dailyForecasts
            };
            weatherForecastServiceMock = new Mock<IWeatherForecastService>();
            weatherForecastServiceMock.Setup(x =>
                    x.GetWeatherForecast(latitude, longitude, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(weatherForecast);
        }

        private void SetupLocationServiceMock()
        {
            var locations = new List<Resource>
            {
                new Resource
                {
                    point = new Point
                    {
                        coordinates = new List<double> {latitude, longitude}
                    }
                }
            };
            locationServiceMock = new Mock<ILocationService>();
            locationServiceMock.Setup(x =>
                    x.GetLocations(this.location, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(locations);
        }

        private void SetupWindSpeedConverterServiceMock()
        {
            windSpeedConverterServiceMock = new Mock<IWindSpeedConverterService>();
            foreach (var forecast in dailyForecasts)
            {
                windSpeedConverterServiceMock.Setup(x => x.MetersPerSecondToBeaufort(forecast.wind_speed))
                    .Returns((int) forecast.wind_speed);
            }
        }

        public IWindForecastService CreateWindForecastService()
        {
            return new WindForecastService(weatherForecastServiceMock.Object, locationServiceMock.Object, windSpeedConverterServiceMock.Object);
        }

        public void VerifyMocks()
        {
            weatherForecastServiceMock.Verify(x => x.GetWeatherForecast(latitude, longitude, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            locationServiceMock.Verify(x => x.GetLocations(location, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()));
            windSpeedConverterServiceMock.Verify(x => x.MetersPerSecondToBeaufort(It.IsAny<double>()));
        }
    }
}