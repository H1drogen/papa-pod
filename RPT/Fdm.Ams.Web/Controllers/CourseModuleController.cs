using Fdm.Ams.Services;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Net;
using System.Web;

namespace Fdm.Ams.Web.Controllers
{
    public class CourseModuleController : Controller
    {
        private readonly ICourseModuleService courseModuleService;
        private readonly ILogger logger;

        public CourseModuleController(ILogger<CourseModuleController> logger, ICourseModuleService courseModuleService)
        {
            this.logger = logger;
            this.courseModuleService = courseModuleService;
        }

        public IActionResult CourseModuleSchedule()
        {
            logger.LogInformation("CourseModuleController - CourseModuleSchedule");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(CourseModuleViewModel courseModuleViewModel)
        {
            await courseModuleService.DeleteAsync(courseModuleViewModel.Id);
            TempData["SuccessMessageTrainerSchedule"] = $"{courseModuleViewModel.Name} deleted successfully";
            return RedirectToAction("TrainerSchedule", "Trainer");
        }

        /*
        [HttpPost]
        public async Task<IActionResult> DeleteAsyncForCourseSchedule(CourseModuleViewModel courseModuleViewModel)
        {
            await courseModuleService.DeleteAsync(courseModuleViewModel.Id);
            TempData["SuccessMesssage"] = $"{courseModuleViewModel.Name} deleted successfully";
            return RedirectToAction("CourseSchedule", "Course");
        }*/

        [HttpPost]
        public async Task<IActionResult> DeleteCourseModuleAsync(CourseModuleViewModel courseModuleViewModel)
        {
            await courseModuleService.DeleteAsync(courseModuleViewModel.Id);
            return Json(null);
        }

        
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            await courseModuleService.DeleteAsync(id);
            //Redirect(Request.Headers["Referer"].ToString());
            return Json(null);
        }
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(CourseModuleViewModel courseModuleViewModel)
        {
            await courseModuleService.PutAsync(courseModuleViewModel);
            TempData["SuccessMessageTrainerSchedule"] = $"{courseModuleViewModel.Name} updated successfully";
            return RedirectToAction("TrainerSchedule", "Trainer");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsyncForCourseSchedule(CourseModuleViewModel courseModuleViewModel)
        {
            await courseModuleService.PutAsync(courseModuleViewModel);
            TempData["SuccessMesssage"] = $"{courseModuleViewModel.Name} updated successfully";
            return RedirectToAction("CourseSchedule", "Course");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUnAssignAsync(CourseModuleViewModel courseModuleViewModel)
        {
            await courseModuleService.PutUnAssignAsync(courseModuleViewModel);
            TempData["SuccessMessageTrainerSchedule"] = $"{courseModuleViewModel.Name} Unassigned successfully";
            return RedirectToAction("TrainerSchedule", "Trainer");
        } */

        public async Task<IActionResult> GetAllAsync()
        {
            return Json(await courseModuleService.GetAllAsync());
        }

        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Json(await courseModuleService.GetByIdAsync(id));
        }

        public async Task<IActionResult> GetByIdForEditAsync(int id)
        {
            var courseModuleViewModel = await courseModuleService.GetByIdForEditAsync(id);
            //Console.WriteLine("course module type: " + courseModuleViewModel.GetType());
            return PartialView("_UpdateCourseModule", courseModuleViewModel);
        }

        /*
        public async Task<IActionResult> GetByIdForEditForCourseScheduleAsync(int id)
        {
            var courseModuleViewModel = await courseModuleService.GetByIdForEditAsync(id);
            Console.WriteLine("course module type: " + courseModuleViewModel.GetType());
            return PartialView("../CourseModule/_CourseScheduleUpdate", courseModuleViewModel);
        }
        */
        public IActionResult Index()
        {
            logger.LogInformation("CourseModuleController - Index");
            return View();
        }

        
        public async Task<IActionResult> EditCourseModule(CourseModuleViewModel courseModuleViewModel)
        {
            return Json(await courseModuleService.PutAsync(courseModuleViewModel));
        }

        public async Task<IActionResult> PutAsync(CourseModuleViewModel courseModuleViewModel)
        {
            return Json(await courseModuleService.PutAsync(courseModuleViewModel));
        }

        public async Task<IActionResult> PutUnAssign(CourseModuleViewModel courseModuleViewModel)
        {
            return Json(await courseModuleService.PutUnAssignAsync(courseModuleViewModel));
        }

    }
}