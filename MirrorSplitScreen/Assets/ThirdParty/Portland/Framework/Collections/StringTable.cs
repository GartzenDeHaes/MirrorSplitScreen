using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

using Portland.Text;

namespace Portland.Collections
{
	//[StructLayout(LayoutKind.Explicit)]
	//public struct StringTableToken
	//{
	//	/// <summary></summary>
	//	[FieldOffset(0)]
	//	public byte C0;
	//	/// <summary></summary>
	//	[FieldOffset(1)]
	//	public byte C1;
	//	/// <summary></summary>
	//	[FieldOffset(2)]
	//	public byte C2;
	//	/// <summary></summary>
	//	[FieldOffset(3)]
	//	public byte C3;
	//	/// <summary></summary>
	//	[FieldOffset(4)]
	//	public byte C4;
	//	/// <summary></summary>
	//	[FieldOffset(5)]
	//	public byte C5;
	//	/// <summary></summary>
	//	[FieldOffset(6)]
	//	public ushort Index;
	//	/// <summary></summary>
	//	[FieldOffset(0)]
	//	public ulong Bits;

	//	public bool Equals(in StringTableToken stok)
	//	{
	//		return Bits == stok.Bits;
	//	}

	//	public override bool Equals(object obj)
	//	{
	//		if (obj is StringTableToken stok)
	//		{
	//			return Bits == stok.Bits;
	//		}
	//		return false;
	//	}

	//	public override int GetHashCode()
	//	{
	//		return (int)(Bits & 0xFFFFFFFF ^ Bits >> 32);
	//	}

	//	public override string ToString()
	//	{
	//		//return $"{StartsWith}({Index})";
	//		return $"{C0}{C1}{C2}{C3}{C4}{C5}:{Index}";
	//	}

	//	public static bool operator ==(in StringTableToken a, in StringTableToken b)
	//	{
	//		return a.Bits == b.Bits;// && a.HashCode == b.HashCode;
	//	}

	//	public static bool operator !=(in StringTableToken a, in StringTableToken b)
	//	{
	//		return a.Bits != b.Bits;// && a.HashCode != b.HashCode;
	//	}

	//	public static StringTableToken Create(string s, int index)
	//	{
	//		StringTableToken tok = new StringTableToken { Index = (ushort)index };
	//		for (int i = 0; i < s.Length && i < 6; i++)
	//		{
	//			tok.Bits |= ((ulong)s[i] << (i * 8));
	//		}

	//		Debug.Assert(tok.Index == index);
	//		Debug.Assert(s.Length == 0 || tok.C0 == s[0]);
	//		Debug.Assert(s.Length < 6 || tok.C5 == s[5]);

	//		return tok;
	//	}
	//}

	public struct StringTableToken : IEquatable<StringTableToken>
	{
		public int HashCode;
		public ushort Index;

		public bool Equals(in StringTableToken stok)
		{
			return Index == stok.Index;
		}

		public override bool Equals(object obj)
		{
			if (obj is StringTableToken stok)
			{
				return Index == stok.Index;
			}
			return false;
		}

		public bool Equals(StringTableToken other)
		{
			return Index == other.Index;
		}

		public override int GetHashCode()
		{
			//return Length << 16 | (int)Index;
			return HashCode;
		}

		public override string ToString()
		{
			//return $"{StartsWith}({Index})";
			return $"({Index})";
		}

		public static bool operator ==(in StringTableToken a, in StringTableToken b)
		{
			return a.Index == b.Index;
		}

		public static bool operator !=(in StringTableToken a, in StringTableToken b)
		{
			return a.Index != b.Index;
		}

		public static StringTableToken Create(in StringTable.StringItem s)
		{
			return new StringTableToken { Index = (ushort)s.Index, HashCode = s.HashCode };
		}
	}

	public sealed class StringTable : IStringProvider<StringTableToken>
	{
		public struct StringItem
		{
			public string Lexum;
			public int HashCode;
			public int Index;
		}

		Dictionary<int, int> _hashIndex = new Dictionary<int, int>();
		//List<string> _strings = new List<string>(64);
		Vector<StringItem> _strings = new Vector<StringItem>(128);

		public StringTable()
		{
			StringItem empty = new StringItem { Lexum = String.Empty, HashCode = StringHelper.HashMurmur32(String.Empty) };
			// Index zero is empty string
			_hashIndex.Add(empty.HashCode, 0);
			_strings.Add(empty);
		}

