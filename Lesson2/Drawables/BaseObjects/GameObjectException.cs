using System;
using Lesson2.Loggers;

namespace Lesson2.Drawables.BaseObjects
{
    /// <summary>
    /// Объект игрового исключения
    /// </summary>
    public class GameObjectException : Exception
    {
        public GameObjectException(string message) : base(message)
        {
            Logger.Error(message);
            Logger.Error(StackTrace);
        }
    }
}