using Example5.Enums;

namespace Example5
{
    public interface IImportFileCommand
    {
        ImportingStatus ImportEntity(string filePath);
    }
}
