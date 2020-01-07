using System;
using System.IO;
using Example2.Enums;

namespace Example2
{
    /*
     * Created new class EntityReader for isolation
     * Still tightly coupled
     */
    public class ImportFileCommand
    {
        public ImportingStatus ImportEntity(string filePath)
        {
            Console.WriteLine($"Received file to import: {filePath}...");

            var fileContent = File.ReadAllText(filePath);
            var reader = new EntityReader();
            var entity = reader.ReadEntity(fileContent);

            var database = new Database();
            database.SaveEntity(entity);

            Console.WriteLine("Imported entity.");

            return ImportingStatus.Success;
        }
    }
}
