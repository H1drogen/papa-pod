using Fdm.Common.Service;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;

namespace Fdm.ResourcePlanningTool.Services.Interfaces
{
    public interface ITrainerService : IGenericService<TrainerDto, PostTrainerDto>
    {
    }
}