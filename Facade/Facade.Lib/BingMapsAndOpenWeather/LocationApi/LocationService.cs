using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace Facade.Lib.BingMapsAndOpenWeather.LocationApi
{
    public class LocationService : ILocationService
    {
        public List<Resource> GetLocations(string query, string includeNeighbourhood, string include,
            int maxResults,
            string key)
        {
            var httpClient = new HttpClient();
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

            var response = httpClient.GetAsync(uri).Result;
            var payload = response.Content.ReadAsStringAsync().Result;
            var responseObj = JsonSerializer.Deserialize<LocationApiResponse>(payload);

            if (responseObj.resourceSets.Count == 0)
            {
                var message = string.Format(StringResources.UnexpectedApiResponse, payload);
                throw new WebException(message);
            }

            return responseObj.resourceSets[0].resources;
        }
    }
}