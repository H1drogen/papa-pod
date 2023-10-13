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
    /// in the GenericRepository Tests are run for the CourseTemplateRepository
    /// </summary>
    public class CourseTemplateRepositoryTests : GenericRepositoryTests<ResourcePlanningToolContext,
        CourseTemplate>,
        IClassFixture<SharedDatabaseFixture<ResourcePlanningToolContext,
            CourseTemplate>>
    {
        public CourseTemplateRepositoryTests(SharedDatabaseFixture<ResourcePlanningToolContext,
            CourseTemplate> fixture)
            : base(fixture)
        {
        }

        /// <summary>
        /// Overriding the CreateRepository method to return a CourseTemplateRepository
        /// so the generic tests will be run against the CourseTemplateRepository
        /// </summary>
        /// <param name="context"></param>
        /// <returns>CourseTemplateRepository</returns>
        protected override GenericRepository<ResourcePlanningToolContext,
            CourseTemplate> CreateRepository(ResourcePlanningToolContext context)
        {
            return new CourseTemplateRepository(context);
        }
    }
}