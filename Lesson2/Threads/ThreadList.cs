using System;
using System.Collections.Generic;
using System.Threading;
using Lesson2.Drawables.BaseObjects;
using Lesson2.Scenes;

namespace Lesson2
{
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