using Fdm.Common.Repository;
using Fdm.Common.TestHelpers;
using Fdm.Common.Tests.Repositories;
using Fdm.Data.ResourcePlanningTool.Dal;
using Fdm.Data.ResourcePlanningTool.Models;
using Xunit;

namespace Fdm.Data.ResourcePlanningTool.Repositories.Tests
{
    /// <summary>
    /// This class inherits from the GenericRepository Tests so all the test defined
    /// in the GenericRepository Tests are run for the VenueRepository
    /// </summary>
    public class VenueRepositoryTests : GenericRepositoryTests<ResourcePlanningToolContext, Venue>, IClassFixture<SharedDatabaseFixture<ResourcePlanningToolContext, Venue>>
    {
        public VenueRepositoryTests(SharedDatabaseFixture<ResourcePlanningToolContext, Venue> fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Overriding the CreateRepository method to return a VenueRepository
        /// so the generic tests will be run against the VenueRepository
        /// </summary>
        /// <param name="context"></param>
        /// <returns>VenueRepository</returns>
        protected override GenericRepository<ResourcePlanningToolContext,
            Venue>
            CreateRepository(ResourcePlanningToolContext context)
        {
            return new VenueRepository(context);
        }
    }
}