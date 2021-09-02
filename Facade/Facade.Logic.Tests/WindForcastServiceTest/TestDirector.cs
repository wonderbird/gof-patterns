namespace Facade.Logic.Tests.WindForcastServiceTest
{
    public class TestDirector
    {
        private readonly ITestBuilder builder;

        public TestDirector(ITestBuilder builder) => this.builder = builder;

        public void SetupWindspeedForNextDays(params int[] windSpeedForNextDays)
        {
            builder.SetupWindspeedForNextDays(windSpeedForNextDays);
        }

        public int GetWindForecastBeaufort(string location, int daysFromToday)
        {
            builder.SetupMocks(location);
            var service = builder.CreateWindForecastService();

            return service.GetWindForecastBeaufort(location, daysFromToday);
        }

        public void VerifyMocks()
        {
            builder.VerifyMocks();
        }
    }
}