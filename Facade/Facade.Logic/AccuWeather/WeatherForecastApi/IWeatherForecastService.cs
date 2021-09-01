using System.Net;
using Facade.Logic.AccuWeather.LocationApi;

namespace Facade.Logic.AccuWeather.WeatherForecastApi
{
    public interface IWeatherForecastService
    {
        /// <summary>
        ///     Returns a list of daily forecasts for the next 5 days for a specific location.
        /// </summary>
        /// <remarks>
        ///     Forecast searches require a location key. Please use the <see cref="ILocationService.GetLocations" />
        ///     API to obtain the location key for your desired location. By default, a truncated version of
        ///     the hourly forecast data is returned. The full object can be obtained by passing
        ///     details=true.
        ///     See also:
        ///     https://developer.accuweather.com/accuweather-forecast-api/apis/get/forecasts/v1/daily/5day/%7BlocationKey%7D
        /// </remarks>
        /// <param name="locationKey">String indicating the language in which to return the resource</param>
        /// <param name="apikey">Provided API Key</param>
        /// <param name="language">String indicating the language in which to return the resource</param>
        /// <param name="details">Boolean value specifies whether or not to include full details in the response.</param>
        /// <param name="metric">Boolean value specifies whether or not to display metric values.</param>
        /// <returns>List of daily forecasts for the next 5 days for a specific location</returns>
        /// <exception cref="WebException">Unexpected API response, e.g. access denied because of invalid apikey</exception>
        WeatherForecast GetWeatherForecast(string locationKey, string apikey, string language, bool details,
            bool metric);
    }
}