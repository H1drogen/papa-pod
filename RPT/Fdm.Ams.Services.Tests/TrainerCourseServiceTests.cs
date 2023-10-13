using AutoMapper;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Fdm.Ams.Services.Tests
{
    public class TrainerCourseServiceTests
    {
        private Mock<ITrainerCourseDal> mockDal;
        private Mock<IMapper> mockMapper;

        public TrainerCourseServiceTests()
        {
            mockDal = new Mock<ITrainerCourseDal>();
            mockMapper = new Mock<IMapper>();
        }

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_ShouldCallDalGetAllAsyncGetAsyncOnce_WhenCalled()
        {
            //Arrange
            var service = CreateTrainerCourseService();
            //Act
            await service.GetAllAsync();
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAnEnumerableOfTrainerCourse_WhenSuccessful()
        {
            //Arrange
            var trainerCourseList = Builder<TrainerCourse>.CreateListOfSize(3).Build();

            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(trainerCourseList);
            var service = CreateTrainerCourseService();
            //Act
            var result = await service.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(trainerCourseList);
        }

        #endregion GetAllAsync

        #region GetById

        [Fact]
        public async Task GetByIdAsync_ShouldReturnTrainerCourseFromDal_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var trainerCourse = Builder<TrainerCourse>.CreateNew().Build();

            mockDal.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(trainerCourse);
            var service = CreateTrainerCourseService();

            //Act
            var result = await service.GetByIdAsync(id);

            //Assert
            result.Should().BeEquivalentTo(trainerCourse);
        }

        #endregion GetById

        #region PostAsync

        [Fact]
        public async Task PostAsync_ShoudReturnTrainerCourse_WhenCalled()
        {
            //Arrange
            var trainerCourse = Builder<TrainerCourse>.CreateNew().Build();
            mockDal.Setup(x => x.PostAsync(trainerCourse)).ReturnsAsync(trainerCourse);

            var service = CreateTrainerCourseService();
            //Act
            var result = await service.PostAsync(trainerCourse);
            //Assert
            result.Should().BeEquivalentTo(trainerCourse);
        }

        [Fact]
        public async Task PostAsync_ShouldCallDalPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var trainerCourse = Builder<TrainerCourse>.CreateNew().Build();

            var service = CreateTrainerCourseService();
            //Act
            await service.PostAsync(trainerCourse);
            //Assert
            mockDal.Verify(x => x.PostAsync(trainerCourse), Times.Once);
        }

        #endregion PostAsync

        #region PutAsync

        [Fact]
        public async Task PutAsync_ShouldCallDalPutAsync_WhenCalled()
        {
            //Arrange
            var trainerCourse = Builder<TrainerCourse>.CreateNew().Build();

            var service = CreateTrainerCourseService();
            //Act
            await service.PutAsync(trainerCourse);
            //Assert
            mockDal.Verify(x => x.PutAsync(trainerCourse), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldReturnTrainerCourse_WhenCalled()
        {
            //Arrange
            var trainerCourse = Builder<TrainerCourse>.CreateNew().Build();

            mockDal.Setup(x => x.PutAsync(trainerCourse)).ReturnsAsync(trainerCourse);
            var service = CreateTrainerCourseService();
            //Act
            var actualResult = await service.PutAsync(trainerCourse);
            //Assert
            actualResult.Should().BeEquivalentTo(trainerCourse);
        }

        #endregion PutAsync

        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_ShouldCallDalDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var service = CreateTrainerCourseService();

            //Act
            await service.DeleteAsync(id);

            //Assert
            mockDal.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        #endregion DeleteAsync

        private TrainerCourseService CreateTrainerCourseService()
        {
            return new TrainerCourseService(mockDal.Object, mockMapper.Object);
        }
    }
}