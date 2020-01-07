using System.IO;
using Example1.Enums;
using NUnit.Framework;

namespace Example1.Tests
{
    /*
    * We can only test ImportingStatus result
    */
    [TestFixture]
    public class Example1Tests
    {
        [Test]
        public void ImportFileCommand_ShouldReturnImportingStatusSuccess()
        {
            // Arrange
            string json = @"{ ""id"": 1, ""name"": ""Test Name"", ""type"": 1 }";
            File.WriteAllText(@"C:\test.json", json);
            var file = new FileInfo(@"C:\test.json");
            var command = new ImportFileCommand();

            // Act
            var result = command.ImportEntity(file.FullName);

            // Assert
            Assert.AreEqual(ImportingStatus.Success, result);
        }
    }
}
