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
    /// in the BaseController Tests are run for the TrainerController
    /// </summary>
    public class TrainerControllerTest : BaseControllerTests<ITrainerService, TrainerDto, PostTrainerDto>
    {
        /// <summary>
        /// Overriding the BaseControlerTestHelper method to return a TrainerController
        /// so the generic tests will be run against the TrainerController
        /// </summary>
        /// <param name="mockService"></param>
        /// <param name="mockLogger"></param>
        /// <returns>TrainerController</returns>

        protected override BaseController<ITrainerService, TrainerDto, PostTrainerDto>
            BaseControllerTestHelper(Mock<ITrainerService> mockService,
            Mock<ILogger<TrainerDto>> mockLogger)
        {
            return new TrainerController(mockService.Object, mockLogger.Object);
        }
    }
}