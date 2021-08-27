using kata_gof_pattern_facade_windforecast.WindSpeedConverterApi;
using Xunit;

namespace Facade.Lib.Tests.WindSpeedConverterApi
{
    public class WindSpeedConverterTest
    {
        [Theory]
        [InlineData(0, 0.0)]
        [InlineData(1, 0.3)]
        [InlineData(2, 1.6)]
        [InlineData(3, 3.4)]
        [InlineData(4, 5.5)]
        [InlineData(5, 8.0)]
        [InlineData(6, 10.8)]
        [InlineData(7, 13.9)]
        [InlineData(8, 17.2)]
        [InlineData(9, 20.8)]
        [InlineData(10, 24.5)]
        [InlineData(11, 28.5)]
        [InlineData(12, 32.7)]
        public void MetersPerSecondToBeaufort(int expected, double input)
        {
            var converter = new WindSpeedConverterService();
            var actual = converter.MetersPerSecondToBeaufort(input);

            Assert.Equal(expected, actual);
        }
    }
}
