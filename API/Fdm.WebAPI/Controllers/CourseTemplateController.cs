using Fdm.Common.Web;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.WebAPI.Controllers
{
    /// <summary>
    ///  Api Controller for CourseTemplate Data
    /// </summary>
    //[Authorize]
    [ApiController]
    [Route("api/rpt/course-templates")]
    public class CourseTemplateController : BaseController<ICourseTemplateService,
        CourseTemplateDto, PostCourseTemplateDto>
    {
        /// <summary>
        /// Constructor of CourseTemplateController, passing in the CourseTemplateService
        /// that will be called for data manipulation
        /// </summary>
        /// <param name="courseTemplateService"></param>
        /// <param name="logger"></param>
        public CourseTemplateController(ICourseTemplateService courseTemplateService,
            ILogger<CourseTemplateDto> logger)
            : base(courseTemplateService, logger)
        {
        }
    }
}