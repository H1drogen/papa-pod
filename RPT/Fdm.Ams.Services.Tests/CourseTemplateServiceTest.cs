using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.ViewModels;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using Xunit;

namespace Fdm.Ams.Services.Tests
{
    public class CourseTemplateServiceTest
    {
        private Mock<ICourseModuleTemplateDal> mockCourseModuleTemplateDal;
        private Mock<ICourseTypeDal> mockCourseTypeDal;
        private Mock<ICourseTemplateDal> mockDal;
        private Mock<IMapper> mockMapper;
        private Mock<IRegionDal> mockRegionDal;

        public CourseTemplateServiceTest()
        {
            mockDal = new Mock<ICourseTemplateDal>();
            mockMapper = new Mock<IMapper>();
            mockCourseModuleTemplateDal = new Mock<ICourseModuleTemplateDal>();
            mockRegionDal = new Mock<IRegionDal>();
            mockCourseTypeDal = new Mock<ICourseTypeDal>();
        }

        #region CreateEvents

        [Fact]
        public async Task CreateEvents_ShouldCallCourseModuleTemplateGetByIdAsyncAtLeastOnce_WhenCalled()
        {
            //Arrange
            int courseTemplateId = 1;
            var eventsToBeCreated = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).Build();
            var courseModuleTemplates = Builder<CourseModuleTemplate>.CreateListOfSize(2).Build();

            foreach (var calendarEvent in eventsToBeCreated)
            {
                var index = eventsToBeCreated.IndexOf(calendarEvent);
                var courseModuleTemplate = mockCourseModuleTemplateDal.Setup(x => x.GetByIdAsync(calendarEvent.Id))
                    .ReturnsAsync(courseModuleTemplates[index]);

                mockCourseModuleTemplateDal.Setup(x => x.PostAsync(It.IsAny<CourseModuleTemplate>()));
            }

            var service = CreateCourseTemplateService();

            //Act
            await service.CreateEventsAsync(courseTemplateId, eventsToBeCreated);

            //Assert
            mockCourseModuleTemplateDal.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task CreateEvents_ShouldCallCourseModuleTemplatePostAsyncAtLeastOnce_WhenCalled()
        {
            //Arrange
            int courseTemplateId = 1;
            var eventsToBeCreated = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).Build();
            var courseModuleTemplates = Builder<CourseModuleTemplate>.CreateListOfSize(2).Build();

            foreach (var calendarEvent in eventsToBeCreated)
            {
                var index = eventsToBeCreated.IndexOf(calendarEvent);
                var courseModuleTemplate = mockCourseModuleTemplateDal.Setup(x => x.GetByIdAsync(calendarEvent.Id))
                    .ReturnsAsync(courseModuleTemplates[index]);

                mockCourseModuleTemplateDal.Setup(x => x.PostAsync(It.IsAny<CourseModuleTemplate>()));
            }

            var service = CreateCourseTemplateService();

            //Act
            await service.CreateEventsAsync(courseTemplateId, eventsToBeCreated);

            //Assert
            mockCourseModuleTemplateDal.Verify(x => x.PostAsync(It.IsAny<CourseModuleTemplate>()), Times.AtLeastOnce);
        }

        #endregion CreateEvents

        #region ModifyEvents

        [Fact]
        public async Task ModifyEvents_ShouldCallCourseModuleTemplateGetByIdAsyncAtleastOnce_WhenCalled()
        {
            //Arrange
            var eventsToBeCreated = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).Build();
            var courseModuleTemplates = Builder<CourseModuleTemplate>.CreateListOfSize(2).Build();

            foreach (var calendarEvent in eventsToBeCreated)
            {
                var index = eventsToBeCreated.IndexOf(calendarEvent);
                var courseModuleTemplate = mockCourseModuleTemplateDal.Setup(x => x.GetByIdAsync(calendarEvent.Id))
                    .ReturnsAsync(courseModuleTemplates[index]);

                mockCourseModuleTemplateDal.Setup(x => x.PutAsync(It.IsAny<CourseModuleTemplate>()));
            }

