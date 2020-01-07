using System;
using Example5.Model;

namespace Example5.Db
{
    public class Database : IDatabase
    {
        public void SaveEntity(Entity entity)
        {
            Console.WriteLine("Saving database...");
        }

        public bool IsValidationEnabled()
        {
            return true;
        }
    }
}
