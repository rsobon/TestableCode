using Example3.Db;
using Example3.Enums;
using Example3.Infrastructure;
using Example3.Logging;
using Example3.Wrappers;

namespace Example3
{
    /*
     * Programming to interfaces
     */
    public class ImportFileCommand
    {
        private readonly IDatabase _database;
        private readonly IEntityReader _entityReader;
        private readonly ILogger _logger;
        private readonly IFileSystemWrapper _fileSystemWrapper;

        public ImportFileCommand(
            IDatabase database,
            IEntityReader entityReader,
            ILogger logger,
            IFileSystemWrapper fileSystemWrapper)
        {
            _database = database;
            _entityReader = entityReader;
            _logger = logger;
            _fileSystemWrapper = fileSystemWrapper;
        }

        public ImportingStatus ImportEntity(string filePath)
        {
            _logger.WriteLine($"Received file to import: {filePath}...");

            var fileContent = _fileSystemWrapper.ReadFile(filePath);
            var entity = _entityReader.ReadEntity(fileContent);

            _database.SaveEntity(entity);
            _logger.WriteLine("Imported entity.");

            return ImportingStatus.Success;
        }
    }
}
