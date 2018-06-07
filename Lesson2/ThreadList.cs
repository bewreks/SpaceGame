using System;
using System.Collections.Generic;
using System.Threading;
using Lesson2.Drawables.BaseObjects;
using Lesson2.Scenes;

namespace Lesson2
{
    public class ThreadList<T> : IThreadList<T>
    {
        private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
        private List<T> _list;

        public ThreadList()
        {
            _list = new List<T>();
        }

        public void RemoveAll(Predicate<T> match)
        {
            cacheLock.EnterWriteLock();
            try
            {
                _list.RemoveAll(match);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public void Add(T obj)
        {
            cacheLock.EnterWriteLock();
            try
            {
                _list.Add(obj);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public void Add(IEnumerable<T> collection)
        {
            cacheLock.EnterWriteLock();
            try
            {
                _list.AddRange(collection);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public void Clear()
        {
            cacheLock.EnterWriteLock();
            try
            {
                _list.Clear();
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public void ForEach(Action<T> action)
        {
            cacheLock.EnterReadLock();
            try
            {
                _list.ForEach(action);
            }
            finally
            {
                cacheLock.ExitReadLock();
            }
        }

        public void Remove(T obj)
        {
            cacheLock.EnterWriteLock();
            try
            {
                _list.Remove(obj);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }
    }
}