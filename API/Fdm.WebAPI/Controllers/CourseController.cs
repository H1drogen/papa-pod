using Fdm.Common.Web;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.WebAPI.Controllers
{
    /// <summary>
    ///  Api Controller for Course Data
    /// </summary>
    [ApiController]
    [Route("api/rpt/courses")]
    public class CourseController : BaseController<ICourseService, CourseDto, PostCourseDto>
    {
        /// <summary>
        /// Constructor of CourseController, passing in the CourseService that will be called for data manipulation
        /// </summary>
        /// <param name="courseService"></param>
        /// <param name="logger"></param>

        public CourseController(ICourseService courseService, ILogger<CourseDto> logger) :
             base(courseService, logger)
        {
        }
    }
}