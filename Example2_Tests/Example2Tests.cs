using System.IO;
using System.Threading.Tasks;
using Example2.Command;
using Example2.Enums;
using Example2.Model;
using Example2.Reader;
using NUnit.Framework;

namespace Example2_Tests;

[TestFixture]
public class Example2Tests
{
    private FileInfo _testData;

    [SetUp]
    public void SetUp()
    {
        _testData = new FileInfo(@"App_Data\testdata.json");
    }

    [Test]
    public async Task PokemonReader_ShouldDeserializePokemon()
    {
        // Arrange
        var expectedPokemon = new Pokemon
        {
            Id = 4,
            Name = "Charmander",
            Type = PokemonType.Fire
        };
        var reader = new PokemonReader();
        var json = await File.ReadAllTextAsync(_testData.FullName);

        // Act
        var result = reader.ReadPokemon(json);

        // Assert
        Assert.AreEqual(expectedPokemon.Id, result.Id);
        Assert.AreEqual(expectedPokemon.Name, result.Name);
        Assert.AreEqual(expectedPokemon.Type, result.Type);
    }

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