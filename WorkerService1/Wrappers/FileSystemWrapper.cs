namespace Example6.Wrappers;

public class FileSystemWrapper : IFileSystemWrapper
{
    public Stream OpenRead(string filePath)
    {
        return File.OpenRead(filePath);
    }
}