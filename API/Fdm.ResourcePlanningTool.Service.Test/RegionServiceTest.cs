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
    public class RegionServiceTest : GenericServiceTest<Region, RegionDto,
        PostRegionDto, IRegionRepository>
    {
        private Mock<IMapper> mockMapper;
        private Mock<IRegionRepository> mockRepo;

        public RegionService RegionServiceTestHelper()
        {
            return new RegionService(mockRepo.Object, mockMapper.Object);
        }

        [SetUp]
        public void SetUp()
        {
            mockRepo = new Mock<IRegionRepository>();
            mockMapper = new Mock<IMapper>();
        }

        protected override GenericService<Region, RegionDto, PostRegionDto, IRegionRepository> GenericServiceTestHelper
                                       (Mock<IMapper> mockMapper, Mock<IRegionRepository> mockRepo)
        {
            return new RegionService(mockRepo.Object, mockMapper.Object);
        }

        /*

                #region Test Helper Methods

                private void SetupUserRegionRepoGetEntitiesWithExpressionFunc(IList<UserRegionPermission> userRegionPermissions)
                {
                    mockUserRegionRepo.Setup(mock => mock.GetEntities(It.IsAny<Expression<Func<UserRegionPermission, bool>>>())).Returns(userRegionPermissions);
                }

                #endregion Test Helper Methods

                #region GetAll

                [Test]
                public async Task GetAll_ReturnIEnumerableOfRegionDtosFromMapper_WhenCalled()
                {
                    //Arrange
                    var service = RegionServiceTestHelper();
                    var pocoList = Builder<Region>.CreateListOfSize(3).Build();
                    var dtoList = Builder<RegionDto>.CreateListOfSize(3).Build();
                    mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(pocoList);
                    mockMapper.Setup(x => x.Map<IEnumerable<RegionDto>>(pocoList)).Returns(dtoList);
                    //Act
                    var result = await service.GetAll();
                    //Assert
                    result.Should().BeSameAs(dtoList);
                }

                [Test]
                public async Task GetAll_ShouldCall_GetAll_WhenCalled()
                {
                    //Arrange
                    var service = RegionServiceTestHelper();
                    //Act
                    await service.GetAll();
                    //Assert
                    mockRepo.Verify(x => x.GetAllAsync(), Times.Once);
                }

                #endregion GetAll

                #region GetAllActive

                [Test]
                public async Task GetAllActive_ReturnsAListOfRegionsDtosFromMapper_WhenCalled()
                {
                    //Arrange
                    var service = RegionServiceTestHelper();
                    var pocoList = Builder<Region>.CreateListOfSize(3).Build();
                    var dtoList = Builder<RegionDto>.CreateListOfSize(3).Build();
                    Expression<Func<Region, bool>> expression = w => w.IsActive;
                    mockRepo.Setup(x => x.GetEntitiesAsync(It.Is<Expression<Func<Region, bool>>>(actualExpression =>
                        Lambda.Eq(actualExpression, expression)))).ReturnsAsync(pocoList);
                    mockMapper.Setup(x => x.Map<IEnumerable<RegionDto>>(pocoList)).Returns(dtoList);
                    //Act
                    var result = await service.GetAllActive();
                    //Assert
                    result.Should().BeEquivalentTo(dtoList);
                }

                [Test]
                public async Task GetAllActive_ShouldCallGetEntitiesWithActiveFilter_WhenCalled()
                {
                    //Arrange
                    var service = RegionServiceTestHelper();
                    Expression<Func<Region, bool>> expectedExpression = w => w.IsActive;
                    //Act
                    await service.GetAllActive();
                    //Assert
                    mockRepo.Verify(x => x.GetEntitiesAsync(It.Is<Expression<Func<Region, bool>>>(actualExpression =>
                        Lambda.Eq(actualExpression, expectedExpression))), Times.Once);
                }

                #endregion GetAllActive

                #region GetRegionsForUserInt

                [Test]
                public async Task GetRegionsForUserInt_ReturnsAnIEnumerableOfRegionDtosFromMapper_WhenCalled()
                {
                    //Arrange
                    int id = 1;
                    var pocoList = Builder<UserRegionPermission>.CreateListOfSize(3).Build();
                    var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Region, RegionDto>().ReverseMap());
                    IMapper mapper = new Mapper(configuration);
                    var service = new RegionService(mockRepo.Object, mockUserRegionRepo.Object, mapper);
                    var dtoList = mapper.Map<IEnumerable<RegionDto>>(pocoList.Select(x => x.Region).Distinct());

                    Expression<Func<UserRegionPermission, bool>> expectedExpression = ur => ur.User.Id == id && !ur.RemovalDate.HasValue;
                    mockUserRegionRepo.Setup(x => x.GetEntities(It.Is<Expression<Func<UserRegionPermission, bool>>>(actualExpression =>
                        Lambda.Eq(actualExpression, expectedExpression)))).Returns(pocoList);
                    //Act
                    var result = await service.GetRegionsForUser(id);

                    //Assert
                    result.Should().BeEquivalentTo(dtoList);
                }

                [Test]
                public async Task GetRegionsForUserInt_ShouldCallUserRegionRepositoryGetEntities_WithIdPassedIn()
                {
                    //Arrange
                    var userId = 1;
                    var service = RegionServiceTestHelper();
                    Expression<Func<UserRegionPermission, bool>> expectedExpression = userRegionPermission => userRegionPermission.User.Id == userId && !userRegionPermission.RemovalDate.HasValue;

                    //Act
                    var result = await service.GetRegionsForUser(userId);

                    //Assert
                    mockUserRegionRepo.Verify(x => x.GetEntities(It.Is<Expression<Func<UserRegionPermission, bool>>>(actualExpression => Lambda.Eq(actualExpression, expectedExpression))));
                }

                #endregion GetRegionsForUserInt

                #region GetActiveRegionsForUser

                [Test]
                public async Task GetActiveRegionsForUser_ShouldCallUserRegionPermissionRepo_WhenCalled()
                {
                    //Arrange
                    var service = RegionServiceTestHelper();
                    var userObjectId = GetRandom.String(10);
                    Expression<Func<UserRegionPermission, bool>> expectedExpression = ur => ur.User.ObjectId == userObjectId && !ur.RemovalDate.HasValue
                        && ur.Region.IsActive;
                    //Act
                    await service.GetActiveRegionsForUser(userObjectId);
                    //Assert
                    mockUserRegionRepo.Verify(x => x.GetEntities(It.Is<Expression<Func<UserRegionPermission, bool>>>(actualExpression => Lambda.Eq(actualExpression, expectedExpression))));
                }

                [Test]
                public async Task GetActiveRegionsForUser_ShouldReturnMappedResultOfListOfActiveRegionsThatUserHasAccessTo_WhenCalled()
                {
                    //Arrange
                    var configuration = new MapperConfiguration(cfg =>
                        cfg.CreateMap<Region, RegionDto>());
                    IMapper mapper = new Mapper(configuration);
                    var service = new RegionService(mockRepo.Object, mockUserRegionRepo.Object, mapper);

                    var userObjectId = GetRandom.String(10);
                    IList<UserRegionPermission> userRegionPermissions = Builder<UserRegionPermission>.CreateListOfSize(3).Build();
                    var activeRegions = userRegionPermissions.Select(x => x.Region).Distinct();
                    Expression<Func<UserRegionPermission, bool>> expectedExpression = ur => ur.User.ObjectId == userObjectId && !ur.RemovalDate.HasValue
                        && ur.Region.IsActive;

                    SetupUserRegionRepoGetEntitiesWithExpressionFunc(userRegionPermissions);

                    //Act
                    var result = await service.GetActiveRegionsForUser(userObjectId);

                    //Assert
                    result.Should().BeEquivalentTo(activeRegions);
                }

                #endregion GetActiveRegionsForUser

        */
    }
}