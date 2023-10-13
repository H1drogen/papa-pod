using Fdm.Ams.Models;
using Fdm.Ams.Services;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.Ams.Web.Controllers
{
    public class CourseModuleTemplateController : Controller
    {
        private readonly ICourseModuleTemplateService courseModuleTemplateService;
        private readonly ILogger logger;

        public CourseModuleTemplateController(ICourseModuleTemplateService courseModuleTemplateService, ILogger<CourseModuleTemplateController> logger)
        {
            this.logger = logger;
            this.courseModuleTemplateService = courseModuleTemplateService;
        }

        public async Task DeleteAsync(int id)
        {
            await courseModuleTemplateService.DeleteAsync(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(CourseModuleTemplateViewModel viewModel)
        {
            await courseModuleTemplateService.PutAsync(viewModel);
            TempData["SuccessMessage"] = $"{viewModel.Name} updated successfully";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetAllAsync()
        {
            return Json(await courseModuleTemplateService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Json(await courseModuleTemplateService.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdForEditAsync(int id)
        {
            var viewModel = await courseModuleTemplateService.GetByIdAsync(id);
            return PartialView("_UpdateCourseModuleTemplate", viewModel);
        }

        public async Task<IActionResult> GetByNullPathwayTemplateIdAsync()
        {
            return Json(await courseModuleTemplateService.GetAllByNullPathwayTemplateIdAsync());
        }

        public async Task<IActionResult> GetByPathwayTemplateIdAsync(int id)
        {
            return Json(await courseModuleTemplateService.GetAllByPathwayTemplateIdAsync(id));
        }

        /*public IActionResult Index()
        {
            logger.LogInformation("CourseModuleTemplateController - Index");
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View();
        }*/

        public async Task<IActionResult> Index()
        {
            logger.LogInformation("CourseModuleTemplateController - Index");
            var courseModuleTemplateDetails = await courseModuleTemplateService.GetAllAsync();
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(courseModuleTemplateDetails);
        }

        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await courseModuleTemplateService.GetByIdAsync(id);
            return View("Details", viewModel);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CourseModuleTemplateViewModel viewModel)
        {
            await courseModuleTemplateService.PostAsync(viewModel);
            TempData["SuccessMessage"] = $"{viewModel.Name} created successfully";
            return RedirectToAction("Index");
        }
    }
}