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
    public class GlobalGeoflexServiceTest
    {
        private Mock<IGlobalGeoflexDal> mockDal;

        public GlobalGeoflexServiceTest()
        {
            mockDal = new Mock<IGlobalGeoflexDal>();
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
        public async Task GetAll_ReturnsEnumerableOfGlobalGeoflex_WhenSuccessful()
        {
            //Arrange
            var list = Builder<GlobalGeoflex>.CreateListOfSize(3).Build();
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var service = ServiceHelper();
            //Act
            var result = await service.GetAll();
            //Assert
            result.Should().BeEquivalentTo(list);
        }

        #endregion GetAll

        #region GetAllByConsultantantId

        [Fact]
        public async Task GetAllByConsultantId_CallsGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            int consultantId = 1;
            var list = Builder<GlobalGeoflex>.CreateListOfSize(3).Build();
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var service = ServiceHelper();
            //Act
            await service.GetAllByConsultantId(consultantId);
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllByConsultantId_ReturnsGlobalGeoflexWithMatchingConsultantId_WhenSuccessful()
        {
            //Arrange
            int consultantId = 1;
            var list = Builder<GlobalGeoflex>.CreateListOfSize(5)
                .Random(2).With(x => x.ConsultantId = consultantId).Build();
            var expectedResult = list.Where(x => x.ConsultantId == consultantId);
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var service = ServiceHelper();
            //Act
            var result = await service.GetAllByConsultantId(consultantId);
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetAllByConsultantantId

        #region GetGlobalGeoflexsForConsultant

        [Fact]
        public async Task GetGlobalGeoflexsForConsultant_CallsDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            int consultantId = 1;
            var list = Builder<GlobalGeoflex>.CreateListOfSize(3).Build();
            mockDal.Setup(x => x.GetAllAsync())
                .ReturnsAsync(list);
            var service = ServiceHelper();
            //Act
            await service.GetGlobalGeoflexForConsultant(consultantId);
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetGlobalGeoflexsForConsultant_ReturnsAGlobalGeoflexWithConsultantId_WhenCalled()
        {
            //Arrange
            int consultantId = 1;
            var list = Builder<GlobalGeoflex>.CreateListOfSize(3)
                .Random(1)
                .With(x => x.ConsultantId)
                .Build();
            mockDal.Setup(x => x.GetAllAsync())
                .ReturnsAsync(list);
            var service = ServiceHelper();
            //Act
            GlobalGeoflex result = await service.GetGlobalGeoflexForConsultant(consultantId);
            //Assert
            result.ConsultantId.Should().Be(consultantId);
        }

        #endregion GetGlobalGeoflexsForConsultant

        private GlobalGeoflexService ServiceHelper()
        {
            return new GlobalGeoflexService(mockDal.Object);
        }
    }
}