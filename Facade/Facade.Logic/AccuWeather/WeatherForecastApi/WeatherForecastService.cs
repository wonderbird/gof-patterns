using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace Facade.Logic.AccuWeather.WeatherForecastApi
{
    public class WeatherForecastService : IWeatherForecastService
    {
        public WeatherForecast GetWeatherForecast(string locationKey, string apikey, string language, bool details,
            bool metric)
        {
            var httpClient = new HttpClient();
            var uri = new UriBuilder
            {
                Scheme = "http",
                Host = "dataservice.accuweather.com",
                Path = $"forecasts/v1/daily/5day/{locationKey}",
                Query = $"apikey={apikey}&"
                        + $"language={language}&"
                        + $"details={details}&"
                        + $"metric={metric}"
            }.Uri;

            var response = httpClient.GetAsync(uri).Result;
            var payload = response.Content.ReadAsStringAsync().Result;
            var forecast = JsonSerializer.Deserialize<WeatherForecast>(payload);

            if (forecast.DailyForecasts.Count == 0)
            {
                throw new UnexpectedApiResponseException(payload);
            }

            return forecast;
        }
    }
}