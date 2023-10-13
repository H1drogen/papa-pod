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
    public class OfficeControllerTest : BaseControllerTests<IOfficeService, OfficeDto, PostOfficeDto>
    {
        /// <summary>
        /// Overriding the BaseControlerTestHelper method to return a OfficeController
        /// so the generic tests will be run against the OfficeController
        /// </summary>
        /// <param name="mockService"></param>
        /// <param name="mockLogger"></param>
        /// <returns>OfficeController</returns>

        protected override BaseController<IOfficeService, OfficeDto, PostOfficeDto> BaseControllerTestHelper(Mock<IOfficeService> mockService, Mock<ILogger<OfficeDto>> logger)
        {
            return new OfficeController(mockService.Object, logger.Object);
        }
    }
}