using System;
using System.Drawing;
using Lesson2.Drawables.BaseObjects;
using Lesson2.Loggers;

namespace Lesson2.Drawables
{
    public class Bullet : CollisionGameObjects
    {
        private readonly Random _random = new Random();

        private bool _isDead;

        public bool IsDead => _isDead;

        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            _isDead = false;
        }

        public override void Draw(Graphics graphics)
        {
            if (_isDead)return;
            graphics.DrawRectangle(Pens.OrangeRed, _position.X, _position.Y, _size.Width, _size.Height);
        }

        public override void Update(float totalSeconds)
        {
            if (_isDead) return;
            _position.X = _position.X + _dir.X * totalSeconds;
            if (_position.X >= Drawer.Width)
            {
                _isDead = true;
            }
        }

        public override void OnCollision()
        {
            if (_isDead)return;
            Logger.Print("Bullet collision");
            System.Media.SystemSounds.Hand.Play();
            Reset();
        }

        private void Reset()
        {
            _position.X = 0;
            _position.Y = _random.Next(Drawer.Height);
        }
    }
}