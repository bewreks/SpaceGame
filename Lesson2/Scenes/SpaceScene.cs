using System;
using System.Collections.Generic;
using System.Drawing;
using Lesson2.Drawables;
using Lesson2.Drawables.BaseObjects;

namespace Lesson2.Scenes
{
    public class SpaceScene : Scene
    {
        private List<GameObjects> _stars = new List<GameObjects>();
        private Bullet _bullet;
        private List<Asteroid> _asteroids = new List<Asteroid>();

        protected  override void OnLoad()
        {
            _stars.Clear();
            _asteroids.Clear();

            var rnd = new Random();

            var random = new Random();
            for (var i = 0; i < 100; i++)
            {
                var position = new Point(rnd.Next(0, Drawer.Width), rnd.Next(0, Drawer.Height));
                var direction = new Point(-rnd.Next(50, 500), 0);
                var size = new Size(3, 3);
                
                var type = random.Next(2) % 2 == 0 ? typeof(Star) : typeof(XStar);
                
                _stars.Add((GameObjects)Activator.CreateInstance(type, position, direction, size));
            }

            _bullet = new Bullet(new Point(0, rnd.Next(0, Drawer.Height)), new Point(500, 0), new Size(4, 1));

            for (var i = 0; i < 3; i++)
            {
                var position = new Point(Drawer.Width, rnd.Next(0, Drawer.Height));
                var direction = new Point(-rnd.Next(50, 500), 0);
                var r = rnd.Next(5, 50);
                var size = new Size(r, r);
                _asteroids.Add(new Asteroid(position, direction, size));
            }

            AddDrawable(_stars);
            AddDrawable(_asteroids);
            AddDrawable(_bullet);

            AddUpdatable(_stars);
            AddUpdatable(_asteroids);
        }

        public override void Update(float totalSeconds)
        {

            base.Update(totalSeconds);

            foreach (var asteroid in _asteroids)
            {
                asteroid.Collision(_bullet);
            }

            _bullet.Update(totalSeconds);

            _asteroids.RemoveAll(asteroid => asteroid.IsDead);
        }
    }
}