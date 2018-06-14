using System;
using System.Collections.Generic;
using System.Drawing;
using Lesson2.Drawables.BaseObjects;
using Lesson2.States.Scenes;
using Lesson2.Threads;

namespace Lesson2.Scenes
{
    /// <summary>
    /// Базовый класс сцены
    /// </summary>
    public abstract class Scene
    {
        /// <summary>
        /// Список объектов для обновления
        /// </summary>
        private ThreadList<IUpdatable> _toUpdate;
        
        /// <summary>
        /// Список объектов для отрисовки
        /// </summary>
        private ThreadList<IDrawable> _toDraw;

        /// <summary>
        /// Текущее состояние сцены
        /// </summary>
        private SceneState _state;

        /// <summary>
        /// Проверка на 
        /// </summary>
        public bool IsLoaded => _state.IsLoaded;

        protected Scene()
        {
            _count = 0;
            _seconds = 0;
            _dateTime = DateTime.Now;

            _toUpdate = new ThreadList<IUpdatable>();

            _toDraw = new ThreadList<IDrawable>();

            _state = new SceneStateLoading();
        }

        /// <summary>
        /// Метод обновления списка объектов для обновления
        /// После завершения работы вызывает метод OnUpdate
        /// </summary>
        /// <param name="delta"></param>
        public void Update(float delta)
        {
            _state.Update(delta, _toUpdate, OnUpdate);
        }

        /// <summary>
        /// Метод отрисовки списка объектов для отрисовки
        /// После завершения работы вызывает метод OnDraw
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(Graphics graphics)
        {
            _state.Draw(graphics, _toDraw, OnDraw);
        }

        /// <summary>
        /// Метод загрузки сцены
        /// Отрабатывает лишь единожды для каждого обекта сцены
        /// После завершения работы вызывает метод OnLoad
        /// </summary>
        public void Load()
        {
            _state = _state.Load(_toDraw, _toUpdate, OnLoad);
        }

        /// <summary>
        /// Добавление объекта для обновления
        /// </summary>
        /// <param name="updatable"></param>
        protected void AddUpdatable(IUpdatable updatable)
        {
            _toUpdate.Add(updatable);
        }

        /// <summary>
        /// Добавление коллекции объектов для обновления
        /// </summary>
        /// <param name="collection"></param>
        protected void AddUpdatable(IEnumerable<IUpdatable> collection)
        {
            _toUpdate.Add(collection);
        }

        /// <summary>
        /// Добавление объекта для отрисовки
        /// </summary>
        /// <param name="drawable"></param>
        protected void AddDrawable(IDrawable drawable)
        {
            _toDraw.Add(drawable);
        }

        /// <summary>
        /// Добавление коллекции объектов для отрисовки
        /// </summary>
        /// <param name="collection"></param>
        protected void AddDrawable(IEnumerable<IDrawable> collection)
        {
            _toDraw.Add(collection);
        }

        #region FPS
        private uint _count;
        private float _seconds;
        private DateTime _dateTime;

        private void FPSCalc()
        {
            var dateTime = DateTime.Now;
            _seconds += (float) (dateTime - _dateTime).TotalSeconds;
            _dateTime = dateTime;

            _count++;

            if (_seconds >= 1)
            {
                Console.WriteLine(_count);
                _seconds = 0;
                _count = 0;
            }
        }
        #endregion
        
        /// <summary>
        /// Пользовательский метод загрузки, вызываемый по окончании работы основного метода загрузки
        /// </summary>
        protected abstract void OnLoad();

        /// <summary>
        /// Пользовательский метод обновления, вызываемый по окончании работы основного метода обновления
        /// </summary>
        /// <param name="delta"></param>
        protected abstract void OnUpdate(float delta);

        /// <summary>
        /// Пользовательский метод отрисовки, вызываемый по окончании работы основного метода отрисовки
        /// </summary>
        /// <param name="graphics"></param>
        protected abstract void OnDraw(Graphics graphics);

        /// <summary>
        /// Метод, вызываемый перед переключением сцены
        /// </summary>
        public abstract void OnShown();
    }
}