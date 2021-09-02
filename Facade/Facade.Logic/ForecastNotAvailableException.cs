#nullable enable
using System;

namespace Facade.Logic
{
    public class ForecastNotAvailableException : ArgumentOutOfRangeException
    {
        public ForecastNotAvailableException(string? paramName, object? actualValue, string? message)
            : base(paramName, actualValue, message)
        {
        }

        public ForecastNotAvailableException(string? paramName, object? actualValue,
            long lastAvailableForecastDateInUnixTimeSeconds)
            : base(paramName, actualValue,
                CreateMaximumNumberOfDaysSupportedMessage(lastAvailableForecastDateInUnixTimeSeconds))
        {
        }

        private static string CreateMaximumNumberOfDaysSupportedMessage(long lastAvailableForecastDateInUnixTimeSeconds)
        {
            var lastForecastDate = DateTimeOffset.FromUnixTimeSeconds(lastAvailableForecastDateInUnixTimeSeconds).Date;
            var numberOfDays = (lastForecastDate - DateTime.Now.ToUniversalTime().Date).Days;

            return $"Forecast only available for {numberOfDays} days";
        }
    }
}