using System.Collections.Generic;
using System.Threading.Tasks;
using Fdm.Ams.Models;
using Fdm.Ams.ViewModels;

namespace Fdm.Ams.Services.Interfaces
{
    public interface IRegionService
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<Region>> GetAllAsync();

        Task<RegionViewModel> GetByIdAsync(int id);

        Task<RegionViewModel> PostAsync(RegionViewModel regionViewModel);

        Task<RegionViewModel> PutAsync(RegionViewModel regionViewModel);
    }
}