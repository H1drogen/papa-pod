using AutoMapper;
using Fdm.Common.Service;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;

namespace Fdm.ResourcePlanningTool.Services
{
    public class OfficeService : GenericService<Office, OfficeDto, PostOfficeDto, IOfficeRepository>, IOfficeService
    {
        private readonly ICountryRepository countryRepository;
        private readonly IMapper mapper;
        private readonly IOfficeRepository repo;

        public OfficeService(IMapper mapper, IOfficeRepository repo,
            ICountryRepository countryRepository
            ) : base(mapper, repo)
        {
            this.mapper = mapper;
            this.repo = repo;
            this.countryRepository = countryRepository;
        }

        public async Task<IEnumerable<OfficeDto>> GetActiveOfficesByCountryId(int countryId)
        {
            var activeOfficesByCountry = await this.repo.GetEntitiesAsync(o => o.IsActive && o.CountryId == countryId);
            return mapper.Map<IEnumerable<OfficeDto>>(activeOfficesByCountry);

        }

        /*
                public override async Task<OfficeDto> Create(PostOfficeDto officeDto)
                {
                    var officePoco = mapper.Map<Office>(officeDto);
                    officePoco.Country = await countryRepository.GetByIdAsync(officeDto.CountryId);
                    var result = await repo.InsertAsync(officePoco);
                    return mapper.Map<OfficeDto>(result);
                }

                public async Task<IEnumerable<OfficeDto>> GetActiveOfficesForUser(string userObjectId)
                {
                    var userRegions = userRegionPermissionRepository.GetEntities(ur => ur.User.ObjectId == userObjectId && !ur.RemovalDate.HasValue)
                        .Select(ur => ur.Region)
                        .Distinct();
                    var offices = (await repo.GetAllAsync()).Where(office => office.IsActive && userRegions.Contains(office.Country.Region));
                    return mapper.Map<IEnumerable<OfficeDto>>(offices);
                }

                public override async Task<IEnumerable<OfficeDto>> GetAll()
                {
                    var office = (await (repo.GetAllAsync())).OrderBy(o => o.Name);
                    return mapper.Map<IEnumerable<OfficeDto>>(office);
                }

                public async Task<IEnumerable<OfficeDto>> GetAllActive()
                {
                    var activeOffice = await repo.GetEntitiesAsync(o => o.IsActive);

                    return mapper.Map<IEnumerable<OfficeDto>>(activeOffice);
                }*/
    }
}