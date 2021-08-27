namespace kata_gof_pattern_facade_windforecast.AccuWeather.WeatherForecastApi
{
    public class DailyForecast
    {
        public long EpochDate { get; set; }
        public Day Day { get; set; }
    }
}