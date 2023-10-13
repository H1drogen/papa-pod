using Fdm.Common.Web;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.WebAPI.Controllers
{
    /// <summary>
    ///  Api Controller for Region Data
    /// </summary>
    [ApiController]
    [Route("api/rpt/regions")]
    public class RegionController : BaseController<IRegionService, RegionDto, PostRegionDto>
    {
        /// <summary>
        /// Constructor of RegionController, passing in the RegionService that will be called for data manipulation
        /// </summary>
        /// <param name="regionService"></param>
        /// <param name="logger"></param>

        public RegionController(IRegionService regionService, ILogger<RegionDto> logger) :
             base(regionService, logger)
        {
        }
    }
}