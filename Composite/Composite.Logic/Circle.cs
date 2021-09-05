using System;
using System.Globalization;

namespace Composite.Logic
{
    public class Circle : IShape
    {
        private readonly int _centerX;
        private readonly int _centerY;
        private readonly int _radius;

        public Circle(string input)
        {
            var fields = input.Split(" ");

            _centerX = Convert.ToInt32(fields[1], CultureInfo.CurrentCulture);
            _centerY = Convert.ToInt32(fields[2], CultureInfo.CurrentCulture);
            _radius = Convert.ToInt32(fields[3], CultureInfo.CurrentCulture);
        }

        public BoundingBox GetBoundingBox() =>
            new(_centerX - _radius, _centerY - _radius, _centerX + _radius, _centerY + _radius);
    }
}