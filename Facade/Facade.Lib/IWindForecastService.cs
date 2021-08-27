namespace Facade.Lib
{
    public interface IWindForecastService
    {
        int GetWindForecastBeaufort(string location, int daysFromToday);
    }
}