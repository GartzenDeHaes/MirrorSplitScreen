using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

using Portland.Collections;
using Portland.Mathmatics;
using Portland.Text;

using Half = Portland.Mathmatics.Half;

namespace Portland
{
	/// <summary>
	/// Variant type
	/// </summary>
	public enum VariantType : short
	{
		/// <summary></summary>
		Null = 0,
		/// <summary></summary>
		Int = 1,
		/// <summary></summary>
		Float = 2,
		/// <summary></summary>
		StringIntern = 3,
		/// <summary></summary>
		Vec3 = 4,
		/// <summary></summary>
		Bool = 5,
		/// <summary>Uses the StringTable</summary>
		String = 6,
		/// <summary></summary>
		Vec3i = 7,
		/// <summary></summary>
		Long = 8,
	}

	/// <summary>
	/// A 64-bit variant value type.  Strings longer than 3 characters are stored in a static StringTable.
	/// </summary>
	public struct Variant8 : IEquatable<Variant8>
	{
		[Serializable]
		[StructLayout(LayoutKind.Explicit)]
		struct VariantData
		{
			[FieldOffset(0)]
			public float AsFloat;

			[FieldOffset(4)]
			public Half AsFloat2;

			[FieldOffset(0)]
			public int AsInt;

			[FieldOffset(4)]
			public short AsInt2;

			[FieldOffset(0)]
			public short AsShort;

			[FieldOffset(2)]
			public short AsShort2;

			[FieldOffset(4)]
			public short AsShort3;

			[FieldOffset(0)]
			public Vector3h AsVector3;

			//[FieldOffset(0)]
			//public String3 AsString;

			[FieldOffset(0)]
			public StringTableToken AsStrInTab;

			[FieldOffset(0)]
			public BitSet64 AsBits;

			[FieldOffset(6)]
			public VariantType TypeIs;
		}

		/// <summary>
		/// Ok to use this table else where
		/// </summary>
		public readonly static StringTable StrTab;

		/// <summary>Helper to avoid using new</summary>
		public readonly static Variant8 Zero;

		static Variant8()
		{
			StrTab = new StringTable();
			StrTab.Get(String.Empty);     // index 0 will be empty string
			Zero = new Variant8();
		}

		private VariantData _value;

		public VariantType TypeIs { get { return _value.TypeIs; } }

		public bool IsNumeric { get { return _value.TypeIs == VariantType.Int || _value.TypeIs == VariantType.Float; } }

		/// <summary>Constructor</summary>
		public Variant8(int i)
		{
			_value = Zero._value;
			Set(i);
		}

		/// <summary>Constructor</summary>
		public Variant8(float i)
		{
			_value = Zero._value;
			Set(i);
		}

		/// <summary>Constructor</summary>
		public Variant8(bool i)
		{
			_value = Zero._value;
			Set(i);
		}

		/// <summary>Constructor</summary>
		public Variant8(in String8 i)
		{
			_value = Zero._value;
			Set(i);
		}

		///// <summary>Constructor</summary>
		//public Variant8(String3 i)
		//{
		//	_value = Zero._value;
		//	Set(i);
		//}

		/// <summary>Uses String8 if length of the string is less than eight.</summary>
		public Variant8(string str)
		{
			_value = Zero._value;

			//if (str.Length > String3.MAX_LEN)
			//{
			_value.AsStrInTab = StrTab.Get(str);
			_value.TypeIs = VariantType.String;
			//_value.AsInt2 = (short)str[0];
			//}
			//else
			//{
			//	Set(String3.From(str));
			//}
		}

		/// <summary>Constructor</summary>
		public Variant8(StringBuilder str)
		{
			_value = Zero._value;

			//if (str.Length > String3.MAX_LEN)
			//{
			_value.AsStrInTab = StrTab.Get(str);
			_value.TypeIs = VariantType.String;
			//_value.AsInt2 = str.Length > 0 ? (short)str[0] : (short)0;
			//}
			//else
			//{
			//	Set(String3.From(str));
			//}
		}

