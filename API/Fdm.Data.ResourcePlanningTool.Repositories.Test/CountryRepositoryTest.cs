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
    /// in the GenericRepository Tests are run for the CountryRepository
    /// </summary>

    public class CountryRepositoryTest : GenericRepositoryTests<ResourcePlanningToolContext, Country>, IClassFixture<SharedDatabaseFixture<ResourcePlanningToolContext, Country>>
    {
        public CountryRepositoryTest(SharedDatabaseFixture<ResourcePlanningToolContext, Country> fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Overriding the CreateRepository method to return a CountryRepository
        /// so the generic tests will be run against the CountryRepository
        /// </summary>
        /// <param name="context"></param>
        /// <returns>CountryRepository</returns>

        protected override GenericRepository<ResourcePlanningToolContext, Country> CreateRepository(ResourcePlanningToolContext context)
        {
            return new CountryRepository(context);
        }
    }
}