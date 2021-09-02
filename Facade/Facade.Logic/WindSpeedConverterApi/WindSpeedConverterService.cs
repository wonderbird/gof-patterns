using System.Collections.Generic;

namespace Facade.Logic.WindSpeedConverterApi
{
    public class WindSpeedConverterService : IWindSpeedConverterService
    {
        public int MetersPerSecondToBeaufort(double input)
        {
            var beaufortUpperThresholdsInMeterPerSecond = new List<double>
            {
                0.2,
                1.5,
                3.3,
                5.4,
                7.9,
                10.7,
                13.8,
                17.1,
                20.7,
                24.4,
                28.4,
                32.6,
                int.MaxValue
            };

            var beaufort = beaufortUpperThresholdsInMeterPerSecond.FindIndex(x => x >= input);

            return beaufort;
        }

        public int KilometersPerHourToBeaufort(double input)
        {
            var metersPerSecond = input / 3.6;
            return MetersPerSecondToBeaufort(metersPerSecond);
        }
    }
}