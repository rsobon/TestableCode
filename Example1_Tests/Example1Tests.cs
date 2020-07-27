using System.IO;
using System.Threading.Tasks;
using Example1.Command;
using Example1.Enums;
using NUnit.Framework;

namespace Example1_Tests
{
    /*
    * We can only test ImportingStatus result
    */
    [TestFixture]
    public class Example1Tests
    {
        [Test]
        public async Task ImportPokemonCommand_ShouldReturnImportingStatusSuccess()
        {
            // Arrange
            string json = @"{ ""id"": 1, ""name"": ""Charmander"", ""type"": 1 }";
            File.WriteAllText("test.json", json);
            var file = new FileInfo("test.json");
            var command = new ImportPokemonCommand();

            // Act
            var result = await command.ImportPokemon(file.FullName);
            File.Delete("test.json");

            // Assert
            Assert.AreEqual(ImportingStatus.Success, result);
        }

        [Test]
        public async Task ImportPokemonCommand_ShouldReturnImportingStatusError_WhenMissingFile()
        {
            // Arrange
            var file = new FileInfo("dummy_file.json");
            var command = new ImportPokemonCommand();

            // Act
            var result = await command.ImportPokemon(file.FullName);

            // Assert
            Assert.AreEqual(ImportingStatus.Error, result);
        }
    }
}
