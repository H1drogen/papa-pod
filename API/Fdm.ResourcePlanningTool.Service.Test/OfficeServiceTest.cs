using AutoMapper;
using Fdm.Common.Service;
using Fdm.Common.Tests.Services;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services;
using Moq;
using NUnit.Framework;

namespace Fdm.ResourcePlainningTool.Services.Tests
{
    /// <summary>
    /// Class for all the Test for the Office Service tests.
    /// First level is for any custom methods specific to the OfficeService and NOT inherited from the GenericServiceTest
    /// </summary>

    public class OfficeServiceTest

    {
        public class OfficeGenericServiceTest : GenericServiceTest<Office, OfficeDto, PostOfficeDto, IOfficeRepository>

        {
            private Mock<ICountryRepository> mockCountryRepo;
            private Mock<IMapper> mockMapper;
            private Mock<IOfficeRepository> mockRepo;

            public OfficeService OfficeServiceTestHelper()
            {
                return new OfficeService(mockMapper.Object, mockRepo.Object, mockCountryRepo.Object);
            }

            [SetUp]
            public void SetUp()
            {
                mockRepo = new Mock<IOfficeRepository>();

                mockCountryRepo = new Mock<ICountryRepository>();

                mockMapper = new Mock<IMapper>();
            }

            protected override GenericService<Office, OfficeDto, PostOfficeDto, IOfficeRepository> GenericServiceTestHelper
                                       (Mock<IMapper> mockMapper, Mock<IOfficeRepository> mockRepo)
            {
                return new OfficeService(mockMapper.Object, mockRepo.Object, mockCountryRepo.Object);
            }

