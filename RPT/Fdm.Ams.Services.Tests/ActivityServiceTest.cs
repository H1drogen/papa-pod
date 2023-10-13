using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Fdm.Ams.Services.Tests
{
    public class ActivityServiceTest
    {
        private Mock<IActivityDal> mockDal;

        public ActivityServiceTest()
        {
            mockDal = new Mock<IActivityDal>();
        }

        private ActivityService ServiceHelper()
        {
            return new ActivityService(mockDal.Object);
        }

        #region GetAll

        [Fact]
        public async Task GetAll_CallsDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var service = ServiceHelper();
            //Act
            await service.GetAll();
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAll_ReturnsIEnumerableOfActivity_WhenSuccessful()
        {
            //Arrange
            var list = Builder<Activity>.CreateListOfSize(3).Build();
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var service = ServiceHelper();
            //Act
            var result = await service.GetAll();
            //Assert
            result.Should().BeEquivalentTo(list);
        }

        #endregion GetAll

        #region GetAllByConsultantId

        [Fact]
        public async Task GetAllByConsultantId_CallsDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var service = ServiceHelper();
            //Act
            await service.GetAll();
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllByConsultantId_ReturnsActivityWithMatchingConsultantId_WhenSuccessful()
        {
            //Arrange
            int consultantId = 1;
            var list = Builder<Activity>.CreateListOfSize(5).Random(2).With(x => x.ConsultantId = consultantId).Build();
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var expectedResult = list.Where(x => x.ConsultantId == consultantId);
            var service = ServiceHelper();
            //Act
            var result = await service.GetAllByConsultantId(consultantId);
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetAllByConsultantId
    }
}