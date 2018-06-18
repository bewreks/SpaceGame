using System;
using System.Drawing;

namespace Lesson2.Drawables.BaseObjects
{
    /// <summary>
    /// Базовый класс игрового объекта
    /// Требует от потомков реализацию интерфейсов IDrawable и IUpdatable
    /// </summary>
    public abstract class GameObjects : IDrawable, IUpdatable
    {
        protected PointF _position;
        protected Point _dir;
        protected Size _size;

        public GameObjects(Point position, Point dir, Size size)
        {
            if (position.X < -Drawer.Width || position.Y < -Drawer.Height)
            {
                throw new GameException("Слишком маленькая позиция");
            }

            if (position.X > 2 * Drawer.Width || position.Y > 2 * Drawer.Height)
            {
                throw new GameException("Слишком большая позиция");
            }

            _position = position;

            if (Math.Abs(dir.X) >= Drawer.Width || Math.Abs(dir.Y) >= Drawer.Height)
            {
                throw new GameException("Слишком большая скорость");
            }

            _dir = dir;

            if (size.Width < 0 || size.Height < 0)
            {
                throw new GameException("Слишком маленький размер");
            }

            if (size.Width > Drawer.Width || size.Height > Drawer.Height)
            {
                throw new GameException("Слишком большой размер");
            }

            _size = size;
        }

        public abstract void Draw(Graphics graphics);
        public abstract void Update(float deltaTime);
    }
}