using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using Portland.Collections;

namespace Portland
{
	/// <summary>
	/// An 8-byte, 10 character ASCII 6-bit string (upper case only, case insensitive)
	/// </summary>
	[Serializable]
	//[StructLayout(LayoutKind.Explicit)]
	public struct String10 : IEquatable<String10>
	{
		public const int MAX_LEN = 10;
		public static String10 Empty;
		BitSet64 _bits;

		/// <summary>String length</summary>
		public int Length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (int)_bits.BitsAt(60, 4);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private set
			{
				_bits.SetBitsAt(60, 4, (uint)value);
			}
		}

		/// <summary></summary>
		public char this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (char)(_bits.BitsAt(index * 6, 6) + 32);
			}
			set
			{
				ushort ch = (ushort)value;
				if (value < 32)
				{
					value = (char)32;
				}
				if (value > 63)
				{
					ch = (ushort)(ch & ~0x20U);
				}
				//_bits.SetBitsAt(index * 6, 6, (ushort)((value & ~0x20) - 32));
				_bits.SetBitsAt(index * 6, 6, ch - 32U);
				if (index >= Length)
				{
					Length = index + 1;
				}
			}
		}

		/// <summary>
		/// Update string in-place
		/// </summary>
		/// <exception cref="ArgumentException">Throws exception if that argument is longer than 8 chars.</exception>
		private void Parse(string s, bool checkLen = true)
		{
			int len = s.Length;

			if (checkLen && s.Length > MAX_LEN)
			{
				throw new ArgumentException($"String10 string can only be up to {MAX_LEN} characters ('{s}')");
			}

			for (int i = 0; i < len; i++)
			{
				this[i] = s[i];
			}

			//Debug.Assert(len == Length);
		}

		/// <summary></summary>
		public bool StartsWith(string str)
		{
			int slen = str.Length;
			int len = Length;

			if (slen <= len)
			{
				char ch;
				for (int i = 0; i < slen; i++)
				{
					ch = str[i];
					if (this[i] != (char)((ch & ~0x20)))
					{
						return false;
					}
				}
			}
			else if (slen > len)
			{
				return false;
			}
			return true;
		}

		/// <summary></summary>
		public bool EndsWith(string str)
		{
			int slen = str.Length;
			int len = Length;

			if (slen > len)
			{
				return false;
			}

			char ch;
			for (int x = 0; x < slen; x++)
			{
				ch = str[slen - 1 - x];
				if (this[len - 1 - x] != (char)((ch & ~0x20)))
				{
					return false;
				}
			}

			return true;
		}

		/// <summary></summary>
		public int IndexOf(char ch)
		{
			ch = (char)((ch & ~0x20));
			int len = Length;
			for (int i = 0; i < len; i++)
			{
				if (this[i] == ch)
				{
					return i;
				}
			}

			return -1;
		}

		/// <summary></summary>
		public int IndexOf(string str, int start = 0)
		{
			int slen = str.Length;

			if (slen == 0)
			{
				return -1;
			}

			int len = Length;
			char ch;
			for (int x = start; x < len; x++)
			{
				if (this[x] == (str[0] & ~0x20))
				{
					int y = 0;
					for (y = 1; y < slen; y++)
					{
						int x2 = x + y;
						if (x2 >= len)
						{
							break;
						}
						ch = (char)((str[y] & ~0x20));
						if (this[x2] != ch)
						{
							break;
						}
					}

					if (y == slen)
					{
						return x;
					}
				}
			}

			return -1;
		}

		/// <summary></summary>
		public String10 SubString(int start, int len = 32000)
		{
			var ret = String10.Empty;
			int length = Length;
			int end = start + len;
			end = end > length ? length : end;
			int pos = 0;

			for (int x = start; x < end; x++)
			{
				ret[pos++] = this[x];
			}

			return ret;
		}

		/// <summary></summary>
		public String10 TrimStart()
		{
			int pos = 0;
			int len = Length;
			var ret = String10.Empty;

			for (int x = 0; x < len; x++)
			{
				char ch = this[x];
				if (pos == 0 && Char.IsWhiteSpace(ch))
				{
					continue;
				}
				ret[pos++] = ch;
			}

			return ret;
		}

		/// <summary>
		/// Simple hash function, may not work well with non-ASCII character sets.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			const uint fnv_prime = 0x811C9DC5;
			uint hash = 0;
			int i = 0;

			for (i = 0; i < MAX_LEN; i++)
			{
				hash *= fnv_prime;
				hash ^= (uint)_bits.BitsAt(i * 6, 6);
			}

			return unchecked((int)hash);
		}

		/// <summary>
		/// Equals
		/// </summary>
		public bool Equals(string str)
		{
			int len = Length;

			if (len != str.Length)
			{
				return false;
			}

			char ch;
			for (int x = 0; x < len; x++)
			{
				ch = str[x];
				if (this[x] != (char)((ch & ~0x20)))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Equals
		/// </summary>
		public bool Equals(StringBuilder str)
		{
			int len = Length;

			if (len != str.Length)
			{
				return false;
			}

			char ch;
			for (int x = 0; x < len; x++)
			{
				ch = str[x];
				if (this[x] != (char)((ch & ~0x20)))
				{
					return false;
				}
			}
			return true;
		}

		public bool Equals(String10 str)
		{
			return _bits == str._bits;
		}

		public bool Equals(in String10 str)
		{
			return _bits == str._bits;
		}

		/// <summary>
		/// Equals
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(char ch)
		{
			return this[0] == (char)((ch & ~0x20)) && Length == 1;
		}

		/// <summary>
		/// Equals
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(in String8 s)
		{
			int len = Length;

			if (len == s.Length)
			{
				char ch;
				for (int i = 0; i < len; i++)
				{
					ch = s[i];
					if (this[i] != (char)((ch & ~0x20)))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// Equals
		/// </summary>
		public override bool Equals(object obj)
		{
			if (obj is String10 s10)
			{
				return Equals(s10);
			}
			if (obj is String8 s)
			{
				return Equals(s);
			}
			if (obj is String str)
			{
				return Equals(str);
			}
			if (obj is StringBuilder buf)
			{
				return Equals(buf);
			}
			if (obj is char ch)
			{
				return Equals(ch);
			}

			return false;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in String10 s1, in String10 s2)
		{
			return s1._bits == s2._bits;
		}

		/// <summary>!=</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in String10 s1, in String10 s2)
		{
			return s1._bits != s2._bits;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in String10 s1, String s2)
		{
			return s1.Equals(s2);
		}

		/// <summary>!=</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in String10 s1, String s2)
		{
			return !s1.Equals(s2);
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in String10 s1, char ch)
		{
			return s1[0] == (ch & ~0x20) && s1.Length == 1;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in String10 s1, char ch)
		{
			return s1[0] != (ch & ~0x20) || s1.Length != 1;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in String10 s1, StringBuilder buf)
		{
			return s1.Equals(buf);
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in String10 s1, StringBuilder buf)
		{
			return !s1.Equals(buf);
		}

		/// <summary>
		/// To a regular string
		/// </summary>
		public override string ToString()
		{
			StringBuilder buf = new StringBuilder();
			for (int i = 0; i < Length; i++)
			{
				buf.Append(this[i]);
			}
			return buf.ToString();
		}

		public String10(string s)
		{
			_bits = new BitSet64();
			Parse(s);
		}

		/// <summary>
		/// Static constructor
		/// </summary>
		public static String10 From(string s)
		{
			var s10 = String10.Empty;
			s10.Parse(s);
			return s10;
		}

		/// <summary>
		/// Static constructor; take the first 8 chars
		/// </summary>
		public static String10 FromTruncate(string s)
		{
			var s10 = String10.Empty;
			s10.Parse(s, false);
			return s10;
		}

		/// <summary>
		/// Static constructor
		/// </summary>
		public static String10 From(StringBuilder sb)
		{
			var s = String10.Empty;
			int len = sb.Length;

			for (int i = 0; i < sb.Length; i++)
			{
				s[i] = sb[i];
			}

			return s;
		}

		/// <summary>Implicit String8 to string cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator string(in String10 s) => s.ToString();
		/// <summary>Implicit string to String8 cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator String10(string x) => String10.From(x);
	}
}
