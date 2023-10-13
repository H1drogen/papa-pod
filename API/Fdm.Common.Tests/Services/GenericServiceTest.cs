using AutoMapper;
using Fdm.Common.Repository;
using Fdm.Common.Service;
using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using Fdm.ResourcePlanningTool.Dtos;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Fdm.Common.Tests.Services
{
    [TestFixture]
    public abstract class GenericServiceTest<T, TDto, TPostDto, TIRepository>
        where T : Model
        where TDto : class, IGenericDto
        where TPostDto : class
        where TIRepository : class, IGenericRepository<T>
    {
        private Mock<IMapper> mockMapper;
        private Mock<TIRepository> mockRepo;

        protected abstract GenericService<T, TDto, TPostDto, TIRepository> GenericServiceTestHelper(Mock<IMapper> mockMapper, Mock<TIRepository> mockRepo);

        [SetUp]
        protected async Task Setup()
        {
            mockMapper = new Mock<IMapper>();
            mockRepo = new Mock<TIRepository>();
        }

        #region Create

        [Test]
        public virtual async Task Create_ReturnsResultFrommockMapper_WhenCalled()
        {
            //Arrange
            var dto = CreateTDtoData();
            var postDto = CreateTPostDtoData();
            var poco = CreateTData();
            mockMapper.Setup(x => x.Map<T>(postDto)).Returns(poco);
            mockMapper.Setup(x => x.Map<TDto>(poco)).Returns(dto);
            mockRepo.Setup(x => x.InsertAsync(poco)).ReturnsAsync(poco);
            var service = GenericServiceTestHelper(mockMapper, mockRepo);
            //Act
            var result = await service.Create(postDto);
            //Assert
            result.Should().Be(dto);
        }

        [Test]
        public virtual async Task Create_ShouldCallRepositoryInsertAsyncWithCorrectObject_WhenPassedDto()
        {
            //Arrange
            var service = GenericServiceTestHelper(mockMapper, mockRepo);
            var postDto = CreateTPostDtoData();
            var poco = CreateTData();
            mockMapper.Setup(x => x.Map<T>(postDto)).Returns(poco);
            //Act
            await service.Create(postDto);
            //Assert
            mockRepo.Verify(x => x.InsertAsync(poco), Times.Once);
        }

        #endregion Create

        #region Delete

        [Test]
        public virtual async Task Delete_ShouldCallRepositoryDeleteAsync_WhenPassedInDto()
        {
            //Arrange
            var service = GenericServiceTestHelper(mockMapper, mockRepo);
            var dto = CreateTDtoData();
            var poco = CreateTData();
            //Act
            await service.Delete(dto.Id);
            //Assert
            mockRepo.Verify(x => x.Delete(poco.Id), Times.Once);
        }

        [Test]
        public virtual async Task Delete_ShouldCallRepositorySaveChangesAsync_WhenCalled()
        {
            //Arrange
            var service = GenericServiceTestHelper(mockMapper, mockRepo);
            var dto = CreateTDtoData();
            //Act
            await service.Delete(dto.Id);
            //Assert
            mockRepo.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        #endregion Delete

        #region GetById

        [Test]
        public virtual async Task GetById_ReturnsNull_WhenPassedIdThatDoesNotExist()
        {
            //Arrange
            var service = GenericServiceTestHelper(mockMapper, mockRepo);
            //Act
            var result = await service.GetById(1);
            //Assert
            result.Should().BeNull();
        }

        [Test]
        public virtual async Task GetById_ReturnsResultFromockMapper_WhenCalled()
        {
            //Arrange
            var service = GenericServiceTestHelper(mockMapper, mockRepo);
            var poco = CreateTData();
            var dto = CreateTDtoData();
            mockMapper.Setup(x => x.Map<TDto>(poco)).Returns(dto);
            mockRepo.Setup(x => x.GetByIdAsync(poco.Id).Result).Returns(poco);
            //Act
            var result = await service.GetById(poco.Id);
            //Assert
            result.Should().Be(dto);
        }

        [Test]
        public virtual async Task GetById_ShouldCallGetByIdAsync_WhenCalled()
        {
            //Arrange
            int id = 1;
            var service = GenericServiceTestHelper(mockMapper, mockRepo);
            //Act
            await service.GetById(id);
            //Assert
            mockRepo.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        #endregion GetById

        #region GetByIdNoTracking

        [Test]
        public virtual async Task GetByIdNoTracking_ShouldCallGetByIdNoTrackingAsync_WhenCalled()
        {
            //Arrange
            int id = 1;
            var service = GenericServiceTestHelper(mockMapper, mockRepo);
            //Act
            await service.GetByIdNoTracking(id);
            //Assert
            mockRepo.Verify(x => x.GetByIdNoTrackingAsync(id), Times.Once);
        }

        [Test]
        public virtual async Task GetByIdNoTracking_ShouldReturnNull_WhenPassedIdThatDoesNotExist()
        {
            //Arrange
            var service = GenericServiceTestHelper(mockMapper, mockRepo);
            //Act
            var result = await service.GetByIdNoTracking(1);
            //Assert
            result.Should().BeNull();
        }

        #endregion GetByIdNoTracking

        #region GetAll

        [Test]
        public virtual async Task GetAll_ReturnIEnumerableOfDtosFrommockMapper_WhenCalled()
        {
            //Arrange
            var service = GenericServiceTestHelper(mockMapper, mockRepo);
            var pocoList = Builder<T>.CreateListOfSize(3).Build();
            var dtoList = Builder<TDto>.CreateListOfSize(3).Build();
            mockRepo.Setup(x => x.GetAllAsync().Result).Returns(pocoList);
            mockMapper.Setup(x => x.Map<IEnumerable<TDto>>(pocoList)).Returns(dtoList);
            //Act
            var result = await service.GetAll();
            //Assert
            result.Should().BeSameAs(dtoList);
        }

        [Test]
        public virtual async Task GetAll_ShouldCallGetAllAsync_WhenCalled()
        {
            //Arrange
            var service = GenericServiceTestHelper(mockMapper, mockRepo);
            //Act
            await service.GetAll();
            //Assert
            mockRepo.Verify(x => x.GetAllAsync(), Times.Once);
        }

        #endregion GetAll

        #region Update

        [Test]
        public virtual async Task Update_ShouldCallRepositorySaveChangesAsync_WhenCalled()
        {
            //Arrange
            var service = GenericServiceTestHelper(mockMapper, mockRepo);
            var dto = CreateTDtoData();
            //Act
            await service.Update(dto);
            //Assert
            mockRepo.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public virtual async Task Update_ShouldCallRepositoryUpdateAsync_WhenDtoIsPassedIn()
        {
            //Arrange
            var service = GenericServiceTestHelper(mockMapper, mockRepo);
            var dto = CreateTDtoData();
            var poco = CreateTData();
            mockMapper.Setup(x => x.Map<T>(dto)).Returns(poco);
            //Act
            await service.Update(dto);
            //Assert
            mockRepo.Verify(x => x.Update(poco), Times.Once);
        }

        #endregion Update

        #region Test Helper Methods

        private static T CreateTData()
        {
            return Builder<T>.CreateNew().Build();
        }

        private static TDto CreateTDtoData()
        {
            return Builder<TDto>.CreateNew().Build();
        }

        private static TPostDto CreateTPostDtoData()
        {
            return Builder<TPostDto>.CreateNew().Build();
        }

        #endregion Test Helper Methods
    }
}