using Fdm.Common.Web;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.WebAPI.Controllers
{
    /// <summary>
    /// Api Controller for Country data
    /// </summary>
    [ApiController]
    [Route("api/rpt/countries")]
    public class CountryController : BaseController<ICountryService, CountryDto, PostCountryDto>
    {
        private ICountryService countryService;
        /// <summary>
        /// Constructor of CountryController, passing in the CountryService,logger that will be called for data manipulation
        ///
        /// </summary>
        /// <param name="countryService"></param>
        /// <param name="logger"></param>

        public CountryController(ICountryService countryService,
            ILogger<CountryDto> logger)

             : base(countryService, logger)
        {
            this.countryService = countryService;
        }

        /// <summary>
        /// Method to retrieve a list of entities for given foreign key : regionId
        /// </summary>
        /// <param regionId="regionId">The foreign key relating countries to a region</param>
        /// <returns>NotFound when nothing is returned or a list of Dto</returns>
        // GET api/rpt/countries/regionId/<regionID>
        [HttpGet("regionId/{regionId:int}")]
        public async Task<IEnumerable<CountryDto>> Get(int regionId)
        {
            try
            {
                logger.LogInformation($"Getting {regionId}");
                var dto = await this.countryService.GetActiveCountriesByRegionId(regionId);

                logger.LogInformation($"Got {dto}");

                if (dto == null)
                {
                    logger.LogError($"{regionId} not found");
                    return (IEnumerable<CountryDto>)NotFound("Id doesn't exist.");
                }
                return dto;
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong: {ex}");
                return (IEnumerable<CountryDto>)StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }

        }
    }
}