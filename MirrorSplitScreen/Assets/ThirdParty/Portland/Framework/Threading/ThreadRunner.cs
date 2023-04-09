//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Portland.Threading
//{
//	public sealed class ThreadRunner
//	{
//		Action OnRun;

//		public ThreadRunner(string name, int idleLoopMs, Action onrun)
//		: base(name)
//		{
//			WaitTime = idleLoopMs;
//			OnRun = onrun;
//		}

//		protected override void RunServiceOne()
//		{
//			OnRun();
//		}
//	}
//}
