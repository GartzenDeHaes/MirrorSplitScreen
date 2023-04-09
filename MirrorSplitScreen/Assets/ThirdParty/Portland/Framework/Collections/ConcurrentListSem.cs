using System;
using System.Collections.Generic;
using System.Threading;

namespace Portland.Collections
{
	internal sealed class ListNode<T>
	{
		public ListNode<T> Next;
		public T Item;
	}

	public sealed class ConcurrentListSem<T> : /*IList<T>, IIndexed<T>,*/ IEnumerable<T>, IDisposable
	{
		readonly SemaphoreSlim _lock = new SemaphoreSlim(1);

		readonly ListNode<T> _head = new ListNode<T>();
		int _count;

		public ConcurrentListSem()
		{
		}

		public int Count
		{
			get
			{
				_lock.Wait();

				int count = _count;

				_lock.Release();

				return count;
			}
		}

		//public bool IsReadOnly
		//{
		//	get
		//	{
		//		return false;
		//	}
		//}

		//public T this[int index]
		//{
		//	get
		//	{
		//		bool gotlock = false;
		//		_lock.Enter(ref gotlock);

		//		T result;
		//		try
		//		{
		//			result = _list[index];
		//		}
		//		finally
		//		{
		//			if (gotlock) _lock.Exit();
		//		}

		//		return result;
		//	}
		//	set
		//	{
		//		bool gotlock = false;
		//		_lock.Enter(ref gotlock);

		//		try
		//		{
		//			_list[index] = value;
		//		}
		//		finally
		//		{
		//			if (gotlock) _lock.Exit();
		//		}
		//	}
		//}

		public void Add(T item)
		{
			var node = new ListNode<T>() { Item = item };

			_lock.Wait();

			node.Next = _head.Next;
			_head.Next = node;
			_count++;

			_lock.Release();
		}

		//public int IndexOf(T item)
		//{
		//	bool gotlock = false;
		//	_lock.Enter(ref gotlock);

		//	int result;
		//	try
		//	{
		//		result = _list.IndexOf(item);
		//	}
		//	finally
		//	{
		//		if (gotlock) _lock.Exit();
		//	}

		//	return result;
		//}

		//public void Insert(int index, T item)
		//{
		//	bool gotlock = false;
		//	_lock.Enter(ref gotlock);

		//	try
		//	{
		//		_list.Insert(index, item);
		//	}
		//	finally
		//	{
		//		if (gotlock) _lock.Exit();
		//	}
		//}

		//public void RemoveAt(int index)
		//{
		//	bool gotlock = false;
		//	_lock.Enter(ref gotlock);

		//	try
		//	{
		//		_list.RemoveAt(index);
		//	}
		//	finally
		//	{
		//		if (gotlock) _lock.Exit();
		//	}
		//}

		public void Clear()
		{
			_lock.Wait();

			_head.Next = null;
			_count = 0;

			_lock.Release();
		}

		public bool Contains(T item, Func<T, T, bool> compare)
		{
			_lock.Wait();

			bool found = false;

			var node = _head.Next;
			while (node != null)
			{
				if (compare(item, node.Item))
				{
					found = true;
					break;
				}
				node = node.Next;
			}

			_lock.Release();

			return found;
		}

		//public void CopyTo(T[] array, int arrayIndex)
		//{
		//	bool gotlock = false;
		//	_lock.Enter(ref gotlock);

		//	try
		//	{
		//		_list.CopyTo(array, arrayIndex);
		//	}
		//	finally
		//	{
		//		if (gotlock) _lock.Exit();
		//	}
		//}

		public bool Remove(T item, Func<T, T, bool> compare)
		{
			_lock.Wait();

			bool found = false;

			var prev = _head;
			var node = _head.Next;
			while (node != null)
			{
				if (compare(node.Item, item))
				{
					found = true;
					_count--;
					prev.Next = node.Next;
					break;
				}

				prev = node;
				node = node.Next;
			}

			_lock.Release();

			return found;
		}

		public bool TryTake(out T item)
		{
			bool found = true;

			_lock.Wait();

			var node = _head.Next;
			if (node == null)
			{
				found = false;
				item = default;
			}
			else
			{
				_head.Next = node.Next;
				item = node.Item;
				_count--;
			}

			_lock.Release();

			return found;
		}