		public bool TryGet(string lexum, out StringTableToken ret)
		{
			int hash = StringHelper.HashMurmur32(lexum);
			int index;

			if (TryHash(lexum, hash, out index) || ScanForString(lexum, out index))
			{
				ret = StringTableToken.Create(new StringItem { Lexum = lexum, HashCode = hash, Index = index });
				//ret.Index = (short)index;
				//ret.Length = (short)lexum.Length;
				return true;
			}

			ret = default(StringTableToken);
			return false;
		}

		bool TryHash(string lexum, int hash, out int index)
		{
			if (_hashIndex.TryGetValue(hash, out index))
			{
				return _strings[index].Lexum.Equals(lexum);
			}

			return false;
		}

		bool ScanForString(string lexum, out int index)
		{
			for (index = 0; index < _strings.Count; index++)
			{
				if (_strings[index].Equals(lexum))
				{
					return true;
				}
			}

			return false;
		}

		public StringTableToken Get(string lexum)
		{
			if (TryGet(lexum, out StringTableToken ret))
			{
				return ret;
			}

			return Add(lexum);
		}

		public StringTableToken Get(int index)
		{
			return StringTableToken.Create(_strings[index]);
		}

		public string GetString(in StringTableToken token)
		{
			return _strings[token.Index].Lexum;
		}

		public int GetHashCode(in StringTableToken token)
		{
			return token.HashCode;
			//return _strings[token.Index].HashCode;
		}

		bool TryHash(StringBuilder lexum, out int index)
		{
			int hash = StringHelper.HashMurmur32(lexum);
			if (_hashIndex.TryGetValue(hash, out index))
			{
				return lexum.Equals(_strings[index]);
			}

			return false;
		}

		bool ScanForString(StringBuilder lexum, out int index)
		{
			for (index = 0; index < _strings.Count; index++)
			{
				if (_strings[index].Equals(lexum))
				{
					return true;
				}
			}

			return false;
		}

		public bool TryGet(StringBuilder buf, out StringTableToken ret)
		{
			int index;

			if (TryHash(buf, out index) || ScanForString(buf, out index))
			{
				ret = StringTableToken.Create(_strings[index]);
				//ret.Index = (short)index;
				//ret.Length = (short)buf.Length;
				return true;
			}

			ret = default(StringTableToken);
			return false;
		}

		public StringTableToken Get(StringBuilder buf)
		{
			if (TryGet(buf, out StringTableToken ret))
			{
				return ret;
			}

			return Add(buf.ToString());
		}

		public string GetString(StringBuilder buf)
		{
			if (TryGet(buf, out StringTableToken ret))
			{
				return GetString(ret);
			}

			return GetString(Add(buf.ToString()));
		}

		StringTableToken Add(string lexum)
		{
			int hash = StringHelper.HashMurmur32(lexum);
			var item = new StringItem { Lexum = lexum, HashCode = hash, Index = _strings.Count };
			int idx = _strings.AddAndGetIndex(item);
			
			Debug.Assert(idx == item.Index);

			var tok = StringTableToken.Create(item);
			//Debug.Assert(_strings[tok.Index] == lexum);

			//var tok = new StringTableToken()
			//{
			//	Index = (short)_strings.Add(lexum),
			//	Length = (short)lexum.Length,
			//};

			if (! _hashIndex.ContainsKey(hash))
			{
				_hashIndex.Add(hash, idx);
			}

			return tok;
		}

		public bool Contains(string lexum)
		{
			int hash = StringHelper.HashMurmur32(lexum);
			return TryHash(lexum, hash, out int _) || ScanForString(lexum, out int _);
		}

		public bool Contains(StringBuilder buf)
		{
			return TryHash(buf, out int _) || ScanForString(buf, out int _);
		}

		public bool AreEqual(in StringTableToken a, in StringTableToken b)
		{
			return a == b;
		}

		public StringView<StringTableToken> CreateStringView(in StringTableToken tok)
		{
			return new StringView<StringTableToken>(tok, this);
		}

		public StringView<StringTableToken> CreateStringView(string lexum)
		{
			return new StringView<StringTableToken>(Get(lexum), this);
		}

		public StringView<StringTableToken> CreateStringView(StringBuilder lexum)
		{
			return new StringView<StringTableToken>(Get(lexum), this);
		}
	}
}
