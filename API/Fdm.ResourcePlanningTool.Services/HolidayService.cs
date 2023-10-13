using AutoMapper;
using Fdm.Common.Service;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;
using System.Data;

namespace Fdm.ResourcePlanningTool.Services
{
    public class HolidayService : GenericService<Holiday, HolidayDto, PostHolidayDto, IHolidayRepository>, IHolidayService
    {
        private readonly ICountryRepository countryRepository;
        private readonly IMapper mapper;
        private readonly IHolidayRepository repo;

        public HolidayService(IMapper mapper, IHolidayRepository repo,
            ICountryRepository countryRepository
            ) : base(mapper, repo)
        {
            this.mapper = mapper;
            this.repo = repo;
            this.countryRepository = countryRepository;
        }

        public override async Task<HolidayDto> Create(PostHolidayDto postDto)
        {
            var result = await repo.GetEntitiesAsync(
                x => x.Name == postDto.Name &&
                x.CountryId == postDto.CountryId &&
                x.StartDate.Date == postDto.StartDate.Date);

            if (result.Any())
            {
                throw new DuplicateNameException();
            }
            var createdDto = mapper.Map<HolidayDto>(await repo.InsertAsync(mapper.Map<Holiday>(postDto)));
            return createdDto;
        }

        public override async Task<HolidayDto> Update(HolidayDto holidayDto)
        {
            var result = await repo.GetEntitiesAsync(
                x => x.Name == holidayDto.Name &&
                x.CountryId == holidayDto.CountryId &&
                x.StartDate.Date == holidayDto.StartDate.Date);

            if (result.Any())
            {
                throw new DuplicateNameException();
            }
            var updatedDto = mapper.Map<HolidayDto>(repo.Update(mapper.Map<Holiday>(holidayDto)));
            await repo.SaveChangesAsync();
            return updatedDto;
        }
    }
}