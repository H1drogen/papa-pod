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
    /// in the BaseController Tests are run for the CourseTemplateController
    /// </summary>
    public class CourseTemplateControllerTests : BaseControllerTests<ICourseTemplateService,
        CourseTemplateDto, PostCourseTemplateDto>
    {
        /// <summary>
        /// Overriding the BaseControllerTestHelper method to return a CourseTemplateController
        /// so the generic tests will be run against the CourseTemplateController
        /// </summary>
        /// <param name="mockService"></param>
        /// <param name="mockLogger"></param>
        /// <returns>CourseTemplateController</returns>
        protected override BaseController<ICourseTemplateService,
            CourseTemplateDto, PostCourseTemplateDto>
            BaseControllerTestHelper(Mock<ICourseTemplateService> mockService,
            Mock<ILogger<CourseTemplateDto>> mockLogger)
        {
            return new CourseTemplateController(mockService.Object, mockLogger.Object);
        }
    }
}