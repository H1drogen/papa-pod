using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.Ams.Web.Controllers
{
    public class TrainerCourseController : Controller
    {
        private readonly ILogger logger;
        private readonly ITrainerCourseService trainerCourseService;

        public TrainerCourseController(ITrainerCourseService trainerCourseService, ILogger<TrainerCourseController> logger)
        {
            this.trainerCourseService = trainerCourseService;
            this.logger = logger;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        /*Microsoft Documentation recommends use of HttpPost for methods that make changes to data on the server.
       https://learn.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/examining-the-details-and-delete-methods*/
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await trainerCourseService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetAllAsync()
        {
            return Json(await trainerCourseService.GetAllAsync());
        }

        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Json(await trainerCourseService.GetByIdAsync(id));
        }

        public IActionResult Index()
        {
            logger.LogInformation("TrainerCourseController - Index");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostAsync(TrainerCourse trainerCourse)
        {
            await trainerCourseService.PostAsync(trainerCourse);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        /*Microsoft Documentation recommends use of HttpPost for methods that make changes to data on the server.
       https://learn.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/examining-the-details-and-delete-methods*/
        public async Task<IActionResult> PutAsync(TrainerCourse trainerCourse)
        {
            await trainerCourseService.PutAsync(trainerCourse);
            return RedirectToAction("Index");
        }
    }
}