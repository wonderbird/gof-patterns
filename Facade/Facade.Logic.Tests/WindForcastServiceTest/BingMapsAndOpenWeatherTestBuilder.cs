using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Facade.Logic.BingMapsAndOpenWeather;
using Facade.Logic.BingMapsAndOpenWeather.LocationApi;
using Facade.Logic.BingMapsAndOpenWeather.WeatherForecastApi;
using Facade.Logic.WindSpeedConverterApi;
using Moq;

namespace Facade.Logic.Tests.WindForcastServiceTest
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

        public IWindForecastService CreateWindForecastService() =>
            new WindForecastService(_weatherForecastServiceMock.Object, _locationServiceMock.Object,
                _windSpeedConverterServiceMock.Object);

        public void VerifyMocks()
        {
            _weatherForecastServiceMock.Verify(InvokeGetWeatherForecast());
            _locationServiceMock.Verify(InvokeGetLocations());
            _windSpeedConverterServiceMock.Verify(InvokeMetersPerSecondToBeaufort());
        }

        private void SetupWeatherForecastMock()
        {
            var weatherForecast = new WeatherForecast { daily = _dailyForecasts };
            _weatherForecastServiceMock = new Mock<IWeatherForecastService>();
            _weatherForecastServiceMock.Setup(InvokeGetWeatherForecast()).Returns(weatherForecast);
        }

        private void SetupLocationServiceMock()
        {
            var locations = new List<Resource>
            {
                new() { point = new Point { coordinates = new List<double> { Latitude, Longitude } } }
            };
            _locationServiceMock = new Mock<ILocationService>();
            _locationServiceMock.Setup(InvokeGetLocations()).Returns(locations);
        }

        private void SetupWindSpeedConverterServiceMock()
        {
            _windSpeedConverterServiceMock = new Mock<IWindSpeedConverterService>();
            foreach (var forecast in _dailyForecasts)
            {
                _windSpeedConverterServiceMock.Setup(InvokeMetersPerSecondToBeaufort(forecast.wind_speed))
                    .Returns((int)forecast.wind_speed);
            }
        }

        private static Expression<Func<IWeatherForecastService, WeatherForecast>> InvokeGetWeatherForecast()
        {
            return x =>
                x.GetWeatherForecast(Latitude, Longitude, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
        }

        private static Expression<Func<IWindSpeedConverterService, int>> InvokeMetersPerSecondToBeaufort()
        {
            return x => x.MetersPerSecondToBeaufort(It.IsAny<double>());
        }

        private Expression<Func<ILocationService, List<Resource>>> InvokeGetLocations()
        {
            return x => x.GetLocations(_location, It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>());
        }

        private static Expression<Func<IWindSpeedConverterService, int>> InvokeMetersPerSecondToBeaufort(
            double windSpeed)
        {
            return x => x.MetersPerSecondToBeaufort(windSpeed);
        }
    }
}