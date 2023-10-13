using Fdm.Ams.Models;
using Fdm.Ams.Models.Post;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal.Interfaces
{
    public interface IConsultantModuleDal
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<ConsultantModule>> GetAllAsync();

        Task<ConsultantModule> GetByIdAsync(int id);

        Task<ConsultantModule> PostAsync(PostConsultantModuleDto postDto);

        Task PutAsync(ConsultantModule consultantModule);
    }
}