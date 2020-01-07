using System;
using Example5.Db;
using Example5.Enums;
using Example5.Infrastructure;
using Example5.Logging;
using Example5.Model;
using Example5.Wrappers;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Example5.Tests
{
    [TestFixture]
    public class Example5Tests
    {
        private Mock<IDateTimeWrapper> _dateTimeWrapperMock;
        private Mock<IDatabase> _databaseMock;
        private Mock<IEntityReader> _entityReaderMock;
        private Mock<ILogger> _loggerMock;
        private Mock<IFileSystemWrapper> _fileSystemWrapperMock;

        private string _json;
        private Entity _expectedEntity;

        [SetUp]
        public void SetUp()
        {
            _dateTimeWrapperMock = new Mock<IDateTimeWrapper>();
            _databaseMock = new Mock<IDatabase>();
            _entityReaderMock = new Mock<IEntityReader>();
            _loggerMock = new Mock<ILogger>();
            _fileSystemWrapperMock = new Mock<IFileSystemWrapper>();

            _json = @"{ ""id"": 1, ""name"": ""Test Name"", ""type"": 1 }";
            _expectedEntity = new Entity
            {
                Id = 1,
                Name = "Test Name",
                Type = EntityType.NotAwesomeType,
                Timestamp = new DateTime(2010, 1, 1)
            };
        }

        [Test]
        public void EntityReader_ShouldDeserializeEntity()
        {
            // Arrange
            _dateTimeWrapperMock.Setup(x => x.GetNow()).Returns(new DateTime(2010, 1, 1));
            var reader = new EntityReader(_dateTimeWrapperMock.Object, _loggerMock.Object, _databaseMock.Object);

            // Act
            var result = reader.ReadEntity(_json);

            // Assert
            result.Should().BeEquivalentTo(_expectedEntity);
        }

        [Test]
        public void EntityReader_ShouldLogDeserializationOfEntity()
        {
            // Arrange
            _dateTimeWrapperMock.Setup(x => x.GetNow()).Returns(new DateTime(2010, 1, 1));
            var reader = new EntityReader(_dateTimeWrapperMock.Object, _loggerMock.Object, _databaseMock.Object);

            // Act
            reader.ReadEntity(_json);

            // Assert
            _loggerMock.Verify(x => x.WriteLine($"Entity deserialized. Id: {_expectedEntity.Id}, Name: {_expectedEntity.Name}, Type: {_expectedEntity.Type}, Timestamp: {_expectedEntity.Timestamp}"));
        }

        [Test]
        public void EntityReader_ShouldPassValidation()
        {
            // Arrange
            _dateTimeWrapperMock.Setup(x => x.GetNow()).Returns(new DateTime(2010, 1, 1));
            _databaseMock.Setup(x => x.IsValidationEnabled()).Returns(true);
            var reader = new EntityReader(_dateTimeWrapperMock.Object, _loggerMock.Object, _databaseMock.Object);

            // Act
            reader.ReadEntity(_json);

            // Assert
            _loggerMock.Verify(x => x.WriteLine($"Validation result: {true}"));
        }

        [Test]
        public void ImportFileCommand_ShouldLogReceivedFile()
        {
            // Arrange
            var filePath = @"C:\test.json";
            var command = new ImportFileCommand(_databaseMock.Object, _entityReaderMock.Object, _loggerMock.Object, _fileSystemWrapperMock.Object);

            // Act
            command.ImportEntity(@"C:\test.json");

            // Assert
            _loggerMock.Verify(x => x.WriteLine($"Received file to import: {filePath}..."));
        }

        [Test]
        public void ImportFileCommand_ShouldReadFile()
        {
            // Arrange
            var filePath = @"C:\test.json";
            var command = new ImportFileCommand(_databaseMock.Object, _entityReaderMock.Object, _loggerMock.Object, _fileSystemWrapperMock.Object);

            // Act
            command.ImportEntity(@"C:\test.json");

            // Assert
            _fileSystemWrapperMock.Verify(x => x.ReadFile(filePath));
        }

        [Test]
        public void ImportFileCommand_ShouldReadEntity()
        {
            // Arrange
            var filePath = @"C:\test.json";
            var command = new ImportFileCommand(_databaseMock.Object, _entityReaderMock.Object, _loggerMock.Object, _fileSystemWrapperMock.Object);

            _fileSystemWrapperMock.Setup(x => x.ReadFile(filePath)).Returns(_json);

            // Act
            command.ImportEntity(@"C:\test.json");

            // Assert
            _entityReaderMock.Verify(x => x.ReadEntity(_json));
        }

        [Test]
        public void ImportFileCommand_ShouldSaveDatabase()
        {
            // Arrange
            var filePath = @"C:\test.json";
            var command = new ImportFileCommand(_databaseMock.Object, _entityReaderMock.Object, _loggerMock.Object, _fileSystemWrapperMock.Object);

            _fileSystemWrapperMock.Setup(x => x.ReadFile(filePath)).Returns(_json);
            _entityReaderMock.Setup(x => x.ReadEntity(_json)).Returns(_expectedEntity);

            // Act
            command.ImportEntity(@"C:\test.json");

            // Assert
            _databaseMock.Verify(x => x.SaveEntity(_expectedEntity));
        }

        [Test]
        public void ImportFileCommand_ShouldReturnImportingStatusSuccess()
        {
            // Arrange
            var filePath = @"C:\test.json";
            var command = new ImportFileCommand(_databaseMock.Object, _entityReaderMock.Object, _loggerMock.Object, _fileSystemWrapperMock.Object);

            _fileSystemWrapperMock.Setup(x => x.ReadFile(filePath)).Returns(_json);
            _entityReaderMock.Setup(x => x.ReadEntity(_json)).Returns(_expectedEntity);

            // Act
            var result = command.ImportEntity(@"C:\test.json");

            // Assert
            Assert.AreEqual(ImportingStatus.Success, result);
        }
    }
}
