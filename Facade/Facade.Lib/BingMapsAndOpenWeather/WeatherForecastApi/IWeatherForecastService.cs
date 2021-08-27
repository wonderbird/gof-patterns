using kata_gof_pattern_facade_windforecast.BingMapsAndOpenWeather.LocationApi;

namespace kata_gof_pattern_facade_windforecast.BingMapsAndOpenWeather.WeatherForecastApi
{
    public interface IWeatherForecastService
    {
        /// <summary>
        /// One Call API provides the weather data in different granularity for any geographical coordinate.
        /// </summary>
        /// <remarks>
        /// Forecast searches require the GPS latitude and longitude of the location. Please use the
        /// <see cref="ILocationService.GetLocations"/> API to obtain the coordinates for your desired location.
        /// 
        /// See also: https://openweathermap.org/api/one-call-api
        /// </remarks>
        /// <param name="lat">Geographical coordinates of the location (latitude)</param>
        /// <param name="lat">Geographical coordinates of the location (longitude)</param>
        /// <param name="apikey">Personal API key</param>
        /// <param name="units">Use the ISO standard for physical units</param>
        /// <param name="lang">Language for description fields</param>
        /// <returns>Weather data in different granularity for any geographical coordinate</returns>
        /// <exception cref="WebException">Unexpected API response, e.g. access denied because of invalid apikey</exception>
        WeatherForecast GetWeatherForecast(double lat, double lon, string apikey, string units, string lang);
    }
}