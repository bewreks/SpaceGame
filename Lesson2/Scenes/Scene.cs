using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using Lesson2.Drawables.BaseObjects;
using Lesson2.States.Scenes;
using Lesson2.Threads;

namespace Lesson2.Scenes
{
    public abstract class Scene
    {
        // Отдельные списки для рисовки и обновления
        // Ведь нет смысла обновлять то, что не должно обновляться
        // Заполняются только во время загрузки
        private ThreadList<IUpdatable> _toUpdate;
        private ThreadList<IDrawable> _toDraw;

        private SceneState _state;

        // FPS
        private uint _count;
        private float _seconds;
        private DateTime _dateTime;

        public bool Loaded => State.Loaded;

        public ThreadList<IUpdatable> ToUpdate => _toUpdate;
        public ThreadList<IDrawable> ToDraw => _toDraw;

        public SceneState State
        {
            get => _state;
            set => _state = value;
        }

        protected Scene()
        {
            _count = 0;
            _seconds = 0;
            _dateTime = DateTime.Now;

            _toUpdate = new ThreadList<IUpdatable>();

            _toDraw = new ThreadList<IDrawable>();

            State = new SceneStateLoading(this);
        }

        // Перебераем весь список, но оставляем пользователю возможность
        // переопределить метод и обновлять данные как ему надо
        public virtual void Update(float delta)
        {
            State.Update(delta);
        }

        // Перебераем весь список, но оставляем пользователю возможность
        // переопределить метод и рисовать как ему надо
        public virtual void Draw(Graphics graphics)
        {
            State.Draw(graphics);
        }

        // Нельзя вызывать Load дважды
        // Остальные методы, кроме добавления, не отработают, пока загрузка не будет завершена
        public void Load()
        {
            State.Load();
        }

        public virtual void OnShown()
        {
            
        }

        protected void AddUpdatable(IUpdatable updatable)
        {
            _toUpdate.Add(updatable);
        }

        protected void AddUpdatable(IEnumerable<IUpdatable> collection)
        {
            _toUpdate.Add(collection);
        }

        protected void AddDrawable(IDrawable drawable)
        {
            _toDraw.Add(drawable);
        }

        protected void AddDrawable(IEnumerable<IDrawable> collection)
        {
            _toDraw.Add(collection);
        }

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

        // Метод создания объектов сцены
        public abstract void OnLoad();
        
    }
}