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
    /// in the GenericService Tests are run for the TrainerCourseService
    /// </summary>
    public class TrainerCourseServiceTest : GenericServiceTest<Trainer_Course,
        TrainerCourseDto, PostTrainerCourseDto, ITrainerCourseRepository>
    {
        /// <summary>
        /// Overriding the GenericServiceTestHelper method to return a TrainerCourseService
        /// so the generic tests will be run against the TrainerCourseService
        /// </summary>
        /// <param name="mockMapper"></param>
        /// <param name="mockRepo"></param>
        /// <returns>TrainerCourseService</returns>
        protected override GenericService<Trainer_Course,
            TrainerCourseDto, PostTrainerCourseDto, ITrainerCourseRepository>
            GenericServiceTestHelper(Mock<IMapper> mockMapper,
            Mock<ITrainerCourseRepository> mockRepo)
        {
            return new TrainerCourseService(mockMapper.Object, mockRepo.Object);
        }
    }
}