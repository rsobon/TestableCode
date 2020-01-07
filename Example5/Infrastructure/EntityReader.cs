using System.IO;
using Example5.Db;
using Example5.Logging;
using Example5.Model;
using Example5.Wrappers;
using Newtonsoft.Json;

namespace Example5.Infrastructure
{
    /*
     * Logic in constructor
     * Requires to do more setup for tests
     */
    public class EntityReader : IEntityReader
    {
        private readonly IDateTimeWrapper _dateTimeWrapper;
        private readonly ILogger _logger;

        private readonly bool _isValidationEnabled;

        public EntityReader(IDateTimeWrapper dateTimeWrapper, ILogger logger, IDatabase database)
        {
            _dateTimeWrapper = dateTimeWrapper;
            _logger = logger;

            _isValidationEnabled = database.IsValidationEnabled();
        }

        public Entity ReadEntity(string fileContent)
        {
            Entity entity = JsonConvert.DeserializeObject<Entity>(fileContent);

            if (_isValidationEnabled)
            {
                var validationResult = IsValid(entity);
                _logger.WriteLine($"Validation result: {validationResult}");
                if (!validationResult)
                {
                    throw new InvalidDataException();
                }
            }

            entity.Timestamp = _dateTimeWrapper.GetNow();
            _logger.WriteLine($"Entity deserialized. Id: {entity.Id}, Name: {entity.Name}, Type: {entity.Type}, Timestamp: {entity.Timestamp}");

            return entity;
        }

        private bool IsValid(Entity entity)
        {
            _logger.WriteLine("Performing validation...");

            if (string.IsNullOrEmpty(entity.Name))
            {
                _logger.WriteLine($"{entity.Name} is null or empty!");
                return false;
            }

            return true;
        }
    }
}
