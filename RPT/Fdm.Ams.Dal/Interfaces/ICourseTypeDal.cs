using Fdm.Ams.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal.Interfaces
{
    public interface ICourseTypeDal
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<CourseType>> GetAllAsync();

        Task<CourseType> GetByIdAsync(int id);

        Task<CourseType> PostAsync(CourseType courseType);

        Task<CourseType> PutAsync(CourseType courseType);
    }
}