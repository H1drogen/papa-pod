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
    public class UserLanguageDal : IUserLanguageDal
    {
        private readonly string apiUrl = "/academy-hub/user-languages";
        private readonly HttpClient httpClient;
        private readonly IHttpClientWrapper httpClientWrapper;
        private readonly string route;

        public UserLanguageDal(HttpClient httpClient, IConfiguration configuration, IHttpClientWrapper httpClientWrapper)
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

        public async Task<IEnumerable<UserLanguage>> GetAllAsync()
        {
            var response = await httpClientWrapper.GetAsync(httpClient, route);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IEnumerable<UserLanguage>>();
        }

        public async Task<UserLanguage> GetByIdAsync(int id)
        {
            var response = await httpClientWrapper.GetAsync(httpClient, $"{route}/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<UserLanguage>();
        }

        public async Task<UserLanguage> PostAsync(PostUserLanguageDto postDto)
        {
            var json = JsonConvert.SerializeObject(postDto);
            var postContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await httpClientWrapper.PostAsync(httpClient, route, postContent);
            response.EnsureSuccessStatusCode();

            var UserLanguage = await response.Content.ReadAsAsync<UserLanguage>();
            return UserLanguage;
        }

        public async Task PutAsync(UserLanguage userLanguage)
        {
            var json = JsonConvert.SerializeObject(userLanguage);
            var putContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await httpClientWrapper.PutAsync(httpClient, $"{route}/{userLanguage.Id}", putContent);
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<UserLanguage>();
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
        }
    }
}