using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Lesson2.Drawables.BaseObjects;

namespace Lesson2.Scenes
{
    public class SpaceShip : CollisionGameObjects
    {
        private GraphicsPath _graphicsPath;
        private float _scaleX;
        private float _scaleY;

        private MouseMoveGameEvent _prEventArgs;

        public SpaceShip(Point position, Point dir, Size size) : base(position, dir, size)
        {
            _scaleX = _size.Width / 6.0f;
            _scaleY = _size.Height / 4.0f;

            _graphicsPath = new GraphicsPath();
            _graphicsPath.AddLine(0, 2, 6, 0);
            _graphicsPath.AddLine(6, 0, 0, -2);

            var matrix = new Matrix();
            matrix.Translate(_position.X, _position.Y);
            matrix.Scale(_scaleX, _scaleY);
            _graphicsPath.Transform(matrix);

            // Оставлю возможность управления с клавиатуры
            EventManager.AddEventListener(EventManager.Events.UpEvent, Up);
            EventManager.AddEventListener(EventManager.Events.DownEvent, Down);
            EventManager.AddEventListener(EventManager.Events.MoveEvent, Move);
        }

        private void Move(IEventArgs args)
        {
            lock (_graphicsPath)
            {
                var arg = args as MouseMoveGameEvent;
                float i = arg.Y - (_prEventArgs?.Y ?? 0);
                _prEventArgs = arg;

                _position.Y = _position.Y + i;
                var matrix = new Matrix();
                matrix.Translate(_dir.X, i);

                _graphicsPath.Transform(matrix);
            }
        }

        private void Down(IEventArgs args)
        {
            lock (_graphicsPath)
            {
                if (_position.Y >= Drawer.Height - _size.Height)
                {
                    return;
                }

                _position.Y = _position.Y + _dir.Y;
                var matrix = new Matrix();
                matrix.Translate(_dir.X, _dir.Y);

                _graphicsPath.Transform(matrix);
            }
        }

        private void Up(IEventArgs args)
        {
            lock (_graphicsPath)
            {
                if (_position.Y <= 0)
                {
                    return;
                }

                _position.Y = _position.Y - _dir.Y;
                var matrix = new Matrix();
                matrix.Translate(_dir.X, -_dir.Y);

                _graphicsPath.Transform(matrix);
            }
        }

        public override void Draw(Graphics graphics)
        {
            lock (_graphicsPath)
            {
                graphics.FillPath(Brushes.Wheat, _graphicsPath);
            }
        }

        public override void Update(float totalSeconds)
        {
        }


        public override void OnCollision()
        {
        }

        public PointF GetPoint()
        {
            return _graphicsPath.PathPoints[1];
        }
    }
}