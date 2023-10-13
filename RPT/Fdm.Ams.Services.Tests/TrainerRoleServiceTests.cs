using AutoMapper;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.ViewModels;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Fdm.Ams.Services.Tests
{
    public class TrainerRoleServiceTests
    {
        private Mock<ITrainerRoleDal> mockDal;

        private Mock<IMapper> mockMapper;

        public TrainerRoleServiceTests()
        {
            mockDal = new Mock<ITrainerRoleDal>();
            mockMapper = new Mock<IMapper>();
        }

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange

            var service = CreateTrainerRoleService();
            //Act
            await service.GetAllAsync();
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAnEnumerableOfTrainerRoleViewModel_WhenSuccessful()
        {
            //Arrange
            var trainerRoleList = Builder<TrainerRole>.CreateListOfSize(3).Build();
            var trainerRoleViewModelList = Builder<TrainerRoleViewModel>.CreateListOfSize(3).Build();
            mockMapper.Setup(x => x.Map<IEnumerable<TrainerRoleViewModel>>(trainerRoleList)).Returns(trainerRoleViewModelList);
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(trainerRoleList);
            var service = CreateTrainerRoleService();
            //Act
            var result = await service.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(trainerRoleList);
        }

        #endregion GetAllAsync

        #region GetByIdAsync

        [Fact]
        public async Task GetByIdAsync_CallsDalGetByIdAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var service = CreateTrainerRoleService();
            //Act
            await service.GetByIdAsync(id);
            //Assert
            mockDal.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnsTrainerRoleFromDal_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var trainerRole = Builder<TrainerRole>.CreateNew().Build();
            var trainerRoleViewModel = Builder<TrainerRoleViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<TrainerRoleViewModel>(trainerRole)).Returns(trainerRoleViewModel);
            mockDal.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(trainerRole);
            var service = CreateTrainerRoleService();
            //Act
            var result = await service.GetByIdAsync(id);
            //Assert
            result.Should().BeEquivalentTo(trainerRole);
        }

        #endregion GetByIdAsync

        #region PutAsync

        [Fact]
        public async Task PutAsync_ShouldCallDalPutAsycOnce_WhenCalled()
        {
            //Arrange
            var trainerRoleViewModel = Builder<TrainerRoleViewModel>.CreateNew().Build();
            var trainerRole = Builder<TrainerRole>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<TrainerRole>(trainerRoleViewModel)).Returns(trainerRole);
            var service = CreateTrainerRoleService();

            //Act
            await service.PutAsync(trainerRoleViewModel);

            //Assert
            mockDal.Verify(x => x.PutAsync(trainerRole), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldReturnTrainerRoleViewModel_WhenSuccessful()
        {
            //Arrange
            var trainerRole = Builder<TrainerRole>.CreateNew().Build();
            var trainerRoleViewModel = Builder<TrainerRoleViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<TrainerRole>(trainerRoleViewModel)).Returns(trainerRole);
            mockMapper.Setup(x => x.Map<TrainerRoleViewModel>(trainerRole)).Returns(trainerRoleViewModel);
            mockDal.Setup(x => x.PutAsync(trainerRole)).ReturnsAsync(trainerRole);
            var service = CreateTrainerRoleService();
            //Act
            var result = await service.PutAsync(trainerRoleViewModel);

            //Assert
            result.Should().BeEquivalentTo(trainerRoleViewModel);
        }

        #endregion PutAsync

        #region PostAsync

        [Fact]
        public async Task PostAsync_CallsDalPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var trainerRole = Builder<TrainerRole>.CreateNew().Build();
            var trainerRoleViewModel = Builder<TrainerRoleViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<TrainerRole>(trainerRoleViewModel)).Returns(trainerRole);
            var service = CreateTrainerRoleService();

            //Act
            await service.PostAsync(trainerRoleViewModel);

            //Assert
            mockDal.Verify(x => x.PostAsync(trainerRole), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldReturnsTrainerRoleViewModel_WhenCalled()
        {
            //Arrange
            var trainerRole = Builder<TrainerRole>.CreateNew().Build();
            var trainerRoleViewModel = Builder<TrainerRoleViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<TrainerRole>(trainerRoleViewModel)).Returns(trainerRole);
            mockMapper.Setup(x => x.Map<TrainerRoleViewModel>(trainerRole)).Returns(trainerRoleViewModel);
            mockDal.Setup(x => x.PostAsync(trainerRole)).ReturnsAsync(trainerRole);

            var service = CreateTrainerRoleService();
            //Act
            var result = await service.PostAsync(trainerRoleViewModel);
            //Assert
            result.Should().BeEquivalentTo(trainerRoleViewModel);
        }

        #endregion PostAsync

        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_ShouldCallDalDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var service = CreateTrainerRoleService();

            //Act
            await service.DeleteAsync(id);

            //Assert
            mockDal.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        #endregion DeleteAsync

        private TrainerRoleService CreateTrainerRoleService()
        {
            return new TrainerRoleService(mockDal.Object, mockMapper.Object);
        }
    }
}