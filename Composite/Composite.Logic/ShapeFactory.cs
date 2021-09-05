using System;
using System.Collections.Generic;
using System.Linq;

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

        public static IList<IShape> FromUserInput(IEnumerable<string> inputLines)
        {
            return inputLines.Select(FromUserInput).ToList();
        }
    }
}