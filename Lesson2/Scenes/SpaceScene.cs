using System.Collections.Generic;
using Lesson2.Drawables;
using Lesson2.Drawables.BaseObjects;
using Lesson2.Loggers;

namespace Lesson2.Scenes
{
    public class SpaceScene : Scene
    {
        private List<GameObjects> _stars = new List<GameObjects>();
//        private Bullet _bullet;
        private List<Asteroid> _asteroids = new List<Asteroid>();
        private SpaceShip _ship;

        protected override void OnLoad()
        {
            Logger.Print("Space scene start load");

            _stars.Clear();
            _asteroids.Clear();

            for (var i = 0; i < 100; i++)
            {
                _stars.Add(GameObjectsFactory.CreateStar());
            }
            
            Logger.Print("Created {0} stars", _stars.Count);

            //_bullet = GameObjectsFactory.CreateBullet();

            for (var i = 0; i < 3; i++)
            {
                _asteroids.Add(GameObjectsFactory.CreateAsteroid());
            }

            _ship = GameObjectsFactory.CreateSpaceShip();

            AddDrawable(_stars);
            AddDrawable(_asteroids);
            AddDrawable(_ship);
//            AddDrawable(_bullet);

            AddUpdatable(_stars);
            AddUpdatable(_asteroids);
        }

        public override void Update(float totalSeconds)
        {
            base.Update(totalSeconds);

/*            foreach (var asteroid in _asteroids)
            {
                asteroid.Collision(_bullet);
            }

            _bullet.Update(totalSeconds);
*/
            _asteroids.RemoveAll(asteroid => asteroid.IsDead);
        }
    }
}