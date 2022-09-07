using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Example5.Configuration;
using Example5.Logging;
using Example5.Model;
using Example5.Wrappers;

namespace Example5.Reader;

public class PokemonReader : IPokemonReader
{
    private readonly IDateTimeWrapper _dateTimeWrapper;
    private readonly ILogger _logger;
    private readonly JsonSerializerOptions _jso;
    private readonly IList<string> _allowedPokemonNames;

    public PokemonReader(IDateTimeWrapper dateTimeWrapper, ILogger logger, IPokemonConfiguration configuration)
    {
        _dateTimeWrapper = dateTimeWrapper;
        _logger = logger;
        _jso = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        _allowedPokemonNames = configuration.GetAllowedPokemonNames();
    }

    public async Task<Pokemon> ReadPokemon(Stream stream)
    {
        var pokemon = await JsonSerializer.DeserializeAsync<Pokemon>(stream, _jso);

        if (pokemon == null)
        {
            throw new InvalidDataException("Json invalid");
        }

        var isValid = IsValid(pokemon);

        if (!isValid)
        {
            throw new InvalidDataException("Validation failed!");
        }

        pokemon.Timestamp = _dateTimeWrapper.GetNow();
        _logger.Information($"Pokemon deserialized. Id: {pokemon.Id}, Name: {pokemon.Name}, Type: {pokemon.Type}, Timestamp: {pokemon.Timestamp}");
        return pokemon;
    }

    private bool IsValid(Pokemon pokemon)
    {
        var validationResult = true;

        if (string.IsNullOrEmpty(pokemon.Name))
        {
            _logger.Information("Pokemon name is null or empty!");
            validationResult = false;
        }

        if (!_allowedPokemonNames.Contains(pokemon.Name))
        {
            _logger.Information($"Pokemon name: \"{pokemon.Name}\" is not allowed!");
            validationResult = false;
        }

        return validationResult;
    }
}