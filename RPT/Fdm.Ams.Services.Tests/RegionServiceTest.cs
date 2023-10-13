using AutoMapper;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.ViewModels;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Fdm.Ams.Services.Tests
{
    public class RegionServiceTests
    {
        private Mock<IRegionDal> mockDal;
        private Mock<IMapper> mockMapper;

        public RegionServiceTests()
        {
            mockDal = new Mock<IRegionDal>();
            mockMapper = new Mock<IMapper>();
        }

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var service = CreateRegionService();
            //Act
            await service.GetAllAsync();
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAnEnumerableOfRegionFromDal_WhenSuccessful()
        {
            //Arrange
            var list = Builder<Region>.CreateListOfSize(3).Build();
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var service = CreateRegionService();
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
            var service = CreateRegionService();
            //Act
            await service.GetByIdAsync(id);
            //Assert
            mockDal.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsRegionFromDal_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var region = Builder<Region>.CreateNew().Build();
            var regionViewModel = Builder<RegionViewModel>.CreateNew().Build();
            mockDal.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(region);
            mockMapper.Setup(x => x.Map<RegionViewModel>(region)).Returns(regionViewModel);
            var service = CreateRegionService();
            //Act
            var result = await service.GetByIdAsync(id);
            //Assert
            result.Should().BeEquivalentTo(region);
        }

        #endregion GetByIdAsync

        #region PutAsync

        [Fact]
        public async Task PutAsync_CallsDalPutAsycOnce_WhenCalled()
        {
            //Arrange
            var regionViewModel = Builder<RegionViewModel>.CreateNew().Build();
            var region = Builder<Region>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Region>(regionViewModel)).Returns(region);
            var service = CreateRegionService();

            //Act
            await service.PutAsync(regionViewModel);

            //Assert
            mockDal.Verify(x => x.PutAsync(region), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ReturnsRegionViewModel_WhenSuccessful()
        {
            //Arrange
            var region = Builder<Region>.CreateNew().Build();

            mockDal.Setup(x => x.PutAsync(region)).ReturnsAsync(region);

            var regionViewModel = Builder<RegionViewModel>.CreateNew().Build();

            mockMapper.Setup(x => x.Map<Region>(regionViewModel)).Returns(region);
            mockMapper.Setup(x => x.Map<RegionViewModel>(region)).Returns(regionViewModel);
            var service = CreateRegionService();

            //Act
            var result = await service.PutAsync(regionViewModel);

            //Assert
            result.Should().BeEquivalentTo(regionViewModel);
        }

        #endregion PutAsync

        #region CreateAsync

        [Fact]
        public async Task CreateAsync_CallsDalPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var region = Builder<Region>.CreateNew().Build();
            var regionViewModel = Builder<RegionViewModel>.CreateNew().Build();

            mockMapper.Setup(x => x.Map<Region>(regionViewModel)).Returns(region);

            var service = CreateRegionService();

            //Act
            await service.PostAsync(regionViewModel);

            //Assert
            mockDal.Verify(x => x.PostAsync(region), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ReturnsRegion_WhenSuccessful()
        {
            //Arrange
            var region = Builder<Region>.CreateNew().Build();
            var regionViewModel = Builder<RegionViewModel>.CreateNew().Build();

            mockDal.Setup(x => x.PostAsync(region)).ReturnsAsync(region);
            mockMapper.Setup(x => x.Map<Region>(regionViewModel)).Returns(region);
            mockMapper.Setup(x => x.Map<RegionViewModel>(region)).Returns(regionViewModel);

            var service = CreateRegionService();

            //Act
            var result = await service.PostAsync(regionViewModel);

            //Assert
            result.Should().BeEquivalentTo(region);
        }

        #endregion CreateAsync

        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_CallsDalDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var service = CreateRegionService();

            //Act
            await service.DeleteAsync(id);

            //Assert
            mockDal.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WillDeleteRegion_WhenCalled()
        {
            //Arrange
            var Region = Builder<Region>.CreateNew().Build();
            var region = Builder<Region>.CreateNew().Build();
            int id = Region.Id;
            mockDal.Setup(x => x.PostAsync(region)).ReturnsAsync(Region);
            var Service = CreateRegionService();
            //Act
            await Service.DeleteAsync(id);
            //Assert
            Assert.DoesNotContain(Region, await Service.GetAllAsync());
        }

        #endregion DeleteAsync

        private RegionService CreateRegionService()
        {
            return new RegionService(mockDal.Object, mockMapper.Object);
        }
    }
}