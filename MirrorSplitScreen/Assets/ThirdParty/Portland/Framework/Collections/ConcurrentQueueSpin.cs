using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Portland.Collections
{
	internal sealed class ListNodeDouble<T>
	{
		public ListNodeDouble<T> Prev;
		public ListNodeDouble<T> Next;
		public T Item;
	}

	public sealed class ConcurrentQueueSpin<T>
	{
		SpinLock _lock;
		readonly ListNodeDouble<T> _head = new ListNodeDouble<T>();
		readonly ListNodeDouble<T> _tail = new ListNodeDouble<T>();

		int _count;

		public int Count { get { return _count; } }

		public ConcurrentQueueSpin()
		{
			_head.Next = _tail;
			_tail.Prev = _head;
		}

		public void Enqueue(T item)
		{
			//Debug.Assert(_head.Prev == null);
			//Debug.Assert(_tail.Next == null);

			bool gotlock = false;
			_lock.Enter(ref gotlock);

			var node = new ListNodeDouble<T>() { Item = item, Next = _tail, Prev = _tail.Prev };
			_tail.Prev.Next = node;
			_tail.Prev = node;

			_count++;

			if (gotlock) { _lock.Exit(); }
		}

		public bool TryDequeue(out T item)
		{
			//Debug.Assert(_head.Prev == null);
			//Debug.Assert(_tail.Next == null);

			bool found = true;
			bool gotlock = false;
			_lock.Enter(ref gotlock);

			if (_head.Next == _tail)
			{
				Debug.Assert(_count == 0);
				item = default;
				found = false;
			}
			else
			{
				item = _head.Next.Item;
				_head.Next = _head.Next.Next;
				_head.Next.Prev = _head;
				_count--;
			}

			if (gotlock) { _lock.Exit(); }

			return found;
		}

		public void Clear()
		{
			bool gotlock = false;
			_lock.Enter(ref gotlock);

			_head.Next = _tail;
			_tail.Prev = _head;
			_count = 0;

			if (gotlock) { _lock.Exit(); }
		}
	}
}
