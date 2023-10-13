using Fdm.Common.Web;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.WebAPI.Controllers
{
    /// <summary>
    ///  Api Controller for Programme Data
    /// </summary>
    [ApiController]
    [Route("api/rpt/programmes")]
    public class ProgrammeController : BaseController<IProgrammeService, ProgrammeDto, PostProgrammeDto>
    {
        /// <summary>
        /// Constructor of ProgrammeController, passing in the ProgrammeService that will be called for data manipulation
        /// </summary>
        /// <param name="ProgrammeService"></param>
        /// <param name="logger"></param>
        ///
        public ProgrammeController(IProgrammeService ProgrammeService, ILogger<ProgrammeDto> logger) : base(ProgrammeService, logger)
        {
        }
    }
}