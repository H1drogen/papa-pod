using AutoMapper;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fdm.Ams.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly IMapper mapper;

        private readonly IOfficeDal officeDal;
        private readonly ITrainerDal trainerDal;

        public TrainerService(ITrainerDal trainerDal, IOfficeDal officeDal, IMapper mapper)
        {
            this.trainerDal = trainerDal;
            this.officeDal = officeDal;
            this.mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            await trainerDal.DeleteAsync(id);
        }

        public async Task<IEnumerable<Trainer>> GetAllAsync()
        {
            return await trainerDal.GetAllAsync();
        }

        public async Task<TrainerViewModel> GetByIdAsync(int id)
        {
            return mapper.Map<TrainerViewModel>(await trainerDal.GetByIdAsync(id));
        }

        public async Task<TrainerViewModel> GetTrainerWithOfficeList()
        {
            var offices = await officeDal.GetAllAsync();
            Dictionary<int, string> listOfOffices = new();
            offices.ToList().ForEach(x => listOfOffices.Add(x.Id, x.Name));

            var viewModel = new TrainerViewModel()
            {
                OfficeDictionaries = listOfOffices,
            };

            return viewModel;
        }

        public async Task<TrainerViewModel> PostAsync(TrainerViewModel trainerViewModel)
        {
            var trainer = await trainerDal.PostAsync(mapper.Map<Trainer>(trainerViewModel));

            return mapper.Map<TrainerViewModel>(trainer);
        }

        public async Task<TrainerViewModel> PutAsync(TrainerViewModel trainerViewModel)
        {
            var trainer = await trainerDal.PutAsync(mapper.Map<Trainer>(trainerViewModel));
            return mapper.Map<TrainerViewModel>(trainer);
        }
    }
}