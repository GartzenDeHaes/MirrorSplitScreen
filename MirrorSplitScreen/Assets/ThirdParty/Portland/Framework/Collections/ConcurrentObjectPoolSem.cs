using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Portland.Collections
{
	public sealed class ConcurrentObjectPoolSem<T> : IDisposable where T : IPoolable, new()
	{
		private readonly SemaphoreSlim _lock = new SemaphoreSlim(1);
		private Stack<T> _pool = new Stack<T>();
		int _fillSize;

		/// <summary>
		/// Fills the pool with 8 objects.
		/// </summary>
		public ConcurrentObjectPoolSem(int fillSize = 8)
		{
			_fillSize = fillSize;
		}

		/// <summary>
		/// Fetch object from the pool; new objects are created if neccessary.
		/// </summary>
		/// <returns>Recycled object.</returns>
		public T Get()
		{
			if (_pool.Count == 0)
			{
				Fill();
			}

			_lock.Wait();

			var obj = _pool.Pop();
			
			_lock.Release();

			obj.Pool_Activate();
			return obj;
		}

		/// <summary>
		/// Return an object to the pool.
		/// </summary>
		/// <param name="obj">Object to be reused.</param>
		public void Release(T obj)
		{
			obj.Pool_Deactivate();

			_lock.Wait();

			_pool.Push(obj);

			_lock.Release();
		}

		void Fill()
		{
			_lock.Wait();

			for (int x = 0; x < _fillSize; x++)
			{
				_pool.Push(new T());
			}

			_lock.Release();
		}

		public void Dispose()
		{
			_pool.Clear();
			_lock.Dispose();
		}
	}
}
