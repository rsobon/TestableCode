using Example4.Enums;

namespace Example4
{
    public interface IImportFileCommand
    {
        ImportingStatus ImportEntity(string filePath);
    }
}
