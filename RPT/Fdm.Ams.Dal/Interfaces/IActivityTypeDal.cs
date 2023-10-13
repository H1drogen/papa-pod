using Fdm.Ams.Models;
using Fdm.Ams.Models.Post;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal.Interfaces
{
    public interface IActivityTypeDal
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<ActivityType>> GetAllAsync();

        Task<ActivityType> GetByIdAsync(int id);

        Task<ActivityType> PostAsync(PostActivityTypeDto postDto);

        Task PutAsync(ActivityType activityType);
    }
}