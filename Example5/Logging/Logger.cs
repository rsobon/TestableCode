using System;

namespace Example5.Logging
{
    public class Logger : ILogger
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
