using AutoMapper;
using Fdm.Common.Service;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;

namespace Fdm.ResourcePlanningTool.Services
{
    public class TrainerService : GenericService<Trainer, TrainerDto, PostTrainerDto, ITrainerRepository>, ITrainerService
    {
        private readonly IMapper mapper;
        private ITrainerRepository trainerRepository;

        public TrainerService(IMapper mapper, ITrainerRepository repo) : base(mapper, repo)
        {
            this.mapper = mapper;
            this.trainerRepository = repo;
        }

        /*      public override async Task<TrainerDto> Create(PostTrainerDto postDto)
              {
                  var trainer = mapper.Map<Trainer>(postDto);
                  // We add the Id manually do to the auto-increment not working for inserting new users in the db
                  var userRecordCounts = userRepository.GetAll();
                  if (!userRecordCounts.Any())
                  {
                      trainer.Id++;
                  }
                  else
                  {
                      trainer.Id = userRecordCounts.Last().Id + 1;
                  }
                  trainer = await trainerRepository.InsertAsync(trainer);
                  return mapper.Map<TrainerDto>(trainer);
              }*/
    }
}