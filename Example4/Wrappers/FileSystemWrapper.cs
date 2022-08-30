using System.IO;

namespace Example4.Wrappers;

public class FileSystemWrapper : IFileSystemWrapper
{
    public Stream OpenRead(string filePath)
    {
        return File.OpenRead(filePath);
    }
}