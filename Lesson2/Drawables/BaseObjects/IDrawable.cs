using System.Drawing;

namespace Lesson2.Drawables.BaseObjects
{
    public interface IDrawable
    {
        Point GetPosition();
        void SetPosition(int x, int y);
        void SetPosition(Point pos);
        void Draw(Graphics graphics);
    }
}