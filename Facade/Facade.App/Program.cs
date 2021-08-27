using System;
using Facade.Lib.BingMapsAndOpenWeather;

namespace Facade.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("For which location would you like to know the wind forecast? ");
            var location = Console.ReadLine();

            Console.Write("How many days ahead (0-5)? ");
            var daysFromNow = int.Parse(Console.ReadLine());

            var windForecastService = new WindForecastService();
            //var windForecastService = new kata_gof_pattern_facade_windforecast.AccuWeather.WindForecastService();
            var beaufort = windForecastService.GetWindForecastBeaufort(location, daysFromNow);

            Console.WriteLine($"Wind of {beaufort} Beaufort is expected.");
        }
    }
}