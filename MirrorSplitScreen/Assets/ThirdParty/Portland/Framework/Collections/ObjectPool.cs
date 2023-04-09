using System;
using System.Collections.Generic;

namespace Portland.Collections
{
	/// <summary>
	/// Simple object pool.
	/// </summary>
	/// <typeparam name="T">Must implement <see cref="IPoolable"/>.</typeparam>
	public sealed class ObjectPool<T> where T : IPoolable, new()
	{
		int _fillSize;
		private Stack<T> _pool = new Stack<T>();

		/// <summary>
		/// Fills the pool with 8 objects.
		/// </summary>
		public ObjectPool(int fillSize = 8)
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

			var obj = _pool.Pop();
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
			_pool.Push(obj);
		}

		private void Fill()
		{
			for (int x = 0; x < _fillSize; x++)
			{
				_pool.Push(new T());
			}
		}
	}
}
