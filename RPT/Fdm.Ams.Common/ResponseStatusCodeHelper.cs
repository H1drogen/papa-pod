using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fdm.Ams.Common
{
    public class ResponseStatusCodeHelper : IResponseStatusCodeHelper
    {
        public async Task<HttpRequestException> FailedResponseFromAPI(HttpResponseMessage responseMessage, string conflictedMessage)
        {
            var statusCode = responseMessage.StatusCode;
            var responseBody = await responseMessage.Content.ReadAsStringAsync();
            if (statusCode == HttpStatusCode.Conflict)
            {
                responseBody = conflictedMessage;
            }

            responseMessage.Content?.Dispose();
            throw new HttpRequestException(responseBody, null, statusCode);
        }
    }
}