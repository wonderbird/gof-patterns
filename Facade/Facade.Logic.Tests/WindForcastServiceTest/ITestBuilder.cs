namespace Facade.Logic.Tests.WindForcastServiceTest
{
    public interface ITestBuilder
    {
        void SetupWindspeedForNextDays(params int[] windSpeedForNextDays);
        void SetupMocks(string location);
        IWindForecastService CreateWindForecastService();
        void VerifyMocks();
    }
}