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
    public class VenueControllerTest : BaseControllerTests<IVenueService, VenueDto, PostVenueDto>
    {
        protected override BaseController<IVenueService, VenueDto, PostVenueDto> BaseControllerTestHelper(Mock<IVenueService> service, Mock<ILogger<VenueDto>> logger)
        {
            return new VenueController(service.Object, logger.Object);
        }
    }
}