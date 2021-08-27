namespace kata_gof_pattern_facade_windforecast
{
    public interface IWindForecastService
    {
        int GetWindForecastBeaufort(string location, int daysFromToday);
    }
}