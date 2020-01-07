using Example4.Model;

namespace Example4.Infrastructure
{
    public interface IEntityReader
    {
        Entity ReadEntity(string fileContent);
    }
}