namespace Facade.Lib.WindSpeedConverterApi
{
    public interface IWindSpeedConverterService
    {
        int MetersPerSecondToBeaufort(double input);
        int KilometersPerHourToBeaufort(double input);
    }
}