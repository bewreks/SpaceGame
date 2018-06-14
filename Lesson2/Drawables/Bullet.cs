using System.Drawing;
using System.Media;
using Lesson2.Drawables.BaseObjects;
using Lesson2.Events;

namespace Lesson2.Drawables
{
    /// <summary>
    /// Базовый объект пули
    /// </summary>
    public class Bullet : CollisionKillableGameObjects
    {
        private bool _isDead;

        public bool IsDead => _isDead;

        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            _isDead = false;
        }

        public override void Draw(Graphics graphics)
        {
            if (IsDead) return;
            graphics.DrawRectangle(Pens.OrangeRed, _position.X, _position.Y, _size.Width, _size.Height);
        }

        public override void Update(float deltaTime)
        {
            if (IsDead) return;

            _position.X = _position.X + _dir.X * deltaTime;
            if (_position.X >= Drawer.Width)
            {
                _isDead = true;
            }
        }

        public override void OnCollision(ICollision obj)
        {
            if (IsDead) return;
            
            if (obj is Asteroid)
            {
                EventManager.DispatchEvent(EventManager.Events.ChangeScoreEvent,
                    new ChangeScoreEvent((obj as Asteroid).Score));
                SystemSounds.Hand.Play();
            }
        }
    }
}