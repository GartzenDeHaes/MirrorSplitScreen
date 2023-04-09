using System;
using System.Collections;
using System.Collections.Generic;

namespace Portland.Collections
{
	public sealed class RingBuffer<T> : IEnumerable<T>
	{
		readonly T[] _elements;
		int _start;
		int _end;
		int _count;
		readonly int _capacity;
		readonly bool _errorOnOverflow;

		public RingBuffer(int count, bool errorOnOverflow = true)
		{
			_elements = new T[count];
			_capacity = count;
			_errorOnOverflow = errorOnOverflow;
		}

		public void Add(T element)
		{
			if (_count == _capacity)
			{
				if (_errorOnOverflow)
				{
					throw new ArgumentException("Ring buffer full");
				}
			}
			else
			{
				_count++;
			}

			_elements[_end] = element;
			_end = (_end + 1) % _capacity;
		}

		public void FastClear()
		{
			_start = 0;
			_end = 0;
			_count = 0;
		}

		public T this[int i] => _elements[(_start + i) % _capacity];
		public int Count => _count;
		public T First => _elements[_start];
		public T Last => _elements[(_start + _count - 1) % _capacity];
		public bool IsFull => _count == _capacity;

		public void RemoveFirst()
		{
			if (_count == 0)
			{
				throw new ArgumentException("RingBuffer empty");
			}

			_start = (_start + 1) % _capacity;
			_count -= 1;
		}

		public IEnumerator<T> GetEnumerator()
		{
			int counter = _start;

			while (counter != _end)
			{
				yield return _elements[counter];
				counter = (counter + 1) % _capacity;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
