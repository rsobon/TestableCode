using System;
using Example4.Model;

namespace Example4.Db
{
    public class Database : IDatabase
    {
        public void SaveEntity(Entity entity)
        {
            Console.WriteLine("Saving database...");
        }
    }
}
