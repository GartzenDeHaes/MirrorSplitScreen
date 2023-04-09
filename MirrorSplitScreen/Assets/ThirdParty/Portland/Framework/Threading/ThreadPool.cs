using System;
using System.Collections.Concurrent;
using System.Diagnostics;

using Portland.Collections;

namespace Portland.Threading
{
	/// <summary>
	/// Exposes access to the .NET/Mono thread pool via IThreadPool
	/// </summary>
	public sealed class SystemThreadPool : IThreadPool
	{
#if !UNITY_5_3_OR_NEWER
		private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
#endif
		public int TotalQueueLength { get { return 0; } }

		public SystemThreadPool(int maxThreads, int maxIoThreads)
		{
			System.Threading.ThreadPool.SetMaxThreads(maxThreads, maxIoThreads);
		}

		public void Shutdown()
		{
		}

		public int MaxThreads
		{
			get
			{
				int maxWorkers, maxIO;
				System.Threading.ThreadPool.GetMaxThreads(out maxWorkers, out maxIO);
				return maxWorkers;
			}
		}

		public int AvailableThreads
		{
			get
			{
				int availableWorkers, availableIO;
				System.Threading.ThreadPool.GetAvailableThreads(out availableWorkers, out availableIO);
				return availableWorkers;
			}
		}

		public bool QueueWorkItem(Action callback, Action oncomplete, Action<Exception> onError)
		{
			return System.Threading.ThreadPool.QueueUserWorkItem(o => RunOne(callback, oncomplete));
		}

		private void RunOne(Action callback, Action oncomplete)
		{
			try
			{
				callback.Invoke();
				oncomplete?.Invoke();
			}
			catch (Exception ex)
			{
#if !UNITY_5_3_OR_NEWER
				Log.Error(ex);
#else
				UnityEngine.Debug.LogException(ex);
#endif
			}
		}

		public void Dispose()
		{
		}
	}

	public sealed class NoThreadPool : IThreadPool
	{
		public int MaxThreads { get { return 1; } }
		public int AvailableThreads { get { return 1; } }
		public int TotalQueueLength { get { return 0; } }

		public void Dispose()
		{
		}

		public void Shutdown()
		{
		}

		public bool QueueWorkItem(Action callback, Action oncomplete, Action<Exception> onError)
		{
			try
			{
				callback.Invoke();
				oncomplete?.Invoke();
			}
			catch (Exception ex)
			{
				onError?.Invoke(ex);
			}
			return true;
		}
	}

	public sealed class ThreadPoolSingleThreaded : IThreadPool
	{
		public int MaxThreads { get { return 1; } }
		public int AvailableThreads { get { return 1; } }
		public int TotalQueueLength { get { return _thread.QueueLength; } }

		ThreadWorker _thread;

		public ThreadPoolSingleThreaded()
		{
			_thread = new ThreadWorker(false);
		}

		public void Shutdown()
		{
			_thread.Shutdown();
		}

		public void Dispose()
		{
			_thread.Shutdown();
			_thread.Dispose();
			_thread = null;
		}

		public bool QueueWorkItem(Action callback, Action oncomplete, Action<Exception> onError)
		{
			_thread.EnqueueWork(callback, oncomplete, onError);
			return true;
		}
	}

	struct ThreadActions
	{
		public Action Work;
		public Action OnWorkDone;
		public Action<Exception> OnError;
	}

	public sealed class ThreadPoolSlim : /*ThreadServiceBase,*/ IThreadPool
	{
		public int TotalQueueLength { get { return Work.Count; } }

		ConcurrentListSem<ThreadWorker> Ready = new ConcurrentListSem<ThreadWorker>();
		ConcurrentQueueSpin<ThreadActions> Work = new ConcurrentQueueSpin<ThreadActions>();

		public int MaxThreads
		{
			get; set;
		}

		public int AvailableThreads
		{
			get { return Ready.Count; }
		}

		public ThreadPoolSlim(int maxThreads)
		//: base(nameof(ThreadPoolLite))
		{
			//WaitTime = 5000;

			MaxThreads = (maxThreads < 1) ? 1 : maxThreads;

			for (int x = 0; x < MaxThreads; x++)
			{
				Ready.Add(new ThreadWorker(false) { OnWorkComplete = OnThreadWorkDone });
			}

			//Start();
		}

		public /*override*/ void Shutdown()
		{
			//base.Shutdown();

#if UNITY_2019_4_OR_NEWER
			while (Work.TryDequeue(out _))
			{
			}
#else
			Work.Clear();
#endif
		}

		void OnThreadWorkDone(ThreadWorker t)
		{
			Ready.Add(t);

			//Debug.Assert(Ready.Count <= MaxThreads);

			//SignalRunState();
			RunServiceOne();
		}

		public bool QueueWorkItem(Action work, Action oncomplete, Action<Exception> onError)
		{
			Work.Enqueue(new ThreadActions() { Work = work, OnWorkDone = oncomplete, OnError = onError });

			//SignalRunState();
			RunServiceOne();

			return true;
		}

		/*protected override*/
		void RunServiceOne()
		{
			//Debug.Assert(Ready.Count <= MaxThreads);

			while (Work.Count > 0 && Ready.Count > 0)
			{
				if (Ready.TryTake(out var thread))
				{
					if (Work.TryDequeue(out var work))
					{
						thread.EnqueueWork(work.Work, work.OnWorkDone, work.OnError);
					}
					else
					{
						Ready.Add(thread);
						//Debug.Assert(Ready.Count < MaxThreads);
					}
				}
			}
		}

		public void Dispose()
		{
			Shutdown();
		}
	}
}
