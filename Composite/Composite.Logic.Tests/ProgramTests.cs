using Composite.App;
using Composite.App.IO;
using Moq;
using Xunit;

namespace Composite.Logic.Tests
{
    public class ProgramTests
    {
        private const int NumberOfDocumentationMessages = 4;

        private readonly Mock<IReader> _inputMock;
        private readonly Mock<IWriter> _outputMock;
        private int _expectedReadCalls;

        public ProgramTests()
        {
            _outputMock = new Mock<IWriter>();
            Program.Output = _outputMock.Object;

            _inputMock = new Mock<IReader>();
            Program.Input = _inputMock.Object;
        }

        [Theory]
        [InlineData("rect 1 2 10 20", "(1, 2) (11, 22)")]
        [InlineData("rect 0 0 0 0", "(0, 0) (0, 0)")]
        [InlineData("rect 90 90 10 10", "(90, 90) (100, 100)")]
        public void Main_SingleRectangle_PrintsBoundingBox(string input, string expectedBoundingBox)
        {
            GivenUserInput_WhenProgramRuns_ThenBoundingBoxIsPrintedAndAllInputIsConsumed(input,
                expectedBoundingBox);
        }

        [Theory]
        [InlineData("circle 1 2 5", "(-4, -3) (6, 7)")]
        public void Main_SingleCircle_PrintsBoundingBox(string input, string expectedBoundingBox)
        {
            GivenUserInput_WhenProgramRuns_ThenBoundingBoxIsPrintedAndAllInputIsConsumed(input,
                expectedBoundingBox);
        }

        [Theory]
        [InlineData(new[] { "rect 5 5 5 10", "rect 8 8 1 10" }, "(5, 5) (10, 18)")]
        [InlineData(new[] { "rect 20 20 100 100", "circle 170 170 50" }, "(20, 20) (220, 220)")]
        public void Main_MultipleShapes_PrintsCombinedBoundingBox(string[] input, string expectedBoundingBox)
        {
            GivenUserInput_WhenProgramRuns_ThenBoundingBoxIsPrintedAndAllInputIsConsumed(input, expectedBoundingBox);
        }

        [Theory]
        [InlineData(new[] { "scale group 1.0", "circle 170 170 50", "end group" }, "(120, 120) (220, 220)")]
        [InlineData(new[] { "scale group 0.1", "circle 170 170 50", "end group" }, "(120, 120) (130, 130)")]
        public void Main_ScaleGroup_PrintsScaledBoundingBox(string[] input, string expectedBoundingBox)
        {
            GivenUserInput_WhenProgramRuns_ThenBoundingBoxIsPrintedAndAllInputIsConsumed(input, expectedBoundingBox);
        }

        private void GivenUserInput_WhenProgramRuns_ThenBoundingBoxIsPrintedAndAllInputIsConsumed(string[] input,
            string expectedBoundingBox)
        {
            SetupUserInput(input);

            Program.Main(null);

            AssertBoundingBoxIsPrintedAndAllInputIsConsumed(expectedBoundingBox);
        }

        private void GivenUserInput_WhenProgramRuns_ThenBoundingBoxIsPrintedAndAllInputIsConsumed(string input,
            string expectedBoundingBox)
        {
            GivenUserInput_WhenProgramRuns_ThenBoundingBoxIsPrintedAndAllInputIsConsumed(new[] { input }, expectedBoundingBox);
        }

        private void SetupUserInput(string[] input)
        {
            var readInvocation = _inputMock.SetupSequence(x => x.Read());
            foreach (var current in input)
            {
                readInvocation.Returns(current);
            }

            readInvocation.Returns("");

            _expectedReadCalls = input.Length + 1;
        }

        private void AssertBoundingBoxIsPrintedAndAllInputIsConsumed(string expectedBoundingBox)
        {
            _inputMock.Verify(x => x.Read(), Times.Exactly(_expectedReadCalls));
            _outputMock.Verify(x => x.Write(expectedBoundingBox));
            _outputMock.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(NumberOfDocumentationMessages + 1));
        }
    }
}