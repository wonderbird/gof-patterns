using System.Collections.Generic;
using System.Linq;
using Facade.Lib.BingMapsAndOpenWeather;
using Facade.Lib.BingMapsAndOpenWeather.LocationApi;
using Facade.Lib.BingMapsAndOpenWeather.WeatherForecastApi;
using Facade.Lib.WindSpeedConverterApi;
using Moq;

namespace Facade.Lib.Tests.WindForcastServiceTest
{
    public class BingMapsAndOpenWeatherTestBuilder : ITestBuilder
    {
        private const double Latitude = 50.0;
        private const double Longitude = 7.0;
        private List<WeatherForecastForMoment> _dailyForecasts = new();

        private string _location;
        private Mock<ILocationService> _locationServiceMock;

        private Mock<IWeatherForecastService> _weatherForecastServiceMock;
        private Mock<IWindSpeedConverterService> _windSpeedConverterServiceMock;

        public void SetupWindspeedForNextDays(params int[] windSpeedForNextDays)
        {
            var epochDatesForForecast = EpochDateListGenerator.EpochDatesForNextDays(windSpeedForNextDays.Length);

            _dailyForecasts = epochDatesForForecast
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
            _location = location;

            SetupWeatherForecastMock();
            SetupLocationServiceMock();
            SetupWindSpeedConverterServiceMock();
        }

        public IWindForecastService CreateWindForecastService()
        {
            return new WindForecastService(_weatherForecastServiceMock.Object, _locationServiceMock.Object,
                _windSpeedConverterServiceMock.Object);
        }

        public void VerifyMocks()
        {
            _weatherForecastServiceMock.Verify(x =>
                x.GetWeatherForecast(Latitude, Longitude, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            _locationServiceMock.Verify(x => x.GetLocations(_location, It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>()));
            _windSpeedConverterServiceMock.Verify(x => x.MetersPerSecondToBeaufort(It.IsAny<double>()));
        }

        private void SetupWeatherForecastMock()
        {
            var weatherForecast = new WeatherForecast
            {
                daily = _dailyForecasts
            };
            _weatherForecastServiceMock = new Mock<IWeatherForecastService>();
            _weatherForecastServiceMock.Setup(x =>
                    x.GetWeatherForecast(Latitude, Longitude, It.IsAny<string>(), It.IsAny<string>(),
                        It.IsAny<string>()))
                .Returns(weatherForecast);
        }

        private void SetupLocationServiceMock()
        {
            var locations = new List<Resource>
            {
                new()
                {
                    point = new Point
                    {
                        coordinates = new List<double> { Latitude, Longitude }
                    }
                }
            };
            _locationServiceMock = new Mock<ILocationService>();
            _locationServiceMock.Setup(x =>
                    x.GetLocations(_location, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(),
                        It.IsAny<string>()))
                .Returns(locations);
        }

        private void SetupWindSpeedConverterServiceMock()
        {
            _windSpeedConverterServiceMock = new Mock<IWindSpeedConverterService>();
            foreach (var forecast in _dailyForecasts)
                _windSpeedConverterServiceMock.Setup(x => x.MetersPerSecondToBeaufort(forecast.wind_speed))
                    .Returns((int)forecast.wind_speed);
        }
    }
}
