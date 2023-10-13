using Fdm.Ams.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services.Interfaces
{
    public interface IVenueService
    {
        public Task<IEnumerable<Venue>> GetActiveVenuesInOffice(int officeId);
        
        public Task<IEnumerable<Venue>> GetAllAsync();

        public Task<Venue> GetByIdAsync(int id);
    }
}