using Fdm.Common.Web;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.WebAPI.Controllers
{
    /// <summary>
    ///  Api Controller for TrainerRole Data
    /// </summary>
    [ApiController]
    [Route("api/rpt/trainer-roles")]
    public class TrainerRoleController : BaseController<ITrainerRoleService, TrainerRoleDto, PostTrainerRoleDto>
    {
        /// <summary>
        /// Constructor of TrainerRoleController, passing in the TrainerRoleService that will be called for data manipulation
        /// </summary>
        /// <param name="TrainerRoleService"></param>
        /// <param name="logger"></param>
        ///
        public TrainerRoleController(ITrainerRoleService TrainerRoleService, ILogger<TrainerRoleDto> logger) : base(TrainerRoleService, logger)
        {
        }
    }
}