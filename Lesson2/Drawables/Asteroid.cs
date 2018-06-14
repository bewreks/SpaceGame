﻿using System.Drawing;
using Lesson2.Drawables.BaseObjects;

namespace Lesson2.Drawables
{
    public class Asteroid : CollisionKillableGameObjects
    {
        private bool _isDead;

        public bool IsDead => _isDead;

        public int Energy { get; }
        public int Score { get; }

        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Energy = 5;
            _isDead = false;
            Score = 5;
        }

        public override void Draw(Graphics graphics)
        {
            if (IsDead) return;

            graphics.FillEllipse(Brushes.White, _position.X, _position.Y, _size.Width, _size.Height);
        }

        public override void Update(float deltaTime)
        {
            if (IsDead) return;

            _position.X = _position.X + _dir.X * deltaTime;
            if (_position.X < 0)
            {
                // Потом можно отнимать счет за пропущенные астероиды
            }
        }

        public override void OnCollision(ICollision obj)
        {
            _isDead = true;
        }
    }
}