using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.ViewModels;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Fdm.Ams.Services.Tests
{
    public class CourseModuleTemplateServiceTests
    {
        private Mock<ICourseModuleTemplateDal> mockDal;
        private Mock<IMapper> mockMapper;

        public CourseModuleTemplateServiceTests()
        {
            mockDal = new Mock<ICourseModuleTemplateDal>();
            mockMapper = new Mock<IMapper>();
        }

        #region DeleteAsync

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        [InlineData(int.MaxValue)]
        public async Task DeleteAsync_CallsDalDeleteAsync_Once(int id)
        {
            //Arrange
            var service = CreateCourseModuleTemplateService();

            //Act
            await service.DeleteAsync(id);

            //Assert
            mockDal.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        #endregion DeleteAsync

        #region GetAllByPathwayTemplateIdAsync

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        [InlineData(int.MaxValue)]
        public async Task GetAllByPathwayTemplateIdAsync_CallsDalDeleteAsync_Once(int id)
        {
            //Arrange
            var service = CreateCourseModuleTemplateService();

            //Act
            await service.GetAllByPathwayTemplateIdAsync(id);

            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Theory]
        [InlineData(0, 1, 5)]
        [InlineData(100, 3, 10)]
        [InlineData(int.MaxValue, 5, 15)]
        public async Task GetAllByPathwayTemplateIdAsync_ReturnsExpectedCourseModuleTemplates_WhenIdIsPassed(int pathwayTemplateId, int numberOfMatches, int listSize)
        {
            //Arrange
            var service = CreateCourseModuleTemplateService();
            var list = Builder<CourseModuleTemplate>.CreateListOfSize(listSize)
                .Random(numberOfMatches)
                .With(x => x.PathwayTemplateId = pathwayTemplateId)
                .Build();
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var expected = list.Where(x => x.PathwayTemplateId == pathwayTemplateId);
            //Act
            var result = await service.GetAllByPathwayTemplateIdAsync(pathwayTemplateId);

            //Assert
            result.Should().BeEquivalentTo(expected);
        }

        #endregion GetAllByPathwayTemplateIdAsync

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var service = CreateCourseModuleTemplateService();
            //Act
            await service.GetAllAsync();
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAnEnumerableOfCourseModuleTemplateViewModel_WhenSuccessful()
        {
            //Arrange
            var list = Builder<CourseModuleTemplate>.CreateListOfSize(3).Build();
            var viewModelList = Builder<CourseModuleTemplateViewModel>.CreateListOfSize(3).Build();
            mockMapper.Setup(x => x.Map<IEnumerable<CourseModuleTemplateViewModel>>(list)).Returns(viewModelList);
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            var service = CreateCourseModuleTemplateService();
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
            var service = CreateCourseModuleTemplateService();
            //Act
            await service.GetByIdAsync(id);
            //Assert
            mockDal.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCourseModuleTemplateViewModel_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var courseModuleTemplate = Builder<CourseModuleTemplate>.CreateNew().Build();
            var viewModel = Builder<CourseModuleTemplateViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<CourseModuleTemplateViewModel>(courseModuleTemplate)).Returns(viewModel);

            mockDal.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(courseModuleTemplate);
            var service = CreateCourseModuleTemplateService();
            //Act
            var result = await service.GetByIdAsync(id);
            //Assert
            result.Should().BeEquivalentTo(courseModuleTemplate);
        }

        #endregion GetByIdAsync

        #region GetAllByPathwayTemplateIdAsync

        [Fact]
        public async Task GetAllByPathwayTemplateIdAsync_CallsDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            int pathwayTemplateId = 1;
            var service = CreateCourseModuleTemplateService();
            //Act
            await service.GetAllByPathwayTemplateIdAsync(pathwayTemplateId);
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllByPathwayTemplateIdAsync_ReturnsEnumerableOfCourseModule_WithPathwayTemplateIdTheSameAsParameter()
        {
            //Arrange
            int pathwayTemplateId = 1;
            var list = Builder<CourseModuleTemplate>.CreateListOfSize(5)
                .Random(2)
                .With(x => x.PathwayTemplateId = pathwayTemplateId)
                .Build();
            var expectedResult = list.Where(x => x.PathwayTemplateId == pathwayTemplateId);
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(list.Where(x => x.PathwayTemplateId == pathwayTemplateId));
            var json = JsonConvert.SerializeObject(list);
            var service = CreateCourseModuleTemplateService();
            //Act
            var result = await service.GetAllByPathwayTemplateIdAsync(pathwayTemplateId);
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetAllByPathwayTemplateIdAsync

        #region GetAllByNullPathwayTemplateIdAsync

        [Fact]
        public async Task GetAllByNullPathwayTemplateIdAsync_CallsDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var service = CreateCourseModuleTemplateService();
            //Act
            await service.GetAllByNullPathwayTemplateIdAsync();
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllByNullPathwayTemplateIdAsync_ReturnsEnumerableOfCourseModule_WithNullPathwayTemplateId()
        {
            //Arrange
            var list = Builder<CourseModuleTemplate>.CreateListOfSize(5)
                .Random(2)
                .With(x => x.PathwayTemplateId = null)
                .Build();
            var expectedResult = list.Where(x => x.PathwayTemplateId == null);
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedResult);
            var json = JsonConvert.SerializeObject(list);
            var service = CreateCourseModuleTemplateService();
            //Act
            var result = await service.GetAllByNullPathwayTemplateIdAsync();
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        #endregion GetAllByNullPathwayTemplateIdAsync

        #region PostAsync

        [Fact]
        public async Task PostAsync_CallsDalPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var model = Builder<CourseModuleTemplate>.CreateNew().Build();
            var viewModel = Builder<CourseModuleTemplateViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<CourseModuleTemplate>(viewModel)).Returns(model);
            var service = CreateCourseModuleTemplateService();
            //Act
            await service.PostAsync(viewModel);
            //Assert
            mockDal.Verify(x => x.PostAsync(model), Times.Once());
        }

        [Fact]
        public async Task PostAsync_PostAsyncReturnsCourseModuleTemplateViewModel_WhenCalled()
        {
            //Arrange
            var model = Builder<CourseModuleTemplate>.CreateNew().Build();
            var viewModel = Builder<CourseModuleTemplateViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<CourseModuleTemplate>(viewModel)).Returns(model);
            mockMapper.Setup(x => x.Map<CourseModuleTemplateViewModel>(model)).Returns(viewModel);
            mockDal.Setup(x => x.PostAsync(model)).ReturnsAsync(model);
            var service = CreateCourseModuleTemplateService();
            //Act
            var result = await service.PostAsync(viewModel);
            //Assert
            result.Should().BeEquivalentTo(viewModel);
        }

        #endregion PostAsync

        #region PutAsync

        [Fact]
        public async Task PutAsync_CallsDalPutAsync_WhenCalled()
        {
            //Arrange
            var viewModel = Builder<CourseModuleTemplateViewModel>.CreateNew().Build();
            var model = Builder<CourseModuleTemplate>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<CourseModuleTemplate>(viewModel)).Returns(model);
            var service = CreateCourseModuleTemplateService();
            //Act
            await service.PutAsync(viewModel);
            //Assert
            mockDal.Verify(x => x.PutAsync(model), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ReturnsCourseModuleTemplateViewModel_WhenCalled()
        {
            //Arrange
            var model = Builder<CourseModuleTemplate>.CreateNew().Build();
            var viewModel = Builder<CourseModuleTemplateViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<CourseModuleTemplate>(viewModel)).Returns(model);
            mockMapper.Setup(x => x.Map<CourseModuleTemplateViewModel>(model)).Returns(viewModel);
            mockDal.Setup(x => x.PutAsync(model)).ReturnsAsync(model);
            var service = CreateCourseModuleTemplateService();
            //Act
            var actualResult = await service.PutAsync(viewModel);
            //Assert
            actualResult.Should().BeEquivalentTo(viewModel);
        }

        #endregion PutAsync

        private CourseModuleTemplateService CreateCourseModuleTemplateService()
        {
            return new CourseModuleTemplateService(mockDal.Object, mockMapper.Object);
        }
    }
}