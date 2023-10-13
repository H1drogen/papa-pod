using Fdm.Ams.Models;
using Fdm.Ams.Models.Post;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal.Interfaces
{
    public interface IActivityDal
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<Activity>> GetAllAsync();

        Task<Activity> GetByIdAsync(int id);

        Task<Activity> PostAsync(PostActivityDto postDto);

        Task PutAsync(Activity activity);
    }
}