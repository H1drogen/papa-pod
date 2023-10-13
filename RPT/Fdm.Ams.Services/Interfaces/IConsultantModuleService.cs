using Fdm.Ams.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services.Interfaces
{
    public interface IConsultantModuleService
    {
        Task<IEnumerable<ConsultantModule>> GetAll();

        Task<IEnumerable<ConsultantModule>> GetAllByConsultantId(int id);

        Task<ConsultantModule> GetConsultantModulesForConsultant(int id);
    }
}