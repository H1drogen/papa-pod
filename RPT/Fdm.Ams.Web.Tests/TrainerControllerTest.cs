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
    public class TrainerControllerTest
    {
        private readonly Mock<ILogger<TrainerController>> logger;
        private readonly Mock<ICourseModuleService> mockCourseModuleService;
        private readonly Mock<ITrainerService> mockTrainerService;

        public TrainerControllerTest()
        {
            mockCourseModuleService = new Mock<ICourseModuleService>();
            mockTrainerService = new Mock<ITrainerService>();
            logger = new Mock<ILogger<TrainerController>>();
        }

        public TrainerController ControllerHelper()
        {
            return new TrainerController(mockCourseModuleService.Object, mockTrainerService.Object, logger.Object);
        }

        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_CallsTrainerServiceDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var httpContext = new DefaultHttpContext();
            var trainerViewModel = Builder<TrainerViewModel>.CreateNew().Build();
            mockTrainerService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(trainerViewModel);
            var controller = ControllerHelper();

            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{trainerViewModel.FirstName} Deleted successfully"
            };
            await controller.DeleteAsync(id);
            //Assert
            mockTrainerService.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            int id = 1;
            var httpContext = new DefaultHttpContext();
            var trainerViewModel = Builder<TrainerViewModel>.CreateNew().Build();
            mockTrainerService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(trainerViewModel);
            var controller = ControllerHelper();
            //Act

            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{trainerViewModel.FirstName} Deleted successfully"
            };
            var result = await controller.DeleteAsync(id);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion DeleteAsync

        #region EditAsync

        [Fact]
        public async Task EditAsync_ShouldCallsTrainerServicePutAsyncOnce_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var trainerViewModel = Builder<TrainerViewModel>.CreateNew().Build();
            var controller = ControllerHelper();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{trainerViewModel.FirstName} updated successfully"
            };

            //Act
            await controller.EditAsync(trainerViewModel);

            //Assert
            mockTrainerService.Verify(x => x.PutAsync(trainerViewModel), Times.Once);
        }

        [Fact]
        public async Task EditAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var trainerViewModel = Builder<TrainerViewModel>.CreateNew().Build();
            mockTrainerService.Setup(x => x.PutAsync(trainerViewModel));
            var controller = ControllerHelper();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{trainerViewModel.FirstName} updated successfully"
            };
            //Act
            var result = await controller.EditAsync(trainerViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion EditAsync

        #region Index

        [Fact]
        public void Index_ShouldNotReturn_Null()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var controller = ControllerHelper();
            var trainerViewModel = Builder<TrainerViewModel>.CreateNew().Build();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{trainerViewModel.FirstName} created successfully"
            };
            // Act
            var result = controller.Index();

            // Assert
            result.Should().NotBeNull();
        }

        #endregion Index

        #region GetByIdForEditAsync

        [Fact]
        public async Task GetByIdForEditAsync_CallsGetTrainerWithOfficeListOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = ControllerHelper();
            var offices = Builder<Office>.CreateListOfSize(3).Build();

            Dictionary<int, string> officesDictionary = new();

            offices.ToList().ForEach(x => officesDictionary.Add(x.Id, x.Name));

            var trainerViewModel = Builder<TrainerViewModel>.CreateNew()
                .With(x => x.OfficeDictionaries = officesDictionary)
                .Build();

            mockTrainerService.Setup(x => x.GetTrainerWithOfficeList()).ReturnsAsync(trainerViewModel);

            var viewModel = Builder<TrainerViewModel>.CreateNew()
               .With(x => x.OfficeDictionaries = officesDictionary)
               .Build();

            mockTrainerService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(viewModel);

            //Act
            await controller.GetByIdForEditAsync(id);

            //Assert
            mockTrainerService.Verify(x => x.GetTrainerWithOfficeList(), Times.Once);
        }

        [Fact]
        public async Task GetByIdForEditAsync_ShouldReturnPartialView_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = ControllerHelper();
            var offices = Builder<Office>.CreateListOfSize(3).Build();

            Dictionary<int, string> officesDictionary = new();

            offices.ToList().ForEach(x => officesDictionary.Add(x.Id, x.Name));

            var trainerViewModel = Builder<TrainerViewModel>.CreateNew()
                .With(x => x.OfficeDictionaries = officesDictionary)
                .Build();

            mockTrainerService.Setup(x => x.GetTrainerWithOfficeList()).ReturnsAsync(trainerViewModel);

            var viewModel = Builder<TrainerViewModel>.CreateNew()
               .With(x => x.OfficeDictionaries = officesDictionary)
               .Build();

            mockTrainerService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(viewModel);

            //Act
            var result = await controller.GetByIdForEditAsync(id) as PartialViewResult;
            //Assert
            result.ViewName.Should().BeEquivalentTo("UpdateTrainer");
        }

        [Fact]
        public async Task GetByIdForEditAsync_ShouldReturnViewWithModel_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = ControllerHelper();

            var offices = Builder<Office>.CreateListOfSize(3).Build();

            Dictionary<int, string> officesDictionary = new();

            offices.ToList().ForEach(x => officesDictionary.Add(x.Id, x.Name));

            var trainerViewModel = Builder<TrainerViewModel>.CreateNew()
                .With(x => x.OfficeDictionaries = officesDictionary)
                .Build();

            mockTrainerService.Setup(x => x.GetTrainerWithOfficeList()).ReturnsAsync(trainerViewModel);

            var viewModel = Builder<TrainerViewModel>.CreateNew()
               .With(x => x.OfficeDictionaries = officesDictionary)
               .Build();

            mockTrainerService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(viewModel);
            //Act
            var result = await controller.GetByIdForEditAsync(id) as PartialViewResult;

            //Assert
            result.Model.Should().BeEquivalentTo(trainerViewModel);
        }

        #endregion GetByIdForEditAsync

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsGetAllAsync_Once()
        {
            //Arrange
            var controller = ControllerHelper();
            //Act
            await controller.GetAllAsync();
            //Asset
            mockTrainerService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsSerializedVersionOfEntityReturnedFromGetAllAsync_WhenCalled()
        {
            //Arrange
            var controller = ControllerHelper();
            var trainer = Builder<Trainer>.CreateListOfSize(4).Build();
            mockTrainerService.Setup(x => x.GetAllAsync()).ReturnsAsync(trainer);
            var expectedResult = new JsonResult(trainer);
            //Act
            var result = await controller.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetAllAsync

        #region TrainerSchedule

        [Fact]
        public async Task TrainerSchedule_ShouldCallCourseModuleServiceGetAllAsyncOnce_WhenCalled()
        {
            // Arrange

            var httpContext = new DefaultHttpContext();
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            mockCourseModuleService.Setup(x => x.GetByIdAsync(courseModuleViewModel.Id));

            //Act
           
            var controller = ControllerHelper();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseModuleViewModel.Name} updated successfully"
            };
            var courseModuleViewModelList = Builder<CourseModuleViewModel>.CreateListOfSize(5).Build();
            mockCourseModuleService.Setup(x => x.GetAllAsync()).ReturnsAsync(courseModuleViewModelList);
            // Act
            await controller.TrainerSchedule();
            // Assert
            mockCourseModuleService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public void TrainerSchedule_ShouldNotReturnNull_WhenCalled()
        {
            //Arrange
            var controller = ControllerHelper();
            //Act
            var result = controller.TrainerSchedule();
            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task TrainerSchedule_ShouldReturnAnEnumerableOfCourseModule_ThatHaveTrainerIdSetToNull_WhenCalled()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            mockCourseModuleService.Setup(x => x.GetByIdAsync(courseModuleViewModel.Id));

            var controller = ControllerHelper();
            var courseModuleViewModelList = Builder<CourseModuleViewModel>.CreateListOfSize(5)
                .Random(2).With(x => x.TrainerId = null)
                .TheRest().With(x => x.TrainerId = 1)
                .Build();
            var expectedResult = courseModuleViewModelList.Where(x => x.TrainerId == null);
            mockCourseModuleService.Setup(x => x.GetAllAsync()).ReturnsAsync(courseModuleViewModelList);
            // Act

            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{courseModuleViewModel.Name} updated successfully"
            };

            var result = await controller.TrainerSchedule() as ViewResult;
            // Assert
            result.Model.Should().BeEquivalentTo(expectedResult);
        }

        #endregion TrainerSchedule

        #region CreateAsync

        [Fact]
        public async Task CreateAsync_CallsGetTrainerWithOfficeListOnce_WhenCalled()
        {
            //Arrange
            var controller = ControllerHelper();

            //Act
            await controller.CreateAsync();

            //Assert
            mockTrainerService.Verify(x => x.GetTrainerWithOfficeList(), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnPartialView_WhenCalled()
        {
            //Arrange
            var controller = ControllerHelper();

            //Act
            var result = await controller.CreateAsync() as PartialViewResult;
            //Assert
            result.ViewName.Should().BeEquivalentTo("Create");
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnsViewWithModel_WhenCalled()
        {
            //Arrange
            var controller = ControllerHelper();

            var offices = Builder<Office>.CreateListOfSize(3).Build();

            Dictionary<int, string> officesDictionary = new();

            offices.ToList().ForEach(x => officesDictionary.Add(x.Id, x.Name));

            var trainerViewModel = Builder<TrainerViewModel>.CreateNew()
                .With(x => x.OfficeDictionaries = officesDictionary)
                .Build();

            mockTrainerService.Setup(x => x.GetTrainerWithOfficeList()).ReturnsAsync(trainerViewModel);

            //Act
            var result = await controller.CreateAsync() as PartialViewResult;

            //Assert
            result.Model.Should().BeEquivalentTo(trainerViewModel);
        }

        #endregion CreateAsync

        #region PostAsync

        [Fact]
        public async Task PostAsync_ShouldCallsTrainerServicePostAsyncOnce_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var trainerViewModel = Builder<TrainerViewModel>.CreateNew().Build();
            var controller = ControllerHelper();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{trainerViewModel.FirstName} created successfully"
            };

            //Act
            await controller.PostAsync(trainerViewModel);

            //Assert
            mockTrainerService.Verify(x => x.PostAsync(trainerViewModel), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var trainerViewModel = Builder<TrainerViewModel>.CreateNew().Build();
            mockTrainerService.Setup(x => x.PostAsync(trainerViewModel));
            var controller = ControllerHelper();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{trainerViewModel.FirstName} created successfully"
            };
            //Act
            var result = await controller.PostAsync(trainerViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion PostAsync
    }
}