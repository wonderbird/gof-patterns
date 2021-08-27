using System.Collections.Generic;
using System.Net;

namespace kata_gof_pattern_facade_windforecast.AccuWeather.LocationApi
{
    public interface ILocationService
    {
        /// <summary>
        /// Returns information for an array of locations that match the search text.
        /// </summary>
        /// <remarks>See also: https://developer.accuweather.com/accuweather-locations-api/apis/get/locations/v1/search </remarks>
        /// <param name="apikey">Provided API Key.</param>
        /// <param name="q">Text to search for.</param>
        /// <param name="language">String indicating the language in which to return the resource.</param>
        /// <param name="details">Boolean value specifies whether or not to include full details in the response.</param>
        /// <param name="offset">Integer, along with the limit (25) that determines the first resource to be returned. If no offset is provided, the max number (100) of results will be returned.</param>
        /// <param name="alias">Enumeration that specifies when alias locations should be included in the results. By default, an alias will only be returned if no official match for the search text was found. Enumeration values: Never or Always</param>
        /// <returns>List of <seealso cref="Location"/> matching the query string q</returns>
        /// <exception cref="WebException">Unexpected API response, e.g. access denied because of invalid apikey</exception>
        IList<Location> GetLocations(string apikey, string q, string language, bool details, int offset, string alias);
    }
}