using System;
using Example2.Enums;

namespace Example2
{
    public class Entity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public EntityType Type { get; set; }

        public DateTime? Timestamp { get; set; }
    }
}
