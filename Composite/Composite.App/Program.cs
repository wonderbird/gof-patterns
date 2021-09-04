using System;
using System.Globalization;
using Composite.Logic.Tests;

namespace Composite.App
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Output.Write("Available shapes:");
            Output.Write("Rectangle: rect x y width height (example: rect 1 2 10 20)");
            Output.Write("Enter shape:");

            var input = Input.Read();
            var fields = input.Split(" ");
            var left = Convert.ToInt32(fields[1], CultureInfo.CurrentCulture);
            var top = Convert.ToInt32(fields[2], CultureInfo.CurrentCulture);
            var width = Convert.ToInt32(fields[3], CultureInfo.CurrentCulture);
            var height = Convert.ToInt32(fields[4], CultureInfo.CurrentCulture);

            Output.Write($"({left}, {top}) ({left + width}, {top + height})");
        }

        public static IWriter Output { get; set; } = new ConsoleWriter();
        public static IReader Input { get; set; } = new ConsoleReader();
    }
}