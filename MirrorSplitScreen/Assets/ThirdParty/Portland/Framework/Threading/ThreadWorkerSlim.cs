//using System;
//using System.Collections.Concurrent;
//using System.Diagnostics;

//using Portland.Collections;

//namespace Portland.Threading
//{
//	public sealed class ThreadWorkerSlim : ThreadServiceBaseSlim
//	{
//		struct WorkItem
//		{
//			public Action Work;
//			public Action OnComplete;
//			public Action<Exception> OnError;
//		}

//		private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

//		public Action<ThreadWorkerSlim> OnWorkComplete;

//		ConcurrentQueueSpin<WorkItem> _workToDo = new ConcurrentQueueSpin<WorkItem>();

//		public int QueueLength { get { return _workToDo.Count; } }

//		public ThreadWorkerSlim()
//		: base(nameof(ThreadWorkerSlim))
//		{
//			WaitTime = 5000;
//			Start();
//		}

//		public void EnqueueWork(Action work, Action oncomplete, Action<Exception> onError)
//		{
//			_workToDo.Enqueue(new WorkItem() { Work = work, OnComplete = oncomplete, OnError = onError });

//			SignalRunState();
//		}

//		protected override void RunServiceOne()
//		{
//			while (_workToDo.TryDequeue(out var work))
//			{
//				try
//				{
//					work.Work();
//					work.OnComplete?.Invoke();

//					OnWorkComplete?.Invoke(this);
//				}
//				catch (Exception ex)
//				{
//					Log.Error(ex);
//					work.OnError?.Invoke(ex);
//				}
//			}
//		}
//	}
//}
