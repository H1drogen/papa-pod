using AutoMapper;
using Fdm.Common.Service;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;

namespace Fdm.ResourcePlanningTool.Services
{
    public class PathwayTemplateService : GenericService<PathwayTemplate, PathwayTemplateDto, PostPathwayTemplateDto, IPathwayTemplateRepository>, IPathwayTemplateService
    {
        public PathwayTemplateService(IMapper mapper, IPathwayTemplateRepository repo)
            : base(mapper, repo)
        {
        }
    }
}