using Example3.Model;

namespace Example3.Reader
{
    public interface IPokemonReader
    {
        Pokemon ReadPokemon(string fileContent);
    }
}