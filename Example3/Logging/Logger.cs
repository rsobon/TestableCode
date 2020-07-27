using System;

namespace Example3.Logging
{
    public class Logger : ILogger
    {
        public void Information(string message)
        {
            Console.WriteLine(message);
        }
    }
}
