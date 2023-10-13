using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace Fdm.Ams.Web.Controllers
{
    public class OfficeController : Controller
    {
        private readonly IOfficeService officeService;
        private readonly ILogger logger;

        public OfficeController(IOfficeService officeService, ILogger<OfficeController> logger)
        {
            this.officeService = officeService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Json(await officeService.GetAllAsync());
        }

        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Json(await officeService.GetByIdAsync(id));
        }
 

        public IActionResult Index()
        {
            logger.LogInformation("OfficeController - Index");
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostAsync(OfficeViewModel officeViewModel)
        {
            await officeService.PostAsync(officeViewModel);
            TempData["SuccessMessage"] = $"{officeViewModel.Name} created successfully";
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> CreateAsync()
        {
            var office = await officeService.GetOfficeWithCountryList();
            return PartialView("_CreateOffice", office);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(OfficeViewModel officeViewModel)
        {
            await officeService.PutAsync(officeViewModel);
            TempData["SuccessMessage"] = $"{officeViewModel.Name} updated successfully";

            return RedirectToAction("Index");
        }
     

        [HttpGet]
        public async Task<IActionResult> GetByIdForEditAsync(int id)
        {
            var officeCountries = await officeService.GetOfficeWithCountryList();
            var officeViewModel = await officeService.GetByIdAsync(id);
            officeViewModel.CountriesDictionary = officeCountries.CountriesDictionary;

            return PartialView("_UpdateOffice", officeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var office = await officeService.GetByIdAsync(id);
            await officeService.DeleteAsync(id);
            TempData["SuccessMessage"] = $"{office.Name} archived successfully";
            return RedirectToAction("Index");
        }

    }
}
