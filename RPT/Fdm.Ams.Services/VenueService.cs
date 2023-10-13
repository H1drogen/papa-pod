using AutoMapper;
using Fdm.Ams.Dal;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services
{
    public class VenueService : IVenueService
    {
        private readonly IOfficeDal officeDal;
        private readonly IVenueDal venueDal;

        public VenueService(IVenueDal venueDal, IOfficeDal officeDal)
        {
            this.venueDal = venueDal;
            this.officeDal = officeDal;
        }

        public async Task<IEnumerable<Venue>> GetAllAsync()
        {
            return await venueDal.GetAllAsync();
        }

        public async Task<Venue> GetByIdAsync(int id)
        {
            return await venueDal.GetByIdAsync(id);
        }

        public async Task<Office> RetrieveOfficeById(int id)
        {
            return await officeDal.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Venue>> GetActiveVenuesInOffice(int officeId)
        {
            return await venueDal.GetActiveVenuesByOffice(officeId);
        }
    }
}