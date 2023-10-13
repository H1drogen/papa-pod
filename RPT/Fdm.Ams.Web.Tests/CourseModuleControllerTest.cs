using System.Net.Http;
using System.Threading.Tasks;
using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using Fdm.Ams.Web.Controllers;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Fdm.Ams.Web.Tests
{
    public class CourseModuleControllerTests
    {
        private Mock<ICourseModuleService> mockCourseModuleService;
        private Mock<ILogger<CourseModuleController>> mockLogger;

        public CourseModuleControllerTests()
        {
            mockLogger = new Mock<ILogger<CourseModuleController>>();
            mockCourseModuleService = new Mock<ICourseModuleService>();
        }

        private CourseModuleController CreateController()
        {
            return new CourseModuleController(mockLogger.Object, mockCourseModuleService.Object);
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

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsCourseModuleServiceGetAllAsync_WhenCalled()
        {
            //Arrange
            var controller = CreateController();
            //Act
            await controller.GetAllAsync();
            //Assert
            mockCourseModuleService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsJsonResult_WithCorrectData_WhenCalled()
        {
            //Arrange
            var courseModuleViewModelList = Builder<CourseModuleViewModel>.CreateListOfSize(3).Build();
            mockCourseModuleService.Setup(x => x.GetAllAsync()).ReturnsAsync(courseModuleViewModelList);
            var expectedResult = new JsonResult(courseModuleViewModelList);
            var controller = CreateController();
            //Act
            var result = await controller.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetAllAsync

        #region GetByIdAsync

        [Fact]
        public async Task GetByIdAsync_CallsCourseModuleServiceGetByIdAsync_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = CreateController();
            //Act
            await controller.GetByIdAsync(id);
            //Assert
            mockCourseModuleService.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsJsonResult_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = CreateController();
            //Act
            var result = await controller.GetByIdAsync(id);
            //Assert
            result.Should().BeOfType<JsonResult>();
        }

        #endregion GetByIdAsync

        #region CourseModuleSchedule

        [Fact]
        public void CourseModuleSchedule_ReturnsAValueThatIsNotNull_WhenCalled()
        {
            //Arrange
            var controller = CreateController();
            //Act
            var result = controller.CourseModuleSchedule();
            //Assert
            result.Should().NotBeNull();
        }

        #endregion CourseModuleSchedule

        #region EditAsync
        [Fact]
        public async Task EditAsync_ShouldCallCourseModuleServicePutAsync_WhenANonNullCourseModuleIsPassed()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            var controller = CreateController();
            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseModuleViewModel.Name} updated successfully"
            };
            await controller.EditAsync(courseModuleViewModel);
            //Assert
            mockCourseModuleService.Verify(x => x.PutAsync(courseModuleViewModel), Times.Once);
        }

        [Fact]
        public async Task EditAsync_ShouldRedirectToActionTrainerSchedulePage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            mockCourseModuleService.Setup(x => x.PutAsync(courseModuleViewModel));
            var controller = CreateController();
            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseModuleViewModel.Name} updated successfully"
            };

            var result = await controller.EditAsync(courseModuleViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("TrainerSchedule");
        }

        #endregion EditAsync





        #region EditUnAssignAsync
        [Fact]
        public async Task EditUnAssignAsync_ShouldCallCourseModuleServicePutUnAssignAsync_WhenANonNullCourseModuleIsPassed()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            var controller = CreateController();
            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseModuleViewModel.Name} unassigned successfully"
            };
            await controller.EditUnAssignAsync(courseModuleViewModel);
            //Assert
            mockCourseModuleService.Verify(x => x.PutUnAssignAsync(courseModuleViewModel), Times.Once);
        }

        [Fact]
        public async Task EditUnAssignAsync_ShouldRedirectToActionTrainerSchedulePage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            mockCourseModuleService.Setup(x => x.PutUnAssignAsync(courseModuleViewModel));
            var controller = CreateController();
            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseModuleViewModel.Name} unassigned successfully"
            };

            var result = await controller.EditUnAssignAsync(courseModuleViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("TrainerSchedule");
        }

        #endregion EditUnAssignAsync




        #region PutAsync

        [Fact]
        public async Task PutAsync_ShouldCallCourseModuleServicePutAsync_WhenANonNullCourseModuleIsPassed()
        {
            //Arrange
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            var controller = CreateController();
            //Act
            await controller.PutAsync(courseModuleViewModel);
            //Assert
            mockCourseModuleService.Verify(x => x.PutAsync(courseModuleViewModel), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldReturnJsontResult_WhenCalled()
        {
            //Arrange
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            var controller = CreateController();
            //Act
            var result = await controller.PutAsync(courseModuleViewModel);
            //Assert
            result.Should().BeOfType<JsonResult>();
        }

        [Fact]
        public async Task PutAsync_SetTheReturnedCourseModuleAsync_ToHaveTheRelevantDataChangedToWhatTheInputHasProvided_WhenAReturnedIsProvided()
        {
            //Arrange
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            var returnedCourseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            mockCourseModuleService.Setup(x => x.GetByIdAsync(courseModuleViewModel.Id)).ReturnsAsync(returnedCourseModuleViewModel);
            var controller = CreateController();
            var expectedCourseModuleResult = new CourseModule
            {
                Name = courseModuleViewModel.Name,
                PreparationNotes = courseModuleViewModel.PreparationNotes,
                Description = courseModuleViewModel.Description,
                StartDate = courseModuleViewModel.StartDate,
                EndDate = courseModuleViewModel.EndDate,
                VenueId = courseModuleViewModel.VenueId,
                TrainerId = courseModuleViewModel.TrainerId,
                Id = returnedCourseModuleViewModel.Id,
                PathwayId = returnedCourseModuleViewModel.PathwayId,
                Provisional = returnedCourseModuleViewModel.Provisional
            };
            //Act
            await controller.PutAsync(courseModuleViewModel);
            //Assert
            returnedCourseModuleViewModel.Should().BeEquivalentTo(expectedCourseModuleResult);
        }

        #endregion PutAsync
        #region GetByIdForEditAsyncForCourseSchedule
        [Fact]
        public async Task GetByIdForEditAsyncForCourseSchedule_ShouldCallCourseModuleServiceGetByIdForEditAsyncForCourseSchedule_Once_WhenCalled()
        {
            //Arrange
            int id = 1;
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            var controller = CreateController();
            //Act
            await controller.GetByIdForEditAsyncForCourseSchedule(id);
            //Assert
            mockCourseModuleService.Verify(x => x.GetByIdForEditAsync(id), Times.Once);
        }
        [Fact]
        public async Task GetByIdForEditAsync_ShouldReturnUpdateCourseModulePartialView_WhenCalledCourseSchedule_ShouldReturnsPartialViewEditAsync_WhenCalled()
        {
            //Arrange
            var id = 1;
           
            var controller = CreateController();
            //Act
           var result = await controller.GetByIdForEditAsyncForCourseSchedule(id) as PartialViewResult;
            //Assert
            result.ViewName.Should().BeEquivalentTo("_CourseScheduleUpdate");
        }

        #endregion
        #region EditAsyncForCourseSchedule
        [Fact]
        public async Task EditAsyncForCourseSchedule_ShouldCallCourseModuleServicePutAsync_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            mockCourseModuleService.Setup(x => x.PutAsync(courseModuleViewModel));
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMesssge"] = $"{courseModuleViewModel.Name} updated successfully"
            };
            //Act
            var result = await controller.EditAsyncForCourseSchedule(courseModuleViewModel);
            //Assert
            mockCourseModuleService.Verify(x => x.PutAsync(courseModuleViewModel), Times.Once);
        }
        [Fact]
        public async Task EditAsyncForCourseSchedule_ShouldRedirectToActionCourseSchedulePage_WhenCalled()
        {
            //Arrange

            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            var httpContext = new DefaultHttpContext();
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseModuleViewModel.Name} updated successfully"
            };
            //Act
            var result = await controller.EditAsyncForCourseSchedule(courseModuleViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("CourseSchedule");
        }

        #endregion

        #region GetByIdForEditAsync

        [Fact]
        public async Task GetByIdForEditAsync_ShouldCallCourseModuleServiceGetByIdForEditAsync_Once_WhenCalled()
        {
            //Arrange
            int id = 1;
            var httpContext = new DefaultHttpContext();
            var courseModuleViewModel= Builder<CourseModuleViewModel>.CreateNew().Build();
            mockCourseModuleService.Setup(x => x.DeleteAsync(id));

            var controller = CreateController();

            //Act
            await controller.GetByIdForEditAsync(id);

            //Assert
            mockCourseModuleService.Verify(x => x.GetByIdForEditAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdForEditAsync_ShouldReturnUpdateCourseModulePartialView_WhenCalled()
        {
            //Arrange
            int id = 1;

            var controller = CreateController();

            //Act
            var result = await controller.GetByIdForEditAsync(id) as PartialViewResult;
            //Assert

            result.ViewName.Should().BeEquivalentTo("_TrainerScheduleUpdate");
        }

        #endregion GetByIdForEditAsync



        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_ShouldCallCourseModuleServiceDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
           
            var httpContext = new DefaultHttpContext();
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            mockCourseModuleService.Setup(x => x.GetByIdAsync(courseModuleViewModel.Id));
            var controller = CreateController();
            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseModuleViewModel.Name} updated successfully"
            };
            await controller.DeleteAsync(courseModuleViewModel);
            //Assert
            mockCourseModuleService.Verify(x => x.DeleteAsync(courseModuleViewModel.Id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRedirectToActionTrainerSchedulePage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            mockCourseModuleService.Setup(x => x.GetByIdAsync(courseModuleViewModel.Id));
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseModuleViewModel.Name} deleted successfully"
            };
            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseModuleViewModel.Name} updated successfully"
            };
            var result = await controller.DeleteAsync(courseModuleViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("TrainerSchedule");
        }

        #endregion DeleteAsync

        #region DeleteAsyncForCourseSchedule

        [Fact]
        public async Task DeleteAsyncForCourseSchedule_ShouldCallCourseModuleServiceDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
           
            var httpContext = new DefaultHttpContext();
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            mockCourseModuleService.Setup(x => x.GetByIdAsync(courseModuleViewModel.Id));
            var controller = CreateController();
            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseModuleViewModel.Name} updated successfully"
            };
            await controller.DeleteAsyncForCourseSchedule(courseModuleViewModel);
            //Assert
            mockCourseModuleService.Verify(x => x.DeleteAsync(courseModuleViewModel.Id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsyncForCourseSchedule_ShouldRedirectToActionCourseSchedulePage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            mockCourseModuleService.Setup(x => x.GetByIdAsync(courseModuleViewModel.Id));
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseModuleViewModel.Name} deleted successfully"
            };
         
            var result = await controller.DeleteAsyncForCourseSchedule(courseModuleViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("CourseSchedule");
        }

        #endregion DeleteAsyncForCourseSchedule

        #region PutUnAssignAsync

        [Fact]
        public async Task PutUnAssignAsync_ShouldCallCourseModuleServiceUnAssignAsync_WhenCalled()
        {
            //Arrange
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            var controller = CreateController();
            //Act
            await controller.PutUnAssign(courseModuleViewModel);
            //Assert
            mockCourseModuleService.Verify(x => x.PutUnAssignAsync(courseModuleViewModel), Times.Once);
        }

        [Fact]
        public async Task PutUnAssignAsync_ShouldReturnJsonResult_WhenCalled()
        {
            //Arrange
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            var controller = CreateController();
            //Act
            var result = await controller.PutUnAssign(courseModuleViewModel);
            //Assert
            result.Should().BeOfType<JsonResult>();
        }

        #endregion PutUnAssignAsync
    }
}