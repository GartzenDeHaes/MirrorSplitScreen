using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Portland.Mathmatics
{
	/// <summary>
	/// Various math functions
	/// </summary>
	public static class NumberHelper
	{
		/// <summary>First 260 primes</summary>
		public static readonly short[] SmallPrimes = {
			2,   3,   5,   7,   11,  13,  17,  19,  23,  29,  
			31,  37,  41,  43,  47,  53,  59,  61,  67,  71,  
			73,  79,  83,  89,  97,  101, 103, 107, 109, 113, 
			127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 
			179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 
			233, 239, 241, 251, 257, 263, 269, 271, 277, 281,
			283, 293, 307, 311, 313, 317, 331, 337, 347, 349,
			353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 
			419, 421, 431, 433, 439, 443, 449, 457, 461, 463,
			467, 479, 487, 491, 499, 503, 509, 521, 523, 541,
			547, 557, 563, 569, 571, 577, 587, 593, 599, 601,
			607, 613, 617, 619, 631, 641, 643, 647, 653, 659,
			661, 673, 677, 683, 691, 701, 709, 719, 727, 733,
			739, 743, 751, 757, 761, 769, 773, 787, 797, 809,
			811, 821, 823, 827, 829, 839, 853, 857, 859, 863,
			877, 881, 883, 887, 907, 911, 919, 929, 937, 941,
			947, 953, 967, 971, 977, 983, 991, 997,1009, 1013,
			1019, 1021, 1031, 1033, 1039, 1049, 1051, 1061, 1063, 1069,
			1087, 1091, 1093, 1097, 1103, 1109, 1117, 1123, 1129, 1151,
			1153, 1163, 1171, 1181, 1187, 1193, 1201, 1213, 1217, 1223,
			1229, 1231, 1237, 1249, 1259, 1277, 1279, 1283, 1289, 1291,
			1297, 1301, 1303, 1307, 1319, 1321, 1327, 1361, 1367, 1373,
			1381, 1399, 1409, 1423, 1427, 1429, 1433, 1439, 1447, 1451,
			1453, 1459, 1471, 1481, 1483, 1487, 1489, 1493, 1499, 1511,
			1523, 1531, 1543, 1549, 1553, 1559, 1567, 1571, 1579, 1583,
			1597, 1601, 1607, 1609, 1613, 1619, 1621, 1627, 1637, 1657
		};

		/// <summary>Used in RSA</summary>
		public static ulong ModPow(ulong baseNum, ulong exponent, ulong modulus)
		{
			if (modulus == 1)
				return 0;
			ulong curPow = baseNum % modulus;
			ulong res = 1;

			while (exponent > 0)
			{
				if (exponent % 2 == 1)
					res = (res * curPow) % modulus;
				exponent /= 2;
				curPow = (curPow * curPow) % modulus;  // square curPow
			}
			return res;
		}

		/// <summary>Used in RSA</summary>
		public static uint ModPow(uint baseNum, uint exponent, uint modulus)
		{
			if (modulus == 1)
				return 0;
			uint curPow = baseNum % modulus;
			uint res = 1;
			while (exponent > 0)
			{
				if (exponent % 2 == 1)
					res = (res * curPow) % modulus;
				exponent /= 2;
				curPow = (curPow * curPow) % modulus;  // square curPow
			}
			return res;
		}

		/// <summary>Used in RSA</summary>
		public static int GCD(int a, int b)
		{
			int rem;

			while (b != 0)
			{
				rem = a % b;
				a = b;
				b = rem;
			}

			return a;
		}

		/// <summary>Used in RSA</summary>
		public static long GCD(long a, long b)
		{
			long rem;

			while (b != 0)
			{
				rem = a % b;
				a = b;
				b = rem;
			}

			return a;
		}

		/// <summary>Get least significant bit</summary>
		public static int GetLowestSetBit(uint val)
		{
			for (int x = 0; x < 32; x++)
			{
				if ((val & (1U << x)) != 0)
				{
					return x;
				}
			}

			return -1;
		}

		/// <summary>Get most significant bit</summary>
		public static int GetHighestSetBit(uint val)
		{
			for (int x = 31; x >= 0; x++)
			{
				if ((val & (1U << x)) != 0)
				{
					return x;
				}
			}

			return -1;
		}

		/// <summary>An approximate prime number test</summary>
		public static bool IsPrime(uint n, int tries, Random r)
		{
			if (n < 2)
			{
				return false;
			}

			if (n != 2 && n % 2 == 0)
			{
				return false;
			}

			uint s = n - 1;
			while (s % 2 == 0)
			{
				s >>= 1;
			}

			for (int i = 0; i < tries; i++)
			{
				double a = r.NextDouble() * (n - 1) + 1;
				uint temp = s;
				uint mod = (uint)Math.Pow(a, (double)temp) % n;

				while (temp != n - 1 && mod != 1 && mod != n - 1)
				{
					mod = (mod * mod) % n;
					temp *= 2;
				}
				if (mod != n - 1 && temp % 2 == 0)
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>no-alloc long to bytes</summary>
		static public void LongToBytes(ulong l, byte[] bytes)
		{
			bytes[0] = (byte)(l & 0xFF);
			bytes[1] = (byte)((l >> 8) & 0xFF);
			bytes[2] = (byte)((l >> 16) & 0xFF);
			bytes[3] = (byte)((l >> 24) & 0xFF);
			bytes[4] = (byte)((l >> 32) & 0xFF);
			bytes[5] = (byte)((l >> 40) & 0xFF);
			bytes[6] = (byte)((l >> 48) & 0xFF);
			bytes[7] = (byte)((l >> 56) & 0xFF);
		}

		/// <summary>long to bytes</summary>
		static public byte[] LongToBytes(ulong l)
		{
			byte[] bytes = new byte[8];

			LongToBytes(l, bytes);

			return bytes;
		}

		private const uint _m = 0x5bd1e995;
		private const int _r = 24;

		/// <summary>Murmur string hash</summary>
		public static uint HashMurmur32(string data, int start, int length, uint seed = 0xc58f1a7a)
		{
			//int length = data.Length;
			if (length == 0)
				return 0;

			uint h = seed ^ (uint)length;
			int currentIndex = start;

			while (length >= 4)
			{
				uint k = (uint)(data[currentIndex++] | data[currentIndex++] << 8 | data[currentIndex++] << 16 | data[currentIndex++] << 24);
				k *= _m;
				k ^= k >> _r;
				k *= _m;

				h *= _m;
				h ^= k;
				length -= 4;
			}

			switch (length)
			{
				case 3:
					h ^= (UInt16)(data[currentIndex++] | data[currentIndex++] << 8);
					h ^= (uint)(data[currentIndex] << 16);
					h *= _m;
					break;
				case 2:
					h ^= (UInt16)(data[currentIndex++] | data[currentIndex] << 8);
					h *= _m;
					break;
				case 1:
					h ^= data[currentIndex];
					h *= _m;
					break;
				default:
					break;
			}

			h ^= h >> 13;
			h *= _m;
			h ^= h >> 15;

			return h;
		}

		/// <summary>Murmur string hash</summary>
		public static uint HashMurmur32(StringBuilder data, int start, int length, uint seed = 0xc58f1a7a)
		{
			//int length = len;
			if (length == 0)
				return 0;

			uint h = seed ^ (uint)length;
			int currentIndex = start;

			while (length >= 4)
			{
				uint k = (uint)(data[currentIndex++] | data[currentIndex++] << 8 | data[currentIndex++] << 16 | data[currentIndex++] << 24);
				k *= _m;
				k ^= k >> _r;
				k *= _m;

				h *= _m;
				h ^= k;
				length -= 4;
			}

			switch (length)
			{
				case 3:
					h ^= (UInt16)(data[currentIndex++] | data[currentIndex++] << 8);
					h ^= (uint)(data[currentIndex] << 16);
					h *= _m;
					break;
				case 2:
					h ^= (UInt16)(data[currentIndex++] | data[currentIndex] << 8);
					h *= _m;
					break;
				case 1:
					h ^= data[currentIndex];
					h *= _m;
					break;
				default:
					break;
			}

			h ^= h >> 13;
			h *= _m;
			h ^= h >> 15;

			return h;
		}

		/// <summary>Murmur2 string hash</summary>
		public static ulong HashMurmur64(byte[] data, ulong length, ulong seed = 0xc58f1a7a)
		{
			ulong m = 0xc6a4a7935bd1e995L;
			ulong r = 47;

			ulong h = (seed & 0xffffffffL) ^ (length * m);

			ulong length8 = length / 8;

			for (uint i = 0; i < length8; i++)
			{
				uint i8 = i * 8;
				ulong k = ((ulong)data[i8 + 0] & 0xff) + (((ulong)data[i8 + 1] & 0xff) << 8)
					 + (((ulong)data[i8 + 2] & 0xff) << 16) + (((ulong)data[i8 + 3] & 0xff) << 24)
					 + (((ulong)data[i8 + 4] & 0xff) << 32) + (((ulong)data[i8 + 5] & 0xff) << 40)
					 + (((ulong)data[i8 + 6] & 0xff) << 48) + (((ulong)data[i8 + 7] & 0xff) << 56);

				k *= m;
				k ^= k >> (short)r;
				k *= m;

				h ^= k;
				h *= m;
			}

			switch (length % 8)
			{
				case 7:
					h ^= (ulong)(data[(length & ~(ulong)7) + 6] & 0xff) << 48;
					h ^= (ulong)(data[(length & ~(ulong)7) + 5] & 0xff) << 40;
					h ^= (ulong)(data[(length & ~(ulong)7) + 4] & 0xff) << 32;
					h ^= (ulong)(data[(length & ~(ulong)7) + 3] & 0xff) << 24;
					h ^= (ulong)(data[(length & ~(ulong)7) + 2] & 0xff) << 16;
					h ^= (ulong)(data[(length & ~(ulong)7) + 1] & 0xff) << 8;
					h ^= (ulong)(data[length & ~(ulong)7] & 0xff);
					h *= m;
					break;
				case 6:
					h ^= (ulong)(data[(length & ~(ulong)7) + 5] & 0xff) << 40;
					h ^= (ulong)(data[(length & ~(ulong)7) + 4] & 0xff) << 32;
					h ^= (ulong)(data[(length & ~(ulong)7) + 3] & 0xff) << 24;
					h ^= (ulong)(data[(length & ~(ulong)7) + 2] & 0xff) << 16;
					h ^= (ulong)(data[(length & ~(ulong)7) + 1] & 0xff) << 8;
					h ^= (ulong)(data[length & ~(ulong)7] & 0xff);
					h *= m;
					break;
				case 5:
					h ^= (ulong)(data[(length & ~(ulong)7) + 4] & 0xff) << 32;
					h ^= (ulong)(data[(length & ~(ulong)7) + 3] & 0xff) << 24;
					h ^= (ulong)(data[(length & ~(ulong)7) + 2] & 0xff) << 16;
					h ^= (ulong)(data[(length & ~(ulong)7) + 1] & 0xff) << 8;
					h ^= (ulong)(data[length & ~(ulong)7] & 0xff);
					h *= m;
					break;
				case 4:
					h ^= (ulong)(data[(length & ~(ulong)7) + 3] & 0xff) << 24;
					h ^= (ulong)(data[(length & ~(ulong)7) + 2] & 0xff) << 16;
					h ^= (ulong)(data[(length & ~(ulong)7) + 1] & 0xff) << 8;
					h ^= (ulong)(data[length & ~(ulong)7] & 0xff);
					h *= m;
					break;
				case 3:
					h ^= (ulong)(data[(length & ~(ulong)7) + 2] & 0xff) << 16;
					h ^= (ulong)(data[(length & ~(ulong)7) + 1] & 0xff) << 8;
					h ^= (ulong)(data[length & ~(ulong)7] & 0xff);
					h *= m;
					break;
				case 2:
					h ^= (ulong)(data[(length & ~(ulong)7) + 1] & 0xff) << 8;
					h ^= (ulong)(data[length & ~(ulong)7] & 0xff);
					h *= m;
					break;
				case 1:
					h ^= (ulong)(data[length & ~(ulong)7] & 0xff);
					h *= m;
					break;
			};

			h ^= h >> (short)r;
			h *= m;
			h ^= h >> (short)r;

			return h;
		}

		/// <summary>0-15 to hex digit</summary>
		public static char NibbleToHex(int nibble)
		{
			if (nibble > 0xF)
			{
				throw new Exception("internal nibble error");
			}
			switch (nibble)
			{
				case 0:
					return '0';
				case 1:
					return '1';
				case 2:
					return '2';
				case 3:
					return '3';
				case 4:
					return '4';
				case 5:
					return '5';
				case 6:
					return '6';
				case 7:
					return '7';
				case 8:
					return '8';
				case 9:
					return '9';
				case 10:
					return 'A';
				case 11:
					return 'B';
				case 12:
					return 'C';
				case 13:
					return 'D';
				case 14:
					return 'E';
				case 15:
					return 'F';
				default:
					throw new Exception("NIBBLE error");
			}
		}

		private static char[] _map = {
				'!', '"', '#', '$',  '%', '&', '\'', '(', ')', '*',
				'+', ',', '-', '.',  '/', ':', ';',  '<', '=', '>',
				'?', '@', 'G', 'H',  'I', 'J', 'K',  'L', 'M', 'N',
				'O', 'P', 'Q', 'R',  'S', 'T', 'U',  'V', 'W', 'X',
				'Y', 'Z', '[', '\\', ']', '^', '_',  '`', 'a', 'b',
				'c', 'd', 'e', 'f',  'g', 'h', 'i',  'j', 'k', 'l',
				'm', 'n', 'o', 'p',  'q', 'r', 't',  'u', 'v', 'w',
				'x', 'y', 'z', '{',  '|', '}', '~'
			};

		/// <summary>A naive bin to text converter</summary>
		public static void TextEncode(StringBuilder output, byte[] data, int start, int len)
		{
			int end = start + len;

			for (int x = start; x < end; x++)
			{
				if (data[x] >= _map.Length)
				{
					output.Append(NibbleToHex(data[x] >> 4));
					output.Append(NibbleToHex(data[x] & 0xF));
				}
				else
				{
					output.Append(_map[data[x]]);
				}
			}
		}

		//private static byte TextDecodeFromHex(char ch1, char ch2)
		//{
		//	byte b;

		//	if (Char.IsDigit(ch1))
		//	{
		//		b = (byte)((ch1 - '0') << 4);
		//	}
		//	else
		//	{
		//		b = (byte)((ch1 - 'A') << 4);
		//	}
		//	if (Char.IsDigit(ch2))
		//	{
		//		b |= (byte)(ch2 - '0');
		//	}
		//	else
		//	{
		//		b |= (byte)(ch2 - 'A');
		//	}

		//	return b;
		//}

		/// <summary>
		/// A utility function to check whether n is a power of 2 or not.
		/// See http://goo.gl/17Arj
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsPowerOfTwo(uint n)
		{
			return (n > 0 && ((n & (n - 1)) == 0)) ? true : false;
		}

		/// <summary>
		/// Returns position of the first (should be only) set bit in 'n'
		/// </summary>
		public static int FindSetBitPosition(uint n)
		{
			if (!IsPowerOfTwo(n))
			{
				return -1;
			}

			uint i = 1U;
			int pos = 0;

			while ((i & n) == 0U)
			{
				i = i << 1;

				++pos;
			}

			return pos;
		}
	}
}
