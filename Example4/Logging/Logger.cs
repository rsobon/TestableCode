using System;

namespace Example4.Logging
{
    public class Logger : ILogger
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
