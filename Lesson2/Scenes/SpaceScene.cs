using System;
using System.Collections.Generic;
using System.Drawing;
using Lesson2.Drawables;
using Lesson2.Drawables.BaseObjects;
using Lesson2.Events;
using Lesson2.Loggers;
using Lesson2.States.Scenes.SpaceSceneStates;
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
        private readonly List<GameObjects> _stars = new List<GameObjects>();
        
        /// <summary>
        /// Очередь игровых объектов которые появляются
        /// </summary>
        private readonly Queue<GameObjects> _objects = new Queue<GameObjects>();

        /// <summary>
        /// Список астероидов
        /// Пополняется и удаляется на лету, поэтому используется потокобезопасный список
        /// </summary>
        private readonly ThreadList<Asteroid> _asteroids = new ThreadList<Asteroid>();

        /// <summary>
        /// Список пуль
        /// Пополняется и удаляется на лету, поэтому используется потокобезопасный список
        /// </summary>
        private readonly ThreadList<Bullet> _bullets = new ThreadList<Bullet>();
        
        /// <summary>
        /// Список аптечек
        /// Пополняется и удаляется на лету, поэтому используется потокобезопасный список
        /// </summary>
        private readonly ThreadList<Medic> _medics = new ThreadList<Medic>();

        /// <summary>
        /// Коспический корабль игрока
        /// </summary>
        private SpaceShip _ship;

        /// <summary>
        /// Объект игрока
        /// Пока не используется
        /// </summary>
        private Player _player;

        /// <summary>
        /// Состояние волны
        /// </summary>
        private WaveState _waveState;
        
        /// <summary>
        /// Состояние создания новой волны
        /// </summary>
        private readonly GenerateWaveState _generateWaveState = new GenerateWaveState();
        
        /// <summary>
        /// Состояние забрасывания нового объекта волны на сцену
        /// </summary>
        private readonly ThrowObjectWaveState _throwObjectWaveState = new ThrowObjectWaveState();
        
        /// <summary>
        /// Состояние ожидания генерации новой волны
        /// </summary>
        private readonly WaitForNewWaveState _waitForNewWaveState = new WaitForNewWaveState();

        protected override void OnUpdate(float delta)
        {
            _waveState?.Update(delta);
            
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
            
        }

        protected override void OnLoad()
        {
            Logger.Print("Space scene start load");

            EventManager<ThrowObjectWaveEventArgs>.AddEventListener(GameEvents.WAVE_NEXT_OBJECT, OnNextObject);
            EventManager.AddEventListener(GameEvents.SHOOT, Shoot);
            EventManager.AddEventListener(GameEvents.STAGE_COMPLETE ,OnStageCompleted);
            EventManager.AddEventListener(GameEvents.STAGE_GENERATED, OnStageGenerated);
            EventManager.AddEventListener(GameEvents.STAGE_GENERATE, OnStageGenerate);
            
            _generateWaveState.Init(_objects);
            _throwObjectWaveState.Init(_objects);
            _waitForNewWaveState.Init(_objects);

            _player = new Player();

            _stars.Clear();
            _asteroids.Clear();
            _bullets.Clear();

            for (var i = 0; i < 100; i++)
            {
                _stars.Add(GameObjectsFactory.CreateStar());
            }

            Logger.Print("Created {0} stars", _stars.Count);

            _ship = GameObjectsFactory.CreateSpaceShip();

            AddDrawable(_stars);
            AddDrawable(_ship);

            AddUpdatable(_stars);
         
            EventManager.DispatchEvent(GameEvents.STAGE_GENERATE);
        }

        protected override void OnDraw(Graphics graphics)
        {
            
        }

        /// <summary>
        /// Обработчик события генерации новой волны
        /// </summary>
        /// <param name="args"></param>
        private void OnStageGenerate(GameEventArgs args)
        {
            _waveState = _generateWaveState;
        }

        /// <summary>
        /// Обработчик события добавления нового объекта волны на сцену
        /// </summary>
        /// <param name="args"></param>
        private void OnNextObject(ThrowObjectWaveEventArgs args)
        {
            var obj = args.Object;
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
        private void OnStageCompleted(GameEventArgs arg)
        {
            _waveState = _waitForNewWaveState;
        }

        /// <summary>
        /// Обработчик события успешной генерации волны
        /// </summary>
        /// <param name="args"></param>
        private void OnStageGenerated(GameEventArgs args)
        {
            _waveState = _throwObjectWaveState;
        }

        // TODO: добавить отграничение скорострельность
        /// <summary>
        /// Обработчик события выстрела
        /// </summary>
        /// <param name="args"></param>
        private void Shoot(GameEventArgs args)
        {
            var bullet = GameObjectsFactory.CreateBullet(_ship.GetPoint());
            _bullets.Add(bullet);
            AddDrawable(bullet);
        }
        
    }
}