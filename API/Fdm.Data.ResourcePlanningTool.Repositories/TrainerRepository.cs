using Fdm.Common.Repository;
using Fdm.Data.ResourcePlanningTool.Dal;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;

namespace Fdm.Data.ResourcePlanningTool.Repositories
{
    public class TrainerRepository : GenericRepository<ResourcePlanningToolContext, Trainer>, ITrainerRepository
    {
        public TrainerRepository(ResourcePlanningToolContext context) : base(context)
        {
        }
    }
}