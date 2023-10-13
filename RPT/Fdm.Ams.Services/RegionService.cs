using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;

namespace Fdm.Ams.Services
{
    public class RegionService : IRegionService
    {
        private readonly IMapper mapper;
        private readonly IRegionDal regionDal;

        public RegionService(IRegionDal regionDal, IMapper mapper)
        {
            this.regionDal = regionDal;
            this.mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            await regionDal.DeleteAsync(id);
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await regionDal.GetAllAsync();
        }

        public async Task<RegionViewModel> GetByIdAsync(int id)
        {
            return mapper.Map<RegionViewModel>(await regionDal.GetByIdAsync(id));
        }

        public async Task<RegionViewModel> PostAsync(RegionViewModel regionViewModel)
        {
            var region = await regionDal.PostAsync(mapper.Map<Region>(regionViewModel));

            return mapper.Map<RegionViewModel>(region);
        }

        public async Task<RegionViewModel> PutAsync(RegionViewModel regionViewModel)
        {
            var region = await regionDal.PutAsync(mapper.Map<Region>(regionViewModel));

            return mapper.Map<RegionViewModel>(region);
        }
    }
}