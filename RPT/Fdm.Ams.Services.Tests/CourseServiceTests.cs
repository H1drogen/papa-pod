using AutoMapper;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.ViewModels;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Fdm.Ams.Services.Tests
{
    public class CourseServiceTests

    {
        private Mock<ICountryDal> mockCountryDal;
        private Mock<ICourseModuleDal> mockCourseModuleDal;

        private Mock<ICourseModuleTemplateDal> mockCourseModuleTemplateDal;

        private Mock<ICourseTypeDal> mockCourseTypeDal;

        private Mock<ICourseDal> mockDal;

        private Mock<IMapper> mockMapper;

        private Mock<IOfficeDal> mockOfficeDal;
        private Mock<IProgrammeDal> mockProgrammeDal;
        private Mock<IRegionDal> mockRegionDal;

        private Mock<ICourseTemplateDal> mockTemplateDal;

        public CourseServiceTests()
        {
            mockDal = new Mock<ICourseDal>();
            mockMapper = new Mock<IMapper>();
            mockTemplateDal = new Mock<ICourseTemplateDal>();
            mockCourseModuleTemplateDal = new Mock<ICourseModuleTemplateDal>();
            mockCourseTypeDal = new Mock<ICourseTypeDal>();
            mockRegionDal = new Mock<IRegionDal>();
            mockCourseModuleDal = new Mock<ICourseModuleDal>();
            mockOfficeDal = new Mock<IOfficeDal>();
            mockProgrammeDal = new Mock<IProgrammeDal>();
            mockCountryDal = new Mock<ICountryDal>();
        }

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange

            var service = CreateCourseService();
            //Act
            await service.GetAllAsync();
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAnEnumerableOfCourseFromDal_WhenSuccessful()
        {
            //Arrange
            var list = Builder<Course>.CreateListOfSize(3).Build();
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var service = CreateCourseService();
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
            var service = CreateCourseService();
            //Act
            await service.GetByIdAsync(id);
            //Assert
            mockDal.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCourseFromDal_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var courseViewModel = Builder<CourseViewModel>.CreateNew().Build();
            var course = Builder<Course>.CreateNew().Build();
            mockDal.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(course);
            mockMapper.Setup(x => x.Map<CourseViewModel>(course)).Returns(courseViewModel);
            var service = CreateCourseService();

            //Act
            var result = await service.GetByIdAsync(id);
            //Assert
            result.Should().BeEquivalentTo(courseViewModel);
        }

        #endregion GetByIdAsync

        #region GetCourseCode

        [Fact]
        public async Task GetCourseCode_ShouldCallDalCountryGetByIdAsyncOnce_WhenCalled()
        {
            //Arrange
            int officeId = 1;
            int templateId = 1;
            int year = 22;
            int programmeId = 1;

            var (courseViewModel, courseModuleViewModel, courseTemplate, courseModule, courseType, courseModuleTemplateList, course, office, region, country, courseList, programme, courseTemplateList, officeList, programmeList) = BuildModelsViewModelsAndMockModelDalHelper();

            var service = CreateCourseService();

            //Act
            var expectedResult = await service.GetCourseCode(officeId, templateId, year, programmeId);
            //Assert
            mockCountryDal.Verify(x => x.GetByIdAsync(office.CountryId), Times.Once);
        }

        [Fact]
        public async Task GetCourseCode_ShouldCallDalCourseGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            int officeId = 1;
            int templateId = 1;
            int year = 22;
            int programmeId = 1;

            var (courseViewModel, courseModuleViewModel, courseTemplate, courseModule, courseType, courseModuleTemplateList, course, office, region, country, courseList, programme, courseTemplateList, officeList, programmeList) = BuildModelsViewModelsAndMockModelDalHelper();

            var service = CreateCourseService();
            //Act
            var expectedResult = await service.GetCourseCode(officeId, templateId, year, programmeId);
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetCourseCode_ShouldCallDalCourseTemplateGetByIdAsyncOnce_WhenCalled()
        {
            //Arrange
            int officeId = 1;
            int templateId = 1;
            int year = 22;
            int programmeId = 1;

            var (courseViewModel, courseModuleViewModel, courseTemplate, courseModule, courseType, courseModuleTemplateList, course, office, region, country, courseList, programme, courseTemplateList, officeList, programmeList) = BuildModelsViewModelsAndMockModelDalHelper();

            var service = CreateCourseService();

            //Act
            var expectedResult = await service.GetCourseCode(officeId, templateId, year, programmeId);
            //Assert
            mockTemplateDal.Verify(x => x.GetByIdAsync(courseTemplate.Id), Times.Once);
        }

        [Fact]
        public async Task GetCourseCode_ShouldCallDalCourseTypeGetByIdAsyncOnce_WhenCalled()
        {
            //Arrange
            int officeId = 1;
            int templateId = 1;
            int year = 22;
            int programmeId = 1;

            var (courseViewModel, courseModuleViewModel, courseTemplate, courseModule, courseType, courseModuleTemplateList, course, office, region, country, courseList, programme, courseTemplateList, officeList, programmeList) = BuildModelsViewModelsAndMockModelDalHelper();

            var service = CreateCourseService();
            //Act
            var expectedResult = await service.GetCourseCode(officeId, templateId, year, programmeId);
            //Assert
            mockCourseTypeDal.Verify(x => x.GetByIdAsync(courseTemplate.PathwayTypeId), Times.Once);
        }

        [Fact]
        public async Task GetCourseCode_ShouldCallDalProgrammeGetByIdAsyncOnce_WhenCalled()
        {
            //Arrange
            int officeId = 1;
            int templateId = 1;
            int year = 22;
            int programmeId = 1;

            var (courseViewModel, courseModuleViewModel, courseTemplate, courseModule, courseType, courseModuleTemplateList, course, office, region, country, courseList, programme, courseTemplateList, officeList, programmeList) = BuildModelsViewModelsAndMockModelDalHelper();

            var service = CreateCourseService();

            //Act
            var expectedResult = await service.GetCourseCode(officeId, templateId, year, programmeId);
            //Assert
            mockProgrammeDal.Verify(x => x.GetByIdAsync(programmeId), Times.Once);
        }

        [Fact]
        public async Task GetCourseCode_ShouldCallDalRegionGetByIdAsyncOnce_WhenCalled()
        {
            //Arrange
            int officeId = 1;
            int templateId = 1;
            int year = 22;
            int programmeId = 1;

            var (courseViewModel, courseModuleViewModel, courseTemplate, courseModule, courseType, courseModuleTemplateList, course, office, region, country, courseList, programme, courseTemplateList, officeList, programmeList) = BuildModelsViewModelsAndMockModelDalHelper();

            var service = CreateCourseService();
            //Act
            var expectedResult = await service.GetCourseCode(officeId, templateId, year, programmeId);
            //Assert
            mockRegionDal.Verify(x => x.GetByIdAsync(country.RegionId), Times.Once);
        }

        [Fact]
        public async Task GetCourseCode_ShouldReturnsCourseCode_WhenSuccessful()
        {
            //Arrange
            int officeId = 1;
            int templateId = 1;
            int year = 22;
            int programmeId = 1;

            var (courseViewModel, courseModuleViewModel, courseTemplate, courseModule, courseType, courseModuleTemplateList, course, office, region, country, courseList, programme, courseTemplateList, officeList, programmeList) = BuildModelsViewModelsAndMockModelDalHelper();

            var unprefixedCourseCode = $"{region.Abbreviation.ToUpper()}{office.Abbreviation}{"-"}{year}{"-"}{programme.Abbreviation}{courseType.Abbreviation}";
            var matchingCourses = courseList.Where(x => x.PathwayCode.Contains(unprefixedCourseCode));
            var pathwayCode = $"{unprefixedCourseCode}{"-"}{(matchingCourses.Count() + 1)}";

            var service = CreateCourseService();

            //Act
            var expectedResult = await service.GetCourseCode(officeId, templateId, year, programmeId);
            //Assert
            expectedResult.Should().BeEquivalentTo(pathwayCode);
        }

        #endregion GetCourseCode

        #region GetCoursesByPathwayId

        [Fact]
        public async Task GetCoursesByPathwayId_ShouldCalcourseModuleDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var courses = Builder<CourseModule>.CreateListOfSize(2).Build();
            mockCourseModuleDal.Setup(x => x.GetAllAsync()).ReturnsAsync(courses);
            var service = CreateCourseService();

            //Act
            await service.GetCoursesByPathwayId(id);

            //Assert
            mockCourseModuleDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetCoursesByPathwayId_ShouldReturnCourseViewModelWithDictionaryOfCourses_WhenCalled()
        {
            //Arrange
            int id = 1;
            var service = CreateCourseService();

            var courseModules = Builder<CourseModule>.CreateListOfSize(3).Build().Where(x => x.PathwayId == id);

            Dictionary<int, string> coursesDictionary = new();

            courseModules.ToList().ForEach(x => coursesDictionary.Add(x.Id, x.Name));

            var courseViewModel = Builder<CourseViewModel>.CreateNew()
                .With(x => x.CoursesDictionary = coursesDictionary)
                .Build();

            mockCourseModuleDal.Setup(x => x.GetAllAsync()).ReturnsAsync(courseModules);

            //Act
            var result = await service.GetCoursesByPathwayId(id);

            //Assert
            result.Should().BeEquivalentTo(courseViewModel, config =>
            config
            .Excluding(x => x.CreatedBy)
            .Excluding(x => x.EndDate)
            .Excluding(x => x.Id)
            .Excluding(x => x.MaxCapacity)
            .Excluding(x => x.OfficeId)
            .Excluding(x => x.PathwayCode)
            .Excluding(x => x.PathwayTemplateId)
            .Excluding(x => x.PathwayTypeId)
            .Excluding(x => x.ProgrammeId)
            .Excluding(x => x.RegionId)
            .Excluding(x => x.SelectedTimeZone)
            .Excluding(x => x.StartDate)
            );
        }

        #endregion GetCoursesByPathwayId

        #region GetProgrammeRegionPathwayTypes

        [Fact]
        public async Task GetProgrammeRegionPathwayTypes_ShouldCallCourseTypeDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var pathwayTypes = Builder<CourseType>.CreateListOfSize(2).Build();
            mockCourseTypeDal.Setup(x => x.GetAllAsync()).ReturnsAsync(pathwayTypes);
            var service = CreateCourseService();

            //Act
            await service.GetProgrammeRegionPathwayTypes();

            //Assert
            mockCourseTypeDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetProgrammeRegionPathwayTypes_ShouldCallProgrammeDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var programmes = Builder<Programme>.CreateListOfSize(2).Build();
            mockProgrammeDal.Setup(x => x.GetAllAsync()).ReturnsAsync(programmes);
            var service = CreateCourseService();

            //Act
            await service.GetProgrammeRegionPathwayTypes();

            //Assert
            mockProgrammeDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetProgrammeRegionPathwayTypes_ShouldCallRegionDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var regions = Builder<Region>.CreateListOfSize(2).Build();
            mockRegionDal.Setup(x => x.GetAllAsync()).ReturnsAsync(regions);
            var service = CreateCourseService();

            //Act
            await service.GetProgrammeRegionPathwayTypes();

            //Assert
            mockRegionDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetProgrammeRegionPathwayTypes_ShouldReturnCourseViewModelWithDictionaries_WhenCalled()
        {
            //Arrange
            var service = CreateCourseService();

            var regions = Builder<Region>.CreateListOfSize(3).Build();

            Dictionary<int, string> regionsDictionary = new();

            regions.ToList().ForEach(x => regionsDictionary.Add(x.Id, x.Name));

            var programmes = Builder<Programme>.CreateListOfSize(3).Build();

            Dictionary<int, string> programmesDictionary = new();

            programmes.ToList().ForEach(x => programmesDictionary.Add(x.Id, x.Name));

            var pathwayTypes = Builder<CourseType>.CreateListOfSize(3).Build();

            Dictionary<int, string> pathwayTypesDictionary = new();

            pathwayTypes.ToList().ForEach(x => pathwayTypesDictionary.Add(x.Id, x.Name));

            var courseViewModel = Builder<CourseViewModel>.CreateNew()
                .With(x => x.ProgrammesDictionary = programmesDictionary)
                .With(x => x.RegionsDictionary = regionsDictionary)
                .With(x => x.PathwayTypesDictionary = pathwayTypesDictionary)
                .Build();

            mockRegionDal.Setup(x => x.GetAllAsync()).ReturnsAsync(regions);
            mockProgrammeDal.Setup(x => x.GetAllAsync()).ReturnsAsync(programmes);
            mockCourseTypeDal.Setup(x => x.GetAllAsync()).ReturnsAsync(pathwayTypes);

            //Act
            var result = await service.GetProgrammeRegionPathwayTypes();

            //Assert
            result.Should().BeEquivalentTo(courseViewModel, config =>
            config
            .Excluding(x => x.CreatedBy)
            .Excluding(x => x.EndDate)
            .Excluding(x => x.Id)
            .Excluding(x => x.MaxCapacity)
            .Excluding(x => x.OfficeId)
            .Excluding(x => x.PathwayCode)
            .Excluding(x => x.PathwayTemplateId)
            .Excluding(x => x.PathwayTypeId)
            .Excluding(x => x.ProgrammeId)
            .Excluding(x => x.RegionId)
            .Excluding(x => x.SelectedTimeZone)
            .Excluding(x => x.StartDate)
            );
        }

        #endregion GetProgrammeRegionPathwayTypes

        #region PostAsync

        [Fact]
        public async Task PostAsync_ShouldCallsCourseModuleTemplatesDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var (courseViewModel, courseModuleViewModel, courseTemplate, courseModule, courseType, courseModuleTemplateList, course, office, region, country, courseList, programme, courseTemplateList, officeList, programmeList) = BuildModelsViewModelsAndMockModelDalHelper();

            mockMapper.Setup(x => x.Map<Course>(courseViewModel)).Returns(course);

            var service = CreateCourseService();

            //Act
            await service.PostAsync(courseViewModel);

            //Assert
            mockCourseModuleTemplateDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldCallsCourseTemplateDalGetByIdAsyncOnce_WhenCalled()
        {
            //Arrange
            var (courseViewModel, courseModuleViewModel, courseTemplate, courseModule, courseType, courseModuleTemplateList, course, office, region, country, courseList, programme, courseTemplateList, officeList, programmeList) = BuildModelsViewModelsAndMockModelDalHelper();

            mockMapper.Setup(x => x.Map<Course>(courseViewModel)).Returns(course);

            var service = CreateCourseService();

            //Act
            await service.PostAsync(courseViewModel);

            //Assert
            mockTemplateDal.Verify(x => x.GetByIdAsync(courseTemplate.Id), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldCallsDalPostAsyncOnce_WhenCalled()
        {
            //Arrange

            var (courseViewModel, courseModuleViewModel, courseTemplate, courseModule, courseType, courseModuleTemplateList, course, office, region, country, courseList, programme, courseTemplateList, officeList, programmeList) = BuildModelsViewModelsAndMockModelDalHelper();

            mockMapper.Setup(x => x.Map<Course>(courseViewModel)).Returns(course);

            var service = CreateCourseService();

            //Act
            await service.PostAsync(courseViewModel);

            //Assert
            mockDal.Verify(x => x.PostAsync(course), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldReturnsCourseViewModel_WhenSuccessful()
        {
            //Arrange

            var (courseViewModel, courseModuleViewModel, courseTemplate, courseModule, courseType, courseModuleTemplateList, course, office, region, country, courseList, programme, courseTemplateList, officeList, programmeList) = BuildModelsViewModelsAndMockModelDalHelper();

            mockMapper.Setup(x => x.Map<Course>(courseViewModel)).Returns(course);

            mockMapper.Setup(x => x.Map<CourseViewModel>(course)).Returns(courseViewModel);

            var service = CreateCourseService();

            //Act
            var expectedResult = await service.PostAsync(courseViewModel);

            //Assert
            expectedResult.Should().BeEquivalentTo(courseViewModel);
        }

        #endregion PostAsync

        #region GetOfficesProgrammesCourseTemplatesAndTimeZones

        [Fact]
        public async Task GetOfficesProgrammesCourseTemplatesAndTimeZones_ShouldCallsCourseTemplateDalAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var service = CreateCourseService();
            var courseTemplates = Builder<CourseTemplate>.CreateListOfSize(2).Build();
            mockTemplateDal.Setup(x => x.GetAllAsync()).ReturnsAsync(courseTemplates);

            //Act
            await service.GetOfficesProgrammesCourseTemplatesAndTimeZones();

            //Assert
            mockTemplateDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetOfficesProgrammesCourseTemplatesAndTimeZones_ShouldCallsProgrammeDalAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var service = CreateCourseService();
            var programmes = Builder<Programme>.CreateListOfSize(2).Build();
            mockProgrammeDal.Setup(x => x.GetAllAsync()).ReturnsAsync(programmes);

            //Act
            await service.GetOfficesProgrammesCourseTemplatesAndTimeZones();

            //Assert
            mockProgrammeDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetOfficesProgrammesCourseTemplatesAndTimeZones_ShouldCallsRegionDalAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var service = CreateCourseService();
            var offices = Builder<Office>.CreateListOfSize(2).Build();
            mockOfficeDal.Setup(x => x.GetAllAsync()).ReturnsAsync(offices);

            //Act
            await service.GetOfficesProgrammesCourseTemplatesAndTimeZones();

            //Assert
            mockOfficeDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetOfficesProgrammesCourseTemplatesAndTimeZones_ShouldReturnCourseViewModel_WhenCalled()
        {
            //Arrange
            var service = CreateCourseService();
            var (courseViewModel, courseTemplateList, officeList, programmeList) = BuildCourseViewModelWithDictionary();

            //Act
            var result = await service.GetOfficesProgrammesCourseTemplatesAndTimeZones();

            //Assert
            result.Should().BeEquivalentTo(courseViewModel, options =>
            {
                options.Excluding(cv => cv.PathwayTemplateId);
                options.Excluding(cv => cv.PathwayTypeId);
                options.Excluding(cv => cv.PathwayCode);
                options.Excluding(cv => cv.OfficeId);
                options.Excluding(cv => cv.MaxCapacity);
                options.Excluding(cv => cv.SelectedTimeZone);
                options.Excluding(cv => cv.StartDate);
                options.Excluding(cv => cv.EndDate);
                options.Excluding(cv => cv.ProgrammeId);
                options.Excluding(cv => cv.RegionId);
                options.Excluding(cv => cv.Id);
                options.Excluding(cv => cv.CreatedBy);

                return options;
            });
        }

        #endregion GetOfficesProgrammesCourseTemplatesAndTimeZones

        #region Test Helper Methods

        private (CourseViewModel viewModel,
        IEnumerable<CourseTemplate> modelCourseTemplateList, IEnumerable<Office> modelOfficeList,
        IEnumerable<Programme> modelProgrammeList)
        BuildCourseViewModelWithDictionary()
        {
            IEnumerable<CourseTemplate> courseTemplateList = Builder<CourseTemplate>.CreateListOfSize(3).Build();
            IEnumerable<Office> officeList = Builder<Office>.CreateListOfSize(3).Build();
            IEnumerable<Programme> programmeList = Builder<Programme>.CreateListOfSize(3).Build();

            Dictionary<int, string> courseTemplatesDictionary = new();
            Dictionary<int, string> officesDictionary = new();
            Dictionary<int, string> programmesDictionary = new();

            courseTemplateList.ToList().ForEach(x => courseTemplatesDictionary.Add(x.Id, x.Name));
            officeList.ToList().ForEach(x => officesDictionary.Add(x.Id, x.Name));
            programmeList.ToList().ForEach(x => programmesDictionary.Add(x.Id, x.Name));

            var timezone = Builder<TimeZonesViewModel>.CreateNew().Build();
            var courseViewModel = Builder<CourseViewModel>.CreateNew().With(x => x.CourseTemplatesDictionary = courseTemplatesDictionary)
                 .With(x => x.OfficesDictionary = officesDictionary).With(x => x.Timezones = timezone)
                 .With(x => x.ProgrammesDictionary = programmesDictionary)
                 .Build();

            mockOfficeDal.Setup(x => x.GetAllAsync()).ReturnsAsync(officeList);
            mockTemplateDal.Setup(x => x.GetAllAsync()).ReturnsAsync(courseTemplateList);
            mockProgrammeDal.Setup(x => x.GetAllAsync()).ReturnsAsync(programmeList);

            return (courseViewModel, courseTemplateList, officeList, programmeList);
        }

        private (CourseViewModel viewModel, CourseModuleViewModel ModuleViewModel,
                 CourseTemplate modelCourseTemplate, CourseModule modelCourseModule,
         CourseType modelcourseType, IEnumerable<CourseModuleTemplate> modelCourseModuleTemplateList,
         Course modelCourse, Office modelOffice,
         Region modelRegion, Country modelCountry,
         IEnumerable<Course> modelCourseList, Programme modelProgramme,
         IEnumerable<CourseTemplate> modelCourseTemplateList, IEnumerable<Office> modelOfficeList,
         IEnumerable<Programme> modelProgrammeList)
         BuildModelsViewModelsAndMockModelDalHelper()
        {
            var selectedTimeZone = "UTC";
            var courseViewModel = Builder<CourseViewModel>.CreateNew().With(x => x.SelectedTimeZone = selectedTimeZone).Build();
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            var courseTemplate = Builder<CourseTemplate>.CreateNew().Build();
            IEnumerable<CourseModuleTemplate> courseModuleTemplateList = Builder<CourseModuleTemplate>.CreateListOfSize(3).Build();

            var office = Builder<Office>.CreateNew().Build();
            var region = Builder<Region>.CreateNew().Build();
            var country = Builder<Country>.CreateNew().Build();
            var courseType = Builder<CourseType>.CreateNew().Build();
            var courseModule = Builder<CourseModule>.CreateNew().Build();
            var course = Builder<Course>.CreateNew().Build();
            var programme = Builder<Programme>.CreateNew().Build();

            IEnumerable<CourseTemplate> courseTemplateList = Builder<CourseTemplate>.CreateListOfSize(3).Build();
            IEnumerable<Office> officeList = Builder<Office>.CreateListOfSize(3).Build();
            IEnumerable<Programme> programmeList = Builder<Programme>.CreateListOfSize(3).Build();
            IEnumerable<Course> courseList = Builder<Course>.CreateListOfSize(3).Build();

            mockTemplateDal.Setup(x => x.GetByIdAsync(courseTemplate.Id)).ReturnsAsync(courseTemplate);
            mockOfficeDal.Setup(x => x.GetByIdAsync(office.Id)).ReturnsAsync(office);
            mockCountryDal.Setup(x => x.GetByIdAsync(office.CountryId)).ReturnsAsync(country);
            mockRegionDal.Setup(x => x.GetByIdAsync(region.Id)).ReturnsAsync(region);
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(courseList);
            mockProgrammeDal.Setup(x => x.GetByIdAsync(programme.Id)).ReturnsAsync(programme);
            mockCourseTypeDal.Setup(x => x.GetByIdAsync(courseTemplate.PathwayTypeId)).ReturnsAsync(courseType);
            mockCourseModuleTemplateDal.Setup(x => x.GetAllAsync()).ReturnsAsync(courseModuleTemplateList);
            mockDal.Setup(x => x.PostAsync(course)).ReturnsAsync(course);
            mockTemplateDal.Setup(x => x.GetAllAsync()).ReturnsAsync(courseTemplateList);
            mockOfficeDal.Setup(x => x.GetAllAsync()).ReturnsAsync(officeList);
            mockProgrammeDal.Setup(x => x.GetAllAsync()).ReturnsAsync(programmeList);

            return (courseViewModel, courseModuleViewModel, courseTemplate, courseModule, courseType, courseModuleTemplateList, course, office, region, country, courseList, programme, courseTemplateList, officeList, programmeList);
        }

        #endregion Test Helper Methods

        private CourseService CreateCourseService()
        {
            return new CourseService(mockDal.Object, mockTemplateDal.Object, mockCourseModuleTemplateDal.Object, mockMapper.Object, mockCourseTypeDal.Object, mockOfficeDal.Object, mockCourseModuleDal.Object, mockProgrammeDal.Object, mockRegionDal.Object, mockCountryDal.Object);
        }
    }
}