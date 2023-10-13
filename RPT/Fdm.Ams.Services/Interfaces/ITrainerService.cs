using Fdm.Ams.Models;
using Fdm.Ams.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services.Interfaces
{
    public interface ITrainerService
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<Trainer>> GetAllAsync();

        Task<TrainerViewModel> GetByIdAsync(int id);

        Task<TrainerViewModel> GetTrainerWithOfficeList();

        Task<TrainerViewModel> PostAsync(TrainerViewModel trainerViewModel);

        Task<TrainerViewModel> PutAsync(TrainerViewModel trainerViewModel);
    }
}