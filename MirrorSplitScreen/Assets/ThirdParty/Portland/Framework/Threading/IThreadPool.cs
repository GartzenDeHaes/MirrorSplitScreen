using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Portland.Threading
{
	/// <summary>
	/// The interface for simple thread pool access
	/// </summary>
	public interface IThreadPool : IDisposable
	{
		int MaxThreads { get; }
		int AvailableThreads { get; }
		int TotalQueueLength { get; }

		void Shutdown();
		bool QueueWorkItem(Action work, Action oncomplete = null, Action<Exception> onError = null);
	}
}
