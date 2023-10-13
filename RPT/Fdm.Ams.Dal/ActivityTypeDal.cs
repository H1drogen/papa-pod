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
    public class ActivityTypeDal : IActivityTypeDal
    {
        private readonly string apiUrl = "/academy-hub/activity-types";
        private readonly HttpClient httpClient;
        private readonly IHttpClientWrapper httpClientWrapper;
        private readonly string route;

        public ActivityTypeDal(HttpClient httpClient, IConfiguration configuration, IHttpClientWrapper httpClientWrapper)
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

        public async Task<IEnumerable<ActivityType>> GetAllAsync()
        {
            var response = await httpClientWrapper.GetAsync(httpClient, route);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IEnumerable<ActivityType>>();
        }

        public async Task<ActivityType> GetByIdAsync(int id)
        {
            var response = await httpClientWrapper.GetAsync(httpClient, $"{route}/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<ActivityType>();
        }

        public async Task<ActivityType> PostAsync(PostActivityTypeDto postDto)
        {
            var json = JsonConvert.SerializeObject(postDto);
            var postContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await httpClientWrapper.PostAsync(httpClient, route, postContent);
            response.EnsureSuccessStatusCode();

            var activityType = await response.Content.ReadAsAsync<ActivityType>();
            return activityType;
        }

        public async Task PutAsync(ActivityType activityType)
        {
            var json = JsonConvert.SerializeObject(activityType);
            var putContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await httpClientWrapper.PutAsync(httpClient, $"{route}/{activityType.Id}", putContent);
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<ActivityType>();
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
        }
    }
}