using System.Collections.Generic;
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

            for (var i = 0; i < 100; i++)
            {
                _stars.Add(GameObjectsFactory.CreateStar());
            }

            _bullet = GameObjectsFactory.CreateBullet();

            for (var i = 0; i < 3; i++)
            {
                _asteroids.Add(GameObjectsFactory.CreateAsteroid());
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