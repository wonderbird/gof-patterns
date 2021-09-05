using System;
using System.Collections.Generic;
using System.Globalization;

namespace Composite.Logic
{
    public class ScaleGroup : IShape
    {
        private readonly List<IShape> _shapes = new();
        private readonly double _scaleFactor;

        public ScaleGroup(string inputLine)
        {
            var fields = inputLine.Split(" ");
            _scaleFactor = Convert.ToDouble(fields[2], CultureInfo.CurrentCulture);
        }


        public void Add(IShape shape)
        {
            _shapes.Add(shape);
        }

        public BoundingBox GetBoundingBox()
        {
            var unscaledBoundingBox = GetUnscaledBoundingBox();
            var scaledBoundingBox = ScaleBoundingBox(unscaledBoundingBox, _scaleFactor);

            return scaledBoundingBox;
        }

        private BoundingBox GetUnscaledBoundingBox()
        {
            var merged = _shapes[0].GetBoundingBox();

            foreach (var shape in _shapes)
            {
                var current = shape.GetBoundingBox();
                merged.Left = Math.Min(merged.Left, current.Left);
                merged.Top = Math.Min(merged.Top, current.Top);
                merged.Right = Math.Max(merged.Right, current.Right);
                merged.Bottom = Math.Max(merged.Bottom, current.Bottom);
            }

            return merged;
        }

        private static BoundingBox ScaleBoundingBox(BoundingBox unscaledBoundingBox, double scaleFactor)
        {
            var scaledBoundingBox = new BoundingBox(unscaledBoundingBox.Left, unscaledBoundingBox.Top, 0, 0);

            var width = unscaledBoundingBox.Right - unscaledBoundingBox.Left;
            var height = unscaledBoundingBox.Bottom - unscaledBoundingBox.Top;
            var scaledWidth = (int)Math.Round(width * scaleFactor);
            var scaledHeight = (int)Math.Round(height * scaleFactor);

            scaledBoundingBox.Right = unscaledBoundingBox.Left + scaledWidth;
            scaledBoundingBox.Bottom = unscaledBoundingBox.Top + scaledHeight;
            return scaledBoundingBox;
        }
    }
}