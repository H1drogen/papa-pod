using System.Collections.Generic;
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
using Newtonsoft.Json;
using Xunit;

namespace Fdm.Ams.Web.Tests
{
    public class CourseTemplateControllerTest
    {
        private const string post = "/Post";
        private readonly Mock<ILogger<CourseTemplateController>> mockLogger;
        private Mock<ICourseTemplateService> mockCourseTemplateService;
        private Mock<ICourseTypeService> mockCourseTypeService;
        private Mock<IRegionService> mockRegionService;

        public CourseTemplateControllerTest()
        {
            mockLogger = new Mock<ILogger<CourseTemplateController>>();
            mockCourseTemplateService = new Mock<ICourseTemplateService>();
            mockRegionService = new Mock<IRegionService>();
            mockCourseTypeService = new Mock<ICourseTypeService>();
        }

        #region Index

        [Fact]
        public void Index_ShouldNotReturn_Null()
        {
            // Arrange
            var controller = CreateController();

            var httpContext = new DefaultHttpContext();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["Success"] = true
            };

            // Act
            var result = controller.Index();

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void Index_ShouldReturnViewDataSuccess_WhenCalled()
        {
            //Arrange
            var controller = CreateController();

            var httpContext = new DefaultHttpContext();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["Success"] = true
            };

            var keyExpectedResult = "SuccessMessage";

            //Act
            var result = controller.Index() as ViewResult;

            //Assert
            result.ViewData.Keys.Should().BeEquivalentTo(keyExpectedResult);
        }

