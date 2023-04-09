using System;
using System.Collections.Concurrent;
using System.Diagnostics;

using Portland.Collections;

namespace Portland.Threading
{
	public sealed class ThreadWorker : IDisposable
	{
		struct WorkItem
		{
			public Action Work;
			public Action OnComplete;
			public Action<Exception> OnError;
		}

#if !UNITY_5_3_OR_NEWER
		private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
#endif
		public Action<ThreadWorker> OnWorkComplete;

		ConcurrentQueueSpin<WorkItem> _workToDo = new ConcurrentQueueSpin<WorkItem>();

		public int QueueLength { get { return _workToDo.Count; } }

		IThreadService _thread;

		public ThreadWorker(bool useSlim)
		{
			if (useSlim)
			{
				_thread = new ThreadServiceSlim(nameof(ThreadWorker), RunServiceOne);
			}
			else
			{
				_thread = new ThreadService(nameof(ThreadWorker), RunServiceOne);
			}
			_thread.WaitTime = 5000;
			_thread.Start();
		}

		public void EnqueueWork(Action work, Action oncomplete, Action<Exception> onError)
		{
			_workToDo.Enqueue(new WorkItem() { Work = work, OnComplete = oncomplete, OnError = onError });

			_thread.SignalRunState();
		}

		void RunServiceOne()
		{
			while (_workToDo.TryDequeue(out var work))
			{
				try
				{
					work.Work();
					work.OnComplete?.Invoke();

					OnWorkComplete?.Invoke(this);
				}
				catch (Exception ex)
				{
#if !UNITY_5_3_OR_NEWER
					Log.Error(ex);
#else
					UnityEngine.Debug.LogException(ex);
#endif
					work.OnError?.Invoke(ex);
				}
			}
		}

		public void Shutdown()
		{
			_thread.Shutdown();
		}

		public void Dispose()
		{
			_thread.Dispose();
		}
	}
}
