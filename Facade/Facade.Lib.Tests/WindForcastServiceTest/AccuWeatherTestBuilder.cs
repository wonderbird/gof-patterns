using System.Collections.Generic;
using System.Linq;
using kata_gof_pattern_facade_windforecast;
using kata_gof_pattern_facade_windforecast.AccuWeather;
using kata_gof_pattern_facade_windforecast.AccuWeather.LocationApi;
using kata_gof_pattern_facade_windforecast.AccuWeather.WeatherForecastApi;
using kata_gof_pattern_facade_windforecast.WindSpeedConverterApi;
using Moq;

namespace Facade.Lib.Tests.WindForcastServiceTest
{
    public class AccuWeatherTestBuilder : ITestBuilder
    {
        private List<DailyForecast> dailyForecasts;

        private Mock<IWeatherForecastService> weatherForecastServiceMock;
        private Mock<ILocationService> locationServiceMock;
        private Mock<IWindSpeedConverterService> windSpeedConverterServiceMock;

        private string location;
        private const string LocationKey = "SAMPLE_KEY";

        public void SetupWindspeedForNextDays(params int[] windSpeedForNextDays)
        {
            var epochDatesForForecast = EpochDateListGenerator.EpochDatesForNextDays(windSpeedForNextDays.Length);

            dailyForecasts = epochDatesForForecast
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
            this.location = location;

            SetupWeatherForecastMock();
            SetupLocationServiceMock();
            SetupWindSpeedConverterServiceMock();
        }

        private void SetupWeatherForecastMock()
        {
            var weatherForecast = new WeatherForecast
            {
                DailyForecasts = dailyForecasts
            };
            weatherForecastServiceMock = new Mock<IWeatherForecastService>();
            weatherForecastServiceMock.Setup(x =>
                    x.GetWeatherForecast(LocationKey, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .Returns(weatherForecast);
        }

        private void SetupLocationServiceMock()
        {
            var locations = new List<Location>
            {
                new Location
                {
                    Key = LocationKey
                }
            };
            locationServiceMock = new Mock<ILocationService>();
            locationServiceMock.Setup(x => x.GetLocations(It.IsAny<string>(), this.location, It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(locations);
        }

        private void SetupWindSpeedConverterServiceMock()
        {
            windSpeedConverterServiceMock = new Mock<IWindSpeedConverterService>();
            foreach (var forecast in dailyForecasts)
            {
                windSpeedConverterServiceMock.Setup(x => x.KilometersPerHourToBeaufort(forecast.Day.Wind.Speed.Value))
                    .Returns((int) forecast.Day.Wind.Speed.Value);
            }
        }

        public IWindForecastService CreateWindForecastService()
        {
            return new WindForecastService(weatherForecastServiceMock.Object, locationServiceMock.Object, windSpeedConverterServiceMock.Object);
        }

        public void VerifyMocks()
        {
            weatherForecastServiceMock.Verify(x => x.GetWeatherForecast(LocationKey, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()));
            windSpeedConverterServiceMock.Verify(x => x.KilometersPerHourToBeaufort(It.IsAny<double>()));
            locationServiceMock.Verify(x => x.GetLocations(It.IsAny<string>(), location, It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()));
        }
    }
}