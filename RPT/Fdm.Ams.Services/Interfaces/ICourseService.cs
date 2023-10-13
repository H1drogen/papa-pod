using Fdm.Ams.Models;
using Fdm.Ams.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services.Interfaces
{
    public interface ICourseService
    {
        public Task<IEnumerable<Course>> GetAllAsync();

        public Task<CourseViewModel> GetByIdAsync(int id);

        public Task<string> GetCourseCode(int regionId, int templateId, int year, int programmeId);

        public Task<CourseViewModel> GetCoursesByPathwayId(int id);

        public Task<CourseViewModel> GetOfficesProgrammesCourseTemplatesAndTimeZones();

        public Task<CourseViewModel> GetProgrammeRegionPathwayTypes();

        public Task<CourseViewModel> PostAsync(CourseViewModel viewModel);


    }
}