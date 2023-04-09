using System;

namespace Portland.Threading
{
	public interface IThreadService : IDisposable
	{
		bool IsRunning { get; }
		//IThreadPool ThreadPool { get; set; }
		int WaitTime { get; set; }

		void Start();
		void Shutdown();
		void SignalRunState();
	}
}