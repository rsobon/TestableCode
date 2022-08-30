using System.IO;

namespace Example3.Wrappers;

public class FileSystemWrapper : IFileSystemWrapper
{
    public Stream OpenRead(string filePath)
    {
        return File.OpenRead(filePath);
    }
}