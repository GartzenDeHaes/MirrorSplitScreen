using System;
using System.Runtime.CompilerServices;
using System.Text;

using Portland.Collections;

namespace Portland
{
	/// <summary>
	/// A 4 byte character ID
	/// </summary>
	[Serializable]
	public struct AsciiId4 : IEquatable<AsciiId4>
	{
		public static AsciiId4 Empty;

		public BitSet32 Bits;

		/// <summary>String length</summary>
		public int Length
		{
			get
			{
				if (Bits.RawBits == 0)
				{
					return 0;
				}
				if ((Bits.RawBits & 0xFF000000) != 0)
				{
					return 4;
				}
				if ((Bits.RawBits & 0x00FF0000) != 0)
				{
					return 3;
				}
				if ((Bits.RawBits & 0x0000FF00) != 0)
				{
					return 2;
				}
				return 1;
			}
		}

		/// <summary></summary>
		public char this[int index]
		{
			get
			{
				return (char)Bits.BitsAt(index * 8, 8);
			}
			set
			{
				Bits.SetBitsAt(index * 8, 8, (byte)value);
			}
		}

		/// <summary>
		/// The characters are the hash
		/// </summary>
		public override int GetHashCode()
		{
			uint hashCode = this[0];
			hashCode = hashCode * 31 + this[1];
			hashCode = hashCode * 31 + this[2];
			hashCode = hashCode * 31 + this[3];
			return (int)hashCode;
		}

		/// <summary>
		/// Equals
		/// </summary>
		public bool Equals(string str)
		{
			return str.Length < 5 &&
				this[0] == (str.Length > 0 ? str[0] : '\0') &&
				this[1] == (str.Length > 0 ? str[1] : '\0') &&
				this[2] == (str.Length > 0 ? str[2] : '\0') &&
				this[3] == (str.Length > 0 ? str[3] : '\0');
		}

		/// <summary>
		/// Equals
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(char c)
		{
			return this[0] == c &&
				this[1] == '\0';
		}

		/// <summary>
		/// Equals
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(in AsciiId4 s)
		{
			return Bits.RawBits == s.Bits.RawBits;
		}

		/// <summary>
		/// Equals
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(AsciiId4 s)
		{
			return Bits.RawBits == s.Bits.RawBits;
		}

		/// <summary>
		/// Equals
		/// </summary>
		public override bool Equals(object obj)
		{
			if (obj is String s)
			{
				return Equals(s);
			}
			if (obj is AsciiId4 s8)
			{
				return Equals(s8);
			}
			return false;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in AsciiId4 s1, in AsciiId4 s2)
		{
			return s1.Equals(s2);
		}

		/// <summary>!=</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in AsciiId4 s1, in AsciiId4 s2)
		{
			return !s1.Equals(s2);
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in AsciiId4 s1, String s2)
		{
			return s1.Equals(s2);
		}

		/// <summary>!=</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in AsciiId4 s1, String s2)
		{
			return !s1.Equals(s2);
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in AsciiId4 s1, char ch)
		{
			return s1.Equals(ch);
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in AsciiId4 s1, char ch)
		{
			return !s1.Equals(ch);
		}

		/// <summary>
		/// To a regular string
		/// </summary>
		public override string ToString()
		{
			StringBuilder buf = new StringBuilder();
			for (int x = 0; x < 4; x++)
			{
				char ch = this[x];
				if (ch == '\0')
				{
					break;
				}
				buf.Append(ch);
			}
			return buf.ToString();
		}

		/// <summary>
		/// constructor
		/// </summary>
		public AsciiId4(StringBuilder sb)
		{
			Bits.RawBits = Empty.Bits.RawBits;
			int len = sb.Length;

			if (len > 4)
			{
				throw new ArgumentException($"AsciiId4(StringBuilder) string too long {len} chars");
			}

			this[0] = len > 0 ? (sb[0]) : '\0';
			this[1] = len > 1 ? (sb[1]) : '\0';
			this[2] = len > 2 ? (sb[2]) : '\0';
			this[3] = len > 2 ? (sb[3]) : '\0';
		}

		/// <summary>
		/// Create a 4 ASCII char string
		/// </summary>
		///<exception cref = "ArgumentException" >If str is longer than 4</exception>
		public AsciiId4(string str)
		{
			Bits.RawBits = Empty.Bits.RawBits;
			int len = str.Length;

			if (len > 4)
			{
				throw new ArgumentException($"AsciiId4(String) string too long {len} chars");
			}

			this[0] = len > 0 ? (str[0]) : '\0';
			this[1] = len > 1 ? (str[1]) : '\0';
			this[2] = len > 2 ? (str[2]) : '\0';
			this[3] = len > 3 ? (str[3]) : '\0';
		}

		public AsciiId4(int rawAsciiBits)
		{
			Bits.RawBits = (uint)rawAsciiBits;
		}

		public int ToInt()
		{
			return (int)Bits.RawBits;
		}

		public static AsciiId4 ConstructStartsWith(string str)
		{
			int len = str.Length;
			AsciiId4 a = new AsciiId4();
			a[0] = len > 0 ? (str[0]) : '\0';
			a[1] = len > 1 ? (str[1]) : '\0';
			a[2] = len > 2 ? (str[2]) : '\0';
			a[3] = len > 3 ? (str[3]) : '\0';
			return a;
		}

		public static AsciiId4 ConstructStartsWith(StringBuilder str)
		{
			int len = str.Length;
			AsciiId4 a = new AsciiId4();
			a[0] = len > 0 ? (str[0]) : '\0';
			a[1] = len > 1 ? (str[1]) : '\0';
			a[2] = len > 2 ? (str[2]) : '\0';
			a[3] = len > 3 ? (str[3]) : '\0';
			return a;
		}

		/// <summary>Implicit String8 to string cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator string(AsciiId4 s) => s.ToString();
		/// <summary>Implicit string to String8 cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator AsciiId4(string x) => new AsciiId4(x);

		/// <summary></summary>
		public static bool IsNullEmptyOrWhiteSpace(AsciiId4 s)
		{
			return (s.Bits.RawBits == 0) ||
				(
					s.Bits.BitsAt(0, 8) < 33 && 
					s.Bits.BitsAt(8, 8) < 33 && 
					s.Bits.BitsAt(16, 8) < 33 && 
					s.Bits.BitsAt(24, 8) < 33
				);
		}

		/// <summary></summary>
		public static bool IsWhiteSpace(AsciiId4 s)
		{
			return (s.Bits.RawBits != 0) &&
				(
					s.Bits.BitsAt(0, 8) < 33 &&
					s.Bits.BitsAt(8, 8) < 33 &&
					s.Bits.BitsAt(16, 8) < 33 &&
					s.Bits.BitsAt(24, 8) < 33
				);
		}
	}
}
