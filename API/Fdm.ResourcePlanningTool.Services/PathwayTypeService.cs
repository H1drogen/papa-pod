using AutoMapper;
using Fdm.Common.Service;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;

namespace Fdm.ResourcePlanningTool.Services
{
    public class PathwayTypeService : GenericService<PathwayType,
        PathwayTypeDto,
        PostPathwayTypeDto,
        IPathwayTypeRepository>,
        IPathwayTypeService
    {
        public PathwayTypeService(IMapper mapper, IPathwayTypeRepository repo)
            : base(mapper, repo)
        {
        }
    }
}