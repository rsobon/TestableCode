using System;
using Example1.Enums;

namespace Example1
{
    public class Entity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public EntityType Type { get; set; }

        public DateTime? Timestamp { get; set; }
    }
}
