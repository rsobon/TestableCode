using System;
using Example4.Enums;

namespace Example4.Model
{
    public class Entity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public EntityType Type { get; set; }

        public DateTime? Timestamp { get; set; }
    }
}
