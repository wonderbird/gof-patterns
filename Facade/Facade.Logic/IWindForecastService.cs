namespace Facade.Logic
{
    public interface IWindForecastService
    {
        int GetWindForecastBeaufort(string location, int daysFromToday);
    }
}