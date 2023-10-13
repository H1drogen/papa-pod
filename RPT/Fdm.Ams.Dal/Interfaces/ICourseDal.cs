using Fdm.Ams.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal.Interfaces
{
    public interface ICourseDal
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<Course>> GetAllAsync();

        Task<Course> GetByIdAsync(int id);

        Task<Course> PostAsync(Course course);

        Task<Course> PutAsync(Course course);
    }
}