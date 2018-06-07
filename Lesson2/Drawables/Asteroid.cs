using System;
using System.Drawing;
using Lesson2.Drawables.BaseObjects;
using Lesson2.Loggers;

namespace Lesson2.Drawables
{
    public class Asteroid : CollisionGameObjects
    {
        private bool _isDead;

        public bool IsDead => _isDead;
        public int Energy { get; set; }

        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Energy = 5;
            _isDead = false;
        }

        public override void Draw(Graphics graphics)
        {
            if (_isDead) return;

            graphics.FillEllipse(Brushes.White, _position.X, _position.Y, _size.Width, _size.Height);
        }

        public override void Update(float totalSeconds)
        {
            if (_isDead) return;

            _position.X = _position.X + _dir.X * totalSeconds;
            if (_position.X < 0) Reset();
        }

        public override void OnCollision(ICollision obj)
        {
            _isDead = true;
        }

        private void Reset()
        {
            _position.X = Drawer.Width + _size.Width;
            _position.Y = new Random().Next(Drawer.Height);
        }

    }
}