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
    public class ConsultantModuleDal : IConsultantModuleDal
    {
        private readonly string apiUrl = "/academy-hub/consultant-modules";
        private readonly HttpClient httpClient;

        private readonly IHttpClientWrapper httpClientWrapper;
        private readonly string route;

        public ConsultantModuleDal(HttpClient httpClient, IConfiguration configuration, IHttpClientWrapper httpClientWrapper)
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

        public async Task<IEnumerable<ConsultantModule>> GetAllAsync()
        {
            var response = await httpClientWrapper.GetAsync(httpClient, route);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IEnumerable<ConsultantModule>>();
        }

        public async Task<ConsultantModule> GetByIdAsync(int id)
        {
            var response = await httpClientWrapper.GetAsync(httpClient, $"{route}/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<ConsultantModule>();
        }

        public async Task<ConsultantModule> PostAsync(PostConsultantModuleDto postDto)
        {
            var json = JsonConvert.SerializeObject(postDto);
            var postContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await httpClientWrapper.PostAsync(httpClient, route, postContent);
            response.EnsureSuccessStatusCode();

            var ConsultantModule = await response.Content.ReadAsAsync<ConsultantModule>();
            return ConsultantModule;
        }

        public async Task PutAsync(ConsultantModule ConsultantModule)
        {
            var json = JsonConvert.SerializeObject(ConsultantModule);
            var putContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await httpClientWrapper.PutAsync(httpClient, $"{route}/{ConsultantModule.Id}", putContent);
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<ConsultantModule>();
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
        }
    }
}