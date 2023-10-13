using AutoMapper;
using Fdm.Common.Service;
using Fdm.Common.Tests.Services;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services;
using Moq;

namespace Fdm.ResourcePlainningTool.Services.Tests
{
    public class VenueServiceTests : GenericServiceTest<Venue, VenueDto, PostVenueDto, IVenueRepository>
    {
        protected override GenericService<Venue, VenueDto, PostVenueDto, IVenueRepository> GenericServiceTestHelper(Mock<IMapper> mockMapper, Mock<IVenueRepository> mockRepo)
        {
            return new VenueService(mockMapper.Object, mockRepo.Object);
        }
    }
}