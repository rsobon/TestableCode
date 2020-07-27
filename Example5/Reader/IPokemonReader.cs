using Example5.Model;

namespace Example5.Reader
{
    public interface IPokemonReader
    {
        Pokemon ReadPokemon(string fileContent);
    }
}