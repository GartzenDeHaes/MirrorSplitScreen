using System;
using System.Runtime.CompilerServices;

namespace Portland.Mathmatics
{
	[Serializable]
	public struct Int64Guid : IComparable<Int64Guid>
	{
		public long Value;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Init()
		{
			Value = (long)MathHelper.RandomNext() << 32 | (long)MathHelper.RandomNext();
		}

		public Int64Guid(long val)
		{
			Value = val;
		}

		public int CompareTo(Int64Guid other)
		{
			return (int)(Value - other.Value);
		}

		public override bool Equals(object obj)
		{
			return Value == obj.GetHashCode();
		}

		public override int GetHashCode()
		{
			return (int)(Value & 0xFFFFFFFF | Value >> 32);
		}

		public override string ToString()
		{
			return Value.ToString();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Int64Guid(long d)
		{
			return new Int64Guid(d);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator long(Int64Guid d)
		{
			return d.Value;
		}

		public static Int64Guid Parse(string txt)
		{
			return new Int64Guid(Int64.Parse(txt));
		}

		public static Int64Guid Parse(int val)
		{
			return new Int64Guid(val);
		}

		public static Int64Guid Create()
		{
			Int64Guid id;
			id.Value = 0;
			id.Init();
			return id;
		}
	}
}
