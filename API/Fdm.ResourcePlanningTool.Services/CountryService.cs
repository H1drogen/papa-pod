using AutoMapper;
using Fdm.Common.Service;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;

namespace Fdm.ResourcePlanningTool.Services
{
    public class CountryService : GenericService<Country, CountryDto, PostCountryDto, ICountryRepository>, ICountryService
    {
        private readonly IMapper mapper;
        private readonly ICountryRepository repo;

        public CountryService(IMapper mapper, ICountryRepository repo) : base(mapper, repo)
        {
            this.mapper = mapper;
            this.repo = repo;
        }


        public async Task<IEnumerable<CountryDto>> GetActiveCountriesByRegionId(int regionId)
        {
            var activeCountriesByRegion = await this.repo.GetEntitiesAsync(c => c.IsActive && c.RegionId == regionId);
            return mapper.Map<IEnumerable<CountryDto>>(activeCountriesByRegion);

        }

        /*        #region extension methods

                public async Task<IEnumerable<CountryDto>> GetActiveCountriesForUser(string userObjectId)
                {
                    var regions = userRegionPermissionRepository.GetEntities(ur => ur.User.ObjectId == userObjectId && !ur.RemovalDate.HasValue)
                        .Select(ur => ur.Region)
                        .Distinct();
                    var activeCountriesForUser = (await repo.GetAllAsync()).Where(country => country.IsActive && regions.Contains(country.Region));
                    return mapper.Map<IEnumerable<CountryDto>>(activeCountriesForUser);
                }

                public override async Task<IEnumerable<CountryDto>> GetAll()
                {
                    var country = (await repo.GetAllAsync()).OrderBy(l => l.Name);
                    return mapper.Map<IEnumerable<CountryDto>>(country);
                }

                public async Task<IEnumerable<CountryDto>> GetAllActive()
                {
                    var activeCountry = await repo.GetEntitiesAsync(x => x.IsActive);
                    return mapper.Map<IEnumerable<CountryDto>>(activeCountry);
                }

                public Dictionary<string, CountryDto> GetCountriesDictionary(IEnumerable<CountryDto> countries, string unknownText)
                {
                    var countriesDictionary = new Dictionary<string, CountryDto>();

                    if (countries != null)
                    {
                        foreach (var country in countries)
                        {
                            countriesDictionary.Add(country.Name, country);
                        }
                    }

                    countriesDictionary.Add(unknownText, new CountryDto { Name = unknownText, RegionName = unknownText });

                    return countriesDictionary;
                }

                #endregion extension methods*/
    }
}