		/// <summary>Constructor</summary>
		public Variant8(ulong bits)
		{
			_value = Zero._value;
			// high order byte reserved for type data
			_value.AsBits.RawBits = (~(0xFFUL << 56)) & bits;
		}

		/// <summary>The data is 12 bytes long, so we can store 2 ints there</summary>
		public Variant8(int a, short b)
		{
			_value = Zero._value;
			Set(a, b);
		}

		/// <summary>floats too</summary>
		public Variant8(int a, Half b)
		{
			_value = Zero._value;
			Set(a, b);
		}

		/// <summary>Constructor</summary>
		public Variant8(char ch)
		{
			_value = Zero._value;
			Set(ch);
		}

		public Variant8(Vector3h vec)
		{
			_value = Zero._value;
			Set(vec);
		}

		public Variant8(Vector3i vec)
		{
			_value = Zero._value;
			Set(vec);
		}

		public Variant8(Vector3 vec)
		{
			_value = Zero._value;
			Set(new Vector3h(vec));
		}

		//public Variant8(in Variant16 v)
		//{
		//	_value = Zero._value;
		//	switch (v.TypeIs)
		//	{
		//		case VariantType.Null:
		//			break;
		//		case VariantType.Int:
		//			Set(v.Int);
		//			break;
		//		case VariantType.Float:
		//			Set((float)v.Float);
		//			break;
		//		case VariantType.StringIntern:
		//			Set(v.ToString());
		//			break;
		//		case VariantType.Vec3:
		//			Set(v.ToVector3());
		//			break;
		//		case VariantType.Bool:
		//			Set(v.Bool);
		//			break;
		//		case VariantType.String:
		//			Set(v.ToString());
		//			break;
		//		case VariantType.Vec3i:
		//			Set(v.ToVector3i());
		//			break;
		//		case VariantType.Long:
		//			Set(v.ToInt());
		//			break;
		//	}
		//}

		/// <summary>In-place update</summary>
		public void Set(int i)
		{
			_value.TypeIs = VariantType.Int;
			_value.AsInt = i;
		}

		/// <summary>In-place update</summary>
		public void Set(float f)
		{
			_value.TypeIs = VariantType.Float;
			_value.AsFloat = f;
		}

		/// <summary>In-place update</summary>
		public void Set(bool b)
		{
			_value.TypeIs = VariantType.Bool;
			_value.AsInt = b ? 1 : 0;
		}

		/// <summary>In-place update</summary>
		public void Set(int a, in Half b)
		{
			_value.TypeIs = VariantType.Int;
			_value.AsInt = a;
			_value.AsFloat2 = b;
		}

		/// <summary>In-place update</summary>
		public void Set(int a, short b)
		{
			_value.TypeIs = VariantType.Int;
			_value.AsInt = a;
			_value.AsInt2 = b;
		}

		///// <summary>In-place update</summary>
		//public void Set(in String3 s)
		//{
		//	_value.AsString = s;
		//	_value.TypeIs = VariantType.StringIntern;
		//}

		/// <summary>In-place update</summary>
		public void Set(in String8 s)
		{
			//if (s.Length < String8.MAX_LEN)
			//{
			//	_value.AsString = String3.From(s);
			//	_value.TypeIs = VariantType.StringIntern;
			//}
			//else
			//{
			Set(s.ToString());
			//}
		}

		/// <summary>In-place update</summary>
		public void Set(string s)
		{
			//if (s.Length > String3.MAX_LEN)
			//{
			_value.AsStrInTab = StrTab.Get(s);
			_value.TypeIs = VariantType.String;
			//_value.AsInt2 = (short)s[0];
			//}
			//else
			//{
			//	_value.TypeIs = VariantType.StringIntern;
			//	_value.AsString = String3.From(s);
			//}
		}

