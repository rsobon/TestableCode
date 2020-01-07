using Example5.Model;

namespace Example5.Db
{
    public interface IDatabase
    {
        void SaveEntity(Entity entity);

        bool IsValidationEnabled();
    }
}
