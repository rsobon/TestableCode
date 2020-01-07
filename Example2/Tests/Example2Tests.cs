using System.IO;
using Example2.Enums;
using NUnit.Framework;

namespace Example2.Tests
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
            _json = @"{ ""id"": 1, ""name"": ""Test Name"", ""type"": 1 }";
        }

        [Test]
        public void EntityReader_ShouldDeserializeEntity()
        {
            // Arrange
            var expectedEntity = new Entity
            {
                Id = 1,
                Name = "Test Name",
                Type = EntityType.NotAwesomeType
            };
            var reader = new EntityReader();

            // Act
            var result = reader.ReadEntity(_json);

            // Assert
            Assert.AreEqual(expectedEntity.Id, result.Id);
            Assert.AreEqual(expectedEntity.Name, result.Name);
            Assert.AreEqual(expectedEntity.Type, result.Type);
        }

        [Test]
        public void ImportFileCommand_ShouldReturnImportingStatusSuccess()
        {
            // Arrange
            // Arrange
            File.WriteAllText(@"C:\test.json", _json);
            var command = new ImportFileCommand();

            // Act
            var result = command.ImportEntity(@"C:\test.json");

            // Assert
            Assert.AreEqual(ImportingStatus.Success, result);
        }
    }
}
