using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.Ams.Web.Controllers
{
    public class RegionController : Controller
    {
        private readonly ILogger logger;
        private readonly IRegionService regionService;

        public RegionController(IRegionService regionService, ILogger<RegionController> logger)
        {
            this.regionService = regionService;
            this.logger = logger;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var region = await regionService.GetByIdAsync(id);
            await regionService.DeleteAsync(id);
            TempData["SuccessMessage"] = $"{region.Name} deleted successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(RegionViewModel regionViewModel)
        {
            await regionService.PutAsync(regionViewModel);
            TempData["SuccessMessage"] = $"{regionViewModel.Name} updated successfully";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Json(await regionService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Json(await regionService.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdForEditAsync(int id)
        {
            var regionViewModel = await regionService.GetByIdAsync(id);

            return PartialView("_UpdateRegion", regionViewModel);
        }

        public async Task<IActionResult> Index()
        {
            logger.LogInformation("RegionController - Index");
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            var regionDetails = await regionService.GetAllAsync();
            return View(regionDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostAsync(RegionViewModel regionViewModel)
        {
            await regionService.PostAsync(regionViewModel);

            TempData["SuccessMessage"] = $"{regionViewModel.Name} created successfully";

            return RedirectToAction("Index");
        }
    }
}