using AutoMapper;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.ViewModels;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Fdm.Ams.Services.Tests
{
    public class CountryServiceTests
    {
        private Mock<ICountryDal> mockCountryDal;
        private Mock<IMapper> mockMapper;
        private Mock<IRegionDal> mockRegionDal;

        public CountryServiceTests()
        {
            mockCountryDal = new Mock<ICountryDal>();
            mockRegionDal = new Mock<IRegionDal>();
            mockMapper = new Mock<IMapper>();
        }

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var service = CreateCountryService();
            //Act
            await service.GetAllAsync();
            //Assert
            mockCountryDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAnEnumerableOfCountryFromDal_WhenSuccessful()
        {
            //Arrange
            var list = Builder<Country>.CreateListOfSize(3).Build();
            mockCountryDal.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var service = CreateCountryService();
            //Act
            var result = await service.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(list);
        }

        #endregion GetAllAsync

        #region GetByIdAsync

        [Fact]
        public async Task GetByIdAsync_CallsDalGetByIdAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var service = CreateCountryService();
            //Act
            await service.GetByIdAsync(id);
            //Assert
            mockCountryDal.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnsCountryViewModel_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var countryViewModel = Builder<CountryViewModel>.CreateNew().Build();
            var country = Builder<Country>.CreateNew().Build();
            mockCountryDal.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(country);
            mockMapper.Setup(x => x.Map<CountryViewModel>(country)).Returns(countryViewModel);
            var service = CreateCountryService();
            //Act
            var result = await service.GetByIdAsync(id);
            //Assert
            result.Should().BeEquivalentTo(countryViewModel);
        }

        #endregion GetByIdAsync

        #region PutAsync

        [Fact]
        public async Task PutAsync_CallsDalPutAsycOnce_WhenCalled()
        {
            //Arrange
            var countryViewModel = Builder<CountryViewModel>.CreateNew().Build();
            var country = Builder<Country>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Country>(countryViewModel)).Returns(country);
            var service = CreateCountryService();

            //Act
            await service.PutAsync(countryViewModel);

            //Assert
            mockCountryDal.Verify(x => x.PutAsync(country), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldReturnsCountryViewModel_WhenSuccessful()
        {
            //Arrange
            var country = Builder<Country>.CreateNew().Build();
            var countryViewModel = Builder<CountryViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Country>(countryViewModel)).Returns(country);
            mockMapper.Setup(x => x.Map<CountryViewModel>(country)).Returns(countryViewModel);
            mockCountryDal.Setup(x => x.PutAsync(country)).ReturnsAsync(country);
            var service = CreateCountryService();
            //Act
            var result = await service.PutAsync(countryViewModel);

            //Assert
            result.Should().BeEquivalentTo(countryViewModel);
        }

        #endregion PutAsync

        #region GetCountryWithRegionList

        [Fact]
        public async Task GetCountryWithRegionList_ShouldCallsRegionDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var regions = Builder<Region>.CreateListOfSize(2).Build();
            mockRegionDal.Setup(x => x.GetAllAsync()).ReturnsAsync(regions);
            var service = CreateCountryService();

            //Act
            await service.GetCountryWithRegionList();

            //Assert
            mockRegionDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetCountryWithRegionList_ShouldReturnCountryViewModelWithDictionaryOfRegions_WhenCalled()
        {
            //Arrange
            var service = CreateCountryService();

            var regions = Builder<Region>.CreateListOfSize(3).Build();

            Dictionary<int, string> regionDictionary = new();

            regions.ToList().ForEach(x => regionDictionary.Add(x.Id, x.Name));

            var countryViewModel = Builder<CountryViewModel>.CreateNew()
                .With(x => x.RegionDictionaries = regionDictionary)
                .Build();

            mockRegionDal.Setup(x => x.GetAllAsync()).ReturnsAsync(regions);

            //Act
            var result = await service.GetCountryWithRegionList();

            //Assert
            result.Should().BeEquivalentTo(countryViewModel, config =>
            config
            .Excluding(x => x.Name)
            .Excluding(x => x.IsActive)
            .Excluding(x => x.RegionId)
            .Excluding(x => x.Id)
            );
        }

        #endregion GetCountryWithRegionList

        #region PostAsync

        [Fact]
        public async Task PostAsync_CallsDalPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var country = Builder<Country>.CreateNew().Build();
            var countryViewModel = Builder<CountryViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Country>(countryViewModel)).Returns(country);
            var service = CreateCountryService();

            //Act
            await service.PostAsync(countryViewModel);

            //Assert
            mockCountryDal.Verify(x => x.PostAsync(country), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldReturnsCountryViewModel_WhenSuccessful()
        {
            //Arrange
            var countryViewModel = Builder<CountryViewModel>.CreateNew().Build();
            var country = Builder<Country>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Country>(countryViewModel)).Returns(country);
            mockMapper.Setup(x => x.Map<CountryViewModel>(country)).Returns(countryViewModel);
            mockCountryDal.Setup(x => x.PostAsync(country)).ReturnsAsync(country);
            var service = CreateCountryService();

            //Act
            var result = await service.PostAsync(countryViewModel);

            //Assert
            result.Should().BeEquivalentTo(countryViewModel);
        }

        #endregion PostAsync

        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_ShouldCallsDalDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var service = CreateCountryService();

            //Act
            await service.DeleteAsync(id);

            //Assert
            mockCountryDal.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteCountry_WhenCalled()
        {
            //Arrange
            var country = Builder<Country>.CreateNew().Build();
            mockCountryDal.Setup(x => x.PostAsync(country)).ReturnsAsync(country);
            var Service = CreateCountryService();
            //Act
            await Service.DeleteAsync(country.Id);
            //Assert
            (await Service.GetAllAsync()).Should().NotContain(country);
        }

        #endregion DeleteAsync

        private CountryService CreateCountryService()
        {
            return new CountryService(mockCountryDal.Object, mockRegionDal.Object, mockMapper.Object);
        }
    }
}