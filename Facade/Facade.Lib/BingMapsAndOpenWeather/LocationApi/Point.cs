using System.Collections.Generic;

namespace Facade.Lib.BingMapsAndOpenWeather.LocationApi
{
    public class Point
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; } = new List<double>();
    }
}