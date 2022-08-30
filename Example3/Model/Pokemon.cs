using System;
using Example3.Enums;

namespace Example3.Model;

public class Pokemon
{
    public int Id { get; set; }

    public string Name { get; set; }

    public PokemonType Type { get; set; }

    public DateTime? Timestamp { get; set; }
}