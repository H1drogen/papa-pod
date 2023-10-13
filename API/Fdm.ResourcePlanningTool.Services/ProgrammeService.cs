using AutoMapper;
using Fdm.Common.Service;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;

namespace Fdm.ResourcePlanningTool.Services
{
    public class ProgrammeService : GenericService<Programme,
        ProgrammeDto, PostProgrammeDto,
        IProgrammeRepository>, IProgrammeService
    {
        public ProgrammeService(IMapper mapper, IProgrammeRepository repo)
            : base(mapper, repo)
        {
        }
    }
}