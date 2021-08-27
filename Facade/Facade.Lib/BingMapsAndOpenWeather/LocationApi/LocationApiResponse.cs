using System.Collections.Generic;

namespace kata_gof_pattern_facade_windforecast.BingMapsAndOpenWeather.LocationApi
{
    public class LocationApiResponse
    {
        public List<ResourceSet> resourceSets { get; set; }
    }
}