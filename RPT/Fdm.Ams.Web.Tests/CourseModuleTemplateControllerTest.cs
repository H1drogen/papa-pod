using System.Linq;
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
    public class CourseModuleTemplateControllerTest
    {
        private readonly Mock<ILogger<CourseModuleTemplateController>> logger;
        private readonly Mock<ICourseModuleTemplateService> mockCourseModuleTemplateService;

        public CourseModuleTemplateControllerTest()
        {
            mockCourseModuleTemplateService = new Mock<ICourseModuleTemplateService>();
            logger = new Mock<ILogger<CourseModuleTemplateController>>();
        }

        public CourseModuleTemplateController ControllerHelper()
        {
            return new CourseModuleTemplateController(mockCourseModuleTemplateService.Object, logger.Object);
        }

        #region DeleteAsync

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        [InlineData(-int.MaxValue)]
        public async Task DeleteAsync_CallsDeleteAsync_Once(int id)
        {
            //Arrange
            var controller = ControllerHelper();
            //Act
            await controller.DeleteAsync(id);
            //Assert
            mockCourseModuleTemplateService.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        #endregion DeleteAsync

        #region GetByPathwayTemplateIdAsync

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        [InlineData(-int.MaxValue)]
        public async Task GetByPathwayTemplateId_CallsGetAllByPathwayTemplateIdAsync_Once(int id)
        {
            //arrange
            var controller = ControllerHelper();
            //Act
            await controller.GetByPathwayTemplateIdAsync(id);
            //Assert
            mockCourseModuleTemplateService.Verify(x => x.GetAllByPathwayTemplateIdAsync(id), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        [InlineData(-int.MaxValue)]
        public async Task GetByPathwayTemplateIdAsync_ReturnsCourseModuleTemplateByCourseTemplate_WhenIdIsPassed(int pathwayTemplateId)
        {
            //arrange
            var controller = ControllerHelper();
            var courseModuleTemplate = Builder<CourseModuleTemplate>.CreateListOfSize(5)
                .Random(1)
                .With(x => x.PathwayTemplateId = pathwayTemplateId)
                .Build();
            var returnedFromService = courseModuleTemplate.Where(x => x.PathwayTemplateId == pathwayTemplateId);
            mockCourseModuleTemplateService.Setup(x => x.GetAllByPathwayTemplateIdAsync(pathwayTemplateId)).ReturnsAsync(returnedFromService);
            var expected = new JsonResult(returnedFromService);
            //Act
            var result = (await controller.GetByPathwayTemplateIdAsync(pathwayTemplateId));
            //Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        [InlineData(-int.MaxValue)]
        public async Task GetByPathwayTemplateIdAsync_ReturnsIActionResult_WhenIdIsPassed(int pathwayTemplateId)
        {
            //arrange
            var controller = ControllerHelper();
            var courseModuleTemplate = Builder<CourseModuleTemplate>.CreateListOfSize(5)
                .Random(1)
                .With(x => x.PathwayTemplateId = pathwayTemplateId)
                .Build();
            var returnedFromService = courseModuleTemplate.Where(x => x.PathwayTemplateId == pathwayTemplateId);
            mockCourseModuleTemplateService.Setup(x => x.GetAllByPathwayTemplateIdAsync(pathwayTemplateId)).ReturnsAsync(returnedFromService);
            //Act
            var result = (await controller.GetByPathwayTemplateIdAsync(pathwayTemplateId));
            //Assert
            result.Should().BeOfType<JsonResult>();
        }

        #endregion GetByPathwayTemplateIdAsync

        #region GetByNullPathwayTemplateIdAsync

        [Fact]
        public async Task GetByNullPathwayTemplateId_CallsGetAllByNullPathwayTemplateIdAsync_Once()
        {
            //arrange
            var controller = ControllerHelper();
            //Act
            await controller.GetByNullPathwayTemplateIdAsync();
            //Assert
            mockCourseModuleTemplateService.Verify(x => x.GetAllByNullPathwayTemplateIdAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByNullPathwayTemplateIdAsync_ReturnsAllCourseModuleTemplateWhereNull()
        {
            //arrange
            var controller = ControllerHelper();
            var courseModuleTemplate = Builder<CourseModuleTemplate>.CreateListOfSize(5)
                .Random(1)
                .With(x => x.PathwayTemplateId = null)
                .Build();
            var returnedFromService = courseModuleTemplate.Where(x => x.PathwayTemplateId == null);
            mockCourseModuleTemplateService.Setup(x => x.GetAllByNullPathwayTemplateIdAsync()).ReturnsAsync(returnedFromService);
            var expected = new JsonResult(returnedFromService);
            //Act
            var result = (await controller.GetByNullPathwayTemplateIdAsync());
            //Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetByNullPathwayTemplateIdAsync_ReturnsIActionResult_WhenIdIsNull()
        {
            //arrange
            var controller = ControllerHelper();
            var courseModuleTemplate = Builder<CourseModuleTemplate>.CreateListOfSize(5)
                .Random(1)
                .With(x => x.PathwayTemplateId = null)
                .Build();
            var returnedFromService = courseModuleTemplate.Where(x => x.PathwayTemplateId == null);
            mockCourseModuleTemplateService.Setup(x => x.GetAllByNullPathwayTemplateIdAsync()).ReturnsAsync(returnedFromService);
            //Act
            var result = (await controller.GetByNullPathwayTemplateIdAsync());
            //Assert
            result.Should().BeOfType<JsonResult>();
        }

        #endregion GetByNullPathwayTemplateIdAsync

        #region Index

        [Fact]
        public void Index_ShouldNotReturn_Null()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var controller = ControllerHelper();
            var viewModel = Builder<CourseModuleTemplateViewModel>.CreateNew().Build();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{viewModel.Name} created successfully"
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
            mockCourseModuleTemplateService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsSerializedVersionOfEntityReturnedFromGetAllAsync_WhenCalled()
        {
            //Arrange
            var controller = ControllerHelper();
            var viewModel = Builder<CourseModuleTemplateViewModel>.CreateListOfSize(4).Build();
            mockCourseModuleTemplateService.Setup(x => x.GetAllAsync()).ReturnsAsync(viewModel);
            var expectedResult = new JsonResult(viewModel);
            //Act
            var result = await controller.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetAllAsync

        #region GetByIdAsync

        [Fact]
        public async Task GetAByIdAsync_ReturnsSerializedVersionOfEntityReturnedFromGetByIdAsync_WhenCalled()
        {
            // Arrange
            int id = 1;
            var controller = ControllerHelper();
            var viewModel = Builder<CourseModuleTemplateViewModel>.CreateNew().Build();
            mockCourseModuleTemplateService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(viewModel);
            var expectedResult = new JsonResult(viewModel);
            //Act
            var result = await controller.GetByIdAsync(id);
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetByIdAsync_CallsGetByIdAsync_Once()
        {
            //Arrange
            int id = 1;
            var controller = ControllerHelper();
            //Act
            await controller.GetByIdAsync(id);
            //Asset
            mockCourseModuleTemplateService.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        #endregion GetByIdAsync

        #region PostAsync

        [Fact]
        public async Task PostAsync_CallsCourseModuleTemplateServicePostAsyncOnce_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var viewModel = Builder<CourseModuleTemplateViewModel>.CreateNew().Build();
            var controller = ControllerHelper();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{viewModel.Name} created successfully"
            };

            //Act
            await controller.PostAsync(viewModel);

            //Assert
            mockCourseModuleTemplateService.Verify(x => x.PostAsync(viewModel), Times.Once);
        }

        [Fact]
        public async Task PostAsync_RedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var viewModel = Builder<CourseModuleTemplateViewModel>.CreateNew().Build();
            mockCourseModuleTemplateService.Setup(x => x.PostAsync(viewModel));
            var controller = ControllerHelper();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{viewModel.Name} created successfully"
            };
            //Act
            var result = await controller.PostAsync(viewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion PostAsync

        #region GetByIdForEditAsync

        [Fact]
        public async Task GetByIdForEditAsync_CallsCourseModuleTemplateServiceGetByIdAsync_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = ControllerHelper();

            //Act
            await controller.GetByIdForEditAsync(id);

            //Assert
            mockCourseModuleTemplateService.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdForEditAsync_ShouldReturnPartialView_WhenCalled()
        {
            //Arrange
            int id = 1;

            var controller = ControllerHelper();

            //Act
            var result = await controller.GetByIdForEditAsync(id) as PartialViewResult;
            //Assert
            result.ViewName.Should().BeEquivalentTo("_UpdateCourseModuleTemplate");
        }

        #endregion GetByIdForEditAsync

        #region EditAsync

        [Fact]
        public async Task EditAsync_CallsCourseModuleTemplateServicePutAsync_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var viewModel = Builder<CourseModuleTemplateViewModel>.CreateNew().Build();
            var controller = ControllerHelper();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{viewModel.Name} updated successfully"
            };

            //Act
            await controller.EditAsync(viewModel);

            //Assert
            mockCourseModuleTemplateService.Verify(x => x.PutAsync(viewModel), Times.Once);
        }

        [Fact]
        public async Task EditAsync_RedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var viewModel = Builder<CourseModuleTemplateViewModel>.CreateNew().Build();
            mockCourseModuleTemplateService.Setup(x => x.PutAsync(viewModel));
            var controller = ControllerHelper();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{viewModel.Name} updated successfully"
            };
            //Act
            var result = await controller.EditAsync(viewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion EditAsync
    }
}