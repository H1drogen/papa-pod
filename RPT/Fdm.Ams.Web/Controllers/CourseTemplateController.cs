using Fdm.Ams.Services;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Fdm.Ams.Web.Controllers
{
    public class CourseTemplateController : Controller
    {
        private readonly ICourseTemplateService courseTemplateService;
        private readonly ICourseTypeService courseTypeService;
        private readonly ILogger logger;
        private readonly IRegionService regionService;

        public CourseTemplateController(ILogger<CourseTemplateController> logger, ICourseTemplateService courseTemplateService, IRegionService regionService, ICourseTypeService courseTypeService)
        {
            this.logger = logger;
            this.courseTemplateService = courseTemplateService;
            this.regionService = regionService;
            this.courseTypeService = courseTypeService;
        }

        public async Task<IActionResult> CreateAsync()
        {
            var courseTemplateViewModel = await courseTemplateService.GetPathwayTypesAndRegions();

            return View(courseTemplateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(CourseTemplateViewModel courseTemplateViewModel)
        {
            //Deserializing calendar events Json object.
            var eventsViewModel = JsonConvert.DeserializeObject<IEnumerable<CourseTemplateCalendarEventViewModel>>(courseTemplateViewModel.CourseTemplates);

            //Assigning the sequence and duration of events by using the start and end dates.
            var deserializedNewEvents = courseTemplateService.DeserializeEvents(eventsViewModel);

            //Creating Pathway Template
            var pathwayTemplate = await courseTemplateService.PostAsync(courseTemplateViewModel);

            //Posting Course Template
            await courseTemplateService.CreateEventsAsync(pathwayTemplate.Id, deserializedNewEvents);

            TempData["SuccessMessage"] = $"{courseTemplateViewModel.Name} created successfully";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var courseTemplate = await courseTemplateService.GetByIdAsync(id);

            await courseTemplateService.DeleteAsync(id);

            TempData["SuccessMessage"] = $"{courseTemplate.Name} deleted successfully";

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(CourseTemplateViewModel courseTemplateViewModel)
        {
            //Deserializing calendar events Json object.
            var newEventsViewModel = JsonConvert.DeserializeObject<IEnumerable<CourseTemplateCalendarEventViewModel>>(courseTemplateViewModel.CourseTemplates);

            //Assigning the sequence and duration of events by using the start and end dates.
            var deserializedNewEvents = courseTemplateService.DeserializeEvents(newEventsViewModel);

            //Determining the events that need to be created, modified and deleted.
            var createdEvents = await courseTemplateService.SortEventsToBeCreatedAsync(courseTemplateViewModel.Id, deserializedNewEvents);
            var modifiedEvents = await courseTemplateService.SortEventsToBeModifiedAsync(courseTemplateViewModel.Id, deserializedNewEvents);
            var deletedEventsById = await courseTemplateService.SortEventsToBeDeletedAsync(courseTemplateViewModel.Id, modifiedEvents);

            //Calling methods from CourseTemplateService to create, modify and delete events.
            await courseTemplateService.CreateEventsAsync(courseTemplateViewModel.Id, createdEvents);
            await courseTemplateService.ModifyEventsAsync(modifiedEvents);
            await courseTemplateService.DeleteEventsAsync(deletedEventsById);

            //Calling PutAsync to update Course Template.
            await courseTemplateService.PutAsync(courseTemplateViewModel);

            TempData["SuccessMessage"] = $"{courseTemplateViewModel.Name} updated successfully";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetAllAsync()
        {
            return Json(await courseTemplateService.GetAllAsync());
        }

        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Json(await courseTemplateService.GetByIdAsync(id));
        }

        public async Task<IActionResult> GetByIdForEditAsync(int id)
        {
            var courseTemplateViewModel = await courseTemplateService.GetByIdAsync(id);
            var regionName = regionService.GetByIdAsync(courseTemplateViewModel.RegionId).Result.Name;
            var pathwayTypeName = courseTypeService.GetByIdAsync(courseTemplateViewModel.PathwayTypeId).Result.Name;

            courseTemplateViewModel.PathwayTypeName = pathwayTypeName;
            courseTemplateViewModel.RegionName = regionName;

            return View("UpdateCourseTemplate", courseTemplateViewModel);
        }

        /*public IActionResult Index()
        {
            logger.LogInformation("CourseTemplateController - Index");

            ViewBag.SuccessMessage = TempData["SuccessMessage"];

            return View();
        }*/

        public async Task<IActionResult> Index()
        {
            logger.LogInformation("CourseTemplateController - Index");
            var pathwayTemplateDetails = await courseTemplateService.GetAllAsync();
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(pathwayTemplateDetails);
        }

        public async Task<IActionResult> Details(int id)
        {
            var courseTemplateViewModel = await courseTemplateService.GetByIdAsync(id);
            var regionName = regionService.GetByIdAsync(courseTemplateViewModel.RegionId).Result.Name;
            var pathwayTypeName = courseTypeService.GetByIdAsync(courseTemplateViewModel.PathwayTypeId).Result.Name;

            courseTemplateViewModel.PathwayTypeName = pathwayTypeName;
            courseTemplateViewModel.RegionName = regionName;

            return View("Details", courseTemplateViewModel);
        }
    }
}