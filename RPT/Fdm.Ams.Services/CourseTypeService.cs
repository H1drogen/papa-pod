using AutoMapper;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services
{
    public class CourseTypeService : ICourseTypeService
    {
        private readonly ICourseTypeDal courseTypeDal;
        private readonly IMapper mapper;

        public CourseTypeService(ICourseTypeDal courseTypeDal,
            IMapper mapper)
        {
            this.courseTypeDal = courseTypeDal;
            this.mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            await courseTypeDal.DeleteAsync(id);
        }

        public async Task<IEnumerable<CourseTypeViewModel>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<CourseTypeViewModel>>(await courseTypeDal.GetAllAsync());
        }

        public async Task<CourseTypeViewModel> GetByIdAsync(int id)
        {
            return mapper.Map<CourseTypeViewModel>(await courseTypeDal.GetByIdAsync(id));
        }

        public async Task<CourseTypeViewModel> PostAsync(CourseTypeViewModel courseTypeViewModel)
        {
            var courseType = await courseTypeDal.PostAsync(mapper.Map<CourseType>(courseTypeViewModel));

            return mapper.Map<CourseTypeViewModel>(courseType);
        }

        public async Task<CourseTypeViewModel> PutAsync(CourseTypeViewModel courseTypeViewModel)
        {
            var courseType = await courseTypeDal.PutAsync(mapper.Map<CourseType>(courseTypeViewModel));
            return mapper.Map<CourseTypeViewModel>(courseType);
        }
    }
}