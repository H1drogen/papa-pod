using Fdm.Ams.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services.Interfaces
{
    public interface IHolidayService
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<HolidayViewModel>> GetAllAsync();

        Task<HolidayViewModel> GetByIdAsync(int id);

        Task<HolidayViewModel> GetCountries();

        Task<HolidayViewModel> PostAsync(HolidayViewModel holidayViewModel);

        Task<HolidayViewModel> PutAsync(HolidayViewModel holiday);
    }
}