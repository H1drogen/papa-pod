using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.Ams.Web.Controllers
{
    public class HolidayController : Controller
    {
        private readonly IHolidayService holidayService;
        private readonly ILogger logger;

        public HolidayController(IHolidayService holidayService, ILogger<HolidayController> logger)
        {
            this.holidayService = holidayService;
            this.logger = logger;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var holiday = await holidayService.GetByIdAsync(id);
            await holidayService.DeleteAsync(id);
            TempData["SuccessMessage"] = $"{holiday.Name} deleted successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(HolidayViewModel holidayViewModel)
        {
            await holidayService.PutAsync(holidayViewModel);
            TempData["SuccessMessage"] = $"{holidayViewModel.Name} updated successfully";
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public async Task<IActionResult> Details()
        //{
        //    return Json(await holidayService.GetAllAsync());
        //}


        //[HttpGet]
        //public async Task<IActionResult> Details(int id)
        //{
        //    return Json(await holidayService.GetByIdAsync(id));
        //}

        public async Task<IActionResult> Details(int id)
        {
            var holidayViewModel = await holidayService.GetByIdAsync(id);

            var holidayCountries = await holidayService.GetCountries();
            holidayViewModel.CountryDictionary = holidayCountries.CountryDictionary;

            holidayViewModel.CountriesName = holidayViewModel.CountryDictionary[holidayViewModel.CountryId].ToString();

            return View("Details", holidayViewModel);
        }

        public async Task<IActionResult> Index()
        {
            logger.LogInformation("HolidayController - Index");
            var countryDetails = await holidayService.GetCountries();
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(countryDetails.Holidays);
        }

        [HttpPost]
        // GET: Holiday/Create
        public async Task<IActionResult> Create(HolidayViewModel holidayViewModel)
        {
            await holidayService.PostAsync(holidayViewModel);
            TempData["SuccessMessage"] = $"{holidayViewModel.Name} created successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            List<Country> countryDDLOf = new List<Country>();
            var holidayCountries = await holidayService.GetCountries();
            countryDDLOf = holidayCountries.Countries.ToList();
            ViewBag.CountryDDL = countryDDLOf;
            return View();
        }

            [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostAsync(HolidayViewModel holidayViewModel)
        {
            await holidayService.PostAsync(holidayViewModel);
            TempData["SuccessMessage"] = $"{holidayViewModel.Name} created successfully";
            return RedirectToAction("Index");
        }
    }
}