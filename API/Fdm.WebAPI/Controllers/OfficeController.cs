using Fdm.Common.Web;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.WebAPI.Controllers
{       
    /// <summary>
    ///  Api Controller for Office Data
    /// </summary>
    [ApiController]
    [Route("api/rpt/offices")]
    public class OfficeController : BaseController<IOfficeService, OfficeDto, PostOfficeDto>
    {
        private IOfficeService officeService;

        /// <summary>
        /// Constructor for OfficeController, passing in office service for office data manipulation
        /// </summary>
        /// <param name="officeService"></param>
        /// <param name="logger"></param>
        public OfficeController(IOfficeService officeService, ILogger<OfficeDto> logger)
            : base(officeService, logger)
        {
            this.officeService = officeService;
        }


        /// <summary>
        /// Method to retrieve a list of entities for given foreign key : countryId
        /// </summary>
        /// <param countryId="countryId">The foreign key relating offices to a country</param>
        /// <returns>NotFound when nothing is returned or a list of Dto</returns>
        // GET api/rpt/offices/countryId/<countryId>
        [HttpGet("countryId/{countryId:int}")]
        public async Task<IEnumerable<OfficeDto>> Get(int countryId)
        {
            try
            {
                logger.LogInformation($"Getting {countryId}");
                var dto = await this.officeService.GetActiveOfficesByCountryId(countryId);

                logger.LogInformation($"Got {dto}");

                if (dto == null)
                {
                    logger.LogError($"{countryId} not found");
                    return (IEnumerable<OfficeDto>)NotFound("Id doesn't exist.");
                }
                return dto;
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong: {ex}");
                return (IEnumerable<OfficeDto>)StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }

        }
    }
}