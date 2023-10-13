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
    /// in the GenericRepository Tests are run for the TrainerCourseRepository
    /// </summary>

    public class TrainerCourseRepositoryTest : GenericRepositoryTests<ResourcePlanningToolContext, Trainer_Course>, IClassFixture<SharedDatabaseFixture<ResourcePlanningToolContext, Trainer_Course>>
    {
        public TrainerCourseRepositoryTest(SharedDatabaseFixture<ResourcePlanningToolContext, Trainer_Course> fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Overriding the CreateRepository method to return a TrainerCourseRepository
        /// so the generic tests will be run against the TrainerCourseRepository
        /// </summary>
        /// <param name="context"></param>
        /// <returns>TrainerCourseRepository</returns>

        protected override GenericRepository<ResourcePlanningToolContext, Trainer_Course> CreateRepository(ResourcePlanningToolContext context)
        {
            return new TrainerCourseRepository(context);
        }
    }
}