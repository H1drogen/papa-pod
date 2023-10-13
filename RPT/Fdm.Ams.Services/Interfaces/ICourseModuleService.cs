using Fdm.Ams.Models;
using Fdm.Ams.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services.Interfaces
{
    public interface ICourseModuleService
    {
        Task DeleteAsync(int id);

        Task<CourseModuleViewModel> GetByIdForEditAsync(int id);

        Task<IEnumerable<CourseModuleViewModel>> GetAllAsync();

        Task<CourseModuleViewModel> GetByIdAsync(int id);
        Task PostListOfCourseModules(CourseViewModel returnedCourseViewModel, IEnumerable<CourseModuleTemplate> courseModuleTemplates);

        Task<CourseModule> PostAsync(CourseModule courseModule);

        Task<CourseModule> PutAsync(CourseModuleViewModel courseModuleViewModel);

        Task<CourseModule> PutUnAssignAsync(CourseModuleViewModel courseModuleViewModel);

    }
}