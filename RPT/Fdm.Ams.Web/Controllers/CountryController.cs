using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Fdm.Ams.Web.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService countryService;
        private readonly ILogger logger;

        public CountryController(ICountryService countryService, ILogger<CountryController> logger)
        {
            this.countryService = countryService;
            this.logger = logger;
        }

        public async Task<IActionResult> CreateAsync()
        {
            var country = await countryService.GetCountryWithRegionList();

            return View("Create", country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var country = await countryService.GetByIdAsync(id);
            await countryService.DeleteAsync(id);
            TempData["SuccessMessage"] = $"{country.Name} archived successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(CountryViewModel countryViewModel)
        {
            await countryService.PutAsync(countryViewModel);
            TempData["SuccessMessage"] = $"{countryViewModel.Name} updated successfully";

            return RedirectToAction("Index");
        }

        /*
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Json(await countryService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Json(await countryService.GetByIdAsync(id));
        }*/

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var countryRegions = await countryService.GetCountryWithRegionList();
            var countryViewModel = await countryService.GetByIdAsync(id);
            countryViewModel.RegionDictionaries = countryRegions.RegionDictionaries;

            return View("Details", countryViewModel);
        }

        public async Task<IActionResult> Index()
        {
            logger.LogInformation("CountryController - Index");
            var viewModel = await countryService.GetAllAsync();
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CountryViewModel countryViewModel)
        {
            await countryService.PostAsync(countryViewModel);
            TempData["SuccessMessage"] = $"{countryViewModel.Name} created successfully";
            return RedirectToAction("Index");
        }
    }
}