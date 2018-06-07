using System;

namespace Lesson2
{
    public interface IThreadList<T>
    {
        void ForEach(Action<T> action);
    }
}