using Example6;
using Example6.Command;
using Example6.Db;
using Example6.Enums;
using Example6.Model;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Example6_Tests
{
    public class Tests
    {
        private IHost _app;
        private string _testDirectory;

        [SetUp]
        public void Setup()
        {
            var builder = Host.CreateDefaultBuilder();

            // long lasting sqllite database
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            builder.ConfigureServices(services =>
            {
                services.RegisterServices();
                services.AddDbContext<PokemonDbContext>(options => options.UseSqlite(connection));
            });
            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<PokemonDbContext>();
            db.Database.EnsureCreated();
            _app = app;

            _testDirectory = Guid.NewGuid().ToString();
            Directory.CreateDirectory(_testDirectory);
        }

        [TearDown]
        public void Teardown()
        {
            Directory.Delete(_testDirectory, true);
        }

        [Test]
        public async Task Test_Correct_Data()
        {
            // Arrange
            var expected = new List<Pokemon>
            {
                new ()
                {
                    Id = 1,
                    Name = "Bulbasaur",
                    Type = PokemonType.Grass
                },
                new ()
                {
                    Id = 4,
                    Name = "Charmander",
                    Type = PokemonType.Fire
                },
                new ()
                {
                    Id = 7,
                    Name = "Squirtle",
                    Type = PokemonType.Water
                }

            };

            using var scope = _app.Services.CreateScope();
            var command = scope.ServiceProvider.GetRequiredService<IImportPokemonCommand>();

            // Act
            File.Copy(@"Test_Data\correct_data.json", Path.Combine(_testDirectory, "correct_data.json"));
            await command.ImportFiles(_testDirectory, CancellationToken.None);

            // Assert
            var db = scope.ServiceProvider.GetRequiredService<PokemonDbContext>();
            var pokemon = await db.Pokemon.ToListAsync();
            pokemon.Should().BeEquivalentTo(expected,
                opt => opt
                    .Excluding(x => x.ObjectId)
                    .Excluding(x => x.Timestamp));
        }

        [Test]
        public async Task Test_Invalid_Data()
        {
            // Arrange
            var expected = new List<Pokemon>();
            using var scope = _app.Services.CreateScope();
            var command = scope.ServiceProvider.GetRequiredService<IImportPokemonCommand>();
            
            // Act
            File.Copy(@"Test_Data\invalid_data.json", Path.Combine(_testDirectory, "invalid_data.json"));
            await command.ImportFiles(_testDirectory, CancellationToken.None);

            // Assert
            var db = scope.ServiceProvider.GetRequiredService<PokemonDbContext>();
            var pokemon = await db.Pokemon.ToListAsync();
            pokemon.Should().BeEquivalentTo(expected,
                opt => opt
                    .Excluding(x => x.ObjectId)
                    .Excluding(x => x.Timestamp));
        }
    }
}