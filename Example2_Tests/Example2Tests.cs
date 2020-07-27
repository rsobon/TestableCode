using System.IO;
using System.Threading.Tasks;
using Example2.Command;
using Example2.Enums;
using Example2.Model;
using Example2.Reader;
using NUnit.Framework;

namespace Example2_Tests
{
    /*
     * We can test both the Entity and ImportingStatus thanks to extracting logic to isolated class
     * We can't however test the Timestamp property of Entity class and Console.WriteLine because it's tightly coupled
     */
    [TestFixture]
    public class Example2Tests
    {
        private string _json;

        [SetUp]
        public void SetUp()
        {
            _json = @"{ ""id"": 1, ""name"": ""Charmander"", ""type"": 1 }";
            File.WriteAllText("test.json", _json);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete("test.json");
        }

        [Test]
        public void PokemonReader_ShouldDeserializePokemon()
        {
            // Arrange
            var expectedPokemon = new Pokemon
            {
                Id = 1,
                Name = "Charmander",
                Type = PokemonType.Fire
            };
            var reader = new PokemonReader();

            // Act
            var result = reader.ReadPokemon(_json);

            // Assert
            Assert.AreEqual(expectedPokemon.Id, result.Id);
            Assert.AreEqual(expectedPokemon.Name, result.Name);
            Assert.AreEqual(expectedPokemon.Type, result.Type);
        }

        [Test]
        public async Task ImportPokemonCommand_ShouldReturnImportingStatusSuccess()
        {
            // Arrange
            var file = new FileInfo("test.json");
            var command = new ImportPokemonCommand();

            // Act
            var result = await command.ImportPokemon(file.FullName);

            // Assert
            Assert.AreEqual(ImportingStatus.Success, result);
        }
    }
}
