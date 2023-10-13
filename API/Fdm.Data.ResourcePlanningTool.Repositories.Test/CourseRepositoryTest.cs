using Fdm.Common.Repository;
using Fdm.Common.TestHelpers;
using Fdm.Common.Tests.Repositories;
using Fdm.Data.ResourcePlanningTool.Dal;
using Fdm.Data.ResourcePlanningTool.Models;
using Xunit;

namespace Fdm.Data.ResourcePlanningTool.Repositories.Tests

{
    public class CourseRepositoryTest : GenericRepositoryTests<ResourcePlanningToolContext, Course>, IClassFixture<SharedDatabaseFixture<ResourcePlanningToolContext, Course>>
    {
        public CourseRepositoryTest(SharedDatabaseFixture<ResourcePlanningToolContext, Course> fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Overriding the CreateRepository method to return a CourseRepository
        /// so the generic tests will be run against the CourseRepository
        /// </summary>
        /// <param name="context"></param>
        /// <returns>CourseRepository</returns>

        protected override GenericRepository<ResourcePlanningToolContext, Course> CreateRepository(ResourcePlanningToolContext context)
        {
            return new CourseRepository(context);
        }
    }
}