using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using Fdm.Ams.Web.Controllers;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Fdm.Ams.Web.Tests
{
    public class VenueControllerTests
    {
        private readonly Mock<ICourseModuleService> mockCourseModuleService;
        private readonly Mock<ILogger<VenueController>> mockLogger;
        private readonly Mock<IVenueService> mockVenueService;

        public VenueControllerTests()
        {
            mockLogger = new Mock<ILogger<VenueController>>();
            mockVenueService = new Mock<IVenueService>();
            mockCourseModuleService = new Mock<ICourseModuleService>();
        }

        private VenueController CreateController()
        {
            return new VenueController(mockLogger.Object, mockVenueService.Object, mockCourseModuleService.Object);
        }

        #region Index

        [Fact]
        public void Index_DoesNotReturnANullResult_WhenCalled()
        {
            //Arrange
            var controller = CreateController();
            //Act
            var result = controller.Index();
            //Assert
            result.Should().NotBeNull();
        }

        #endregion Index

        #region GetAll

        [Fact]
        public async Task GetAll_CallsVenueServiceGetAllAsync_WhenCalled()
        {
            //Arrange
            var controller = CreateController();
            //Act
            await controller.GetAll();
            //Assert
            mockVenueService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAll_ReturnsJsonResult_WithCorrectData_WhenCalled()
        {
            //Arrange
            var list = Builder<Venue>.CreateListOfSize(3).Build();
            mockVenueService.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var expectedResult = new JsonResult(list);
            var controller = CreateController();
            //Act
            var result = await controller.GetAll();
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetAll

        #region VenueSchedule

        [Fact]
        public async Task VenueSchedule_ReturnsAValueThatIsNotNull_WhenCalled()
        {
            //Arrange
            var controller = CreateController();
            //Act
            var result = await controller.VenueSchedule();
            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task VenueSchedule_ShouldCallCourseModuleServiceGetAllAsync_WhenCalled()
        {
            //Arrange
            var controller = CreateController();
            //Act
            await controller.VenueSchedule();
            //Assert
            mockCourseModuleService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task VenueSchedule_ShouldReturnEnumerableOfCourseModule_WhereVenueIdIsNull_FromCourseModuleTemplateServiceGetAllAsync_WhenCalled()
        {
            //Arrange
            var controller = CreateController();
            var courseModuleViewModelLists = Builder<CourseModuleViewModel>
                .CreateListOfSize(5)
                .All()
                .Build();
            var expectedResult = courseModuleViewModelLists.Where(x => x.VenueId == null);
            mockCourseModuleService.Setup(x => x.GetAllAsync());
            //Act
            var result = await controller.VenueSchedule() as ViewResult;
            //Assert
            result.Model.Should().BeEquivalentTo(expectedResult);
        }

        #endregion VenueSchedule
    }
}