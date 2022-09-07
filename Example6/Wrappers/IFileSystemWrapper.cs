namespace Example6.Wrappers;

public interface IFileSystemWrapper
{
    Stream OpenRead(string filePath);
}