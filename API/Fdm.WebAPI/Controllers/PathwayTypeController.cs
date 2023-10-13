using Fdm.Common.Web;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.WebAPI.Controllers
{
    /// <summary>
    ///  Api Controller for PathwayType Data
    /// </summary>
    //[Authorize]
    [ApiController]
    [Route("api/rpt/pathway-types")]
    public class PathwayTypeController : BaseController<IPathwayTypeService,
        PathwayTypeDto,
        PostPathwayTypeDto>
    {
        /// <summary>
        /// Constructor of PathwayTypeController, passing in the PathwayTypeService that will
        /// be called for data manipulation.
        /// </summary>
        /// <param name="PathwayTypeService"></param>
        /// <param name="logger"></param>
        public PathwayTypeController(IPathwayTypeService PathwayTypeService, ILogger<PathwayTypeDto> logger)
            : base(PathwayTypeService, logger)
        {
        }
    }
}