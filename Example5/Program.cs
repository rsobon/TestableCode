using System;
using Example5.Db;
using Example5.Infrastructure;
using Example5.Logging;
using Example5.Wrappers;
using Unity;

namespace Example5
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
