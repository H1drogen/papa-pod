using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.Web.Controllers;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Fdm.Ams.Web.Tests
{
    public class TrainerCourseControllerTests
    {
        private Mock<ILogger<TrainerCourseController>> mockLogger;
        private Mock<ITrainerCourseService> mockTrainerCourseService;

        public TrainerCourseControllerTests()
        {
            mockLogger = new Mock<ILogger<TrainerCourseController>>();
            mockTrainerCourseService = new Mock<ITrainerCourseService>();
        }

        private TrainerCourseController CreateController()
        {
            return new TrainerCourseController(mockTrainerCourseService.Object, mockLogger.Object);
        }

        #region GetByIdAsync

        [Fact]
        public async Task GetByIdAsync_ShouldCallGetByIdAsync_Once()
        {
            //Arrange
            int id = 1;
            var controller = CreateController();
            //Act
            await controller.GetByIdAsync(id);
            //Asset
            mockTrainerCourseService.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnSerializedVersionOfEntityReturnedFromGetAsync_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = CreateController();
            var trainerCourse = Builder<TrainerCourse>.CreateNew().Build();
            mockTrainerCourseService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(trainerCourse);
            var expectedResult = new JsonResult(trainerCourse);

            //Act
            var result = await controller.GetByIdAsync(id);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetByIdAsync

        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_ShouldCallTrainerCourseServiceDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var trainerCourse = Builder<TrainerCourse>.CreateNew().Build();
            mockTrainerCourseService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(trainerCourse);
            var controller = CreateController();

            //Act
            await controller.DeleteAsync(id);
            //Assert
            mockTrainerCourseService.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            int id = 1;
            var trainerCourse = Builder<TrainerCourse>.CreateNew().Build();
            mockTrainerCourseService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(trainerCourse);
            var controller = CreateController();
            //Act

            var result = await controller.DeleteAsync(id);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion DeleteAsync

        #region Index

        [Fact]
        public void Index_ShouldNotReturnANullResult_WhenCalled()
        {
            //Arrange
            var controller = CreateController();

            //Act
            var result = controller.Index();
            //Assert
            result.Should().NotBeNull();
        }

        #endregion Index

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_ShouldCallTrainerCourseServiceGetAllAsync_WhenCalled()
        {
            //Arrange
            var controller = CreateController();
            //Act
            await controller.GetAllAsync();
            //Assert
            mockTrainerCourseService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnJsonResult_WithCorrectData_WhenCalled()
        {
            //Arrange
            var list = Builder<TrainerCourse>.CreateListOfSize(3).Build();
            mockTrainerCourseService.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var expectedResult = new JsonResult(list);
            var controller = CreateController();
            //Act
            var result = await controller.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetAllAsync

        #region PostAsync

        [Fact]
        public async Task PostAsync_ShouldCallsTrainerCourseServicePostAsync_WhenCalled()
        {
            //Arrange
            var trainerCourse = Builder<TrainerCourse>.CreateNew().Build();
            var controller = CreateController();

            //Act
            await controller.PostAsync(trainerCourse);
            //Assert
            mockTrainerCourseService.Verify(x => x.PostAsync(trainerCourse), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var trainerCourse = Builder<TrainerCourse>.CreateNew().Build();
            mockTrainerCourseService.Setup(x => x.PostAsync(trainerCourse));
            var controller = CreateController();

            //Act
            var result = await controller.PostAsync(trainerCourse);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion PostAsync

        #region PutAsync

        [Fact]
        public async Task PutAsync_ShouldCallTrainerCourseService_WhenANonNullCourseTypeIsPassed()
        {
            //Arrange
            var trainerCourse = Builder<TrainerCourse>.CreateNew().Build();
            var controller = CreateController();
            //Act

            await controller.PutAsync(trainerCourse);
            //Assert
            mockTrainerCourseService.Verify(x => x.PutAsync(trainerCourse), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var trainerCourse = Builder<TrainerCourse>.CreateNew().Build();
            mockTrainerCourseService.Setup(x => x.PutAsync(trainerCourse));
            var controller = CreateController();
            //Act

            var result = await controller.PutAsync(trainerCourse);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion PutAsync
    }
}