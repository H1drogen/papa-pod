using AutoMapper;
using Fdm.Common.Service;
using Fdm.Common.Tests.Services;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services;
using Moq;

namespace Fdm.ResourcePlainningTool.Services.Tests
{
    /// <summary>
    /// Class for all the Test for the PathwayTemplate Service tests.
    /// First level is for any custom methods specific to the PathwayTempalteService and NOT inherited from the GenericServiceTest
    /// </summary>
    public class PathwayTemplateServiceTests
    {
        /// <summary>
        /// This class inherits from the GenericService Tests so all the test defined
        /// in the GenericService Tests are run for the PathwayTemplateService
        /// </summary>
        public class PathwayTemplateGenericServiceTests : GenericServiceTest<PathwayTemplate,
            PathwayTemplateDto,
            PostPathwayTemplateDto,
            IPathwayTemplateRepository>
        {
            /// <summary>
            /// Overriding the GenericServiceTestHelper method to return a PathwayTemplateService
            /// so the generic tests will be run against the PathwayTemplateService
            /// </summary>
            /// <param name="mockMapper"></param>
            /// <param name="mockRepo"></param>
            /// <returns>PathwayTemplateService</returns>
            protected override GenericService<PathwayTemplate,
                PathwayTemplateDto,
                PostPathwayTemplateDto,
                IPathwayTemplateRepository>
                GenericServiceTestHelper(Mock<IMapper> mockMapper, Mock<IPathwayTemplateRepository> mockRepo)
            {
                var PathwayTemplateRepo = mockRepo.As<IPathwayTemplateRepository>();
                return new PathwayTemplateService(mockMapper.Object, PathwayTemplateRepo.Object);
            }
        }
    }
}