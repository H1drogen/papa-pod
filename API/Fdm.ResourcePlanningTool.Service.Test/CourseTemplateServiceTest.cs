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
    /// in the GenericService Tests are run for the CourseTemplateService
    /// </summary>
    public class CourseTemplateServiceTest : GenericServiceTest<CourseTemplate,
        CourseTemplateDto,
        PostCourseTemplateDto,
        ICourseTemplateRepository>
    {
        /// <summary>
        /// Overriding the GenericServiceTestHelper method to return a CourseTemplateService
        /// so the generic tests will be run against the CourseTemplateService
        /// </summary>
        /// <param name="mockMapper"></param>
        /// <param name="mockRepo"></param>
        /// <returns>CourseTemplateService</returns>
        protected override GenericService<CourseTemplate,
            CourseTemplateDto,
            PostCourseTemplateDto,
            ICourseTemplateRepository>
            GenericServiceTestHelper(Mock<IMapper> mockMapper,
            Mock<ICourseTemplateRepository> mockRepo)
        {
            return new CourseTemplateService(mockMapper.Object, mockRepo.Object);
        }
    }
}