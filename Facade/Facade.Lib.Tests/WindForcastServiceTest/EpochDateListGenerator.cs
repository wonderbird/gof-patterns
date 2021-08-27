using System;
using System.Collections.Generic;
using System.Linq;

namespace Facade.Lib.Tests.WindForcastServiceTest
{
    public static class EpochDateListGenerator
    {
        public static IEnumerable<long> EpochDatesForNextDays(int numberOfDays)
        {
            var datesForForecast = Enumerable.Range(0, numberOfDays)
                .Select(daysFromToday => DateTime.Now.ToUniversalTime().Date.AddDays(daysFromToday));

            var epochDatesForForecast = datesForForecast
                .Select(date => new DateTimeOffset(date).ToUnixTimeSeconds());
            return epochDatesForForecast;
        }
    }
}