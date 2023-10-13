using Fdm.Ams.Models;
using Fdm.Ams.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services.Interfaces
{
    public interface ICountryService
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<Country>> GetAllAsync();

        Task<CountryViewModel> GetByIdAsync(int id);

        Task<CountryViewModel> GetCountryWithRegionList();

        Task<CountryViewModel> PostAsync(CountryViewModel countryViewModel);

        Task<CountryViewModel> PutAsync(CountryViewModel countryViewModel);

        public Task<IEnumerable<Country>> GetActiveCountriesInRegion(int regionID);
    }
}