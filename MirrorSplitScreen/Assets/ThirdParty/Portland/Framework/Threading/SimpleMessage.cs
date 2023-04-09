using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Portland.Collections;

#if UNITY_2018_1_OR_NEWER
using UnityEngine;
#else
#endif

using Portland.Text;

namespace Portland.Threading
{
	public sealed class SimpleMessage
	{
		public String10 MsgName;
		public Variant8 Arg;
		public object Data;
	}
}
