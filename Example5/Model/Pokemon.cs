using System;
using Example5.Enums;

namespace Example5.Model;

public class Pokemon
{
    public int Id { get; set; }

    public string Name { get; set; }

    public PokemonType Type { get; set; }

    public DateTime? Timestamp { get; set; }
}