using AutoMapper;
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
using System.Threading.Tasks;
using Xunit;

namespace Fdm.Ams.Web.Tests
{
    public class RegionControllerTests
    {
        private Mock<ILogger<RegionController>> mockLogger;
        private Mock<IMapper> mockMapper;
        private Mock<IRegionService> mockRegionService;

        public RegionControllerTests()
        {
            mockLogger = new Mock<ILogger<RegionController>>();
            mockRegionService = new Mock<IRegionService>();
            mockMapper = new Mock<IMapper>();
        }

        private RegionController CreateController()
        {
            return new RegionController(mockRegionService.Object, mockLogger.Object);
        }

        #region Index

        [Fact]
        public void Index_ShouldNotReturnANullResult_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var controller = CreateController();
            var regionViewModel = Builder<RegionViewModel>.CreateNew().Build();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{regionViewModel.Name} created successfully"
            };

            //Act
            var result = controller.Index();

            //Assert
            result.Should().NotBeNull();
        }

        #endregion Index

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsRegionServiceGetAllAsync_WhenCalled()
        {
            //Arrange
            var controller = CreateController();
            //Act
            await controller.GetAllAsync();
            //Assert
            mockRegionService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsJsonResult_WithCorrectData_WhenCalled()
        {
            //Arrange
            var list = Builder<Region>.CreateListOfSize(3).Build();
            mockRegionService.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
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
        public async Task PostAsync_ShouldCallCourseModuleTemplateServicePostAsyncOnce_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var regionViewModel = Builder<RegionViewModel>.CreateNew().Build();
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{regionViewModel.Name} created successfully"
            };

            //Act
            await controller.PostAsync(regionViewModel);

            //Assert
            mockRegionService.Verify(x => x.PostAsync(regionViewModel), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var regionViewModel = Builder<RegionViewModel>.CreateNew().Build();
            mockRegionService.Setup(x => x.PostAsync(regionViewModel));
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{regionViewModel.Name} created successfully"
            };
            //Act
            var result = await controller.PostAsync(regionViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion PostAsync

        #region EditAsync

        [Fact]
        public async Task EditAsync_ShouldCallRegionServicePutAsyncOnce_WhenANonNullRegionIsPassed()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var regionViewModel = Builder<RegionViewModel>.CreateNew().Build();
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{regionViewModel.Name} created successfully"
            };

            //Act
            await controller.EditAsync(regionViewModel);

            //Assert
            mockRegionService.Verify(x => x.PutAsync(regionViewModel), Times.Once);
        }

        [Fact]
        public async Task EditAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var regionViewModel = Builder<RegionViewModel>.CreateNew().Build();
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{regionViewModel.Name} created successfully"
            };

            //Act
            var result = await controller.EditAsync(regionViewModel);

            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion EditAsync

        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_CallsRegionServiceDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var httpContext = new DefaultHttpContext();
            var regionViewModel = Builder<RegionViewModel>.CreateNew().Build();
            mockRegionService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(regionViewModel);
            var controller = CreateController();
            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{regionViewModel.Name} archived successfully"
            };
            await controller.DeleteAsync(id);
            //Assert
            mockRegionService.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            int id = 1;
            var httpContext = new DefaultHttpContext();
            var regionViewModel = Builder<RegionViewModel>.CreateNew().Build();
            mockRegionService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(regionViewModel);
            var controller = CreateController();
            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{regionViewModel.Name} archived successfully"
            };
            var result = await controller.DeleteAsync(id);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion DeleteAsync
    }
}