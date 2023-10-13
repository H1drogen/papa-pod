using Fdm.Common.Web;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.WebAPI.Controllers
{
    /// <summary>
    ///  Api Controller for Pathway Data
    /// </summary>
    [ApiController]
    [Route("api/rpt/pathway")]
    public class PathwayController : BaseController<IPathwayService, PathwayDto, PostPathwayDto>
    {
        /// <summary>
        /// Constructor of PathwayController, passing in the PathwayService that will be called for data manipulation
        /// </summary>
        /// <param name="PathwayService"></param>
        /// <param name="logger"></param>
        ///
        public PathwayController(IPathwayService PathwayService, ILogger<PathwayDto> logger) : base(PathwayService, logger)
        {
        }
    }
}