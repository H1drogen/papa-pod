using Fdm.Ams.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services.Interfaces
{
    public interface IGlobalGeoflexService
    {
        public Task<IEnumerable<GlobalGeoflex>> GetAll();

        public Task<IEnumerable<GlobalGeoflex>> GetAllByConsultantId(int id);

        public Task<GlobalGeoflex> GetGlobalGeoflexForConsultant(int id);
    }
}