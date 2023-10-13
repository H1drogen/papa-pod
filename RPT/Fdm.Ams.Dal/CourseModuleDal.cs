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
    public class CourseModuleDal : ICourseModuleDal
    {
        private readonly string apiUrl = "/rpt/courses";
        private readonly IGenericMessageHelper genericMessageHelper;
        private readonly HttpClient httpClient;
        private readonly IHttpClientWrapper httpClientWrapper;
        private readonly IResponseStatusCodeHelper responseStatusCodeHelper;
        private readonly string route;
        private string conflictedMessage = string.Empty;

        public CourseModuleDal(HttpClient httpClient,
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

        public async Task<IEnumerable<CourseModule>> GetAllAsync()
        {
            var response = await httpClientWrapper.GetAsync(httpClient, route);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IEnumerable<CourseModule>>();
        }

        public async Task<CourseModule> GetByIdAsync(int id)
        {
            var response = await httpClientWrapper.GetAsync(httpClient, $"{route}/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<CourseModule>();
        }

        public async Task<CourseModule> PostAsync(CourseModule courseModule)
        {
            var json = JsonConvert.SerializeObject(courseModule);
            var postContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await httpClientWrapper.PostAsync(httpClient, route, postContent);

            if (!response.IsSuccessStatusCode)
            {
                await responseStatusCodeHelper.FailedResponseFromAPI(response, conflictedMessage);
            }

            return await response.Content.ReadAsAsync<CourseModule>();
        }

        public async Task<CourseModule> PutAsync(CourseModule courseModule)
        {
            var json = JsonConvert.SerializeObject(courseModule);
            var putContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await httpClientWrapper.PutAsync(httpClient, $"{route}/{courseModule.Id}", putContent);

            if (!response.IsSuccessStatusCode)
            {
                await responseStatusCodeHelper.FailedResponseFromAPI(response, conflictedMessage);
            }
            return await response.Content.ReadAsAsync<CourseModule>();
        }
    }
}