using Example3.Model;

namespace Example3.Infrastructure
{
    public interface IEntityReader
    {
        Entity ReadEntity(string fileContent);
    }
}