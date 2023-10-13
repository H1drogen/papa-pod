using AutoMapper;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services
{
    public class ProgrammeService : IProgrammeService
    {
        private readonly IMapper mapper;
        private readonly IProgrammeDal programmeDal;

        public ProgrammeService(IProgrammeDal programmeDal, IMapper mapper)
        {
            this.programmeDal = programmeDal;
            this.mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            await programmeDal.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProgrammeViewModel>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<ProgrammeViewModel>>(await programmeDal.GetAllAsync());
        }

        public async Task<ProgrammeViewModel> GetByIdAsync(int id)
        {
            return mapper.Map<ProgrammeViewModel>(await programmeDal.GetByIdAsync(id));
        }

        public async Task<ProgrammeViewModel> PostAsync(ProgrammeViewModel programmeViewModel)
        {
            var programme = await programmeDal.PostAsync(mapper.Map<Programme>(programmeViewModel));
            return mapper.Map<ProgrammeViewModel>(programme);
        }

        public async Task<ProgrammeViewModel> PutAsync(ProgrammeViewModel viewModel)
        {
            var programme = await programmeDal.PutAsync(mapper.Map<Programme>(viewModel));

            return mapper.Map<ProgrammeViewModel>(programme);
        }
    }
}