using Fdm.Ams.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services.Interfaces
{
    public interface ITrainerCourseService
    {
        public Task DeleteAsync(int id);

        public Task<IEnumerable<TrainerCourse>> GetAllAsync();

        public Task<TrainerCourse> GetByIdAsync(int id);

        public Task<TrainerCourse> PostAsync(TrainerCourse trainerCourse);

        public Task<TrainerCourse> PutAsync(TrainerCourse trainerCourse);
    }
}