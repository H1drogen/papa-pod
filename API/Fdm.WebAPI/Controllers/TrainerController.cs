using Fdm.Common.Web;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.WebAPI.Controllers
{
    /// <summary>
    ///  Api Controller for Trainer Data
    /// </summary>
    [ApiController]
    [Route("api/rpt/trainers")]
    public class TrainerController : BaseController<ITrainerService, TrainerDto, PostTrainerDto>
    {
        /// <summary>
        /// Constructor of TrainerController, passing in the TrainerService that will be called for data manipulation
        /// </summary>
        /// <param name="trainerService"></param>
        /// <param name="logger"></param>
        public TrainerController(ITrainerService trainerService, ILogger<TrainerDto> logger) : 
            base(trainerService, logger)
        {

        }
    }
}