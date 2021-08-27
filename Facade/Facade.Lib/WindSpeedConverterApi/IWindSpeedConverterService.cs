namespace kata_gof_pattern_facade_windforecast.WindSpeedConverterApi
{
    public interface IWindSpeedConverterService
    {
        int MetersPerSecondToBeaufort(double input);
        int KilometersPerHourToBeaufort(double input);
    }
}