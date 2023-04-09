using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portland.Collections
{
	public class ConcurrentRingBufferSpin<T>
	{
		RingBuffer<T> _array;
		SpinLock _lock = new SpinLock();

		public ConcurrentRingBufferSpin(int count, bool exceptionOnOverflow = true)
		{
			_array = new RingBuffer<T>(count, exceptionOnOverflow);
		}

		public void Add(T element)
		{
			bool locked = false;
			_lock.Enter(ref locked);

			try
			{
				_array.Add(element);
			}
			finally
			{
				if (locked) { _lock.Exit(); }
			}
		}

		public int Count
		{
			get
			{
				bool locked = false;
				_lock.Enter(ref locked);

				int count = _array.Count;

				if (locked) { _lock.Exit(); }

				return count;
			}
		}

		public T First
		{
			get
			{
				bool locked = false;
				_lock.Enter(ref locked);

				var val = _array.First;

				if (locked) { _lock.Exit(); }

				return val;
			}
		}

		public T Last
		{
			get
			{
				bool locked = false;
				_lock.Enter(ref locked);

				var val = _array.Last;

				if (locked) { _lock.Exit(); }

				return val;
			}
		}

		public bool IsFull
		{
			get
			{
				bool locked = false;
				_lock.Enter(ref locked);

				var val = _array.IsFull;

				if (locked) { _lock.Exit(); }

				return val;
			}
		}

		public bool TryRemoveFirst(out T value)
		{
			bool locked = false;
			_lock.Enter(ref locked);

			try
			{
				if (_array.Count > 0)
				{
					value = _array.First;
					_array.RemoveFirst();
					return true;
				}
			}
			finally
			{
				if (locked) { _lock.Exit(); }
			}

			value = default(T);
			return false;
		}
	}
}
