using Example5.Db;
using Example5.Enums;
using Example5.Infrastructure;
using Example5.Logging;
using Example5.Wrappers;

namespace Example5
{
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
