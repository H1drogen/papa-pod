using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services
{
    public class ActivityTypeService : IActivityTypeService
    {
        private readonly IActivityTypeDal activityTypeDal;

        public ActivityTypeService(IActivityTypeDal activityTypeDal)
        {
            this.activityTypeDal = activityTypeDal;
        }

        public async Task<IEnumerable<ActivityType>> GetActivityTypes()
        {
            return await activityTypeDal.GetAllAsync();
        }

        public async Task<ActivityType> GetById(int id)
        {
            return await activityTypeDal.GetByIdAsync(id);
        }
    }
}