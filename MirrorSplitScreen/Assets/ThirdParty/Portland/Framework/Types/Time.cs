using System;
using System.Diagnostics;

#if !UNITY_2017_1_OR_NEWER
namespace Portland
{
	public sealed class Time : ITime
	{
		Stopwatch _timer;
		float _frameLast;
		float _frameStart;
		int _frameCount;
		float _maxDeltaTime;
		//DateTime _simulationStart;
		//float _simulationTimeScale;

		public float CurrentTime
		{
			get { return _frameStart; }
		}

		public float CurrentTimeExact
		{
			get { return _timer.ElapsedMilliseconds / 1000f; }
		}

		public float DeltaTime
		{
			get
			{
				var dt = _frameStart - _frameLast;
				return dt > _maxDeltaTime ? _maxDeltaTime : dt;
			}
		}

		public float DeltaTimeMax
		{
			get { return _maxDeltaTime; }
			set { _maxDeltaTime = value; }
		}

		public int FrameCount
		{
			get { return _frameCount; }
		}

		public bool IsHighResolution
		{
			get { return Stopwatch.IsHighResolution; }
		}

		//public DateTime SimulationDateTimeStart
		//{
		//	get { return _simulationStart; }
		//	set { _simulationStart = value; }
		//}

		public float TimeScale
		{
			get { return 1f; }
			//	set { _simulationTimeScale = value; }
		}

		//public DateTime SimulationCurrent
		//{
		//	get { return _simulationStart + (new TimeSpan((long)(_frameStart * _simulationTimeScale))); }
		//}

		//public DateTime SimulationCurrentExact
		//{
		//	get { return _simulationStart + (new TimeSpan((long)(_timer.ElapsedTicks * _simulationTimeScale))); }
		//}

		public Time()
		{
			_timer = Stopwatch.StartNew();
			Reset();
		}

		public void FrameStart()
		{
			_frameCount++;
			_frameLast = _frameStart;
			_frameStart = CurrentTimeExact;
		}

		public void FrameEnd()
		{
		}

		public void Pause()
		{
			_timer.Stop();
		}

		public void Resume()
		{
			_timer.Start();
		}

		public void Reset()
		{
			_timer.Restart();
			_frameCount = 0;
			_frameLast = 0;
			_frameStart = 0;
			//_simulationStart = DateTime.Now;
			//_simulationTimeScale = 1f;
			_maxDeltaTime = 1f;
		}

		public void Dispose()
		{
			_timer.Stop();
		}
	}
}
#endif
