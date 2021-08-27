using System;
using Xunit;

namespace Facade.Lib.Tests.WindForcastServiceTest
{
    /// <summary>
    /// Standardized test for all IWindForecastService facade implementations
    /// </summary>
    /// <remarks>
    /// The following test ensures that <seealso cref="IWindForecastService.GetWindForecastBeaufort" />
    /// returns the correct wind speed for a given day in the future. It is rather independent from
    /// the way the facade is actually implemented.
    ///
    /// The test uses a builder for each facade implementation. The task of the builder is to
    /// - set up the required mocks and associated data they should return
    /// - create an instance of the IWindForecastService facade under test including the wired mocks
    /// - assert that the mocked methods have been called as expected by
    ///   <seealso cref="IWindForecastService.GetWindForecastBeaufort" />
    ///
    /// The builder is controlled by the <seealso cref="TestDirector"/>, which offers
    /// high level methods for the test.
    /// </remarks>
    public class WindForecastServiceTest
    {
        [Theory]
        [InlineData(typeof(BingMapsAndOpenWeatherTestBuilder), 0, 7)]
        [InlineData(typeof(BingMapsAndOpenWeatherTestBuilder), 4, 11)]
        [InlineData(typeof(AccuWeatherTestBuilder), 0, 7)]
        [InlineData(typeof(AccuWeatherTestBuilder), 4, 11)]
        public void GetWindForecast_GivenDayInTheFuture_ReturnsWindSpeed(Type testBuilderType, int daysFromToday, int expectedWindSpeedBeaufort)
        {
            const string location = "Sample Location";

            var builder = (ITestBuilder)Activator.CreateInstance(testBuilderType);
            var director = new TestDirector(builder);

            director.SetupWindspeedForNextDays(7, 8, 9, 10, 11);
            var actualWindSpeedBeaufort = director.GetWindForecastBeaufort(location, daysFromToday);

            director.VerifyMocks();
            Assert.Equal(expectedWindSpeedBeaufort, actualWindSpeedBeaufort);
        }

        [Theory]
        [InlineData(typeof(BingMapsAndOpenWeatherTestBuilder), -1)]
        [InlineData(typeof(BingMapsAndOpenWeatherTestBuilder), 5)]
        [InlineData(typeof(AccuWeatherTestBuilder), -1)]
        [InlineData(typeof(AccuWeatherTestBuilder), 5)]
        public void GetWindForecast_InvalidDayInTheFuture_ThrowsOutOfRangeException(Type testBuilderType, int daysFromToday)
        {
            const string location = "Sample Location";

            var builder = (ITestBuilder)Activator.CreateInstance(testBuilderType);
            var director = new TestDirector(builder);

            director.SetupWindspeedForNextDays(7, 8, 9, 10, 11);

            Assert.Throws<ArgumentOutOfRangeException>(() => director.GetWindForecastBeaufort(location, daysFromToday));
        }
    }
}
