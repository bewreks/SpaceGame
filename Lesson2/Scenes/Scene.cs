using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Lesson2.Drawables.BaseObjects;

namespace Lesson2.Scenes
{
    public abstract class Scene
    {
        // Отдельные списки для рисовки и обновления
        // Ведь нет смысла обновлять то, что не должно обновляться
        // Заполняются только во время загрузки
        private List<IDrawable> _toDraw;
        private List<IUpdatable> _toUpdate;

        private bool _loaded;

        // FPS
        private uint _count;
        private float _seconds;
        private DateTime _dateTime;

        public bool Loaded => _loaded;

        protected Scene()
        {
            _count = 0;
            _seconds = 0;
            _dateTime = DateTime.Now;
            _loaded = false;

            _toDraw = new List<IDrawable>();
            _toUpdate = new List<IUpdatable>();
        }

        // Перебераем весь список, но оставляем пользователю возможность
        // переопределить метод и обновлять данные как ему надо
        public virtual void Update(float totalSeconds)
        {
            if (!_loaded)
            {
                return;
            }
            
            foreach (var updatable in _toUpdate.ToArray())
            {
                updatable.Update(totalSeconds);
            }
        }

        // Перебераем весь список, но оставляем пользователю возможность
        // переопределить метод и рисовать как ему надо
        public virtual void Draw(Graphics graphics)
        {
            if (!_loaded)
            {
                return;
            }

            // Пока не знаю как правильно блокировать вне потока
            // поэтому изменение вместо коллекции отправляю массив из этой коллекции
            foreach (var drawable in _toDraw.ToArray())
            {
                drawable.Draw(graphics);
            }
        }

        // Нельзя вызывать Load дважды
        // Остальные методы, кроме добавления, не отработают, пока загрузка не будет завершена
        public void Load()
        {
            if (_loaded)
            {
                return;
            }
            
            _toDraw.Clear();
            _toUpdate.Clear();
            
            OnLoad();
            
            _loaded = true;
        }

        protected void AddUpdatable(IUpdatable updatable)
        {
            if (_loaded)
            {
                return;
            }
            _toUpdate.Add(updatable);
        }

        protected void AddUpdatable(IEnumerable<IUpdatable> collection)
        {
            if (_loaded)
            {
                return;
            }
            _toUpdate.AddRange(collection);
        }

        protected void AddDrawable(IDrawable updatable)
        {
            if (_loaded)
            {
                return;
            }
            _toDraw.Add(updatable);
        }

        protected void AddDrawable(IEnumerable<IDrawable> collection)
        {
            if (_loaded)
            {
                return;
            }
            _toDraw.AddRange(collection);
        }

        private void FPSCalc()
        {
            var dateTime = DateTime.Now;
            _seconds += (float)(dateTime - _dateTime).TotalSeconds;
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
        protected abstract void OnLoad();
    }
}