		///// <summary>In-place update</summary>
		//public void Set(char ch)
		//{
		//	_value.TypeIs = VariantType.String;
		//	_value.AsStrInTab = StrTab.Get(ch.ToString())
		//	_value.AsString.c0 = ch;
		//	_value.AsString.c1 = '\0';
		//}

		/// <summary>In-place update</summary>
		public void SetParse(string value)
		{
			_value = Parse(_value.TypeIs, value)._value;
		}

		public void Set(in Vector3h v)
		{
			_value.TypeIs = VariantType.Vec3;
			_value.AsVector3 = v;
		}

		public void Set(in Vector3i v)
		{
			_value.TypeIs = VariantType.Vec3i;
			_value.AsShort = (short)v.X;
			_value.AsShort2 = (short)v.Y;
			_value.AsShort3 = (short)v.Z;
		}

		public void SetTypeDefaultValue(VariantType typ)
		{
			switch (typ)
			{
				case VariantType.Null:
				case VariantType.Int:
				case VariantType.Bool:
					Set(0);
					break;
				case VariantType.Float:
					Set(0.0f);
					break;
				//case VariantType.StringIntern:
				//	Set(new String3());
				//	break;
				case VariantType.Vec3:
					Set(Vector3h.Zero);
					break;
				case VariantType.Vec3i:
					Set(new Vector3i(0, 0, 0));
					break;
				case VariantType.String:
					Set(String.Empty);
					break;
				default:
					Set(0);
					break;
			}
		}

