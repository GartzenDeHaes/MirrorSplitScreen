using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Portland.Mathmatics
{
	// -- helper for float conversion without allocations or unsafe --
	[StructLayout(LayoutKind.Explicit)]
	public struct IntFloat
	{
		[FieldOffset(0)]
		public float FloatValue;

		[FieldOffset(0)]
		public uint UIntValue;

		[FieldOffset(0)]
		public int IntValue;

		[FieldOffset(0)]
		public byte b0;
		[FieldOffset(1)]
		public byte b1;
		[FieldOffset(2)]
		public byte b2;
		[FieldOffset(3)]
		public byte b3;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct LongDouble
	{
		[FieldOffset(0)]
		public double DoubleValue;

		[FieldOffset(0)]
		public ulong ULongValue;

		[FieldOffset(0)]
		public long LongValue;

		[FieldOffset(0)]
		public byte b0;
		[FieldOffset(1)]
		public byte b1;
		[FieldOffset(2)]
		public byte b2;
		[FieldOffset(3)]
		public byte b3;
		[FieldOffset(4)]
		public byte b4;
		[FieldOffset(5)]
		public byte b5;
		[FieldOffset(6)]
		public byte b6;
		[FieldOffset(7)]
		public byte b7;
	}
}
