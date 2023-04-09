using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Portland
{
	/// <summary>
	/// A 8 character long value type string.
	/// Set STRING8_UTF16 to use 16-bit chars (for 16 bytes total; otherwise 8 bytes total).
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Explicit)]
	public struct String8 : IEquatable<String8>
	{
		public const int MAX_LEN = 8;
		public static String8 Empty;
#if STRING8_UTF16
		/// <summary></summary>
		[FieldOffset(0)]
		public char c0;
		/// <summary></summary>
		[FieldOffset(2)]
		public char c1;
		/// <summary></summary>
		[FieldOffset(4)]
		public char c2;
		/// <summary></summary>
		[FieldOffset(6)]
		public char c3;
		/// <summary></summary>
		[FieldOffset(8)]
		public char c4;
		/// <summary></summary>
		[FieldOffset(10)]
		public char c5;
		/// <summary></summary>
		[FieldOffset(12)]
		public char c6;
		/// <summary></summary>
		[FieldOffset(14)]
		public char c7;

		/// <summary></summary>
		[FieldOffset(0)]
		public ulong Bits1;
		/// <summary></summary>
		[FieldOffset(8)]
		public ulong Bits2;
#else
		/// <summary></summary>
		[FieldOffset(0)]
		public byte c0;
		/// <summary></summary>
		[FieldOffset(1)]
		public byte c1;
		/// <summary></summary>
		[FieldOffset(2)]
		public byte c2;
		/// <summary></summary>
		[FieldOffset(3)]
		public byte c3;
		/// <summary></summary>
		[FieldOffset(4)]
		public byte c4;
		/// <summary></summary>
		[FieldOffset(5)]
		public byte c5;
		/// <summary></summary>
		[FieldOffset(6)]
		public byte c6;
		/// <summary></summary>
		[FieldOffset(7)]
		public byte c7;

		/// <summary></summary>
		[FieldOffset(0)]
		public ulong Bits64;
		/// <summary></summary>
		[FieldOffset(0)]
		public uint Bits1;
		/// <summary></summary>
		[FieldOffset(4)]
		public uint Bits2;
#endif

		/// <summary>String length</summary>
		public int Length
		{
			get
			{
				if (Bits2 == 0)
				{
					return (c3 != 0 ? 4 :
						(c2 != 0 ? 3 :
						(c1 != 0 ? 2 :
						(c0 != 0 ? 1 : 0))));
				}
				else
				{
					return (c7 != 0 ? 8 :
						(c6 != 0 ? 7 :
						(c5 != 0 ? 6 :
						(c4 != 0 ? 5 : 0))));
				}

				//return (c7 != 0 ? 8 :
				//	(c6 != 0 ? 7 : 
				//	(c5 != 0 ? 6 : 
				//	(c4 != 0 ? 5 : 
				//	(c3 != 0 ? 4 : 
				//	(c2 != 0 ? 3 : 
				//	(c1 != 0 ? 2 : 
				//	(c0 != 0 ? 1 : 0))))))));
			}
		}

		/// <summary></summary>
		public char this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
#if STRING8_UTF16
				switch (index)
				{
					case 0: return (char)c0;
					case 1: return (char)c1;
					case 2: return (char)c2;
					case 3: return (char)c3;
					case 4: return (char)c4;
					case 5: return (char)c5;
					case 6: return (char)c6;
					case 7: return (char)c7;
					default:
						throw new IndexOutOfRangeException();
				}
#else
				//int pos = index * 8;
				//ulong mask = (0xFFUL << pos);
				//ulong masked = mask & Bits64;
				//ulong ret = masked >> pos;
				//return (char)ret;
				return (char)(((0xFFUL << index * 8) & Bits64) >> (index * 8));
#endif
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
#if STRING8_UTF16
				switch (index)
				{
					case 0: c0 = (value); break;
					case 1: c1 = (value); break;
					case 2: c2 = (value); break;
					case 3: c3 = (value); break;
					case 4: c4 = (value); break;
					case 5: c5 = (value); break;
					case 6: c6 = (value); break;
					case 7: c7 = (value); break;
					default:
						throw new IndexOutOfRangeException();
				}
#else
				Bits64 &= ~(0xFFUL << index * 8);
				Bits64 |= ((ulong)value << (index * 8));
#endif
			}
		}

		/// <summary>
		/// Update string in-place
		/// </summary>
		/// <exception cref="ArgumentException">Throws exception if that argument is longer than 8 chars.</exception>
		private void Parse(string s, bool checkLen = true)
		{
			int len = s.Length;

			if (checkLen && s.Length > 8)
			{
				throw new ArgumentException("String8 string can only be up to 8 characters long");
			}

			var zero = CharToInternal('\0');
			c0 = len > 0 ? CharToInternal(s[0]) : zero;
			c1 = len > 1 ? CharToInternal(s[1]) : zero;
			c2 = len > 2 ? CharToInternal(s[2]) : zero;
			c3 = len > 3 ? CharToInternal(s[3]) : zero;
			c4 = len > 4 ? CharToInternal(s[4]) : zero;
			c5 = len > 5 ? CharToInternal(s[5]) : zero;
			c6 = len > 6 ? CharToInternal(s[6]) : zero;
			c7 = len > 7 ? CharToInternal(s[7]) : zero;
		}

		/// <summary></summary>
		public bool StartsWith(string str)
		{
			int len = str.Length;

			return (len <= Length) &&
				(char)c0 == (len > 0 ? str[0] : (char)c0) &&
				(char)c1 == (len > 1 ? str[1] : (char)c1) &&
				(char)c2 == (len > 2 ? str[2] : (char)c2) &&
				(char)c3 == (len > 3 ? str[3] : (char)c3) &&
				(char)c4 == (len > 4 ? str[4] : (char)c4) &&
				(char)c5 == (len > 5 ? str[5] : (char)c5) &&
				(char)c6 == (len > 6 ? str[6] : (char)c6) &&
				(char)c7 == (len > 7 ? str[7] : (char)c7);
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

			for (int x = 0; x < slen; x++)
			{
				if (this[len - 1 - x] != str[slen - 1 - x])
				{
					return false;
				}
			}

			return true;
		}

		/// <summary></summary>
		public int IndexOf(char ch)
		{
			if (ch == c0) return 0;
			if (ch == c1) return 1;
			if (ch == c2) return 2;
			if (ch == c3) return 3;
			if (ch == c4) return 4;
			if (ch == c5) return 5;
			if (ch == c6) return 6;
			if (ch == c7) return 7;

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
			for (int x = start; x < len; x++)
			{
				if (this[x] == str[0])
				{
					int y = 0;
					for (y = 1; y < slen; y++)
					{
						int x2 = x + y;
						if (x2 >= len)
						{
							break;
						}
						if (this[x2] != str[y])
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
		public String8 SubString(int start, int len = 32000)
		{
			String8 ret = String8.Empty;
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
		public String8 TrimStart()
		{
			int pos = 0;
			int len = Length;
			String8 ret = String8.Empty;

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

		/// <summary></summary>
		public String8 TrimEnd()
		{
			int len = Length;
			String8 ret = this;

			for (int x = len - 1; x >= 0; x--)
			{
				if (!Char.IsWhiteSpace(this[x]))
				{
					return ret;
				}

				ret[x] = '\0';
			}

			return ret;
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public String8 Trim()
		{
			return TrimEnd().TrimStart();
		}

		/// <summary>
		/// Simple hash function, may not work well with non-ASCII character sets.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			//return (c0 | c5 << 8 | c2 << 16 | c7 << 24)
			//	^ (c4 | c1 << 8 | c6 << 16 | c3 << 24);

			uint hashCode = c0;
			hashCode = hashCode * 31 + c1;
			hashCode = hashCode * 31 + c2;
			hashCode = hashCode * 31 +	c3;
			hashCode = hashCode * 31 + c4;
			hashCode = hashCode * 31 + c5;
			hashCode = hashCode * 31 + c6;
			hashCode = hashCode * 31 + c7;
			return (int)hashCode;
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

			for (int x = 0; x < len; x++)
			{
				if (this[x] != str[x])
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

			for (int x = 0; x < len; x++)
			{
				if (this[x] != str[x])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Equals
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(char c)
		{
			return c0 == c &&
				c1 == '\0';
		}

		/// <summary>
		/// Equals
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(in String8 s)
		{
			return Bits64 == s.Bits64;
		}

		public bool Equals(String8 other)
		{
			return Bits64 == other.Bits64;
		}

		/// <summary>
		/// Equals
		/// </summary>
		public override bool Equals(object obj)
		{
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
		public static bool operator ==(in String8 s1, String s2)
		{
			return s1.Equals(s2);
		}

		/// <summary>!=</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in String8 s1, String s2)
		{
			return !s1.Equals(s2);
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in String8 s1, char ch)
		{
			return s1.c0 == ch && s1.c1 == '\0';
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in String8 s1, char ch)
		{
			return s1.c0 != ch || s1.c1 != '\0';
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in String8 s1, StringBuilder buf)
		{
			return s1.Equals(buf);
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in String8 s1, StringBuilder buf)
		{
			return !s1.Equals(buf);
		}

		/// <summary>
		/// To a regular string
		/// </summary>
		public override string ToString()
		{
			StringBuilder buf = new StringBuilder();
			_ = c0 == '\0' ? buf : buf.Append((char)c0);
			_ = c1 == '\0' ? buf : buf.Append((char)c1);
			_ = c2 == '\0' ? buf : buf.Append((char)c2);
			_ = c3 == '\0' ? buf : buf.Append((char)c3);
			_ = c4 == '\0' ? buf : buf.Append((char)c4);
			_ = c5 == '\0' ? buf : buf.Append((char)c5);
			_ = c6 == '\0' ? buf : buf.Append((char)c6);
			_ = c7 == '\0' ? buf : buf.Append((char)c7);

			return buf.ToString();
		}

		/// <summary>
		/// Static constructor
		/// </summary>
		public static String8 From(string s)
		{
			String8 s8 = String8.Empty;
			s8.Parse(s);
			return s8;
		}

		/// <summary>
		/// Static constructor; take the first 8 chars
		/// </summary>
		public static String8 FromTruncate(string s)
		{
			String8 s8 = String8.Empty;
			s8.Parse(s, false);
			return s8;
		}

		/// <summary>
		/// Static constructor
		/// </summary>
		public static String8 From(StringBuilder sb)
		{
			String8 s = String8.Empty;
			int len = sb.Length;
			var zero = CharToInternal('\0');

			s.c0 = len > 0 ? CharToInternal(sb[0]) : zero;
			s.c1 = len > 1 ? CharToInternal(sb[1]) : zero;
			s.c2 = len > 2 ? CharToInternal(sb[2]) : zero;
			s.c3 = len > 3 ? CharToInternal(sb[3]) : zero;
			s.c4 = len > 4 ? CharToInternal(sb[4]) : zero;
			s.c5 = len > 5 ? CharToInternal(sb[5]) : zero;
			s.c6 = len > 6 ? CharToInternal(sb[6]) : zero;
			s.c7 = len > 7 ? CharToInternal(sb[7]) : zero;

			return s;
		}

		/// <summary>Implicit String8 to string cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator string(in String8 s) => s.ToString();
		/// <summary>Implicit string to String8 cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator String8(string x) => String8.From(x);

		/// <summary></summary>
		public static bool IsNullEmptyOrWhiteSpace(String8 s)
		{
			return s.Length == 0 ||
			(
				(s.c0 == '\0' || Char.IsWhiteSpace(InternalToChar(s.c0))) &&
				(s.c1 == '\0' || Char.IsWhiteSpace(InternalToChar(s.c1))) &&
				(s.c2 == '\0' || Char.IsWhiteSpace(InternalToChar(s.c2))) &&
				(s.c3 == '\0' || Char.IsWhiteSpace(InternalToChar(s.c3))) &&
				(s.c4 == '\0' || Char.IsWhiteSpace(InternalToChar(s.c4))) &&
				(s.c5 == '\0' || Char.IsWhiteSpace(InternalToChar(s.c5))) &&
				(s.c6 == '\0' || Char.IsWhiteSpace(InternalToChar(s.c6))) &&
				(s.c7 == '\0' || Char.IsWhiteSpace(InternalToChar(s.c7)))
			);
		}

		/// <summary></summary>
		public static bool IsWhiteSpace(String8 s)
		{
			return
			(
				(Char.IsWhiteSpace(InternalToChar(s.c0))) &&
				(s.c1 == '\0' || Char.IsWhiteSpace(InternalToChar(s.c1))) &&
				(s.c2 == '\0' || Char.IsWhiteSpace(InternalToChar(s.c2))) &&
				(s.c3 == '\0' || Char.IsWhiteSpace(InternalToChar(s.c3))) &&
				(s.c4 == '\0' || Char.IsWhiteSpace(InternalToChar(s.c4))) &&
				(s.c5 == '\0' || Char.IsWhiteSpace(InternalToChar(s.c5))) &&
				(s.c6 == '\0' || Char.IsWhiteSpace(InternalToChar(s.c6))) &&
				(s.c7 == '\0' || Char.IsWhiteSpace(InternalToChar(s.c7)))
			);
		}

#if STRING8_UTF16
		private static char InternalToChar(char ch)
		{
			return ch;
		}

		private static char CharToInternal(char ch)
		{
			return ch;
		}
#else
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static char InternalToChar(byte ch)
		{
			return (char)ch;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static byte CharToInternal(char ch)
		{
			return (byte)ch;
		}
#endif
	}
}
