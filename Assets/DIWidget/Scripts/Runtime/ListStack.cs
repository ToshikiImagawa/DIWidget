using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DIWidget
{
    /// <summary>
    /// List stack
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <inheritdoc />
    public class ListStack<T> : IEnumerable<T>
    {
        private List<T> _list = new List<T>();

        public int Length => _list.Count;

        public T Last => _list.FirstOrDefault();

        public T Peek => _list.LastOrDefault();

        public T Pop()
        {
            var item = Peek;
            Remove(item);
            return item;
        }

        public void Push(T item)
        {
            _list.Add(item);
        }

        public void Remove(T item)
        {
            if (item != null) _list.Remove(item);
        }

        public List<T> Clear()
        {
            var tmp = _list;
            _list = new List<T>();
            return tmp;
        }

        public bool IsExist(T item)
        {
            return _list.Count > 0 && _list.Contains(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}