		public IEnumerator<T> GetEnumerator()
		{
			_lock.Wait();

			try
			{
				var node = _head.Next;
				while (node != null)
				{
					yield return node.Item;
					node = node.Next;
				}
			}
			finally
			{
				_lock.Release();
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void ForEach(Action<T> act)
		{
			_lock.Wait();

			try
			{
				var node = _head.Next;
				while (node != null)
				{
					act(node.Item);
					node = node.Next;
				}
			}
			finally
			{
				_lock.Release();
			}
		}

		public void Dispose()
		{
			_head.Next = null;
			_count = 0;
			_lock.Dispose();
		}
	}
}

//using System;
//using System.Collections.Generic;
//using System.Threading;

//namespace Portland.Collections
//{
//	public sealed class ConcurrentListSem<T> : IList<T>, IIndexed<T>, IDisposable
//	{
//		private readonly SemaphoreSlim _lock;
//		private readonly List<T> _list;

//		public ConcurrentListSem()
//		{
//			_list = new List<T>();
//			_lock = new SemaphoreSlim(1);
//		}

//		public int Count
//		{
//			get
//			{
//				_lock.Wait();

//				int count;
//				try
//				{
//					count = _list.Count;
//				}
//				finally
//				{
//					_lock.Release();
//				}

//				return count;
//			}
//		}

//		public bool IsReadOnly
//		{
//			get
//			{
//				return false;
//			}
//		}

//		public T this[int index]
//		{
//			get
//			{
//				_lock.Wait();

//				T result;
//				try
//				{
//					result = _list[index];
//				}
//				finally
//				{
//					_lock.Release();
//				}

//				return result;
//			}
//			set
//			{
//				_lock.Wait();

//				try
//				{
//					_list[index] = value;
//				}
//				finally
//				{
//					_lock.Release();
//				}
//			}
//		}

//		public void Add(T item)
//		{
//			_lock.Wait();

//			try
//			{
//				_list.Add(item);
//			}
//			finally
//			{
//				_lock.Release();
//			}
//		}

//		public int IndexOf(T item)
//		{
//			_lock.Wait();

//			int result;
//			try
//			{
//				result = _list.IndexOf(item);
//			}
//			finally
//			{
//				_lock.Release();
//			}

//			return result;
//		}

//		public void Insert(int index, T item)
//		{
//			_lock.Wait();

//			try
//			{
//				_list.Insert(index, item);
//			}
//			finally
//			{
//				_lock.Release();
//			}
//		}

//		public void RemoveAt(int index)
//		{
//			_lock.Wait();

//			try
//			{
//				_list.RemoveAt(index);
//			}
//			finally
//			{
//				_lock.Release();
//			}
//		}

//		public void Clear()
//		{
//			_lock.Wait();

//			try
//			{
//				_list.Clear();
//			}
//			finally
//			{
//				_lock.Release();
//			}
//		}

//		public bool Contains(T item)
//		{
//			_lock.Wait();

//			bool result;
//			try
//			{
//				result = _list.Contains(item);
//			}
//			finally
//			{
//				_lock.Release();
//			}

//			return result;
//		}

//		public void CopyTo(T[] array, int arrayIndex)
//		{
//			_lock.Wait();

//			try
//			{
//				_list.CopyTo(array, arrayIndex);
//			}
//			finally
//			{
//				_lock.Release();
//			}
//		}

//		public bool Remove(T item)
//		{
//			_lock.Wait();

//			bool result;
//			try
//			{
//				result = _list.Remove(item);
//			}
//			finally
//			{
//				_lock.Release();
//			}

//			return result;
//		}

//		public IEnumerator<T> GetEnumerator()
//		{
//			_lock.Wait();

//			try
//			{
//				foreach (T value in _list)
//				{
//					yield return value;
//				}
//			}
//			finally
//			{
//				_lock.Release();
//			}
//		}

//		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
//		{
//			return GetEnumerator();
//		}

//		public void Dispose()
//		{
//			_list.Clear();
//			_lock.Dispose();
//		}
//	}
//}
