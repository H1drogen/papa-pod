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
    public class ConsultantModuleServiceTest
    {
        private Mock<IConsultantModuleDal> mockDal;

        public ConsultantModuleServiceTest()
        {
            mockDal = new Mock<IConsultantModuleDal>();
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
        public async Task GetAll_ReturnsIEnumerableOfConsultantModule_WhenSuccessful()
        {
            //Arrange
            var list = Builder<ConsultantModule>.CreateListOfSize(3).Build();
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
        public async Task GetAllByConsultantId_ReturnsConsultantModuleWithMatchingConsultantId_WhenSuccessful()
        {
            //Arrange
            int consultantId = 1;
            var list = Builder<ConsultantModule>.CreateListOfSize(5).Random(2).With(x => x.ConsultantId = consultantId).Build();
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var expectedResult = list.Where(x => x.ConsultantId == consultantId);
            var service = ServiceHelper();
            //Act
            var result = await service.GetAllByConsultantId(consultantId);
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetAllByConsultantId

        #region GetConsultantModulesForConsultant

        [Fact]
        public async Task GetConsultantModulesForConsultant_CallsDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var service = ServiceHelper();
            //Act
            await service.GetAll();
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetConsultantModulesForConsultant_ReturnsConsultantModuleMatchingConsultantId_WhenSuccessful()
        {
            //Arrange
            int consultantId = 1;
            var list = Builder<ConsultantModule>.CreateListOfSize(5).Random(2).With(x => x.ConsultantId = consultantId).Build();
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var expectedResult = list.First(x => x.ConsultantId == consultantId);
            var service = ServiceHelper();
            //Act
            var result = await service.GetConsultantModulesForConsultant(consultantId);
            //Assert
            result.Should().Be(expectedResult);
        }

        #endregion GetConsultantModulesForConsultant

        private ConsultantModuleService ServiceHelper()
        {
            return new ConsultantModuleService(mockDal.Object);
        }
    }
}