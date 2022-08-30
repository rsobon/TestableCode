using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Example4.Logging;
using Example4.Model;
using Example4.Wrappers;

namespace Example4.Reader;

public class PokemonReader : IPokemonReader
{
    private readonly IDateTimeWrapper _dateTimeWrapper;
    private readonly ILogger _logger;
    private readonly JsonSerializerOptions _jso;

    public PokemonReader(IDateTimeWrapper dateTimeWrapper, ILogger logger)
    {
        _dateTimeWrapper = dateTimeWrapper;
        _logger = logger;
        _jso = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
    }

    public async Task<Pokemon> ReadPokemon(Stream stream)
    {
        var pokemon = await JsonSerializer.DeserializeAsync<Pokemon>(stream, _jso);

        if (pokemon == null)
        {
            throw new InvalidDataException("Json invalid");
        }

        pokemon.Timestamp = _dateTimeWrapper.GetNow();
        _logger.Information($"Pokemon deserialized. Id: {pokemon.Id}, Name: {pokemon.Name}, Type: {pokemon.Type}, Timestamp: {pokemon.Timestamp}");
        return pokemon;
    }
}