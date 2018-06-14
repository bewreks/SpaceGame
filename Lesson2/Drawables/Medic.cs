using System.Drawing;
using Lesson2.Drawables.BaseObjects;

namespace Lesson2.Drawables
{
    public class Medic : CollisionKillableGameObjects
    {
        private bool _isDead;

        public bool IsDead => _isDead;

        public int Energy { get; }
        public int Score { get; }

        public Medic(Point position, Point dir, Size size) : base(position, dir, size)
        {
            Energy = 10;
            _isDead = false;
            Score = 1;
        }

        public override void Draw(Graphics graphics)
        {
            if (IsDead)
                return;

            graphics.FillEllipse(Brushes.Red, _position.X, _position.Y, _size.Width, _size.Height);
        }

        public override void Update(float deltaTime)
        {
            if (IsDead)
                return;

            _position.X = _position.X + _dir.X * deltaTime;
            if (_position.X < 0) _isDead = true;
        }

        public override void OnCollision(ICollision obj)
        {
            _isDead = true;
        }
    }
}