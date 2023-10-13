using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.Ams.Web.Controllers
{
    public class TrainerRoleController : Controller
    {
        private readonly ILogger logger;
        private readonly ITrainerRoleService trainerRoleService;

        public TrainerRoleController(ITrainerRoleService trainerRoleService, ILogger<TrainerRoleController> logger)
        {
            this.trainerRoleService = trainerRoleService;
            this.logger = logger;
        }

        public ActionResult CreateAsync()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var trainerRole = await trainerRoleService.GetByIdAsync(id);
            await trainerRoleService.DeleteAsync(id);
            TempData["SuccessMessage"] = $"{trainerRole.Name} deleted successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(TrainerRoleViewModel trainerRoleViewModel)
        {
            await trainerRoleService.PutAsync(trainerRoleViewModel);
            TempData["SuccessMessage"] = $"{trainerRoleViewModel.Name} updated successfully";

            return RedirectToAction("Index");
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Json(await trainerRoleService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Json(await trainerRoleService.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdForEditAsync(int id)
        {
            var trainerRoleViewModel = await trainerRoleService.GetByIdAsync(id);

            return PartialView("_UpdateTrainerRole", trainerRoleViewModel);
        }

        /*
        [HttpGet]
        public async Task<IActionResult> details(int id)
        {
            var trainerRoleViewModel = await trainerRoleService.GetByIdAsync(id);
            
            return View(trainerRoleViewModel);
        }*/

       //[HttpGet]
       /* public IActionResult Create()
        {
            return View();
        }*/


        public async Task<IActionResult> Index()
        {
            logger.LogInformation("TrainerRoleController - Index");
            var viewModels = await trainerRoleService.GetAllAsync();
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(viewModels);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostAsync(TrainerRoleViewModel trainerRoleViewModel)
        {
            await trainerRoleService.PostAsync(trainerRoleViewModel);
            TempData["SuccessMessage"] = $"{trainerRoleViewModel.Name} created successfully";
            return RedirectToAction("Index");
        }
    }
}