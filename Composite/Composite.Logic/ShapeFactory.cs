using System;
using System.Collections.Generic;
using System.Linq;

namespace Composite.Logic
{
    public static class ShapeFactory
    {
        private enum ShapeIdentifier
        {
            Rectangle = 'r',
            Circle = 'c'
        }

        private enum ScaleGroupIdentifier
        {
            ScaleGroupStart = 's',
            ScaleGroupEnd = 'e'
        }

        private static IShape TakeSingleShapeFromUserInput(string input) =>
            (ShapeIdentifier)input[0] switch
            {
                ShapeIdentifier.Rectangle => new Rectangle(input),
                ShapeIdentifier.Circle => new Circle(input),
                _ => throw new ArgumentException($"\"{input}\" does not start with shape name", nameof(input))
            };

        private static IShape TakeScaleGroupFromInput(Stack<string> inputLines)
        {
            var scaleGroup = new ScaleGroup();

            while (inputLines.TryPop(out var inputLine))
            {
                var scaleGroupIdentifier = (ScaleGroupIdentifier)inputLine[0];
                switch (scaleGroupIdentifier)
                {
                    case ScaleGroupIdentifier.ScaleGroupStart:
                        var subGroup = TakeScaleGroupFromInput(inputLines);
                        scaleGroup.Add(subGroup);
                        break;

                    case ScaleGroupIdentifier.ScaleGroupEnd:
                        return scaleGroup;

                    default:
                        scaleGroup.Add(TakeSingleShapeFromUserInput(inputLine));
                        break;
                }
            }

            return scaleGroup;
        }

        public static IShape FromUserInput(IEnumerable<string> inputLines)
        {
            var inputLinesStack = new Stack<string>(inputLines.Reverse());
            return TakeScaleGroupFromInput(inputLinesStack);
        }
    }
}