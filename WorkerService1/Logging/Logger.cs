namespace Example6.Logging;

public class Logger : ILogger
{
    public void Information(string message)
    {
        Console.WriteLine(message);
    }
}