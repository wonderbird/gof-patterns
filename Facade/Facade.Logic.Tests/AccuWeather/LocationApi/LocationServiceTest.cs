using System;
using System.Net;
using Facade.Logic.AccuWeather.LocationApi;
using Xunit;

namespace Facade.Logic.Tests.AccuWeather.LocationApi
{
    public class LocationServiceTest
    {
        private readonly string ApiKey = Environment.GetEnvironmentVariable("ACCUWEATHER_APIKEY");

        [Fact]
        public void GetLocation__ReturnsCorrectKey()
        {
            var query = "Roermond NL";
            var expectedKey = "248715";

            var locationService = new LocationService();
            var locations = locationService.GetLocations(ApiKey, query, "de-de", false, 0, "NoOfficialMatchFound");

            Assert.Equal(expectedKey, locations[0].Key);
        }

        [Fact]
        public void GetLocation_InvalidApiKey_ThrowsWebException()
        {
            var locationService = new LocationService();
            Assert.Throws<WebException>(() => locationService.GetLocations("INVALID API KEY", "", "", false, 0, ""));
        }
    }
}