            /*

                        #region Test Helper methods

                        private IList<OfficeDto> CreateListOfOfficeDto()
                        {
                            return Builder<OfficeDto>.CreateListOfSize(3).Build();
                        }

                        private IList<Office> CreateListOfOfficePoco()
                        {
                            return Builder<Office>.CreateListOfSize(3).Build();
                        }

                        private OfficeDto CreateOfficeDto()
                        {
                            return Builder<OfficeDto>.CreateNew().Build();
                        }

                        private Office CreateOfficePoco()
                        {
                            return Builder<Office>.CreateNew().Build();
                        }

                        private PostOfficeDto CreatePostOfficeDto()
                        {
                            return Builder<PostOfficeDto>.CreateNew().Build();
                        }

                        #endregion Test Helper methods

                        #region Create

                        [Test]
                        public async Task OfficeService_Create_ShouldCall_CountryRepositoryGetByIdAsyncOnce_WhenCalled()
                        {
                            //Arrange
                            var service = OfficeServiceTestHelper();
                            var postOfficeDto = CreatePostOfficeDto();
                            var officePoco = CreateOfficePoco();
                            mockMapper.Setup(x => x.Map<Office>(postOfficeDto)).Returns(officePoco);

                            //Act
                            await service.Create(postOfficeDto);
                            //Assert
                            mockCountryRepo.Verify(x => x.GetByIdAsync(postOfficeDto.CountryId), Times.Once);
                        }

                        [Test]
                        public async Task OfficeService_Create_ShouldCall_OfficeRepositoryInsertAsync_WhenCalled()
                        {
                            //Arrange
                            var service = OfficeServiceTestHelper();
                            var postOfficeDto = CreatePostOfficeDto();
                            var officePoco = CreateOfficePoco();

                            mockMapper.Setup(x => x.Map<Office>(postOfficeDto)).Returns(officePoco);
                            //Act
                            await service.Create(postOfficeDto);
                            //Assert
                            mockRepo.Verify(m => m.InsertAsync(officePoco), Times.Once);
                        }

                        [Test]
                        public async Task OfficeService_Create_ShouldReturnResultFromMapper_WhenCalled()
                        {
                            //Arrange
                            var service = OfficeServiceTestHelper();
                            var postOfficeDto = CreatePostOfficeDto();
                            var mapperResult = CreateOfficeDto();
                            var officePoco = CreateOfficePoco();
                            var country = Builder<Country>.CreateNew().With(x => x.Id = postOfficeDto.CountryId).Build();
                            officePoco.Country = country;
                            mockMapper.Setup(x => x.Map<Office>(postOfficeDto)).Returns(officePoco);
                            mockMapper.Setup(x => x.Map<OfficeDto>(officePoco)).Returns(mapperResult);
                            mockCountryRepo.Setup(x => x.GetByIdAsync(postOfficeDto.CountryId)).ReturnsAsync(country);
                            mockRepo.Setup(x => x.InsertAsync(officePoco)).ReturnsAsync(officePoco);
                            //Act
                            var result = await service.Create(postOfficeDto);
                            //Assert
                            result.Should().BeSameAs(mapperResult);
                        }

                        #endregion Create

                        #region GetActiveOfficesForUser

                        [Test]
                        public async Task GetActiveOfficesForUser_ReturnsMappedResultOfListOfActiveOfficesThatUserHasRegionAccessTo_WhenCalled()
                        {
                            //Arrange
                            var userObjectId = "objectId";
                            var service = OfficeServiceTestHelper();
                            var userRegion = Builder<Region>.CreateNew().Build();
                            var randomRegion = Builder<Region>.CreateNew().Build();
                            var userRegionPermissions = Builder<UserRegionPermission>.CreateListOfSize(3).TheFirst(1).With(userRegionPermission => userRegionPermission.Region = userRegion).Build();

                            var countryWithAllowedUserRegion = Builder<Country>.CreateNew().With(country => country.Region = userRegion).Build();
                            var countryWithNonAllowedUserRegion = Builder<Country>.CreateNew().With(country => country.Region = randomRegion).Build();

                            var officesInDb = Builder<Office>
                                .CreateListOfSize(5)
                                .TheFirst(2).With(office => office.Country = countryWithNonAllowedUserRegion)
                                .TheRest().With(office => office.Country = countryWithAllowedUserRegion)
                                .Build();

                            var mapperResult = Builder<OfficeDto>.CreateListOfSize(4).Build().ToList();

                            mockRepo.Setup(mock => mock.GetAllAsync()).ReturnsAsync(officesInDb);
                            mockUserRegionRepo.Setup(mock => mock.GetEntities(It.IsAny<Expression<Func<UserRegionPermission, bool>>>())).Returns(userRegionPermissions);
                            mockMapper.Setup(x => x.Map<IEnumerable<OfficeDto>>(It.Is<IEnumerable<Office>>(list => list.All(x => x.Country.Region == userRegion)))).Returns(mapperResult);
                            //Act
                            var result = await service.GetActiveOfficesForUser(userObjectId);

                            //Assert
                            result.Should().BeSameAs(mapperResult);
                        }

                        #endregion GetActiveOfficesForUser

                        #region GetAllActive

                        [Test]
                        public async Task OfficeService_GetAllActive_ReturnResultsFromMapper_WhenCalled()
                        {
                            //Arrange
                            Expression<Func<Office, bool>> expectedExpression = x => x.IsActive;
                            var pocoList = CreateListOfOfficePoco();
                            var dtoList = CreateListOfOfficeDto();
                            mockMapper.Setup(x => x.Map<IEnumerable<OfficeDto>>(pocoList)).Returns(dtoList);
                            mockRepo.Setup(m => m.GetEntitiesAsync(It.Is<Expression<Func<Office, bool>>>(actualExpression =>
                                Lambda.Eq(actualExpression, expectedExpression)))).ReturnsAsync(pocoList);
                            var service = OfficeServiceTestHelper();
                            //Act
                            var result = await service.GetAllActive();
                            //Assert
                            result.Should().BeSameAs(dtoList);
                        }

                        [Test]
                        public async Task OfficeService_GetAllActive_ShouldCallGetEntitiesAsync_WhenCalled()
                        {
                            //Arrange
                            Expression<Func<Office, bool>> expectedExpression = x => x.IsActive;
                            var service = OfficeServiceTestHelper();
                            //Act
                            await service.GetAllActive();
                            //Assert
                            mockRepo.Verify(x => x.GetEntitiesAsync(It.Is<Expression<Func<Office, bool>>>(actualExpression =>
                            Lambda.Eq(actualExpression, expectedExpression))), Times.Once);
                        }

                        [Test]
                        public async Task OfficeService_GetAllActive_ShouldReturn_ActiveOfficeResultFromTheMapper_WhenCalled()
                        {
                            //Arrange
                            Expression<Func<Office, bool>> expectedExpression = x => x.IsActive;
                            var service = OfficeServiceTestHelper();
                            var dtoList = CreateListOfOfficeDto().Where(z => z.IsActive);
                            var pocoList = CreateListOfOfficePoco().Where(z => z.IsActive);
                            mockMapper.Setup(x => x.Map<IEnumerable<OfficeDto>>(pocoList)).Returns(dtoList);
                            mockRepo.Setup(m => m.GetEntitiesAsync(It.Is<Expression<Func<Office, bool>>>(actualExpression =>
                                Lambda.Eq(actualExpression, expectedExpression)))).ReturnsAsync(pocoList);
                            //Act
                            var returned = await service.GetAllActive();
                            //Assert
                            returned.Should().BeEquivalentTo(dtoList.Where(x => x.IsActive));
                        }

                        #endregion GetAllActive

                        #region GetAll

                        [Test]
                        public async Task OfficeService_GetAll_ShouldCall_GetAllASync_WhenCalled()
                        {
                            //Arrange
                            var service = OfficeServiceTestHelper();
                            //Act
                            var returned = await service.GetAll();
                            //Assert
                            mockRepo.Verify(m => m.GetAllAsync(), Times.Once);
                        }

                        [Test]
                        public async Task OfficeService_GetAll_ShouldCall_MapperOnce_WhenCalled()
                        {
                            //Arrange
                            var service = OfficeServiceTestHelper();
                            var dtoList = CreateListOfOfficeDto();
                            var pocoList = CreateListOfOfficePoco();

                            mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(pocoList);
                            mockMapper.Setup(x => x.Map<IEnumerable<OfficeDto>>(pocoList)).Returns(dtoList);
                            //Act
                            await service.GetAll();
                            //Assert
                            mockMapper.Verify(x => x.Map<IEnumerable<OfficeDto>>(pocoList), Times.Once);
                        }

                        [Test]
                        public async Task OfficeService_GetAll_ShouldReturn_AnOrderedResultFromTheMapper_WhenCalled()
                        {
                            //Arrange
                            var service = OfficeServiceTestHelper();
                            var dtoList = CreateListOfOfficeDto();
                            var pocoList = CreateListOfOfficePoco();
                            mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(pocoList);
                            mockMapper.Setup(x => x.Map<IEnumerable<OfficeDto>>(pocoList)).Returns(dtoList);
                            //Act
                            var returned = await service.GetAll();
                            //Assert
                            returned.Should().BeEquivalentTo(dtoList.OrderBy(x => x.Name));
                        }

                        #endregion GetAll

            */
        }
    }
}