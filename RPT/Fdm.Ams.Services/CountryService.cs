using AutoMapper;
using Fdm.Ams.Dal;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fdm.Ams.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryDal countryDal;
        private readonly IMapper mapper;
        private readonly IRegionDal regionDal;

        public CountryService(ICountryDal countryDal, IRegionDal regionDal, IMapper mapper)
        {
            this.countryDal = countryDal;
            this.regionDal = regionDal;
            this.mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            await countryDal.DeleteAsync(id);
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            var countries = await countryDal.GetAllAsync();
            List<Region> regions = (await regionDal.GetAllAsync()).ToList();
            foreach(var country in countries)
            {
                country.RegionName = regions.Find(reg => reg.Id == country.RegionId).Name.ToString();
            }
            return countries;
        }

        public async Task<CountryViewModel> GetByIdAsync(int id)
        {
            return mapper.Map<CountryViewModel>(await countryDal.GetByIdAsync(id));
        }

        public async Task<CountryViewModel> GetCountryWithRegionList()
        {
            var regions = await regionDal.GetAllAsync();
            Dictionary<int, string> listOfRegions = new();
            regions.ToList().ForEach(x => listOfRegions.Add(x.Id, x.Name));

            var viewModel = new CountryViewModel()
            {
                RegionDictionaries = listOfRegions,
            };

            return viewModel;
        }

        public async Task<CountryViewModel> PostAsync(CountryViewModel countryViewModel)
        {
            var country = await countryDal.PostAsync(mapper.Map<Country>(countryViewModel));
            return mapper.Map<CountryViewModel>(country);
        }

        public async Task<CountryViewModel> PutAsync(CountryViewModel countryViewModel)
        {
            var country = await countryDal.PutAsync(mapper.Map<Country>(countryViewModel));
            return mapper.Map<CountryViewModel>(country);
        }

        public async Task<IEnumerable<Country>> GetActiveCountriesInRegion(int regionId)
        {
            Console.WriteLine("Inside country service");
            return mapper.Map<IEnumerable<Country>>(await countryDal.GetActiveCountriesByRegionId(regionId));
        }
    }
}