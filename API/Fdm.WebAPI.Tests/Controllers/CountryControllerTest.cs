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
    public class CountryControllerTest : BaseControllerTests<ICountryService, CountryDto, PostCountryDto>
    {
        /// <summary>
        /// Overriding the BaseControlerTestHelper method to return a CountryController
        /// so the generic tests will be run against the CountryController
        /// </summary>
        /// <param name="mockService"></param>
        /// <param name="mockLogger"></param>
        /// <returns>CountryController</returns>

        protected override BaseController<ICountryService, CountryDto, PostCountryDto> BaseControllerTestHelper(Mock<ICountryService> mockService, Mock<ILogger<CountryDto>> logger)
        {
            return new CountryController(mockService.Object, logger.Object);
        }
    }
}