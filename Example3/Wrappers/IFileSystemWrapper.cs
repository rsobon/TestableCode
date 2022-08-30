using System.IO;

namespace Example3.Wrappers;

public interface IFileSystemWrapper
{
    Stream OpenRead(string filePath);
}