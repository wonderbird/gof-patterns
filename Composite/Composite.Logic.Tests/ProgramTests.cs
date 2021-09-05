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
        public void Main_SingleRectangle_PrintsBoundingBox(string input, string expected)
        {
            _inputMock.SetupSequence(x => x.Read())
                .Returns(input)
                .Returns("");

            Program.Main(null);

            _inputMock.Verify(x => x.Read());
            _outputMock.Verify(x => x.Write(expected));
            _outputMock.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(NumberOfDocumentationMessages + 1));
        }

        [Theory]
        [InlineData("circle 1 2 5", "(-4, -3) (6, 7)")]
        public void Main_SingleCircle_PrintsBoundingBox(string input, string expected)
        {
            _inputMock.SetupSequence(x => x.Read())
                .Returns(input)
                .Returns("");

            Program.Main(null);

            _inputMock.Verify(x => x.Read());
            _outputMock.Verify(x => x.Write(expected));
            _outputMock.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(NumberOfDocumentationMessages + 1));
        }

        [Theory]
        [InlineData(new[] { "rect 5 5 5 10", "rect 8 8 1 10" }, "(5, 5) (10, 18)")]
        public void Main_MultipleRectangles_PrintsCombinedBoundingBox(string[] input, string expected)
        {
            var readInvocation = _inputMock.SetupSequence(x => x.Read());
            foreach (var current in input)
            {
                readInvocation.Returns(current);
            }

            readInvocation.Returns("");

            Program.Main(null);

            _inputMock.Verify(x => x.Read(), Times.Exactly(3));
            _outputMock.Verify(x => x.Write(expected));
            _outputMock.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(NumberOfDocumentationMessages + 1));
        }
    }
}