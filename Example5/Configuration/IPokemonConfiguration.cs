using System.Collections.Generic;

namespace Example5.Configuration;

public interface IPokemonConfiguration
{
    IList<string> GetAllowedPokemonNames();
}