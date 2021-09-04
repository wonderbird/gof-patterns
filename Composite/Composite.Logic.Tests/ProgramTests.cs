using Xunit;
using Composite.App;
using Moq;

namespace Composite.Logic.Tests
{
    public class ProgramTests
    {
        [Theory]
        [InlineData("rect 1 2 10 20", "(1, 2) (11, 22)")]
        [InlineData("rect 0 0 0 0", "(0, 0) (0, 0)")]
        [InlineData("rect 90 90 10 10", "(90, 90) (100, 100)")]
        public void Main_SingleShape_PrintsBoundingBox(string input, string expected)
        {
            var outputMock = new Mock<IWriter>();
            Program.Output = outputMock.Object;

            var inputMock = new Mock<IReader>();
            inputMock.Setup(x => x.Read()).Returns(input);
            Program.Input = inputMock.Object;

            Program.Main(null);

            inputMock.Verify(x => x.Read());
            outputMock.Verify(x => x.Write(expected));
            outputMock.Verify(x => x.Write(It.IsAny<string>()), Times.Exactly(4));
        }
    }
}