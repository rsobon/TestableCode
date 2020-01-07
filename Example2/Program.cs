using System;

namespace Example2
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var command = new ImportFileCommand();
            command.ImportEntity(@"Data\data.json");

            Console.ReadKey();
        }
    }
}
