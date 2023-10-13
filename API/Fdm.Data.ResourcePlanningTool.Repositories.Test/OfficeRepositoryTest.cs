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
    /// in the GenericRepository Tests are run for the OfficeRepository
    /// </summary>

    public class OfficeRepositoryTest : GenericRepositoryTests<ResourcePlanningToolContext, Office>, IClassFixture<SharedDatabaseFixture<ResourcePlanningToolContext, Office>>
    {
        public OfficeRepositoryTest(SharedDatabaseFixture<ResourcePlanningToolContext, Office> fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Overriding the CreateRepository method to return a OfficeRepository
        /// so the generic tests will be run against the OfficeRepository
        /// </summary>
        /// <param name="context"></param>
        /// <returns>OfficeRepository</returns>

        protected override GenericRepository<ResourcePlanningToolContext, Office> CreateRepository(ResourcePlanningToolContext context)
        {
            return new OfficeRepository(context);
        }
    }
}