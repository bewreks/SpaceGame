using Lesson2.Drawables.BaseObjects;

namespace Lesson2.Events
{
    public class ThrowObjectWaveEventArgs : IEventArgs
    {
        public GameObjects Object { get; }


        public ThrowObjectWaveEventArgs(GameObjects @object)
        {
            Object = @object;
        }
    }
}