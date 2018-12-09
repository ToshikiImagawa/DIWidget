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
    public class ListStack<T> : IEnumerable
    {
        private List<T> _list = new List<T>();

        public int Length => _list.Count;

        public T Last => _list.FirstOrDefault();

        public T Peek => _list.LastOrDefault();

        public T Pop()
        {
            var elm = Peek;
            Remove(elm);
            return elm;
        }

        public void Push(T elm)
        {
            _list.Add(elm);
        }

        public void Remove(T elm)
        {
            if (elm != null) _list.Remove(elm);
        }

        public List<T> Clear()
        {
            var tmp = _list;
            _list = new List<T>();
            return tmp;
        }

        public bool IsExist(T elm)
        {
            return _list.Count > 0 && _list.Contains(elm);
        }

        public IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}