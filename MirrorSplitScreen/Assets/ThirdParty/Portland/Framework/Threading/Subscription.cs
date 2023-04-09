using System;
using System.Collections.Generic;
using System.Text;

using Portland.Text;

namespace Portland.Threading
{
	public sealed class Subscription<TMSG>
	{
		public string SubscriberUniqueKey;
		public String10 MessageName;
		public MessageExecContext ThreadContext;
		public Action<TMSG> Callback;
	}
}
