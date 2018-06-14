using System.Drawing;
using Lesson2.Drawables.BaseObjects;

namespace Lesson2.Drawables
{
    /// <summary>
    /// Класс четырехконечной звезды
    /// </summary>
    public class XStar : GameObjects
    {
        public XStar(Point position, Point dir, Size size) : base(position, dir, size)
        {
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawLine(Pens.White, _position.X, _position.Y, _position.X + _size.Width,
                _position.Y + _size.Height);
            graphics.DrawLine(Pens.White, _position.X + _size.Width, _position.Y, _position.X,
                _position.Y + _size.Height);
        }

        public override void Update(float deltaTime)
        {
            _position.X = _position.X + _dir.X * deltaTime;
            if (_position.X < 0) _position.X = Drawer.Width + _size.Width;
        }
    }
}