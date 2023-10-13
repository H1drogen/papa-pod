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
    /// in the BaseController Tests are run for the TrainerCourseController
    /// </summary>
    public class TrainerCourseControllerTest : BaseControllerTests<ITrainerCourseService,
        TrainerCourseDto, PostTrainerCourseDto>
    {
        /// <summary>
        /// Overriding the BaseControlerTestHelper method to return a TrainerCourseController
        /// so the generic tests will be run against the TrainerCourseController
        /// </summary>
        /// <param name="mockService"></param>
        /// <param name="mockLogger"></param>
        /// <returns>TrainerCourseController</returns>
        protected override BaseController<ITrainerCourseService,
            TrainerCourseDto, PostTrainerCourseDto>
            BaseControllerTestHelper(Mock<ITrainerCourseService> mockService,
            Mock<ILogger<TrainerCourseDto>> mockLogger)
        {
            return new TrainerCourseController(mockService.Object, mockLogger.Object);
        }
    }
}