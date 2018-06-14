using System;

namespace Lesson2.Loggers
{
    /// <summary>
    /// Класс логгера для записи в консоль
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        public void Print(string message)
        {
            Console.WriteLine("[LOG] " + message);
        }

        public void Print(string message, params object[] args)
        {
            Console.WriteLine("[LOG] " + message, args);
        }

        public void ErrorPrint(string message)
        {
            var foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERROR] " + message);
            Console.ForegroundColor = foregroundColor;
        }

        public void ErrorPrint(string message, params object[] args)
        {
            var foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERROR] \r\n" + message, args);
            Console.ForegroundColor = foregroundColor;
        }
    }
}