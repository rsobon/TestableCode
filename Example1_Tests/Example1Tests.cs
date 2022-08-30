using System.IO;
using System.Threading.Tasks;
using Example1.Command;
using Example1.Enums;
using NUnit.Framework;

namespace Example1_Tests;

[TestFixture]
public class Example1Tests
{
    [Test]
    public async Task ImportPokemonCommand_ShouldReturnImportingStatusSuccess()
    {
        // Arrange
        var file = new FileInfo(@"App_Data\testdata.json");
        var command = new ImportPokemonCommand();

        // Act
        var result = await command.ImportPokemon(file.FullName);

        // Assert
        Assert.AreEqual(ImportingStatus.Success, result);
    }

    [Test]
    public async Task ImportPokemonCommand_ShouldReturnImportingStatusError_WhenMissingFile()
    {
        // Arrange
        var file = new FileInfo("test.json");
        var command = new ImportPokemonCommand();

        // Act
        var result = await command.ImportPokemon(file.FullName);

        // Assert
        Assert.AreEqual(ImportingStatus.Error, result);
    }
}