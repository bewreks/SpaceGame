using System;
using System.Collections.Generic;
using System.Drawing;
using Lesson2.Drawables.BaseObjects;

namespace Lesson2.Scenes
{
    public abstract class Scene
    {
        // Отдельные списки для рисовки и обновления
        // Ведь нет смысла обновлять то, что не должно обновляться
        protected List<IDrawable> _toDraw;
        protected List<IUpdatable> _toUpdate;

        private uint _count;
        private float _seconds;
        private DateTime _dateTime;

        protected Scene()
        {
            _count = 0;
            _seconds = 0;
            _dateTime = DateTime.Now;

            _toDraw = new List<IDrawable>();
            _toUpdate = new List<IUpdatable>();
        }

        // Перебераем весь список, но оставляем пользователю возможность
        // переопределить метод и обновлять данные как ему надо
        public virtual void Update(float totalSeconds)
        {
            foreach (var updatable in _toUpdate)
            {
                updatable.Update(totalSeconds);
            }
        }

        // Перебераем весь список, но оставляем пользователю возможность
        // переопределить метод и рисовать как ему надо
        public virtual void Draw(Graphics graphics)
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

            foreach (var drawable in _toDraw)
            {
                drawable.Draw(graphics);
            }
        }

        // Метод создания объектов сцены
        public abstract void Load();
    }
}