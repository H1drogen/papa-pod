using Fdm.Common.Repository;
using Fdm.Common.TestHelpers;
using Fdm.Common.Tests.Repositories;
using Fdm.Data.ResourcePlanningTool.Dal;
using Fdm.Data.ResourcePlanningTool.Models;
using Xunit;

namespace Fdm.Data.ResourcePlanningTool.Repositories.Tests
{
    /// <summary>
    /// Class for all the Test for the PathwayTemplate Repository tests.
    /// First level is for any custom methods specific to the PathwayTempalteRepository and NOT inherited from the GenericRepositoryTests
    /// </summary>
    public class PathwayTemplateRepositoryTest
    {
        /// <summary>
        /// This class inherits from the GenericRepository Tests so all the test defined
        /// in the GenericRepository Tests are run for the PathwayTemplateRepository
        /// </summary>
        public class PathwayTemplateGenericRepositoryTest : GenericRepositoryTests<ResourcePlanningToolContext, PathwayTemplate>, IClassFixture<SharedDatabaseFixture<ResourcePlanningToolContext, PathwayTemplate>>
        {
            public PathwayTemplateGenericRepositoryTest(SharedDatabaseFixture<ResourcePlanningToolContext, PathwayTemplate> fixture) : base(fixture)
            {
            }

            /// <summary>
            /// Overriding the CreateRepository method to return a PathwayTemplateRepository
            /// so the generic tests will be run against the CoursModuleTempalteRepository
            /// </summary>
            /// <param name="context"></param>
            /// <returns>PathwayTemplateRepository</returns>
            protected override GenericRepository<ResourcePlanningToolContext, PathwayTemplate> CreateRepository(ResourcePlanningToolContext context)
            {
                return new PathwayTemplateRepository(context);
            }
        }
    }
}