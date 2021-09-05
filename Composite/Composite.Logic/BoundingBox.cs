namespace Composite.Logic
{
    public class BoundingBox
    {
        public BoundingBox(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }

        public override string ToString() => $"({Left}, {Top}) ({Right}, {Bottom})";
    }
}