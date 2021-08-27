using System.Collections.Generic;

namespace kata_gof_pattern_facade_windforecast.BingMapsAndOpenWeather.LocationApi
{
    public class ResourceSet
    {
        public List<Resource> resources { get; set; } = new List<Resource>();
    }
}