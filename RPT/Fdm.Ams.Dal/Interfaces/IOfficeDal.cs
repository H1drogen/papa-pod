using Fdm.Ams.Models;
using Fdm.Ams.Models.Post;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal.Interfaces
{
    public interface IOfficeDal
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<Office>> GetAllAsync();

        Task<Office> GetByIdAsync(int id);

        Task<Office> PostAsync(Office office);

        Task<Office> PutAsync(Office office);

        Task<IEnumerable<Office>> GetActiveOfficesByCountryID(int countryId);
    }
}