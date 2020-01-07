using System;

namespace Example3.Logging
{
    public class Logger : ILogger
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
