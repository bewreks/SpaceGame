using System.Drawing;
using System.Drawing.Drawing2D;
using Lesson2.Drawables.BaseObjects;

namespace Lesson2.Drawables
{
    public class Star : GameObjects
    {
        private float _scale = 1;
        private GraphicsPath _graphicsPath;

        public Star(Point position, Point direction, Size size) : base(position, direction, size)
        {
            _scale = _size.Width / 3.0f;

            _graphicsPath = new GraphicsPath();
            _graphicsPath.AddLine(0, 3, 2, -3);
            _graphicsPath.AddLine(2, -3, -3, 1);
            _graphicsPath.AddLine(-3, 1, 3, 1);
            _graphicsPath.AddLine(3, 1, -2, -3);
            _graphicsPath.AddLine(-2, -3, 0, 3);
            
            var matrix = new Matrix();
            matrix.Translate(_position.X, _position.Y);
            matrix.Scale(_scale, -_scale);
            _graphicsPath.Transform(matrix);
            _graphicsPath.FillMode = FillMode.Winding;
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
            lock (_graphicsPath)
            {
                var matrix = new Matrix();
                _position.X = _position.X + _dir.X * totalSeconds;
                if (_position.X < 0)
                {
                    _position.X = Drawer.Width + _size.Width;
                    matrix.Translate(Drawer.Width + _size.Width, _dir.Y * totalSeconds);
                }
                else
                {
                    matrix.Translate(_dir.X * totalSeconds, _dir.Y * totalSeconds);
                }
                
                _graphicsPath.Transform(matrix);
            }
        }
    }
}