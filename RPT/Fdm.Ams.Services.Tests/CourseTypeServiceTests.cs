using AutoMapper;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.ViewModels;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Fdm.Ams.Services.Tests
{
    public class CourseTypeServiceTests
    {
        private Mock<ICourseTypeDal> mockDal;
        private Mock<IMapper> mockMapper;
        public CourseTypeServiceTests()
        {
            mockDal = new Mock<ICourseTypeDal>();
            mockMapper = new Mock<IMapper>();
        }


        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsDalGetAllAsyncGetAsyncOnce_WhenCalled()
        {
            //Arrange
            var service = CreateCourseTypeService();
            //Act
            await service.GetAllAsync();
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAnEnumerableOfCourseType_WhenSuccessful()
        {
            //Arrange
            var courseTypeList = Builder<CourseType>.CreateListOfSize(3).Build();
            var courseTypeViewModelList = Builder<CourseTypeViewModel>.CreateListOfSize(3).Build();
            mockMapper.Setup(x => x.Map<IEnumerable<CourseType>>(courseTypeViewModelList)).Returns(courseTypeList);
            mockMapper.Setup(x => x.Map<IEnumerable<CourseTypeViewModel>>(courseTypeList)).Returns(courseTypeViewModelList);

            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(courseTypeList);
            var service = CreateCourseTypeService();
            //Act
            var result = await service.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(courseTypeList);
        }

        #endregion GetAllAsync

        #region GetById

        [Fact]
        public async Task GetByIdAsync_ReturnsCourseTypeFromDal_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var courseType = Builder<CourseType>.CreateNew().Build();
            var courseTypeViewModel = Builder<CourseTypeViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<CourseType>(courseTypeViewModel)).Returns(courseType);
            mockMapper.Setup(x => x.Map<CourseTypeViewModel>(courseType)).Returns(courseTypeViewModel);

            mockDal.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(courseType);
            var service = CreateCourseTypeService();

            //Act
            var result = await service.GetByIdAsync(id);

            //Assert
            result.Should().BeEquivalentTo(courseType);
        }

        #endregion GetById

        #region PostAsync

        [Fact]
        public async Task PostAsync_CallsDalPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var courseType = Builder<CourseType>.CreateNew().Build();
            var courseTypeViewModel = Builder<CourseTypeViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<CourseType>(courseTypeViewModel)).Returns(courseType);

            var service = CreateCourseTypeService();
            //Act
            await service.PostAsync(courseTypeViewModel);
            //Assert
            mockDal.Verify(x => x.PostAsync(courseType), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ReturnsCourseTypeViewModel_WhenCalled()
        {
            //Arrange
            var courseType = Builder<CourseType>.CreateNew().Build();
            var courseTypeViewModel = Builder<CourseTypeViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<CourseType>(courseTypeViewModel)).Returns(courseType);
            mockMapper.Setup(x => x.Map<CourseTypeViewModel>(courseType)).Returns(courseTypeViewModel);
            mockDal.Setup(x => x.PostAsync(courseType)).ReturnsAsync(courseType);


            var service = CreateCourseTypeService();
            //Act
            var result = await service.PostAsync(courseTypeViewModel);
            //Assert
            result.Should().BeEquivalentTo(courseTypeViewModel);
        }

        #endregion PostAsync



        #region PutAsync

        [Fact]
        public async Task PutAsync_CallsDalPutAsync_WhenCalled()
        {
            //Arrange
            var courseType = Builder<CourseType>.CreateNew().Build();
            var courseTypeViewModel = Builder<CourseTypeViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<CourseType>(courseTypeViewModel)).Returns(courseType);

            var service = CreateCourseTypeService();
            //Act
            await service.PutAsync(courseTypeViewModel);
            //Assert
            mockDal.Verify(x => x.PutAsync(courseType), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldReturnCourseTypeViewModel_WhenCalled()
        {
            //Arrange
            var courseType = Builder<CourseType>.CreateNew().Build();
            var courseTypeViewModel = Builder<CourseTypeViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<CourseType>(courseTypeViewModel)).Returns(courseType);
            mockMapper.Setup(x => x.Map<CourseTypeViewModel>(courseType)).Returns(courseTypeViewModel);
            mockDal.Setup(x => x.PutAsync(courseType)).ReturnsAsync(courseType);
            var service = CreateCourseTypeService();
            //Act
            var actualResult = await service.PutAsync(courseTypeViewModel);
            //Assert
            actualResult.Should().BeEquivalentTo(courseTypeViewModel);
        }

        #endregion PutAsync



        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_CallsDalDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var service = CreateCourseTypeService();

            //Act
            await service.DeleteAsync(id);

            //Assert
            mockDal.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        #endregion DeleteAsync

        private CourseTypeService CreateCourseTypeService()
        {
            return new CourseTypeService(mockDal.Object, mockMapper.Object);
        }
    }
}