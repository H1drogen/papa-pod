using AutoMapper;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fdm.Ams.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly ICountryDal countryDal;
        private readonly IMapper mapper;
        private readonly IOfficeDal officeDal;

        public OfficeService(IOfficeDal officeDal,
            IMapper mapper, ICountryDal countryDal)
        {
            this.officeDal = officeDal;
            this.mapper = mapper;
            this.countryDal = countryDal;
        }

        public async Task DeleteAsync(int id)
        {
            await officeDal.DeleteAsync(id);
        }

        public async Task<IEnumerable<OfficeViewModel>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<OfficeViewModel>>(await officeDal.GetAllAsync());
        }

        public async Task<OfficeViewModel> GetByIdAsync(int id)
        {
            return mapper.Map<OfficeViewModel>(await officeDal.GetByIdAsync(id));
        }

        public async Task<List<Office>> GetActiveOfficesInACountry(int countryId)
        {
            return mapper.Map<List<Office>>(await officeDal.GetActiveOfficesByCountryID(countryId));
        }

        public async Task<OfficeViewModel> GetOfficeWithCountryList()
        {
            var countries = await countryDal.GetAllAsync();
            Dictionary<int, string> listOfCountries = new();
            countries.ToList().ForEach(x => listOfCountries.Add(x.Id, x.Name));

            var viewModel = new OfficeViewModel()
            {
                CountriesDictionary = listOfCountries,
            };

            return viewModel;
        }



        public async Task<OfficeViewModel> PostAsync(OfficeViewModel officeViewModel)
        {
            return mapper.Map<OfficeViewModel>(await officeDal.PostAsync(mapper.Map<Office>(officeViewModel)));
        }

        public async Task<OfficeViewModel> PutAsync(OfficeViewModel officeViewModel)
        {
            return mapper.Map<OfficeViewModel>(await officeDal.PutAsync(mapper.Map<Office>(officeViewModel)));
        }
    }
}