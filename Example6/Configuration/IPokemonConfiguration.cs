namespace Example6.Configuration;

public interface IPokemonConfiguration
{
    IList<string> GetAllowedPokemonNames();
}