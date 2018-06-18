using System;
using Lesson2.Loggers;

namespace Lesson2.Drawables.BaseObjects
{
    /// <summary>
    /// Объект игрового исключения
    /// </summary>
    public class GameException : Exception
    {
        public GameException(string message) : base(message)
        {
            Logger.Error(message);
            Logger.Error(StackTrace);
        }
    }
}