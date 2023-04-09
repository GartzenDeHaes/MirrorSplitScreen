using System;

namespace Portland.Mathmatics
{
	public class RandomWithInterface : Random, IRandom
	{
		public bool NextBool()
		{
			return NextFloat() > 0.5f;
		}

		public byte NextByte()
		{
			return (byte)Range(0, 256);
		}

		public byte[] NextBytes(int count)
		{
			byte[] ret = new byte[count];
			for (int x = 0; x < count; x++)
			{
				ret[x] = NextByte();
			}

			return ret;
		}

		public double NextDouble(double max)
		{
			return base.NextDouble() * max;
		}

		public double NextDouble(double min, double max)
		{
			return base.NextDouble() * (max - min) + min;
		}

		public float NextFloat()
		{
			return (float)base.NextDouble();
		}

		public float NextFloat(float max)
		{
			return NextFloat() * max;
		}

		public uint NextUInt()
		{
			return (uint)(NextDouble() * UInt32.MaxValue);
		}

		public uint NextUInt(uint max)
		{
			return (uint)(NextDouble() * max);
		}

		public float Range(float min, float max)
		{
			return NextFloat() * (max - min) + min;
		}

		public int Range(int min, int max)
		{
			return base.Next(min, max);
		}

		public uint Range(uint min, uint max)
		{
			return NextUInt() * (max - min) + min;
		}
	}
}
