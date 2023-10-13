using AutoMapper;
using Fdm.Common.Service;
using Fdm.Common.Tests.Services;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services;
using Moq;

namespace Fdm.ResourcePlainningTool.Services.Tests
{
    /// <summary>
    /// Class for all the Test for the Country Service tests.
    /// First level is for any custom methods specific to the CountryService and NOT inherited from the GenericServiceTest
    /// </summary>

    public class CountryServiceTest : GenericServiceTest<Country, CountryDto, PostCountryDto, ICountryRepository>
    {
        protected override GenericService<Country, CountryDto, PostCountryDto, ICountryRepository> GenericServiceTestHelper
        (Mock<IMapper> mockMapper, Mock<ICountryRepository> mockRepo)
        {
            return new CountryService(mockMapper.Object, mockRepo.Object);
        }

        /*
                [Test]
                public async Task GetActiveCountriesForUser_CallsCountryRepositoryGetAllAsyncOnce_WhenCalled()
                {
                    //Arrange
                    var service = CountryServiceTestHelper();
                    var userObjectId = GetRandom.String(10);
                    //Act
                    await service.GetActiveCountriesForUser(userObjectId);
                    //Assert
                    mockRepo.Verify(x => x.GetAllAsync(), Times.Once);
                }

                [Test]
                public async Task GetActiveCountriesForUser_CallsUserRegionPermissionRepositoryGetEntitiesOnce_WhenCalled()
                {
                    //Arrange
                    var service = CountryServiceTestHelper();
                    var userObjectId = GetRandom.String(10);
                    Expression<Func<UserRegionPermission, bool>> expectedExpression = ur => ur.User.ObjectId == userObjectId && !ur.RemovalDate.HasValue;
                    //Act
                    await service.GetActiveCountriesForUser(userObjectId);
                    //Assert
                    mockUserRegionRepo.Verify(x => x.GetEntities(It.Is<Expression<Func<UserRegionPermission, bool>>>(actualExpression =>
                        Lambda.Eq(expectedExpression, actualExpression))), Times.Once);
                }

                public async Task GetActiveCountriesForUser_ShouldReturnsListOfActiveCountriesThatUserHasRegionAccessTo_WhenCalled()
                {
                    //Arrange
                    var mapperConfig = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Region, RegionDto>().ReverseMap();
                        cfg.CreateMap<Country, CountryDto>()
                        .ForMember(dto => dto.RegionName, opts => opts.MapFrom(src => src.Region.Name)).ReverseMap();
                    }
                    );
                    IMapper mapper = new Mapper(mapperConfig);
                    var service = CountryServiceTestHelper(); //new CountryService(mapper, mockRepo.Object, mockUserRegionRepo.Object);

                    var userObjectId = GetRandom.String(10);
                    var userRegionPermissionList = Builder<UserRegionPermission>.CreateListOfSize(3)
                        .All().With(x => x.Region = Builder<Region>.CreateNew().Build()).Build();

                    var regions = userRegionPermissionList.Select(ur => ur.Region).Distinct();
                    Expression<Func<UserRegionPermission, bool>> userRegionExpression = ur => ur.User.ObjectId == userObjectId && !ur.RemovalDate.HasValue;
                    mockUserRegionRepo.Setup(x => x.GetEntities(It.Is<Expression<Func<UserRegionPermission, bool>>>(actualExpression =>
                        Lambda.Eq(userRegionExpression, actualExpression)))).Returns(userRegionPermissionList);

                    var countryList = Builder<Country>.CreateListOfSize(5).Random(2).With(x => x.IsActive = true)
                        .TheRest().With(x => x.IsActive = false)
                        .Random(3).With(x => x.Region = regions.First()).Build();
                    mockRepo.Setup(x => x.GetAllAsync().Result).Returns(countryList);

                    var expectedResult = mapper.Map<IEnumerable<CountryDto>>(countryList.Where(country => country.IsActive && regions.Contains(country.Region)));
                    //Act
                    var result = await service.GetActiveCountriesForUser(userObjectId);
                    //Assert
                    result.Should().BeEquivalentTo(expectedResult);
                }

                [Test]
                public async Task GetAll_CallsCountryRepositoryGetAllAsync_WhenCalled()
                {
                    //Arrange
                    var service = CountryServiceTestHelper();
                    //Act
                    await service.GetAll();
                    //Assert
                    mockRepo.Verify(m => m.GetAllAsync(), Times.Once);
                }

                [Test]
                public async Task GetAll_ReturnsListOfCountriesDto_WhenCalled()
                {
                    //Arrange
                    var service = CountryServiceTestHelper();
                    var pocoList = Builder<Country>.CreateListOfSize(3).Build();
                    var dtoList = Builder<CountryDto>.CreateListOfSize(3).Build();
                    mockRepo.Setup(x => x.GetAllAsync().Result).Returns(pocoList);
                    mockMapper.Setup(x => x.Map<IEnumerable<CountryDto>>(pocoList)).Returns(dtoList);

                    //Act
                    var result = await service.GetAll();
                    //Assert

                    result.Should().BeSameAs(dtoList);
                }

                [Test]
                public async Task GetAllActive_CallsCountryRepositoryGetEntitiesAsync_WhenCalled()
                {
                    //Arrange
                    Expression<Func<Country, bool>> expectedExpression = x => x.IsActive;
                    var service = CountryServiceTestHelper();
                    //Act
                    await service.GetAllActive();
                    //Assert
                    mockRepo.Verify(m => m.GetEntitiesAsync(It.Is<Expression<Func<Country, bool>>>(actualExpression =>
                        Lambda.Eq(actualExpression, expectedExpression))), Times.Once);
                }

                [Test]
                public async Task GetAllActive_ReturnsResultFromMapper_WhenCalled()
                {
                    //Arrange
                    Expression<Func<Country, bool>> expectedExpression = x => x.IsActive;
                    var pocoList = Builder<Country>.CreateListOfSize(3).Build().Where(x => x.IsActive);
                    var dtoList = Builder<CountryDto>.CreateListOfSize(3).Build().Where(x => x.IsActive);
                    mockMapper.Setup(x => x.Map<IEnumerable<CountryDto>>(pocoList)).Returns(dtoList);
                    mockRepo.Setup(m => m.GetEntitiesAsync(It.Is<Expression<Func<Country, bool>>>(actualExpression =>
                        Lambda.Eq(actualExpression, expectedExpression)))).ReturnsAsync(pocoList);
                    var service = CountryServiceTestHelper();
                    //Act
                    var result = await service.GetAllActive();
                    //Assert
                    result.Should().BeSameAs(dtoList);
                }

                [Test]
                public void GetCountriesDictionary_ReturnsDictionary_WhenCalled()
                {
                    //Arrange
                    var service = CountryServiceTestHelper();
                    IList<CountryDto> countryDtos = CreateListOfCountryDtoData();
                    //Act
                    var result = service.GetCountriesDictionary(countryDtos, "Unknown");
                    //Assert
                    result.Should().BeOfType<Dictionary<string, CountryDto>>();
                }

                [Test]
                public void GetCountriesDictionary_ShouldReturnsDictionaryWithRelevantCountryKey_WhenPassedInCountryListWithCountrysIn()
                {
                    //Arrange
                    var service = CountryServiceTestHelper();
                    var leeds = "Leeds";
                    IList<CountryDto> countryDtos = CreateListOfCountryDtoDataWithNameLeedsAndLondon(leeds, "London");
                    //Act
                    var result = service.GetCountriesDictionary(countryDtos, "Unknown");
                    //Assert
                    result.Keys.Should().Contain(leeds);
                }

                [Test]
                public void GetCountriesDictionary_ShouldReturnsDictionaryWithRelevantCountryValue_WhenPassedInCountryListWithCountrysIn()
                {
                    //Arrange
                    var service = CountryServiceTestHelper();
                    var leeds = "Leeds";
                    CountryDto expectedCountryValue = CreateCountryDtoDataWithLeedsName(leeds);
                    IList<CountryDto> countryDtos = CreateListOfCountryDtoDataWithNameLeedsAndLondon(leeds, "London");
                    //Act
                    var result = service.GetCountriesDictionary(countryDtos, "Unknown");
                    //Assert
                    result.Values.Should().ContainEquivalentOf(expectedCountryValue);
                }

                [Test]
                public void GetCountriesDictionary_ShouldReturnsDictionaryWithRelevantUnknownKey_WhenPassedInStringUnknown()
                {
                    //Arrange
                    var service = CountryServiceTestHelper();
                    var unknownKey = "Unknown";
                    IList<CountryDto> countryDtos = CreateListOfCountryDtoData();
                    //Act
                    var result = service.GetCountriesDictionary(countryDtos, unknownKey);
                    //Assert
                    result.Keys.Should().Contain(unknownKey);
                }

                [Test]
                public void GetCountriesDictionary_ShouldReturnsDictionaryWithRelevantUnknownNameValue_WhenPassedInStringUnknown()
                {
                    //Arrange
                    var service = CountryServiceTestHelper();
                    var unknownKey = "Unknown";
                    IList<CountryDto> countryDtos = CreateListOfCountryDtoData();
                    //Act
                    var result = service.GetCountriesDictionary(countryDtos, unknownKey);
                    var unknownCountry = result[unknownKey];

                    //Assert
                    unknownCountry.Name.Should().Be(unknownKey);
                }

                [Test]
                public void GetCountriesDictionary_ShouldReturnsDictionaryWithRelevantUnknownRegionNameValue_WhenPassedInStringUnknown()
                {
                    //Arrange
                    var service = CountryServiceTestHelper();
                    var unknownKey = "Unknown";
                    IList<CountryDto> countryDtos = CreateListOfCountryDtoData();
                    //Act
                    var result = service.GetCountriesDictionary(countryDtos, unknownKey);
                    var unknownCountry = result[unknownKey];

                    //Assert
                    unknownCountry.RegionName.Should().Be(unknownKey);
                }

                [SetUp]
                public void SetUp()
                {
                    mockRepo = new Mock<ICountryRepository>();

                    mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Country>());
                    mockRepo.Setup(x => x.GetEntitiesAsync(It.IsAny<Expression<Func<Country, bool>>>())).ReturnsAsync(new List<Country>());

                    mockMapper = new Mock<IMapper>();
                    mockUserRegionRepo = new Mock<IDmsGenericRepository<UserRegionPermission>>();
                }
*/

        /*   #region Test Helper Methods

           private static CountryDto CreateCountryDtoData()
           {
               return Builder<CountryDto>.CreateNew().Build();
           }

           private static CountryDto CreateCountryDtoDataWithLeedsName(string leeds)
           {
               return Builder<CountryDto>.CreateNew().With(x => x.Name = leeds).Build();
           }

           private static IList<CountryDto> CreateListOfCountryDtoData()
           {
               return Builder<CountryDto>.CreateListOfSize(5).Build();
           }

           private static IList<CountryDto> CreateListOfCountryDtoDataWithNameLeedsAndLondon(string leeds, string london)
           {
               return Builder<CountryDto>.CreateListOfSize(2)
                               .All()
                               .TheFirst(1)
                               .With(x => x.Name = leeds)
                               .TheLast(1)
                               .With(x => x.Name = london)
                               .Build();
           }

           #endregion Test Helper Methods

   */
    }
}