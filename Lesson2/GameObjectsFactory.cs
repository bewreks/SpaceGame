using System;
using System.Drawing;
using Lesson2.Drawables;
using Lesson2.Drawables.BaseObjects;
using Lesson2.Scenes;

namespace Lesson2
{
    public static class GameObjectsFactory
    {
        private static Random rnd = new Random();

        public static GameObjects CreateStar()
        {
            var position = new Point(rnd.Next(0, Drawer.Width), rnd.Next(0, Drawer.Height));
            var direction = new Point(-rnd.Next(50, 500), 0);
            var size = new Size(3, 3);

            var type = rnd.Next(2) % 2 == 0 ? typeof(Star) : typeof(XStar);

            return (GameObjects) Activator.CreateInstance(type, position, direction, size);
        }

        public static Bullet CreateBullet(PointF position)
        {
            return new Bullet(new Point((int) position.X, (int) position.Y), new Point(500, 0), new Size(4, 1));
        }

        public static Asteroid CreateAsteroid()
        {
            var position = new Point(Drawer.Width, rnd.Next(0, Drawer.Height));
            var direction = new Point(-rnd.Next(50, 500), 0);
            var r = rnd.Next(5, 50);
            var size = new Size(r, r);
            return new Asteroid(position, direction, size);
        }

        public static SpaceShip CreateSpaceShip()
        {
            return new SpaceShip(new Point(0, 0), new Point(0, 5), new Size(50, 45));
        }

        public static Medic CreateMedic()
        {
            var position = new Point(Drawer.Width, rnd.Next(0, Drawer.Height));
            var direction = new Point(-200, 0);
            var size = new Size(10, 10);
            return new Medic(position, direction, size);
        }
    }
}