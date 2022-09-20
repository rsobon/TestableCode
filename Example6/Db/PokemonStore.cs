using Example6.Model;

namespace Example6.Db;

public class PokemonStore : IPokemonStore
{
    private readonly PokemonDbContext _db;

    public PokemonStore(PokemonDbContext db)
    {
        _db = db;
    }

    public async Task SavePokemon(IList<Pokemon> pokemon, CancellationToken token)
    {
        foreach (var item in pokemon)
        {
            item.ObjectId = Guid.NewGuid();
        }

        await _db.AddRangeAsync(pokemon, token);
        await _db.SaveChangesAsync(token);
    }
}