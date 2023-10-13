using Fdm.Ams.Common;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal
{
    public class CourseDal : ICourseDal
    {
        private readonly string apiUrl = "/rpt/pathway";
        private readonly IGenericMessageHelper genericMessageHelper;
        private readonly HttpClient httpClient;
        private readonly IHttpClientWrapper httpClientWrapper;
        private readonly IResponseStatusCodeHelper responseStatusCodeHelper;
        private readonly string route;
        private string conflictedMessage = string.Empty;

        public CourseDal(HttpClient httpClient,
            IConfiguration configuration,
            IHttpClientWrapper httpClientWrapper,
            IResponseStatusCodeHelper responseStatusCodeHelper, IGenericMessageHelper genericMessageHelper)
        {
            this.httpClient = httpClient;
            this.httpClientWrapper = httpClientWrapper;
            route = $"{configuration["ServiceDependencies:ApiBaseUrl"]}{apiUrl}";
            this.responseStatusCodeHelper = responseStatusCodeHelper;
            this.genericMessageHelper = genericMessageHelper;
        }

        public async Task DeleteAsync(int id)
        {
            var response = await httpClientWrapper.DeleteAsync(httpClient, $"{route}/{id}");
            conflictedMessage = genericMessageHelper.CreateGenericConflictMessage(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                await responseStatusCodeHelper.FailedResponseFromAPI(response, conflictedMessage);
            }
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            var response = await httpClientWrapper.GetAsync(httpClient, route);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IEnumerable<Course>>();
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            var response = await httpClientWrapper.GetAsync(httpClient, $"{route}/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Course>();
        }

        public async Task<Course> PostAsync(Course course)
        {
            var json = JsonConvert.SerializeObject(course);
            var postContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await httpClientWrapper.PostAsync(httpClient, route, postContent);

            if (!response.IsSuccessStatusCode)
            {
                await responseStatusCodeHelper.FailedResponseFromAPI(response, conflictedMessage);
            }

            return await response.Content.ReadAsAsync<Course>();
        }

        public async Task<Course> PutAsync(Course course)
        {
            var json = JsonConvert.SerializeObject(course);
            var putContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await httpClientWrapper.PutAsync(httpClient, $"{route}/{course.Id}", putContent);
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<Course>();
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return await response.Content?.ReadAsAsync<Course>();   

        }
    }
}