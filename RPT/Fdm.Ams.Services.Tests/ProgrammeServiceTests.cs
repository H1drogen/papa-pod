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
    public class ProgrammeServiceTests
    {
        private Mock<IProgrammeDal> mockDal;

        private Mock<IMapper> mockMapper;

        public ProgrammeServiceTests()
        {
            mockDal = new Mock<IProgrammeDal>();
            mockMapper = new Mock<IMapper>();
        }

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange

            var service = CreateProgrammeService();
            //Act
            await service.GetAllAsync();
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAnEnumerableOfProgrammeViewModel_WhenSuccessful()
        {
            //Arrange
            var programmeList = Builder<Programme>.CreateListOfSize(3).Build();
            var programmeViewModelList = Builder<ProgrammeViewModel>.CreateListOfSize(3).Build();
            mockMapper.Setup(x => x.Map<IEnumerable<ProgrammeViewModel>>(programmeList)).Returns(programmeViewModelList);
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(programmeList);
            var service = CreateProgrammeService();
            //Act
            var result = await service.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(programmeList);
        }

        #endregion GetAllAsync

        #region GetByIdAsync

        [Fact]
        public async Task GetByIdAsync_CallsDalGetByIdAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var service = CreateProgrammeService();
            //Act
            await service.GetByIdAsync(id);
            //Assert
            mockDal.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnsProgrammeViewModel_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var programme = Builder<Programme>.CreateNew().Build();
            var programmeViewModel = Builder<ProgrammeViewModel>.CreateNew().Build();
            mockDal.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(programme);
            mockMapper.Setup(x => x.Map<ProgrammeViewModel>(programme)).Returns(programmeViewModel);
            var service = CreateProgrammeService();
            //Act
            var result = await service.GetByIdAsync(id);
            //Assert
            result.Should().BeEquivalentTo(programmeViewModel);
        }

        #endregion GetByIdAsync

        #region PostAsync

        [Fact]
        public async Task PostAsync_CallsDalPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var programme = Builder<Programme>.CreateNew().Build();
            var programmeViewModel = Builder<ProgrammeViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Programme>(programmeViewModel)).Returns(programme);
            var service = CreateProgrammeService();

            //Act
            await service.PostAsync(programmeViewModel);

            //Assert
            mockDal.Verify(x => x.PostAsync(programme), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldReturnsProgrammeViewModel_WhenCalled()
        {
            //Arrange
            var programme = Builder<Programme>.CreateNew().Build();
            var programmeViewModel = Builder<ProgrammeViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<Programme>(programmeViewModel)).Returns(programme);
            mockMapper.Setup(x => x.Map<ProgrammeViewModel>(programme)).Returns(programmeViewModel);
            mockDal.Setup(x => x.PostAsync(programme)).ReturnsAsync(programme);

            var service = CreateProgrammeService();
            //Act
            var result = await service.PostAsync(programmeViewModel);
            //Assert
            result.Should().BeEquivalentTo(programmeViewModel);
        }

        #endregion PostAsync

        private ProgrammeService CreateProgrammeService()
        {
            return new ProgrammeService(mockDal.Object, mockMapper.Object);
        }
    }
}