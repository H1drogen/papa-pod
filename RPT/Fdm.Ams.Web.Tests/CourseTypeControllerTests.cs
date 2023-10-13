using Fdm.Ams.Models;
using Fdm.Ams.Services;
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
    public class CourseTypeControllerTests
    {
        private readonly Mock<ILogger<CourseTypeController>> logger;
        private readonly Mock<ICourseTypeService> mockCourseTypeService;

        public CourseTypeControllerTests()
        {
            mockCourseTypeService = new Mock<ICourseTypeService>();
            logger = new Mock<ILogger<CourseTypeController>>();
        }

        private CourseTypeController ControllerHelper()
        {
            return new CourseTypeController(mockCourseTypeService.Object, logger.Object);
        }

        #region Index

        [Fact]
        public void Index_ShouldNotReturn_Null()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var controller = ControllerHelper();
            var courseTypeViewModel = Builder<CourseTypeViewModel>.CreateNew().Build();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseTypeViewModel.Name} created successfully"
            };

            // Act
            var result = controller.Index();

            // Assert
            result.Should().NotBeNull();
        }

        #endregion Index


        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsGetAllAsync_Once()
        {
            //Arrange
            var controller = ControllerHelper();
            //Act
            await controller.GetAllAsync();
            //Asset
            mockCourseTypeService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsSerializedVersionOfEntityReturnedFromGetAllAsync_WhenCalled()
        {
            //Arrange
            var controller = ControllerHelper();
            var course = Builder<CourseType>.CreateListOfSize(4).Build();

            var courseTypeViewModel = Builder<CourseTypeViewModel>.CreateListOfSize(4).Build();
            mockCourseTypeService.Setup(x => x.GetAllAsync()).ReturnsAsync(courseTypeViewModel);
            var expectedResult = new JsonResult(course);
            //Act
            var result = await controller.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetAllAsync

        #region GetByIdAsync

        [Fact]
        public async Task GetByIdAsync_CallsGetByIdAsync_Once()
        {
            //Arrange
            int id = 1;
            var controller = ControllerHelper();
            //Act
            await controller.GetByIdAsync(id);
            //Asset
            mockCourseTypeService.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsSerializedVersionOfEntityReturnedFromGetAsync_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = ControllerHelper();
            var courseType = Builder<CourseType>.CreateNew().Build();
            var courseTypeViewModel = Builder<CourseTypeViewModel>.CreateNew().Build();
            mockCourseTypeService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(courseTypeViewModel);
            var expectedResult = new JsonResult(courseType);

            //Act
            var result = await controller.GetByIdAsync(id);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetByIdAsync

        #region PostAsync

        [Fact]
        public async Task PostAsync_CallsCourseTypeServicePostAsyncOnce_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var courseTypeViewModel = Builder<CourseTypeViewModel>.CreateNew().Build();
            var controller = ControllerHelper();
            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseTypeViewModel.Name} created successfully"
            };
            await controller.PostAsync(courseTypeViewModel);
            //Assert
            mockCourseTypeService.Verify(x => x.PostAsync(courseTypeViewModel), Times.Once);
        }


        [Fact]
        public async Task PostAsync_RedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var courseTypeViewModel = Builder<CourseTypeViewModel>.CreateNew().Build();
            mockCourseTypeService.Setup(x => x.PostAsync(courseTypeViewModel));
            var controller = ControllerHelper();

            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseTypeViewModel.Name} created successfully"
            };
            var result = await controller.PostAsync(courseTypeViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion PostAsync


        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_CallsCourseTypeServiceDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var httpContext = new DefaultHttpContext();
            var courseTypeViewModel = Builder<CourseTypeViewModel>.CreateNew().Build();
            mockCourseTypeService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(courseTypeViewModel);
            var controller = ControllerHelper();

            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseTypeViewModel.Name} Deleted successfully"
            };
            await controller.DeleteAsync(id);
            //Assert
            mockCourseTypeService.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            int id = 1;
            var httpContext = new DefaultHttpContext();
            var courseTypeViewModel = Builder<CourseTypeViewModel>.CreateNew().Build();
            mockCourseTypeService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(courseTypeViewModel);
            var controller = ControllerHelper();
            //Act
            
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseTypeViewModel.Name} Deleted successfully"
            };
            var result = await controller.DeleteAsync(id);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion DeleteAsync

        #region GetByIdForEditAsync

        [Fact]
        public async Task GetByIdForEditAsync_CallsCourseTypeServiceGetByIdAsync_Once_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = ControllerHelper();

            //Act
            await controller.GetByIdForEditAsync(id);

            //Assert
            mockCourseTypeService.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdForEditAsync_ShouldReturnUpdateCourseTypePartialView_WhenCalled()
        {
            //Arrange
            int id = 1;

            var controller = ControllerHelper();

            //Act
            var result = await controller.GetByIdForEditAsync(id) as PartialViewResult;
            //Assert

            result.ViewName.Should().BeEquivalentTo("UpdateCourseType");
        }

        #endregion GetByIdForEditAsync



        #region PutAsync

        [Fact]
        public async Task PutAsync_CallsCourseTypeService_WhenANonNullCourseTypeIsPassed()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var courseTypeViewModel = Builder<CourseTypeViewModel>.CreateNew().Build();
            var controller = ControllerHelper();
            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseTypeViewModel.Name} updated successfully"
            };
            await controller.PutAsync(courseTypeViewModel);
            //Assert
            mockCourseTypeService.Verify(x => x.PutAsync(courseTypeViewModel), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var courseTypeViewModel = Builder<CourseTypeViewModel>.CreateNew().Build();
            mockCourseTypeService.Setup(x => x.PutAsync(courseTypeViewModel));
            var controller = ControllerHelper();
            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseTypeViewModel.Name} updated successfully"
            };

            var result = await controller.PutAsync(courseTypeViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion PutAsync

    }
}