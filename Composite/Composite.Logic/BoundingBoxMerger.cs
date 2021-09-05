using System;
using System.Collections.Generic;

namespace Composite.Logic
{
    public static class BoundingBoxMerger
    {
        public static BoundingBox Merge(IList<IShape> shapes)
        {
            var merged = shapes[0].GetBoundingBox();

            foreach (var shape in shapes)
            {
                var current = shape.GetBoundingBox();
                merged.Left = Math.Min(merged.Left, current.Left);
                merged.Top = Math.Min(merged.Top, current.Top);
                merged.Right = Math.Max(merged.Right, current.Right);
                merged.Bottom = Math.Max(merged.Bottom, current.Bottom);
            }

            return merged;
        }
    }
}