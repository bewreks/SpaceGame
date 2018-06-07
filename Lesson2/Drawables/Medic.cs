using System.Drawing;
using Lesson2.Drawables.BaseObjects;

namespace Lesson2.Drawables
{
    public class Medic : CollisionGameObjects
    {
        private bool _isDead;

        public bool IsDead
        {
            get
            {
                return _isDead;
            }
        }

        public int Energy { get; set; }

        public Medic(Point position, Point dir, Size size) : base(position, dir, size)
        {
            Energy = 10;
            _isDead = false;
        }

        public override void Draw(Graphics graphics)
        {
            if (IsDead)
                return;

            graphics.FillEllipse(Brushes.Red, _position.X, _position.Y, _size.Width, _size.Height);
        }

        public override void Update(float totalSeconds)
        {
            if (IsDead) 
                return;

            _position.X = _position.X + _dir.X * totalSeconds;
            if (_position.X < 0) _isDead = true;
        }

        public override void OnCollision(ICollision obj)
        {
            _isDead = true;
        }
    }
}