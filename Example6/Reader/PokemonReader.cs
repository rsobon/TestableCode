using System.Text.Json;
using Example6.Model;
using Example6.Validation;
using Example6.Wrappers;
using Microsoft.Extensions.Logging;

namespace Example6.Reader;

public class PokemonReader : IPokemonReader
{
    private readonly ILogger<PokemonReader> _logger;
    private readonly IDateTimeWrapper _dateTimeWrapper;
    private readonly IPokemonValidationService _validationService;
    private readonly JsonSerializerOptions _jso;

    public PokemonReader(
        ILogger<PokemonReader> logger, 
        IDateTimeWrapper dateTimeWrapper, 
        IPokemonValidationService validationService)
    {
        _logger = logger;
        _dateTimeWrapper = dateTimeWrapper;
        _validationService = validationService;
        _jso = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
    }

    public async Task<IList<Pokemon>> ReadPokemon(Stream stream)
    {
        var pokemonList = await JsonSerializer.DeserializeAsync<IList<Pokemon>>(stream, _jso);

        if (pokemonList == null)
        {
            throw new InvalidDataException("Json invalid");
        }

        var now = _dateTimeWrapper.GetNow();

        foreach (var pokemon in pokemonList)
        {
            var isValid = IsValid(pokemon);

            if (!isValid)
            {
                _logger.LogInformation($"Pokemon invalid. Id: {pokemon.Id}, Name: {pokemon.Name}, Type: {pokemon.Type}. Skipping...");
                continue;
            }

            pokemon.Timestamp = now;
            _logger.LogInformation($"Pokemon deserialized. Id: {pokemon.Id}, Name: {pokemon.Name}, Type: {pokemon.Type}, Timestamp: {pokemon.Timestamp}");
        }

        return pokemonList;
    }

    private bool IsValid(Pokemon pokemon)
    {
        var validationResult = true;

        if (string.IsNullOrEmpty(pokemon.Name))
        {
            _logger.LogInformation("Pokemon name is null or empty!");
            validationResult = false;
        }

        if (pokemon.Id < 1)
        {
            _logger.LogInformation("Pokemon id is lower than 1!");
            validationResult = false;
        }

        if (!_validationService.GetAllowedPokemonTypes().Contains(pokemon.Type))
        {
            _logger.LogInformation($"Pokemon type: \"{pokemon.Type}\" is not allowed!");
            validationResult = false;
        }

        return validationResult;
    }
}