using System;
using Newtonsoft.Json;

namespace Example2
{
    public class EntityReader
    {
        public Entity ReadEntity(string fileContent)
        {
            Entity entity = JsonConvert.DeserializeObject<Entity>(fileContent);
            entity.Timestamp = DateTime.Now;
            Console.WriteLine($"Entity deserialized. Id: {entity.Id}, Name: {entity.Name}, Type: {entity.Type}, Timestamp: {entity.Timestamp}");
            return entity;
        }
    }
}
