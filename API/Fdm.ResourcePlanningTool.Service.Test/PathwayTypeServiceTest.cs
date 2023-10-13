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
    /// in the GenericService Tests are run for the PathwayTypeService
    /// </summary>
    public class PathwayTypeServiceTests : GenericServiceTest<PathwayType,
        PathwayTypeDto,
        PostPathwayTypeDto,
        IPathwayTypeRepository>
    {
        /// <summary>
        /// Overriding the GenericServiceTestHelper method to return a PathwayTypeService
        /// so the generic tests will be run against the PathwayTypeService
        /// </summary>
        /// <param name="mockMapper"></param>
        /// <param name="mockRepo"></param>
        /// <returns>PathwayTypeService</returns>
        protected override GenericService<PathwayType,
            PathwayTypeDto,
            PostPathwayTypeDto,
            IPathwayTypeRepository>
            GenericServiceTestHelper(Mock<IMapper> mockMapper, Mock<IPathwayTypeRepository> mockRepo)
        {
            return new PathwayTypeService(mockMapper.Object, mockRepo.Object);
        }
    }
}