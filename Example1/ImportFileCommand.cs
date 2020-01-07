using System;
using System.IO;
using Example1.Enums;
using Newtonsoft.Json;

namespace Example1
{
    /*
     * Tight coupling:
     * - static classes and methods
     * - framework classes and methods
     * - new keyword
     */
    public class ImportFileCommand
    {
        public ImportingStatus ImportEntity(string filePath)
        {
            Console.WriteLine($"Received file to import: {filePath}..."); // usage of static method

            var fileContent = File.ReadAllText(filePath);
            Entity entity = JsonConvert.DeserializeObject<Entity>(fileContent); // usage of framework method
            entity.Timestamp = DateTime.Now;

            var database = new Database(); // new keyword
            database.SaveEntity(entity);

            Console.WriteLine($"Entity saved. Id: {entity.Id}, Name: {entity.Name}, Type: {entity.Type}, Timestamp: {entity.Timestamp}");
            Console.WriteLine("Imported entity.");

            return ImportingStatus.Success;
        }
    }
}
