using System.IO;

namespace Example4.Wrappers
{
    public class FileSystemWrapper : IFileSystemWrapper
    {
        public string ReadFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}