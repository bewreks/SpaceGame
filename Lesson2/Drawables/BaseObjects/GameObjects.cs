using System;
using System.Drawing;

namespace Lesson2.Drawables.BaseObjects
{
    // Базовый класс игрового объекта
    // Потомки должны реализовать методы отрисовки и обновления данных
    // Для объектов с коллизией нужно использовать класс CollisionGameObjects
    public abstract class GameObjects : IDrawable, IUpdatable
    {
        protected Point _position;
        protected Point _dir;
        protected Size _size;

        public GameObjects(Point position, Point dir, Size size)
        {
            if (position.X < -Drawer.Width  || position.Y < -Drawer.Height)
            {
                throw new GameObjectException("Слишком маленькая позиция");
            }
            if (position.X > 2 * Drawer.Width || position.Y > 2 * Drawer.Height)
            {
                throw new GameObjectException("Слишком большая позиция");
            }
            _position = position;
            
            if (Math.Abs(dir.X) >= Drawer.Width || Math.Abs(dir.Y) >= Drawer.Height)
            {
                throw new GameObjectException("Слишком большая скорость");
            }
            _dir = dir;
            
            if (size.Width < 0 || size.Height < 0)
            {
                throw new GameObjectException("Слишком маленький размер");
            }
            if (size.Width > Drawer.Width || size.Height > Drawer.Height)
            {
                throw new GameObjectException("Слишком большой размер");
            }
            _size = size;
        }
        
        public Point GetPosition()
        {
            return _position;
        }

        public virtual void SetPosition(int x, int y)
        {
            _position.X = x;
            _position.Y = y;
        }

        public void SetPosition(Point pos)
        {
            SetPosition(pos.X, pos.Y);
        }

        public abstract void Draw(Graphics graphics);
        public abstract void Update();
    }
}