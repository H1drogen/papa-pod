using Fdm.Common.Repository;
using Fdm.Common.TestHelpers;
using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Fdm.Common.Tests.Repositories
{
    public abstract class GenericRepositoryTests<TContext, T>
         : IClassFixture<SharedDatabaseFixture<TContext, T>>
        where T : Model
        where TContext : DbContext
    {
        protected GenericRepositoryTests(SharedDatabaseFixture<TContext, T> fixture)
        {
            Fixture = fixture;
        }

        public SharedDatabaseFixture<TContext, T> Fixture { get; }

        #region GetAll

        [Fact]
        public async Task GetAllAsync_ShouldCheckReturnTypeIsListOfT_WhenCalled()
        {
            using (var context = Fixture.CreateContext())
            {
                //Arrange
                var repo = CreateRepository(context);
                //Act
                var result = await repo.GetAllAsync();
                //Assert
                result.Should().BeOfType<List<T>>();
            }
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnTheCorrectNumberOfGenericTestModels_WhenCalled()
        {
            using (var context = Fixture.CreateContext())
            {
                //Arrange
                var listLength = 4;
                AddDataToDbContext<T>(context, listLength);
                var repo = CreateRepository(context);

                //Act
                var actualGenericTestModels = (await repo.GetAllAsync()).Count();
                //Assert
                actualGenericTestModels.Should().Be(listLength);
            }
        }

        #endregion GetAll

        #region GetById

        [Fact]
        public async Task GetById_ReturnsGenericTestModel_WhenCalled()

        {
            using (var context = Fixture.CreateContext())
            {
                //Arrange
                var repo = CreateRepository(context);
                //Act
                var returned = await repo.GetByIdAsync(1);
                //Assert
                returned.Should().BeOfType<T>();
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetById_ReturnsGenericTestModelWithCorrectId_WhenCalled(int modelId)
        {
            using (var context = Fixture.CreateContext())
            {
                //Arrange
                var repo = CreateRepository(context);

                //Act
                var result = await repo.GetByIdAsync(modelId);

                //Assert
                result.Id.Should().Be(modelId);
            }
        }

        #endregion GetById

        #region Insert

        [Fact]
        public async Task InsertAsync_InsertsGenericTestModel_WhenCalled()
        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                using (var context = Fixture.CreateContext(transaction))
                {
                    //Arrange
                    var modelToAdd = CreateGenericTestModel(5);
                    var repo = CreateRepository(context);
                    CheckGenericTestModelDoesNotExistInDb(context, modelToAdd.Id);

                    //Act
                    await repo.InsertAsync(modelToAdd);

                    //Assert
                    context.Set<T>().Find(modelToAdd.Id).Should().BeEquivalentTo(modelToAdd);
                }
            }
        }

        [Fact]
        public async Task InsertAsync_ReturnsGenericTestModel_WhenCalled()

        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                using (var context = Fixture.CreateContext(transaction))
                {
                    //Arrange
                    var modelToAdd = CreateGenericTestModel(5);
                    var repo = CreateRepository(context);
                    CheckGenericTestModelDoesNotExistInDb(context, modelToAdd.Id);

                    //Act
                    var returned = await repo.InsertAsync(modelToAdd);
                    //Assert
                    returned.Should().BeEquivalentTo(modelToAdd);
                }
            }
        }

        [Fact]
        public async Task InsertAsync_ReturnsGenericTestModelType_WhenCalled()

        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                using (var context = Fixture.CreateContext(transaction))
                {
                    //Arrange
                    var modelToAdd = CreateGenericTestModel(5);
                    var repo = CreateRepository(context);
                    CheckGenericTestModelDoesNotExistInDb(context, modelToAdd.Id);

                    //Act
                    var returned = await repo.InsertAsync(modelToAdd);
                    //Assert
                    returned.Should().BeOfType<T>();
                }
            }
        }

        #endregion Insert

        #region Delete

        [Fact]
        public async Task Delete_DeletesandPersistsGenericTestModel_WhenCalled()
        {
            T modelToDelete;
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                using (var context = Fixture.CreateContext(transaction))
                {
                    //Arrange
                    modelToDelete = CreateGenericTestModel();
                    var repo = CreateRepository(context);
                    await CheckGenericTestModelExistsInDbWithoutTrackingEntity(modelToDelete, context);

                    //Act
                    await repo.Delete(modelToDelete.Id);
                }
                // Accessing a new context instance to ensure the update to the Model is persisted to the database and not just in memory
                using (var context = Fixture.CreateContext(transaction))
                {
                    //Assert
                    context.Set<T>().Should().NotContain(modelToDelete);
                }
            }
        }

        [Fact]
        public async Task Delete_DeletesGenericTestModel_WhenCalled()
        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                using (var context = Fixture.CreateContext(transaction))
                {
                    //Arrange
                    var modelToDelete = CreateGenericTestModel();
                    var repo = CreateRepository(context);
                    await CheckGenericTestModelExistsInDbWithoutTrackingEntity(modelToDelete, context);

                    //Act
                    await repo.Delete(modelToDelete.Id);

                    //Assert
                    context.Set<T>().Should().NotContain(modelToDelete);
                }
            }
        }

        #endregion Delete

        #region DoesExist

        [Theory]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public async Task DoesExist_ReturnFalse_WhenGenericTestModelIsNotInDB(int modelId)
        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                using (var context = Fixture.CreateContext(transaction))
                {
                    //Arrange
                    var repo = CreateRepository(context);
                    CheckGenericTestModelDoesNotExistInDb(context, modelId);

                    //Act
                    var result = await repo.DoesExistAsync(modelId);

                    //Assert
                    result.Should().Be(false);
                }
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task DoesExist_ReturnTrue_WhenGenericTestModelIsInDB(int modelId)
        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                using (var context = Fixture.CreateContext(transaction))
                {
                    //Arrange
                    var repo = CreateRepository(context);
                    await CheckGenericTestModelDoesExistInDb(modelId, context);

                    //Act
                    var result = await repo.DoesExistAsync(modelId);

                    //Assert
                    result.Should().Be(true);
                }
            }
        }

        #endregion DoesExist

        #region Update

        [Fact]
        public async Task Update_UpdatesAndPersistsGenericTestModel_WhenCalled()
        {
            T modelToUpdate;
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                using (var context = Fixture.CreateContext(transaction))
                {
                    //Arrange
                    modelToUpdate = CreateGenericTestModel();
                    var repo = CreateRepository(context);
                    await CheckExistingGenericTestModelDoesNotMatchUpdatedGenericTestModelInDbWithoutTrackingEntity(modelToUpdate, context);

                    //Act
                    repo.Update(modelToUpdate);

                    //Assert
                    context.Set<T>().Find(modelToUpdate.Id).Should().BeEquivalentTo(modelToUpdate);
                }
            }
        }

        [Fact]
        public async Task Update_UpdatesGenericTestModel_WhenCalled()
        {
            using (var transaction = Fixture.Connection.BeginTransaction())
            {
                using (var context = Fixture.CreateContext(transaction))
                {
                    //Arrange
                    var modelToUpdate = CreateGenericTestModel();
                    var repo = CreateRepository(context);
                    await CheckExistingGenericTestModelDoesNotMatchUpdatedGenericTestModelInDbWithoutTrackingEntity(modelToUpdate, context);

                    //Act
                    repo.Update(modelToUpdate);

                    //Assert
                    context.Set<T>().Find(modelToUpdate.Id).Should().BeEquivalentTo(modelToUpdate);
                }
            }
        }

        #endregion Update

        #region Test Helper Methods

        protected virtual void AddDataToDbContext<T>(TContext context, int amount = 3) where T : Model
        {
            var genericList = Builder<T>.CreateListOfSize(amount)
                    .All()
                    .Build();

            context.AddRangeAsync(genericList);
            context.SaveChangesAsync();
        }

        private async Task CheckExistingGenericTestModelDoesNotMatchUpdatedGenericTestModelInDbWithoutTrackingEntity(T model, TContext context)
        {
            //Checking that the T in the database does not already match the updated value
            //so it does not return a false positive
            //using AsNoTracking to prevent the returned entity from being attached to the context
            //as this is a test condition and should not impact the Act
            context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id).Should().NotBeEquivalentTo(model);
        }

        private async Task CheckGenericTestModelDoesExistInDb(int modelId, TContext context)
        {
            //Checking that the T in the database does exist so we get a better error
            //message if the data changes
            context.Set<T>().Find(modelId).Should().NotBeNull();
        }

        private void CheckGenericTestModelDoesNotExistInDb(TContext context, int modelId)
        {
            //Checking the model wanting to be added is not already seeded in the database
            //means the test won't pass falsely
            context.Set<T>().Find(modelId).Should().BeNull();
        }

        private async Task CheckGenericTestModelExistsInDbWithoutTrackingEntity(T model, TContext context)
        {
            //Checking that the T in the database does not already match the deleted value
            //so it does not return a false positive
            //using AsNoTracking to prevent the returned entity from being attached to the context
            //as this is a test condition and should not impact the Act
            context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id).Should().NotBeNull();
        }

        private T CreateGenericTestModel(int id = 1)
        {
            var list = Builder<T>.CreateListOfSize(6).Build();
            return list[id];
        }

        #endregion Test Helper Methods

        protected abstract GenericRepository<TContext, T> CreateRepository(TContext context);
    }
}