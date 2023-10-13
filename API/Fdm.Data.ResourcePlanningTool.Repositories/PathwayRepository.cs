using Fdm.Common.Repository;
using Fdm.Data.ResourcePlanningTool.Dal;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;

namespace Fdm.Data.ResourcePlanningTool.Repositories
{
    public class PathwayRepository : GenericRepository<ResourcePlanningToolContext, Pathway>, IPathwayRepository
    {
        public PathwayRepository(ResourcePlanningToolContext context) : base(context)
        {
        }
    }
}