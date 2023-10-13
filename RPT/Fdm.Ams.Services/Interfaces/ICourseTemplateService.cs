using System.Collections.Generic;
using System.Threading.Tasks;
using Fdm.Ams.ViewModels;

namespace Fdm.Ams.Services.Interfaces
{
    public interface ICourseTemplateService
    {
        public Task CreateEventsAsync(int courseTemplateId, IEnumerable<CourseTemplateCalendarEventViewModel> createdEvents);

        public Task DeleteAsync(int id);

        public Task DeleteEventsAsync(IEnumerable<int> deletedEventsById);

        public IEnumerable<CourseTemplateCalendarEventViewModel> DeserializeEvents(IEnumerable<CourseTemplateCalendarEventViewModel> newEventsViewModels);

        public Task<IEnumerable<CourseTemplateViewModel>> GetAllAsync();

        public Task<CourseTemplateViewModel> GetByIdAsync(int id);

        public Task<CourseTemplateViewModel> GetPathwayTypesAndRegions();

        public Task ModifyEventsAsync(IEnumerable<CourseTemplateCalendarEventViewModel> modifiedEvents);

        public Task<CourseTemplateViewModel> PostAsync(CourseTemplateViewModel courseTemplateViewModel);

        public Task<CourseTemplateViewModel> PutAsync(CourseTemplateViewModel courseTemplateViewModel);

        public Task<IEnumerable<CourseTemplateCalendarEventViewModel>> SortEventsToBeCreatedAsync(int courseTemplateId, IEnumerable<CourseTemplateCalendarEventViewModel> deserializedNewEvents);

        public Task<IEnumerable<int>> SortEventsToBeDeletedAsync(int courseTemplateId, IEnumerable<CourseTemplateCalendarEventViewModel> newEvents);

        public Task<IEnumerable<CourseTemplateCalendarEventViewModel>> SortEventsToBeModifiedAsync(int courseTemplateId, IEnumerable<CourseTemplateCalendarEventViewModel> deserializedNewEvents);
    }
}