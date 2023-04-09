using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portland.Text
{
	public static class StringBufferExtensions
	{
		public static bool IsEqualTo(this StringBuilder buf, string str)
		{
			int len = buf.Length;
			if (len != str.Length)
			{
				return false;
			}
			for (int x = 0; x < len; x++)
			{
				if (buf[x] != str[x])
				{
					return false;
				}
			}

			return true;
		}

		public static bool IsEqualToIgnoreCase(this StringBuilder buf, string str)
		{
			int len = buf.Length;
			if (len != str.Length)
			{
				return false;
			}
			for (int x = 0; x < len; x++)
			{
				if (Char.ToUpperInvariant(buf[x]) != Char.ToUpperInvariant(str[x]))
				{
					return false;
				}
			}

			return true;
		}
	}
}
