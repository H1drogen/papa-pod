using System.Threading.Tasks;
using Fdm.Common.Service;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;

namespace Fdm.ResourcePlanningTool.Services.Interfaces
{
    public interface IOfficeService : IGenericService<OfficeDto, PostOfficeDto>
    {
        Task<IEnumerable<OfficeDto>> GetActiveOfficesByCountryId(int countryId);

        /*       Task<IEnumerable<OfficeDto>> GetActiveOfficesForUser(string userObjectId);

               Task<IEnumerable<OfficeDto>> GetAllActive();*/
    }
}