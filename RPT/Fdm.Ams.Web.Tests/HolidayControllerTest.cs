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
    public class HolidayControllerTest
    {
        private Mock<IHolidayService> mockHolidayService;
        private Mock<ILogger<HolidayController>> mockLogger;

        public HolidayControllerTest()
        {
            mockLogger = new Mock<ILogger<HolidayController>>();
            mockHolidayService = new Mock<IHolidayService>();
        }

        private HolidayController CreateController()
        {
            return new HolidayController(mockHolidayService.Object, mockLogger.Object);
        }

        #region Index

        [Fact]
        public void Index_ShouldNotReturnANullResult_WhenCalled()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var holidayViewModel = Builder<HolidayViewModel>.CreateNew().Build();
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMesssge"] = $"{holidayViewModel.Name}{"created successfully"}"
            };
            // Act
            var result = controller.Index();

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Index_ShouldReturnsHolidayViewModel_WhenCalled()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var controller = CreateController();
            var holidayViewModel = Builder<HolidayViewModel>.CreateNew().Build();
            mockHolidayService.Setup(x => x.GetCountries()).ReturnsAsync(holidayViewModel);

            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{holidayViewModel.Name} created successfully"
            };

            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            result.Model.Should().BeEquivalentTo(holidayViewModel, options =>
            {
                options.Excluding(hv => hv.CountryDictionary);
                return options;
            });
        }

        #endregion Index

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsHolidayServiceGetAllAsync_WhenCalled()
        {
            //Arrange
            var controller = CreateController();
            //Act
            await controller.GetAllAsync();
            //Assert
            mockHolidayService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnsJsonResult_WithCorrectData_WhenCalled()
        {
            //Arrange
            var holidayViewModelList = Builder<HolidayViewModel>.CreateListOfSize(3).Build();
            mockHolidayService.Setup(x => x.GetAllAsync()).ReturnsAsync(holidayViewModelList);
            var expectedResult = new JsonResult(holidayViewModelList);
            var controller = CreateController();
            //Act
            var result = await controller.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetAllAsync

        #region PostAsync

        [Fact]
        public async Task PostAsync_ShouldCallHolidayServicePostAsync_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var holidayViewModel = Builder<HolidayViewModel>.CreateNew().Build();
            var controller = CreateController();
            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{holidayViewModel.Name} created successfully"
            };
            await controller.PostAsync(holidayViewModel);
            //Assert
            mockHolidayService.Verify(x => x.PostAsync(holidayViewModel), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var holidayViewModel = Builder<HolidayViewModel>.CreateNew().Build();
            mockHolidayService.Setup(x => x.PostAsync(holidayViewModel));
            var controller = CreateController();

            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{holidayViewModel.Name} created successfully"
            };
            var result = await controller.PostAsync(holidayViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion PostAsync

        #region GetByIdForEditAsync

        [Fact]
        public async Task GetByIdForEditAsync_ShouldCallGetCountriesOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = CreateController();
            var countries = Builder<Country>.CreateListOfSize(3).Build();

            Dictionary<int, string> countriesDictionary = new();

            countries.ToList().ForEach(x => countriesDictionary.Add(x.Id, x.Name));

            var holidayViewModel = Builder<HolidayViewModel>.CreateNew()
                .With(x => x.CountryDictionary = countriesDictionary)
                .Build();

            mockHolidayService.Setup(x => x.GetCountries()).ReturnsAsync(holidayViewModel);

            var viewModel = Builder<HolidayViewModel>.CreateNew()
               .With(x => x.CountryDictionary = countriesDictionary)
               .Build();

            mockHolidayService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(viewModel);

            //Act
            await controller.GetByIdForEditAsync(id);

            //Assert
            mockHolidayService.Verify(x => x.GetCountries(), Times.Once);
        }

        [Fact]
        public async Task GetByIdForEditAsync_ShouldReturnPartialView_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = CreateController();
            var countries = Builder<Country>.CreateListOfSize(3).Build();

            Dictionary<int, string> countriesDictionary = new();

            countries.ToList().ForEach(x => countriesDictionary.Add(x.Id, x.Name));

            var holidayViewModel = Builder<HolidayViewModel>.CreateNew()
                .With(x => x.CountryDictionary = countriesDictionary)
                .Build();

            mockHolidayService.Setup(x => x.GetCountries()).ReturnsAsync(holidayViewModel);

            var viewModel = Builder<HolidayViewModel>.CreateNew()
               .With(x => x.CountryDictionary = countriesDictionary)
               .Build();

            mockHolidayService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(viewModel);

            //Act
            var result = await controller.GetByIdForEditAsync(id) as PartialViewResult;
            //Assert
            result.ViewName.Should().BeEquivalentTo("UpdateHoliday");
        }

        [Fact]
        public async Task GetByIdForEditAsync_ShouldReturnViewWithModel_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = CreateController();
            var countries = Builder<Country>.CreateListOfSize(3).Build();

            Dictionary<int, string> countriesDictionary = new();

            countries.ToList().ForEach(x => countriesDictionary.Add(x.Id, x.Name));

            var holidayViewModel = Builder<HolidayViewModel>.CreateNew()
                .With(x => x.CountryDictionary = countriesDictionary)
                .Build();

            mockHolidayService.Setup(x => x.GetCountries()).ReturnsAsync(holidayViewModel);

            var viewModel = Builder<HolidayViewModel>.CreateNew()
               .With(x => x.CountryDictionary = countriesDictionary)
               .Build();

            mockHolidayService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(viewModel);
            //Act
            var result = await controller.GetByIdForEditAsync(id) as PartialViewResult;

            //Assert
            result.Model.Should().BeEquivalentTo(holidayViewModel);
        }

        #endregion GetByIdForEditAsync

        #region EditAsync

        [Fact]
        public async Task EditAsync_ShouldCallHolidayServicePutAsyncOnce_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var holidayViewModel = Builder<HolidayViewModel>.CreateNew().Build();
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{holidayViewModel.Name} updated successfully"
            };

            //Act
            await controller.EditAsync(holidayViewModel);

            //Assert
            mockHolidayService.Verify(x => x.PutAsync(holidayViewModel), Times.Once);
        }

        [Fact]
        public async Task EditAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var holidayViewModel = Builder<HolidayViewModel>.CreateNew().Build();
            mockHolidayService.Setup(x => x.PutAsync(holidayViewModel));
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{holidayViewModel.Name} updated successfully"
            };
            //Act
            var result = await controller.EditAsync(holidayViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion EditAsync

        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_ShouldCallHolidayServiceDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var httpContext = new DefaultHttpContext();
            var holidayViewModel = Builder<HolidayViewModel>.CreateNew().Build();
            mockHolidayService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(holidayViewModel);
            var controller = CreateController();

            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{holidayViewModel.Name} deleted successfully"
            };
            await controller.DeleteAsync(id);
            //Assert
            mockHolidayService.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            int id = 1;
            var httpContext = new DefaultHttpContext();
            var holidayViewModel = Builder<HolidayViewModel>.CreateNew().Build();
            mockHolidayService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(holidayViewModel);
            var controller = CreateController();
            //Act

            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{holidayViewModel.Name} deleted successfully"
            };
            var result = await controller.DeleteAsync(id);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion DeleteAsync
    }
}