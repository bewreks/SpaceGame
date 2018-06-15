using System;
using System.Collections.Generic;

namespace Lesson2.Events
{
    public static class EventManager
    {
        public enum Events
        {
            /// <summary>
            /// Событие нажатия кнопки вверх
            /// </summary>
            UpEvent,
            /// <summary>
            /// Событие нажатия кнопки вниз
            /// </summary>
            DownEvent,
            /// <summary>
            /// Событие выстрела
            /// </summary>
            ShootEvent,
            /// <summary>
            /// Событие движения мыши
            /// Требуется MouseMoveGameEvent
            /// </summary>
            MoveEvent,
            /// <summary>
            /// Событие изменения счета
            /// </summary>
            ChangeScoreEvent,
            /// <summary>
            /// Событие обновления энергии
            /// </summary>
            ChangeEnergyEvent,
            /// <summary>
            /// Событие завершения волны
            /// </summary>
            StageCompletedEvent,
            /// <summary>
            /// Событие успешной генерации новой волны 
            /// </summary>
            StageGeneratedEvent
        }

        public delegate void EventFunc(IEventArgs args);

        private static Dictionary<Events, EventFunc> _container = new Dictionary<Events, EventFunc>();

        /// <summary>
        /// Отправка события с базовыми аргументами
        /// </summary>
        /// <param name="e">Событие</param>
        public static void DispatchEvent(Events e)
        {
            DispatchEvent(e, new GameEventArgs());
        }

        /// <summary>
        /// Отправка события с кастомными аргументами
        /// </summary>
        /// <param name="e"></param>
        /// <param name="args"></param>
        public static void DispatchEvent(Events e, IEventArgs args)
        {
            try
            {
                _container[e]?.Invoke(args);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Добавление слушателя события
        /// </summary>
        /// <param name="e"></param>
        /// <param name="func"></param>
        public static void AddEventListener(Events e, EventFunc func)
        {
            if (!_container.ContainsKey(e))
            {
                _container.Add(e, func);
            }
            else
            {
                _container[e] += func;
            }
        }
    }
}