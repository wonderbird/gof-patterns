using System;
using System.Linq;
using Facade.Lib.AccuWeather.LocationApi;
using Facade.Lib.AccuWeather.WeatherForecastApi;
using Facade.Lib.WindSpeedConverterApi;

namespace Facade.Lib.AccuWeather
{
    public class WindForecastService : IWindForecastService
    {
        public string AccuWeatherServiceApiKey { private get; set; } = Environment.GetEnvironmentVariable("ACCUWEATHER_APIKEY");

        private readonly IWeatherForecastService weatherForecastService;
        private readonly IWindSpeedConverterService windSpeedConverterService;
        private readonly ILocationService locationService;

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

        public int GetWindForecastBeaufort(string location, int daysFromToday)
        {
            if (daysFromToday < 0)
            {
                throw new ArgumentOutOfRangeException($"daysFromToday", daysFromToday, "daysFromToday must be greater or equal 0");
            }

            var locations = locationService.GetLocations(AccuWeatherServiceApiKey, location, "de-de", false, 0, "NoOfficialMatchFound");
            var locationKey = locations[0].Key;

            var weatherForecast = weatherForecastService.GetWeatherForecast(locationKey, AccuWeatherServiceApiKey, "de-de", true, true);

            var desiredDate = DateTime.Now.ToUniversalTime().Date.AddDays(daysFromToday);
            var desiredForecast = weatherForecast.DailyForecasts.FirstOrDefault(x => desiredDate == DateTimeOffset.FromUnixTimeSeconds(x.EpochDate).Date);

            if (desiredForecast == null)
            {
                var lastForecastDate = DateTimeOffset.FromUnixTimeSeconds(weatherForecast.DailyForecasts.Last().EpochDate).Date;
                var numberOfDays = (lastForecastDate - DateTime.Now.ToUniversalTime().Date).Days;
                throw new ArgumentOutOfRangeException($"daysFromToday", daysFromToday, $"Forecast only available for {numberOfDays} days");
            }

            var windSpeedKmh = desiredForecast.Day.Wind.Speed.Value;
            var windSpeedBeaufort = windSpeedConverterService.KilometersPerHourToBeaufort(windSpeedKmh);
            
            return windSpeedBeaufort;
        }
    }
}