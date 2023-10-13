using Fdm.Ams.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal.Interfaces
{
    public interface IHolidayDal
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<Holiday>> GetAllAsync();

        Task<Holiday> GetByIdAsync(int id);

        Task<Holiday> PostAsync(Holiday holiday);

        Task<Holiday> PutAsync(Holiday holiday);
    }
}