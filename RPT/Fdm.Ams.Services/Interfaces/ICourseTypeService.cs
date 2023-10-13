using Fdm.Ams.Models;
using Fdm.Ams.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services.Interfaces
{
    public interface ICourseTypeService
    {
        public Task<IEnumerable<CourseTypeViewModel>> GetAllAsync();

        public Task<CourseTypeViewModel> GetByIdAsync(int id);

        public Task<CourseTypeViewModel> PostAsync(CourseTypeViewModel courseTypeViewModel);

        public Task DeleteAsync(int id);

        public Task<CourseTypeViewModel> PutAsync(CourseTypeViewModel courseTypeViewModel);

    }
}