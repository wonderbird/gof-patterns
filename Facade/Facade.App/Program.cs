using System;
using System.Globalization;
using Facade.Logic.BingMapsAndOpenWeather;

namespace Facade.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("For which location would you like to know the wind forecast? ");
            var location = Console.ReadLine();

            Console.Write("How many days ahead (0-5)? ");
            var daysFromNow = int.Parse(Console.ReadLine(), CultureInfo.CurrentCulture);

            var windForecastService = new WindForecastService();
            //var windForecastService = new Facade.Logic.AccuWeather.WindForecastService();
            var beaufort = windForecastService.GetWindForecastBeaufort(location, daysFromNow);

            Console.WriteLine($"Wind of {beaufort} Beaufort is expected.");
        }
    }
}