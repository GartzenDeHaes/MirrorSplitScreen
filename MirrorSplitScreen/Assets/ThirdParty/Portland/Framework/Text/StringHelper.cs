using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

using Portland.Mathmatics;

namespace Portland.Text
{
	/// <summary>
	/// StringHelper has a number of string utility functions.
	/// </summary>
	public static class StringHelper
	{
		public const string ComputerLangOperators = "$~!@#%^&()_+=-[]{}|\\;.:><,/?";

		/// <summary>
		/// Expand Char.IsPunct
		/// </summary>
		public static bool IsProgrammingLanguagePunct(char ch)
		{
			return Char.IsPunctuation(ch) || ComputerLangOperators.IndexOf(ch) > -1;
		}

		/// <summary>
		/// Returns true if the argument is a C or HTML format hex number.
		/// </summary>
		public static bool IsHexNum(string value)
		{
			if (1 >= value.Length)
			{
				return Char.IsNumber(value[0]);
			}
			int pos = 0;
			if (value[0] == '#')
			{
				pos = 1;
			}
			else if (value[0] == '0' && (value[1] == 'x' || value[1] == 'X'))
			{
				pos = 2;
			}
			for (; pos < value.Length; pos++)
			{
				if (Char.IsNumber(value[pos]))
				{
					continue;
				}
				if ((value[pos] >= 'a' && value[pos] <= 'f') || (value[pos] >= 'A' && value[pos] <= 'F'))
				{
					continue;
				}
				return false;
			}
			return true;
		}

		/// <summary>
		/// Returns the number of time ch occures in str.
		/// </summary>
		public static int CountOccurancesOf(string value, char ch)
		{
			int count = 0;
			for (int x = 0; x < value.Length; x++)
			{
				if (value[x] == ch)
				{
					count++;
				}
			}
			return count;
		}

		/// <summary>
		/// Returns true if the argument is ALPHA and upper case.
		/// </summary>
		public static bool IsUpperAlphaOnly(string value)
		{
			char ch;
			int len = value.Length;
	
			for (int x = 0; x < len; x++)
			{
				ch = value[x];
				if (Char.IsPunctuation(ch) || !Char.IsUpper(ch) || !Char.IsLetter(ch))
				{
					return false;
				}
			}
			return len > 0;
		}

		/// <summary>
		/// Returns true if the argument is upper case, ignores non letters.
		/// </summary>
		public static bool IsUpper(string value)
		{
			for (int x = 0; x < value.Length; x++)
			{
				if (Char.IsLetter(value[x]) && !Char.IsUpper(value[x]))
				{
					return false;
				}
			}
			return value.Length > 0;
		}

