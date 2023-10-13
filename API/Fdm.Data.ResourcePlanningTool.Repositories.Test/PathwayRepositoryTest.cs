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
    /// in the GenericRepository Tests are run for the PathwayRepository
    /// </summary>
    public class PathwayRepositoryTest : GenericRepositoryTests<ResourcePlanningToolContext,
        Pathway>, IClassFixture<SharedDatabaseFixture<ResourcePlanningToolContext, Pathway>>
    {
        public PathwayRepositoryTest(SharedDatabaseFixture<ResourcePlanningToolContext,
            Pathway> fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Overriding the CreateRepository method to return a PathwayRepository
        /// so the generic tests will be run against the PathwayRepository
        /// </summary>
        /// <param name="context"></param>
        /// <returns>PathwayRepository</returns>
        protected override GenericRepository<ResourcePlanningToolContext, Pathway>
            CreateRepository(ResourcePlanningToolContext context)
        {
            return new PathwayRepository(context);
        }
    }
}