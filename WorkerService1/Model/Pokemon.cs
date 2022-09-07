using Example6.Enums;

namespace Example6.Model;

public class Pokemon
{
    public int Id { get; set; }

    public string Name { get; set; }

    public PokemonType Type { get; set; }

    public DateTime? Timestamp { get; set; }
}