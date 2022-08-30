using System;

namespace Example5.Logging;

public class Logger : ILogger
{
    public void Information(string message)
    {
        Console.WriteLine(message);
    }
}