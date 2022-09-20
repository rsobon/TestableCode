using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Example5.Command;
using Example5.Db;
using Example5.Enums;
using Example5.Logging;
using Example5.Model;
using Example5.Reader;
using Example5.Validation;
using Example5.Wrappers;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Example5_Tests;

[TestFixture]
public class Example5Tests
{
    private Mock<IDateTimeWrapper> _dateTimeWrapperMock;
    private Mock<IPokemonStore> _databaseMock;
    private Mock<IPokemonValidationService> _configurationMock;
    private Mock<IPokemonReader> _pokemonReaderMock;
    private Mock<ILogger> _loggerMock;
    private Mock<IFileSystemWrapper> _fileSystemWrapperMock;

    private FileInfo _testData;
    private Pokemon _expectedPokemon;

    [SetUp]
    public void SetUp()
    {
        _dateTimeWrapperMock = new Mock<IDateTimeWrapper>();
        _databaseMock = new Mock<IPokemonStore>();
        _configurationMock = new Mock<IPokemonValidationService>();
        _pokemonReaderMock = new Mock<IPokemonReader>();
        _loggerMock = new Mock<ILogger>();
        _fileSystemWrapperMock = new Mock<IFileSystemWrapper>();

        _testData = new FileInfo(@"App_Data\testdata.json");
        _expectedPokemon = new Pokemon
        {
            Id = 4,
            Name = "Charmander",
            Type = PokemonType.Fire,
            Timestamp = new DateTime(2010, 1, 1)
        };
    }

    [Test]
    public async Task PokemonReader_ShouldDeserializePokemon()
    {
        // Arrange
        _dateTimeWrapperMock.Setup(x => x.GetNow()).Returns(new DateTime(2010, 1, 1));
        _configurationMock.Setup(x => x.GetAllowedPokemonNames()).Returns(new List<string>
        {
            "Charmander"
        });
        var stream = File.OpenRead(_testData.FullName);
        var reader = new PokemonReader(_dateTimeWrapperMock.Object, _loggerMock.Object, _configurationMock.Object);

        // Act
        var result = await reader.ReadPokemon(stream);

        // Assert
        result.Should().BeEquivalentTo(_expectedPokemon);
    }

    [Test]
    public async Task PokemonReader_ShouldLogDeserializationOfPokemon()
    {
        // Arrange
        _dateTimeWrapperMock.Setup(x => x.GetNow()).Returns(new DateTime(2010, 1, 1));
        _configurationMock.Setup(x => x.GetAllowedPokemonNames()).Returns(new List<string>
        {
            "Charmander"
        });
        var stream = File.OpenRead(_testData.FullName);
        var reader = new PokemonReader(_dateTimeWrapperMock.Object, _loggerMock.Object, _configurationMock.Object);

        // Act
        await reader.ReadPokemon(stream);

        // Assert
        _loggerMock.Verify(x => x.Information($"Pokemon deserialized. Id: {_expectedPokemon.Id}, Name: {_expectedPokemon.Name}, Type: {_expectedPokemon.Type}, Timestamp: {_expectedPokemon.Timestamp}"));
    }

    [Test]
    public async Task EntityReader_ShouldLogValidationFailed()
    {
        // Arrange
        _dateTimeWrapperMock.Setup(x => x.GetNow()).Returns(new DateTime(2010, 1, 1));
        _configurationMock.Setup(x => x.GetAllowedPokemonNames()).Returns(new List<string>
        {
            "Bulbasaur"
        });
        var stream = File.OpenRead(_testData.FullName);
        var reader = new PokemonReader(_dateTimeWrapperMock.Object, _loggerMock.Object, _configurationMock.Object);

        // Act
        var act = () => reader.ReadPokemon(stream);

        // Assert
        await act.Should().ThrowAsync<InvalidDataException>().WithMessage("Validation failed!");
        _loggerMock.Verify(x => x.Information("Pokemon name: \"Charmander\" is not allowed!"));
    }

    [Test]
    public async Task ImportPokemonCommand_ShouldLogReceivedFile()
    {
        // Arrange
        var command = new ImportPokemonCommand(_loggerMock.Object, _databaseMock.Object, _pokemonReaderMock.Object, _fileSystemWrapperMock.Object);

        // Act
        await command.ImportPokemon(_testData.FullName);

        // Assert
        _loggerMock.Verify(x => x.Information($"Received pokemon to import: {_testData.FullName}..."));
    }

    [Test]
    public async Task ImportPokemonCommand_ShouldReadFile()
    {
        // Arrange
        var command = new ImportPokemonCommand(_loggerMock.Object, _databaseMock.Object, _pokemonReaderMock.Object, _fileSystemWrapperMock.Object);

        // Act
        await command.ImportPokemon(_testData.FullName);

        // Assert
        _fileSystemWrapperMock.Verify(x => x.OpenRead(_testData.FullName));
    }

    [Test]
    public async Task ImportPokemonCommand_ShouldReadPokemon()
    {
        // Arrange
        const string fakeFilePath = "fake.json";
        var stream = File.OpenRead(_testData.FullName);
        _fileSystemWrapperMock.Setup(x => x.OpenRead(fakeFilePath)).Returns(stream);
        var command = new ImportPokemonCommand(_loggerMock.Object, _databaseMock.Object, _pokemonReaderMock.Object, _fileSystemWrapperMock.Object);

        // Act
        await command.ImportPokemon(fakeFilePath);

        // Assert
        _pokemonReaderMock.Verify(x => x.ReadPokemon(stream));
    }

    [Test]
    public async Task ImportPokemonCommand_ShouldSaveDatabase()
    {
        // Arrange
        const string fakeFilePath = "fake.json";
        var stream = File.OpenRead(_testData.FullName);
        _fileSystemWrapperMock.Setup(x => x.OpenRead(fakeFilePath)).Returns(stream);
        _pokemonReaderMock.Setup(x => x.ReadPokemon(stream)).ReturnsAsync(_expectedPokemon);
        var command = new ImportPokemonCommand(_loggerMock.Object, _databaseMock.Object, _pokemonReaderMock.Object, _fileSystemWrapperMock.Object);

        // Act
        await command.ImportPokemon(fakeFilePath);

        // Assert
        _databaseMock.Verify(x => x.SavePokemon(_expectedPokemon));
    }

    [Test]
    public async Task ImportPokemonCommand_ShouldReturnImportingStatusSuccess()
    {
        // Arrange
        const string fakeFilePath = "fake.json";
        var stream = File.OpenRead(_testData.FullName);
        _fileSystemWrapperMock.Setup(x => x.OpenRead(fakeFilePath)).Returns(stream);
        _pokemonReaderMock.Setup(x => x.ReadPokemon(stream)).ReturnsAsync(_expectedPokemon);
        var command = new ImportPokemonCommand(_loggerMock.Object, _databaseMock.Object, _pokemonReaderMock.Object, _fileSystemWrapperMock.Object);

        // Act
        var result = await command.ImportPokemon(fakeFilePath);

        // Assert
        Assert.AreEqual(ImportingStatus.Success, result);
    }
}