using System.Collections.Generic;
using System.Linq;
using Facade.Lib.AccuWeather;
using Facade.Lib.AccuWeather.LocationApi;
using Facade.Lib.AccuWeather.WeatherForecastApi;
using Facade.Lib.WindSpeedConverterApi;
using Moq;

namespace Facade.Lib.Tests.WindForcastServiceTest
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
                        Wind = new Wind
                        {
                            Speed = new WindSpeed
                            {
                                Value = epochDateAndWindSpeed.Second
                            }
                        }
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
            _weatherForecastServiceMock.Verify(x => x.GetWeatherForecast(LocationKey, It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()));
            _windSpeedConverterServiceMock.Verify(x => x.KilometersPerHourToBeaufort(It.IsAny<double>()));
            _locationServiceMock.Verify(x => x.GetLocations(It.IsAny<string>(), _location, It.IsAny<string>(),
                It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()));
        }

        private void SetupWeatherForecastMock()
        {
            var weatherForecast = new WeatherForecast
            {
                DailyForecasts = _dailyForecasts
            };
            _weatherForecastServiceMock = new Mock<IWeatherForecastService>();
            _weatherForecastServiceMock.Setup(x =>
                    x.GetWeatherForecast(LocationKey, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .Returns(weatherForecast);
        }

        private void SetupLocationServiceMock()
        {
            var locations = new List<Location>
            {
                new()
                {
                    Key = LocationKey
                }
            };
            _locationServiceMock = new Mock<ILocationService>();
            _locationServiceMock.Setup(x => x.GetLocations(It.IsAny<string>(), _location, It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(locations);
        }

        private void SetupWindSpeedConverterServiceMock()
        {
            _windSpeedConverterServiceMock = new Mock<IWindSpeedConverterService>();
            foreach (var forecast in _dailyForecasts)
                _windSpeedConverterServiceMock.Setup(x => x.KilometersPerHourToBeaufort(forecast.Day.Wind.Speed.Value))
                    .Returns((int)forecast.Day.Wind.Speed.Value);
        }
    }
}
