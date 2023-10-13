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
    public class CountryControllerTests
    {
        private Mock<ICountryService> mockCountryService;
        private Mock<ILogger<CountryController>> mockLogger;

        public CountryControllerTests()
        {
            mockLogger = new Mock<ILogger<CountryController>>();
            mockCountryService = new Mock<ICountryService>();
        }

        private CountryController CreateController()
        {
            return new CountryController(mockCountryService.Object, mockLogger.Object);
        }

        #region CreateAsync

        [Fact]
        public async Task CreateAsync_CallsGetCountryWithRegionListOnce_WhenCalled()
        {
            //Arrange
            var controller = CreateController();

            //Act
            await controller.CreateAsync();

            //Assert
            mockCountryService.Verify(x => x.GetCountryWithRegionList(), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnPartialView_WhenCalled()
        {
            //Arrange
            var controller = CreateController();

            //Act
            var result = await controller.CreateAsync() as PartialViewResult;
            //Assert
            result.ViewName.Should().BeEquivalentTo("Create");
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnsViewWithModel_WhenCalled()
        {
            //Arrange
            var controller = CreateController();

            var countries = Builder<Country>.CreateListOfSize(3).Build();

            Dictionary<int, string> countriesDictionary = new();

            countries.ToList().ForEach(x => countriesDictionary.Add(x.Id, x.Name));

            var countryViewModel = Builder<CountryViewModel>.CreateNew()
                .With(x => x.RegionDictionaries = countriesDictionary)
                .Build();

            mockCountryService.Setup(x => x.GetCountryWithRegionList()).ReturnsAsync(countryViewModel);

            //Act
            var result = await controller.CreateAsync() as PartialViewResult;

            //Assert
            result.Model.Should().BeEquivalentTo(countryViewModel);
        }

        #endregion CreateAsync

        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_CallsCountryServiceDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var httpContext = new DefaultHttpContext();
            var countryViewModel = Builder<CountryViewModel>.CreateNew().Build();
            mockCountryService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(countryViewModel);
            var controller = CreateController();

            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{countryViewModel.Name} archived successfully"
            };
            await controller.DeleteAsync(id);
            //Assert
            mockCountryService.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            int id = 1;
            var httpContext = new DefaultHttpContext();
            var countryViewModel = Builder<CountryViewModel>.CreateNew().Build();
            mockCountryService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(countryViewModel);
            var controller = CreateController();
            //Act

            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{countryViewModel.Name} archived successfully"
            };
            var result = await controller.DeleteAsync(id);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion DeleteAsync

        #region Index

        [Fact]
        public void Index_ShouldNotReturnANullResult_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var controller = CreateController();
            var countryViewModel = Builder<CountryViewModel>.CreateNew().Build();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{countryViewModel.Name} created successfully"
            };
            //Act
            var result = controller.Index();
            //Assert
            result.Should().NotBeNull();
        }

        #endregion Index

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_ShouldCallsCountryServiceGetAllAsync_WhenCalled()
        {
            //Arrange
            var controller = CreateController();
            //Act
            await controller.GetAllAsync();
            //Assert
            mockCountryService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnsJsonResult_WithCorrectData_WhenCalled()
        {
            //Arrange
            var list = Builder<Country>.CreateListOfSize(3).Build();
            mockCountryService.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
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
        public async Task PostAsync_ShouldCallsCountryServicePostAsync_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var countryViewModel = Builder<CountryViewModel>.CreateNew().Build();
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{countryViewModel.Name} created successfully"
            };
            //Act
            await controller.PostAsync(countryViewModel);
            //Assert
            mockCountryService.Verify(x => x.PostAsync(countryViewModel), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var countryViewModel = Builder<CountryViewModel>.CreateNew().Build();
            mockCountryService.Setup(x => x.PostAsync(countryViewModel));
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{countryViewModel.Name} created successfully"
            };
            //Act
            var result = await controller.PostAsync(countryViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion PostAsync

        #region GetByIdForEditAsync

        [Fact]
        public async Task GetByIdForEditAsync_CallsGetCountryWithRegionListOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = CreateController();
            var regions = Builder<Region>.CreateListOfSize(3).Build();

            Dictionary<int, string> regionsDictionary = new();

            regions.ToList().ForEach(x => regionsDictionary.Add(x.Id, x.Name));

            var countryViewModel = Builder<CountryViewModel>.CreateNew()
                .With(x => x.RegionDictionaries = regionsDictionary)
                .Build();

            mockCountryService.Setup(x => x.GetCountryWithRegionList()).ReturnsAsync(countryViewModel);

            var viewModel = Builder<CountryViewModel>.CreateNew()
               .With(x => x.RegionDictionaries = regionsDictionary)
               .Build();

            mockCountryService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(viewModel);

            //Act
            await controller.GetByIdForEditAsync(id);

            //Assert
            mockCountryService.Verify(x => x.GetCountryWithRegionList(), Times.Once);
        }

        [Fact]
        public async Task GetByIdForEditAsync_ShouldReturnPartialView_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = CreateController();
            var regions = Builder<Region>.CreateListOfSize(3).Build();

            Dictionary<int, string> regionsDictionary = new();

            regions.ToList().ForEach(x => regionsDictionary.Add(x.Id, x.Name));

            var countryViewModel = Builder<CountryViewModel>.CreateNew()
                .With(x => x.RegionDictionaries = regionsDictionary)
                .Build();

            mockCountryService.Setup(x => x.GetCountryWithRegionList()).ReturnsAsync(countryViewModel);

            var viewModel = Builder<CountryViewModel>.CreateNew()
               .With(x => x.RegionDictionaries = regionsDictionary)
               .Build();

            mockCountryService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(viewModel);

            //Act
            var result = await controller.GetByIdForEditAsync(id) as PartialViewResult;
            //Assert
            result.ViewName.Should().BeEquivalentTo("UpdateCountry");
        }

        [Fact]
        public async Task GetByIdForEditAsync_ShouldReturnViewWithModel_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = CreateController();
            var regions = Builder<Region>.CreateListOfSize(3).Build();

            Dictionary<int, string> regionsDictionary = new();

            regions.ToList().ForEach(x => regionsDictionary.Add(x.Id, x.Name));

            var countryViewModel = Builder<CountryViewModel>.CreateNew()
                .With(x => x.RegionDictionaries = regionsDictionary)
                .Build();

            mockCountryService.Setup(x => x.GetCountryWithRegionList()).ReturnsAsync(countryViewModel);

            var viewModel = Builder<CountryViewModel>.CreateNew()
               .With(x => x.RegionDictionaries = regionsDictionary)
               .Build();

            mockCountryService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(viewModel);
            //Act
            var result = await controller.GetByIdForEditAsync(id) as PartialViewResult;

            //Assert
            result.Model.Should().BeEquivalentTo(countryViewModel);
        }

        #endregion GetByIdForEditAsync

        #region EditAsync

        [Fact]
        public async Task EditAsync_ShouldCallsCountryServicePutAsyncOnce_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var countryViewModel = Builder<CountryViewModel>.CreateNew().Build();
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{countryViewModel.Name} updated successfully"
            };

            //Act
            await controller.EditAsync(countryViewModel);

            //Assert
            mockCountryService.Verify(x => x.PutAsync(countryViewModel), Times.Once);
        }

        [Fact]
        public async Task EditAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var countryViewModel = Builder<CountryViewModel>.CreateNew().Build();
            mockCountryService.Setup(x => x.PutAsync(countryViewModel));
            var controller = CreateController();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{countryViewModel.Name} updated successfully"
            };
            //Act
            var result = await controller.EditAsync(countryViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion EditAsync
    }
}