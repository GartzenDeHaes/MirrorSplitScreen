using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using Priority_Queue;

namespace Portland.Threading
{
	public interface ISchedulerWorkItemReadOnly
	{
		string JobName { get; }
		bool IsScheduled { get; }
	}

	public enum ThreadRunlength
	{
		Short,
		Long,
	}

	public sealed class Scheduler : IScheduler
	{
#if !UNITY_5_3_OR_NEWER
		static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
#endif

		public class SchedulerWorkItem : ISchedulerWorkItemReadOnly
		{
			public int IntervalInMs;
			public float BaseTime;
			public bool Repeat;
			public bool IsRunning;
			public ThreadRunlength ThreadPoolPriority;
			public Action WorkUnit;
			public string JobName { get; set; }
			public bool IsScheduled { get; set; }

			public SchedulerWorkItem()
			{
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public float NextRuntimeInSeconds()
			{
				return BaseTime + IntervalInMs / 1000f;
			}
		}

		SimplePriorityQueue<SchedulerWorkItem, float> _queue = new SimplePriorityQueue<SchedulerWorkItem, float>();
		ITime _time;

		public IThreadPool ThreadPoolHighPriority { get; set; } = new NoThreadPool();
		public IThreadPool ThreadPool { get; set; } = new NoThreadPool();

		/// <summary>
		/// Defaults to 5 seconds.  Set to Timeout.Infinite for no polling.
		/// </summary>
		public int PollingTime
		{
			get; set;
		}

		public int Count
		{
			get { return _queue.Count; }
		}

		public float SecondsUntilNextJobRuns
		{
			get
			{
				if (_queue.Count == 0)
				{
					return Single.MaxValue;
				}
				float delta = _queue.Peek().NextRuntimeInSeconds() - _time.CurrentTimeExact;
				return delta;
			}
		}

		ThreadService _thread;

		public Scheduler(ITime time)
		{
			PollingTime = 1000 * 5;// Timeout.Infinite;
			_time = time;

			_thread = new ThreadService(nameof(Scheduler), RunServiceOne, null, null, System.Threading.ThreadPriority.AboveNormal);
		}

		public void Shutdown()
		{
			_queue.Clear();
			_thread.Shutdown();
		}

		/// <summary>
		/// Adjust run time by game time scale
		/// </summary>
		public ISchedulerWorkItemReadOnly RunOnce(string jobName, TimeSpan when, Action workItem, ThreadRunlength priority)
		{
			return RunOnce(jobName, (int)(when.TotalMilliseconds * _time.TimeScale), workItem, priority);
		}

		public ISchedulerWorkItemReadOnly RunOnce(string jobName, float runAfterThisManySeconds, Action workItem, ThreadRunlength priority)
		{
			return RunOnce(jobName, (int)(runAfterThisManySeconds * 1000f), workItem, priority);
		}

		public ISchedulerWorkItemReadOnly RunOnce(string jobName, int runAfterThisManyMilliseconds, Action workItem, ThreadRunlength priority)
		{
			SchedulerWorkItem item = new SchedulerWorkItem()
			{
				IntervalInMs = runAfterThisManyMilliseconds,
				BaseTime = _time.CurrentTimeExact,
				Repeat = false,
				WorkUnit = workItem,
				ThreadPoolPriority = priority,
				JobName = jobName
			};

			EnqueueWorkItem(item);

#if !UNITY_5_3_OR_NEWER
			Log.Trace("Added one time job {0} running ever {1}ms.", jobName, runAfterThisManyMilliseconds);
#endif
			return item;
		}

		public ISchedulerWorkItemReadOnly RunRepeat(string jobName, float repeatAfterThisManySeconds, Action workItem, ThreadRunlength priority)
		{
			return RunRepeat(jobName, (int)(repeatAfterThisManySeconds * 1000f), workItem, priority);
		}

		public ISchedulerWorkItemReadOnly RunRepeat(string jobName, int repeatAfterThisManyMilliseconds, Action workItem, ThreadRunlength priority)
		{
			SchedulerWorkItem item = new SchedulerWorkItem()
			{
				IntervalInMs = repeatAfterThisManyMilliseconds,
				BaseTime = _time.CurrentTimeExact,
				Repeat = true,
				ThreadPoolPriority = priority,
				WorkUnit = workItem,
				JobName = jobName
			};

			EnqueueWorkItem(item);

#if !UNITY_5_3_OR_NEWER
			Log.Trace("Added repeating time job {0} running every {1}ms.", jobName, repeatAfterThisManyMilliseconds);
#endif
			return item;
		}

