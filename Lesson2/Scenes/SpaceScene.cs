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
        private Queue<GameObjects> _objects = new Queue<GameObjects>();

        private ThreadList<Asteroid> _asteroids = new ThreadList<Asteroid>();
        private ThreadList<Bullet> _bullets = new ThreadList<Bullet>();
        private ThreadList<Medic> _medics = new ThreadList<Medic>();

        private SpaceShip _ship;
        private Timer _timer;
        private int _count;

        private bool _waiting = false;

        private Player _player;

        public override void Update(float delta)
        {
            base.Update(delta);
            
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
                    medic.Update(delta);
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
                    bullet.Update(delta);
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

        public override void OnShown()
        {
            _timer.Start();
        }

        public override void OnLoad()
        {
            Logger.Print("Space scene start load");

            EventManager.AddEventListener(EventManager.Events.ShootEvent, Shoot);
            EventManager.AddEventListener(EventManager.Events.StageCompletedEvent, OnStageCompleted);

            _player = new Player();

            _count = 10;

            _stars.Clear();
            _asteroids.Clear();
            _bullets.Clear();

            for (var i = 0; i < 100; i++)
            {
                _stars.Add(GameObjectsFactory.CreateStar());
            }

            Logger.Print("Created {0} stars", _stars.Count);

            _ship = GameObjectsFactory.CreateSpaceShip();

            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += (sender, args) =>
            {
                if (_objects.Count == 0)
                {
                    if (!_waiting)
                    {
                        _waiting = true;
                        EventManager.DispatchEvent(EventManager.Events.StageCompletedEvent);
                    }

                    return;
                }
                
                var obj = _objects.Dequeue();
                switch (obj)
                {
                    case Asteroid _:
                        _asteroids.Add(obj as Asteroid);
                        AddUpdatable(obj);
                        break;
                    case Medic _:
                        _medics.Add(obj as Medic);
                        break;
                }

                AddDrawable(obj);
                
            };

            AddDrawable(_stars);
            AddDrawable(_ship);

            AddUpdatable(_stars);
            
            LoadQueue();
        }

        private void OnStageCompleted(IEventArgs arg)
        {
            _timer.Stop();
            var timer = new Timer {Interval = 5000};
            timer.Tick += (sender, args) =>
            {
                timer.Stop();
                LoadQueue();
                _timer.Start();
                _waiting = false;
            };
            timer.Start();
        }

        // TODO: добавить отграничение скорострельность
        private void Shoot(IEventArgs args)
        {
            var bullet = GameObjectsFactory.CreateBullet(_ship.GetPoint());
            _bullets.Add(bullet);
            AddDrawable(bullet);
        }

        private void LoadQueue()
        {
            var random = new Random();
            for (int i = 0; i < _count; i++)
            {
                GameObjects obj;
                var next = random.Next(100);
                if (next % 10 == 0)
                {
                    obj = GameObjectsFactory.CreateMedic();
                }
                else
                {
                    obj = GameObjectsFactory.CreateAsteroid();
                }

                _objects.Enqueue(obj);
            }
            _count++;
        }
        
    }
}