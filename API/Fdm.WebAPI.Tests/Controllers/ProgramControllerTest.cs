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
    /// in the BaseController Tests are run for the ProgrammeController
    /// </summary>
    public class ProgramControllerTest : BaseControllerTests<IProgrammeService,
        ProgrammeDto, PostProgrammeDto>
    {
        /// <summary>
        /// Overriding the BaseControlerTestHelper method to return a ProgrammeController
        /// so the generic tests will be run against the ProgrammeController
        /// </summary>
        /// <param name="mockService"></param>
        /// <param name="mockLogger"></param>
        /// <returns>ProgrammeController</returns>
        protected override BaseController<IProgrammeService,
            ProgrammeDto, PostProgrammeDto>
            BaseControllerTestHelper(Mock<IProgrammeService> mockService,
            Mock<ILogger<ProgrammeDto>> mockLogger)
        {
            return new ProgrammeController(mockService.Object, mockLogger.Object);
        }
    }
}