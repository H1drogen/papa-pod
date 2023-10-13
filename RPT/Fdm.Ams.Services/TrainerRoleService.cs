using AutoMapper;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services
{
    public class TrainerRoleService : ITrainerRoleService
    {
        private readonly IMapper mapper;
        private readonly ITrainerRoleDal trainerRoleDal;

        public TrainerRoleService(ITrainerRoleDal trainerRoleDal, IMapper mapper)
        {
            this.trainerRoleDal = trainerRoleDal;
            this.mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            await trainerRoleDal.DeleteAsync(id);
        }

        public async Task<IEnumerable<TrainerRoleViewModel>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<TrainerRoleViewModel>>(await trainerRoleDal.GetAllAsync());
        }

        public async Task<TrainerRoleViewModel> GetByIdAsync(int id)
        {
            return mapper.Map<TrainerRoleViewModel>(await trainerRoleDal.GetByIdAsync(id));
        }

        public async Task<TrainerRoleViewModel> PostAsync(TrainerRoleViewModel trainerRoleViewModel)
        {
            var trainerRole = await trainerRoleDal.PostAsync(mapper.Map<TrainerRole>(trainerRoleViewModel));
            return mapper.Map<TrainerRoleViewModel>(trainerRole);
        }

        public async Task<TrainerRoleViewModel> PutAsync(TrainerRoleViewModel trainerRoleViewModel)
        {
            var trainerRole = await trainerRoleDal.PutAsync(mapper.Map<TrainerRole>(trainerRoleViewModel));
            return mapper.Map<TrainerRoleViewModel>(trainerRole);
        }
    }
}