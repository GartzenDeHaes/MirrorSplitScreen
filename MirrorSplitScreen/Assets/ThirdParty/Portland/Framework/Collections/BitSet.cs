using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Portland.Collections
{
	/// <summary>
	/// Convience helper for bits
	/// </summary>
	[Serializable]
	public struct BitSet64 : IEquatable<BitSet64>
	{
		/// <summary></summary>
		public ulong RawBits;

		public bool this[int index]
		{
			get { return IsSet(index); }
			set { SetTest(index, value); }
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsSet(int bitNum)
		{
			return (RawBits & (1UL << bitNum)) != 0UL;
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ClearBit(int bitNum)
		{
			RawBits &= ~(1UL << bitNum);
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ClearBits(in BitSet64 clearTheseBits)
		{
			RawBits &= ~clearTheseBits.RawBits;
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetBit(int bitNum)
		{
			RawBits |= (1UL << bitNum);
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetTest(int bitNum, bool value)
		{
			RawBits = value ? (RawBits | (1UL << bitNum)) : (RawBits & ~(1UL << bitNum));
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsAnySet(in BitSet64 bitsToCheck)
		{
			return (RawBits & bitsToCheck.RawBits) != 0UL;
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsAllSet(in BitSet64 bitsToCheck)
		{
			return (RawBits & bitsToCheck.RawBits) == bitsToCheck.RawBits;
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsNoneSet(in BitSet64 bitsToCheck)
		{
			return (RawBits & bitsToCheck.RawBits) == 0UL;
		}

		/// <summary></summary>
		public ulong BitsAt(int bitNum, int count)
		{
			ulong result = 0UL;
			for (int x = 0; x < count; x++)
			{
				result |= RawBits & (1UL << x + bitNum);
			}

			return result >> bitNum;
		}

		/// <summary></summary>
		public void SetBitsAt(int bitNum, int count, uint value)
		{
			for (int x = 0; x < count; x++)
			{
				SetTest(bitNum + x, (value & (1UL << x)) != 0UL);
			}
		}

		/// <summary>Returns the number of bits set</summary>
		public int NumberOfBitsSet()
		{
			int count = 0;
			for (int x = 0; x < 64; x++)
			{
				count += this.IsSet(x) ? 1 : 0;
			}
			return count;
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return RawBits.GetHashCode();
		}

		/// <summary></summary>
		public override bool Equals(object obj)
		{
			if (obj is ulong ui8)
			{
				return (ulong)ui8 == RawBits;
			}
			if (obj is uint ui4)
			{
				return (ulong)ui4 == RawBits;
			}
			if (obj is long i8)
			{
				return (ulong)i8 == RawBits;
			}
			if (obj is int i4)
			{
				return (ulong)i4 == RawBits;
			}

			return false;
		}

		/// <summary></summary>
		public override string ToString()
		{
			StringBuilder buf = new StringBuilder(64);

			for (int x = 0; x < 64; x++)
			{
				buf.Append(IsSet(x) ? '1' : '0');
			}

			return buf.ToString();
		}

		public bool Equals(BitSet64 other)
		{
			return RawBits == other.RawBits;
		}

		public bool Equals(in BitSet64 other)
		{
			return RawBits == other.RawBits;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(BitSet64 b1, BitSet64 b2)
		{
			return b1.RawBits == b2.RawBits;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(BitSet64 b1, BitSet64 b2)
		{
			return b1.RawBits != b2.RawBits;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(BitSet64 b1, ulong b2)
		{
			return b1.RawBits == b2;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(BitSet64 b1, ulong b2)
		{
			return b1.RawBits != b2;
		}
	}

	/// <summary>
	/// Convience helper for bits
	/// </summary>
	[Serializable]
	public struct BitSet32
	{
		/// <summary>the bits</summary>
		public uint RawBits;

		public bool this[int index]
		{
			get { return IsSet(index); }
			set { SetTest(index, value); }
		}

		/// <summary>Is bit number bitNum set?</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsSet(int bitNum)
		{
			return (RawBits & (1U << bitNum)) != 0U;
		}

		/// <summary>Clear the bit</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ClearBit(int bitNum)
		{
			RawBits &= ~(1U << bitNum);
		}

		/// <summary>Clear the bits</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ClearBits(in BitSet32 clearTheseBits)
		{
			RawBits &= ~clearTheseBits.RawBits;
		}

		/// <summary>Set the bit</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetBit(int bitNum)
		{
			RawBits |= (1U << bitNum);
		}

		/// <summary>Set or clear the bit</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetTest(int bitNum, bool value)
		{
			RawBits = value ? (RawBits | (1U << bitNum)) : (RawBits & ~(1U << bitNum));
		}

		/// <summary>Is any of the bits set?</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsAnySet(in BitSet32 bitsToCheck)
		{
			return (RawBits & bitsToCheck.RawBits) != 0U;
		}

		/// <summary>Are all of the bits set?</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsAllSet(in BitSet32 bitsToCheck)
		{
			return (RawBits & bitsToCheck.RawBits) == bitsToCheck.RawBits;
		}

		/// <summary>Are none of the bits set?</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsNoneSet(in BitSet32 bitsToCheck)
		{
			return (RawBits & bitsToCheck.RawBits) == 0U;
		}

		/// <summary>Covert a subset of bits to an integer</summary>
		public uint BitsAt(int bitNum, int count)
		{
			uint result = 0U;
			for (int x = 0; x < count; x++)
			{
				result |= RawBits & (1U << (x + bitNum));
			}

			return result >> bitNum;
		}

		/// <summary></summary>
		public void SetBitsAt(int bitNum, int count, uint value)
		{
			for (int x = 0; x < count; x++)
			{
				SetTest(bitNum + x, (value & (1U << x)) != 0U);
			}
		}

		/// <summary>Returns the number of bits set</summary>
		public int NumberOfBitsSet()
		{
			int count = 0;
			for (int x = 0; x < 32; x++)
			{
				count += this.IsSet(x) ? 1 : 0;
			}
			return count;
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)RawBits;
		}

		/// <inheritdoc/>
		public override bool Equals(object obj)
		{
			if (obj is ulong ui8)
			{
				return (uint)ui8 == RawBits;
			}
			if (obj is uint ui4)
			{
				return ui4 == RawBits;
			}
			if (obj is long i8)
			{
				return (uint)i8 == RawBits;
			}
			if (obj is int i4)
			{
				return (uint)i4 == RawBits;
			}

			return false;
		}

		/// <inheritdoc/>
		public override string ToString()
		{
			StringBuilder buf = new StringBuilder(32);

			for (int x = 0; x < 32; x++)
			{
				buf.Append(IsSet(x) ? '1' : '0');
			}

			return buf.ToString();
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in BitSet32 b1, in BitSet32 b2)
		{
			return b1.RawBits == b2.RawBits;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in BitSet32 b1, in BitSet32 b2)
		{
			return b1.RawBits != b2.RawBits;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in BitSet32 b1, uint b2)
		{
			return b1.RawBits == b2;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in BitSet32 b1, uint b2)
		{
			return b1.RawBits != b2;
		}

		public BitSet32(uint bits)
		{
			RawBits = bits;
		}
	}

	/// <summary>
	/// Convience helper for bits
	/// </summary>
	[Serializable]
	public struct BitSet16
	{
		/// <summary></summary>
		public ushort RawBits;

		public bool this[int index]
		{
			get { return IsSet(index); }
			set { SetTest(index, value); }
		}

		public BitSet16(ushort bits)
		{
			RawBits = bits;
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsSet(int bitNum)
		{
			return (RawBits & (1U << bitNum)) != 0U;
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ClearBit(int bitNum)
		{
			RawBits &= (ushort)~(1U << bitNum);
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ClearBits(in BitSet16 clearTheseBits)
		{
			RawBits &= (ushort)~clearTheseBits.RawBits;
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetBit(int bitNum)
		{
			RawBits |= (ushort)(1U << bitNum);
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetTest(int bitNum, bool value)
		{
			RawBits = (ushort)(value ? (RawBits | (1U << bitNum)) : (RawBits & ~(1U << bitNum)));
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsAnySet(in BitSet16 bitsToCheck)
		{
			return (RawBits & bitsToCheck.RawBits) != 0U;
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsAllSet(in BitSet16 bitsToCheck)
		{
			return (RawBits & bitsToCheck.RawBits) == bitsToCheck.RawBits;
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsNoneSet(in BitSet16 bitsToCheck)
		{
			return (RawBits & bitsToCheck.RawBits) == 0U;
		}

		/// <summary></summary>
		public ushort BitsAt(int bitNum, int count)
		{
			ushort result = 0;
			for (int x = 0; x < count; x++)
			{
				result |= (ushort)(RawBits & (1U << x + bitNum));
			}

			return (ushort)(result >> bitNum);
		}

		/// <summary></summary>
		public void SetBitsAt(int bitNum, int count, uint value)
		{
			for (int x = 0; x < count; x++)
			{
				SetTest(bitNum + x, (value & (1U << x)) != 0U);
			}
		}

		/// <summary>Returns the number of bits set</summary>
		public int NumberOfBitsSet()
		{
			int count = 0;
			for (int x = 0; x < 16; x++)
			{
				count += this.IsSet(x) ? 1 : 0;
			}
			return count;
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return RawBits;
		}

		/// <summary></summary>
		public override bool Equals(object obj)
		{
			if (obj is ulong ui8)
			{
				return (ushort)ui8 == RawBits;
			}
			if (obj is uint ui4)
			{
				return (ushort)ui4 == RawBits;
			}
			if (obj is long i8)
			{
				return (ushort)i8 == RawBits;
			}
			if (obj is int i4)
			{
				return (ushort)i4 == RawBits;
			}

			return false;
		}

		/// <summary></summary>
		public override string ToString()
		{
			StringBuilder buf = new StringBuilder(16);

			for (int x = 0; x < 16; x++)
			{
				buf.Append(IsSet(x) ? '1' : '0');
			}

			return buf.ToString();
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in BitSet16 b1, in BitSet16 b2)
		{
			return b1.RawBits == b2.RawBits;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in BitSet16 b1, in BitSet16 b2)
		{
			return b1.RawBits != b2.RawBits;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in BitSet16 b1, ushort b2)
		{
			return b1.RawBits == b2;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in BitSet16 b1, ushort b2)
		{
			return b1.RawBits != b2;
		}
	}
}
