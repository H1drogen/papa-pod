using AutoMapper;
using Fdm.Common.Service;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;

namespace Fdm.ResourcePlanningTool.Services
{
    public class RegionService : GenericService<Region, RegionDto, PostRegionDto,
        IRegionRepository>, IRegionService
    {
        private readonly IMapper mapper;
        private readonly IRegionRepository repo;

        public RegionService(IRegionRepository repo,
            IMapper mapper) : base(mapper, repo)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        /*     public async Task<IEnumerable<RegionDto>> GetActiveRegionsForUser(string userObjectId)
             {
                 var activeRegions = userRegionPermissionRepository.GetEntities(ur => ur.User.ObjectId == userObjectId && !ur.RemovalDate.HasValue
                     && ur.Region.IsActive)
                     .Select(ur => ur.Region)
                     .Distinct();
                 var regions = (await repo.GetAllAsync()).Where(region => region.IsActive);
                 return mapper.Map<IEnumerable<RegionDto>>(activeRegions);
             }

             public override async Task<IEnumerable<RegionDto>> GetAll()
             {
                 var regions = (await (repo.GetAllAsync())).OrderBy(r => r.Name);
                 return mapper.Map<IEnumerable<RegionDto>>(regions);
             }

             public async Task<IEnumerable<RegionDto>> GetAllActive()
             {
                 var activeRegions = await repo.GetEntitiesAsync(w => w.IsActive);
                 return mapper.Map<IEnumerable<RegionDto>>(activeRegions);
             }

             public async Task<IEnumerable<RegionDto>> GetRegionsForUser(int id)
             {
                 var regionsForUser = userRegionPermissionRepository
                     .GetEntities(ur => ur.User.Id == id && !ur.RemovalDate.HasValue)
                     .Select(ur => ur.Region)
                     .Distinct();

                 return mapper.Map<IEnumerable<RegionDto>>(regionsForUser);
             }*/
    }
}