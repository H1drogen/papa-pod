using Fdm.Ams.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal.Interfaces
{
    public interface ICourseModuleDal
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<CourseModule>> GetAllAsync();

        Task<CourseModule> GetByIdAsync(int id);

        Task<CourseModule> PostAsync(CourseModule courseModule);

        Task<CourseModule> PutAsync(CourseModule courseModule);
    }
}