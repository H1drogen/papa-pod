using Fdm.Ams.Services;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.Ams.Web.Controllers
{
    public class CourseController : Controller
    {
        private const int daysInWeek = 7;
        private readonly ICourseModuleService courseModuleService;
        private readonly ICourseModuleTemplateService courseModuleTemplateService;
        private readonly ICourseService courseService;
        private readonly ICourseTemplateService courseTemplateService;
        private readonly ICourseTypeService courseTypeService;
        private readonly ILogger logger;
        private readonly IRegionService regionService;

        public CourseController(
            ICourseService courseService,
            ILogger<CourseController> logger,
            ICourseModuleTemplateService courseModuleTemplateService,
            ICourseModuleService courseModuleService,
            ICourseTypeService courseTypeService,
            IRegionService regionService,
            ICourseTemplateService courseTemplateService)
        {
            this.courseService = courseService;
            this.logger = logger;
            this.courseModuleTemplateService = courseModuleTemplateService;
            this.courseModuleService = courseModuleService;
            this.courseTypeService = courseTypeService;
            this.regionService = regionService;
            this.courseTemplateService = courseTemplateService;
        }

        public async Task<IActionResult> CourseSchedule()
        {
            var viewModel = await courseService.GetOfficesProgrammesCourseTemplatesAndTimeZones();
            ViewBag.SuccessMessage = TempData["SuccessMesssage"];
            return View(viewModel);
        }

        public async Task<JsonResult> GetAllAsync()
        {
            return Json(await courseService.GetAllAsync());
        }

        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Json(await courseService.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetCourseCode(int officeId, int templateId, int year, int programmeId)
        {
            var courseCode = await courseService.GetCourseCode(officeId, templateId, year, programmeId);
            return Json(courseCode);
        }

        [HttpGet]
        public async Task<IActionResult> GetDetailsAsync(int id)
        {
            var pathwayTypes = await courseService.GetProgrammeRegionPathwayTypes();
            var courses = await courseService.GetCoursesByPathwayId(id);
            var regions = await courseService.GetProgrammeRegionPathwayTypes();
            var programmes = await courseService.GetProgrammeRegionPathwayTypes();
            var courseViewModel = await courseService.GetByIdAsync(id);
            courseViewModel.RegionsDictionary = regions.RegionsDictionary;
            courseViewModel.PathwayTypesDictionary = pathwayTypes.PathwayTypesDictionary;
            courseViewModel.ProgrammesDictionary = programmes.ProgrammesDictionary;
            courseViewModel.CoursesDictionary = courses.CoursesDictionary;

            return PartialView("_Details", courseViewModel);
        }

        /*public async Task<IActionResult> Index()
                {
                    logger.LogInformation("CourseController - Index");
                    var courseDetails = await courseService.GetAllAsync();
                    ViewBag.SuccessMessage = TempData["SuccessMessage"];
                    return View(courseDetails);
                }*/

        public async Task<IActionResult> Index()
        {
            logger.LogInformation("CourseController - Index");
            var courseDetails = await courseService.GetProgrammeRegionPathwayTypes();
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(courseDetails.Courses);
        }

        public async Task<IActionResult> Details(int id)
        {
            var courseViewModel = await courseService.GetByIdAsync(id);

            return View("Details", courseViewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostAsync(CourseViewModel courseViewModel)
        {
            var returnedCourseViewModel = await courseService.PostAsync(courseViewModel);
            var courseModuleTemplates = await courseModuleTemplateService.GetAllByPathwayTemplateIdAsync(courseViewModel.PathwayTemplateId.Value);
            await courseModuleService.PostListOfCourseModules(returnedCourseViewModel, courseModuleTemplates);
            TempData["SuccessMesssage"] = $"{returnedCourseViewModel.PathwayCode} {"created successfully"}";
            return RedirectToAction("CourseSchedule");
        }
    }
}