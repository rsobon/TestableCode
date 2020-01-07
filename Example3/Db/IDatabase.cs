using Example3.Model;

namespace Example3.Db
{
    public interface IDatabase
    {
        void SaveEntity(Entity entity);
    }
}
