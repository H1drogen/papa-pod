using Fdm.Common.Service;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;

namespace Fdm.ResourcePlanningTool.Services.Interfaces
{
    public interface ICountryService : IGenericService<CountryDto, PostCountryDto>
    {

         Task<IEnumerable<CountryDto>> GetActiveCountriesByRegionId(int regionId);



        /*        Task<IEnumerable<CountryDto>> GetActiveCountriesForUser(string userObjectId);

                Task<IEnumerable<CountryDto>> GetAllActive();

                Dictionary<string, CountryDto> GetCountriesDictionary(IEnumerable<CountryDto> countries, string unknownText);
        */
    }
}