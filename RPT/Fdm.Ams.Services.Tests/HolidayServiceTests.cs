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
    public class HolidayServiceTests
    {
        private Mock<ICountryDal> mockCountryDal;
        private Mock<IHolidayDal> mockDal;

        private Mock<IMapper> mockMapper;

        public HolidayServiceTests()
        {
            mockDal = new Mock<IHolidayDal>();
            mockMapper = new Mock<IMapper>();
            mockCountryDal = new Mock<ICountryDal>();
        }

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange

            var service = CreateHolidayService();
            //Act
            await service.GetAllAsync();
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAnEnumerableOfHolidayViewModel_WhenSuccessful()
        {
            //Arrange
            var holidayList = Builder<Holiday>.CreateListOfSize(3).Build();
            var holidayViewModelList = Builder<HolidayViewModel>.CreateListOfSize(3).Build();
            mockMapper.Setup(x => x.Map<IEnumerable<HolidayViewModel>>(holidayList)).Returns(holidayViewModelList);
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(holidayList);
            var service = CreateHolidayService();
            //Act
            var result = await service.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(holidayList);
        }

        #endregion GetAllAsync

        #region GetByIdAsync

        [Fact]
        public async Task GetByIdAsync_CallsDalGetByIdAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var service = CreateHolidayService();
            //Act
            await service.GetByIdAsync(id);
            //Assert
            mockDal.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnHolidayFromDal_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var holiday = Builder<Holiday>.CreateNew().Build();
            var holidayViewModel = Builder<HolidayViewModel>.CreateNew().Build();
            mockDal.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(holiday);
            mockMapper.Setup(x => x.Map<HolidayViewModel>(holiday)).Returns(holidayViewModel);
            var service = CreateHolidayService();
            //Act
            var result = await service.GetByIdAsync(id);
            //Assert
            result.Should().BeEquivalentTo(holidayViewModel);
        }

        #endregion GetByIdAsync

        #region PostAsync

        [Fact]
        public async Task PostAsync_ShouldCallDalPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var holiday = Builder<Holiday>.CreateNew().Build();
            var holidayViewModel = Builder<HolidayViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Holiday>(holidayViewModel)).Returns(holiday);
            var service = CreateHolidayService();

            //Act
            await service.PostAsync(holidayViewModel);

            //Assert
            mockDal.Verify(x => x.PostAsync(holiday), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldReturnHolidayViewModel_WhenCalled()
        {
            //Arrange
            var holiday = Builder<Holiday>.CreateNew().Build();
            var holidayViewModel = Builder<HolidayViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Holiday>(holidayViewModel)).Returns(holiday);
            mockMapper.Setup(x => x.Map<HolidayViewModel>(holiday)).Returns(holidayViewModel);
            mockDal.Setup(x => x.PostAsync(holiday)).ReturnsAsync(holiday);

            var service = CreateHolidayService();
            //Act
            var result = await service.PostAsync(holidayViewModel);
            //Assert
            result.Should().BeEquivalentTo(holidayViewModel);
        }

        #endregion PostAsync

        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_ShouldCallDalDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var service = CreateHolidayService();

            //Act
            await service.DeleteAsync(id);

            //Assert
            mockDal.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteHoliday_WhenCalled()
        {
            //Arrange
            var holiday = Builder<Holiday>.CreateNew().Build();
            var holidayViewModel = Builder<HolidayViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Holiday>(holidayViewModel)).Returns(holiday);

            var Service = CreateHolidayService();
            //Act
            await Service.DeleteAsync(holiday.Id);
            //Assert
            (await Service.GetAllAsync()).Should().NotContain(holidayViewModel);
        }

        #endregion DeleteAsync

        #region PutAsync

        [Fact]
        public async Task PutAsync_ShouldCallDalPutAsycOnce_WhenCalled()
        {
            //Arrange
            var holidayViewModel = Builder<HolidayViewModel>.CreateNew().Build();
            var holiday = Builder<Holiday>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Holiday>(holidayViewModel)).Returns(holiday);
            var service = CreateHolidayService();

            //Act
            await service.PutAsync(holidayViewModel);

            //Assert
            mockDal.Verify(x => x.PutAsync(holiday), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldReturnHolidayViewModel_WhenSuccessful()
        {
            //Arrange
            var holiday = Builder<Holiday>.CreateNew().Build();
            var holidayViewModel = Builder<HolidayViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Holiday>(holidayViewModel)).Returns(holiday);
            mockMapper.Setup(x => x.Map<HolidayViewModel>(holiday)).Returns(holidayViewModel);
            mockDal.Setup(x => x.PutAsync(holiday)).ReturnsAsync(holiday);
            var service = CreateHolidayService();
            //Act
            var result = await service.PutAsync(holidayViewModel);

            //Assert
            result.Should().BeEquivalentTo(holidayViewModel);
        }

        #endregion PutAsync

        #region GetCountries

        [Fact]
        public async Task GetCountries_ShouldCallCountryDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var service = CreateHolidayService();
            var country = Builder<Country>.CreateListOfSize(2).Build();
            mockCountryDal.Setup(x => x.GetAllAsync()).ReturnsAsync(country);

            //Act
            await service.GetCountries();

            //Assert
            mockCountryDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetCountries_ShouldReturnHolidayViewModel_WhenCalled()
        {
            //Arrange
            var service = CreateHolidayService();

            IEnumerable<Country> countryList = Builder<Country>.CreateListOfSize(3).Build();
            Dictionary<int, string> countryDictionary = new();
            var holidayViewModel = Builder<HolidayViewModel>.CreateNew().With(x => x.CountryDictionary = countryDictionary).Build();

            countryList.ToList().ForEach(x => countryDictionary.Add(x.Id, x.Name));
            mockCountryDal.Setup(x => x.GetAllAsync()).ReturnsAsync(countryList);

            //Act
            var result = await service.GetCountries();
            //Assert
            result.Should().BeEquivalentTo(holidayViewModel, options =>
            {
                options.Excluding(hv => hv.CountryId);
                options.Excluding(hv => hv.Description);
                options.Excluding(hv => hv.Id);
                options.Excluding(hv => hv.EndDate);
                options.Excluding(hv => hv.Name);
                options.Excluding(hv => hv.StartDate);
                return options;
            });
        }

        #endregion GetCountries

        private HolidayService CreateHolidayService()
        {
            return new HolidayService(mockDal.Object, mockMapper.Object, mockCountryDal.Object);
        }
    }
}