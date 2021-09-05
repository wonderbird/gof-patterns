using System;
using System.Globalization;
using Composite.Logic;

namespace Composite.App
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

        public BoundingBox GetBoundingBox() => new BoundingBox(_centerX - _radius, _centerY - _radius, _centerX + _radius, _centerY + _radius);
    }
}