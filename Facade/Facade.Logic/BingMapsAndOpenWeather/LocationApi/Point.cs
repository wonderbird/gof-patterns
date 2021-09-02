using System.Collections.Generic;

namespace Facade.Logic.BingMapsAndOpenWeather.LocationApi
{
    public class Point
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; } = new();
    }
}