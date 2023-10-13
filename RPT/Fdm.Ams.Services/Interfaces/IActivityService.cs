using Fdm.Ams.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services.Interfaces
{
    public interface IActivityService
    {
        Task<IEnumerable<Activity>> GetActivitiesForConsultant(string sid);

        Task<IEnumerable<Activity>> GetAll();

        Task<IEnumerable<Activity>> GetAllByConsultantId(int id);
    }
}