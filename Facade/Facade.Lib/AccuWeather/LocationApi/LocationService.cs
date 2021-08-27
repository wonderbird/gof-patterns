using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace Facade.Lib.AccuWeather.LocationApi
{
    public class LocationService : ILocationService
    {
        public IList<Location> GetLocations(string apikey, string q, string language, bool details, int offset, string alias)
        {
            var httpClient = new HttpClient();
            var uri = new UriBuilder
            {
                Scheme = "http",
                Host = "dataservice.accuweather.com",
                Path = "locations/v1/search",
                Query = $"apikey={apikey}&"
                        + $"q={q}&"
                        + $"language={language}&"
                        + $"details={details}&"
                        + $"offset={offset}&"
                        + $"alias={alias}"
            }.Uri;

            var response = httpClient.GetAsync(uri).Result;
            var payload = response.Content.ReadAsStringAsync().Result;

            try
            {
                var responseObj = JsonSerializer.Deserialize<IList<Location>>(payload);
                return responseObj;
            }
            catch (JsonException)
            {
                var message = string.Format(StringResources.UnexpectedApiResponse, payload);
                throw new WebException(message);
            }
        }
    }
}