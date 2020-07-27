using System;
using Example1.Enums;

namespace Example1.Model
{
    public class Pokemon
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public PokemonType Type { get; set; }

        public DateTime? Timestamp { get; set; }
    }
}
