using System.Collections.Generic;
using Composite.App.IO;
using Composite.Logic;

namespace Composite.App
{
    public static class Program
    {
        public static IWriter Output { get; set; } = new ConsoleWriter();
        public static IReader Input { get; set; } = new ConsoleReader();

        public static void Main(string[] args)
        {
            PrintUsageInstructions();

            var inputLines = ReadUserInput();

            var shapes = ShapeFactory.FromUserInput(inputLines);

            var boundingBox = BoundingBoxMerger.Merge(shapes);

            Output.Write(boundingBox.ToString());
        }

        private static void PrintUsageInstructions()
        {
            Output.Write("Available shapes:");
            Output.Write("Rectangle:   rect x y width height  (example: rect 1 2 10 20)");
            Output.Write("   Circle: circle x y r             (example: rect 1 2 5)");
            Output.Write("Enter shape (enter a blank line to finish):");
        }

        private static List<string> ReadUserInput()
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