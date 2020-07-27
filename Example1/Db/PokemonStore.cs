using System;
using System.Threading.Tasks;
using Example1.Model;

namespace Example1.Db
{
    public class PokemonStore
    {
        public async Task SavePokemon(Pokemon pokemon)
        {
            Console.WriteLine("Saving to database: " + pokemon.Name);

            await Task.Delay(0);
        }
    }
}
