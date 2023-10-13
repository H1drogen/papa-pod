using System.Collections.Generic;
using System.Threading.Tasks;
using Fdm.Ams.Models;

namespace Fdm.Ams.Dal.Interfaces
{
    public interface IRegionDal
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<Region>> GetAllAsync();

        Task<Region> GetByIdAsync(int id);

        Task<Region> PostAsync(Region region);

        Task<Region> PutAsync(Region region);
    }
}