using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Example5.Configuration;

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