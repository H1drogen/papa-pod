using Fdm.Common.Repository;
using Fdm.Data.ResourcePlanningTool.Dal;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;

namespace Fdm.Data.ResourcePlanningTool.Repositories
{
    public class VenueRepository : GenericRepository<ResourcePlanningToolContext, Venue>, IVenueRepository
    {
        public VenueRepository(ResourcePlanningToolContext context) : base(context)
        {
        }
    }
}