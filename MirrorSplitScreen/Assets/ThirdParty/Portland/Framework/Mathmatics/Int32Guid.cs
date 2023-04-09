using System;
using System.Runtime.CompilerServices;

namespace Portland.Mathmatics
{
	[Serializable]
	public struct Int32Guid : IComparable<Int32Guid>
	{
		public int Value;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Init()
		{
			Value = MathHelper.RandomRange(0, Int32.MaxValue);
		}

		public Int32Guid(int val)
		{
			Value = val;
		}

		public int CompareTo(Int32Guid other)
		{
			return Value - other.Value;
		}

		public override bool Equals(object obj)
		{
			return Value == obj.GetHashCode();
		}

		public override int GetHashCode()
		{
			return Value;
		}

		public override string ToString()
		{
			return Value.ToString();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Int32Guid(int d)
		{
			return new Int32Guid(d);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int(Int32Guid d)
		{
			return d.Value;
		}

		public static Int32Guid Parse(string txt)
		{
			return new Int32Guid(Int32.Parse(txt));
		}

		public static Int32Guid Parse(int val)
		{
			return new Int32Guid(val);
		}

		public static Int32Guid Create()
		{
			Int32Guid id;
			id.Value = 0;
			id.Init();
			return id;
		}
	}
}
