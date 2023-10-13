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
    public class ActivityDal : IActivityDal
    {
        private readonly string apiUrl = "/academy-hub/activities";
        private readonly HttpClient httpClient;
        private readonly IHttpClientWrapper httpClientWrapper;
        private readonly string route;

        public ActivityDal(HttpClient httpClient, IConfiguration configuration, IHttpClientWrapper httpClientWrapper)
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

        public async Task<IEnumerable<Activity>> GetAllAsync()
        {
            var response = await httpClientWrapper.GetAsync(httpClient, $"{route}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IEnumerable<Activity>>();
        }

        public async Task<Activity> GetByIdAsync(int id)
        {
            var response = await httpClientWrapper.GetAsync(httpClient, $"{route}/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Activity>();
        }

        public async Task<Activity> PostAsync(PostActivityDto postDto)
        {
            var json = JsonConvert.SerializeObject(postDto);
            var postContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await httpClientWrapper.PostAsync(httpClient, route, postContent);
            response.EnsureSuccessStatusCode();

            var activity = await response.Content.ReadAsAsync<Activity>();
            return activity;
        }

        public async Task PutAsync(Activity activity)
        {
            var json = JsonConvert.SerializeObject(activity);
            var putContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await httpClientWrapper.PutAsync(httpClient, $"{route}/{activity.Id}", putContent);
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<Activity>();
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
        }
    }
}