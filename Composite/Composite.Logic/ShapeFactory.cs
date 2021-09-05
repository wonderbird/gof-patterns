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

        private static IShape ConsumeInputAndAddShapesToScaleGroup(Stack<string> inputLines, ScaleGroup scaleGroup)
        {
            while (inputLines.TryPop(out var inputLine))
            {
                var scaleGroupIdentifier = (ScaleGroupIdentifier)inputLine[0];
                switch (scaleGroupIdentifier)
                {
                    case ScaleGroupIdentifier.ScaleGroupStart:
                        var subGroup = new ScaleGroup(inputLine);
                        ConsumeInputAndAddShapesToScaleGroup(inputLines, subGroup);
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
            var scaleGroup = new ScaleGroup("scale group 1.0");
            return ConsumeInputAndAddShapesToScaleGroup(inputLinesStack, scaleGroup);
        }
    }
}