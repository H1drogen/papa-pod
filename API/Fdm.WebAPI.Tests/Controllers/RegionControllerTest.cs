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
    public class RegionControllerTest : BaseControllerTests<IRegionService, RegionDto, PostRegionDto>
    {
        /// <summary>
        /// Overriding the BaseControlerTestHelper method to return a RegionController
        /// so the generic tests will be run against the RegionController
        /// </summary>
        /// <param name="mockService"></param>
        /// <param name="mockLogger"></param>
        /// <returns>RegionController</returns>

        protected override BaseController<IRegionService, RegionDto, PostRegionDto> BaseControllerTestHelper(Mock<IRegionService> mockService, Mock<ILogger<RegionDto>> mockLogger)
        {
            return new RegionController(mockService.Object, mockLogger.Object);
        }
    }
}