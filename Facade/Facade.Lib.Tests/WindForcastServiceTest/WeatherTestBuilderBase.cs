namespace Facade.Lib.Tests.WindForcastServiceTest
{
    public abstract class WeatherTestBuilderBase<TWeatherForecastServiceMock> : ITestBuilder
    {
        protected TWeatherForecastServiceMock weatherForecastServiceMock;

        public abstract void SetupWindspeedForNextDays(params int[] windSpeedForNextDays);
        public abstract void SetupMocks(string location);
        public abstract IWindForecastService CreateWindForecastService();
        public abstract void VerifyMocks();
    }
}
