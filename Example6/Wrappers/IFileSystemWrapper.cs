namespace Example6.Wrappers;

public interface IFileSystemWrapper
{
    Stream OpenRead(string filePath);

    IList<string> GetFiles(string directory);

    void DeleteFile(string filePath);
}