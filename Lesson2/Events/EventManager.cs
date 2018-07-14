using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson2.Events
{
    public static class EventManager<T>
    {
        private static readonly Dictionary<string, Delegate> _handlers = new Dictionary<string, Delegate>();
        
        /// <summary>
        /// Добавление слушателя события
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public static void AddEventListener(string eventType, Action<T> handler)
        {
            _handlers[eventType] = Delegate.Combine(GetDelegate(eventType), handler);
        }

        /// <summary>
        /// Уделание слушателя события
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public static void RemoveEventListener(string eventType, Action<T> handler)
        {
            _handlers[eventType] = Delegate.Remove(GetDelegate(eventType), handler);
        }

        /// <summary>
        /// Отправка события с кастомными аргументами
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="args"></param>
        public static void DispatchEvent(string eventType, T args)
        {
            Action<T>[] invocationList;
            try
            {
                invocationList = GetDelegate(eventType).GetInvocationList().Cast<Action<T>>().ToArray();
            }
            catch (Exception e)
            {
                invocationList = new Action<T>[0];
            }
            
            foreach (var action in invocationList)
            {
                action.Invoke(args);
            }
        }

        private static Delegate GetDelegate(string eventType)
        {
            Delegate @delegate;
            try
            {
                @delegate = _handlers[eventType];
            }
            catch (Exception e)
            {
                @delegate = null;
            }

            return @delegate;
        }
    }

    public static class EventManager
    {
        /// <summary>
        /// Добавление слушателя события
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public static void AddEventListener(string eventType, Action<GameEventArgs> handler)
        {
            EventManager<GameEventArgs>.AddEventListener(eventType, handler);
        }

        /// <summary>
        /// Уделание слушателя события
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public static void RemoveEventListener(string eventType, Action<GameEventArgs> handler)
        {
            EventManager<GameEventArgs>.RemoveEventListener(eventType, handler);
        }

        /// <summary>
        /// Отправка события с базовыми аргументами
        /// </summary>
        /// <param name="eventType">Событие</param>
        public static void DispatchEvent(string eventType)
        {
            EventManager<GameEventArgs>.DispatchEvent(eventType, new GameEventArgs());
        }
    }
}