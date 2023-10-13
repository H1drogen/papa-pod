using Fdm.Ams.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal.Interfaces
{
    public interface ITrainerCourseDal
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<TrainerCourse>> GetAllAsync();

        Task<TrainerCourse> GetByIdAsync(int id);

        Task<TrainerCourse> PostAsync(TrainerCourse trainerCourse);

        Task<TrainerCourse> PutAsync(TrainerCourse trainerCourse);
    }
}