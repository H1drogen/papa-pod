using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fdm.Ams.Services
{
    public class GlobalGeoflexService : IGlobalGeoflexService
    {
        private readonly IGlobalGeoflexDal globalGeoflexDal;

        public GlobalGeoflexService(IGlobalGeoflexDal globalGeoflexDal)
        {
            this.globalGeoflexDal = globalGeoflexDal;
        }

        public async Task<IEnumerable<GlobalGeoflex>> GetAll()
        {
            return await globalGeoflexDal.GetAllAsync();
        }

        public async Task<IEnumerable<GlobalGeoflex>> GetAllByConsultantId(int id)
        {
            return (await globalGeoflexDal.GetAllAsync()).Where(x => x.ConsultantId == id);
        }

        public async Task<GlobalGeoflex> GetGlobalGeoflexForConsultant(int id)
        {
            return (await globalGeoflexDal.GetAllAsync()).First(x => x.ConsultantId == id);
        }
    }
}