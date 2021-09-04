namespace Composite.App
{
    public class BoundingBox
    {
        private readonly int _left;
        private readonly int _top;
        private readonly int _right;
        private readonly int _bottom;

        public BoundingBox(int left, int top, int right, int bottom)
        {
            _left = left;
            _top = top;
            _right = right;
            _bottom = bottom;
        }

        public override string ToString() => $"({_left}, {_top}) ({_right}, {_bottom})";
    }
}