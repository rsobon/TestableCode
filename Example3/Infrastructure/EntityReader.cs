using Example3.Logging;
using Example3.Model;
using Example3.Wrappers;
using Newtonsoft.Json;

namespace Example3.Infrastructure
{
    public class EntityReader : IEntityReader
    {
        private readonly IDateTimeWrapper _dateTimeWrapper;
        private readonly ILogger _logger;

        public EntityReader(IDateTimeWrapper dateTimeWrapper, ILogger logger)
        {
            _dateTimeWrapper = dateTimeWrapper;
            _logger = logger;
        }

        public Entity ReadEntity(string fileContent)
        {
            Entity entity = JsonConvert.DeserializeObject<Entity>(fileContent);
            entity.Timestamp = _dateTimeWrapper.GetNow();
            _logger.WriteLine($"Entity deserialized. Id: {entity.Id}, Name: {entity.Name}, Type: {entity.Type}, Timestamp: {entity.Timestamp}");

            return entity;
        }
    }
}
