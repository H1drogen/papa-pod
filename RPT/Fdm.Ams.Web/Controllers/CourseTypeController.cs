using Fdm.Ams.Services;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.Ams.Web.Controllers
{
    public class CourseTypeController : Controller
    {
        private readonly ICourseTypeService courseTypeService;
        private readonly ILogger logger;

        public CourseTypeController(ICourseTypeService courseTypeService, ILogger<CourseTypeController> logger)
        {
            this.courseTypeService = courseTypeService;
            this.logger = logger;
        }

        [HttpPost]
        /*Microsoft Documentation recommends use of HttpPost for methods that make changes to data on the server.
        https://learn.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/examining-the-details-and-delete-methods*/
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var courseType = await courseTypeService.GetByIdAsync(id);
            await courseTypeService.DeleteAsync(id);
            TempData["SuccessMessage"] = $"{courseType.Name} deleted successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetAllAsync()
        {
            return Json(await courseTypeService.GetAllAsync());
        }

        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Json(await courseTypeService.GetByIdAsync(id));
        }

        public async Task<IActionResult> GetByIdForEditAsync(int id)
        {
            var courseType = await courseTypeService.GetByIdAsync(id);
            return PartialView("UpdateCourseType", courseType);
        }

        public async Task<IActionResult> Index()
        {
            logger.LogInformation("CourseTypeController - Index");
            var pathwayTypeDetails = await courseTypeService.GetAllAsync();
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(pathwayTypeDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostAsync(CourseTypeViewModel courseTypeViewModel)
        {
            await courseTypeService.PostAsync(courseTypeViewModel);
            TempData["SuccessMessage"] = $"{courseTypeViewModel.Name} created successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PutAsync(CourseTypeViewModel courseTypeViewModel)
        {
            await courseTypeService.PutAsync(courseTypeViewModel);
            TempData["SuccessMessage"] = $"{courseTypeViewModel.Name} updated successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var courseType = await courseTypeService.GetByIdAsync(id);
            return View(courseType);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}