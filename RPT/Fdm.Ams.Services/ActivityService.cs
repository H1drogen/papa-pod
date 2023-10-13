using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fdm.Ams.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityDal activityDal;

        public ActivityService(IActivityDal activityDal)
        {
            this.activityDal = activityDal;
        }

        /* This comment requires use of a consultant's objectId, currently there is no implementation which allows this, please change in the future*/

        public async Task<IEnumerable<Activity>> GetActivitiesForConsultant(string sid)
        {
            var activities = await activityDal.GetAllAsync();

            //return activities.Where(x => x.ConsulantObjectId == sid).First();
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Activity>> GetAll()
        {
            return await activityDal.GetAllAsync();
        }

        public async Task<IEnumerable<Activity>> GetAllByConsultantId(int id)
        {
            return (await activityDal.GetAllAsync()).Where(x => x.ConsultantId == id);
        }
    }
}