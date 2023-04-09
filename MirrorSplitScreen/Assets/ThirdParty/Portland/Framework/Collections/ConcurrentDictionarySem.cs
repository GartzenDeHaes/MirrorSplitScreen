using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Portland.Collections
{
	/// <summary>
	/// dictionary for lite concurrancy
	/// </summary>
	public sealed class ConcurrentDictionarySem<K, V> : IDisposable
	{
		//private long _count;
		private readonly SemaphoreSlim _lock = new SemaphoreSlim(1);
		private readonly Dictionary<K, V> _dict = new Dictionary<K, V>();

		public int Count
		{
			//get { return (int)Interlocked.Read(ref _count); }
			get { return _dict.Count; }
		}

		public void Clear()
		{
			_lock.Wait();
			_dict.Clear();
			_lock.Release();
		}

		public bool ContainsKey(K key)
		{
			_lock.Wait();
			try
			{
				return _dict.ContainsKey(key);
			}
			finally
			{
				_lock.Release();
			}
		}

		public void Add(K key, V value)
		{
			_lock.Wait();
			try
			{
				_dict.Add(key, value);
				//Interlocked.Increment(ref _count);
			}
			finally
			{
				_lock.Release();
			}
		}

		public bool TryAdd(K key, V value)
		{
			if (_lock.Wait(4000))
			{
				try
				{
					if (!_dict.ContainsKey(key))
					{
						_dict.Add(key, value);
						//Interlocked.Increment(ref _count);
						return true;
					}
				}
				finally
				{
					_lock.Release();
				}
			}
			return false;
		}

		public void Remove(K key)
		{
			_lock.Wait();
			try
			{
				_dict.Remove(key);
			}
			finally
			{
				_lock.Release();
			}
		}

		public bool TryRemove(K key, out V value)
		{
			if (TryGetValue(key, out value))
			{
				Remove(key);
				return true;
			}

			return false;
		}

		public bool TryGetValue(K key, out V value)
		{
			if (_lock.Wait(4000))
			{
				try
				{
					return _dict.TryGetValue(key, out value);
				}
				finally
				{
					_lock.Release();
				}
			}
			value = default(V);
			return false;
		}

		public V Get(K key)
		{
			_lock.Wait();
			try
			{
				if (_dict.TryGetValue(key, out var value))
				{
					return value;
				}
			}
			finally
			{
				_lock.Release();
			}

			return default(V);
		}

		public K[] Keys()
		{
			K[] keys = new K[Count];
			_lock.Wait();
			try
			{
				_dict.Keys.CopyTo(keys, 0);
			}
			finally
			{
				_lock.Release();
			}

			return keys;
		}

		public K[] Keys(K[] keys)
		{
			_lock.Wait();
			try
			{
				_dict.Keys.CopyTo(keys, 0);
			}
			finally
			{
				_lock.Release();
			}

			return keys;
		}

		public V[] Values()
		{
			_lock.Wait();
			try
			{
				return _dict.Values.ToArray();
			}
			finally
			{
				_lock.Release();
			}
		}

		public void ForEach(Action<K, V> dothis)
		{
			var keys = Keys();
			for (int x = 0; x < keys.Length; x++)
			{
				if (TryGetValue(keys[x], out var item))
				{
					dothis(keys[x], item);
				}
			}
		}

		/// <summary>
		/// Locks the collection for the entire interation
		/// </summary>
		public void ForEachValue(Action<V> dothis)
		{
			_lock.Wait();
			try
			{
				foreach (var val in _dict.Values)
				{
					dothis(val);
				}
			}
			finally
			{
				_lock.Release();
			}
		}

		public void Dispose()
		{
			_dict.Clear();
			_lock.Dispose();
		}
	}
}
