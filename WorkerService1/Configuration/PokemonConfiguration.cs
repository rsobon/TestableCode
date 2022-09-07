using System.Text.Json;

namespace Example6.Configuration;

public class PokemonConfiguration : IPokemonConfiguration
{
    public IList<string> GetAllowedPokemonNames()
    {
        var config = File.ReadAllText(@"appsettings.json");
        var options = JsonSerializer.Deserialize<PokemonOptions>(config);
        return options.AllowedPokemon;
    }
}

internal class PokemonOptions
{
    public IList<string> AllowedPokemon { get; set; }
}