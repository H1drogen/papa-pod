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
    public class OfficeControllerTest
    {
        private readonly Mock<IOfficeService> mockOfficeService;
        private Mock<ILogger<OfficeController>> mockLogger;

        public OfficeControllerTest()
        {
            mockOfficeService = new Mock<IOfficeService>();
            mockLogger = new Mock<ILogger<OfficeController>>();
        }

        private OfficeController ControllerHelper()
        {
            return new OfficeController(mockOfficeService.Object, mockLogger.Object);
        }



        #region CreateAsync

        [Fact]
        public async Task CreateAsync_ShouldCallGetOfficeWithCountryListOnce_WhenCalled()
        {
            //Arrange
            var controller = ControllerHelper();

            //Act
            await controller.CreateAsync();

            //Assert
            mockOfficeService.Verify(x => x.GetOfficeWithCountryList(), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnPartialView_WhenCalled()
        {
            //Arrange
            var controller = ControllerHelper();

            //Act
            var result = await controller.CreateAsync() as PartialViewResult;
            //Assert
            result.ViewName.Should().BeEquivalentTo("_CreateOffice");
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnViewWithModel_WhenCalled()
        {
            //Arrange
            var controller = ControllerHelper();

            var officies = Builder<Office>.CreateListOfSize(3).Build();

            Dictionary<int, string> officiesDictionary = new();

            officies.ToList().ForEach(x => officiesDictionary.Add(x.Id, x.Name));

            var officeViewModel = Builder<OfficeViewModel>.CreateNew()
                .With(x => x.CountriesDictionary = officiesDictionary)
                .Build();

            mockOfficeService.Setup(x => x.GetOfficeWithCountryList()).ReturnsAsync(officeViewModel);

            //Act
            var result = await controller.CreateAsync() as PartialViewResult;

            //Assert
            result.Model.Should().BeEquivalentTo(officeViewModel);
        }

        #endregion CreateAsync




        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsGetAllAsync_Once()
        {
            //Arrange
            var controller = ControllerHelper();
            //Act
            await controller.GetAllAsync();

            //Asset
            mockOfficeService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnJsonResult_WithCorrectData_WhenCalled()
        {
            //Arrange
            var officeList = Builder<Office>.CreateListOfSize(3).Build();
            var officeViewModelList = Builder<OfficeViewModel>.CreateListOfSize(3).Build();
            mockOfficeService.Setup(x => x.GetAllAsync()).ReturnsAsync(officeViewModelList);
            var expectedResult = new JsonResult(officeList);
            var controller = ControllerHelper();
            //Act
            var result = await controller.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetAllAsync

        #region GetByIdAsync

        [Theory]
        [InlineData(1)]
        public async Task GetByIdAsync_CallsGetByIdAsync_Once(int id)
        {
            var controller = ControllerHelper();
            //Act
            await controller.GetByIdAsync(id);
            //Asset
            mockOfficeService.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetByIdAsync_ReturnsSerializedVersionOfEntityReturnedFromGetByIdAsync_WhenCalled(int id)
        {
            var controller = ControllerHelper();
            var office = Builder<Office>.CreateNew().Build();
            var officeViewModel = Builder<OfficeViewModel>.CreateNew().Build();
            mockOfficeService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(officeViewModel);
            var expecedResult = new JsonResult(office);

            //Act
            var result = await controller.GetByIdAsync(id);

            //Assert
            result.Should().BeEquivalentTo(expecedResult);
        }

        #endregion GetByIdAsync


        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_CallsOfficeServiceDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var httpContext = new DefaultHttpContext();
            var officeViewModel = Builder<OfficeViewModel>.CreateNew().Build();
            mockOfficeService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(officeViewModel);
            var controller = ControllerHelper();

            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{officeViewModel.Name} archived successfully"
            };
            await controller.DeleteAsync(id);
            //Assert
            mockOfficeService.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            int id = 1;
            var httpContext = new DefaultHttpContext();
            var officeViewModel = Builder<OfficeViewModel>.CreateNew().Build();
            mockOfficeService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(officeViewModel);
            var controller = ControllerHelper();
            //Act

            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{officeViewModel.Name} archived successfully"
            };
            var result = await controller.DeleteAsync(id);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion DeleteAsync


        #region EditAsync

        [Fact]
        public async Task EditAsync_ShouldCallsOfficeServicePutAsyncOnce_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var officeViewModel = Builder<OfficeViewModel>.CreateNew().Build();
            var controller = ControllerHelper();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{officeViewModel.Name} updated successfully"
            };

            //Act
            await controller.EditAsync(officeViewModel);

            //Assert
            mockOfficeService.Verify(x => x.PutAsync(officeViewModel), Times.Once);
        }

        [Fact]
        public async Task EditAsync_ShouldRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var officeViewModel = Builder<OfficeViewModel>.CreateNew().Build();
            mockOfficeService.Setup(x => x.PutAsync(officeViewModel));
            var controller = ControllerHelper();
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{officeViewModel.Name} updated successfully"
            };
            //Act
            var result = await controller.EditAsync(officeViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion EditAsync



        #region GetByIdForEditAsync

        [Fact]
        public async Task GetByIdForEditAsync_ShouldCallsGetOfficeWithCountryListOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = ControllerHelper();
            var countries = Builder<Country>.CreateListOfSize(3).Build();

            Dictionary<int, string> countriesDictionary = new();

            countries.ToList().ForEach(x => countriesDictionary.Add(x.Id, x.Name));

            var officeViewModel = Builder<OfficeViewModel>.CreateNew()
                .With(x => x.CountriesDictionary = countriesDictionary)
                .Build();

            mockOfficeService.Setup(x => x.GetOfficeWithCountryList()).ReturnsAsync(officeViewModel);

            var viewModel = Builder<OfficeViewModel>.CreateNew()
               .With(x => x.CountriesDictionary = countriesDictionary)
               .Build();

            mockOfficeService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(viewModel);

            //Act
            await controller.GetByIdForEditAsync(id);

            //Assert
            mockOfficeService.Verify(x => x.GetOfficeWithCountryList(), Times.Once);
        }

        [Fact]
        public async Task GetByIdForEditAsync_ShouldReturnPartialView_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = ControllerHelper();
            var countries = Builder<Country>.CreateListOfSize(3).Build();

            Dictionary<int, string> countriesDictionary = new();

            countries.ToList().ForEach(x => countriesDictionary.Add(x.Id, x.Name));

            var officeViewModel = Builder<OfficeViewModel>.CreateNew()
                .With(x => x.CountriesDictionary = countriesDictionary)
                .Build();

            mockOfficeService.Setup(x => x.GetOfficeWithCountryList()).ReturnsAsync(officeViewModel);

            var viewModel = Builder<OfficeViewModel>.CreateNew()
               .With(x => x.CountriesDictionary = countriesDictionary)
               .Build();

            mockOfficeService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(viewModel);

            //Act
            var result = await controller.GetByIdForEditAsync(id) as PartialViewResult;
            //Assert
            result.ViewName.Should().BeEquivalentTo("_UpdateOffice");
        }

        [Fact]
        public async Task GetByIdForEditAsync_ShouldReturnViewWithModel_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = ControllerHelper();
            var countries = Builder<Country>.CreateListOfSize(3).Build();

            Dictionary<int, string> countriesDictionary = new();

            countries.ToList().ForEach(x => countriesDictionary.Add(x.Id, x.Name));

            var officeViewModel = Builder<OfficeViewModel>.CreateNew()
                .With(x => x.CountriesDictionary = countriesDictionary)
                .Build();

            mockOfficeService.Setup(x => x.GetOfficeWithCountryList()).ReturnsAsync(officeViewModel);

            var viewModel = Builder<OfficeViewModel>.CreateNew()
               .With(x => x.CountriesDictionary = countriesDictionary)
               .Build();

            mockOfficeService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(viewModel);
            //Act
            var result = await controller.GetByIdForEditAsync(id) as PartialViewResult;

            //Assert
            result.Model.Should().BeEquivalentTo(officeViewModel);
        }

        #endregion GetByIdForEditAsync


        #region PostAsync

        [Fact]
        public async Task PostAsync_ShouldCallOfficeServicePostAsyncOnce_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var officeViewModel = Builder<OfficeViewModel>.CreateNew().Build();
            var controller = ControllerHelper();
            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{officeViewModel.Name} created successfully"
            };
            await controller.PostAsync(officeViewModel);
            //Assert
            mockOfficeService.Verify(x => x.PostAsync(officeViewModel), Times.Once);
        }


        [Fact]
        public async Task PostAsync_ShoudRedirectToActionIndexPage_WhenCalled()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var officeViewModel = Builder<OfficeViewModel>.CreateNew().Build();
            mockOfficeService.Setup(x => x.PostAsync(officeViewModel));
            var controller = ControllerHelper();

            //Act
            controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["SuccessMessage"] = $"{officeViewModel.Name} created successfully"
            };
            var result = await controller.PostAsync(officeViewModel);
            //Assert
            Assert.IsType<RedirectToActionResult>(result).ActionName.Should().BeEquivalentTo("Index");
        }

        #endregion PostAsync
    }
}