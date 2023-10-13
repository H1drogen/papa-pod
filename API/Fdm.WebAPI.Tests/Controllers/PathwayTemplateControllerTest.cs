using Fdm.Common.Tests.Web;
using Fdm.Common.Web;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;
using Fdm.WebAPI.Controllers;
using Microsoft.Extensions.Logging;
using Moq;

namespace Fdm.WebAPI.Tests.Controllers
{
    /// <summary>
    /// Class for all the Test for the PathwayTemplate Controller tests.
    /// First level is for any custom methods specific to the PathwayTempalteController and NOT inherited from the BaseControllerTests
    /// </summary>
    public class PathwayTemplateControllerTests
    {
        /// <summary>
        /// This class inherits from the BaseController Tests so all the test defined
        /// in the BaseController Tests are run for the PathwayTemplateController
        /// </summary>
        public class PathwayTemplateBaseControllerTests : BaseControllerTests<IPathwayTemplateService, PathwayTemplateDto, PostPathwayTemplateDto>
        {
            /// <summary>
            /// Overriding the BaseControlerTestHelper method to return a PathwayTemplateController
            /// so the generic tests will be run against the CoursModuleTempalteController
            /// </summary>
            /// <param name="mockService"></param>
            /// <param name="mockLogger"></param>
            /// <returns>PathwayTemplateController</returns>

            protected override BaseController<IPathwayTemplateService, PathwayTemplateDto, PostPathwayTemplateDto>
                BaseControllerTestHelper(Mock<IPathwayTemplateService> mockService, Mock<ILogger<PathwayTemplateDto>> mockLogger)
            {
                return new PathwayTemplateController(mockService.Object, mockLogger.Object);
            }
        }
    }
}