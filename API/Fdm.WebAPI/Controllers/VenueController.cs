using Fdm.Common.Web;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fdm.WebAPI.Controllers
{
    /// <summary>
    ///  Api Controller for Venue Data
    /// </summary>
    ///
    [ApiController]
    [Route("api/rpt/venues")]
    public class VenueController : BaseController<IVenueService, VenueDto, PostVenueDto>
    {

        private IVenueService venueService;

        /// <summary>
        /// Constructor of VenueController, passing in the VenueService that will be called for data manipulation
        /// </summary>
        /// <param name="VenueService"></param>
        /// <param name="logger"></param>
        public VenueController(IVenueService venueService, ILogger<VenueDto> logger) :
            base(venueService, logger)
        {
            this.venueService = venueService;
        }


        /// <summary>
        /// Method to retrieve a list of venues for given foreign key : officeId
        /// </summary>
        /// <param officeId="officeId">The foreign key relating venues to a office</param>
        /// <returns>NotFound when nothing is returned or a list of Dto</returns>
        // GET api/rpt/venues/officeId/<officeID>
        [HttpGet("officeId/{officeId:int}")]
        public async Task<IEnumerable<VenueDto>> Get(int officeId)
        {
            try
            {
                logger.LogInformation($"Getting {officeId}");
                var dto = await this.venueService.GetActiveVenuesByOfficeId(officeId);

                logger.LogInformation($"Got {dto}");

                if (dto == null)
                {
                    logger.LogError($"{officeId} not found");
                    return (IEnumerable<VenueDto>)NotFound("Id doesn't exist.");
                }
                return dto;
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong: {ex}");
                return (IEnumerable<VenueDto>)StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }

        }
    }
}