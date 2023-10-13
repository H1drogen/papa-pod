using Fdm.Ams.Common;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.Models.Post;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal
{
    public class GlobalGeoflexDal : IGlobalGeoflexDal
    {
        private readonly string apiUrl = "/academy-hub/global-geoflex";
        private readonly HttpClient httpClient;
        private readonly IHttpClientWrapper httpClientWrapper;
        private readonly string route;

        public GlobalGeoflexDal(HttpClient httpClient, IConfiguration configuration, IHttpClientWrapper httpClientWrapper)
        {
            this.httpClient = httpClient;
            this.httpClientWrapper = httpClientWrapper;
            route = $"{configuration["ServiceDependencies:ApiBaseUrl"]}{apiUrl}";
        }

        public async Task DeleteAsync(int id)
        {
            var response = await httpClientWrapper.DeleteAsync(httpClient, $"{route}/{id}");
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{id}");
            }
        }

        public async Task<IEnumerable<GlobalGeoflex>> GetAllAsync()
        {
            var response = await httpClientWrapper.GetAsync(httpClient, route);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IEnumerable<GlobalGeoflex>>();
        }

        public async Task<GlobalGeoflex> GetByIdAsync(int id)
        {
            var response = await httpClientWrapper.GetAsync(httpClient, $"{route}/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<GlobalGeoflex>();
        }

        public async Task<GlobalGeoflex> PostAsync(PostGlobalGeoflexDto postDto)
        {
            var json = JsonConvert.SerializeObject(postDto);
            var postContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await httpClientWrapper.PostAsync(httpClient, route, postContent);
            response.EnsureSuccessStatusCode();

            var GlobalGeoflex = await response.Content.ReadAsAsync<GlobalGeoflex>();
            return GlobalGeoflex;
        }

        public async Task PutAsync(GlobalGeoflex globalGeoflex)
        {
            var json = JsonConvert.SerializeObject(globalGeoflex);
            var putContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await httpClientWrapper.PutAsync(httpClient, $"{route}/{globalGeoflex.Id}", putContent);
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<GlobalGeoflex>();
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
        }
    }
}