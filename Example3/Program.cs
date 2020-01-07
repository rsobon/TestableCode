using System;
using Example3.Db;
using Example3.Infrastructure;
using Example3.Logging;
using Example3.Wrappers;

namespace Example3
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var command = new ImportFileCommand(
                new Database(),
                new EntityReader(
                    new DateTimeWrapper(),
                    new Logger()),
                new Logger(),
                new FileSystemWrapper());
            command.ImportEntity(@"Data\data.json");

            Console.ReadKey();
        }
    }
}
