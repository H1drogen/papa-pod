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
    public class CourseControllerTest : BaseControllerTests<ICourseService, CourseDto, PostCourseDto>
    {
        /// <summary>
        /// Overriding the BaseControlerTestHelper method to return a CourseController
        /// so the generic tests will be run against the CourseController
        /// </summary>
        /// <param name="mockService"></param>
        /// <param name="mockLogger"></param>
        /// <returns>CourseController</returns>

        protected override BaseController<ICourseService, CourseDto, PostCourseDto> BaseControllerTestHelper(Mock<ICourseService> mockService, Mock<ILogger<CourseDto>> mockLogger)
        {
            return new CourseController(mockService.Object, mockLogger.Object);
        }
    }
}