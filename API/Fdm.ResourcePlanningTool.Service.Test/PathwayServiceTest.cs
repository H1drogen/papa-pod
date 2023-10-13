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
    /// <summary>
    /// This class inherits from the GenericService Tests so all the test defined
    /// in the GenericService Tests are run for the PathwayService
    /// </summary>
    public class PathwayServiceTest : GenericServiceTest<Pathway,
        PathwayDto, PostPathwayDto, IPathwayRepository>
    {
        /// <summary>
        /// Overriding the GenericServiceTestHelper method to return a PathwayService
        /// so the generic tests will be run against the PathwayService
        /// </summary>
        /// <param name="mockMapper"></param>
        /// <param name="mockRepo"></param>
        /// <returns>PathwayService</returns>
        protected override GenericService<Pathway,
            PathwayDto, PostPathwayDto, IPathwayRepository>
            GenericServiceTestHelper(Mock<IMapper> mockMapper,
            Mock<IPathwayRepository> mockRepo)
        {
            return new PathwayService(mockMapper.Object, mockRepo.Object);
        }
    }
}