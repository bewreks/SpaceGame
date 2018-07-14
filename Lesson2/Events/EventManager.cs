using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson2.Events
{
    public static class EventManager<T>
    {
        private static readonly Dictionary<string, Delegate> _handlers = new Dictionary<string, Delegate>();
        
        public static void AddEventListener(string eventType, Action<T> handler)
        {
            _handlers[eventType] = Delegate.Combine(GetDelegate(eventType), handler);
        }

        public static void RemoveEventListener(string eventType, Action<T> handler)
        {
            _handlers[eventType] = Delegate.Remove(GetDelegate(eventType), handler);
        }

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
        public static void AddEventListener(string eventType, Action<GameEventArgs> handler)
        {
            EventManager<GameEventArgs>.AddEventListener(eventType, handler);
        }

        public static void RemoveEventListener(string eventType, Action<GameEventArgs> handler)
        {
            EventManager<GameEventArgs>.RemoveEventListener(eventType, handler);
        }

        public static void DispatchEvent(string eventType)
        {
            EventManager<GameEventArgs>.DispatchEvent(eventType, new GameEventArgs());
        }
    }
}