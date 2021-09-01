using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
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

            var response = httpClient.GetAsync(uri).Result;
            var payload = response.Content.ReadAsStringAsync().Result;

            try
            {
                var responseObj = JsonSerializer.Deserialize<IList<Location>>(payload);
                return responseObj;
            }
            catch (JsonException)
            {
                var message = string.Format(CultureInfo.InvariantCulture, StringResources.UnexpectedApiResponse,
                    payload);
                throw new WebException(message);
            }
        }
    }
}