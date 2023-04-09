using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// from namespace Unity.Mathematics
// change NS to avoid conflicts.  Note, Net 5.0 has System.Half
namespace Portland.Mathmatics
{
	/// <summary>
	/// A half precision float that uses 16 bits instead of 32 bits.
	/// </summary>
	[Serializable]
	public struct Half : IEquatable<Half>, IFormattable
	{
		/// <summary>Returns the minimum of two uint values.</summary>
		/// <param name="x">The first input value.</param>
		/// <param name="y">The second input value.</param>
		/// <returns>The minimum of the two input values.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint min(uint x, uint y)
		{
			return x < y ? x : y;
		}

		/// <summary>Returns the minimum of two float values.</summary>
		/// <param name="x">The first input value.</param>
		/// <param name="y">The second input value.</param>
		/// <returns>The minimum of the two input values.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static float min(float x, float y)
		{
			return Single.IsNaN(y) || x < y ? x : y;
		}

		/// <summary>Returns trueValue if test is true, falseValue otherwise.</summary>
		/// <param name="falseValue">Value to use if test is false.</param>
		/// <param name="trueValue">Value to use if test is true.</param>
		/// <param name="test">Bool value to choose between falseValue and trueValue.</param>
		/// <returns>The selection between falseValue and trueValue according to bool test.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint select(uint falseValue, uint trueValue, bool test)
		{
			return test ? trueValue : falseValue;
		}

		private static uint asuint(float f)
		{
			IntFloat ivf;
			ivf.UIntValue = 0U;
			ivf.FloatValue = f;
			return ivf.UIntValue;
		}

		private static float asfloat(uint i)
		{
			IntFloat ivf;
			ivf.FloatValue = 0.0f;
			ivf.UIntValue = i;
			return ivf.FloatValue;
		}

		/// <summary>Returns the floating point representation of a half-precision floating point value.</summary>
		/// <param name="x">The half precision float.</param>
		/// <returns>The single precision float representation of the half precision float.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static float f16tof32(uint x)
		{
			const uint shifted_exp = (0x7c00 << 13);
			uint uf = (x & 0x7fff) << 13;
			uint e = uf & shifted_exp;
			uf += (127 - 15) << 23;
			uf += select(0, (128u - 16u) << 23, e == shifted_exp);
			uf = select(uf, asuint(asfloat(uf + (1 << 23)) - 6.10351563e-05f), e == 0);
			uf |= (x & 0x8000) << 16;
			return asfloat(uf);
		}

		/// <summary>Returns the result converting a float value to its nearest half-precision floating point representation.</summary>
		/// <param name="x">The single precision float.</param>
		/// <returns>The half precision float representation of the single precision float.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint f32tof16(float x)
		{
			const int infinity_32 = 255 << 23;
			const uint msk = 0x7FFFF000u;

			uint ux = asuint(x);
			uint uux = ux & msk;
			uint h = (uint)(asuint(min(asfloat(uux) * 1.92592994e-34f, 260042752.0f)) + 0x1000) >> 13;   // Clamp to signed infinity if overflowed
			h = select(h, select(0x7c00u, 0x7e00u, (int)uux > infinity_32), (int)uux >= infinity_32);   // NaN->qNaN and Inf->Inf
			return h | (ux & ~msk) >> 16;
		}

		/// <summary>
		/// The raw 16 bit value of the half.
		/// </summary>
		public ushort value;

		/// <summary>half zero value.</summary>
		public static readonly Half zero = new Half();

		/// <summary>
		/// The maximum finite half value as a single precision float.
		/// </summary>
		public const float MaxValue = 65504.0f;

		/// <summary>
		/// The minimum finite half value as a single precision float.
		/// </summary>
		public const float MinValue = -65504.0f;

		/// <summary>
		/// The maximum finite half value as a half.
		/// </summary>
		public static Half MaxValueAsHalf = new Half(MaxValue);

		/// <summary>
		/// The minimum finite half value as a half.
		/// </summary>
		public static Half MinValueAsHalf = new Half(MinValue);

		/// <summary>Constructs a half value from a half value.</summary>
		/// <param name="x">The input half value to copy.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Half(Half x)
		{
			value = x.value;
		}

