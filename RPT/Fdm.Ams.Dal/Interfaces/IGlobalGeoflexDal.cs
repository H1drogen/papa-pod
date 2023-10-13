using Fdm.Ams.Models;
using Fdm.Ams.Models.Post;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal.Interfaces
{
    public interface IGlobalGeoflexDal
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<GlobalGeoflex>> GetAllAsync();

        Task<GlobalGeoflex> GetByIdAsync(int id);

        Task<GlobalGeoflex> PostAsync(PostGlobalGeoflexDto postDto);

        Task PutAsync(GlobalGeoflex globalGeoflex);
    }
}