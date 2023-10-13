using Fdm.Ams.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal.Interfaces
{
    public interface ICourseModuleTemplateDal
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<CourseModuleTemplate>> GetAllAsync();

        Task<CourseModuleTemplate> GetByIdAsync(int id);

        Task<CourseModuleTemplate> PostAsync(CourseModuleTemplate model);

        Task<CourseModuleTemplate> PutAsync(CourseModuleTemplate courseModuleTemplate);
    }
}