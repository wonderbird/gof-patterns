using System;
using System.Collections.Generic;

namespace Composite.Logic
{
    public static class BoundingBoxMerger
    {
        public static BoundingBox Merge(IList<Rectangle> rectangles)
        {
            var boundingBox = rectangles[0].GetBoundingBox();

            foreach (var rectangle in rectangles)
            {
                var currentShapeBoundingBox = rectangle.GetBoundingBox();
                boundingBox.Left = Math.Min(boundingBox.Left, currentShapeBoundingBox.Left);
                boundingBox.Top = Math.Min(boundingBox.Top, currentShapeBoundingBox.Top);
                boundingBox.Right = Math.Max(boundingBox.Right, currentShapeBoundingBox.Right);
                boundingBox.Bottom = Math.Max(boundingBox.Bottom, currentShapeBoundingBox.Bottom);
            }

            return boundingBox;
        }
    }
}