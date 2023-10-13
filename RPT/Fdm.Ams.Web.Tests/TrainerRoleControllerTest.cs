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
using System.Threading.Tasks;
using Xunit;

namespace Fdm.Ams.Web.Tests
{
    public class TrainerRoleControllerTest
    {
        private Mock<ILogger<TrainerRoleController>> mockLogger;
        private Mock<ITrainerRoleService> mockTrainerRoleService;

        public TrainerRoleControllerTest()
        {
            mockLogger = new Mock<ILogger<TrainerRoleController>>();
            mockTrainerRoleService = new Mock<ITrainerRoleService>();
        }

        private TrainerRoleController CreateController()
        {
            return new TrainerRoleController(mockTrainerRoleService.Object, mockLogger.Object);
        }

        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_ShouldCallTrainerRoleServiceDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var httpContext = new DefaultHttpContext();
            var trainerRoleViewModel = Builder<TrainerRoleViewModel>.CreateNew().Build();
            mockTrainerRoleService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(trainerRoleViewModel);
            var controller = CreateController();
            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{trainerRoleViewModel.Name} deleted successfully"
            };
            await controller.DeleteAsync(id);
            //Assert
            mockTrainerRoleService.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            int id = 1;
            var httpContext = new DefaultHttpContext();
            var trainerRoleViewModel = Builder<TrainerRoleViewModel>.CreateNew().Build();
            mockTrainerRoleService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(trainerRoleViewModel);
            var controller = CreateController();
            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{trainerRoleViewModel.Name} deleted successfully"
            };
            var result = await controller.DeleteAsync(id);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion DeleteAsync

        #region EditAsync

        [Fact]
        public async Task EditAsync_ShouldCallTrainerRoleServicePutAsyncOnce_WhenANonNulTrainerRoleIsPassed()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var trainerRoleViewModel = Builder<TrainerRoleViewModel>.CreateNew().Build();
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{trainerRoleViewModel.Name} updated successfully"
            };

            //Act
            await controller.EditAsync(trainerRoleViewModel);

            //Assert
            mockTrainerRoleService.Verify(x => x.PutAsync(trainerRoleViewModel), Times.Once);
        }

        [Fact]
        public async Task EditAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var trainerRoleViewModel = Builder<TrainerRoleViewModel>.CreateNew().Build();
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{trainerRoleViewModel.Name} updated successfully"
            };

            //Act
            var result = await controller.EditAsync(trainerRoleViewModel);

            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion EditAsync



        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_ShouldCallsTrainerRoleServiceGetAllAsync_WhenCalled()
        {
            //Arrange
            var controller = CreateController();
            //Act
            await controller.GetAllAsync();
            //Assert
            mockTrainerRoleService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnsJsonResult_WithCorrectData_WhenCalled()
        {
            //Arrange
            var list = Builder<TrainerRoleViewModel>.CreateListOfSize(3).Build();
            mockTrainerRoleService.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var expectedResult = new JsonResult(list);
            var controller = CreateController();
            //Act
            var result = await controller.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetAllAsync

        #region GetByIdAsync

        [Fact]
        public async Task GetById_ShouldCallsTrainerRoleServiceGetByIdAsync_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = CreateController();
            //Act
            await controller.GetByIdAsync(id);
            //Assert
            mockTrainerRoleService.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetById_ShouldReturnsJsonResult_WithCorrectData_WhenCalled()
        {
            //Arrange
            int id = 1;
            var trainerRoleViewModel = Builder<TrainerRoleViewModel>.CreateNew().Build();
            mockTrainerRoleService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(trainerRoleViewModel);
            var expectedResult = new JsonResult(trainerRoleViewModel);
            var controller = CreateController();
            //Act
            var result = await controller.GetByIdAsync(id);
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetByIdAsync

        #region GetByIdForEditAsync

        [Fact]
        public async Task GetByIdForEditAsync_ShouldCallTrainerRoleServiceGetByIdAsync_Once_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = CreateController();

            //Act
            await controller.GetByIdForEditAsync(id);

            //Assert
            mockTrainerRoleService.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdForEditAsync_ShouldReturnUpdateTrainerRolePartialView_WhenCalled()
        {
            //Arrange
            int id = 1;

            var controller = CreateController();

            //Act
            var result = await controller.GetByIdForEditAsync(id) as PartialViewResult;
            //Assert

            result.ViewName.Should().BeEquivalentTo("_UpdateTrainerRole");
        }

        #endregion GetByIdForEditAsync

        #region Index

        [Fact]
        public void Index_ShouldNotReturnANullResult_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var controller = CreateController();
            var trainerRoleViewModel = Builder<TrainerRoleViewModel>.CreateNew().Build();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{trainerRoleViewModel.Name} created successfully"
            };
            //Act
            var result = controller.Index();
            //Assert
            result.Should().NotBeNull();
        }

        #endregion Index

        #region PostAsync

        [Fact]
        public async Task PostAsync_ShouldCallsTrainerRoleServicePostAsync_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var trainerRoleViewModel = Builder<TrainerRoleViewModel>.CreateNew().Build();
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{trainerRoleViewModel.Name} created successfully"
            };
            //Act
            await controller.PostAsync(trainerRoleViewModel);
            //Assert
            mockTrainerRoleService.Verify(x => x.PostAsync(trainerRoleViewModel), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var trainerRoleViewModel = Builder<TrainerRoleViewModel>.CreateNew().Build();
            mockTrainerRoleService.Setup(x => x.PostAsync(trainerRoleViewModel));
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{trainerRoleViewModel.Name} created successfully"
            };
            //Act
            var result = await controller.PostAsync(trainerRoleViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion PostAsync
    }
}