using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using Lesson2.Drawables.BaseObjects;
using Lesson2.Loggers;

namespace Lesson2.Scenes
{
    public abstract class Scene
    {
        // Отдельные списки для рисовки и обновления
        // Ведь нет смысла обновлять то, что не должно обновляться
        // Заполняются только во время загрузки
        private ThreadList<IUpdatable> _toUpdate;
        private ThreadList<IDrawable> _toDraw;

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

            _toUpdate = new ThreadList<IUpdatable>();
            
            _toDraw = new ThreadList<IDrawable>();
            
        }

        // Перебераем весь список, но оставляем пользователю возможность
        // переопределить метод и обновлять данные как ему надо
        public virtual void Update(float totalSeconds)
        {
            if (!_loaded)
            {
                return;
            }


            _toUpdate.RemoveAll(DeleteIfDead);
            try
            {
                _toUpdate.ForEach(updatable => updatable.Update(totalSeconds));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
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
            _toDraw.RemoveAll(DeleteIfDead);
            try
            {
                _toDraw.ForEach(drawable => drawable.Draw(graphics));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
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

            var dateTime = DateTime.Now;

            _toDraw.Clear();
            _toUpdate.Clear();

            OnLoad();

            Logger.Print("Scene loaded with {0:f3} seconds", (DateTime.Now - dateTime).TotalSeconds);

            _loaded = true;
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

        protected void RemoveDrawable(IDrawable drawable)
        {
            _toDraw.Remove(drawable);
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

        private bool DeleteIfDead(IDrawable drawable)
        {
            return drawable is IKillable && (drawable as IKillable).IsDead;
        }

        private bool DeleteIfDead(IUpdatable updatable)
        {
            return updatable is IKillable && (updatable as IKillable).IsDead;
        }

        // Метод создания объектов сцены
        protected abstract void OnLoad();
    }
}