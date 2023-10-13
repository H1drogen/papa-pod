using Fdm.Ams.Models;
using Fdm.Ams.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services.Interfaces
{
    public interface IOfficeService
    {
        public Task<IEnumerable<OfficeViewModel>> GetAllAsync();
        Task DeleteAsync(int id);

        public Task<OfficeViewModel> GetByIdAsync(int id);

        public Task<OfficeViewModel> GetOfficeWithCountryList();
        public Task<OfficeViewModel> PostAsync(OfficeViewModel officeViewModel);

        public Task<List<Office>> GetActiveOfficesInACountry(int countryID);


        Task<OfficeViewModel> PutAsync(OfficeViewModel officeViewModel);

    }
}