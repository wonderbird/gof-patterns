using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace Facade.Logic.BingMapsAndOpenWeather.LocationApi
{
    public class LocationService : ILocationService
    {
        public List<Resource> GetLocations(string query, string includeNeighbourhood, string include,
            int maxResults,
            string key)
        {
            var httpClient = new HttpClient();
            var uri = BuildGetLocationsUri(query, includeNeighbourhood, include, maxResults, key);

            var response = httpClient.GetAsync(uri).Result;
            var payload = response.Content.ReadAsStringAsync().Result;
            var responseObj = JsonSerializer.Deserialize<LocationApiResponse>(payload);

            if (responseObj == null || responseObj.resourceSets.Count == 0)
            {
                throw new UnexpectedApiResponseException(payload);
            }

            return responseObj.resourceSets[0].resources;
        }

        private static Uri BuildGetLocationsUri(string query, string includeNeighbourhood, string include, int maxResults,
            string key)
        {
            var uri = new UriBuilder
            {
                Scheme = "http",
                Host = "dev.virtualearth.net",
                Path = "REST/v1/Locations",
                Query = $"query={query}&"
                        + $"includeNeighborhood={includeNeighbourhood}&"
                        + $"include={include}&"
                        + $"maxResults={maxResults}&"
                        + $"key={key}"
            }.Uri;
            return uri;
        }
    }
}