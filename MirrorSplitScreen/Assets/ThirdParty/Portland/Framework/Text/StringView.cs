using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Portland.Collections;

namespace Portland.Text
{
	public class StringView<TTOKEN>
	{
		TTOKEN _tok;
		IStringProvider<TTOKEN> _provider;

		public int Length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return _provider.GetString(_tok).Length; }
		}

		public char this[int idx]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return _provider.GetString(_tok)[idx]; }
		}

		public StringView(TTOKEN tok, IStringProvider<TTOKEN> provider)
		{
			_tok = tok;
			_provider = provider;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int IndexOf(char ch)
		{
			return _provider.GetString(_tok).IndexOf(ch);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int IndexOf(string s)
		{
			return _provider.GetString(_tok).IndexOf(s);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool StartsWith(string pre)
		{
			return _provider.GetString(_tok).StartsWith(pre);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string SubString(int start)
		{
			return _provider.GetString(_tok).Substring(start);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string SubString(int start, int len)
		{
			return _provider.GetString(_tok).Substring(start, len);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToUpper()
		{
			return _provider.GetString(_tok).ToUpper();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToLower()
		{
			return _provider.GetString(_tok).ToLower();
		}

		public override bool Equals(object obj)
		{
			if (obj is StringView<TTOKEN> sv)
			{
				return _provider.AreEqual(_tok, sv._tok);
			}
			if (obj is TTOKEN st)
			{
				return _provider.AreEqual(_tok, st);
			}
			if (obj is String s)
			{
				return ToString().Equals(s);
			}
			return obj.Equals(ToString());
		}

		public override string ToString()
		{
			return _provider.GetString(_tok);
		}

		public override int GetHashCode()
		{
			return _provider.GetHashCode(_tok);
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(StringView<TTOKEN> s1, String s2)
		{
			return s1.ToString().Equals(s2);
		}

		/// <summary>!=</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(StringView<TTOKEN> s1, String s2)
		{
			return !s1.ToString().Equals(s2);
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(StringView<TTOKEN> s1, String8 s2)
		{
			return s2.Equals(s1.ToString());
		}

		/// <summary>!=</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(StringView<TTOKEN> s1, String8 s2)
		{
			return !s2.Equals(s1.ToString());
		}

		/// <summary>Implicit String8 to string cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator string(StringView<TTOKEN> s) => s.ToString();
		/// <summary>Implicit string to String8 cast</summary>
		//[MethodImpl(MethodImplOptions.AggressiveInlining)]
		//public static implicit operator S(string x) => _provider.Get(x);
	}
}
