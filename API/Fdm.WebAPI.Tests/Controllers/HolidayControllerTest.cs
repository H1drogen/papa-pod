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
    public class HolidayControllerTest : BaseControllerTests<IHolidayService, HolidayDto, PostHolidayDto>
    {
        /// <summary>
        /// Overriding the BaseControlerTestHelper method to return a HolidayController
        /// so the generic tests will be run against the HolidayController
        /// </summary>
        /// <param name="mockService"></param>
        /// <param name="mockLogger"></param>
        /// <returns>HolidayController</returns>

        protected override BaseController<IHolidayService, HolidayDto, PostHolidayDto> BaseControllerTestHelper(Mock<IHolidayService> mockService, Mock<ILogger<HolidayDto>> logger)
        {
            return new HolidayController(mockService.Object, logger.Object);
        }
    }
}