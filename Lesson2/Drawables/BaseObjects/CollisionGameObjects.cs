using System.Drawing;

namespace Lesson2.Drawables.BaseObjects
{
    
    // Класс для игровых объектов с коллизией
    public abstract class CollisionGameObjects : GameObjects, ICollision, IKillable
    {
        public Rectangle Rect => new Rectangle(new Point((int) _position.X, (int) _position.Y), _size);

        protected CollisionGameObjects(Point position, Point dir, Size size) : base(position, dir, size){}
        public bool Collision(ICollision obj)
        {
            if (IsDead)
            {
                return false;
            }
            var intersectsWith = obj.Rect.IntersectsWith(Rect);
            if (intersectsWith)
            {
                OnCollision(obj);
                obj.OnCollision(this);
            }
            return intersectsWith;
        }
        
        // Метод, который вызовется у обоих объектов при коллизии
        public abstract void OnCollision(ICollision obj);

        public bool IsDead => false;
    }
}