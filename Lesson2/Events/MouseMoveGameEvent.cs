namespace Lesson2
{
    public class MouseMoveGameEvent : IEventArgs
    {
        public int X;
        public int Y;
        
        public MouseMoveGameEvent(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}