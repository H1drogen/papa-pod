using Fdm.Common.Web;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.WebAPI.Controllers

{
    /// <summary>
    ///  Api Controller for Holiday Data
    /// </summary>
    [ApiController]
    [Route("api/rpt/holidays")]
    public class HolidayController : BaseController<IHolidayService, HolidayDto, PostHolidayDto>
    {
        /// <summary>
        /// Constructor for HolidayController, passing in Holiday service for Holiday data manipulation
        /// </summary>
        /// <param name="holidayService"></param>
        /// <param name="logger"></param>
        public HolidayController(IHolidayService holidayService, ILogger<HolidayDto> logger)
            : base(holidayService, logger)
        {
        }
    }
}