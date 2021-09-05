using System;
using Composite.Logic;

namespace Composite.App
{
    public static class ShapeFactory
    {
        public static IShape Create(string input)
        {
            IShape shape = input[0] switch
            {
                'r' => new Rectangle(input),
                'c' => new Circle(input),
                _ => throw new ArgumentException("input does not start with shape name", nameof(input))
            };

            return shape;
        }
    }
}