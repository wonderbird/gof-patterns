#nullable enable
using System.Globalization;
using System.Net;

namespace Facade.Logic
{
    public class UnexpectedApiResponseException : WebException
    {
        public UnexpectedApiResponseException(string webRequestPayload)
            : base(string.Format(CultureInfo.InvariantCulture, StringResources.UnexpectedApiResponse,
                webRequestPayload))
        {
        }
    }
}