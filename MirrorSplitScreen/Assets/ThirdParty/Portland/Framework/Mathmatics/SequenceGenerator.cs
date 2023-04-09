using System;
using System.Collections.Generic;
using System.Text;

using Portland.Text;

namespace Portland.Mathmatics
{
	// not thread safe
	public class SequenceGenerator
	{
		private readonly ulong[] _state = new ulong[4];

		public SequenceGenerator(string seed)
		: this((ulong)StringHelper.HashMurmur64(seed))
		{
		}

		public SequenceGenerator(ulong seed)
		{
			_state[0] = Splitmix64(ref seed);
			_state[1] = Splitmix64(ref seed);
			_state[2] = Splitmix64(ref seed);
			_state[3] = Splitmix64(ref seed);
		}

		public ulong Next()
		{
			ulong result = unchecked(_state[0] + _state[3]);
			ulong t = unchecked(_state[1] << 17);

			_state[2] ^= _state[0];
			_state[3] ^= _state[1];
			_state[1] ^= _state[2];
			_state[0] ^= _state[3];

			_state[2] ^= t;
			_state[3] = Rol64(_state[3], 45);

			return result;
		}

		public void NextAligned(byte[] bytes)
		{
			NextAligned(bytes, 0, bytes.Length);
		}

		public void Next(byte[] bytes)
		{
			Next(bytes, 0, bytes.Length);
		}

		public void NextAligned(byte[] bytes, int start, int len)
		{
			ulong l;
			int end = start + len;
			for (int x = start; x < end; x +=8)
			{
				l = Next();

				unchecked
				{
					bytes[x] = (byte)(l & 0xFF);
					bytes[x + 1] = (byte)((l >> 8) & 0xFF);
					bytes[x + 2] = (byte)((l >> 16) & 0xFF);
					bytes[x + 3] = (byte)((l >> 24) & 0xFF);
					bytes[x + 4] = (byte)((l >> 32) & 0xFF);
					bytes[x + 5] = (byte)((l >> 40) & 0xFF);
					bytes[x + 6] = (byte)((l >> 48) & 0xFF);
					bytes[x + 7] = (byte)((l >> 56) & 0xFF);
				}
			}
		}

		public void Next(byte[] bytes, int start, int len)
		{
			ulong l;
			int end = start + len;
			int x = start;
			while (x < end)
			{
				l = Next();
				if (x < end)
				{
					bytes[x++] = unchecked((byte)(l & 0xFF));
				}
				if (x < end)
				{
					bytes[x++] = unchecked((byte)((l>>8) & 0xFF));
				}
				if (x < end)
				{
					bytes[x++] = unchecked((byte)((l >> 16) & 0xFF));
				}
				if (x < end)
				{
					bytes[x++] = unchecked((byte)((l >> 24) & 0xFF));
				}
				if (x < end)
				{
					bytes[x++] = unchecked((byte)((l >> 32) & 0xFF));
				}
				if (x < end)
				{
					bytes[x++] = unchecked((byte)((l >> 40) & 0xFF));
				}
				if (x < end)
				{
					bytes[x++] = unchecked((byte)((l >> 48) & 0xFF));
				}
				if (x < end)
				{
					bytes[x++] = unchecked((byte)((l >> 56) & 0xFF));
				}
			}
		}

		private static ulong Rol64(ulong x, int k)
		{
			return (x << k) | (x >> (64 - k));
		}

		private static ulong Splitmix64(ref ulong state)
		{
			ulong result = state;
			state = unchecked(result + 0x9E3779B97f4A7C15);
			result = unchecked((result ^ (result >> 30)) * 0xBF58476D1CE4E5B9);
			result = unchecked((result ^ (result >> 27)) * 0x94D049BB133111EB);
			return unchecked(result ^ (result >> 31));
		}
	}
}
