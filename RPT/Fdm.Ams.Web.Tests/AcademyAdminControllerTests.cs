using Fdm.Ams.Web.Controllers;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Fdm.Ams.Web.Tests
{
    public class AcademyAdminControllerTests
    {
        private Mock<ILogger<AcademyAdminController>> mockLogger;

        public AcademyAdminControllerTests()
        {
            mockLogger = new Mock<ILogger<AcademyAdminController>>();
        }

        private AcademyAdminController CreateController()
        {
            return new AcademyAdminController(mockLogger.Object);
        }

        #region Index

        [Fact]
        public void Index_ReturnsAValueThatIsNotNull_WhenCalled()
        {
            //Arrange
            var controller = CreateController();
            //Act
            var result = controller.Index();
            //Assert
            result.Should().NotBeNull();
        }

        #endregion Index
    }
}