using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DIWidget
{
    public class ListQueue<T> : IEnumerable<T>
    {
        private readonly List<T> _list = new List<T>();

        public int Length => _list.Count;

        public T Last => _list.FirstOrDefault();

        public T Peek => _list.FirstOrDefault();

        public void Enqueue(T item)
        {
            _list.Add(item);
        }

        public T Dequeue()
        {
            var t = _list[0];
            _list.RemoveAt(0);
            return t;
        }

        public void Remove(T item)
        {
            if (item != null) _list.Remove(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}