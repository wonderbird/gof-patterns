using System.Collections.Generic;

namespace kata_gof_pattern_facade_windforecast.BingMapsAndOpenWeather.LocationApi
{
    public interface ILocationService
    {
        /// <summary>
        /// Get latitude and longitude coordinates that correspond to location information provided as a query string.
        /// </summary>
        /// <remarks>See also: https://docs.microsoft.com/en-us/bingmaps/rest-services/locations/find-a-location-by-query </remarks>
        /// <param name="query">A string that contains information about a location, such as an address or landmark name.</param>
        /// <param name="includeNeighbourhood">One of the following values: 1: Include neighborhood information when available. 0 [default]: Do not include neighborhood information.</param>
        /// <param name="include">One or more of the following options: queryParse: Specifies that the response shows how the query string was parsed into address values, such as addressLine, locality, adminDistrict, and postalCode. ciso2: Specifies to include the two-letter ISO country code. If you specify more than one include value, separate the values with a comma. Examples: incl=queryParse incl=queryParse,ciso2</param>
        /// <param name="maxResults">Maximum number of locations returned</param>
        /// <param name="key">Bing Maps API key</param>
        /// <returns>List of <seealso cref="Resource"/> matching the query string query</returns>
        /// <exception cref="WebException">Unexpected API response, e.g. access denied because of invalid apikey</exception>
        List<Resource> GetLocations(string query, string includeNeighbourhood, string include, int maxResults, string key);
    }
}