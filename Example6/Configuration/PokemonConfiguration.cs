using System.Text.Json;

namespace Example6.Configuration;

public class PokemonConfiguration : IPokemonConfiguration
{
    public IList<string> GetAllowedPokemonNames()
    {
        var config = File.ReadAllText(@"App_Data\external-resource.json");
        var options = JsonSerializer.Deserialize<PokemonOptions>(config);
        return options != null ? options.AllowedPokemon : new List<string>();
    }
}

internal class PokemonOptions
{
    public IList<string> AllowedPokemon { get; set; } = new List<string>();
}