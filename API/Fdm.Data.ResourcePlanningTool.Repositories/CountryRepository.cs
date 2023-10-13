using Fdm.Common.Repository;
using Fdm.Data.ResourcePlanningTool.Dal;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;

namespace Fdm.Data.ResourcePlanningTool.Repositories
{
    public class CountryRepository : GenericRepository<ResourcePlanningToolContext, Country>, ICountryRepository
    {
        public CountryRepository(ResourcePlanningToolContext context) : base(context)
        {
        }
    }
}