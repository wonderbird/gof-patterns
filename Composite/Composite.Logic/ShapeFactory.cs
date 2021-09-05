using System;
using System.Collections.Generic;

namespace Composite.Logic
{
    public static class ShapeFactory
    {
        private static IShape FromUserInput(string input) =>
            input[0] switch
            {
                'r' => new Rectangle(input),
                'c' => new Circle(input),
                _ => throw new ArgumentException($"\"{input}\" does not start with shape name", nameof(input))
            };

        public static IShape FromUserInput(IEnumerable<string> inputLines)
        {
            var scaleGroup = new ScaleGroup();

            foreach (var inputLine in inputLines)
            {
                scaleGroup.Add(FromUserInput(inputLine));
            }

            return scaleGroup;
        }
    }
}