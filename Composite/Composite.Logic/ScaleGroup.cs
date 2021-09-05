using System;
using System.Collections.Generic;

namespace Composite.Logic
{
    public class ScaleGroup : IShape
    {
        private readonly List<IShape> _shapes = new();

        public BoundingBox GetBoundingBox()
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

        public void Add(IShape shape)
        {
            _shapes.Add(shape);
        }
    }
}