using Fdm.Ams.Common;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal
{
    public class CountryDal : ICountryDal
    {
        private readonly string apiUrl = "/rpt/countries";
        private readonly IGenericMessageHelper genericMessageHelper;
        private readonly HttpClient httpClient;
        private readonly IHttpClientWrapper httpClientWrapper;
        private readonly IResponseStatusCodeHelper responseStatusCodeHelper;
        private readonly string route;
        private string conflictedMessage = string.Empty;

        public CountryDal(HttpClient httpClient,
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

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            var response = await httpClientWrapper.GetAsync(httpClient, route);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IEnumerable<Country>>();
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            var response = await httpClientWrapper.GetAsync(httpClient, $"{route}/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Country>();
        }

        public async Task<IEnumerable<Country>> GetActiveCountriesByRegionId(int regionId)
        {
            Console.WriteLine("ROUTE VARIABLE:" + route);
            var response = await httpClientWrapper.GetAsync(httpClient, $"{route}/regionId/{regionId}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IEnumerable<Country>>();
        }

        public async Task<Country> PostAsync(Country country)
        {
            var json = JsonConvert.SerializeObject(country);
            var postContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await httpClientWrapper.PostAsync(httpClient, route, postContent);

            if (!response.IsSuccessStatusCode)
            {
                await responseStatusCodeHelper.FailedResponseFromAPI(response, conflictedMessage);
            }

            return await response.Content.ReadAsAsync<Country>();
        }

        public async Task<Country> PutAsync(Country country)
        {
            var json = JsonConvert.SerializeObject(country);
            var putContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await httpClientWrapper.PutAsync(httpClient, $"{route}/{country.Id}", putContent);

            if (!response.IsSuccessStatusCode)
            {
                await responseStatusCodeHelper.FailedResponseFromAPI(response, conflictedMessage);
            }
            return await response.Content.ReadAsAsync<Country>();
        }
    }
}