		private void EnqueueWorkItem(SchedulerWorkItem item)
		{
			float nextRun = item.NextRuntimeInSeconds();

			_queue.Enqueue(item, nextRun);

			item.IsScheduled = true;

			_thread.SignalRunState();
		}

		public void CancelRun(ISchedulerWorkItemReadOnly item)
		{
			if (item != null)
			{
				var witem = (SchedulerWorkItem)item;
				_queue.TryRemove(witem);

				witem.IsScheduled = false;

#if !UNITY_5_3_OR_NEWER
				Log.Trace("Cancelled job {0}.", item.JobName);
#endif
			}
		}

		public void AlterRun(ISchedulerWorkItemReadOnly item, float runIntervalInSeconds)
		{
			var witem = (SchedulerWorkItem)item;
			_queue.Remove(witem);

			Debug.Assert(witem.IsScheduled);

			witem.IntervalInMs = (int)(runIntervalInSeconds * 1000f);
			_queue.Enqueue(witem, witem.NextRuntimeInSeconds());

#if !UNITY_5_3_OR_NEWER
			Log.Trace("Altered job {0} to run every {1}ms.", item.JobName, runIntervalInSeconds);
#endif
			_thread.SignalRunState();
		}

		public void AlterRun(ISchedulerWorkItemReadOnly item, int runIntervalInMilliSeconds)
		{
			var witem = (SchedulerWorkItem)item;
			_queue.Remove(witem);

			Debug.Assert(witem.IsScheduled);

			witem.IntervalInMs = runIntervalInMilliSeconds;
			_queue.Enqueue(witem, witem.NextRuntimeInSeconds());

#if !UNITY_5_3_OR_NEWER
			Log.Trace("Altered job {0} to run every {1}ms.", item.JobName, runIntervalInMilliSeconds);
#endif
			_thread.SignalRunState();
		}

		private void RunServiceOne()
		{
			if (_queue.Count == 0)
			{
				_thread.WaitTime = PollingTime;
				return;
			}

			SchedulerWorkItem witem = _queue.Peek();
			float now = _time.CurrentTimeExact;

			while (witem != null && witem.NextRuntimeInSeconds() <= now)
			{
				// Note, head item could have changed
				if (_queue.TryDequeue(out witem))
				{
					Debug.Assert(witem.IsScheduled);
					if (!witem.IsScheduled)
					{
						continue;
					}
					Debug.Assert(witem != _queue.Peek());

					//Log.Trace("Running job {0} at {1}s.", witem.JobName, now);

					if (now - witem.NextRuntimeInSeconds() > 10f)
					{
#if !UNITY_5_3_OR_NEWER
						Log.Warn($"Scheduler job {witem.JobName} {now - witem.NextRuntimeInSeconds()}s late");
#else
						UnityEngine.Debug.LogWarning($"Scheduler job {witem.JobName} {now - witem.NextRuntimeInSeconds()}s late");
#endif
					}

					witem.IsRunning = true;
					var cpitem = witem;
					var threadPool = cpitem.ThreadPoolPriority == ThreadRunlength.Short ? ThreadPoolHighPriority : ThreadPool;

					threadPool.QueueWorkItem(witem.WorkUnit, () =>
					{
						if (cpitem.Repeat && cpitem.IsRunning)
						{
							cpitem.BaseTime = now;
							_queue.Enqueue(cpitem, cpitem.NextRuntimeInSeconds());
							_thread.SignalRunState();
						}
						else
						{
							cpitem.IsScheduled = false;
						}
						cpitem.IsRunning = false;
					});
				}

				witem = _queue.Peek();
				now = _time.CurrentTimeExact;
			}

			if (_queue.Count == 0)
			{
				_thread.WaitTime = PollingTime;
			}
			else
			{
				witem = _queue.Peek();
				if (witem != null)
				{
					Debug.Assert(witem.IsScheduled);

					_thread.WaitTime = (int)((witem.NextRuntimeInSeconds() - now) * 1000) + 1;
					if (_thread.WaitTime < 0)
					{
						_thread.WaitTime = PollingTime;
					}
					//Log.Debug("Waiting {0}ms.", WaitTime);
				}
			}
		}

		public void Start()
		{
			_thread.Start();
		}
	}
}
