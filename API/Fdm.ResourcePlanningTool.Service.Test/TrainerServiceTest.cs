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
    /// This class inherits from the GenericService Tests so all the test defined
    /// in the GenericService Tests are run for the TrainerService
    /// </summary>
    public class TrainerServiceTest : GenericServiceTest<Trainer, TrainerDto, PostTrainerDto, ITrainerRepository>
    {
        private Mock<IMapper> mockMapper;
        private Mock<ITrainerRepository> mockTrainerRepo;

        [SetUp]
        public void SetUp()
        {
            mockMapper = new Mock<IMapper>();
            mockTrainerRepo = new Mock<ITrainerRepository>();
        }

        /// <summary>
        /// Overriding the GenericServiceTestHelper method to return a TrainerService
        /// so the generic tests will be run against the TrainerService
        /// </summary>
        /// <param name="mockMapper"></param>
        /// <param name="mockRepo"></param>
        /// <returns>TrainerService</returns>
        protected override GenericService<Trainer,
            TrainerDto,
            PostTrainerDto,
            ITrainerRepository>
            GenericServiceTestHelper(Mock<IMapper> mockMapper,
            Mock<ITrainerRepository> mockRepo)
        {
            return new TrainerService(mockMapper.Object, mockRepo.Object);
        }

        /*

                #region Create

                [Test]
                public async Task Create_CallsRepoInsertAsyncOnce_WhenCalled()
                {
                    //Arrange
                    var trainer = Builder<Trainer>.CreateNew().Build();
                    PostTrainerDto postTrainerDto = Builder<PostTrainerDto>.CreateNew().Build();
                    var trainerDto = Builder<Trainer>.CreateListOfSize(1).Build();
                    mockMapper.Setup(x => x.Map<Trainer>(postTrainerDto)).Returns(trainer);
                    mockUserRepo.Setup(x => x.GetAll()).Returns(trainerDto);
                    mockTrainerRepo.Setup(x => x.InsertAsync(trainer)).ReturnsAsync(trainer);
                    var service = GenericServiceTestHelper(mockMapper, mockTrainerRepo);

                    //Act
                    await service.Create(postTrainerDto);

                    //Assert
                    mockTrainerRepo.Verify(x => x.InsertAsync(trainer), Times.Once);
                }

                [Test]
                public async Task Create_CallsTrainerRepoInsertAsyncOnceAndReturnsTrainerWithId1_WhenCalled()
                {
                    //Arrange
                    var trainer = Builder<Trainer>.CreateNew().With(x => x.Id = 0).Build();
                    var trainerDto = Builder<TrainerDto>.CreateNew().Build();
                    var postTrainerDto = Builder<PostTrainerDto>.CreateNew().Build();
                    mockMapper.Setup(x => x.Map<Trainer>(postTrainerDto)).Returns(trainer);
                    mockTrainerRepo.Setup(x => x.InsertAsync(trainer)).ReturnsAsync(trainer);
                    mockMapper.Setup(x => x.Map<TrainerDto>(trainer)).Returns(trainerDto);
                    var service = GenericServiceTestHelper(mockMapper, mockTrainerRepo);

                    //Act
                    var result = await service.Create(postTrainerDto);

                    //Assert
                    result.Should().BeEquivalentTo(trainerDto);
                }

                [Test]
                public async Task Create_CallsTrainerRepoInsertAsyncOnceAndReturnsTrainerWithId3_WhenCalled()
                {
                    //Arrange
                    var postTrainerDto = Builder<PostTrainerDto>.CreateNew().Build();
                    var trainer = Builder<Trainer>.CreateNew().Build();
                    var listOfUserRepo = Builder<User>.CreateListOfSize(2).Build();
                    var trainerDto = Builder<TrainerDto>.CreateNew().With(x => x.Id = 3).Build();
                    mockMapper.Setup(x => x.Map<Trainer>(postTrainerDto)).Returns(trainer);
                    mockUserRepo.Setup(x => x.GetAll()).Returns(listOfUserRepo);
                    mockTrainerRepo.Setup(x => x.InsertAsync(trainer)).ReturnsAsync(trainer);
                    mockMapper.Setup(x => x.Map<TrainerDto>(trainer)).Returns(trainerDto);
                    var service = GenericServiceTestHelper(mockMapper, mockTrainerRepo);

                    //Act
                    var result = await service.Create(postTrainerDto);

                    //Assert
                    result.Should().BeEquivalentTo(trainerDto);
                }

               #endregion Create

        */
    }
}