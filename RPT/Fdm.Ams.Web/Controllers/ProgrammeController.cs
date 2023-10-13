using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.Ams.Web.Controllers
{
    public class ProgrammeController : Controller
    {
        private readonly ILogger logger;
        private readonly IProgrammeService programmeService;

        public ProgrammeController(IProgrammeService programmeService, ILogger<ProgrammeController> logger)
        {
            this.programmeService = programmeService;
            this.logger = logger;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var programme = await programmeService.GetByIdAsync(id);
            await programmeService.DeleteAsync(id);
            TempData["SuccessMessage"] = $"{programme.Name} archived successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(ProgrammeViewModel viewModel)
        {
            await programmeService.PutAsync(viewModel);
            TempData["SuccessMessage"] = $"{viewModel.Name} updated successfully";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Json(await programmeService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Json(await programmeService.GetByIdAsync(id));
        }

        public async Task<IActionResult> GetByIdForEditAsync(int id)
        {
            var viewModel = await programmeService.GetByIdAsync(id);
            return PartialView("UpdateProgramme", viewModel);
        }

        public async Task<IActionResult> Index()
        {
            logger.LogInformation("ProgrammeController - Index");
            var programmeDetails = await programmeService.GetAllAsync();
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(programmeDetails);
        }

        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await programmeService.GetByIdAsync(id);
            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostAsync(ProgrammeViewModel programmeViewModel)
        {
            await programmeService.PostAsync(programmeViewModel);
            TempData["SuccessMessage"] = $"{programmeViewModel.Name} created successfully";
            return RedirectToAction("Index");
        }
    }
}