using System.Net.Http;
using System.Threading.Tasks;

namespace Fdm.Ams.Common
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        public async Task<HttpResponseMessage> DeleteAsync(HttpClient httpClient, string route)
        {
            return await httpClient.DeleteAsync(route);
        }

        public async Task<HttpResponseMessage> GetAsync(HttpClient httpClient, string route)
        {
            return await httpClient.GetAsync(route);
        }

        public async Task<HttpResponseMessage> PostAsync(HttpClient httpClient, string route, StringContent stringContent)
        {
            return await httpClient.PostAsync(route, stringContent);
        }

        public async Task<HttpResponseMessage> PutAsync(HttpClient httpClient, string route, StringContent stringContent)
        {
            return await httpClient.PutAsync(route, stringContent);
        }
    }
}