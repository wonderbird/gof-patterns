using System;
using System.Globalization;

namespace Composite.Logic
{
    public class Rectangle : IShape
    {
        private readonly int _height;
        private readonly int _left;
        private readonly int _top;
        private readonly int _width;

        public Rectangle(int left, int top, int width, int height)
        {
            _left = left;
            _top = top;
            _width = width;
            _height = height;
        }

        public BoundingBox GetBoundingBox() => new(_left, _top, _left + _width, _top + _height);

        public static Rectangle FromUserInput(string input)
        {
            var fields = input.Split(" ");

            return new Rectangle(Convert.ToInt32(fields[1], CultureInfo.CurrentCulture),
                Convert.ToInt32(fields[2], CultureInfo.CurrentCulture),
                Convert.ToInt32(fields[3], CultureInfo.CurrentCulture),
                Convert.ToInt32(fields[4], CultureInfo.CurrentCulture));
        }
    }
}