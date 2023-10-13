using AutoMapper;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services
{
    public class TrainerCourseService : ITrainerCourseService
    {
        private readonly IMapper mapper;
        private readonly ITrainerCourseDal trainerCourseDal;

        public TrainerCourseService(ITrainerCourseDal trainerCourseDal,
            IMapper mapper)
        {
            this.trainerCourseDal = trainerCourseDal;
            this.mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            await trainerCourseDal.DeleteAsync(id);
        }

        public async Task<IEnumerable<TrainerCourse>> GetAllAsync()
        {
            return await trainerCourseDal.GetAllAsync();
        }

        public async Task<TrainerCourse> GetByIdAsync(int id)
        {
            return await trainerCourseDal.GetByIdAsync(id);
        }

        public async Task<TrainerCourse> PostAsync(TrainerCourse trainerCourse)
        {
            return await trainerCourseDal.PostAsync(trainerCourse);
        }

        public async Task<TrainerCourse> PutAsync(TrainerCourse trainerCourse)
        {
            return await trainerCourseDal.PutAsync(trainerCourse);
        }
    }
}