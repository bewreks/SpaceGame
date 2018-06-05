using System;

namespace Lesson2.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void Print(string message)
        {
            Console.WriteLine(message);
        }

        public void Print(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }
    }
}