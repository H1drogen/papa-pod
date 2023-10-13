using AutoMapper;
using Fdm.Common.Service;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;

namespace Fdm.ResourcePlanningTool.Services
{
    public class PathwayService : GenericService<Pathway,
        PathwayDto, PostPathwayDto,
        IPathwayRepository>, IPathwayService
    {
        public PathwayService(IMapper mapper, IPathwayRepository repo)
            : base(mapper, repo)
        {
        }
    }
}