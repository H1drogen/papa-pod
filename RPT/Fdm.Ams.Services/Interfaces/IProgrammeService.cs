using Fdm.Ams.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services.Interfaces
{
    public interface IProgrammeService
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<ProgrammeViewModel>> GetAllAsync();

        Task<ProgrammeViewModel> GetByIdAsync(int id);

        Task<ProgrammeViewModel> PostAsync(ProgrammeViewModel programmeViewModel);

        Task<ProgrammeViewModel> PutAsync(ProgrammeViewModel programmeViewModel);
    }
}