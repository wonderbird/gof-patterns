using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace Facade.Logic.AccuWeather.LocationApi
{
    public class LocationService : ILocationService
    {
        public IList<Location> GetLocations(string apikey, string q, string language, bool details, int offset,
            string useAlias)
        {
            var httpClient = new HttpClient();
            var uri = BuildGetLocationsUri(apikey, q, language, details, offset, useAlias);

            var response = httpClient.GetAsync(uri).Result;
            var payload = response.Content.ReadAsStringAsync().Result;

            try
            {
                var responseObj = JsonSerializer.Deserialize<IList<Location>>(payload);
                return responseObj;
            }
            catch (JsonException)
            {
                throw new UnexpectedApiResponseException(payload);
            }
        }

        private static Uri BuildGetLocationsUri(string apikey, string q, string language, bool details, int offset,
            string useAlias)
        {
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
                        + $"alias={useAlias}"
            }.Uri;
            return uri;
        }
    }
}