using AutoMapper;
using Fdm.Common.Service;
using Fdm.Common.Tests.Services;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using Neleus.LambdaCompare;
using NUnit.Framework;
using System.Data;
using System.Linq.Expressions;

namespace Fdm.ResourcePlainningTool.Services.Tests
{
    /// <summary>
    /// Class for all the Test for the Holiday Service tests.
    /// First level is for any custom methods specific to the HolidayService and NOT inherited from the GenericServiceTest
    /// </summary>

    public class HolidayServiceTest

    {
        public class HolidayGenericServiceTest : GenericServiceTest<Holiday, HolidayDto, PostHolidayDto, IHolidayRepository>

        {
            private Mock<ICountryRepository> mockCountryRepo;
            private Mock<IMapper> mockMapper;
            private Mock<IHolidayRepository> mockRepo;

            public HolidayService HolidayServiceTestHelper()
            {
                return new HolidayService(mockMapper.Object, mockRepo.Object, mockCountryRepo.Object);
            }

            #region Create

            [Test]
            public async Task HolidayService_Create_ShouldCallGetEntitiesAsyncOnce_WhenCalled()
            {
                //Arrange
                var service = HolidayServiceTestHelper();
                var postDto = Builder<PostHolidayDto>.CreateNew().Build();

                Expression<Func<Holiday, bool>> expectedExpression = x => x.Name == postDto.Name &&
                x.CountryId == postDto.CountryId &&
                x.StartDate.Date == postDto.StartDate.Date;

                //Act
                await service.Create(postDto);
                //Assert
                mockRepo.Verify(x => x.GetEntitiesAsync(It.Is<Expression<Func<Holiday, bool>>>(actualExpression =>
                            Lambda.Eq(actualExpression, expectedExpression))), Times.Once);
            }

            [Test]
            public async Task HolidayService_Create_ShouldReturnsResultFromMockMapper_WhenCalled()
            {
                //Arrange
                var service = HolidayServiceTestHelper();
                var postDto = Builder<PostHolidayDto>.CreateNew().Build();
                var dto = Builder<HolidayDto>.CreateNew().Build();
                var poco = Builder<Holiday>.CreateNew().Build();
                mockMapper.Setup(x => x.Map<Holiday>(postDto)).Returns(poco);
                mockMapper.Setup(x => x.Map<HolidayDto>(poco)).Returns(dto);
                mockRepo.Setup(x => x.InsertAsync(poco)).ReturnsAsync(poco);

                Expression<Func<Holiday, bool>> expectedExpression = x => x.Name == postDto.Name &&
                x.CountryId == postDto.CountryId &&
                x.StartDate.Date == postDto.StartDate.Date;
                mockRepo.Setup(m => m.GetEntitiesAsync(It.Is<Expression<Func<Holiday, bool>>>(actualExpression =>
                                Lambda.Eq(actualExpression, expectedExpression))));

                //Act
                var result = await service.Create(postDto);
                //Assert
                result.Should().Be(dto);
            }

            [Test]
            public async Task HolidayService_Create_ThrowsException_WhenDuplicateObjectIsPassed()
            {
                //Arrange
                var service = HolidayServiceTestHelper();
                var postDto = Builder<PostHolidayDto>.CreateNew().Build();
                var pocoList = Builder<Holiday>.CreateListOfSize(1).Build();

                Expression<Func<Holiday, bool>> expectedExpression = x => x.Name == postDto.Name &&
                x.CountryId == postDto.CountryId &&
                x.StartDate.Date == postDto.StartDate.Date;
                mockRepo.Setup(m => m.GetEntitiesAsync(It.Is<Expression<Func<Holiday, bool>>>(actualExpression =>
                                Lambda.Eq(actualExpression, expectedExpression)))).ReturnsAsync(pocoList);

                //Act
                try
                {
                    await service.Create(postDto);
                }
                //Assert
                catch (Exception ex)
                {
                    ex.Should().BeOfType<DuplicateNameException>();
                }
            }

            #endregion Create

