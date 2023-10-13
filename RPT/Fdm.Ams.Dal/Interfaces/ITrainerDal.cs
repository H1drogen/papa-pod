using Fdm.Ams.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal.Interfaces
{
    public interface ITrainerDal
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<Trainer>> GetAllAsync();

        Task<Trainer> GetByIdAsync(int id);

        Task<Trainer> PostAsync(Trainer trainer);

        Task<Trainer> PutAsync(Trainer trainer);
    }
}