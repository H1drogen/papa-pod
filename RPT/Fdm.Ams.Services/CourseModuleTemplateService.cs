using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;

namespace Fdm.Ams.Services
{
    public class CourseModuleTemplateService : ICourseModuleTemplateService
    {
        private readonly ICourseModuleTemplateDal courseModuleTemplateDal;
        private readonly IMapper mapper;

        public CourseModuleTemplateService(ICourseModuleTemplateDal courseModuleTemplateDal, IMapper mapper)
        {
            this.courseModuleTemplateDal = courseModuleTemplateDal;
            this.mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            await courseModuleTemplateDal.DeleteAsync(id);
        }

        public async Task<IEnumerable<CourseModuleTemplateViewModel>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<CourseModuleTemplateViewModel>>(await courseModuleTemplateDal.GetAllAsync());
        }

        public async Task<IEnumerable<CourseModuleTemplate>> GetAllByPathwayTemplateIdAsync(int pathwayTemplateId)
        {
            return (await courseModuleTemplateDal.GetAllAsync()).Where(x => x.PathwayTemplateId == pathwayTemplateId);
        }

        public async Task<IEnumerable<CourseModuleTemplate>> GetAllByNullPathwayTemplateIdAsync()
        {
            return (await courseModuleTemplateDal.GetAllAsync()).Where(x => x.PathwayTemplateId == null);
        }

        public async Task<CourseModuleTemplateViewModel> GetByIdAsync(int id)
        {
            return mapper.Map<CourseModuleTemplateViewModel>(await courseModuleTemplateDal.GetByIdAsync(id));
        }

        public async Task<CourseModuleTemplateViewModel> PostAsync(CourseModuleTemplateViewModel viewModel)
        {
            viewModel.CreatedBy = "System"; // TODO: Change after auth is implemented
            var model = await courseModuleTemplateDal.PostAsync(mapper.Map<CourseModuleTemplate>(viewModel));
            return mapper.Map<CourseModuleTemplateViewModel>(model);
        }

        public async Task<CourseModuleTemplateViewModel> PutAsync(CourseModuleTemplateViewModel viewModel)
        {
            viewModel.CreatedBy = "System"; // TODO: Change after auth is implemented
            var model = await courseModuleTemplateDal.PutAsync(mapper.Map<CourseModuleTemplate>(viewModel));           
            
            return mapper.Map<CourseModuleTemplateViewModel>(model);
        }
    }
}