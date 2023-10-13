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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Fdm.Ams.Web.Tests
{
    public class CourseControllerTest
    {
        private readonly Mock<ILogger<CourseController>> logger;
        private readonly Mock<ICourseModuleService> mockCourseModuleService;
        private readonly Mock<ICourseModuleTemplateService> mockCourseModuleTemplateService;
        private readonly Mock<ICourseService> mockCourseService;
        private readonly Mock<ICourseTypeService> mockCourseTypeService;
        private readonly Mock<IRegionService> mockRegionService;
        private readonly Mock<ICourseTemplateService> mockTemplateService;

        public CourseControllerTest()
        {
            mockCourseService = new Mock<ICourseService>();
            logger = new Mock<ILogger<CourseController>>();
            mockCourseModuleTemplateService = new Mock<ICourseModuleTemplateService>();
            mockCourseModuleService = new Mock<ICourseModuleService>();
            mockCourseTypeService = new Mock<ICourseTypeService>();
            mockRegionService = new Mock<IRegionService>();
            mockTemplateService = new Mock<ICourseTemplateService>();
        }

        private CourseController ControllerHelper()
        {
            return new CourseController(mockCourseService.Object,
                logger.Object,
                mockCourseModuleTemplateService.Object,
                mockCourseModuleService.Object,
                mockCourseTypeService.Object, mockRegionService.Object, mockTemplateService.Object);
        }

        #region Index

        [Fact]
        public void Index_ShouldNotReturn_Null()
        {
            // Arrange
            var controller = ControllerHelper();

            // Act
            var result = controller.Index();

            // Assert
            result.Should().NotBeNull();
        }

        #endregion Index

        #region CourseSchedule

        [Fact]
        public void CourseSchedule_ShouldNotReturn_Null()
        {
            // Arrange
            var (courseViewModel, httpContext) = GetBuiltModelViewModel();
            var controller = ControllerHelper();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMesssge"] = $"{courseViewModel.PathwayCode}{"created successfully"}"
            };
            // Act
            var result = controller.CourseSchedule();

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task CourseSchedule_ShouldReturnsCourseViewModel_WhenCalled()
        {
            //Arrange

            var controller = ControllerHelper();
            var (courseViewModel, httpContext) = GetBuiltModelViewModel();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMesssge"] = $"{courseViewModel.PathwayCode}{"created successfully"}"
            };
            mockCourseService.Setup(x => x.GetOfficesProgrammesCourseTemplatesAndTimeZones()).ReturnsAsync(courseViewModel);
            //Act
            var result = await controller.CourseSchedule() as ViewResult;

            //Assert
            result.Model.Should().BeEquivalentTo(courseViewModel, options =>
            {
                options.Excluding(cv => cv.PathwayTemplateId);
                options.Excluding(cv => cv.PathwayTypeId);
                options.Excluding(cv => cv.OfficeId);
                options.Excluding(cv => cv.CourseTemplatesDictionary);
                options.Excluding(cv => cv.OfficesDictionary);
                options.Excluding(cv => cv.ProgrammesDictionary);
                return options;
            });
        }

        #endregion CourseSchedule

        #region GetCourseCode

        [Fact]
        public async Task GetCourseCode_ShouldCallsCourseServiceGetCourseCodeWhenCalled()
        {
            var controller = ControllerHelper();
            int regionId = 1;
            int templateId = 1;
            int year = 22;
            int programmeId = 1;

            var course = Builder<Course>.CreateNew().Build();
            mockCourseService.Setup(x => x.GetCourseCode(regionId, templateId, year, programmeId)).ReturnsAsync(course.PathwayCode);
            var expecedResult = new JsonResult(course.PathwayCode);

            //Act
            var result = await controller.GetCourseCode(regionId, templateId, year, programmeId) as JsonResult;

            //Assert
            result.Should().BeEquivalentTo(expecedResult);
        }

        #endregion GetCourseCode

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsGetAllAsync_Once()
        {
            //Arrange
            var controller = ControllerHelper();
            //Act
            await controller.GetAllAsync();
            //Asset
            mockCourseService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsSerializedVersionOfEntityReturnedFromGetAllAsync_WhenCalled()
        {
            //Arrange
            var controller = ControllerHelper();
            var course = Builder<Course>.CreateListOfSize(4).Build();
            mockCourseService.Setup(x => x.GetAllAsync()).ReturnsAsync(course);
            var expectedResult = new JsonResult(course);
            //Act
            var result = await controller.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetAllAsync

        #region GetByIdAsync

        [Theory]
        [InlineData(3)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public async Task GetByIdAsync_CallsGetByIdAsync_Once(int id)
        {
            var controller = ControllerHelper();
            //Act
            await controller.GetByIdAsync(id);
            //Asset
            mockCourseService.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public async Task GetByIdAsync_ReturnsSerializedVersionOfEntityReturnedFromGetByIdAsync_WhenCalled(int id)
        {
            var controller = ControllerHelper();
            var courseViewModel = Builder<CourseViewModel>.CreateNew().Build();
            mockCourseService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(courseViewModel);
            var expecedResult = new JsonResult(courseViewModel);

            //Act
            var result = await controller.GetByIdAsync(id);

            //Assert
            result.Should().BeEquivalentTo(expecedResult);
        }

        #endregion GetByIdAsync

        #region GetDetailsAsync

        [Fact]
        public async Task GetDetailsAsync_ShouldGetCoursesByPathwayIdOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = ControllerHelper();
            var (courseViewModel, httpContext) = GetBuiltModelViewModel();

            mockCourseService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(courseViewModel);
            mockCourseService.Setup(x => x.GetProgrammeRegionPathwayTypes()).ReturnsAsync(courseViewModel);
            mockCourseService.Setup(x => x.GetCoursesByPathwayId(id)).ReturnsAsync(courseViewModel);
            //Act
            await controller.GetDetailsAsync(id);

            //Assert
            mockCourseService.Verify(x => x.GetCoursesByPathwayId(id), Times.Once);
        }

        [Fact]
        public async Task GetDetailsAsync_ShouldReturnPartialView_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = ControllerHelper();
            var (courseViewModel, httpContext) = GetBuiltModelViewModel();

            mockCourseService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(courseViewModel);
            mockCourseService.Setup(x => x.GetProgrammeRegionPathwayTypes()).ReturnsAsync(courseViewModel);
            mockCourseService.Setup(x => x.GetCoursesByPathwayId(1)).ReturnsAsync(courseViewModel);

            //Act
            var result = await controller.GetDetailsAsync(id) as PartialViewResult;
            //Assert
            result.ViewName.Should().BeEquivalentTo("_Details");
        }

        [Fact]
        public async Task GetDetailsAsync_ShouldReturnViewWithModel_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = ControllerHelper();
            var (courseViewModel, httpContext) = GetBuiltModelViewModel();

            mockCourseService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(courseViewModel);
            mockCourseService.Setup(x => x.GetProgrammeRegionPathwayTypes()).ReturnsAsync(courseViewModel);
            mockCourseService.Setup(x => x.GetCoursesByPathwayId(id)).ReturnsAsync(courseViewModel);

            //Act
            var result = await controller.GetDetailsAsync(id) as PartialViewResult;

            //Assert
            result.Model.Should().BeEquivalentTo(courseViewModel);
        }

        #endregion GetDetailsAsync

        #region PostAsync

        [Fact]
        public async Task PostAsync_ShouldCallsCourseModuleServicePostAsyncOnce_WhenCalled()
        {
            //Arrange
            var courseModuleTemplateList = Builder<CourseModuleTemplate>.CreateListOfSize(3).Build();
            var controller = ControllerHelper();
            var (courseViewModel, httpContext) = GetBuiltModelViewModel();

            mockCourseModuleService.Setup(x => x.PostListOfCourseModules(courseViewModel, courseModuleTemplateList));
            mockCourseService.Setup(x => x.PostAsync(courseViewModel)).ReturnsAsync(courseViewModel);
            mockCourseModuleTemplateService.Setup(x => x.GetAllByPathwayTemplateIdAsync(courseViewModel.PathwayTemplateId.Value)).ReturnsAsync(courseModuleTemplateList);
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMesssge"] = $"{courseViewModel.PathwayCode}{"created successfully"}"
            };

            //Act
            await controller.PostAsync(courseViewModel);
            //Assert
            mockCourseModuleService.Verify(x => x.PostListOfCourseModules(courseViewModel, courseModuleTemplateList), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldCallsCourseServicePostAsyncOnce_WhenCalled()
        {
            //Arrange
            var (courseViewModel, httpContext) = GetBuiltModelViewModel();

            var controller = ControllerHelper();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMesssge"] = $"{courseViewModel.PathwayCode}{"created successfully"}"
            };
            mockCourseService.Setup(x => x.PostAsync(courseViewModel)).ReturnsAsync(courseViewModel);

            //Act
            await controller.PostAsync(courseViewModel);
            //Assert
            mockCourseService.Verify(x => x.PostAsync(courseViewModel), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldRedirectToActionCourseSchedulePage_WhenCalled()
        {
            //Arrange
            var (courseViewModel, httpContext) = GetBuiltModelViewModel();

            var controller = ControllerHelper();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseViewModel.PathwayCode}{"created successfully"}"
            };
            mockCourseService.Setup(x => x.PostAsync(courseViewModel)).ReturnsAsync(courseViewModel);

            //Act
            var result = await controller.PostAsync(courseViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("CourseSchedule");
        }

        #endregion PostAsync

        #region Helper Method

        private (CourseViewModel viewModel,
            HttpContext httpContext)
            GetBuiltModelViewModel()
        {
            var courseModuleTemplateList = Builder<CourseModuleTemplate>.CreateListOfSize(3).Build();
            var course = Builder<Course>.CreateNew().Build();
            var httpContext = new DefaultHttpContext();
            var courseTemplatesList = Builder<CourseTemplate>.CreateListOfSize(3).Build();
            var regionsList = Builder<Region>.CreateListOfSize(3).Build();
            var programmesList = Builder<Programme>.CreateListOfSize(3).Build();
            var courseModuleList = Builder<CourseModule>.CreateListOfSize(3).Build();
            var pathwayTypeList = Builder<CourseType>.CreateListOfSize(3).Build();

            Dictionary<int, string> courseTemplatesDictionary = new();
            Dictionary<int, string> regionsDictionary = new();
            Dictionary<int, string> programmesDictionary = new();
            Dictionary<int, string> courseModuleDictionary = new();
            Dictionary<int, string> pathwayTypeDictionary = new();

            courseTemplatesList.ToList().ForEach(x => courseTemplatesDictionary.Add(x.Id, x.Name));
            regionsList.ToList().ForEach(x => regionsDictionary.Add(x.Id, x.Name));
            programmesList.ToList().ForEach(x => programmesDictionary.Add(x.Id, x.Name));
            courseModuleList.ToList().ForEach(x => courseModuleDictionary.Add(x.Id, x.Name));

            var courseViewModel = Builder<CourseViewModel>.CreateNew()
              .With(x => x.RegionsDictionary = regionsDictionary)
              .With(x => x.ProgrammesDictionary = programmesDictionary)
              .With(x => x.PathwayTypesDictionary = pathwayTypeDictionary)
              .With(x => x.CoursesDictionary = courseModuleDictionary)
              .Build();

            return (courseViewModel, httpContext);
        }

        #endregion Helper Method
    }
}