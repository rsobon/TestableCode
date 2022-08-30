using System.IO;

namespace Example4.Wrappers;

public interface IFileSystemWrapper
{
    Stream OpenRead(string filePath);
}