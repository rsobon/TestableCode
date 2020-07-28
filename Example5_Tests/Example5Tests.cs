using System;
using System.Threading.Tasks;
using Example5.Command;
using Example5.Db;
using Example5.Enums;
using Example5.Logging;
using Example5.Model;
using Example5.Reader;
using Example5.Wrappers;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Example5_Tests
{
    /*
     * New test for validation logic.
     * If validation would be extracted to separate class (e.g. ValidationService) we could do even more tests.
     */
    [TestFixture]
    public class Example4Tests
    {
        private Mock<IDateTimeWrapper> _dateTimeWrapperMock;
        private Mock<IPokemonStore> _databaseMock;
        private Mock<IPokemonReader> _pokemonReaderMock;
        private Mock<ILogger> _loggerMock;
        private Mock<IFileSystemWrapper> _fileSystemWrapperMock;

        private string _json;
        private Pokemon _expectedPokemon;

        private const string FilePath = "test.json";

        [SetUp]
        public void SetUp()
        {
            _dateTimeWrapperMock = new Mock<IDateTimeWrapper>();
            _databaseMock = new Mock<IPokemonStore>();
            _pokemonReaderMock = new Mock<IPokemonReader>();
            _loggerMock = new Mock<ILogger>();
            _fileSystemWrapperMock = new Mock<IFileSystemWrapper>();

            _json = @"{ ""id"": 1, ""name"": ""Charmander"", ""type"": 1 }";
            _expectedPokemon = new Pokemon
            {
                Id = 1,
                Name = "Charmander",
                Type = PokemonType.Fire,
                Timestamp = new DateTime(2010, 1, 1)
            };
        }

        [Test]
        public void PokemonReader_ShouldDeserializePokemon()
        {
            // Arrange
            _dateTimeWrapperMock.Setup(x => x.GetNow()).Returns(new DateTime(2010, 1, 1));
            var reader = new PokemonReader(_dateTimeWrapperMock.Object, _loggerMock.Object, _databaseMock.Object);

            // Act
            var result = reader.ReadPokemon(_json);

            // Assert
            result.Should().BeEquivalentTo(_expectedPokemon);
        }

        [Test]
        public void PokemonReader_ShouldLogDeserializationOfPokemon()
        {
            // Arrange
            _dateTimeWrapperMock.Setup(x => x.GetNow()).Returns(new DateTime(2010, 1, 1));
            var reader = new PokemonReader(_dateTimeWrapperMock.Object, _loggerMock.Object, _databaseMock.Object);

            // Act
            reader.ReadPokemon(_json);

            // Assert
            _loggerMock.Verify(x => x.Information($"Pokemon deserialized. Id: {_expectedPokemon.Id}, Name: {_expectedPokemon.Name}, Type: {_expectedPokemon.Type}, Timestamp: {_expectedPokemon.Timestamp}"));
        }

        [Test]
        public void EntityReader_ShouldLogValidationResult()
        {
            // Arrange
            _dateTimeWrapperMock.Setup(x => x.GetNow()).Returns(new DateTime(2010, 1, 1));
            _databaseMock.Setup(x => x.IsValidationEnabled()).Returns(true);
            var reader = new PokemonReader(_dateTimeWrapperMock.Object, _loggerMock.Object, _databaseMock.Object);

            // Act
            reader.ReadPokemon(_json);

            // Assert
            _loggerMock.Verify(x => x.Information($"Validation result: {true}"));
        }

        [Test]
        public async Task ImportPokemonCommand_ShouldLogReceivedFile()
        {
            // Arrange
            var command = new ImportPokemonCommand( _loggerMock.Object, _databaseMock.Object, _pokemonReaderMock.Object, _fileSystemWrapperMock.Object);

            // Act
            await command.ImportPokemon(FilePath);

            // Assert
            _loggerMock.Verify(x => x.Information($"Received pokemon to import: {FilePath}..."));
        }

        [Test]
        public async Task ImportPokemonCommand_ShouldReadFile()
        {
            // Arrange
            var command = new ImportPokemonCommand( _loggerMock.Object, _databaseMock.Object, _pokemonReaderMock.Object, _fileSystemWrapperMock.Object);

            // Act
            await command.ImportPokemon(FilePath);

            // Assert
            _fileSystemWrapperMock.Verify(x => x.ReadFile(FilePath));
        }

        [Test]
        public async Task ImportPokemonCommand_ShouldReadPokemon()
        {
            // Arrange
            var command = new ImportPokemonCommand( _loggerMock.Object, _databaseMock.Object, _pokemonReaderMock.Object, _fileSystemWrapperMock.Object);

            _fileSystemWrapperMock.Setup(x => x.ReadFile(FilePath)).Returns(_json);

            // Act
            await command.ImportPokemon(FilePath);

            // Assert
            _pokemonReaderMock.Verify(x => x.ReadPokemon(_json));
        }

        [Test]
        public async Task ImportPokemonCommand_ShouldSaveDatabase()
        {
            // Arrange
            var command = new ImportPokemonCommand( _loggerMock.Object, _databaseMock.Object, _pokemonReaderMock.Object, _fileSystemWrapperMock.Object);

            _fileSystemWrapperMock.Setup(x => x.ReadFile(FilePath)).Returns(_json);
            _pokemonReaderMock.Setup(x => x.ReadPokemon(_json)).Returns(_expectedPokemon);

            // Act
            await command.ImportPokemon(FilePath);

            // Assert
            _databaseMock.Verify(x => x.SavePokemon(_expectedPokemon));
        }

        [Test]
        public async Task ImportPokemonCommand_ShouldReturnImportingStatusSuccess()
        {
            // Arrange
            var command = new ImportPokemonCommand( _loggerMock.Object, _databaseMock.Object, _pokemonReaderMock.Object, _fileSystemWrapperMock.Object);

            _fileSystemWrapperMock.Setup(x => x.ReadFile(FilePath)).Returns(_json);
            _pokemonReaderMock.Setup(x => x.ReadPokemon(_json)).Returns(_expectedPokemon);

            // Act
            var result = await command.ImportPokemon(FilePath);

            // Assert
            Assert.AreEqual(ImportingStatus.Success, result);
        }
    }
}
