using Example4.Logging;
using Example4.Model;
using Example4.Wrappers;
using Newtonsoft.Json;

namespace Example4.Reader
{
    public class PokemonReader : IPokemonReader
    {
        private readonly IDateTimeWrapper _dateTimeWrapper;
        private readonly ILogger _logger;

        public PokemonReader(IDateTimeWrapper dateTimeWrapper, ILogger logger)
        {
            _dateTimeWrapper = dateTimeWrapper;
            _logger = logger;
        }

        public Pokemon ReadPokemon(string fileContent)
        {
            Pokemon pokemon = JsonConvert.DeserializeObject<Pokemon>(fileContent);
            pokemon.Timestamp = _dateTimeWrapper.GetNow();
            _logger.Information($"Pokemon deserialized. Id: {pokemon.Id}, Name: {pokemon.Name}, Type: {pokemon.Type}, Timestamp: {pokemon.Timestamp}");
            return pokemon;
        }
    }
}
