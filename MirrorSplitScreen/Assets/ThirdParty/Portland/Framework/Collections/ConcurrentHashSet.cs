using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Portland.Collections
{
	public sealed class ConcurrentHashSet<K> : IDisposable
	{
		const int TimeOut = 2000;
		private HashSet<K> _dict = new HashSet<K>();
		private SemaphoreSlim _lock = new SemaphoreSlim(1);

		public int Count
		{
			get { return _dict.Count; }
		}

		public void Clear()
		{
			bool locked = _lock.Wait(TimeOut);
			_dict.Clear();
			if (locked) _lock.Release();
		}

		public bool TryAny(out K key)
		{
			bool locked = _lock.Wait(TimeOut);

			try
			{
				if (_dict.Count > 0)
				{
					key = _dict.First();
					return true;
				}
				key = default;
				return false;
			}
			finally
			{
				if (locked) _lock.Release();
			}
		}

		public bool TryTake(out K value)
		{
			bool locked = _lock.Wait(TimeOut);

			try
			{
				if (_dict.Count > 0)
				{
					value = _dict.First();
					_dict.Remove(value);
					return true;
				}
				value = default;
				return false;
			}
			finally
			{
				if (locked) _lock.Release();
			}
		}

		public bool Contains(K key)
		{
			bool locked = _lock.Wait(TimeOut);

			try
			{
				return _dict.Contains(key);
			}
			finally
			{
				if (locked) _lock.Release();
			}
		}

		public void Add(K key)
		{
			bool locked = _lock.Wait(TimeOut);

			try
			{
				_dict.Add(key);
			}
			finally
			{
				if (locked) _lock.Release();
			}
		}

		public bool TryAdd(K key)
		{
			bool locked = _lock.Wait(TimeOut);

			try
			{
				if (!_dict.Contains(key))
				{
					_dict.Add(key);
					return true;
				}

				return false;
			}
			finally
			{
				if (locked) _lock.Release();
			}
		}

		public void Remove(K key)
		{
			bool locked = _lock.Wait(TimeOut);

			try
			{
				_dict.Remove(key);
			}
			finally
			{
				if (locked) _lock.Release();
			}
		}

		public bool TryRemove(K key)
		{
			bool locked = _lock.Wait(TimeOut);

			try
			{
				return _dict.Remove(key);
			}
			finally
			{
				if (locked) _lock.Release();
			}
		}

		public K[] Values()
		{
			bool locked = _lock.Wait(TimeOut);

			try
			{
				return _dict.ToArray();
			}
			finally
			{
				if (locked) _lock.Release();
			}
		}

		public void ForEach(Action<K> act)
		{
			var keys = _dict.ToArray();

			for (int x = 0; x < keys.Length; x++)
			{
				act(keys[x]);
			}
		}

		public void Dispose()
		{
			_dict.Clear();
			_lock.Release();
		}
	}
}