            var service = CreateCourseTemplateService();

            //Act
            await service.ModifyEventsAsync(eventsToBeCreated);

            //Assert
            mockCourseModuleTemplateDal.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task ModifyEvents_ShouldCallCourseModuleTemplatePutAsyncAtleastOnce_WhenCalled()
        {
            //Arrange
            var eventsToBeCreated = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).Build();
            var courseModuleTemplates = Builder<CourseModuleTemplate>.CreateListOfSize(2).Build();

            foreach (var calendarEvent in eventsToBeCreated)
            {
                var index = eventsToBeCreated.IndexOf(calendarEvent);
                var courseModuleTemplate = mockCourseModuleTemplateDal.Setup(x => x.GetByIdAsync(calendarEvent.Id))
                    .ReturnsAsync(courseModuleTemplates[index]);

                mockCourseModuleTemplateDal.Setup(x => x.PutAsync(It.IsAny<CourseModuleTemplate>()));
            }

            var service = CreateCourseTemplateService();

            //Act
            await service.ModifyEventsAsync(eventsToBeCreated);

            //Assert
            mockCourseModuleTemplateDal.Verify(x => x.PutAsync(It.IsAny<CourseModuleTemplate>()), Times.AtLeastOnce);
        }

        #endregion ModifyEvents

        #region DeleteEvents

        [Fact]
        public void DeleteEvents_ShouldCallCourseModuleTemplateDeleteAsyncAtleastOnce_WhenCalled()
        {
            //Arrange
            var eventsToBeDeletedIds = Builder<int>.CreateListOfSize(2).Build();

            var service = CreateCourseTemplateService();

            //Act
            service.DeleteEventsAsync(eventsToBeDeletedIds);

            //Assert
            mockCourseModuleTemplateDal.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.AtLeastOnce);
        }

        #endregion DeleteEvents

        #region GetPathwayTypesAndRegions

        [Fact]
        public async Task GetPathwayTypesAndRegions_ShouldCallCourseTypeDalAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var service = CreateCourseTemplateService();
            var courseTypes = Builder<CourseType>.CreateListOfSize(2).Build();
            mockCourseTypeDal.Setup(x => x.GetAllAsync()).ReturnsAsync(courseTypes);

            //Act
            await service.GetPathwayTypesAndRegions();

            //Assert
            mockCourseTypeDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetPathwayTypesAndRegions_ShouldCallRegionDalAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var service = CreateCourseTemplateService();
            var regions = Builder<Region>.CreateListOfSize(2).Build();
            mockRegionDal.Setup(x => x.GetAllAsync()).ReturnsAsync(regions);

            //Act
            await service.GetPathwayTypesAndRegions();

            //Assert
            mockRegionDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetPathwayTypesAndRegions_ShouldReturnCourseTemplateViewModelWithDictionaryOfRegionsAndPathwayTypes_WhenCalled()
        {
            //Arrange
            var service = CreateCourseTemplateService();

            var pathwayTypesList = Builder<CourseType>.CreateListOfSize(3).Build();
            var regionsList = Builder<Region>.CreateListOfSize(3).Build();
            Dictionary<int, string> pathwayTypesDictionary = new();
            Dictionary<int, string> regionsDictionary = new();
            pathwayTypesList.ToList().ForEach(x => pathwayTypesDictionary.Add(x.Id, x.Name));
            regionsList.ToList().ForEach(x => regionsDictionary.Add(x.Id, x.Name));

            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew()
                .With(x => x.PathwayTypeDictionaries = pathwayTypesDictionary)
                .With(x => x.RegionDictionaries = regionsDictionary)
                .Build();

            mockCourseTypeDal.Setup(x => x.GetAllAsync()).ReturnsAsync(pathwayTypesList);
            mockRegionDal.Setup(x => x.GetAllAsync()).ReturnsAsync(regionsList);

            //Act
            var result = await service.GetPathwayTypesAndRegions();

            //Assert
            result.PathwayTypeDictionaries.Should().BeEquivalentTo(pathwayTypesDictionary);
            result.RegionDictionaries.Should().BeEquivalentTo(regionsDictionary);
        }

        #endregion GetPathwayTypesAndRegions

        #region DeserializeEvents

        [Fact]
        public void DeserializeEvents_ShouldReturnDuration5ForFirstElementOnTheList_WhenStartAndEndDateAre5DaysApart()
        {
            //Arrange
            var startDate = new DateTime(2022, 01, 15);
            var endDate = new DateTime(2022, 01, 20);
            var Duration = 5;
            var courseTemplateCalendarEventViewModels = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(1)
                .All()
                .With(x => x.Start = startDate)
                .With(x => x.End = endDate)
                .Build();
            var service = CreateCourseTemplateService();

            //Act
            var result = service.DeserializeEvents(courseTemplateCalendarEventViewModels).ToList();

            //Assert
            Assert.Equal(result[0].Duration, Duration);
        }

        [Fact]
        public void DeserializeEvents_ShouldReturnFirstElementWithIdOneAsSecondElementOnTheList_WhenStartDateOfSecondElementIsBeforeTheFirstElement()
        {
            //Arrange
            var firstElementStartDate = new DateTime(2022, 01, 11);
            var firstElementEndDate = new DateTime(2022, 01, 12);
            var secondElementStartDate = new DateTime(2022, 01, 05);
            var secondElementEndDate = new DateTime(2022, 01, 10);

            var courseTemplateCalendarEventViewModels = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).Build();
            courseTemplateCalendarEventViewModels[0].Start = firstElementStartDate;
            courseTemplateCalendarEventViewModels[0].End = firstElementEndDate;
            courseTemplateCalendarEventViewModels[1].Start = secondElementStartDate;
            courseTemplateCalendarEventViewModels[1].End = secondElementEndDate;

            var service = CreateCourseTemplateService();

            //Act
            var result = service.DeserializeEvents(courseTemplateCalendarEventViewModels).ToList();

            //Assert
            Assert.Equal(result[1].Id, courseTemplateCalendarEventViewModels[0].Id);
        }

        #endregion DeserializeEvents

        #region SortEventsToBeCreated

        [Fact]
        public async Task SortEventsToBeCreated_ShouldCallCourseModuleTemplateDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var courseTemplateId = 1;
            var newEvents = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).Build();

            var service = CreateCourseTemplateService();

            //Act
            await service.SortEventsToBeCreatedAsync(courseTemplateId, newEvents);

            //Assert
            mockCourseModuleTemplateDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task SortEventsToBeCreated_ShouldReturnCourseModuleTemplatesWithoutAPathwayTemplateId_WhenCalled()
        {
            //Arrange
            var courseTemplateId = 1;
            var courseModuleTempaltesFromDal = Builder<CourseModuleTemplate>.CreateListOfSize(2)
                .All()
                .With(x => x.PathwayTemplateId = courseTemplateId)
                .Build();

            var listOfEvents = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(3).Build();

            mockCourseModuleTemplateDal.Setup(x => x.GetAllAsync()).ReturnsAsync(courseModuleTempaltesFromDal);

            var service = CreateCourseTemplateService();

            var expectedResult = new List<CourseTemplateCalendarEventViewModel>();
            expectedResult.Add(listOfEvents[2]);

            //Act
            var result = await service.SortEventsToBeCreatedAsync(courseTemplateId, listOfEvents);

            //Assert
            Assert.Equal(result, expectedResult);
        }

        #endregion SortEventsToBeCreated

        #region SortEventsToBeModified

        [Fact]
        public async Task SortEventsToBeModified_ShouldCallCourseModuleTemplateDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var courseTemplateId = 1;
            var newEvents = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).Build();

            var service = CreateCourseTemplateService();

            //Act
            await service.SortEventsToBeModifiedAsync(courseTemplateId, newEvents);

            //Assert
            mockCourseModuleTemplateDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task SortEventsToBeModified_ShouldReturnCourseModuleTemplatesWithPathwayTemplateId_WhenCalled()
        {
            //Arrange
            var courseTemplateId = 1;
            var courseModuleTempaltesFromDal = Builder<CourseModuleTemplate>.CreateListOfSize(2)
                .All()
                .With(x => x.PathwayTemplateId = courseTemplateId)
                .Build();

            var listOfEvents = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(3).Build();

            mockCourseModuleTemplateDal.Setup(x => x.GetAllAsync()).ReturnsAsync(courseModuleTempaltesFromDal);

            var service = CreateCourseTemplateService();

            var expectedResult = new List<CourseTemplateCalendarEventViewModel>();
            expectedResult.Add(listOfEvents[0]);
            expectedResult.Add(listOfEvents[1]);

            //Act
            var result = await service.SortEventsToBeModifiedAsync(courseTemplateId, listOfEvents);

            //Assert
            Assert.Equal(result, expectedResult);
        }

        #endregion SortEventsToBeModified

        #region SortEventsToBeDeleted

        [Fact]
        public async Task SortEventsToBeDeleted_ShouldCallCourseModuleTemplateDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var courseTemplateId = 1;
            var newEvents = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).Build();

            var service = CreateCourseTemplateService();

            //Act
            await service.SortEventsToBeDeletedAsync(courseTemplateId, newEvents);

            //Assert
            mockCourseModuleTemplateDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task SortEventsToBeDeleted_ShouldReturnCourseModuleTemplatesWithoutPathwayTemplateId_WhenCalled()
        {
            //Arrange
            var courseTemplateId = 1;
            var courseModuleTempaltesFromDal = Builder<CourseModuleTemplate>.CreateListOfSize(5)
                .All()
                .With(x => x.PathwayTemplateId = courseTemplateId)
                .Build();

            var listOfEvents = Builder<CourseTemplateCalendarEventViewModel>.CreateListOfSize(2).Build();

            mockCourseModuleTemplateDal.Setup(x => x.GetAllAsync()).ReturnsAsync(courseModuleTempaltesFromDal);

            var service = CreateCourseTemplateService();

            var expectedResult = new List<int>();
            expectedResult.Add(courseModuleTempaltesFromDal[2].Id);
            expectedResult.Add(courseModuleTempaltesFromDal[3].Id);
            expectedResult.Add(courseModuleTempaltesFromDal[4].Id);

            //Act
            var result = await service.SortEventsToBeDeletedAsync(courseTemplateId, listOfEvents);

            //Assert
            Assert.Equal(result, expectedResult);
        }

        #endregion SortEventsToBeDeleted

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsDalGetAllAsyncOnce_WhenCalled()
        {
            //Arrange
            var service = CreateCourseTemplateService();
            //Act
            await service.GetAllAsync();
            //Assert
            mockDal.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAnEnumerableOfCourseModuleTemplateFromDal_WhenSuccessful()
        {
            //Arrange
            var listOfCourseTemplates = Builder<CourseTemplate>.CreateListOfSize(3).Build();
            var listOfCourseTemplateViewModels = Builder<CourseTemplateViewModel>.CreateListOfSize(3).Build();
            mockDal.Setup(x => x.GetAllAsync()).ReturnsAsync(listOfCourseTemplates);
            mockMapper.Setup(x => x.Map<IEnumerable<CourseTemplateViewModel>>(listOfCourseTemplates)).Returns(listOfCourseTemplateViewModels);
            var service = CreateCourseTemplateService();
            //Act
            var result = await service.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(listOfCourseTemplates);
        }

        #endregion GetAllAsync

        #region GetByIdAsync

        [Fact]
        public async Task GetByIdAsync_CallsDalGetByIdAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var service = CreateCourseTemplateService();
            //Act
            await service.GetByIdAsync(id);
            //Assert
            mockDal.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCourseModuleTemplateFromDal_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var courseTemplate = Builder<CourseTemplate>.CreateNew().Build();
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew().Build();
            mockDal.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(courseTemplate);
            mockMapper.Setup(x => x.Map<CourseTemplateViewModel>(courseTemplate)).Returns(courseTemplateViewModel);
            var service = CreateCourseTemplateService();
            //Act
            var result = await service.GetByIdAsync(id);
            //Assert
            result.Should().BeEquivalentTo(courseTemplate);
        }

        #endregion GetByIdAsync

        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_CallsDeleteAsyncDalOnce_WhenCalled()
        {
            //Arrange
            var courseTemplate = Builder<CourseTemplate>.CreateNew().Build();
            var service = CreateCourseTemplateService();
            //Act
            await service.DeleteAsync(courseTemplate.Id);
            //Assert
            mockDal.Verify(x => x.DeleteAsync(courseTemplate.Id), Times.Once);
        }

        #endregion DeleteAsync

        #region PostAsync

        [Fact]
        public async Task PostAsync_CallsDalPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var courseTamplateViewModel = Builder<CourseTemplateViewModel>.CreateNew().Build();

            var courseTemplate = Builder<CourseTemplate>.CreateNew().Build();

            mockMapper.Setup(x => x.Map<CourseTemplate>(courseTamplateViewModel)).Returns(courseTemplate);

            var service = CreateCourseTemplateService();

            //Act
            await service.PostAsync(courseTamplateViewModel);

            //Assert
            mockDal.Verify(x => x.PostAsync(courseTemplate), Times.Once());
        }

        [Fact]
        public async Task PostAsync_ShouldReturnsCourseTemplateViewModel_WhenCalled()
        {
            //Arrange
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew().Build();

            var courseTemplate = Builder<CourseTemplate>.CreateNew().Build();

            mockMapper.Setup(x => x.Map<CourseTemplate>(courseTemplateViewModel)).Returns(courseTemplate);
            mockDal.Setup(x => x.PostAsync(courseTemplate)).ReturnsAsync(courseTemplate);

            mockMapper.Setup(x => x.Map<CourseTemplateViewModel>(courseTemplate)).Returns(courseTemplateViewModel);

            var service = CreateCourseTemplateService();

            //Act
            var result = await service.PostAsync(courseTemplateViewModel);

            //Assert
            result.Should().BeEquivalentTo(courseTemplateViewModel);
        }

        #endregion PostAsync

        #region PutAsync

        [Fact]
        public async Task PutAsync_ShouldCallDalGetByIdAsync_WhenCalled()
        {
            //Arrange
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew().Build();
            var service = CreateCourseTemplateService();

            //Act
            await service.PutAsync(courseTemplateViewModel);

            //Assert
            mockDal.Verify(x => x.GetByIdAsync(courseTemplateViewModel.Id), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldCallDalPutAsync_WhenCalled()
        {
            //Arrange
            var courseTemplateViewModel = Builder<CourseTemplateViewModel>.CreateNew().Build();

            var service = CreateCourseTemplateService();
            //Act
            await service.PutAsync(courseTemplateViewModel);
            //Assert
            mockDal.Verify(x => x.PutAsync(It.IsAny<CourseTemplate>()), Times.Once);
        }

        #endregion PutAsync

        private CourseTemplateService CreateCourseTemplateService()
        {
            return new CourseTemplateService(mockDal.Object, mockMapper.Object, mockRegionDal.Object, mockCourseTypeDal.Object, mockCourseModuleTemplateDal.Object);
        }
    }
}