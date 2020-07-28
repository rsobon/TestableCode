using System;
using System.Threading.Tasks;
using Example3.Command;
using Example3.Db;
using Example3.Enums;
using Example3.Logging;
using Example3.Model;
using Example3.Reader;
using Example3.Wrappers;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Example3_Tests
{
    /*
     * Tests no longer require to write physical file to hard drive.
     * Instead mocks can be set up to return what we need.
     */
    [TestFixture]
    public class Example3Tests
    {
        private Mock<IDateTimeWrapper> _dateTimeWrapperMock;
        private Mock<IPokemonStore> _databaseMock;
        private Mock<IPokemonReader> _pokemonReaderMock;
        private Mock<ILogger> _loggerMock;
        private Mock<IFileSystemWrapper> _fileSystemWrapperMock;

        private string _json;
        private Pokemon _expectedPokemon;

        private const string FakeFilePath = "test.json";

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
            var reader = new PokemonReader(_dateTimeWrapperMock.Object, _loggerMock.Object);

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
            var reader = new PokemonReader(_dateTimeWrapperMock.Object, _loggerMock.Object);

            // Act
            reader.ReadPokemon(_json);

            // Assert
            _loggerMock.Verify(x => x.Information($"Pokemon deserialized. Id: {_expectedPokemon.Id}, Name: {_expectedPokemon.Name}, Type: {_expectedPokemon.Type}, Timestamp: {_expectedPokemon.Timestamp}"));
        }

        [Test]
        public async Task ImportPokemonCommand_ShouldLogReceivedFile()
        {
            // Arrange
            var command = new ImportPokemonCommand( _loggerMock.Object, _databaseMock.Object, _pokemonReaderMock.Object, _fileSystemWrapperMock.Object);

            // Act
            await command.ImportPokemon(FakeFilePath);

            // Assert
            _loggerMock.Verify(x => x.Information($"Received pokemon to import: {FakeFilePath}..."));
        }

        [Test]
        public async Task ImportPokemonCommand_ShouldReadFile()
        {
            // Arrange
            var command = new ImportPokemonCommand( _loggerMock.Object, _databaseMock.Object, _pokemonReaderMock.Object, _fileSystemWrapperMock.Object);

            // Act
            await command.ImportPokemon(FakeFilePath);

            // Assert
            _fileSystemWrapperMock.Verify(x => x.ReadFile(FakeFilePath));
        }

        [Test]
        public async Task ImportPokemonCommand_ShouldReadPokemon()
        {
            // Arrange
            var command = new ImportPokemonCommand( _loggerMock.Object, _databaseMock.Object, _pokemonReaderMock.Object, _fileSystemWrapperMock.Object);

            _fileSystemWrapperMock.Setup(x => x.ReadFile(FakeFilePath)).Returns(_json);

            // Act
            await command.ImportPokemon(FakeFilePath);

            // Assert
            _pokemonReaderMock.Verify(x => x.ReadPokemon(_json));
        }

        [Test]
        public async Task ImportPokemonCommand_ShouldSaveDatabase()
        {
            // Arrange
            var command = new ImportPokemonCommand( _loggerMock.Object, _databaseMock.Object, _pokemonReaderMock.Object, _fileSystemWrapperMock.Object);

            _fileSystemWrapperMock.Setup(x => x.ReadFile(FakeFilePath)).Returns(_json);
            _pokemonReaderMock.Setup(x => x.ReadPokemon(_json)).Returns(_expectedPokemon);

            // Act
            await command.ImportPokemon(FakeFilePath);

            // Assert
            _databaseMock.Verify(x => x.SavePokemon(_expectedPokemon));
        }

        [Test]
        public async Task ImportPokemonCommand_ShouldReturnImportingStatusSuccess()
        {
            // Arrange
            var command = new ImportPokemonCommand( _loggerMock.Object, _databaseMock.Object, _pokemonReaderMock.Object, _fileSystemWrapperMock.Object);

            _fileSystemWrapperMock.Setup(x => x.ReadFile(FakeFilePath)).Returns(_json);
            _pokemonReaderMock.Setup(x => x.ReadPokemon(_json)).Returns(_expectedPokemon);

            // Act
            var result = await command.ImportPokemon(FakeFilePath);

            // Assert
            Assert.AreEqual(ImportingStatus.Success, result);
        }
    }
}
