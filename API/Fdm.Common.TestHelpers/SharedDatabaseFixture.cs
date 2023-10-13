using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using FizzWare.NBuilder;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Fdm.Common.TestHelpers
{
    public class SharedDatabaseFixture<TContext, T> : IDisposable
   where TContext : DbContext
        where T : Model
    {
        /*Adding Foreign Key = False, this will helps to not to track any foreign key id's when adding data in the InMemory database
         https://stackoverflow.com/questions/40273184/how-to-ignore-foreign-key-constraints-in-entity-framework-core-sqlite-database */
        private const string connectionString = "Data Source = InMemorySample; Mode = Memory; Foreign Keys = False";
        private static bool databaseInitialized;
        private readonly object lockData = new();

        public SharedDatabaseFixture()
        {
            Connection = new SqliteConnection(connectionString);
            Connection.Open();
            Seed();
        }

        public DbConnection Connection { get; }

        public TContext? CreateContext(DbTransaction? transaction = null)
        {
            var context = (TContext?)Activator.CreateInstance(typeof(TContext)
                , new DbContextOptionsBuilder<TContext>()
                .UseSqlite(Connection)
                 .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options);
            if (transaction != null)
            {
                context?.Database.UseTransaction(transaction);
            }

            return context;
        }

        public void Dispose()
        {
            Connection.Dispose();
        }

        private void Seed()
        {
            lock (lockData)
            {
                if (!databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context?.Database.EnsureDeleted();
                        context?.Database.EnsureCreated();

                        var testModel = Builder<T>.CreateListOfSize(4).All()
                            .HaveNullablePropertiesSetToNull()
                            .Build();

                        context?.AddRange(testModel);

                        context?.SaveChanges();
                    }

                    databaseInitialized = true;
                }
            }
        }
    }
}