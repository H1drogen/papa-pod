using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Fdm.Ams.Services.Tests
{
    public class ActivityTypeServiceTest
    {
        private Mock<IActivityTypeDal> mockDal;

        public ActivityTypeServiceTest()
        {
            mockDal = new Mock<IActivityTypeDal>();
        }

        #region GetActivityTypes

        [Fact]
        public async Task GetActivityTypes_CallsDalGetAllAsyncOnce_WhenSuccessful()
        {
            //Arrange
            var service = ServiceHelper();
            //Act
            await service.GetActivityTypes();
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetActivityTypes_ReturnsIEnumerableOfActivityType_WhenCalled()
        {
            //Arrange
            var list = Builder<ActivityType>.CreateListOfSize(3).Build();
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var service = ServiceHelper();
            //Act
            var result = await service.GetActivityTypes();
            //Assert
            result.Should().BeEquivalentTo(list);
        }

        #endregion GetActivityTypes

        #region GetActivityTypeById

        [Fact]
        public async Task GetById_CallsDalGetByIdAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var service = ServiceHelper();
            //Act
            await service.GetById(id);
            //Assert
            mockDal.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetById_ReturnsActivityType_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var activityType = Builder<ActivityType>.CreateNew().Build();
            mockDal.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(activityType);
            var service = ServiceHelper();
            //Act
            var result = await service.GetById(id);
            //Assert
            result.Should().Be(activityType);
        }

        #endregion GetActivityTypeById

        private ActivityTypeService ServiceHelper()
        {
            return new ActivityTypeService(mockDal.Object);
        }
    }
}