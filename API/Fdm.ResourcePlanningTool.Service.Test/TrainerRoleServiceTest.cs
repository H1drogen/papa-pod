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
    /// in the GenericService Tests are run for the TrainerRoleService
    /// </summary>
    public class TrainerRoleServiceTest : GenericServiceTest<TrainerRole,
        TrainerRoleDto, PostTrainerRoleDto, ITrainerRoleRepository>
    {
        /// <summary>
        /// Overriding the GenericServiceTestHelper method to return a TrainerRoleService
        /// so the generic tests will be run against the TrainerRoleService
        /// </summary>
        /// <param name="mockMapper"></param>
        /// <param name="mockRepo"></param>
        /// <returns>TrainerRoleService</returns>
        protected override GenericService<TrainerRole,
            TrainerRoleDto, PostTrainerRoleDto, ITrainerRoleRepository>
            GenericServiceTestHelper(Mock<IMapper> mockMapper,
            Mock<ITrainerRoleRepository> mockRepo)
        {
            return new TrainerRoleService(mockMapper.Object, mockRepo.Object);
        }
    }
}