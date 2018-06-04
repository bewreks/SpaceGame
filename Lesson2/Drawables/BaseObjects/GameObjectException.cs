using System;

namespace Lesson2.Drawables.BaseObjects
{
    public class GameObjectException : Exception
    {
        public GameObjectException(string message):base(message)
        {
            
        }
    }
}