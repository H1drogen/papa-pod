using AutoMapper;
using Fdm.Common.Service;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;

namespace Fdm.ResourcePlanningTool.Services
{
    public class VenueService : GenericService<Venue, VenueDto, PostVenueDto, IVenueRepository>, IVenueService
    {
        private readonly IMapper mapper;
        private readonly IVenueRepository repo;

        public VenueService(IMapper mapper, IVenueRepository repo) : base(mapper, repo)
        {
            this.mapper = mapper;
            this.repo = repo;
        }

        public async Task<IEnumerable<VenueDto>> GetActiveVenuesByOfficeId(int officeId)
        {
            var activeVenuesByOffice = await this.repo.GetEntitiesAsync(v => v.Active && v.OfficeId == officeId);
            return mapper.Map<IEnumerable<VenueDto>>(activeVenuesByOffice);

        }
    }
}