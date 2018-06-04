using System.Drawing;
using Lesson2.Drawables.BaseObjects;

namespace Lesson2.Drawables
{
    public class Star : GameObjects
    {
        private PointF[] _basePoints;
        private PointF[] _points;

        private float _scale = 1;

        public Star(Point position, Point direction, Size size) : base(position, direction, size)
        {
            _scale = _size.Width / 3.0f;

            _basePoints = new PointF[10];
            _basePoints[0] = new PointF(0, -3);
            _basePoints[1] = new PointF(5 / 6.0f, -1);
            _basePoints[2] = new PointF(3, -1);
            _basePoints[3] = new PointF(49 / 38.0f, 7 / 19.0f);
            _basePoints[4] = new PointF(2, 3);
            _basePoints[5] = new PointF(0, 7 / 5.0f);
            _basePoints[6] = new PointF(-2, 3);
            _basePoints[7] = new PointF(-22 / 16.0f, 9 / 19.0f);
            _basePoints[8] = new PointF(-3, -1);
            _basePoints[9] = new PointF(-2 / 3.0f, -1);

            _points = new PointF[_basePoints.Length];
        }

        public override void Draw(Graphics graphics)
        {
            graphics.FillPolygon(Brushes.Wheat, _points);
        }

        public override void Update()
        {
            _position.X = _position.X + _dir.X;
            if (_position.X < 0) _position.X = Drawer.Width + _size.Width;
            
            for (var i = 0; i < _basePoints.Length; i++)
            {
                _points[i] = new PointF(_basePoints[i].X * _scale + _position.X,
                    _basePoints[i].Y * _scale + _position.Y);
            }
        }
    }
}