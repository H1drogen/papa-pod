using Fdm.Ams.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal.Interfaces
{
    public interface ITrainerRoleDal
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<TrainerRole>> GetAllAsync();

        Task<TrainerRole> GetByIdAsync(int id);

        Task<TrainerRole> PostAsync(TrainerRole trainerRole);

        Task<TrainerRole> PutAsync(TrainerRole trainerRole);
    }
}