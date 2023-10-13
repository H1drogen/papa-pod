using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fdm.Ams.Services
{
    public class ConsultantModuleService : IConsultantModuleService
    {
        private readonly IConsultantModuleDal consultantModuleDal;

        public ConsultantModuleService(IConsultantModuleDal consultantModuleDal)
        {
            this.consultantModuleDal = consultantModuleDal;
        }

        public async Task<IEnumerable<ConsultantModule>> GetAll()
        {
            return await consultantModuleDal.GetAllAsync();
        }

        public async Task<IEnumerable<ConsultantModule>> GetAllByConsultantId(int id)
        {
            return (await consultantModuleDal.GetAllAsync()).Where(x => x.ConsultantId == id);
        }

        public async Task<ConsultantModule> GetConsultantModulesForConsultant(int id)
        {
            return (await consultantModuleDal.GetAllAsync()).First(x => x.ConsultantId == id);
        }
    }
}