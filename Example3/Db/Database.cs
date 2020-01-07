using System;
using Example3.Model;

namespace Example3.Db
{
    public class Database : IDatabase
    {
        public void SaveEntity(Entity entity)
        {
            Console.WriteLine("Saving database...");
        }
    }
}
