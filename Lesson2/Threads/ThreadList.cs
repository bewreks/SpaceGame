using System;
using System.Collections.Generic;
using System.Threading;

namespace Lesson2.Threads
{
    /// <summary>
    /// Класс потокобезопасного списка
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ThreadList<T>
    {
        private ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();
        private List<T> _list;

        public ThreadList()
        {
            _list = new List<T>();
        }

        ~ThreadList()
        {
            _cacheLock.Dispose();
        }

        /// <summary>
        /// Удаление из списка всех объектов, подходящих под установленные параметры
        /// </summary>
        /// <param name="match"></param>
        public void RemoveAll(Predicate<T> match)
        {
            _cacheLock.EnterWriteLock();
            try
            {
                _list.RemoveAll(match);
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Добавление объекта в список
        /// </summary>
        /// <param name="obj"></param>
        public void Add(T obj)
        {
            _cacheLock.EnterWriteLock();
            try
            {
                _list.Add(obj);
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Добавление коллекции объектов в список
        /// </summary>
        /// <param name="collection"></param>
        public void Add(IEnumerable<T> collection)
        {
            _cacheLock.EnterWriteLock();
            try
            {
                _list.AddRange(collection);
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Удаление всех объектов из списка
        /// </summary>
        public void Clear()
        {
            _cacheLock.EnterWriteLock();
            try
            {
                _list.Clear();
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Применение делегата ко всем объектам списка 
        /// </summary>
        /// <param name="action"></param>
        public void ForEach(Action<T> action)
        {
            _cacheLock.EnterReadLock();
            try
            {
                _list.ForEach(action);
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Удаление объекта из списка
        /// </summary>
        /// <param name="obj"></param>
        public void Remove(T obj)
        {
            _cacheLock.EnterWriteLock();
            try
            {
                _list.Remove(obj);
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }
    }
}