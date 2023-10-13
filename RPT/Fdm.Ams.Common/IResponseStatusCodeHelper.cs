using System.Net.Http;
using System.Threading.Tasks;

namespace Fdm.Ams.Common
{
    public interface IResponseStatusCodeHelper
    {
        Task<HttpRequestException> FailedResponseFromAPI(HttpResponseMessage responseMessage, string conflictedMessage);
    }
}