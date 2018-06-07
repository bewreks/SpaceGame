namespace Lesson2.Events
{
    public class MouseMoveGameEvent : IEventArgs
    {
        public int X {get;}
        public int Y {get;}

        public MouseMoveGameEvent(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}