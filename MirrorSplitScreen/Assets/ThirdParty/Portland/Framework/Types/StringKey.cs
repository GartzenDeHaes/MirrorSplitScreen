using System;
using System.Runtime.CompilerServices;

using Portland.Text;

namespace Portland.Types
{
	[Serializable]
	public struct StringKey : IEquatable<StringKey>
	{
		public int HashCode;
		public readonly string Value;

		public StringKey(string s)
		{
			HashCode = StringHelper.HashLnv1a(s);
			Value = s;
		}

		public override int GetHashCode()
		{
			return HashCode;
		}

		public bool Equals(StringKey other)
		{
			return HashCode == other.HashCode;
		}

		public bool Equals(in StringKey other)
		{
			return HashCode == other.HashCode;
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override string ToString()
		{
			return Value;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in StringKey s1, string s)
		{
			return s1.Value == s;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in StringKey s1, string s)
		{
			return s1.Value != s;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in StringKey s1, in StringKey s)
		{
			return s1.HashCode == s.HashCode;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in StringKey s1, in StringKey s)
		{
			return s1.HashCode != s.HashCode;
		}

		/// <summary>Implicit Variant to string cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator string(in StringKey v) => v.Value;
		/// <summary>Implicit string to Variant cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator StringKey(string x) => new StringKey(x);

		public static String Empty = String.Empty;
	}
}
