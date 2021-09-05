using System;
using System.Globalization;

namespace Composite.Logic
{
    public class Circle : IShape
    {
        private readonly int _centerX;
        private readonly int _centerY;
        private readonly int _radius;

        public Circle(int centerX, int centerY, int radius)
        {
            _centerX = centerX;
            _centerY = centerY;
            _radius = radius;
        }

        public BoundingBox GetBoundingBox() =>
            new(_centerX - _radius, _centerY - _radius, _centerX + _radius, _centerY + _radius);

        public static Circle FromUserInput(string input)
        {
            var fields = input.Split(" ");

            return new Circle(Convert.ToInt32(fields[1], CultureInfo.CurrentCulture),
                Convert.ToInt32(fields[2], CultureInfo.CurrentCulture),
                Convert.ToInt32(fields[3], CultureInfo.CurrentCulture));
        }
    }
}