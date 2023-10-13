using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.Ams.Web.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ICourseModuleService courseModuleService;
        private readonly ILogger logger;
        private readonly ITrainerService trainerService;

        public TrainerController(ICourseModuleService courseModuleService, ITrainerService trainerService, ILogger<TrainerController> logger)
        {
            this.courseModuleService = courseModuleService;
            this.logger = logger;
            this.trainerService = trainerService;
        }

        public async Task<IActionResult> CreateAsync()
        {
            var trainer = await trainerService.GetTrainerWithOfficeList();

            return View("Create", trainer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var trainer = await trainerService.GetByIdAsync(id);
            await trainerService.DeleteAsync(id);
            TempData["SuccessMessage"] = $"{trainer.FirstName} archived successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(TrainerViewModel trainerViewModel)
        {
            await trainerService.PutAsync(trainerViewModel);
            TempData["SuccessMessage"] = $"{trainerViewModel.FirstName} updated successfully";

            return RedirectToAction("Index");
        }

        

        public async Task<JsonResult> GetAllAsync()
        {
            logger.LogInformation("TrainerController - GetAllAsync");
            return Json(await trainerService.GetAllAsync());
        }
                
        [HttpGet]
        public async Task<IActionResult> GetByIdForEditAsync(int id)
        {
            var trainerOffices = await trainerService.GetTrainerWithOfficeList();
            var trainerViewModel = await trainerService.GetByIdAsync(id);
            trainerViewModel.OfficeDictionaries = trainerOffices.OfficeDictionaries;

            return PartialView("UpdateTrainer", trainerViewModel);
        }
        

        [HttpGet]
         public async Task<IActionResult> Details(int id)
        {
            var trainerOffice = await trainerService.GetTrainerWithOfficeList();
            var trainerViewModel = await trainerService.GetByIdAsync(id);
            trainerViewModel.OfficeDictionaries = trainerOffice.OfficeDictionaries;

            return View("Details", trainerViewModel);
        }

        public async Task<IActionResult> Index()
        {
            logger.LogInformation("TrainerController - Index");
            var viewModel = await trainerService.GetAllAsync();
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(TrainerViewModel trainerViewModel)
        {
            await trainerService.PostAsync(trainerViewModel);
            TempData["SuccessMessage"] = $"{trainerViewModel.FirstName} created successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> TrainerSchedule()
        {
            logger.LogInformation("TrainerController - TrainerSchedule");
            var courseModules = (await courseModuleService.GetAllAsync()).Where(x => x.TrainerId == null);
            ViewBag.SuccessMessageTrainerSchedule = TempData["SuccessMessageTrainerSchedule"];
            return View(courseModules);
        }
    }
}