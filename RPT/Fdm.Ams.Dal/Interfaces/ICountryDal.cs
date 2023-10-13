using Fdm.Ams.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal.Interfaces
{
    public interface ICountryDal
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<Country>> GetAllAsync();

        Task<Country> GetByIdAsync(int id);

        Task<Country> PostAsync(Country country);

        Task<Country> PutAsync(Country country);

        Task<IEnumerable<Country>> GetActiveCountriesByRegionId(int regionId);
    }
}