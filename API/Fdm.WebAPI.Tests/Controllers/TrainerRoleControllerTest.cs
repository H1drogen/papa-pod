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
    /// in the BaseController Tests are run for the TrainerRoleController
    /// </summary>
    public class TrainerRoleControllerTest : BaseControllerTests<ITrainerRoleService,
        TrainerRoleDto, PostTrainerRoleDto>
    {
        /// <summary>
        /// Overriding the BaseControlerTestHelper method to return a TrainerRoleController
        /// so the generic tests will be run against the TrainerRoleController
        /// </summary>
        /// <param name="mockService"></param>
        /// <param name="mockLogger"></param>
        /// <returns>TrainerRoleController</returns>
        protected override BaseController<ITrainerRoleService,
            TrainerRoleDto, PostTrainerRoleDto>
            BaseControllerTestHelper(Mock<ITrainerRoleService> mockService,
            Mock<ILogger<TrainerRoleDto>> mockLogger)
        {
            return new TrainerRoleController(mockService.Object, mockLogger.Object);
        }
    }
}