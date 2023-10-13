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
    public class HolidayService : IHolidayService
    {
        private readonly ICountryDal countryDal;
        private readonly IHolidayDal holidayDal;
        private readonly IMapper mapper;

        public HolidayService(IHolidayDal holidayDal, IMapper mapper, ICountryDal countryDal)
        {
            this.holidayDal = holidayDal;
            this.mapper = mapper;
            this.countryDal = countryDal;
        }

        public async Task DeleteAsync(int id)
        {
            await holidayDal.DeleteAsync(id);
        }

        public async Task<IEnumerable<HolidayViewModel>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<HolidayViewModel>>(await holidayDal.GetAllAsync());
        }

        public async Task<HolidayViewModel> GetByIdAsync(int id)
        {
            return mapper.Map<HolidayViewModel>(await holidayDal.GetByIdAsync(id));
        }

        public async Task<HolidayViewModel> GetCountries()
        {
            var country = await countryDal.GetAllAsync();
            List<Country> countryAre = new List<Country>();
            List<Holiday> holidayAre = new List<Holiday>();
            var holidays = await holidayDal.GetAllAsync();
            holidayAre = holidays.ToList();
            Dictionary<int, string> countryDictionary = new();
            country.ToList().ForEach(x => countryDictionary.Add(x.Id, x.Name));
            countryAre = country.ToList();
            foreach (var holidayIs in holidayAre)
            {
                holidayIs.CountriesName = countryAre.Find(x => x.Id == holidayIs.CountryId).Name.ToString();
            }

            var viewModel = new HolidayViewModel()
            {
                CountryDictionary = countryDictionary,
                Holidays=holidayAre,
                Countries =countryAre
            };
            return viewModel;
        }

        public async Task<HolidayViewModel> PostAsync(HolidayViewModel holidayViewModel)
        {
            var holiday = await holidayDal.PostAsync(mapper.Map<Holiday>(holidayViewModel));
            return mapper.Map<HolidayViewModel>(holiday);
        }

        public async Task<HolidayViewModel> PutAsync(HolidayViewModel holidayViewModel)
        {
            var holiday = await holidayDal.PutAsync(mapper.Map<Holiday>(holidayViewModel));
            return mapper.Map<HolidayViewModel>(holiday);
        }
    }
}