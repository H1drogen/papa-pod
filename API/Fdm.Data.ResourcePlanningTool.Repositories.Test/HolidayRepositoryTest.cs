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
    /// in the GenericRepository Tests are run for the HolidayRepository
    /// </summary>

    public class HolidayRepositoryTest : GenericRepositoryTests<ResourcePlanningToolContext, Holiday>, IClassFixture<SharedDatabaseFixture<ResourcePlanningToolContext, Holiday>>
    {
        public HolidayRepositoryTest(SharedDatabaseFixture<ResourcePlanningToolContext, Holiday> fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Overriding the CreateRepository method to return a HolidayRepository
        /// so the generic tests will be run against the HolidayRepository
        /// </summary>
        /// <param name="context"></param>
        /// <returns>HolidayRepository</returns>

        protected override GenericRepository<ResourcePlanningToolContext, Holiday> CreateRepository(ResourcePlanningToolContext context)
        {
            return new HolidayRepository(context);
        }
    }
}