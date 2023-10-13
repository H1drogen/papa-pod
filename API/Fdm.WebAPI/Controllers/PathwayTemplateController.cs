using Fdm.Common.Web;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.WebAPI.Controllers
{
    /// <summary>
    ///  Api Controller for PathwayTemplate Data
    /// </summary>
    //[Authorize]
    [ApiController]
    [Route("api/rpt/pathway-templates")]
    public class PathwayTemplateController : BaseController<IPathwayTemplateService, PathwayTemplateDto, PostPathwayTemplateDto>
    {
        /// <summary>
        /// Constructor of PathwayTemplateController, passing in the PathwayTemplateService that will be called for data manipulation
        /// </summary>
        /// <param name="PathwayTemplateService"></param>
        /// <param name="logger"></param>
        public PathwayTemplateController(IPathwayTemplateService PathwayTemplateService, ILogger<PathwayTemplateDto> logger) :
            base(PathwayTemplateService, logger)
        {
        }
    }
}