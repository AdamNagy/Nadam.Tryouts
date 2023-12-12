using Microsoft.EntityFrameworkCore;
using SQLiteDemo;

namespace SQLiteDemoTests
{
    public class SQLitetTests
    {
        [Fact]
        public void CreateContext()
        {
            var context = new SharingsContext(GetDbOptions());

            Assert.NotNull(context);
        }

        [Fact]
        public void Insert()
        {
            // Arrange
            var random = new Random();
            var minDaysToSubtracting = 30 * 365;
            var maxDaysToSubtracting = 60 * 365;

            var data = Enumerable.Range(0, 10).Select(p =>
            {
                var guid = Guid.NewGuid();
                var guidStrings = guid.ToString().Split('-');

                return new Person()
                {
                    Id = guid,
                    DOB = DateTime.Today.AddDays(random.NextInt64(minDaysToSubtracting, maxDaysToSubtracting) * -1),
                    Email = $"{guid}@gmail.com",
                    FirstName = guidStrings[0],
                    LastName = guidStrings[1],
                    Height = random.Next(160, 190),
                    Weight = random.Next()
                };
            });

            SQLitePCL.Batteries.Init();
            var context = new SharingsContext(GetDbOptions());
            var people = context.Set<Person>();

            var insertedGuids = new List<Guid>();

            // Act
            foreach (var person in data)
            {
                people.Add(person);
                insertedGuids.Add(person.Id);
            }

            context.SaveChanges();

            // Assert
            foreach (var item in people)
            {
                Assert.NotNull(insertedGuids.First(p => p == item.Id));
            }

            // Cleanup
            context.Set<Person>().RemoveRange(people);
            context.SaveChanges();
        }

        private DbContextOptions<SharingsContext> GetDbOptions()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var dbPath = Path.Join(path, "unit-test");

            return DbContextOptionFactory<SharingsContext>.GetSqliteDbOption(dbPath);
        }
    }
}