using System.Collections.Generic;
using Composite.App.IO;

namespace Composite.App
{
    public static class Program
    {
        public static IWriter Output { get; set; } = new ConsoleWriter();
        public static IReader Input { get; set; } = new ConsoleReader();

        public static void Main(string[] _)
        {
            PrintUsageInstructions();

            var inputLines = ReadUserInput();

            var shape = ShapeFactory.FromUserInput(inputLines);

            Output.Write(shape.GetBoundingBox().ToString());
        }

        private static void PrintUsageInstructions()
        {
            Output.Write("Available shapes:");
            Output.Write("Rectangle:   rect x y width height  (example: rect 1 2 10 20)");
            Output.Write("   Circle: circle x y r             (example: rect 1 2 5)");
            Output.Write("Enter shape (enter a blank line to finish):");
        }

        private static IEnumerable<string> ReadUserInput()
        {
            var inputLines = new List<string>();

            string input;
            while (!string.IsNullOrEmpty(input = Input.Read()))
            {
                inputLines.Add(input);
            }

            return inputLines;
        }
    }
}