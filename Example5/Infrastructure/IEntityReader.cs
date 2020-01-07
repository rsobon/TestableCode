using Example5.Model;

namespace Example5.Infrastructure
{
    public interface IEntityReader
    {
        Entity ReadEntity(string fileContent);
    }
}