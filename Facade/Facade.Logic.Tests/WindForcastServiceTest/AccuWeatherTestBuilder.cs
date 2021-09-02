using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Facade.Logic.AccuWeather;
using Facade.Logic.AccuWeather.LocationApi;
using Facade.Logic.AccuWeather.WeatherForecastApi;
using Facade.Logic.WindSpeedConverterApi;
using Moq;

namespace Facade.Logic.Tests.WindForcastServiceTest
{
    public class AccuWeatherTestBuilder : ITestBuilder
    {
        private const string LocationKey = "SAMPLE_KEY";
        private List<DailyForecast> _dailyForecasts;

        private string _location;
        private Mock<ILocationService> _locationServiceMock;

        private Mock<IWeatherForecastService> _weatherForecastServiceMock;
        private Mock<IWindSpeedConverterService> _windSpeedConverterServiceMock;

        public void SetupWindspeedForNextDays(params int[] windSpeedForNextDays)
        {
            var epochDatesForForecast = EpochDateListGenerator.EpochDatesForNextDays(windSpeedForNextDays.Length);

            _dailyForecasts = epochDatesForForecast
                .Zip(windSpeedForNextDays)
                .Select(epochDateAndWindSpeed => new DailyForecast
                {
                    EpochDate = epochDateAndWindSpeed.First,
                    Day = new Day
                    {
                        Wind = new Wind { Speed = new WindSpeed { Value = epochDateAndWindSpeed.Second } }
                    }
                }).ToList();
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
            _weatherForecastServiceMock.Verify(InvokeGetWeatherForecast());
            _windSpeedConverterServiceMock.Verify(InvokeKilometersPerHourToBeaufort());
            _locationServiceMock.Verify(InvokeGetLocations());
        }

        private void SetupWeatherForecastMock()
        {
            var weatherForecast = new WeatherForecast { DailyForecasts = _dailyForecasts };
            _weatherForecastServiceMock = new Mock<IWeatherForecastService>();
            _weatherForecastServiceMock.Setup(InvokeGetWeatherForecast()).Returns(weatherForecast);
        }

        private void SetupLocationServiceMock()
        {
            var locations = new List<Location> { new() { Key = LocationKey } };
            _locationServiceMock = new Mock<ILocationService>();
            _locationServiceMock.Setup(InvokeGetLocations()).Returns(locations);
        }

        private void SetupWindSpeedConverterServiceMock()
        {
            _windSpeedConverterServiceMock = new Mock<IWindSpeedConverterService>();
            foreach (var forecast in _dailyForecasts)
            {
                _windSpeedConverterServiceMock.Setup(InvokeKilometersPerHourToBeaufort(forecast.Day.Wind.Speed.Value)).Returns((int)forecast.Day.Wind.Speed.Value);
            }
        }

        private static Expression<Func<IWeatherForecastService, WeatherForecast>> InvokeGetWeatherForecast()
        {
            return x => x.GetWeatherForecast(LocationKey, It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>());
        }

        private Expression<Func<ILocationService, IList<Location>>> InvokeGetLocations()
        {
            return x => x.GetLocations(It.IsAny<string>(), _location, It.IsAny<string>(),
                It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>());
        }

        private static Expression<Func<IWindSpeedConverterService, int>> InvokeKilometersPerHourToBeaufort()
        {
            return x => x.KilometersPerHourToBeaufort(It.IsAny<double>());
        }

        private static Expression<Func<IWindSpeedConverterService, int>> InvokeKilometersPerHourToBeaufort(double windSpeed)
        {
            return x => x.KilometersPerHourToBeaufort(windSpeed);
        }
    }
}