        #endregion Index

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsCourseTemplateServiceGetAllAsync_WhenCalled()
        {
            //Arrange
            var controller = CreateController();
            //Act
            await controller.GetAllAsync();
            //Assert
            mockCourseTemplateService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsJsonResult_WithCorrectData_WhenCalled()
        {
            //Arrange
            var list = Builder<CourseTemplateViewModel>.CreateListOfSize(3).Build();
            mockCourseTemplateService.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var expectedResult = new JsonResult(list);
            var controller = CreateController();
            //Act
            var result = await controller.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetAllAsync

        #region CreateAsync

        [Fact]
        public async Task Create_RedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var eventViewModels = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).Build();
            var jsonEventViewModels = JsonConvert.SerializeObject(eventViewModels);
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew()
                .With(x => x.CourseTemplates = jsonEventViewModels)
                .Build();
            mockCourseTemplateService.Setup(x => x.DeserializeEvents(eventViewModels)).Returns(eventViewModels);
            mockCourseTemplateService.Setup(x => x.PostAsync(courseTemplateViewModel)).ReturnsAsync(courseTemplateViewModel);

            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseTemplateViewModel.Name} created successfully"
            };
            //Act
            var result = await controller.CreateAsync(courseTemplateViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        [Fact]
        public async Task CreateAsync_CallsCourseTemplateServiceCreateEventsAsyncOnce_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var eventViewModels = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).Build();
            var jsonEventViewModels = JsonConvert.SerializeObject(eventViewModels);
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew()
                .With(x => x.CourseTemplates = jsonEventViewModels)
                .Build();
            mockCourseTemplateService.Setup(x => x.DeserializeEvents(eventViewModels)).Returns(eventViewModels);
            mockCourseTemplateService.Setup(x => x.PostAsync(courseTemplateViewModel)).ReturnsAsync(courseTemplateViewModel);

            var controller = CreateController();

            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseTemplateViewModel.Name} created successfully"
            };

            //Act
            await controller.CreateAsync(courseTemplateViewModel);

            //Assert
            mockCourseTemplateService.Verify(x => x.CreateEventsAsync(courseTemplateViewModel.Id, It.IsAny<IEnumerable<CourseTemplateCalendarEventViewModel>>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_CallsCourseTemplateServiceDeserializeEventsOnce_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var eventViewModels = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).Build();
            var jsonEventViewModels = JsonConvert.SerializeObject(eventViewModels);
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew()
                .With(x => x.CourseTemplates = jsonEventViewModels)
                .Build();
            mockCourseTemplateService.Setup(x => x.DeserializeEvents(eventViewModels)).Returns(eventViewModels);
            mockCourseTemplateService.Setup(x => x.PostAsync(courseTemplateViewModel)).ReturnsAsync(courseTemplateViewModel);

            var controller = CreateController();

            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseTemplateViewModel.Name} created successfully"
            };

            //Act
            await controller.CreateAsync(courseTemplateViewModel);

            //Assert
            mockCourseTemplateService.Verify(x => x.DeserializeEvents(It.IsAny<IEnumerable<CourseTemplateCalendarEventViewModel>>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_CallsCourseTemplateServicePostAsyncOnce_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var eventViewModels = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).Build();
            var jsonEventViewModels = JsonConvert.SerializeObject(eventViewModels);
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew()
                .With(x => x.CourseTemplates = jsonEventViewModels)
                .Build();
            mockCourseTemplateService.Setup(x => x.DeserializeEvents(eventViewModels)).Returns(eventViewModels);
            mockCourseTemplateService.Setup(x => x.PostAsync(courseTemplateViewModel)).ReturnsAsync(courseTemplateViewModel);

            var controller = CreateController();

            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseTemplateViewModel.Name} created successfully"
            };

            //Act
            await controller.CreateAsync(courseTemplateViewModel);

            //Assert
            mockCourseTemplateService.Verify(x => x.PostAsync(courseTemplateViewModel), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_RedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var eventViewModels = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).Build();
            var jsonEventViewModels = JsonConvert.SerializeObject(eventViewModels);
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew()
                .With(x => x.CourseTemplates = jsonEventViewModels)
                .Build();
            mockCourseTemplateService.Setup(x => x.DeserializeEvents(eventViewModels)).Returns(eventViewModels);
            mockCourseTemplateService.Setup(x => x.PostAsync(courseTemplateViewModel)).ReturnsAsync(courseTemplateViewModel);

            var controller = CreateController();

            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseTemplateViewModel.Name} created successfully"
            };

            //Act
            var result = await controller.CreateAsync(courseTemplateViewModel);

            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion CreateAsync

        #region GetByIdForEditAsync

        [Fact]
        public async Task GetByIdForEditAsync_ShouldCallCourseTemplateServiceGetByIdAsyncOnce_WhenCalled()
        {
            //Arrange
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew().Build();
            var region = Builder<RegionViewModel>.CreateNew().Build();
            var courseTypeViewModel = Builder<CourseTypeViewModel>.CreateNew().Build();

            var controller = CreateController();

            mockCourseTemplateService.Setup(x => x.GetByIdAsync(courseTemplateViewModel.Id)).ReturnsAsync(courseTemplateViewModel);
            mockRegionService.Setup(x => x.GetByIdAsync(courseTemplateViewModel.RegionId)).ReturnsAsync(region);
            mockCourseTypeService.Setup(x => x.GetByIdAsync(courseTemplateViewModel.PathwayTypeId)).ReturnsAsync(courseTypeViewModel);

            //Act
            await controller.GetByIdForEditAsync(courseTemplateViewModel.Id);
            //Assert
            mockCourseTemplateService.Verify(x => x.GetByIdAsync(courseTemplateViewModel.Id), Times.Once);
        }

        [Fact]
        public async Task GetByIdForEditAsync_ShouldCallCourseTypeServiceGetByIdAsyncOnce_WhenCalled()
        {
            //Arrange
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew().Build();
            var region = Builder<RegionViewModel>.CreateNew().Build();
            var courseTypeViewModel = Builder<CourseTypeViewModel>.CreateNew().Build();

            var controller = CreateController();

            mockCourseTemplateService.Setup(x => x.GetByIdAsync(courseTemplateViewModel.Id)).ReturnsAsync(courseTemplateViewModel);
            mockRegionService.Setup(x => x.GetByIdAsync(courseTemplateViewModel.RegionId)).ReturnsAsync(region);
            mockCourseTypeService.Setup(x => x.GetByIdAsync(courseTemplateViewModel.PathwayTypeId)).ReturnsAsync(courseTypeViewModel);

            //Act
            await controller.GetByIdForEditAsync(courseTemplateViewModel.Id);
            //Assert
            mockCourseTypeService.Verify(x => x.GetByIdAsync(courseTemplateViewModel.PathwayTypeId), Times.Once);
        }

        [Fact]
        public async Task GetByIdForEditAsync_ShouldCallRegionServiceGetByIdAsyncOnce_WhenCalled()
        {
            //Arrange
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew().Build();
            var region = Builder<RegionViewModel>.CreateNew().Build();
            var courseTypeViewModel = Builder<CourseTypeViewModel>.CreateNew().Build();

            var controller = CreateController();

            mockCourseTemplateService.Setup(x => x.GetByIdAsync(courseTemplateViewModel.Id)).ReturnsAsync(courseTemplateViewModel);
            mockRegionService.Setup(x => x.GetByIdAsync(courseTemplateViewModel.RegionId)).ReturnsAsync(region);
            mockCourseTypeService.Setup(x => x.GetByIdAsync(courseTemplateViewModel.PathwayTypeId)).ReturnsAsync(courseTypeViewModel);

            //Act
            await controller.GetByIdForEditAsync(courseTemplateViewModel.Id);
            //Assert
            mockRegionService.Verify(x => x.GetByIdAsync(courseTemplateViewModel.RegionId), Times.Once);
        }

        [Fact]
        public async Task GetByIdForEditAsync_ShouldReturnCourseTemplateViewDataModelResult_WhenCalled()
        {
            //Arrange
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew().Build();
            var region = Builder<RegionViewModel>.CreateNew().Build();
            var courseTypeViewModel = Builder<CourseTypeViewModel>.CreateNew().Build();

            var controller = CreateController();

            mockCourseTemplateService.Setup(x => x.GetByIdAsync(courseTemplateViewModel.Id)).ReturnsAsync(courseTemplateViewModel);
            mockRegionService.Setup(x => x.GetByIdAsync(courseTemplateViewModel.RegionId)).ReturnsAsync(region);
            mockCourseTypeService.Setup(x => x.GetByIdAsync(courseTemplateViewModel.PathwayTypeId)).ReturnsAsync(courseTypeViewModel);

            //Act
            var result = await controller.GetByIdForEditAsync(courseTemplateViewModel.Id);

            //Assert
            Assert.IsType<ViewResult>(result).ViewData.Model.Should().BeEquivalentTo(courseTemplateViewModel);
        }

        [Fact]
        public async Task GetByIdForEditAsync_ShouldReturnUpdateCourseTemplateViewNameResult_WhenCalled()
        {
            //Arrange
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew().Build();
            var region = Builder<RegionViewModel>.CreateNew().Build();
            var courseTypeViewModel = Builder<CourseTypeViewModel>.CreateNew().Build();

            var controller = CreateController();

            mockCourseTemplateService.Setup(x => x.GetByIdAsync(courseTemplateViewModel.Id)).ReturnsAsync(courseTemplateViewModel);
            mockRegionService.Setup(x => x.GetByIdAsync(courseTemplateViewModel.RegionId)).ReturnsAsync(region);
            mockCourseTypeService.Setup(x => x.GetByIdAsync(courseTemplateViewModel.PathwayTypeId)).ReturnsAsync(courseTypeViewModel);

            string expectedResult = "UpdateCourseTemplate";

            //Act
            var result = await controller.GetByIdForEditAsync(courseTemplateViewModel.Id);

            //Assert
            Assert.IsType<ViewResult>(result).ViewName.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetByIdForEditAsync

        #region EditAsync(Post)

        [Fact]
        public async Task EditAsync_ShouldCallCourseTemplateCreateEventsOnce_WhenCalled()
        {
            //Arrange
            var eventViewModels = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).Build();
            var jsonEventViewModels = JsonConvert.SerializeObject(eventViewModels);
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew()
                .With(x => x.CourseTemplates = jsonEventViewModels)
                .Build();

            var controller = CreateController();

            var httpContext = new DefaultHttpContext();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["Success"] = true
            };

            //Act
            await controller.EditAsync(courseTemplateViewModel);

            //Assert
            mockCourseTemplateService.Verify(x => x.CreateEventsAsync(courseTemplateViewModel.Id, It.IsAny<IEnumerable<CourseTemplateCalendarEventViewModel>>())
            , Times.Once());
        }

        [Fact]
        public async Task EditAsync_ShouldCallCourseTemplateDeleteEventsOnce_WhenCalled()
        {
            //Arrange
            var eventViewModels = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).Build();
            var jsonEventViewModels = JsonConvert.SerializeObject(eventViewModels);
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew()
                .With(x => x.CourseTemplates = jsonEventViewModels)
                .Build();

            var controller = CreateController();

            var httpContext = new DefaultHttpContext();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["Success"] = true
            };

            //Act
            await controller.EditAsync(courseTemplateViewModel);

            //Assert
            mockCourseTemplateService.Verify(x => x.DeleteEventsAsync(It.IsAny<IEnumerable<int>>()), Times.Once());
        }

        [Fact]
        public async Task EditAsync_ShouldCallCourseTemplateDeserializeEventsOnce_WhenCalled()
        {
            //Arrange
            var eventViewModels = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).All().With(x => x.Duration = 0).With(x => x.Sequence = 0).Build();
            var jsonEventViewModels = JsonConvert.SerializeObject(eventViewModels);
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew()
                .With(x => x.CourseTemplates = jsonEventViewModels)
                .Build();

            var controller = CreateController();

            var httpContext = new DefaultHttpContext();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["Success"] = true
            };

            //Act
            await controller.EditAsync(courseTemplateViewModel);

            //Assert
            mockCourseTemplateService.Verify(x => x.DeserializeEvents(It.IsAny<IEnumerable<CourseTemplateCalendarEventViewModel>>()), Times.Once());
        }

        [Fact]
        public async Task EditAsync_ShouldCallCourseTemplateModifyEventsOnce_WhenCalled()
        {
            //Arrange
            var eventViewModels = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).Build();
            var jsonEventViewModels = JsonConvert.SerializeObject(eventViewModels);
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew()
                .With(x => x.CourseTemplates = jsonEventViewModels)
                .Build();

            var controller = CreateController();

            var httpContext = new DefaultHttpContext();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["Success"] = true
            };

            //Act
            await controller.EditAsync(courseTemplateViewModel);

            //Assert
            mockCourseTemplateService.Verify(x => x.ModifyEventsAsync(It.IsAny<IEnumerable<CourseTemplateCalendarEventViewModel>>())
            , Times.Once());
        }

        [Fact]
        public async Task EditAsync_ShouldCallCourseTemplateServicePutAsyncOnce_WhenCalled()
        {
            //Arrange
            var eventViewModels = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).All().With(x => x.Duration = 0).With(x => x.Sequence = 0).Build();
            var jsonEventViewModels = JsonConvert.SerializeObject(eventViewModels);
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew()
                .With(x => x.CourseTemplates = jsonEventViewModels)
                .Build();

            var httpContext = new DefaultHttpContext();
            var controller = CreateController();

            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseTemplateViewModel.Name} updated successfully"
            };

            //Act
            await controller.EditAsync(courseTemplateViewModel);

            //Assert
            mockCourseTemplateService.Verify(x => x.PutAsync(courseTemplateViewModel), Times.Once());
        }

        [Fact]
        public async Task EditAsync_ShouldCallCourseTemplateSortEventsToBeCreatedOnce_WhenCalled()
        {
            //Arrange
            var eventViewModels = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).Build();
            var jsonEventViewModels = JsonConvert.SerializeObject(eventViewModels);
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew()
                .With(x => x.CourseTemplates = jsonEventViewModels)
                .Build();

            var controller = CreateController();

            var httpContext = new DefaultHttpContext();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["Success"] = true
            };

            //Act
            await controller.EditAsync(courseTemplateViewModel);

            //Assert
            mockCourseTemplateService.Verify(x => x.SortEventsToBeCreatedAsync(courseTemplateViewModel.Id, It.IsAny<IEnumerable<CourseTemplateCalendarEventViewModel>>())
            , Times.Once());
        }

        [Fact]
        public async Task EditAsync_ShouldCallCourseTemplateSortEventsToBeDeletedOnce_WhenCalled()
        {
            //Arrange
            var eventViewModels = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).Build();
            var jsonEventViewModels = JsonConvert.SerializeObject(eventViewModels);
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew()
                .With(x => x.CourseTemplates = jsonEventViewModels)
                .Build();

            var controller = CreateController();

            var httpContext = new DefaultHttpContext();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["Success"] = true
            };

            //Act
            await controller.EditAsync(courseTemplateViewModel);

            //Assert
            mockCourseTemplateService.Verify(x => x.SortEventsToBeDeletedAsync(courseTemplateViewModel.Id, It.IsAny<IEnumerable<CourseTemplateCalendarEventViewModel>>())
            , Times.Once());
        }

        [Fact]
        public async Task EditAsync_ShouldCallCourseTemplateSortEventsToBeModifiedOnce_WhenCalled()
        {
            //Arrange
            var eventViewModels = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).Build();
            var jsonEventViewModels = JsonConvert.SerializeObject(eventViewModels);
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew()
                .With(x => x.CourseTemplates = jsonEventViewModels)
                .Build();

            var controller = CreateController();

            var httpContext = new DefaultHttpContext();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["Success"] = true
            };

            //Act
            await controller.EditAsync(courseTemplateViewModel);

            //Assert
            mockCourseTemplateService.Verify(x => x.SortEventsToBeModifiedAsync(courseTemplateViewModel.Id, It.IsAny<IEnumerable<CourseTemplateCalendarEventViewModel>>())
            , Times.Once());
        }

        [Fact]
        public async Task EditAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var eventViewModels = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).All().With(x => x.Duration = 0).With(x => x.Sequence = 0).Build();
            var jsonEventViewModels = JsonConvert.SerializeObject(eventViewModels);
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew()
                .With(x => x.CourseTemplates = jsonEventViewModels)
                .Build();

            var httpContext = new DefaultHttpContext();
            var controller = CreateController();

            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseTemplateViewModel.Name} updated successfully"
            };

            var actionNameResult = "Index";

            //Act
            var result = await controller.EditAsync(courseTemplateViewModel);

            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo(actionNameResult);
        }

        #endregion EditAsync(Post)

        private CourseTemplateController CreateController()
        {
            return new CourseTemplateController(mockLogger.Object, mockCourseTemplateService.Object, mockRegionService.Object, mockCourseTypeService.Object);
        }

        #region Create

        [Fact]
        public async Task Create_CallsGetCourseTypesAndRegionsOnce_WhenCalled()
        {
            //Arrange
            var controller = CreateController();

            //Act
            await controller.CreateAsync();

            //Assert
            mockCourseTemplateService.Verify(x => x.GetPathwayTypesAndRegions(), Times.Once);
        }

        [Fact]
        public async Task Create_ReturnsViewWithModel_WhenCalled()
        {
            //Arrange
            var controller = CreateController();

            var courseTypesList = Builder<CourseType>.CreateListOfSize(3).Build();
            var regionsList = Builder<Region>.CreateListOfSize(3).Build();

            Dictionary<int, string> courseTypesDictionary = new();
            Dictionary<int, string> regionsDictionary = new();

            courseTypesList.ToList().ForEach(x => courseTypesDictionary.Add(x.Id, x.Name));
            regionsList.ToList().ForEach(x => regionsDictionary.Add(x.Id, x.Name));

            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew()
                .With(x => x.CourseTemplates = null)
                .With(x => x.Name = null)
                .With(x => x.Description = null)
                .With(x => x.PathwayTypeId = 0)
                .With(x => x.RegionId = 0)
                .With(x => x.PathwayTypeDictionaries = courseTypesDictionary)
                .With(x => x.RegionDictionaries = regionsDictionary)
                .Build();

            mockCourseTemplateService.Setup(x => x.GetPathwayTypesAndRegions()).ReturnsAsync(courseTemplateViewModel);

            //Act
            var result = await controller.CreateAsync() as ViewResult;

            //Assert
            result.Model.Should().BeEquivalentTo(courseTemplateViewModel);
        }

        #endregion Create
    }
}