		/// <summary>Constructs a half value from a float value.</summary>
		/// <param name="v">The single precision float value to convert to half.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Half(float v)
		{
			value = (ushort)f32tof16(v);
		}

		/// <summary>Constructs a half value from a double value.</summary>
		/// <param name="v">The double precision float value to convert to half.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Half(double v)
		{
			value = (ushort)f32tof16((float)v);
		}

		/// <summary>Explicitly converts a float value to a half value.</summary>
		/// <param name="v">The single precision float value to convert to half.</param>
		/// <returns>The converted half value.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Half(float v) { return new Half(v); }

		/// <summary>Explicitly converts a double value to a half value.</summary>
		/// <param name="v">The double precision float value to convert to half.</param>
		/// <returns>The converted half value.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Half(double v) { return new Half(v); }

		/// <summary>Implicitly converts a half value to a float value.</summary>
		/// <param name="d">The half value to convert to a single precision float.</param>
		/// <returns>The converted single precision float value.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float(in Half d) { return f16tof32(d.value); }

		/// <summary>Implicitly converts a half value to a double value.</summary>
		/// <param name="d">The half value to convert to double precision float.</param>
		/// <returns>The converted double precision float value.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double(in Half d) { return f16tof32(d.value); }

		/// <summary>Returns whether two half values are bitwise equivalent.</summary>
		/// <param name="lhs">Left hand side half value to use in comparison.</param>
		/// <param name="rhs">Right hand side half value to use in comparison.</param>
		/// <returns>True if the two half values are bitwise equivalent, false otherwise.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in Half lhs, in Half rhs) { return lhs.value == rhs.value; }

		/// <summary>Returns whether two half values are not bitwise equivalent.</summary>
		/// <param name="lhs">Left hand side half value to use in comparison.</param>
		/// <param name="rhs">Right hand side half value to use in comparison.</param>
		/// <returns>True if the two half values are not bitwise equivalent, false otherwise.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in Half lhs, in Half rhs) { return lhs.value != rhs.value; }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Half operator +(in Half lhs, in Half rhs)
		{ return (Half)((float)lhs + (float)rhs); }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Half operator -(in Half lhs, in Half rhs)
		{ return (Half)((float)lhs - (float)rhs); }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Half operator *(in Half lhs, in Half rhs)
		{ return (Half)((float)lhs * (float)rhs); }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Half operator /(in Half lhs, in Half rhs)
		{ return (Half)((float)lhs / (float)rhs); }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Half operator +(in Half lhs, float rhs)
		{ return (Half)((float)lhs + rhs); }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Half operator -(in Half lhs, float rhs)
		{ return (Half)((float)lhs - rhs); }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Half operator *(in Half lhs, float rhs)
		{ return (Half)((float)lhs * rhs); }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Half operator /(in Half lhs, float rhs)
		{ return (Half)((float)lhs / rhs); }

		/// <summary>Returns true if the half is bitwise equivalent to a given half, false otherwise.</summary>
		/// <param name="rhs">Right hand side half value to use in comparison.</param>
		/// <returns>True if the half value is bitwise equivalent to the input, false otherwise.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Half rhs) { return value == rhs.value; }

		/// <summary>Returns true if the half is equal to a given half, false otherwise.</summary>
		/// <param name="o">Right hand side object to use in comparison.</param>
		/// <returns>True if the object is of type half and is bitwise equivalent, false otherwise.</returns>
		public override bool Equals(object o) { return o is Half converted && Equals(converted); }

		/// <summary>Returns a hash code for the half.</summary>
		/// <returns>The computed hash code of the half.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)value;
		}

		/// <summary>Returns a string representation of the half.</summary>
		/// <returns>The string representation of the half.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return f16tof32(value).ToString();
		}

		/// <summary>Returns a string representation of the half using a specified format and culture-specific format information.</summary>
		/// <param name="format">The format string to use during string formatting.</param>
		/// <param name="formatProvider">The format provider to use during string formatting.</param>
		/// <returns>The string representation of the half.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return f16tof32(value).ToString(format, formatProvider);
		}

		public static Half FromRaw(ushort value)
		{
			Half ret = new Half();
			ret.value = value;
			return ret;
		}
	}
}
