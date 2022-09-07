namespace Example6.Wrappers;

public class FileSystemWrapper : IFileSystemWrapper
{
    public Stream OpenRead(string filePath)
    {
        return File.OpenRead(filePath);
    }

    public IList<string> GetFiles(string directory)
    {
        return Directory.GetFiles(directory);
    }

    public void DeleteFile(string filePath)
    {
        File.Delete(filePath);
    }
}