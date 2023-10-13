using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;

namespace Fdm.Ams.Web.Controllers
{
    public class VenueController : Controller
    {
        private readonly ICourseModuleService courseModuleService;
        private readonly ILogger logger;
        private readonly IVenueService venueService;
        private readonly IRegionService regionService;
        private readonly ICountryService countryService;
        private readonly IOfficeService officeService;

        public VenueController(ILogger<VenueController> logger, IVenueService venueService, ICourseModuleService courseModuleService, IRegionService regionService, IOfficeService officeService, ICountryService countryService)
        {
            this.logger = logger;
            this.venueService = venueService;
            this.courseModuleService = courseModuleService;
            this.regionService = regionService;
            this.officeService = officeService;
            this.countryService = countryService;
        }

        public async Task<IActionResult> GetAll()
        {
            return Json(await venueService.GetAllAsync());
        }

        public IActionResult Index()
        {
            logger.LogInformation("VenueController - Index");
            return View();
        }

        public async Task<IActionResult> VenueSchedule()
        {
            logger.LogInformation("VenueController - VenueSchedule");
            var info = (await courseModuleService.GetAllAsync()).Where(x => x.VenueId == null);

            // Added the below lines to pull activeRegions

            var allRegions = await regionService.GetAllAsync();
           // var allCountries = await countryService.GetAllAsync();
            //Dictionary<string, List<Country>> regionCountries = new Dictionary<string, List<Country>>();

            List<string> activeRegions = new List<string>();

            //Filtering all activeRegions
            foreach (var region in allRegions)
            {
                if (region.IsActive)
                {
                    activeRegions.Add(region.Id+"."+region.Name);
                }

            }


            ViewData["ActiveRegions"] = activeRegions;

            return View(info);
        }

       /* public async Task<IEnumerable<Country>> GetActiveCountriesByRegion(int regionId)
        {
            //Getting Countries list for region
            var allActiveCountriesInRegion = await countryService.GetActiveCountriesInRegion(regionId);

            return allActiveCountriesInRegion;
        }


        public async Task<IEnumerable<OfficeViewModel>> GetActiveOfficesByCountry(int countryId)
        {
            //Getting Offices list for country
            var allActiveOfficesInCountry = await officeService.GetActiveOfficesInACountry(countryId);

 
           return allActiveOfficesInCountry;
        }

        public async Task<IEnumerable<Venue>> GetActiveVenuesByOffice(int officeId)
        {
            //Getting venues list for office
            var allActiveVenuesInOffice = await venueService.GetActiveVenuesInOffice(officeId);

            return allActiveVenuesInOffice;
        }
        */
        public async Task<IActionResult> GetActiveVenuesByRegionId(int regionID)
        {
            Console.WriteLine("Region ID: " + regionID);
            var allCountriesByRegionId = await countryService.GetActiveCountriesInRegion(regionID);
            List<Office> allOfficesInRegion = new List<Office>();
            List<Venue> allVenuesInRegion = new List<Venue>();
            Console.WriteLine("Inside VenueController's getactiveVenuesByregionID()");

            foreach (var country in allCountriesByRegionId)
            {
                var officesInOneCountry = await officeService.GetActiveOfficesInACountry(country.Id);
                foreach(var office in officesInOneCountry)
                {
                    allOfficesInRegion.Add(office);
                }
                
             }
            foreach (var office in allOfficesInRegion)
            {
                var venuesInOneOffice = await venueService.GetActiveVenuesInOffice(office.Id);
                foreach (var venue in venuesInOneOffice)
                {
                    allVenuesInRegion.Add(venue);
                }
            }

            return Json(allVenuesInRegion);
        }








    }
}