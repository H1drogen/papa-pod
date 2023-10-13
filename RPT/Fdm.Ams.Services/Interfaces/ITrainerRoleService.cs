using Fdm.Ams.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services.Interfaces
{
    public interface ITrainerRoleService
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<TrainerRoleViewModel>> GetAllAsync();

        Task<TrainerRoleViewModel> GetByIdAsync(int id);

        Task<TrainerRoleViewModel> PostAsync(TrainerRoleViewModel trainerRoleViewModel);

        Task<TrainerRoleViewModel> PutAsync(TrainerRoleViewModel trainerRoleViewModel);
    }
}