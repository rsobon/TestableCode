using System.IO;
using Example5.Db;
using Example5.Logging;
using Example5.Model;
using Example5.Wrappers;
using Newtonsoft.Json;

namespace Example5.Reader
{
    public class PokemonReader : IPokemonReader
    {
        private readonly IDateTimeWrapper _dateTimeWrapper;
        private readonly ILogger _logger;

        private readonly bool _isValidationEnabled;

        public PokemonReader(IDateTimeWrapper dateTimeWrapper, ILogger logger, IPokemonStore pokemonStore)
        {
            _dateTimeWrapper = dateTimeWrapper;
            _logger = logger;

            _isValidationEnabled = pokemonStore.IsValidationEnabled();
        }

        public Pokemon ReadPokemon(string fileContent)
        {
            Pokemon pokemon = JsonConvert.DeserializeObject<Pokemon>(fileContent);

            if (_isValidationEnabled)
            {
                var isValid = IsValid(pokemon);

                _logger.Information($"Validation result: {isValid}");

                if (!isValid)
                {
                    throw new InvalidDataException();
                }
            }

            pokemon.Timestamp = _dateTimeWrapper.GetNow();
            _logger.Information($"Pokemon deserialized. Id: {pokemon.Id}, Name: {pokemon.Name}, Type: {pokemon.Type}, Timestamp: {pokemon.Timestamp}");
            return pokemon;
        }

        private bool IsValid(Pokemon pokemon)
        {
            _logger.Information("Performing validation...");

            if (string.IsNullOrEmpty(pokemon.Name))
            {
                _logger.Information("Pokemon name is null or empty!");
                return false;
            }

            return true;
        }
    }
}
