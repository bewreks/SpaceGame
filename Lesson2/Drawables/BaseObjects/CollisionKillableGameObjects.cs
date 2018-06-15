using System.Drawing;

namespace Lesson2.Drawables.BaseObjects
{
    /// <summary>
    /// Класс для убиваемых игровых объектов с коллизией 
    /// </summary>
    public abstract class CollisionKillableGameObjects : GameObjects, ICollision, IKillable
    {
        public virtual Rectangle Rect => new Rectangle(new Point((int) _position.X, (int) _position.Y), _size);

        protected CollisionKillableGameObjects(Point position, Point dir, Size size) : base(position, dir, size)
        {
        }

        public bool Collision(ICollision obj)
        {
            var intersectsWith = obj.Rect.IntersectsWith(Rect);
            if (intersectsWith)
            {
                OnCollision(obj);
                obj.OnCollision(this);
            }

            return intersectsWith;
        }

        public abstract void OnCollision(ICollision obj);

        public bool IsDead => false;
    }
}