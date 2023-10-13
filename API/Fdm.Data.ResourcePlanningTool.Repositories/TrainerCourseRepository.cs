using Fdm.Common.Repository;
using Fdm.Data.ResourcePlanningTool.Dal;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;

namespace Fdm.Data.ResourcePlanningTool.Repositories
{
    public class TrainerCourseRepository : GenericRepository<ResourcePlanningToolContext, Trainer_Course>, ITrainerCourseRepository
    {
        public TrainerCourseRepository(ResourcePlanningToolContext context) : base(context)
        {
        }
    }
}