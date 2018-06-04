using System.Drawing;

namespace Lesson2.Drawables.BaseObjects
{
    public interface ICollision
    {
        bool Collision(ICollision obj);
        void OnCollision();
        Rectangle Rect { get; }

    }
}