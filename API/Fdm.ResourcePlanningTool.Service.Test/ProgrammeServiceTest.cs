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
    /// in the GenericService Tests are run for the ProgrammeService
    /// </summary>
    public class ProgrammeServiceTest : GenericServiceTest<Programme,
        ProgrammeDto, PostProgrammeDto, IProgrammeRepository>
    {
        /// <summary>
        /// Overriding the GenericServiceTestHelper method to return a ProgrammeService
        /// so the generic tests will be run against the ProgrammeService
        /// </summary>
        /// <param name="mockMapper"></param>
        /// <param name="mockRepo"></param>
        /// <returns>ProgrammeService</returns>
        protected override GenericService<Programme,
            ProgrammeDto, PostProgrammeDto, IProgrammeRepository>
            GenericServiceTestHelper(Mock<IMapper> mockMapper,
            Mock<IProgrammeRepository> mockRepo)
        {
            return new ProgrammeService(mockMapper.Object, mockRepo.Object);
        }
    }
}