namespace BattleshipsCore.Server
{
    public class DropoutStack<T> where T : class
    {
        private T[] _items;
        private int _top;
        private int _count;

        public DropoutStack(int capacity)
        {
            _items = new T[capacity];
            _top = 0;
            _count = 0;
        }

        public void Push(T item)
        {
            if (_count < _items.Length) _count++;

            _items[_top] = item;
            _top = (_top + 1) % _items.Length;
        }

        public T? Pop()
        {
            if (_count == 0) return null;

            _top = (_items.Length + _top - 1) % _items.Length;
            _count--;

            return _items[_top];
        }

        public T? Peek()
        {
            if (_count == 0) return null;

            var idx = (_items.Length + _top - 1) % _items.Length;

            return _items[idx];
        }

        public void Clear()
        {
            Array.Clear(_items, 0, _items.Length);
        }
    }
}
