using Fdm.Ams.Web.Controllers;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Fdm.Ams.Web.Tests
{
    public class HomeControllerTest
    {
        private readonly Mock<ILogger<HomeController>> logger;

        public HomeControllerTest()
        {
            logger = new Mock<ILogger<HomeController>>();
        }

        public HomeController ControllerHelper()
        {
            return new HomeController(logger.Object);
        }

        [Fact]
        public void Index_ShouldNotReturn_Null()
        {
            // Arrange
            var controller = ControllerHelper();

            // Act
            var result = controller.Index();

            // Assert
            result.Should().NotBeNull();
        }
    }
}