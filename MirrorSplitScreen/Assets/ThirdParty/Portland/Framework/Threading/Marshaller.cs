using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Portland.Collections;

namespace Portland.Threading
{
	public sealed class Marshaller<T>
	{
		ConcurrentRingBufferSpin<T> _messages;

		public Marshaller(int bufferSize, bool throwErrorOnOverflow)
		{
			_messages = new ConcurrentRingBufferSpin<T>(bufferSize, throwErrorOnOverflow);
		}

		public int Count
		{
			get { return _messages.Count; }
		}

		public T First
		{
			get { return _messages.First; }
		}

		public void Add(T msg)
		{
			_messages.Add(msg);
		}

		public bool TryRemove(out T msg)
		{
			if (_messages.TryRemoveFirst(out msg))
			{
				return true;
			}

			return false;
		}
	}
}
