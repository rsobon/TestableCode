using Example4.Model;

namespace Example4.Reader
{
    public interface IPokemonReader
    {
        Pokemon ReadPokemon(string fileContent);
    }
}