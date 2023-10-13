using Fdm.Common.Service;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;

namespace Fdm.ResourcePlanningTool.Services.Interfaces
{
    public interface IRegionService : IGenericService<RegionDto, PostRegionDto>
    {
        /*   Task<IEnumerable<RegionDto>> GetActiveRegionsForUser(string userObjectId);

           Task<IEnumerable<RegionDto>> GetAll();

           Task<IEnumerable<RegionDto>> GetAllActive();

           Task<IEnumerable<RegionDto>> GetRegionsForUser(int id);*/
    }
}