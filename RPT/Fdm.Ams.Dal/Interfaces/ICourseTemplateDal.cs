using Fdm.Ams.Models;
using Fdm.Ams.Models.Post;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal.Interfaces
{
    public interface ICourseTemplateDal
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<CourseTemplate>> GetAllAsync();

        Task<CourseTemplate> GetByIdAsync(int id);

        Task<CourseTemplate> PostAsync(CourseTemplate courseTemplate);

        Task<CourseTemplate> PutAsync(CourseTemplate courseTemplate);
    }
}