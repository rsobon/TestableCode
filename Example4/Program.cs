using System;
using Example4.Db;
using Example4.Infrastructure;
using Example4.Logging;
using Example4.Wrappers;
using Unity;

namespace Example4
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.RegisterType<IDatabase, Database>();
            container.RegisterType<IEntityReader, EntityReader>();
            container.RegisterType<IDateTimeWrapper, DateTimeWrapper>();
            container.RegisterType<ILogger, Logger>();
            container.RegisterType<IFileSystemWrapper, FileSystemWrapper>();
            container.RegisterType<IImportFileCommand, ImportFileCommand>();

            var command = container.Resolve<IImportFileCommand>();
            command.ImportEntity(@"Data\data.json");

            Console.ReadKey();
        }
    }
}
