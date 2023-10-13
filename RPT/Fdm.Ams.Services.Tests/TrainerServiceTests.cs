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
    public class TrainerServiceTests
    {
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IOfficeDal> mockOfficeDal;
        private Mock<ITrainerDal> mockTrainerDal;

        public TrainerServiceTests()
        {
            mockTrainerDal = new Mock<ITrainerDal>();
            mockOfficeDal = new Mock<IOfficeDal>();
            mockMapper = new Mock<IMapper>();
        }

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var service = CreateTrainerService();
            //Act
            await service.GetAllAsync();
            //Assert
            mockTrainerDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAnEnumerableOfTrainerFromDal_WhenSuccessful()
        {
            //Arrange
            var list = Builder<Trainer>.CreateListOfSize(3).Build();
            mockTrainerDal.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var service = CreateTrainerService();
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
            var service = CreateTrainerService();
            //Act
            await service.GetByIdAsync(id);
            //Assert
            mockTrainerDal.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnsTrainerViewModel_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var trainerViewModel = Builder<TrainerViewModel>.CreateNew().Build();
            var trainer = Builder<Trainer>.CreateNew().Build();
            mockTrainerDal.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(trainer);
            mockMapper.Setup(x => x.Map<TrainerViewModel>(trainer)).Returns(trainerViewModel);
            var service = CreateTrainerService();
            //Act
            var result = await service.GetByIdAsync(id);
            //Assert
            result.Should().BeEquivalentTo(trainerViewModel);
        }

        #endregion GetByIdAsync

        #region PutAsync

        [Fact]
        public async Task PutAsync_CallsDalPutAsycOnce_WhenCalled()
        {
            //Arrange
            var trainerViewModel = Builder<TrainerViewModel>.CreateNew().Build();
            var trainer = Builder<Trainer>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Trainer>(trainerViewModel)).Returns(trainer);
            var service = CreateTrainerService();

            //Act
            await service.PutAsync(trainerViewModel);

            //Assert
            mockTrainerDal.Verify(x => x.PutAsync(trainer), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldReturnsTrainerViewModel_WhenCalled()
        {
            //Arrange
            var trainer = Builder<Trainer>.CreateNew().Build();
            var trainerViewModel = Builder<TrainerViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Trainer>(trainerViewModel)).Returns(trainer);
            mockMapper.Setup(x => x.Map<TrainerViewModel>(trainer)).Returns(trainerViewModel);
            mockTrainerDal.Setup(x => x.PutAsync(trainer)).ReturnsAsync(trainer);
            var service = CreateTrainerService();
            //Act
            var actualResult = await service.PutAsync(trainerViewModel);
            //Assert
            actualResult.Should().BeEquivalentTo(trainerViewModel);
        }

        #endregion PutAsync

        #region GetTrainerWithOfficeList

        [Fact]
        public async Task GetTrainerWithOfficeList_CallsOfficeDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var offices = Builder<Office>.CreateListOfSize(2).Build();
            mockOfficeDal.Setup(x => x.GetAllAsync()).ReturnsAsync(offices);
            var service = CreateTrainerService();

            //Act
            await service.GetTrainerWithOfficeList();

            //Assert
            mockOfficeDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetTrainerWithOfficeList_ShouldReturnTrainerViewModelWithDictionaryOfOffices_WhenCalled()
        {
            //Arrange
            var service = CreateTrainerService();

            var offices = Builder<Office>.CreateListOfSize(3).Build();

            Dictionary<int, string> officesDictionary = new();

            offices.ToList().ForEach(x => officesDictionary.Add(x.Id, x.Name));

            var trainerViewModel = Builder<TrainerViewModel>.CreateNew()
                .With(x => x.OfficeDictionaries = officesDictionary)
                .Build();

            mockOfficeDal.Setup(x => x.GetAllAsync()).ReturnsAsync(offices);

            //Act
            var result = await service.GetTrainerWithOfficeList();

            //Assert
            result.Should().BeEquivalentTo(trainerViewModel, config =>
            config
            .Excluding(x => x.FirstName)
            .Excluding(x => x.LastName)
            .Excluding(x => x.OfficeId)
            .Excluding(x => x.TeamName)
            .Excluding(x => x.Username)
            .Excluding(x => x.Email)
            .Excluding(x => x.Active)
            .Excluding(x => x.Id)
            );
        }

        #endregion GetTrainerWithOfficeList

        #region PostAsync

        [Fact]
        public async Task PostAsync_CallsDalPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var trainer = Builder<Trainer>.CreateNew().Build();
            var trainerViewModel = Builder<TrainerViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Trainer>(trainerViewModel)).Returns(trainer);
            var service = CreateTrainerService();

            //Act
            await service.PostAsync(trainerViewModel);

            //Assert
            mockTrainerDal.Verify(x => x.PostAsync(trainer), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldReturnsTrainerViewModel_WhenSuccessful()
        {
            //Arrange
            var trainer = Builder<Trainer>.CreateNew().Build();
            var trainerViewModel = Builder<TrainerViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Trainer>(trainerViewModel)).Returns(trainer);
            mockMapper.Setup(x => x.Map<TrainerViewModel>(trainer)).Returns(trainerViewModel);
            mockTrainerDal.Setup(x => x.PostAsync(trainer)).ReturnsAsync(trainer);
            var service = CreateTrainerService();
            //Act
            var result = await service.PostAsync(trainerViewModel);
            //Assert
            result.Should().BeEquivalentTo(trainerViewModel);
        }

        #endregion PostAsync

        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_CallsDalDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var service = CreateTrainerService();

            //Act
            await service.DeleteAsync(id);

            //Assert
            mockTrainerDal.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteTrainer_WhenCalled()
        {
            //Arrange
            var trainer = Builder<Trainer>.CreateNew().Build();
            var trainerViewModel = Builder<TrainerViewModel>.CreateNew().Build();
            int id = trainer.Id;
            mockTrainerDal.Setup(x => x.PostAsync(trainer)).ReturnsAsync(trainer);
            var service = CreateTrainerService();
            //Act
            await service.DeleteAsync(id);
            //Assert
            Assert.DoesNotContain(trainer, await service.GetAllAsync());
        }

        #endregion DeleteAsync

        private TrainerService CreateTrainerService()
        {
            return new TrainerService(mockTrainerDal.Object, mockOfficeDal.Object, mockMapper.Object);
        }
    }
}