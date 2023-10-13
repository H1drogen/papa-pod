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
    public class ProgrammeControllerTest
    {
        private Mock<ILogger<ProgrammeController>> mockLogger;
        private Mock<IProgrammeService> mockProgrammeService;

        public ProgrammeControllerTest()
        {
            mockLogger = new Mock<ILogger<ProgrammeController>>();
            mockProgrammeService = new Mock<IProgrammeService>();
        }

        private ProgrammeController CreateController()
        {
            return new ProgrammeController(mockProgrammeService.Object, mockLogger.Object);
        }

        #region Index

        [Fact]
        public void Index_ShouldNotReturnANullResult_WhenCalled()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var controller = CreateController();
            var programmeViewModel = Builder<ProgrammeViewModel>.CreateNew().Build();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{programmeViewModel.Name} created successfully"
            };

            // Act
            var result = controller.Index();

            // Assert
            result.Should().NotBeNull();
        }

        #endregion Index

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsProgrammeServiceGetAllAsync_WhenCalled()
        {
            //Arrange
            var controller = CreateController();
            //Act
            await controller.GetAllAsync();
            //Assert
            mockProgrammeService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnsJsonResult_WithCorrectData_WhenCalled()
        {
            //Arrange
            var programmeViewModelList = Builder<ProgrammeViewModel>.CreateListOfSize(3).Build();
            mockProgrammeService.Setup(x => x.GetAllAsync()).ReturnsAsync(programmeViewModelList);
            var expectedResult = new JsonResult(programmeViewModelList);
            var controller = CreateController();
            //Act
            var result = await controller.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetAllAsync

        #region PostAsync

        [Fact]
        public async Task PostAsync_CallsProgrammeServicePostAsync_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var programmeViewModel = Builder<ProgrammeViewModel>.CreateNew().Build();
            var controller = CreateController();
            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{programmeViewModel.Name} created successfully"
            };
            await controller.PostAsync(programmeViewModel);
            //Assert
            mockProgrammeService.Verify(x => x.PostAsync(programmeViewModel), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var programmeViewModel = Builder<ProgrammeViewModel>.CreateNew().Build();
            mockProgrammeService.Setup(x => x.PostAsync(programmeViewModel));
            var controller = CreateController();

            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{programmeViewModel.Name} created successfully"
            };
            var result = await controller.PostAsync(programmeViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion PostAsync

        #region EditAsync

        [Fact]
        public async Task EditAsync_ShouldCallsProgrammeServicePutAsync_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var programmeViewModel = Builder<ProgrammeViewModel>.CreateNew().Build();
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{programmeViewModel.Name} updated successfully"
            };
            //Act
            await controller.EditAsync(programmeViewModel);
            //Assert
            mockProgrammeService.Verify(x => x.PutAsync(programmeViewModel), Times.Once);
        }

        [Fact]
        public async Task EditAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var programmeViewModel = Builder<ProgrammeViewModel>.CreateNew().Build();
            mockProgrammeService.Setup(x => x.PutAsync(programmeViewModel));
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{programmeViewModel.Name} updated successfully"
            };
            //Act
            var result = await controller.EditAsync(programmeViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion EditAsync

        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_CallsProgrammeServiceDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var httpContext = new DefaultHttpContext();
            var programmeViewModel = Builder<ProgrammeViewModel>.CreateNew().Build();
            mockProgrammeService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(programmeViewModel);
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{programmeViewModel.Name} archived successfully"
            };
            //Act

            await controller.DeleteAsync(id);
            //Assert
            mockProgrammeService.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            int id = 1;
            var httpContext = new DefaultHttpContext();
            var programmeViewModel = Builder<ProgrammeViewModel>.CreateNew().Build();
            mockProgrammeService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(programmeViewModel);
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{programmeViewModel.Name} archived successfully"
            };
            //Act

            var result = await controller.DeleteAsync(id);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion DeleteAsync
    }
}