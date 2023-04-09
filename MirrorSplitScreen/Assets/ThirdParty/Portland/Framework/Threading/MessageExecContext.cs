using System;

namespace Portland.Threading
{
	public enum MessageExecContext : short
	{
		BACKGROUND = 0,      // Non-UI thread
		UI_UPDATE = 1,       // Dispatch in UI thread if available
	}
}
