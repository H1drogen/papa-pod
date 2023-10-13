using System.Net.Http;
using System.Threading.Tasks;

namespace Fdm.Ams.Common
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> DeleteAsync(HttpClient httpClient, string route);

        Task<HttpResponseMessage> GetAsync(HttpClient httpClient, string route);

        Task<HttpResponseMessage> PostAsync(HttpClient httpClient, string route, StringContent stringContent);

        Task<HttpResponseMessage> PutAsync(HttpClient httpClient, string route, StringContent stringContent);
    }
}