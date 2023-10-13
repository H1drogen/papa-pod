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
    public class CourseServiceTest : GenericServiceTest<Course,
        CourseDto,
        PostCourseDto,
        ICourseRepository>
    {
        /// <summary>
        /// Overriding the GenericServiceTestHelper method to return a CourseService
        /// so the generic tests will be run against the CourseService
        /// </summary>
        /// <param name="mockMapper"></param>
        /// <param name="mockRepo"></param>
        /// <returns>CourseService</returns>

        protected override GenericService<Course,
            CourseDto,
            PostCourseDto,
            ICourseRepository>
            GenericServiceTestHelper(Mock<IMapper> mockMapper, Mock<ICourseRepository> mockRepo)
        {
            return new CourseService(mockMapper.Object, mockRepo.Object);
        }
    }
}