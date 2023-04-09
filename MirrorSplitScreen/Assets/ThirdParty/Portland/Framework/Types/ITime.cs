using System;

namespace Portland
{
	public interface ITime : IDisposable
	{
		float CurrentTime { get; }
		float CurrentTimeExact { get; }
		float DeltaTime { get; }
		float DeltaTimeMax { get; set; }
		int FrameCount { get; }
		bool IsHighResolution { get; }
		float TimeScale { get; }

		void FrameEnd();
		void FrameStart();
		void Pause();
		void Reset();
		void Resume();
	}
}
