using Fdm.Common.Repository;
using Fdm.Common.TestHelpers;
using Fdm.Common.Tests.Repositories;
using Fdm.Data.ResourcePlanningTool.Dal;
using Fdm.Data.ResourcePlanningTool.Models;
using Xunit;

namespace Fdm.Data.ResourcePlanningTool.Repositories.Tests
{
    /// <summary>
    /// This class inherits from the GenericRepository Tests so all the tests defined
    /// in the GenericRepository Tests are run for the ProgramRepository
    /// </summary>
    public class ProgrammeRepositoryTest : GenericRepositoryTests<ResourcePlanningToolContext,
        Programme>, IClassFixture<SharedDatabaseFixture<ResourcePlanningToolContext, Programme>>
    {
        public ProgrammeRepositoryTest(SharedDatabaseFixture<ResourcePlanningToolContext,
            Programme> fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Overriding the CreateRepository method to return a ProgramRepository
        /// so the generic tests will be run against the ProgramRepository
        /// </summary>
        /// <param name="context"></param>
        /// <returns>ProgramRepository</returns>
        protected override GenericRepository<ResourcePlanningToolContext, Programme>
            CreateRepository(ResourcePlanningToolContext context)
        {
            return new ProgrammeRepository(context);
        }
    }
}