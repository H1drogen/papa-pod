using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Fdm.Ams.Services.Tests
{
    public class VenueServiceTests
    {
        private Mock<IOfficeDal> mockOfficeDal;
        private Mock<IVenueDal> mockVenueDal;

        public VenueServiceTests()
        {
            mockVenueDal = new Mock<IVenueDal>();
            mockOfficeDal = new Mock<IOfficeDal>();
        }

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var service = CreateVenueService();
            //Act
            await service.GetAllAsync();
            //Assert
            mockVenueDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAListOfVenue_WhenCalled()
        {
            //Arrange
            var list = Builder<Venue>.CreateListOfSize(3).Build();
            mockVenueDal.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var service = CreateVenueService();
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
            var service = CreateVenueService();
            //Act
            await service.GetByIdAsync(id);
            //Assert
            mockVenueDal.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsVenueFromResponseMessage_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var venue = Builder<Venue>.CreateNew().Build();
            mockVenueDal.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(venue);
            var service = CreateVenueService();
            //Act
            var result = await service.GetByIdAsync(id);
            //Assert
            result.Should().BeEquivalentTo(venue);
        }

        #endregion GetByIdAsync

        #region RetrieveOfficeById

        [Fact]
        public async Task RetrieveOfficeById_CallsOfficeDalGetByIdAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var service = CreateVenueService();

            //Act
            await service.RetrieveOfficeById(id);

            //Assert
            mockOfficeDal.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task RetrieveOfficeById_ReturnsOffice_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            Office office = Builder<Office>.CreateNew().Build();
            mockOfficeDal.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(office);
            var service = CreateVenueService();

            //Act
            var result = await service.RetrieveOfficeById(id);

            //Assert
            result.Should().Be(office);
        }

        #endregion RetrieveOfficeById

        private VenueService CreateVenueService()
        {
            return new VenueService(mockVenueDal.Object, mockOfficeDal.Object);
        }
    }
}