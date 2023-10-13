using Fdm.Common.Web;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.WebAPI.Controllers
{
    /// <summary>
    ///  Api Controller for TrainerCourse Data
    /// </summary>
    [ApiController]
    [Route("api/rpt/trainer-course")]
    public class TrainerCourseController : BaseController<ITrainerCourseService, TrainerCourseDto, PostTrainerCourseDto>
    {
        /// <summary>
        /// Constructor of TrainerCourseController, passing in the TrainerCourseService that will be called for data manipulation
        /// </summary>
        /// <param name="trainerCourseService"></param>
        /// <param name="logger"></param>
        ///
        public TrainerCourseController(ITrainerCourseService trainerCourseService, ILogger<TrainerCourseDto> logger) : base(trainerCourseService, logger)
        {
        }
    }
}