using System.Drawing;

namespace Lesson2.Drawables.BaseObjects
{
    
    // Класс для игровых объектов с коллизией
    public abstract class CollisionGameObjects : GameObjects, ICollision
    {
        public Rectangle Rect => new Rectangle(_position, _size);

        protected CollisionGameObjects(Point position, Point dir, Size size) : base(position, dir, size){}
        public bool Collision(ICollision obj)
        {
            var intersectsWith = obj.Rect.IntersectsWith(Rect);
            if (intersectsWith)
            {
                OnCollision();
                obj.OnCollision();
            }
            return intersectsWith;
        }
        
        // Метод, который вызовется у обоих объектов при коллизии
        public abstract void OnCollision();

    }
}