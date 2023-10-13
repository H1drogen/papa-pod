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
    /// in the GenericRepository Tests are run for the TrainerRoleRepository
    /// </summary>

    public class TrainerRoleRepositoryTest : GenericRepositoryTests<ResourcePlanningToolContext, TrainerRole>, IClassFixture<SharedDatabaseFixture<ResourcePlanningToolContext, TrainerRole>>
    {
        public TrainerRoleRepositoryTest(SharedDatabaseFixture<ResourcePlanningToolContext, TrainerRole> fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Overriding the CreateRepository method to return a TrainerRoleRepository
        /// so the generic tests will be run against the TrainerRoleRepository
        /// </summary>
        /// <param name="context"></param>
        /// <returns>TrainerRoleRepository</returns>

        protected override GenericRepository<ResourcePlanningToolContext, TrainerRole> CreateRepository(ResourcePlanningToolContext context)
        {
            return new TrainerRoleRepository(context);
        }
    }
}