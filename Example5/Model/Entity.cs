using System;
using Example5.Enums;

namespace Example5.Model
{
    public class Entity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public EntityType Type { get; set; }

        public DateTime? Timestamp { get; set; }
    }
}
