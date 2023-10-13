using System.Linq;
using System.Threading.Tasks;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using Xunit;

namespace Fdm.Ams.Services.Tests
{
    public class UserLanguageServiceTest
    {
        private Mock<IUserLanguageDal> mockDal;

        public UserLanguageServiceTest()
        {
            mockDal = new Mock<IUserLanguageDal>();
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
        public async Task GetAll_ReturnsEnumerableOfUserLanguage_WhenSuccessful()
        {
            //Arrange
            var list = Builder<UserLanguage>.CreateListOfSize(3).Build();
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
            var service = ServiceHelper();
            //Act
            await service.GetAllByConsultantId(consultantId);
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllByConsultantId_ReturnsUserLanguageWithMatchingConsultantId_WhenSuccessful()
        {
            //Arrange
            int consultantId = 1;
            var list = Builder<UserLanguage>.CreateListOfSize(5).Random(2).With(x => x.ConsultantId = consultantId).Build();
            var expectedResult = list.Where(x => x.ConsultantId == consultantId);
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var service = ServiceHelper();
            //Act
            var result = await service.GetAllByConsultantId(consultantId);
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetAllByConsultantantId

        #region GetUserLanguagesForConsultant

        [Fact]
        public async Task GetUserLanguagesForConsultant_CallsGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            int consultantId = 1;
            var list = Builder<UserLanguage>.CreateListOfSize(3).Build();
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var service = ServiceHelper();
            //Act
            await service.GetUserLanguageForConsultant(consultantId);
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetUserLanguagesForConsultant_ReturnsAUserLanguageWithPathwayTemplateId_WhenCalled()
        {
            //Arrange
            int consultantId = 1;
            var list = Builder<UserLanguage>.CreateListOfSize(3)
                .Random(1)
                .With(x => x.ConsultantId)
                .Build();
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var service = ServiceHelper();
            //Act
            var result = await service.GetUserLanguageForConsultant(consultantId);
            //Assert
            result.ConsultantId.Should().Be(consultantId);
        }

        #endregion GetUserLanguagesForConsultant

        private UserLanguageService ServiceHelper()
        {
            return new UserLanguageService(mockDal.Object);
        }
    }
}