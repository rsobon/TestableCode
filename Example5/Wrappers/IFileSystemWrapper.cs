using System.IO;

namespace Example5.Wrappers;

public interface IFileSystemWrapper
{
    Stream OpenRead(string filePath);
}