using AutoMapper;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.ViewModels;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Fdm.Ams.Services.Tests
{
    public class OfficeServiceTests
    {

        private Mock<IOfficeDal> mockOfficeDal;
        private Mock<IMapper> mockMapper;
        private Mock<ICountryDal> mockCountryDal;
        public OfficeServiceTests()
        {
            mockOfficeDal = new Mock<IOfficeDal>();
            mockMapper = new Mock<IMapper>();
            mockCountryDal = new Mock<ICountryDal>();

        }

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_ShouldCallDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var service = CreateOfficeService();
            //Act
            await service.GetAllAsync();
            //Assert
            mockOfficeDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAnEnumerableOfOffice_WhenSuccessful()
        {
            //Arrange
            var officeList = Builder<Office>.CreateListOfSize(3).Build();
            var officeViewModelList = Builder<OfficeViewModel>.CreateListOfSize(3).Build();
            mockMapper.Setup(x => x.Map<IEnumerable<Office>>(officeViewModelList)).Returns(officeList);
            mockMapper.Setup(x => x.Map<IEnumerable<OfficeViewModel>>(officeList)).Returns(officeViewModelList);

            mockOfficeDal.Setup(x => x.GetAllAsync()).ReturnsAsync(officeList);
            var service = CreateOfficeService();
            //Act
            var result = await service.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(officeList);
        }

        #endregion GetAllAsync

        #region GetOfficeWithCountryList

        [Fact]
        public async Task GetOfficeWithCountryList_ShouldCallCountryDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var countries = Builder<Country>.CreateListOfSize(2).Build();
            mockCountryDal.Setup(x => x.GetAllAsync()).ReturnsAsync(countries);
            var service = CreateOfficeService();

            //Act
            await service.GetOfficeWithCountryList();

            //Assert
            mockCountryDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetOfficeWithCountryList_ShouldReturnOfficeViewModelWithDictionaryOfCountries_WhenCalled()
        {
            //Arrange
            var service = CreateOfficeService();

            var countries = Builder<Country>.CreateListOfSize(3).Build();

            Dictionary<int, string> countriesDictionary = new();

            countries.ToList().ForEach(x => countriesDictionary.Add(x.Id, x.Name));

            var officeViewModel = Builder<OfficeViewModel>.CreateNew()
                .With(x => x.CountriesDictionary = countriesDictionary)
                .Build();

            mockCountryDal.Setup(x => x.GetAllAsync()).ReturnsAsync(countries);

            //Act
            var result = await service.GetOfficeWithCountryList();

            //Assert
            result.Should().BeEquivalentTo(officeViewModel, config =>
            config
            .Excluding(x => x.Name)
            .Excluding(x => x.Abbreviation)
            .Excluding(x => x.IsActive)
            .Excluding(x => x.IsPopUp)
            .Excluding(x => x.CountryId)
            .Excluding(x => x.Id)
            );
        }

        #endregion GetOfficeWithCountryList


        #region PostAsync

        [Fact]
        public async Task PostAsync_ShouldCallDalPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var office = Builder<Office>.CreateNew().Build();
            var officeViewModel = Builder<OfficeViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Office>(officeViewModel)).Returns(office);
            var service = CreateOfficeService();

            //Act
            await service.PostAsync(officeViewModel);

            //Assert
            mockOfficeDal.Verify(x => x.PostAsync(office), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldReturnOfficeViewModel_WhenSuccessful()
        {
            //Arrange
            var officeViewModel = Builder<OfficeViewModel>.CreateNew().Build();
            var office = Builder<Office>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Office>(officeViewModel)).Returns(office);
            mockMapper.Setup(x => x.Map<OfficeViewModel>(office)).Returns(officeViewModel);
            mockOfficeDal.Setup(x => x.PostAsync(office)).ReturnsAsync(office);
            var service = CreateOfficeService();

            //Act
            var result = await service.PostAsync(officeViewModel);

            //Assert
            result.Should().BeEquivalentTo(officeViewModel);
        }

        #endregion PostAsync

        #region GetByIdAsync

        [Fact]
        public async Task GetByIdAsync_ShouldCallDalGetByIdAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var office = Builder<Office>.CreateNew().Build();
            mockOfficeDal.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(office);
            var service = CreateOfficeService();
            //Act
            await service.GetByIdAsync(id);
            //Assert
            mockOfficeDal.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnOfficeFromDal_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var office = Builder<Office>.CreateNew().Build();
            var officeViewModel = Builder<OfficeViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Office>(officeViewModel)).Returns(office);
            mockMapper.Setup(x => x.Map<OfficeViewModel>(office)).Returns(officeViewModel);

            mockOfficeDal.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(office);
            var service = CreateOfficeService();

            //Act
            var result = await service.GetByIdAsync(id);

            //Assert
            result.Should().BeEquivalentTo(office);
        }

        #endregion GetByIdAsync



        #region PutAsync

        [Fact]
        public async Task PutAsync_ShouldCallDalPutAsycOnce_WhenCalled()
        {
            //Arrange
            var officeViewModel = Builder<OfficeViewModel>.CreateNew().Build();
            var office = Builder<Office>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Office>(officeViewModel)).Returns(office);
            var service = CreateOfficeService();

            //Act
            await service.PutAsync(officeViewModel);

            //Assert
            mockOfficeDal.Verify(x => x.PutAsync(office), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldReturnsOfficeViewModel_WhenSuccessful()
        {
            //Arrange
            var office = Builder<Office>.CreateNew().Build();
            var officeViewModel = Builder<OfficeViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Office>(officeViewModel)).Returns(office);
            mockMapper.Setup(x => x.Map<OfficeViewModel>(office)).Returns(officeViewModel);
            mockOfficeDal.Setup(x => x.PutAsync(office)).ReturnsAsync(office);
            var service = CreateOfficeService();
            //Act
            var result = await service.PutAsync(officeViewModel);

            //Assert
            result.Should().BeEquivalentTo(officeViewModel);
        }

        #endregion PutAsync


        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_ShouldCallDalDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var service = CreateOfficeService();

            //Act
            await service.DeleteAsync(id);

            //Assert
            mockOfficeDal.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteOffice_WhenSuccessful()
        {
            //Arrange
            var deletedOffice = Builder<Office>.CreateNew().Build();
            var officeViewModel = Builder<OfficeViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Office>(officeViewModel)).Returns(deletedOffice);
            mockOfficeDal.Setup(x => x.PostAsync(deletedOffice));
            var service = CreateOfficeService();

            //Act
            await service.DeleteAsync(officeViewModel.Id);

            //Assert
            (await service.GetAllAsync()).Should().NotContain(officeViewModel);
        }

        #endregion DeleteAsync


        private OfficeService CreateOfficeService()
        {
            return new OfficeService(mockOfficeDal.Object, mockMapper.Object, mockCountryDal.Object);
        }
    }
}