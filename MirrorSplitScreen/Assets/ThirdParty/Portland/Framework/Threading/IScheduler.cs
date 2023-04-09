using System;

namespace Portland.Threading
{
	public interface IScheduler
	{
		int Count { get; }
		int PollingTime { get; set; }
		float SecondsUntilNextJobRuns { get; }
		IThreadPool ThreadPool { get; set; }
		IThreadPool ThreadPoolHighPriority { get; set; }

		void Start();
		void Shutdown();

		/// <summary>
		/// Adjust run time by game time scale
		/// </summary>
		ISchedulerWorkItemReadOnly RunOnce(string jobName, TimeSpan when, Action workItem, ThreadRunlength priority);
		ISchedulerWorkItemReadOnly RunOnce(string jobName, float runAfterThisManySeconds, Action workItem, ThreadRunlength priority);
		ISchedulerWorkItemReadOnly RunOnce(string jobName, int runAfterThisManyMilliseconds, Action workItem, ThreadRunlength priority);
		ISchedulerWorkItemReadOnly RunRepeat(string jobName, float repeatAfterThisManySeconds, Action workItem, ThreadRunlength priority);
		ISchedulerWorkItemReadOnly RunRepeat(string jobName, int repeatAfterThisManyMilliseconds, Action workItem, ThreadRunlength priority);

		void CancelRun(ISchedulerWorkItemReadOnly item);
		void AlterRun(ISchedulerWorkItemReadOnly item, float runIntervalInSeconds);
		void AlterRun(ISchedulerWorkItemReadOnly item, int runIntervalInMilliSeconds);
	}
}
