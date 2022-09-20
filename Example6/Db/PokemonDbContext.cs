using Example6.Model;
using Microsoft.EntityFrameworkCore;

namespace Example6.Db
{
    public class PokemonDbContext : DbContext
    {
        public DbSet<Pokemon> Pokemon { get; set; }

        public PokemonDbContext(DbContextOptions<PokemonDbContext> options) : base(options)
        {
        }
    }
}
