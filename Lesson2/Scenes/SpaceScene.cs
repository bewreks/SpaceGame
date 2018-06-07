using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Lesson2.Drawables;
using Lesson2.Drawables.BaseObjects;
using Lesson2.Events;
using Lesson2.Loggers;
using Lesson2.Threads;

namespace Lesson2.Scenes
{
    public class SpaceScene : Scene
    {
        private List<GameObjects> _stars = new List<GameObjects>();

        private ThreadList<Asteroid> _asteroids = new ThreadList<Asteroid>();
        private ThreadList<Bullet> _bullets = new ThreadList<Bullet>();
        private ThreadList<Medic> _medics = new ThreadList<Medic>();

        private SpaceShip _ship;
        private Timer _timer;

        public int Score { get; set; }

        protected override void OnLoad()
        {
            Logger.Print("Space scene start load");

            EventManager.AddEventListener(EventManager.Events.ShootEvent, Shoot);
            EventManager.AddEventListener(EventManager.Events.ChangeScoreEvent, OnChangeScore);

            Score = 0;

            _stars.Clear();
            _asteroids.Clear();
            _bullets.Clear();

            for (var i = 0; i < 100; i++)
            {
                _stars.Add(GameObjectsFactory.CreateStar());
            }

            Logger.Print("Created {0} stars", _stars.Count);

            _ship = GameObjectsFactory.CreateSpaceShip();

            var random = new Random();
            _timer = new Timer();
            _timer.Interval = 2000;
            _timer.Tick += (sender, args) =>
            {
                GameObjects obj;
                var next = random.Next(100);
                if (next % 2 == 0)
                {
                    obj = GameObjectsFactory.CreateAsteroid();
                    _asteroids.Add(obj as Asteroid);
                    AddUpdatable(obj);
                }
                else
                {
                    obj = GameObjectsFactory.CreateMedic();
                    _medics.Add(obj as Medic);
                }

                AddDrawable(obj);
            };
            _timer.Start();

            AddDrawable(_stars);
            AddDrawable(_ship);

            AddUpdatable(_stars);
        }

        private void OnChangeScore(IEventArgs args)
        {
            Score += (args as ChangeScoreEvent).Score;
            Logger.Print("Score: {0}", Score);
        }

        // TODO: добавить отграничение скорострельность
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
                _medics.ForEach(medic =>
                {
                    medic.Collision(_ship);
                    medic.Update(totalSeconds);
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
            }

            try
            {
                _bullets.ForEach(bullet =>
                {
                    _asteroids.ForEach(asteroid => asteroid.Collision(bullet));
                    bullet.Update(totalSeconds);
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
            }

            _asteroids.RemoveAll(asteroid => asteroid.IsDead);
            _bullets.RemoveAll(bullet => bullet.IsDead);
            _medics.RemoveAll(medic => medic.IsDead);
        }
    }
}