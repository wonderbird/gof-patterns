using System;
using System.Linq;
using Facade.Logic.AccuWeather.LocationApi;
using Facade.Logic.AccuWeather.WeatherForecastApi;
using Facade.Logic.WindSpeedConverterApi;

namespace Facade.Logic.AccuWeather
{
    public class WindForecastService : IWindForecastService
    {
        private readonly ILocationService locationService;

        private readonly IWeatherForecastService weatherForecastService;
        private readonly IWindSpeedConverterService windSpeedConverterService;

        public WindForecastService()
            : this(new WeatherForecastService(), new LocationService(), new WindSpeedConverterService())
        {
        }

        public WindForecastService(IWeatherForecastService weatherForecastService, ILocationService locationService,
            IWindSpeedConverterService windSpeedConverterService)
        {
            this.weatherForecastService = weatherForecastService;
            this.windSpeedConverterService = windSpeedConverterService;
            this.locationService = locationService;
        }

        public string AccuWeatherServiceApiKey { private get; set; } =
            Environment.GetEnvironmentVariable("ACCUWEATHER_APIKEY");

        public int GetWindForecastBeaufort(string location, int daysFromToday)
        {
            if (daysFromToday < 0)
                throw new ForecastNotAvailableException(nameof(daysFromToday), daysFromToday,
                    "daysFromToday must be greater or equal 0");

            var locations = locationService.GetLocations(AccuWeatherServiceApiKey, location, "de-de", false, 0,
                "NoOfficialMatchFound");
            var locationKey = locations[0].Key;

            var weatherForecast =
                weatherForecastService.GetWeatherForecast(locationKey, AccuWeatherServiceApiKey, "de-de", true, true);

            var desiredDate = DateTime.Now.ToUniversalTime().Date.AddDays(daysFromToday);
            var desiredForecast = weatherForecast.DailyForecasts.FirstOrDefault(x =>
                desiredDate == DateTimeOffset.FromUnixTimeSeconds(x.EpochDate).Date);

            if (desiredForecast == null)
                throw new ForecastNotAvailableException(nameof(daysFromToday), daysFromToday,
                    weatherForecast.DailyForecasts.Last().EpochDate);

            var windSpeedKmh = desiredForecast.Day.Wind.Speed.Value;
            var windSpeedBeaufort = windSpeedConverterService.KilometersPerHourToBeaufort(windSpeedKmh);

            return windSpeedBeaufort;
        }
    }
}