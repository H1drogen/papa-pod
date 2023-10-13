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
    /// in the BaseController Tests are run for the CourseTemplateVersionController
    /// </summary>
    public class PathwayTypeControllerTests : BaseControllerTests<IPathwayTypeService,
            PathwayTypeDto,
            PostPathwayTypeDto>
    {
        /// <summary>
        /// Overriding the BaseControllerTestHelper method to return a PathwayTypeController
        /// so the generic tests will be run against the PathwayTypeController
        /// </summary>
        /// <param name="mockService"></param>
        /// <param name="mockLogger"></param>
        /// <returns>PathwayTypeController</returns>

        protected override BaseController<IPathwayTypeService,
            PathwayTypeDto,
            PostPathwayTypeDto>
            BaseControllerTestHelper(Mock<IPathwayTypeService> mockService,
            Mock<ILogger<PathwayTypeDto>> mockLogger)
        {
            return new PathwayTypeController(mockService.Object, mockLogger.Object);
        }
    }
}