		/// <summary>
		/// Returns true if the argument is upper case, ignores non letters.
		/// </summary>
		public static bool IsLower(string value)
		{
			for (int x = 0; x < value.Length; x++)
			{
				if (Char.IsLetter(value[x]) && !Char.IsLower(value[x]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns true if the argument is an integer or non-exponential floting point.
		/// </summary>
		public static bool IsNumeric(string value)
		{
			if (value.Length == 0)
			{
				return false;
			}
			int dotcount = 0;
			for (int x = 0; x < value.Length; x++)
			{
				char ch = value[x];
				if (ch == '.' && dotcount == 0)
				{
					dotcount++;
					continue;
				}
				if (!Char.IsDigit(ch))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns true if the argument consists entirely of numerics.
		/// </summary>
		public static bool IsInt(string value)
		{
			if (value.Length == 0)
			{
				return false;
			}
			for (int x = 0; x < value.Length; x++)
			{
				if (!Char.IsDigit(value[x]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Removes balanced quotes, if any, from the argument.
		/// </summary>
		/// <returns>Returns the argument, minus begining and ending quotes (if any).</returns>
		public static string StripQuotes(string value)
		{
			if (value[0] == '"' && value[value.Length - 1] == '"')
			{
				return value.Substring(1, value.Length - 2);
			}
			return value;
		}

		/// <summary>
		/// Returns true if the argument has HTML path characters.
		/// </summary>
		public static bool HasPath(string filename)
		{
			return filename.IndexOf('/') > -1 || filename.IndexOf("..") > -1;
		}

		/// <summary>
		/// Returns filepath, ensuring that ch is the last character.  This typically
		/// used when pulling file paths out of config files where the trailing '/'
		/// may be ommitted by the user.
		/// </summary>
		public static string EnsureTrailingChar(string filepath, char ch)
		{
			if (filepath[filepath.Length - 1] != ch)
			{
				return filepath + ch.ToString();
			}
			return filepath;
		}

		/// <summary>
		/// This implements javascript's parseInt function.  parseInt will return 123 for strings of the form '123abc'.
		/// </summary>
		public static int ParseRightInt32(string value)
		{
			StringBuilder buf = new StringBuilder(value.Length);
			for (int x = value.Length - 1; x >= 0; x--)
			{
				if (!Char.IsDigit(value[x]))
				{
					break;
				}
				buf.Insert(0, value[x]);
			}
			return Int32.Parse(buf.ToString());
		}

		/// <summary>
		/// Implements VB RIGHT$
		/// </summary>
		/// <param name="value">The string to split</param>
		/// <param name="count">The number of characters to return starting from the end of the string</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string RightStr(string value, int count)
		{
			return value.Substring(value.Length - count);
		}

		/// <summary>
		/// Implements VB MID$
		/// </summary>
		/// <param name="value">The string to split</param>
		/// <param name="start">The index to start from</param>
		/// <param name="stop">The index to end at</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string MidStr(string value, int start, int stop)
		{
			return value.Substring(start, stop - start);
		}

		/// <summary>
		/// Parses numerics of the form '$123,123.00'
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal ParseMoney(string value)
		{
			return (value.Length == 0) ? 0 : Decimal.Parse(value.Replace("$", "").Replace(",", ""));
		}

		/// <summary>
		/// Convert to string handling DBNull and null.
		/// </summary>
		public static string Parse(object o)
		{
			if (o is DBNull)
			{
				return null;
			}
         if (null == o)
         {
               return null;
         }
			if (o is string)
			{
				return (string)o;
			}
			return o.ToString();
		}

		/// <summary>
		/// Returns the argument with all numeric characters removed.
		/// </summary>
		public static string RemoveNonNumerics(string value)
		{
			StringBuilder buf = new StringBuilder();
			char ch;
			for (int x = 0; x < value.Length; x++)
			{
				ch = value[x];
				if (Char.IsDigit(ch))
				{
					buf.Append(ch);
				}
			}
			return buf.ToString();
		}

		private static char[] m_xmlSpChars = new char[] { '&', '<', '>', '"', '\'' };

		/// <summary>
		/// Returns true if the argument requires xml encoding.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool RequiresXmlEncoding(string value)
		{
			return value != null && value.IndexOfAny(m_xmlSpChars) > -1;
		}

		/// <summary>
		/// A simplistic XML encoder.  If available, use the encoders from System.Web instead.
		/// </summary>
		public static string XmlEncode(string value)
		{
			if (!RequiresXmlEncoding(value))
			{
				return value;
			}
			StringBuilder buf = new StringBuilder(value.Length + 30);
			for (int x = 0; x < value.Length; x++)
			{
				switch (value[x])
				{
					case '&':
						buf.Append("&amp;");
						break;
					case '<':
						buf.Append("&lt;");
						break;
					case '>':
						buf.Append("&gt;");
						break;
					case '"':
						buf.Append("&quot;");
						break;
					case '\'':
						buf.Append("&apos;");
						break;
					default:
						buf.Append(value[x]);
						break;
				}
			}
			return buf.ToString();
		}

		/// <summary>
		/// Superset of XmlEncode that converts control and white space to %XX format.
		/// </summary>
		/// <param name="value">A string</param>
		/// <returns>The encoded string.</returns>
		public static string UriEncode(string value)
		{
			value = XmlEncode(value);

			StringBuilder buf = new StringBuilder(value.Length + 30);
			for (int x = 0; x < value.Length; x++)
			{
				if (Char.IsWhiteSpace(value[x]) || Char.IsControl(value[x]))
				{
					buf.Append('%');
					buf.Append(((int)value[x]).ToString("00"));
				}
				else
				{
					buf.Append(value[x]);
				}
			}
			return buf.ToString();
		}

		/// <summary>
		/// Convert the string to a "c" style escape format.  This is necessary for quoted HTTP body post data.
		/// </summary>
		public static string EscapeString(string s)
		{
			StringBuilder buf = new StringBuilder(s.Length + 10);

			for (int x = 0; x < s.Length; x++)
			{
				switch (s[x])
				{
					case '\b':
						buf.Append("\\b");
						break;
					case '\t':
						buf.Append("\\t");
						break;
					case '\v':
						buf.Append("\\v");
						break;
					case '\n':
						buf.Append("\\n");
						break;
					case '\f':
						buf.Append("\\f");
						break;
					case '\r':
						buf.Append("\\r");
						break;
					case '"':
						buf.Append("\\\"");
						break;
					case '\'':
						buf.Append("\\'");
						break;
					case '\\':
						buf.Append("\\\\");
						break;
					default:
						buf.Append(s[x]);
						break;
				}
			}
			return buf.ToString();
		}

		/// <summary>
		/// Remove all control chars from the string
		/// </summary>
		public static string StripControlChars(string s)
		{
			StringBuilder buf = new StringBuilder(s.Length);

			for (int x = 0; x < s.Length; x++)
			{
				if (!Char.IsControl(s[x]))
				{
					buf.Append(s[x]);
				}
			}
			return buf.ToString();
		}

		/// <summary>
		/// No-alloc compare string and StringBuilder contents
		/// </summary>
		public static bool AreEqual(StringBuilder sb, string str)
		{
			if (sb.Length != str.Length)
			{
				return false;
			}
			for (int x = 0; x < str.Length; x++)
			{
				if (str[x] != sb[x])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// No-alloc compare string and StringBuilder contents
		/// </summary>
		public static bool AreEqualNoCase(StringBuilder sb, string str)
		{
			if (sb.Length != str.Length)
			{
				return false;
			}
			for (int x = 0; x < str.Length; x++)
			{
				if (Char.ToUpper(str[x]) != Char.ToUpper(sb[x]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Calls ToString(), or return nbsp;
		/// </summary>
		public static string ToStringOrNbsp(object o)
		{
			if (null == o || (o is DBNull))
			{
				return "&nbsp;";
			}
			return o.ToString();
		}

		/// <summary>
		/// Trim that ignores null;
		/// </summary>
		public static string Trim(string str)
		{
			if (null == str)
			{
				return null;
			}
			return str.Trim();
		}

		/// <summary>
		/// The main function that checks if two given strings match. The first string  
		/// may contain wildcard characters
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool LikeOrNullOrEmpty(string pattern, string text)
		{
			return null == text || text.Length == 0 || Like(text, pattern);
		}

		public static bool AreBothNullOrEqual(string str, string str2)
		{
			return String.IsNullOrEmpty(str) ? String.IsNullOrEmpty(str2) : str == str2; 
		}

		/// <summary>
		/// The main function that checks if two given strings match. The first string  
		/// may contain wildcard characters
		/// </summary>
		public static bool Like(string text, string pattern)
		{
			if (text == null && pattern == "*")
			{
				return true;
			}
			if (text == null || pattern == null)
			{
				return false;
			}
			if (pattern == text)
			{
				return true;
			}

			int alen = pattern.Length;
			int blen = text.Length;
			int bpos = 0;

			if (alen == 0)
			{
				return blen == 0;
			}
			if (blen == 0)
			{
				return alen == 1 && pattern[0] == '*';
			}

			for (int x = 0; x < alen; x++)
			{
				if (bpos == blen)
				{
					return false;
				}

				char aatx = pattern[x];

				if (aatx == '?')
				{
					bpos++;
					continue;
				}

				if (aatx != '*')
				{
					if (aatx != text[bpos++])
					{
						return false;
					}
					continue;
				}

				if (x + 1 == alen)
				{
					return true;
				}

				char exitch = pattern[x + 1];

				while (text[bpos] != exitch)
				{
					bpos++;
					if (bpos >= blen)
					{
						return false;
					}
				}
			}

			return blen == bpos;
		}

		/// <summary>
		/// A simple and fast hash code calculation.  Use Murmur if you need a good hash code.
		/// </summary>
		public static int HashSimple32(string s)
		{
			uint hashCode = 0U;
			int limit = s.Length;
			
			for (int i = 0; i < limit; i++)
			{
				hashCode = hashCode * 31 + s[i];
			}
			
			return (int)hashCode;
		}

		/// <summary>
		/// A simple and fast hash code calculation.  Use Murmur if you need a good hash code.
		/// </summary>
		public static int HashSimple32(String8 s)
		{
			uint hashCode = 0U;

			hashCode = hashCode * 31 + s.c0;
			hashCode = hashCode * 31 + s.c1;
			hashCode = hashCode * 31 + s.c2;
			hashCode = hashCode * 31 + s.c3;
			hashCode = hashCode * 31 + s.c4;
			hashCode = hashCode * 31 + s.c5;
			hashCode = hashCode * 31 + s.c6;
			hashCode = hashCode * 31 + s.c7;

			return (int)hashCode;
		}

		/// <summary>
		/// A simple and fast hash code calculation.  Use Murmur if you need a good hash code.
		/// </summary>
		public static int HashSimple32(StringBuilder sb)
		{
			uint hashCode = 0U;
			int limit = sb.Length;

			for (int i = 0; i < limit; i++)
			{
				hashCode = hashCode * 31 + sb[i];
			}

			return (int)hashCode;
		}

		/// <summary>
		/// Murmur hash code.
		/// </summary>
		public static int HashMurmur32(string data)
		{
			return HashMurmur32(data, 0, data.Length);
		}

		/// <summary>
		/// Murmur hash code.
		/// </summary>
		public static int HashMurmur32(string data, int start, int len)
		{
			return (int)NumberHelper.HashMurmur32(data, start, len);
		}

		/// <summary>
		/// Murmur hash code.
		/// </summary>
		public static int HashMurmur32(StringBuilder data, int start, int len)
		{
			return (int)NumberHelper.HashMurmur32(data, start, len);
		}

		/// <summary>
		/// Murmur hash code.
		/// </summary>
		public static int HashMurmur32(StringBuilder data)
		{
			return (int)NumberHelper.HashMurmur32(data, 0, data.Length);
		}

		/// <summary>
		/// Murmur hash code.
		/// </summary>
		public static long HashMurmur64(string data)
		{
			var bin = Encoding.UTF8.GetBytes(data);
			return (long)NumberHelper.HashMurmur64(bin, (ulong)bin.Length);
		}

		public static int HashLnv1a(string data)
		{
			const uint fnv_prime = 0x811C9DC5;
			uint hash = 0;
			uint i = 0;

			for (i = 0; i < data.Length; i++)
			{
				hash *= fnv_prime;
				hash ^= ((byte)data[(int)i]);
			}

			return unchecked ((int)hash);
		}

		public static string SanitizeFileName(string fileName, char replacementChar = '_')
		{
			var blackList = new HashSet<char>(System.IO.Path.GetInvalidFileNameChars());
			var output = fileName.ToCharArray();
			for (int i = 0, ln = output.Length; i < ln; i++)
			{
				if (blackList.Contains(output[i]))
				{
					output[i] = replacementChar;
				}
			}
			return new String(output);
		}
	}
}
