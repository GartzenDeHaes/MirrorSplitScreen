using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Portland.Text
{
	/// <summary>
	/// A string that is a sub string of another string.  Use for
	/// alloc-free parsing, for example.
	/// </summary>
	public struct StringPart
	{
		public string Source;
		public int Start;
		public int Stop;

		public int Length
		{
			get { return 1 + Stop - Start; }
		}

		public StringPart(string src)
		{
			Source = src;
			Start = 0;
			Stop = src.Length - 1;
		}

		public StringPart(string src, int start, int stop)
		{
			Source = src;
			Start = start;
			Stop = stop;
		}

		public char this[int idx]
		{
			get { return Source[Start + idx]; }
		}

		public StringPart Substring(int start)
		{
			return Substring(start, Length - start);
		}

		public StringPart Substring(int start, int len)
		{
			return new StringPart(Source, Start + start, Start + start + len - 1);
		}

		public int IndexOf(char ch, int startIndex = 0)
		{
			return Source.IndexOf(ch, Start + startIndex);
		}

		public int IndexOf(string sub, int startIndex = 0)
		{
			return Source.IndexOf(sub, Start + startIndex);
		}

		public bool StartsWith(string sub)
		{
			return this[0] == sub[0] && Source.IndexOf(sub) == Start;
		}

		public bool EndsWith(string sub)
		{
			int len = 1 + Stop - sub.Length;
			return len > -1 && Source.IndexOf(sub, len) > -1;
		}

		public bool EndsWith(char ch)
		{
			return Length > 0 && Source[Stop] == ch;
		}

		public bool Equals(string str)
		{
			int len = Length;
			if (str.Length != len)
			{
				return false;
			}
			for (int x = 0; x < len; x++)
			{
				if (this[x] != str[x])
				{
					return false;
				}
			}
			return true;
		}

		public bool Equals(StringPart str)
		{
			int len = Length;
			if (str.Length != len)
			{
				return false;
			}
			for (int x = 0; x < len; x++)
			{
				if (this[x] != str[x])
				{
					return false;
				}
			}
			return true;
		}

		public override bool Equals(object obj)
		{
			if (obj is StringPart sp)
			{
				return Equals(sp);
			}
			if (obj is String str)
			{
				return Equals(str);
			}

			return false;
		}

		public override int GetHashCode()
		{
			return StringHelper.HashMurmur32(Source, Start, Length);
		}

		public override string ToString()
		{
			return Source.Substring(Start, Length);
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(StringPart s1, string s2)
		{
			return s1.Equals(s2);
		}

		/// <summary>!=</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(StringPart s1, string s2)
		{
			return !s1.Equals(s2);
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(StringPart s1, StringPart s2)
		{
			return s1.Equals(s2);
		}

		/// <summary>!=</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(StringPart s1, StringPart s2)
		{
			return !s1.Equals(s2);
		}
	}
}
