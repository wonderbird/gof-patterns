using System;
using System.Globalization;

namespace Composite.App
{
    public class Rectangle
    {
        private readonly int _left;
        private readonly int _top;
        private readonly int _width;
        private readonly int _height;

        public Rectangle(string input)
        {
            var fields = input.Split(" ");

            _left = Convert.ToInt32(fields[1], CultureInfo.CurrentCulture);
            _top = Convert.ToInt32(fields[2], CultureInfo.CurrentCulture);
            _width = Convert.ToInt32(fields[3], CultureInfo.CurrentCulture);
            _height = Convert.ToInt32(fields[4], CultureInfo.CurrentCulture);
        }

        public BoundingBox GetBoundingBox() => new(_left, _top, _left + _width, _top + _height);
    }
}