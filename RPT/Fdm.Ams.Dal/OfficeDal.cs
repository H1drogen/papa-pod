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
    public class OfficeDal : IOfficeDal
    {
        private readonly string apiUrl = "/rpt/offices";
        private readonly IGenericMessageHelper genericMessageHelper;
        private readonly HttpClient httpClient;
        private readonly IHttpClientWrapper httpClientWrapper;
        private readonly IResponseStatusCodeHelper responseStatusCodeHelper;
        private readonly string route;
        private string conflictedMessage = string.Empty;

        public OfficeDal(HttpClient httpClient,
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

        public async Task<IEnumerable<Office>> GetAllAsync()
        {
            var response = await httpClientWrapper.GetAsync(httpClient, route);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IEnumerable<Office>>();
        }

        public async Task<Office> GetByIdAsync(int id)
        {
            var response = await httpClientWrapper.GetAsync(httpClient, $"{route}/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Office>();
        }

        public async Task<IEnumerable<Office>> GetActiveOfficesByCountryID(int countryId)
        {
            var response = await httpClientWrapper.GetAsync(httpClient, $"{route}/countryId/{countryId}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IEnumerable<Office>>();
        }

        public async Task<Office> PostAsync(Office office)
        {
            var json = JsonConvert.SerializeObject(office);
            var postContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await httpClientWrapper.PostAsync(httpClient, route, postContent);

            if (!response.IsSuccessStatusCode)
            {
                await responseStatusCodeHelper.FailedResponseFromAPI(response, conflictedMessage);
            }

            return await response.Content.ReadAsAsync<Office>();
        }

        public async Task<Office> PutAsync(Office office)
        {
            var json = JsonConvert.SerializeObject(office);
            var putContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await httpClientWrapper.PutAsync(httpClient, $"{route}/{office.Id}", putContent);

            if (!response.IsSuccessStatusCode)
            {
                await responseStatusCodeHelper.FailedResponseFromAPI(response, conflictedMessage);
            }
            return await response.Content.ReadAsAsync<Office>();
        }
    }
}