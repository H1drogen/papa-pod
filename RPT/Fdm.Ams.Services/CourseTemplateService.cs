using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fdm.Ams.Dal;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;

namespace Fdm.Ams.Services
{
    public class CourseTemplateService : ICourseTemplateService
    {
        private readonly ICourseModuleTemplateDal courseModuleTemplateDal;
        private readonly ICourseTemplateDal courseTemplateDal;
        private readonly ICourseTypeDal courseTypeDal;
        private readonly IMapper mapper;
        private readonly IRegionDal regionDal;

        public CourseTemplateService(ICourseTemplateDal courseTemplateDal, IMapper mapper, IRegionDal regionDal, ICourseTypeDal courseTypeDal, ICourseModuleTemplateDal courseModuleTemplateDal)
        {
            this.courseTemplateDal = courseTemplateDal;
            this.mapper = mapper;
            this.courseModuleTemplateDal = courseModuleTemplateDal;
            this.courseTypeDal = courseTypeDal;
            this.regionDal = regionDal;
        }

        public async Task CreateEventsAsync(int pathwayTemplateId, IEnumerable<CourseTemplateCalendarEventViewModel> createdEvents)
        {
            foreach (var calendarEvent in createdEvents)
            {
                var newCourseModuleTemplate = await courseModuleTemplateDal.GetByIdAsync(calendarEvent.Id);

                newCourseModuleTemplate.PathwayTemplateId = pathwayTemplateId;
                newCourseModuleTemplate.Duration = calendarEvent.Duration;
                newCourseModuleTemplate.Sequence = calendarEvent.Sequence;

                await courseModuleTemplateDal.PostAsync(newCourseModuleTemplate);
            };
        }

        public async Task DeleteAsync(int id)
        {
            var pathwayTemplate = await courseTemplateDal.GetByIdAsync(id);

            var retrievedCourseModuleTemplates = (await courseModuleTemplateDal.GetAllAsync()).Where(x => x.PathwayTemplateId == pathwayTemplate.Id);

            retrievedCourseModuleTemplates.ToList().ForEach(x => courseModuleTemplateDal.DeleteAsync(x.Id));

            await courseTemplateDal.DeleteAsync(id);
        }

        public async Task DeleteEventsAsync(IEnumerable<int> deletedEventsById)
        {
            foreach (var deletedEventId in deletedEventsById)
            {
                await courseModuleTemplateDal.DeleteAsync(deletedEventId);
            }
        }

        public IEnumerable<CourseTemplateCalendarEventViewModel> DeserializeEvents(IEnumerable<CourseTemplateCalendarEventViewModel> courseTemplateCalendarEventViewModels)
        {
            return courseTemplateCalendarEventViewModels.OrderBy(x => x.Start)
                .Select((x, i) =>
                {
                    x.Sequence = i;
                    x.Duration = x.End.DayOfYear - x.Start.DayOfYear;
                    return x;
                });
        }

        public async Task<IEnumerable<CourseTemplateViewModel>> GetAllAsync()
        {
            var courseTemplates = await courseTemplateDal.GetAllAsync();
            var courseType = await courseTypeDal.GetAllAsync();
            var region = await regionDal.GetAllAsync();

            List<CourseType> coursesTypesAre = new List<CourseType>();
            List<Region> regionsAre = new List<Region>();

            Dictionary<int, string> pathwayTypesDictionary = new();
            Dictionary<int, string> regionsDictionary = new();

            courseType.ToList().ForEach(x => pathwayTypesDictionary.Add(x.Id, x.Name));
            coursesTypesAre = courseType.ToList();

            region.ToList().ForEach(x => regionsDictionary.Add(x.Id, x.Name));
            regionsAre = region.ToList();

            List<CourseTemplate> coursesTemplatesAre = new List<CourseTemplate>();
            coursesTemplatesAre = courseTemplates.ToList();
            foreach (var courseTemplateIs in courseTemplates)
            {
                courseTemplateIs.PathwayTypeName = coursesTypesAre.Find(x => x.Id == courseTemplateIs.PathwayTypeId).Name.ToString();
                courseTemplateIs.RegionName = regionsAre.Find(x => x.Id == courseTemplateIs.RegionId).Name.ToString();

            }

            return mapper.Map<IEnumerable<CourseTemplateViewModel>>(courseTemplates);
        }

