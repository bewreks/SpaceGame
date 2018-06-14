using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Lesson2.Drawables;
using Lesson2.Drawables.BaseObjects;
using Lesson2.Events;
using Lesson2.Loggers;
using Lesson2.Threads;

namespace Lesson2.Scenes
{
    /// <summary>
    /// Основная сцена игры
    /// </summary>
    public class SpaceScene : Scene
    {
        /// <summary>
        /// Список звезд
        /// Создается единожды во время загрузки и больше не меняется
        /// </summary>
        private List<GameObjects> _stars = new List<GameObjects>();
        
        /// <summary>
        /// Очередь игровых объектов которые появляются
        /// </summary>
        private Queue<GameObjects> _objects = new Queue<GameObjects>();

        /// <summary>
        /// Список астероидов
        /// Пополняется и удаляется на лету, поэтому используется потокобезопасный список
        /// </summary>
        private ThreadList<Asteroid> _asteroids = new ThreadList<Asteroid>();

        /// <summary>
        /// Список пуль
        /// Пополняется и удаляется на лету, поэтому используется потокобезопасный список
        /// </summary>
        private ThreadList<Bullet> _bullets = new ThreadList<Bullet>();
        
        /// <summary>
        /// Список аптечек
        /// Пополняется и удаляется на лету, поэтому используется потокобезопасный список
        /// </summary>
        private ThreadList<Medic> _medics = new ThreadList<Medic>();

        /// <summary>
        /// Коспический корабль игрока
        /// </summary>
        private SpaceShip _ship;
        
        /// <summary>
        /// Основной таймер игры
        /// </summary>
        private Timer _timer;
        
        /// <summary>
        /// Количество объектов в волне
        /// </summary>
        private int _count;

        /// <summary>
        /// Флаг определения, что событие о новой волне отправлено
        /// </summary>
        private bool _waiting = false;

        /// <summary>
        /// Объект игрока
        /// Пока не используется
        /// </summary>
        private Player _player;

        protected override void OnUpdate(float delta)
        {
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

        protected override void OnLoad()
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

        /// <summary>
        /// Обработчик события окончания волны
        /// </summary>
        /// <param name="arg"></param>
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

        protected override void OnDraw(Graphics graphics)
        {
            
        }

        // TODO: добавить отграничение скорострельность
        /// <summary>
        /// Обработчик события выстрела
        /// </summary>
        /// <param name="args"></param>
        private void Shoot(IEventArgs args)
        {
            var bullet = GameObjectsFactory.CreateBullet(_ship.GetPoint());
            _bullets.Add(bullet);
            AddDrawable(bullet);
        }

        /// <summary>
        /// Метод генерации новой волны
        /// </summary>
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