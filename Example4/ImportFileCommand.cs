using Example4.Db;
using Example4.Enums;
using Example4.Infrastructure;
using Example4.Logging;
using Example4.Wrappers;

namespace Example4
{
    /*
     * Dependency injection via IoC
     */
    public class ImportFileCommand : IImportFileCommand
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