        public async Task<CourseTemplateViewModel> GetByIdAsync(int id)
        {
            return mapper.Map<CourseTemplateViewModel>(await courseTemplateDal.GetByIdAsync(id));
        }

        public async Task<CourseTemplateViewModel> GetPathwayTypesAndRegions()
        {
            var regions = await regionDal.GetAllAsync();
            Dictionary<int, string> listOfRegions = new();
            regions.ToList().ForEach(x => listOfRegions.Add(x.Id, x.Name));

            var pathwayTypes = await courseTypeDal.GetAllAsync();
            Dictionary<int, string> listOfPathwayTypes = new();
            pathwayTypes.ToList().ForEach(x => listOfPathwayTypes.Add(x.Id, x.Name));

            var model = new CourseTemplateViewModel()
            {
                RegionDictionaries = listOfRegions,
                PathwayTypeDictionaries = listOfPathwayTypes,
            };

            return model;
        }

        public async Task ModifyEventsAsync(IEnumerable<CourseTemplateCalendarEventViewModel> modifiedEvents)
        {
            foreach (var calendarEvent in modifiedEvents)
            {
                var courseModuleTemplate = await courseModuleTemplateDal.GetByIdAsync(calendarEvent.Id);
                courseModuleTemplate.Duration = calendarEvent.Duration;
                courseModuleTemplate.Sequence = calendarEvent.Sequence;

                await courseModuleTemplateDal.PutAsync(courseModuleTemplate);
            }
        }

        public async Task<CourseTemplateViewModel> PostAsync(CourseTemplateViewModel courseTemplateViewModel)
        {
            courseTemplateViewModel.CreatedBy = "System"; // TODO: Change after auth is implemented

            var model = await courseTemplateDal.PostAsync(mapper.Map<CourseTemplate>(courseTemplateViewModel));

            return mapper.Map<CourseTemplateViewModel>(model);
        }

        public async Task<CourseTemplateViewModel> PutAsync(CourseTemplateViewModel courseTemplateViewModel)
        {
            var courseTemplate = await courseTemplateDal.GetByIdAsync(courseTemplateViewModel.Id);

            if (courseTemplate != null)
            {
                courseTemplate.Name = courseTemplateViewModel.Name ?? courseTemplate.Name;
                courseTemplate.Description = courseTemplateViewModel.Description ?? courseTemplate.Description;
            }

            return mapper.Map<CourseTemplateViewModel>(await courseTemplateDal.PutAsync(courseTemplate));
        }

        public async Task<IEnumerable<CourseTemplateCalendarEventViewModel>> SortEventsToBeCreatedAsync(int courseTemplateId, IEnumerable<CourseTemplateCalendarEventViewModel> deserializedNewEvents)
        {
            var existingCourseModuleTemplates = (await courseModuleTemplateDal.GetAllAsync()).Where(x => x.PathwayTemplateId == courseTemplateId);

            return deserializedNewEvents.Where(x => existingCourseModuleTemplates.All(y => y.Id != x.Id));
        }

        public async Task<IEnumerable<int>> SortEventsToBeDeletedAsync(int courseTemplateId, IEnumerable<CourseTemplateCalendarEventViewModel> newEvents)
        {
            var courseModuleTemplates = (await courseModuleTemplateDal.GetAllAsync()).Where(x => x.PathwayTemplateId == courseTemplateId);

            var listOfEventsToBeDeletedById = courseModuleTemplates.Select(x => x.Id).ToList().Except(newEvents.Select(x => x.Id).ToList());

            return listOfEventsToBeDeletedById;
        }

        public async Task<IEnumerable<CourseTemplateCalendarEventViewModel>> SortEventsToBeModifiedAsync(int courseTemplateId, IEnumerable<CourseTemplateCalendarEventViewModel> deserializedNewEvents)
        {
            var courseModuleTemplates = (await courseModuleTemplateDal.GetAllAsync()).Where(x => x.PathwayTemplateId == courseTemplateId);

            return deserializedNewEvents.Where(newEvent => courseModuleTemplates.Any(courseModuleTemplate => courseModuleTemplate.Id == newEvent.Id));
        }

        
    }
}