		/// <summary>No conversion or type checking</summary>
		public bool Bool
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return _value.AsInt != 0; }
		}

		/// <summary>No conversion or type checking</summary>
		public BitSet64 Bits
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return _value.AsBits; }
		}

		/// <summary>No conversion or type checking</summary>
		public int Int
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return _value.AsInt; }
		}

		/// <summary>No conversion or type checking</summary>
		public short Int2
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return _value.AsInt2; }
		}

		/// <summary>No conversion or type checking</summary>
		public float Float
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return _value.AsFloat; }
		}

		/// <summary>No conversion or type checking</summary>
		public Half Float2
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return _value.AsFloat2; }
		}

		/// <summary>No conversion or type checking</summary>
		public Vector3h Vector
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return _value.AsVector3; }
		}

		/// <summary>No conversion or type checking</summary>
		public Vector3i Vectori
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return new Vector3i(_value.AsShort, _value.AsShort2, _value.AsShort3); }
		}

		///// <summary>No conversion or type checking</summary>
		//public String3 StringShort
		//{
		//	[MethodImpl(MethodImplOptions.AggressiveInlining)]
		//	get { return _value.AsString; }
		//}

		/// <summary>Returns 0 on failure</summary>
		public int ToInt()
		{
			if (_value.TypeIs == VariantType.Int)
			{
				return _value.AsInt;
			}
			else if (_value.TypeIs == VariantType.Float)
			{
				return (int)_value.AsFloat;
			}

			string s = ToString();
			if (Int32.TryParse(s, out int i))
			{
				return i;
			}
			if (Single.TryParse(s, out float f))
			{
				return (int)f;
			}

			return 0;
		}

		/// <summary>Returns 0 on failure</summary>
		public float ToFloat()
		{
			if (_value.TypeIs == VariantType.Int)
			{
				return (float)_value.AsInt;
			}
			else if (_value.TypeIs == VariantType.Float)
			{
				return _value.AsFloat;
			}

			string s = ToString();
			if (Single.TryParse(s, out float f))
			{
				return f;
			}

			return 0f;
		}

		public Vector3h ToVector3()
		{
			if (_value.TypeIs == VariantType.Vec3)
			{
				return _value.AsVector3;
			}

			return Vector3h.Zero;
		}

		public Vector3i ToVector3i()
		{
			if (_value.TypeIs == VariantType.Vec3i)
			{
				return Vectori;
			}

			return new Vector3i(0, 0, 0);
		}

		/// <summary>Returns false on failure</summary>
		public bool ToBool()
		{
			return ToInt() != 0;
		}

		/// <summary>No type checking</summary>
		public void ToKeyPair(out int a, out int b)
		{
			a = _value.AsInt;
			b = _value.AsInt2;
		}

		/// <summary>No type checking</summary>
		public void ToKeyPair(out int a, out float b)
		{
			a = _value.AsInt;
			b = _value.AsFloat2;
		}

		/// <summary>Returns 0 on failure</summary>
		public char ToChar()
		{
			return (char)ToInt();
		}

		/// <summary>Primary purpose is to allow local checking of the first char of a string</summary>
		public int this[int idx]
		{
			get
			{
				switch (_value.TypeIs)
				{
					case VariantType.Null:
					case VariantType.Int:
					case VariantType.Bool:
						return idx == 0 ? _value.AsInt : Int32.MinValue;
					case VariantType.Float:
						return idx == 0 ? (int)_value.AsFloat : Int32.MinValue;
					//case VariantType.StringIntern:
					//	return _value.AsString[idx];
					case VariantType.Vec3:
						return idx == 0 ? (int)_value.AsVector3.X : idx == 1 ? (int)_value.AsVector3.Y : idx == 2 ? (int)_value.AsVector3.Z : Int32.MinValue;
					case VariantType.Vec3i:
						return idx == 0 ? (int)Vectori.X : idx == 1 ? (int)Vectori.Y : idx == 2 ? (int)Vectori.Z : Int32.MinValue;
					case VariantType.String:
						return idx == 0 ? (char)_value.AsInt2 : StrTab.GetString(_value.AsStrInTab)[idx];
					default:
						return Int32.MinValue;
				}
			}
		}

		public int Length
		{
			get
			{
				switch (_value.TypeIs)
				{
					case VariantType.Int:
					case VariantType.Bool:
					case VariantType.Float:
						return 1;
					//case VariantType.StringIntern:
					//	return _value.AsString.Length;
					case VariantType.Vec3:
					case VariantType.Vec3i:
						return 3;
					case VariantType.String:
						return StrTab.GetString(_value.AsStrInTab).Length;
					default:
						return 0;
				}
			}
		}

		public bool Equals(Variant8 v2)
		{
			if (_value.TypeIs == VariantType.Float || v2._value.TypeIs == VariantType.Float)
			{
				return ToFloat() == v2.ToFloat();
			}
			if (_value.TypeIs == VariantType.Int || v2._value.TypeIs == VariantType.Int || _value.TypeIs == VariantType.Bool || v2._value.TypeIs == VariantType.Bool)
			{
				return ToInt() == v2.ToInt();
			}
			if (_value.TypeIs == VariantType.Vec3 && v2._value.TypeIs == VariantType.Vec3)
			{
				return ToVector3() == v2.ToVector3();
			}

			return Equals(v2.ToString());
		}

		public bool Equals(in Variant8 v2)
		{
			if (_value.TypeIs == VariantType.Float || v2._value.TypeIs == VariantType.Float)
			{
				return ToFloat() == v2.ToFloat();
			}
			if (_value.TypeIs == VariantType.Int || v2._value.TypeIs == VariantType.Int || _value.TypeIs == VariantType.Bool || v2._value.TypeIs == VariantType.Bool)
			{
				return ToInt() == v2.ToInt();
			}
			if (_value.TypeIs == VariantType.Vec3 && v2._value.TypeIs == VariantType.Vec3)
			{
				return ToVector3() == v2.ToVector3();
			}

			return Equals(v2.ToString());
		}

		/// <summary></summary>
		public bool Equals(string s)
		{
			switch (_value.TypeIs)
			{
				case VariantType.Null:
					return null == s;
				case VariantType.Bool:
					return (_value.AsInt == 0 ? (s.Equals("false") || s.Equals("FALSE")) : (s.Equals("true") || s.Equals("TRUE")));
				case VariantType.Int:
				case VariantType.Float:
				case VariantType.Vec3:
				case VariantType.Vec3i:
					return false;
				//case VariantType.StringIntern:
				//	return _value.AsString.Equals(s);
				case VariantType.String:
					return /*_value.AsStrInTab.Length == s.Length &&*/ StrTab.Get(s).Index == _value.AsStrInTab.Index;
				default:
					return false;
			}
		}

		/// <inheritdoc/>
		public override bool Equals(object obj)
		{
			switch (_value.TypeIs)
			{
				case VariantType.Null:
					return null == obj;
				case VariantType.Int:
				case VariantType.Bool:
					return _value.AsInt.Equals(obj);
				case VariantType.Float:
					return _value.AsFloat.Equals(obj);
				//case VariantType.StringIntern:
				//	return _value.AsString.Equals(obj);
				case VariantType.Vec3:
					return _value.AsVector3.Equals(obj);
				case VariantType.Vec3i:
					return Vectori.Equals(obj);
				case VariantType.String:
					return _value.Equals(obj);
				default:
					return false;
			}
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			switch (_value.TypeIs)
			{
				case VariantType.Int:
				case VariantType.Bool:
					return _value.AsInt.GetHashCode();
				case VariantType.Float:
					return _value.AsFloat.GetHashCode();
				//case VariantType.StringIntern:
				//	return _value.AsString.GetHashCode();
				case VariantType.Vec3:
					return _value.AsVector3.GetHashCode();
				case VariantType.String:
					return _value.AsStrInTab.GetHashCode();
				default:
					return _value.AsBits.GetHashCode();
			}
		}

		/// <inheritdoc/>
		public override string ToString()
		{
			switch (_value.TypeIs)
			{
				case VariantType.Int:
					return _value.AsInt.ToString();
				case VariantType.Float:
					return _value.AsFloat.ToString();
					//case VariantType.StringIntern:
					//	_value.AsStrInTab = StrTab.Get(_value.AsString);
					//	_value.TypeIs = VariantType.String;
					//return Variant8.StrTab.GetString(_value.AsStrInTab);
				case VariantType.Vec3:
					return _value.AsVector3.ToString();
				case VariantType.Vec3i:
					return Vectori.ToString();
				case VariantType.Bool:
					return _value.AsInt != 0 ? "true" : "false";
				case VariantType.String:
					return StrTab.GetString(_value.AsStrInTab);
				default:
					return String.Empty;
			}
		}

		/// <summary>Single type name parser</summary>
		public string TypeName
		{
			get
			{
				switch (TypeIs)
				{
					case VariantType.Int:
						return "int";
					case VariantType.Bool:
						return "bool";
					case VariantType.Float:
						return "float";
					case VariantType.String:
					case VariantType.StringIntern:
						return "string";
					case VariantType.Vec3:
						return "vector3";
					case VariantType.Vec3i:
						return "vector3i";
					default:
						return "null";
				}
			}
		}

		public string SerializeToString()
		{
			return TypeName + ":" + ToString();
		}

		public static Variant8 DeserializeFromString(string str)
		{
			int idx = str.IndexOf(':');
			string stype = str.Substring(0, idx);
			string sdata = str.Substring(idx + 1);

			VariantType vtype = ParseTypeName(stype);
			return Parse(vtype, sdata);
		}

		/// <summary>Single type name parser</summary>
		public static VariantType ParseTypeName(string txt)
		{
			switch (txt)
			{
				case "null":
					return VariantType.Null;
				case "int":
					return VariantType.Int;
				case "bool":
					return VariantType.Bool;
				case "float":
					return VariantType.Float;
				case "string":
					return VariantType.String;
				case "keypair":
					return VariantType.Int;
				case "vector3":
					return VariantType.Vec3;
				case "vector3i":
					return VariantType.Vec3i;
			}

			throw new Exception("Unknown variant type " + txt);
		}

		/// <summary>Parse and create the specified type</summary>
		public static Variant8 Parse(VariantType vttype, string value)
		{
			Variant8 v = new Variant8();

			switch (vttype)
			{
				case VariantType.Int:
					v.Set(Int32.Parse(value));
					break;
				case VariantType.Float:
					v.Set(Single.Parse(value));
					break;
				case VariantType.StringIntern:
					v.Set(value);
					break;
				case VariantType.Vec3:
					v.Set(ParseVector3(value));
					break;
				case VariantType.Vec3i:
					var vec = ParseVector3(value);
					v.Set(new Vector3i((int)vec.X, (int)vec.Y, (int)vec.Z));
					break;
				case VariantType.Bool:
					v.Set(ParseBool(value));
					break;
				case VariantType.String:
					v.Set(value);
					break;
			}

			return v;
		}

		/// <summary>Infer the type and parse</summary>
		public static Variant8 Parse(string value)
		{
			if (Int32.TryParse(value, out int i))
			{
				return new Variant8(i);
			}
			if (Single.TryParse(value, out float f))
			{
				return new Variant8(f);
			}
			if (value.Length == 4 && (value[0] == 't' || value[0] == 'T'))
			{
				if
				(
					(value[1] == 'r' || value[1] == 'R') &&
					(value[2] == 'u' || value[2] == 'U') &&
					(value[3] == 'e' || value[3] == 'E')
				)
				{
					return new Variant8(true);
				}
			}
			if (value.Length == 5 && (value[0] == 'f' || value[0] == 'F'))
			{
				if
				(
					(value[1] == 'a' || value[1] == 'A') &&
					(value[2] == 'l' || value[2] == 'L') &&
					(value[3] == 's' || value[3] == 'S') &&
					(value[4] == 'e' || value[4] == 'E')
				)
				{
					return new Variant8(false);
				}
			}
			if (value.Length > 4 && StringHelper.CountOccurancesOf(value, ',') == 2)
			{
				// maybe a vector
				try
				{
					return new Variant8(ParseVector3(value));
				}
				catch (Exception)
				{
				}
			}

			return new Variant8(value);
		}

		/// <summary>Returns false on failure</summary>
		public static bool ParseBool(string val)
		{
			switch (val)
			{
				case "true":
				case "TRUE":
				case "True":
					return true;
				case "false":
				case "FALSE":
				case "False":
					return false;
			}
			return false;
		}

		private static Vector3h ParseVector3(string val)
		{
			Vector3h v = Vector3h.Zero;

			if (String.IsNullOrWhiteSpace(val))
			{
				return v;
			}

			SimpleLex lex = new SimpleLex(val);
			lex.Next();
			if (lex.Token == SimpleLex.TokenType.PUNCT)
			{
				lex.Next();
			}
			v.X = (Half)Single.Parse(lex.Lexum.ToString());
			lex.Next();
			lex.Next();
			v.Y = (Half)Single.Parse(lex.Lexum.ToString());
			lex.Next();
			lex.Next();
			v.Z = (Half)Single.Parse(lex.Lexum.ToString());

			return v;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in Variant8 s1, in Variant8 s2)
		{
			return (s1._value.AsBits == s2._value.AsBits)
			|| s1.Equals(s2);
		}

		/// <summary>!=</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in Variant8 s1, in Variant8 s2)
		{
			return !s1.Equals(s2);
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in Variant8 s1, string s)
		{
			return s1.Equals(s);
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in Variant8 s1, string s)
		{
			return !s1.Equals(s);
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in Variant8 s1, String8 s)
		{
			return s1._value.AsStrInTab.Equals(s);
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in Variant8 s1, String8 s)
		{
			return !s1._value.AsStrInTab.Equals(s);
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in Variant8 s1, int i)
		{
			return s1.ToInt() == i;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in Variant8 s1, int i)
		{
			return s1.ToInt() != i;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in Variant8 s1, float i)
		{
			return s1.ToFloat() == i;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in Variant8 s1, float i)
		{
			return s1.ToFloat() != i;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in Variant8 s1, bool i)
		{
			return s1.ToBool() == i;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in Variant8 s1, bool i)
		{
			return s1.ToBool() != i;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in Variant8 s1, char c)
		{
			return s1.ToChar() == c;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in Variant8 s1, char c)
		{
			return s1.ToChar() != c;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in Variant8 s1, in Vector3h i)
		{
			return s1.ToVector3() == i;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in Variant8 s1, in Vector3h i)
		{
			return s1.ToVector3() != i;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in Variant8 s1, in Vector3 i)
		{
			return s1.ToVector3().Equals(i);
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in Variant8 s1, in Vector3 i)
		{
			return !s1.ToVector3().Equals(i);
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in Variant8 s1, in Vector3i i)
		{
			return s1.Vectori.Equals(i);
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in Variant8 s1, in Vector3i i)
		{
			return !s1.Vectori.Equals(i);
		}

		/// <summary>+</summary>
		public static Variant8 operator +(in Variant8 v1, in Variant8 v2)
		{
			if (v1._value.TypeIs == VariantType.Float || v2._value.TypeIs == VariantType.Float)
			{
				return v1.ToFloat() + v2.ToFloat();
			}
			if (v1._value.TypeIs == VariantType.Int || v2._value.TypeIs == VariantType.Int || v1._value.TypeIs == VariantType.Bool || v2._value.TypeIs == VariantType.Bool)
			{
				return v1.ToInt() + v2.ToInt();
			}
			if (v1._value.TypeIs == VariantType.Vec3 && v2._value.TypeIs == VariantType.Vec3)
			{
				return new Variant8(v1.ToVector3() + v2.ToVector3());
			}

			return v1.ToString() + v2.ToString();
		}

		/// <summary>-</summary>
		public static Variant8 operator -(in Variant8 v1, in Variant8 v2)
		{
			if (v1._value.TypeIs == VariantType.Float || v2._value.TypeIs == VariantType.Float)
			{
				return v1.ToFloat() - v2.ToFloat();
			}
			if (v1._value.TypeIs == VariantType.Vec3 && v2._value.TypeIs == VariantType.Vec3)
			{
				return new Variant8(v1.ToVector3() - v2.ToVector3());
			}

			return v1.ToInt() - v2.ToInt();
		}

		/// <summary>*</summary>
		public static Variant8 operator *(in Variant8 v1, in Variant8 v2)
		{
			if (v1._value.TypeIs == VariantType.Vec3 && v2._value.TypeIs == VariantType.Float)
			{
				return new Variant8(v1.ToVector3() * v2.ToFloat());
			}
			if (v1._value.TypeIs == VariantType.Float || v2._value.TypeIs == VariantType.Float)
			{
				return v1.ToFloat() * v2.ToFloat();
			}
			if (v1._value.TypeIs == VariantType.Int || v2._value.TypeIs == VariantType.Int || v1._value.TypeIs == VariantType.Bool || v2._value.TypeIs == VariantType.Bool)
			{
				return v1.ToInt() * v2.ToInt();
			}

			throw new InvalidCastException(v1.TypeIs + " or " + v2.TypeIs + " not numeric");
		}

		/// <summary>/</summary>
		public static Variant8 operator /(in Variant8 v1, in Variant8 v2)
		{
			if (v1._value.TypeIs == VariantType.Vec3 && v2._value.TypeIs == VariantType.Float)
			{
				return new Variant8(v1.ToVector3() / v2.ToFloat());
			}
			if (v1._value.TypeIs == VariantType.Float || v2._value.TypeIs == VariantType.Float)
			{
				return v1.ToFloat() / v2.ToFloat();
			}
			if (v1._value.TypeIs == VariantType.Int || v2._value.TypeIs == VariantType.Int || v1._value.TypeIs == VariantType.Bool || v2._value.TypeIs == VariantType.Bool)
			{
				return v1.ToInt() / v2.ToInt();
			}

			throw new InvalidCastException(v1.TypeIs + " or " + v2.TypeIs + " not numeric");
		}

		/// <summary>lt</summary>
		public static bool operator <(in Variant8 v1, in Variant8 v2)
		{
			if (v1._value.TypeIs == VariantType.Float || v2._value.TypeIs == VariantType.Float)
			{
				return v1.ToFloat() < v2.ToFloat();
			}
			if (v1._value.TypeIs == VariantType.Int || v2._value.TypeIs == VariantType.Int || v1._value.TypeIs == VariantType.Bool || v2._value.TypeIs == VariantType.Bool)
			{
				return v1.ToInt() < v2.ToInt();
			}

			return false;
		}

		/// <summary>gt</summary>
		public static bool operator >(in Variant8 v1, in Variant8 v2)
		{
			if (v1._value.TypeIs == VariantType.Float || v2._value.TypeIs == VariantType.Float)
			{
				return v1.ToFloat() > v2.ToFloat();
			}
			if (v1._value.TypeIs == VariantType.Int || v2._value.TypeIs == VariantType.Int || v1._value.TypeIs == VariantType.Bool || v2._value.TypeIs == VariantType.Bool)
			{
				return v1.ToInt() > v2.ToInt();
			}

			return false;
		}

		/// <summary>lteq</summary>
		public static bool operator <=(in Variant8 v1, in Variant8 v2)
		{
			if (v1._value.TypeIs == VariantType.Float || v2._value.TypeIs == VariantType.Float)
			{
				return v1.ToFloat() <= v2.ToFloat();
			}
			if (v1._value.TypeIs == VariantType.Int || v2._value.TypeIs == VariantType.Int || v1._value.TypeIs == VariantType.Bool || v2._value.TypeIs == VariantType.Bool)
			{
				return v1.ToInt() <= v2.ToInt();
			}

			return false;
		}

		/// <summary>gteq</summary>
		public static bool operator >=(in Variant8 v1, in Variant8 v2)
		{
			if (v1._value.TypeIs == VariantType.Float || v2._value.TypeIs == VariantType.Float)
			{
				return v1.ToFloat() >= v2.ToFloat();
			}
			if (v1._value.TypeIs == VariantType.Int || v2._value.TypeIs == VariantType.Int || v1._value.TypeIs == VariantType.Bool || v2._value.TypeIs == VariantType.Bool)
			{
				return v1.ToInt() >= v2.ToInt();
			}

			return false;
		}

		/// <summary>Implicit Variant to string cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator string(in Variant8 v) => v.ToString();
		/// <summary>Implicit string to Variant cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Variant8(string x) => new Variant8(x);
		public static implicit operator Variant8(StringBuilder x) => new Variant8(x);

		/// <summary>cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int(in Variant8 v) => v.ToInt();
		/// <summary>cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Variant8(int x) => new Variant8(x);

		/// <summary>cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float(in Variant8 v) => v.ToFloat();
		/// <summary>cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Variant8(float x) => new Variant8(x);

		/// <summary>cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool(in Variant8 v) => v.ToBool();
		/// <summary>cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Variant8(bool x) => new Variant8(x);

		/// <summary>cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector3h(in Variant8 v) => v.ToVector3();
		/// <summary>cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Variant8(in Vector3h x) => new Variant8(x);

		/// <summary>cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector3i(in Variant8 v) => v.Vectori;
		/// <summary>cast</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Variant8(in Vector3i x) => new Variant8(x);
	}
}
