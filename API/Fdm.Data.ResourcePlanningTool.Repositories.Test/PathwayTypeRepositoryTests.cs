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
    /// in the GenericRepository Tests are run for the PathwayTypeRepository
    /// </summary>
    public class PathwayTypeRepositoryTests : GenericRepositoryTests<ResourcePlanningToolContext,
        PathwayType>,
        IClassFixture<SharedDatabaseFixture<ResourcePlanningToolContext, PathwayType>>
    {
        public PathwayTypeRepositoryTests(SharedDatabaseFixture<ResourcePlanningToolContext,
            PathwayType> fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Overriding the CreateRepository method to return a PathwayTypeRepository
        /// so the generic tests will be run against the PathwayTypeRepository
        /// </summary>
        /// <param name="context"></param>
        /// <returns>PathwayTypeRepository</returns>
        protected override GenericRepository<ResourcePlanningToolContext, PathwayType> CreateRepository(ResourcePlanningToolContext context)
        {
            return new PathwayTypeRepository(context);
        }
    }
}