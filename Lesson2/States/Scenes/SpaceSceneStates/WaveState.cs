using System.Collections.Generic;
using Lesson2.Drawables.BaseObjects;

namespace Lesson2.States.Scenes.SpaceSceneStates
{
    /// <summary>
    /// Базовый класс состояния волны
    /// </summary>
    public abstract class WaveState
    {
        /// <summary>
        /// Объект таймера
        /// </summary>
        protected float _timer;
        
        /// <summary>
        /// Очередь объектов волны
        /// </summary>
        protected Queue<GameObjects> _objects;

        protected WaveState()
        {
            _timer = 0;
        }

        /// <summary>
        /// Метод инициализации
        /// </summary>
        /// <param name="objects"></param>
        public void Init(Queue<GameObjects> objects)
        {
            _objects = objects;
        }

        /// <summary>
        /// Метод обновления
        /// </summary>
        /// <param name="deltaTime">Дельта времени между кадрами</param>
        /// <exception cref="GameException">Выбрасывается если одно из состояний не проинициализировано</exception>
        public void Update(float deltaTime)
        {
            if (_objects == null)
            {
                throw new GameException($"Состояние {GetType()} не инициализировано");
            }
            _timer += deltaTime;
            OnUpdate();
        }

        /// <summary>
        /// Пользовательский метод апдейта
        /// </summary>
        protected abstract void OnUpdate();
    }
}