            #region Update

            [Test]
            public async Task HolidayService_Update_ShouldCallGetEntitiesAsyncOnce_WhenCalled()
            {
                //Arrange
                var service = HolidayServiceTestHelper();
                var holidayDto = Builder<HolidayDto>.CreateNew().Build();

                Expression<Func<Holiday, bool>> expectedExpression = x => x.Name == holidayDto.Name &&
                x.CountryId == holidayDto.CountryId &&
                x.StartDate.Date == holidayDto.StartDate.Date;

                //Act
                await service.Update(holidayDto);
                //Assert
                mockRepo.Verify(x => x.GetEntitiesAsync(It.Is<Expression<Func<Holiday, bool>>>(actualExpression =>
                            Lambda.Eq(actualExpression, expectedExpression))), Times.Once);
            }

            [Test]
            public virtual async Task HolidayService_Update_ShouldCallRepositorySaveChangesAsync_WhenCalled()
            {
                //Arrange
                var service = HolidayServiceTestHelper();
                var holidayDto = Builder<HolidayDto>.CreateNew().Build();
                //Act
                await service.Update(holidayDto);
                //Assert
                mockRepo.Verify(x => x.SaveChangesAsync(), Times.Once);
            }

            [Test]
            public async Task HolidayService_Update_ShouldReturnsResultFromMockMapper_WhenCalled()
            {
                //Arrange
                var service = HolidayServiceTestHelper();
                var holidayDto = Builder<HolidayDto>.CreateNew().Build();

                var poco = Builder<Holiday>.CreateNew().Build();
                mockMapper.Setup(x => x.Map<Holiday>(holidayDto)).Returns(poco);
                mockMapper.Setup(x => x.Map<HolidayDto>(poco)).Returns(holidayDto);
                mockRepo.Setup(x => x.Update(poco)).Returns(poco);

                Expression<Func<Holiday, bool>> expectedExpression = x => x.Name == holidayDto.Name &&
                x.CountryId == holidayDto.CountryId &&
                x.StartDate.Date == holidayDto.StartDate.Date;
                mockRepo.Setup(m => m.GetEntitiesAsync(It.Is<Expression<Func<Holiday, bool>>>(actualExpression =>
                                Lambda.Eq(actualExpression, expectedExpression))));

                //Act
                var result = await service.Update(holidayDto);
                //Assert
                result.Should().Be(holidayDto);
            }

            [Test]
            public async Task HolidayService_Update_ThrowsException_WhenDuplicateObjectIsPassed()
            {
                //Arrange
                var service = HolidayServiceTestHelper();
                var holidayDto = Builder<HolidayDto>.CreateNew().Build();
                var pocoList = Builder<Holiday>.CreateListOfSize(1).Build();

                Expression<Func<Holiday, bool>> expectedExpression = x => x.Name == holidayDto.Name &&
                x.CountryId == holidayDto.CountryId &&
                x.StartDate.Date == holidayDto.StartDate.Date;
                mockRepo.Setup(m => m.GetEntitiesAsync(It.Is<Expression<Func<Holiday, bool>>>(actualExpression =>
                                Lambda.Eq(actualExpression, expectedExpression)))).ReturnsAsync(pocoList);

                //Act
                try
                {
                    await service.Update(holidayDto);
                }
                //Assert
                catch (Exception ex)
                {
                    ex.Should().BeOfType<DuplicateNameException>();
                }
            }

            #endregion Update

            [SetUp]
            public void SetUp()
            {
                mockRepo = new Mock<IHolidayRepository>();

                mockCountryRepo = new Mock<ICountryRepository>();

                mockMapper = new Mock<IMapper>();
            }

            protected override GenericService<Holiday, HolidayDto, PostHolidayDto, IHolidayRepository> GenericServiceTestHelper
                                       (Mock<IMapper> mockMapper, Mock<IHolidayRepository> mockRepo)
            {
                return new HolidayService(mockMapper.Object, mockRepo.Object, mockCountryRepo.Object);
            }
        }
    }
}