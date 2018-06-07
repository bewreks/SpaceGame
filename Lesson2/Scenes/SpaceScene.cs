using System;
using System.Collections.Generic;
using Lesson2.Drawables;
using Lesson2.Drawables.BaseObjects;
using Lesson2.Loggers;

namespace Lesson2.Scenes
{
    public class SpaceScene : Scene
    {
        private List<GameObjects> _stars = new List<GameObjects>();
        
        
        private ThreadList<Asteroid> _asteroids = new ThreadList<Asteroid>();
        private ThreadList<Bullet> _bullets = new ThreadList<Bullet>();
        private SpaceShip _ship;

        protected override void OnLoad()
        {
            Logger.Print("Space scene start load");

            EventManager.AddEventListener(EventManager.Events.ShootEvent, Shoot);

            _stars.Clear();
            _asteroids.Clear();
            _bullets.Clear();

            for (var i = 0; i < 100; i++)
            {
                _stars.Add(GameObjectsFactory.CreateStar());
            }

            Logger.Print("Created {0} stars", _stars.Count);

            for (var i = 0; i < 3; i++)
            {
                _asteroids.Add(GameObjectsFactory.CreateAsteroid());
            }

            _ship = GameObjectsFactory.CreateSpaceShip();

            AddDrawable(_stars);
            _asteroids.ForEach(asteroid => AddDrawable(asteroid));
            AddDrawable(_ship);

            AddUpdatable(_stars);
            _asteroids.ForEach(asteroid => AddUpdatable(asteroid));
        }

        private void Shoot(IEventArgs args)
        {
            var bullet = GameObjectsFactory.CreateBullet(_ship.GetPoint());
            _bullets.Add(bullet);
            AddDrawable(bullet);
        }

        public override void Update(float totalSeconds)
        {
            base.Update(totalSeconds);
            try
            {
                _asteroids.ForEach(asteroid => asteroid.Collision(_ship));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
            }

            try
            {
                _bullets.ForEach(bullet => bullet.Update(totalSeconds));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
            }

            _asteroids.RemoveAll(asteroid => asteroid.IsDead);
            _bullets.RemoveAll(bullet => bullet.IsDead);
        }
    }
}