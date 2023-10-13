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
    /// in the GenericRepository Tests are run for the TrainerRepository
    /// </summary>
    public class TrainerRepositoryTest : GenericRepositoryTests<ResourcePlanningToolContext, Trainer>, IClassFixture<SharedDatabaseFixture<ResourcePlanningToolContext, Trainer>>
    {
        public TrainerRepositoryTest(SharedDatabaseFixture<ResourcePlanningToolContext, Trainer> fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Overriding the CreateRepository method to return a TrainerRepository
        /// so the generic tests will be run against the TrainerRepository
        /// </summary>
        /// <param name="context"></param>
        /// <returns>TrainerRepository</returns>
        protected override GenericRepository<ResourcePlanningToolContext,
            Trainer>
            CreateRepository(ResourcePlanningToolContext context)
        {
            return new TrainerRepository(context);
        }
    }
}