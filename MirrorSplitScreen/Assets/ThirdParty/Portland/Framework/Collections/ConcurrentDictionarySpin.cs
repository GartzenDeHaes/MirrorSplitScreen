//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;

//using NetStack.Buffers;

//namespace Portland.Collections
//{
//	/// <summary>
//	/// dictionary for lite concurrancy
//	/// </summary>
//	public sealed class ConcurrentDictionarySpin<K, V> : IDisposable
//	{
//		//private long _count;
//		private SpinLock _lock = new SpinLock();
//		private readonly Dictionary<K, V> _dict = new Dictionary<K, V>();

//		public int Count
//		{
//			//get { return (int)Interlocked.Read(ref _count); }
//			get { return _dict.Count; }
//		}

//		public void Clear()
//		{
//			bool gotlock = false;
//			_lock.Enter(ref gotlock);
//			try
//			{
//				_dict.Clear();
//			}
//			finally
//			{
//				if (gotlock) _lock.Exit();
//			}
//		}

//		public bool ContainsKey(K key)
//		{
//			bool gotlock = false;
//			_lock.Enter(ref gotlock);
//			try
//			{
//				return _dict.ContainsKey(key);
//			}
//			finally
//			{
//				if (gotlock) _lock.Exit();
//			}
//		}

//		public void Add(K key, V value)
//		{
//			bool gotlock = false;
//			_lock.Enter(ref gotlock);
//			try
//			{
//				_dict.Add(key, value);
//				//Interlocked.Increment(ref _count);
//			}
//			finally
//			{
//				if (gotlock) _lock.Exit();
//			}
//		}

//		public bool TryAdd(K key, V value)
//		{
//			bool gotlock = false;
//			_lock.Enter(ref gotlock);
//			if (gotlock)
//			{
//				try
//				{
//					if (!_dict.ContainsKey(key))
//					{
//						_dict.Add(key, value);
//						//Interlocked.Increment(ref _count);
//						return true;
//					}
//				}
//				finally
//				{
//					if (gotlock) _lock.Exit();
//				}
//			}
//			return false;
//		}

//		public void Remove(K key)
//		{
//			bool gotlock = false;
//			_lock.Enter(ref gotlock);
//			try
//			{
//				_dict.Remove(key);
//			}
//			finally
//			{
//				if (gotlock) _lock.Exit();
//			}
//		}

//		public bool TryRemove(K key, out V value)
//		{
//			if (TryGetValue(key, out value))
//			{
//				Remove(key);
//				return true;
//			}

//			return false;
//		}

//		public bool TryGetValue(K key, out V value)
//		{
//			bool gotlock = false;
//			_lock.Enter(ref gotlock);
//			if (gotlock)
//			{
//				try
//				{
//					return _dict.TryGetValue(key, out value);
//				}
//				finally
//				{
//					if (gotlock) _lock.Exit();
//				}
//			}
//			value = default(V);
//			return false;
//		}

//		public V Get(K key)
//		{
//			bool gotlock = false;
//			_lock.Enter(ref gotlock);
//			try
//			{
//				if (_dict.TryGetValue(key, out var value))
//				{
//					return value;
//				}
//			}
//			finally
//			{
//				if (gotlock) _lock.Exit();
//			}

//			return default(V);
//		}

//		public K[] Keys()
//		{
//			K[] keys = new K[Count];
//			bool gotlock = false;
//			_lock.Enter(ref gotlock);
//			try
//			{
//				_dict.Keys.CopyTo(keys, 0);
//			}
//			finally
//			{
//				if (gotlock) _lock.Exit();
//			}

//			return keys;
//		}

//		public K[] Keys(ArrayPool<K> pool)
//		{
//			K[] keys = pool.Rent(Count);

//			bool gotlock = false;
//			_lock.Enter(ref gotlock);
//			try
//			{
//				_dict.Keys.CopyTo(keys, 0);
//			}
//			finally
//			{
//				if (gotlock) _lock.Exit();
//			}

//			return keys;
//		}

//		public V[] Values()
//		{
//			bool gotlock = false;
//			_lock.Enter(ref gotlock);
//			try
//			{
//				return _dict.Values.ToArray();
//			}
//			finally
//			{
//				if (gotlock) _lock.Exit();
//			}
//		}

//		public void ForEach(Action<K, V> dothis)
//		{
//			var keys = Keys();
//			for (int x = 0; x < keys.Length; x++)
//			{
//				if (TryGetValue(keys[x], out var item))
//				{
//					dothis(keys[x], item);
//				}
//			}
//		}

//		/// <summary>
//		/// Locks the collection for the entire interation
//		/// </summary>
//		public void ForEachValue(Action<V> dothis)
//		{
//			bool gotlock = false;
//			_lock.Enter(ref gotlock);
//			try
//			{
//				foreach (var val in _dict.Values)
//				{
//					dothis(val);
//				}
//			}
//			finally
//			{
//				if (gotlock) _lock.Exit();
//			}
//		}

//		public void Dispose()
//		{
//			_dict.Clear();
//		}
//	}
//}
