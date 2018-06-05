using System;
using System.Drawing;
using Lesson2.Drawables.BaseObjects;
using Lesson2.Loggers;

namespace Lesson2.Drawables
{
    public class Bullet : CollisionGameObjects
    {
        private readonly Random _random = new Random();

        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(Pens.OrangeRed, _position.X, _position.Y, _size.Width, _size.Height);
        }

        public override void Update(float totalSeconds)
        {
            _position.X = _position.X + _dir.X * totalSeconds;
            if (_position.X >= Drawer.Width)
            {
                Reset();
            }
        }

        public override void OnCollision()
        {
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