using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;
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
        private StageCompleteState _stageCompleteState;

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
            
            _stageCompleteState = new StageCompleteDispatchState();

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
            _timer.Elapsed += TimerOnTick;

            AddDrawable(_stars);
            AddDrawable(_ship);

            AddUpdatable(_stars);
            
            LoadQueue();
        }

        private void TimerOnTick(object sender, EventArgs e)
        {
            if (_objects.Count == 0)
            {
                _stageCompleteState = _stageCompleteState.DoDispatch();

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
        }

        /// <summary>
        /// Обработчик события окончания волны
        /// </summary>
        /// <param name="arg"></param>
        private void OnStageCompleted(IEventArgs arg)
        {
            _timer.Stop();
            var timer = new Timer {Interval = 5000};
            timer.Elapsed += (sender, args) =>
            {
                timer.Stop();
                LoadQueue();
                _timer.Start();
                _stageCompleteState = new StageCompleteDispatchState();
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

    /// <summary>
    /// Состояние для запуска окончания волны
    /// </summary>
    public class StageCompleteDispatchState : StageCompleteState
    {
        public override StageCompleteState DoDispatch()
        {
            EventManager.DispatchEvent(EventManager.Events.StageCompletedEvent);
            return new StageCompleteDispatchedState();
        }
    }

    /// <summary>
    /// Состояние после запуска события окончания волны
    /// </summary>
    public class StageCompleteDispatchedState : StageCompleteState
    {
        public override StageCompleteState DoDispatch()
        {
            return this;
        }
    }

    /// <summary>
    /// Базовый класс состояния прохождения волны
    /// </summary>
    public abstract class StageCompleteState
    {
        /// <summary>
        /// Запуск события окончания волны
        /// </summary>
        /// <returns></returns>
        public abstract StageCompleteState DoDispatch();
    }
}