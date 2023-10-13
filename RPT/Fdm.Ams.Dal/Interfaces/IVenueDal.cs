using Fdm.Ams.Models;
using Fdm.Ams.Models.Post;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal.Interfaces
{
    public interface IVenueDal
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<Venue>> GetAllAsync();

        Task<Venue> GetByIdAsync(int id);

        Task<Venue> PostAsync(PostVenueDto postDto);

        Task PutAsync(Venue venue);

        Task<IEnumerable<Venue>> GetActiveVenuesByOffice(int officeId);
    }
}