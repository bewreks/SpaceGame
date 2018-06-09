using System;
using System.Collections.Generic;

namespace Lesson2.Events
{
    public static class EventManager
    {
        public enum Events
        {
            UpEvent,
            DownEvent,
            ShootEvent,
            MoveEvent,
            ChangeScoreEvent,
            ChangeEnergyEvent,
            StageCompletedEvent
        }

        public delegate void EventFunc(IEventArgs args);

        private static Dictionary<Events, EventFunc> _container = new Dictionary<Events, EventFunc>();

        public static void DispatchEvent(Events e)
        {
            DispatchEvent(e, new GameEventArgs());
        }

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