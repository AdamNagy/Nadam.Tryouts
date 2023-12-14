using Moq;
using SQLiteDemo.Config;
using SQLiteDemo.DbContextService;

namespace SQLiteDemoTests
{
    public class DataContextTests
    {
        [Fact]
        public void CreateSQLiteDbContext()
        {
            // Arrange
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var dbPath = Path.Join(path, "QueryExpressionProject");

            var storageProviderMock = new Mock<IStorageProvider>();

            var sqliteConfig = new SqliteConfig()
            {
                DbFileName = "data-context-creation-db",
                DbName = "data-context-creation-db"
            };

            // Act
            var dataContext = new DataContext<TestContext>(sqliteConfig, storageProviderMock.Object, new LocalDriveStorageProvider(dbPath));
            var dbContext = dataContext.CreateContext();

            // Assert
            Assert.NotNull(dbContext);
        }
    }
}
