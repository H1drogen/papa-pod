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
    /// This class inherits from the BaseController Tests so all the test defined
    /// in the BaseController Tests are run for the PathwayController
    /// </summary>
    public class PathwayControllerTest : BaseControllerTests<IPathwayService,
        PathwayDto, PostPathwayDto>
    {
        /// <summary>
        /// Overriding the BaseControlerTestHelper method to return a PathwayController
        /// so the generic tests will be run against the PathwayController
        /// </summary>
        /// <param name="mockService"></param>
        /// <param name="mockLogger"></param>
        /// <returns>PathwayController</returns>
        protected override BaseController<IPathwayService,
            PathwayDto, PostPathwayDto>
            BaseControllerTestHelper(Mock<IPathwayService> mockService,
            Mock<ILogger<PathwayDto>> mockLogger)
        {
            return new PathwayController(mockService.Object, mockLogger.Object);
        }
    }
}