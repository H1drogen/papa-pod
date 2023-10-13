using Fdm.Ams.Models;
using Fdm.Ams.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services.Interfaces
{
    public interface ICourseModuleTemplateService
    {
        Task DeleteAsync(int id);

        public Task<IEnumerable<CourseModuleTemplateViewModel>> GetAllAsync();

        public Task<IEnumerable<CourseModuleTemplate>> GetAllByNullPathwayTemplateIdAsync();

        public Task<IEnumerable<CourseModuleTemplate>> GetAllByPathwayTemplateIdAsync(int pathwayTemplateId);

        public Task<CourseModuleTemplateViewModel> GetByIdAsync(int id);

        public Task<CourseModuleTemplateViewModel> PostAsync(CourseModuleTemplateViewModel viewModel);

        public Task<CourseModuleTemplateViewModel> PutAsync(CourseModuleTemplateViewModel viewModel);
    }
}