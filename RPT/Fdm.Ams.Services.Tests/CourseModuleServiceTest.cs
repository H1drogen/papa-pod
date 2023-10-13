using AutoMapper;
using Fdm.Ams.Common;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.Models.Post;
using Fdm.Ams.ViewModels;
using FizzWare.NBuilder;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Fdm.Ams.Services.Tests
{
    public class CourseModuleServiceTest
    {
        private Mock<ICourseDal> mockCourseDal;
        private Mock<ICourseModuleDal> mockDal;

        private Mock<IMapper> mockMapper;

        public CourseModuleServiceTest()
        {
            mockDal = new Mock<ICourseModuleDal>();
            mockMapper = new Mock<IMapper>();
            mockCourseDal = new Mock<ICourseDal>();
        }

        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_CallsDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var service = CreateCourseModuleService();
            //Act
            await service.DeleteAsync(id);
            //Assert
            mockDal.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Fact] 
        public async Task DeleteAsync_WhenSuccessful_GetAllCourseModule_ShouldNotContainDeletedCourseModule()
        {
            //Arrange
            var deletedCourseModule = Builder<CourseModule>.CreateNew().Build();
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<CourseModule>(courseModuleViewModel)).Returns(deletedCourseModule);
            mockDal.Setup(x => x.PostAsync(deletedCourseModule));
            var service = CreateCourseModuleService();

            //Act
            await service.DeleteAsync(courseModuleViewModel.Id);
           
            //Assert
            Assert.DoesNotContain(courseModuleViewModel, await service.GetAllAsync());
        }



        #endregion DeleteAsync

        #region PostAsync

        [Fact]
        public async Task PostAsync_CallsDalPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            var courseModule = Builder<CourseModule>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<CourseModule>(courseModuleViewModel)).Returns(courseModule);
            var service = CreateCourseModuleService();
            //Act
            await service.PostAsync(courseModule);
            //Assert
            mockDal.Verify(x => x.PostAsync(courseModule), Times.Once);
        }

        #endregion PostAsync
        #region PostCourseModuleList
        [Fact]
        public async Task PostCourseModuleList_CallsDalPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var listSize = 1;
            var courseTemplate = Builder<CourseTemplate>.CreateNew().Build();
            var listOfCourseModuleTemplate = Builder<CourseModuleTemplate>.CreateListOfSize(listSize).Build();
            var selectedTimeZone = "UTC";
            var returnedCourseViewModel = Builder<CourseViewModel>.CreateNew().With(x => x.SelectedTimeZone = selectedTimeZone).Build();
            var moduleStartDate = DateTimeHelper.StartOfWeek(returnedCourseViewModel.StartDate.Value);

            var courseModuleViewModel = new CourseModuleViewModel
            {
                PathwayId = returnedCourseViewModel.Id,
                Description = listOfCourseModuleTemplate[0].Description,
                Duration = listOfCourseModuleTemplate[0].Duration,
                Name = listOfCourseModuleTemplate[0].Name,
                PreparationNotes = listOfCourseModuleTemplate[0].PreparationNotes,
                StartDate = moduleStartDate,
                EndDate = moduleStartDate.AddDays(listOfCourseModuleTemplate[0].Duration).AddHours(-17)
            };
            var courseModule = Builder<CourseModule>.CreateNew()
                .With(x => x.EndDate = courseModuleViewModel.EndDate)
                .With(x => x.VenueId = courseModuleViewModel.VenueId)
                .With(x => x.TrainerId = courseModuleViewModel.TrainerId)
                .Build();
            mockDal.Setup(x => x.PostAsync(It.IsAny<CourseModule>()));

            var service = CreateCourseModuleService();

            //Act
            await service.PostAsync(courseModule);

            //Assert
            mockDal.Verify(x => x.PostAsync(It.IsAny<CourseModule>()), Times.Once);
        }
        #endregion

        #region PutAsync

        [Fact]
        public async Task PutAsync_ShouldCallsDalGetByIdAsync_WhenCalled()
        {
            //Arrange
            var (courseModuleViewModel, course, courseModule) = BuildModelsViewModelsAndMockModelDalHelper();
            var service = CreateCourseModuleService();
            //Act
            await service.PutAsync(courseModuleViewModel);
            //Assert
            mockDal.Verify(x => x.GetByIdAsync(courseModule.Id), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldCallDalPutAsync_WhenCalled()
        {
            //Arrange
            var courseModule = Builder<CourseModule>.CreateNew().Build();
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            mockDal.Setup(x => x.GetByIdAsync(courseModule.Id)).ReturnsAsync(courseModule);
            var service = CreateCourseModuleService();
            //Act
            await service.PutAsync(courseModuleViewModel);
            //Assert
            mockDal.Verify(x => x.PutAsync(courseModule), Times.Once);
        }


        [Fact]
        public async Task PutAsync_ShouldReturnCourseModule_WhenCalled()
        {
            //Arrange
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            var courseModule = Builder<CourseModule>.CreateNew().Build();
            mockDal.Setup(x => x.GetByIdAsync(courseModuleViewModel.Id)).ReturnsAsync(courseModule);
            mockDal.Setup(x => x.PutAsync(courseModule));
            var service = CreateCourseModuleService();
            mockDal.Setup(x => x.GetByIdAsync(courseModuleViewModel.Id)).ReturnsAsync(courseModule);
            mockDal.Setup(x => x.PutAsync(courseModule));
            //Act
            var actualResult = await service.PutAsync(courseModuleViewModel);
            //Assert
            actualResult.Should().BeEquivalentTo(courseModule);
        }


        #endregion PutAsync



        #region GetByIdForEditAsync

        [Fact]
        public async Task GetByIdForEditAsync_ShouldCallsDalGetByIdAsync_WhenCalled()
        {
            //Arrange
            int id = 1;
            var courseModule = Builder<CourseModule>.CreateNew().Build();
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            var course = Builder<Course>.CreateNew().Build();
            var courseViewModel = Builder<CourseViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<CourseModule>(courseModuleViewModel)).Returns(courseModule);
            mockMapper.Setup(x => x.Map<CourseModuleViewModel>(courseModule)).Returns(courseModuleViewModel);
            mockMapper.Setup(x => x.Map<Course>(courseViewModel)).Returns(course);
            mockMapper.Setup(x => x.Map<CourseViewModel>(course)).Returns(courseViewModel);

            mockDal.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(courseModule);
            mockCourseDal.Setup(x => x.GetByIdAsync(courseModule.PathwayId.Value)).ReturnsAsync(course);
            var service = CreateCourseModuleService();
            //Act
            await service.GetByIdForEditAsync(id);
            //Assert
            mockDal.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdForEditAsync_ShouldReturnsCourseModuleViewModel_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var courseModule = Builder<CourseModule>.CreateNew().Build();
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            var course = Builder<Course>.CreateNew().Build();
            var courseViewModel = Builder<CourseViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<CourseModule>(courseModuleViewModel)).Returns(courseModule);
            mockMapper.Setup(x => x.Map<CourseModuleViewModel>(courseModule)).Returns(courseModuleViewModel);
            mockMapper.Setup(x => x.Map<Course>(courseViewModel)).Returns(course);
            mockMapper.Setup(x => x.Map<CourseViewModel>(course)).Returns(courseViewModel);

            mockDal.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(courseModule);
            mockCourseDal.Setup(x => x.GetByIdAsync(courseModule.PathwayId.Value)).ReturnsAsync(course);
            var service = CreateCourseModuleService();
            //Act
            var result = await service.GetByIdForEditAsync(id);
            //Assert
            result.Should().BeEquivalentTo(courseModule);
        }


        #endregion GetByIdForEditAsync





        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_ShouldCallsDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var service = CreateCourseModuleService();
            //Act
            await service.GetAllAsync();
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnsAnEnumerableOfCourseModuleFromClient_WhenSuccessful()
        {
            //Arrange
            var courseModuleList = Builder<CourseModule>.CreateListOfSize(3).Build();
            var courseModuleViewModelList = Builder<CourseModuleViewModel>.CreateListOfSize(3).Build();
            mockMapper.Setup(x => x.Map<IEnumerable<CourseModule>>(courseModuleViewModelList)).Returns(courseModuleList);
            mockMapper.Setup(x => x.Map<IEnumerable<CourseModuleViewModel>>(courseModuleList)).Returns(courseModuleViewModelList);

            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(courseModuleList);
            var service = CreateCourseModuleService();


            //Act
            var result = await service.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(courseModuleList);
        }


        #endregion GetAllAsync

        #region GetByIdAsync

        [Fact]
        public async Task GetByIdAsync_ShouldCallsDalGetByIdAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
          
            var service = CreateCourseModuleService();
          
            //Act
            await service.GetByIdAsync(id);
            //Assert
            mockDal.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnsCourseModuleViewModel_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var courseModule = Builder<CourseModule>.CreateNew().Build();
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            var service = CreateCourseModuleService();

            mockMapper.Setup(x => x.Map<CourseModule>(courseModuleViewModel)).Returns(courseModule);
            mockMapper.Setup(x => x.Map<CourseModuleViewModel>(courseModule)).Returns(courseModuleViewModel);
            mockDal.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(courseModule);
            mockMapper.Setup(x => x.Map<CourseModuleViewModel>(courseModule)).Returns(courseModuleViewModel);

            //Act
            var result = await service.GetByIdAsync(id);
            //Assert
            result.Should().BeEquivalentTo(courseModuleViewModel);
        }

        #endregion GetByIdAsync

        #region PutUnAssign

        [Fact]
        public async Task PutUnAssign_ShouldCallsDalGetByIdAsyncWithIdOnce_WhenCalled()
        {
            //Arrange
            var returnedCourseModule = Builder<CourseModule>.CreateNew().Build();
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<CourseModule>(courseModuleViewModel)).Returns(returnedCourseModule);
            var service = CreateCourseModuleService();
            //Act
            await service.PutUnAssignAsync(courseModuleViewModel);
            //Assert
            mockDal.Verify(x => x.GetByIdAsync(returnedCourseModule.Id), Times.Once);
        }

        [Fact]
        public async Task PutUnAssign_ShouldCallsDalPutAsyncOnce_WhenCalled()
        {
            //Arrange
            var courseModule = Builder<CourseModule>.CreateNew().Build();
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();
            mockMapper.Setup(x => x.Map<CourseModule>(courseModuleViewModel)).Returns(courseModule);
            mockMapper.Setup(x => x.Map<CourseModuleViewModel>(courseModule)).Returns(courseModuleViewModel);
            mockDal.Setup(x => x.GetByIdAsync(courseModule.Id)).ReturnsAsync(courseModule);
            var service = CreateCourseModuleService();
            //Act
            await service.PutUnAssignAsync(courseModuleViewModel);
            //Assert
            mockDal.Verify(x => x.PutAsync(courseModule), Times.Once);
        }

        #endregion PutUnAssign

        #region EditAsync
        [Fact]
        public async Task EditAsync_ShouldCallsCourseModuleDalGetByIdAsync_WhenCalled()
        {
            //Arrange

            var (courseModuleViewModel, course, courseModule) = BuildModelsViewModelsAndMockModelDalHelper();
            var service = CreateCourseModuleService();
          
            //Act
            await service.GetByIdForEditAsync(courseModuleViewModel.Id);
            //Assert
            mockDal.Verify(x => x.GetByIdAsync(courseModule.Id), Times.Once);
        }
        [Fact]
        public async Task EditAsync_ShouldCallsCourseDalGetByIdAsync_WhenCalled()
        {
            //Arrange
            var (courseModuleViewModel, course, courseModule) = BuildModelsViewModelsAndMockModelDalHelper();
            var service = CreateCourseModuleService();

            //Act
            await service.GetByIdForEditAsync(courseModuleViewModel.Id);
            //Assert
            mockCourseDal.Verify(x => x.GetByIdAsync(course.Id), Times.Once);
        }
        [Fact]
        public async Task EditAsync_ShouldReturnsCourseModuleViewModel_WhenSuccessful()
        {
            //Arrange
           var updatedCourseModule= Builder<CourseModule>.CreateNew().Build();
           var updatedCourse= Builder<Course>.CreateNew().Build();
           
            var service = CreateCourseModuleService();
           
            var courseModuleViewModel = new CourseModuleViewModel
            {
                PathwayId = updatedCourseModule.Id,
                Description = updatedCourseModule.Description,
                Name = updatedCourseModule.Name,
                PreparationNotes = updatedCourseModule.PreparationNotes,
                StartDate = updatedCourseModule.StartDate,
                EndDate = updatedCourseModule.EndDate,
                TrainerId =updatedCourseModule.TrainerId,
                VenueId= updatedCourseModule.VenueId,
                Provisional=updatedCourseModule.Provisional,
                Id= updatedCourseModule.Id,
                CourseStartDate=updatedCourse.StartDate,
                CourseEndDate=updatedCourse.EndDate,
            };
            mockDal.Setup(x => x.GetByIdAsync(courseModuleViewModel.Id)).ReturnsAsync(updatedCourseModule);
            mockCourseDal.Setup(x => x.GetByIdAsync(courseModuleViewModel.Id)).ReturnsAsync(updatedCourse);

            //Act
            var result=  await service.GetByIdForEditAsync(courseModuleViewModel.Id);
            //Assert
            result.Should().BeEquivalentTo(courseModuleViewModel);
        }
        #endregion
       
        #region Helper Method
        private (CourseModuleViewModel viewModel, Course modelCourse,CourseModule modelCourseModule)
        BuildModelsViewModelsAndMockModelDalHelper()
        {
            var courseModule = Builder<CourseModule>.CreateNew().Build();
            var course = Builder<Course>.CreateNew().Build();
            var courseModuleViewModel = Builder<CourseModuleViewModel>.CreateNew().Build();


            mockDal.Setup(x => x.GetByIdAsync(courseModuleViewModel.Id)).ReturnsAsync(courseModule);
            mockDal.Setup(x => x.PutAsync(courseModule));
            mockCourseDal.Setup(x => x.GetByIdAsync(courseModuleViewModel.Id)).ReturnsAsync(course);

            return (courseModuleViewModel, course, courseModule);
        }
        #endregion
        private CourseModuleService CreateCourseModuleService()
        {
            return new CourseModuleService(mockDal.Object, mockMapper.Object, mockCourseDal.Object);
        }

        private DateTime StartOfWeek(DateTime dateTime)
        {
            int diff = (7 + (dateTime.DayOfWeek - DayOfWeek.Monday)) % 7;
            return dateTime.AddDays(-1 * diff);
        }
    }
}