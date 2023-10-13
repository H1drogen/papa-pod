using AutoMapper;
using Fdm.Common.Service;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;

namespace Fdm.ResourcePlanningTool.Services
{
    public class TrainerCourseService : GenericService<Trainer_Course,
        TrainerCourseDto, PostTrainerCourseDto,
        ITrainerCourseRepository>, ITrainerCourseService
    {
        public TrainerCourseService(IMapper mapper, ITrainerCourseRepository repo)
            : base(mapper, repo)
        {
        }
    }
}