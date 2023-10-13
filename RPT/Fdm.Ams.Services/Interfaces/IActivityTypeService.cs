using Fdm.Ams.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services.Interfaces
{
    public interface IActivityTypeService
    {
        Task<IEnumerable<ActivityType>> GetActivityTypes();

        Task<ActivityType> GetById(int id);
    }
}