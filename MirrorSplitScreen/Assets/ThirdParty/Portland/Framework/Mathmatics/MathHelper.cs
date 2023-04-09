using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Portland.Mathmatics
{
	public static class MathHelper
	{
		public const double E = (double)Math.E;
		public const double Log10E = 0.4342945f;
		public const double Log2E = 1.442695f;
		public const double Pi = (double)Math.PI;
		public const double PiOver2 = (double)(Math.PI / 2.0);
		public const double PiOver4 = (double)(Math.PI / 4.0);
		public const double TwoPi = (double)(Math.PI * 2.0);
		public const double Sqrt2Pi = 2.5066282746310005024;
		public const double Sqrt3 = 1.7320508075688772935;
		public const double Sqrt5 = 2.2360679774997896964;

		// Radians-to-degrees conversion constant (RO).
		public const float Rad2Degf = 1F / Deg2Radf;

		// Radians-to-degrees conversion constant (RO).
		public const double Rad2Degd = 1F / Deg2Radd;

		/// <summary>
		/// Golden angle in radians
		/// </summary>
		public const double GoldenAngle = Math.PI * (3.0 - Sqrt5);

		public static Func<IRandom> CreateRandomInst = () => new RandomWithInterface();

		//[ThreadStatic]
		private static ThreadLocal<IRandom> _random = new ThreadLocal<IRandom>(() => CreateRandomInst());

		public static IRandom Rnd
		{
			get
			{
				//#if UNITY_2019_4_OR_NEWER
				//				if (_random == null) { _random = CreateRandomInst(); }
				//#else
				//				_random ??= CreateRandomInst();
				//#endif

				return _random.Value;
			}
		}

		public static long StartupTick;

		static MathHelper()
		{
			StartupTick = DateTime.Now.Ticks;
		}

		/// <summary>
		/// Random number between low and high inclusive
		/// </summary>
		/// <param name="low">Inclusive</param>
		/// <param name="high">Inclusive</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RandomRange(int low, int high)
		{
			return (int)Math.Floor(Rnd.NextFloat() * (high - low + 1) + low);
			//return (int)((high - low) * Rnd.NextDouble() + low);
		}

		/// <summary>
		/// Random number between low and high inclusive
		/// </summary>
		/// <param name="low">Inclusive</param>
		/// <param name="high">Inclusive</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float RandomRange(float low, float high)
		{
			return (float)Math.Floor(Rnd.NextFloat() * (high - low + 1) + low);
			//return (float)((high - low) * Rnd.NextDouble() + low);
		}

		/// <summary>
		/// Random number between low and high inclusive
		/// </summary>
		/// <param name="low">Inclusive</param>
		/// <param name="high">Inclusive</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double RandomRange(double low, double high)
		{
			return Math.Floor(Rnd.NextDouble() * (high - low + 1) + low);
			//return ((high - low) * Rnd.NextDouble() + low);
		}

		/// <summary>
		/// Returns a float [0,1)
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float RandomNextFloat()
		{
			return Rnd.NextFloat();
		}

		/// <summary>
		/// Returns a float [0,1)
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double RandomNextDouble()
		{
			return Rnd.NextDouble();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RandomNext()
		{
			return Rnd.Next();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RandomNext(int max)
		{
			return (int)(Rnd.NextDouble() * max);
		}

		public static int[] RandomNonRepeating(int count, int low, int high)
		{
			HashSet<int> memo = new HashSet<int>();

			if (count == 0)
			{
				return memo.ToArray();
			}

			if (high - low < count)
			{
				throw new ArgumentException("count is larger than available number range");
			}

			while (count > 0)
			{
				int r = RandomRange(low, high);
				//int r = (int)(range * RandomNextFloat() + low);
				if (memo.Contains(r))
				{
					continue;
				}
				memo.Add(r);
				count--;
			}

			return memo.ToArray();
		}

		public static void RandomBuf(double[] buf)
		{
			for (int x = 0; x < buf.Length; x++)
			{
				buf[x] = Rnd.NextDouble();
			}
		}

		public static void RandomBuf(float[] buf)
		{
			IntFloat cvt = new IntFloat();

			for (int x = 0; x < buf.Length; x++)
			{
				cvt.IntValue = Rnd.Next();
				buf[x] = cvt.FloatValue;
			}
		}

		public static void RandomBuf(int[] buf)
		{
			for (int x = 0; x < buf.Length; x++)
			{
				buf[x] = Rnd.Next();
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Barycentric(double value1, double value2, double value3, double amount1, double amount2)
		{
			return value1 + (value2 - value1) * amount1 + (value3 - value1) * amount2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Barycentric(float value1, float value2, float value3, float amount1, float amount2)
		{
			return value1 + (value2 - value1) * amount1 + (value3 - value1) * amount2;
		}

		public static double CatmullRom(double value1, double value2, double value3, double value4, double amount)
		{
			// Using formula from http://www.mvps.org/directx/articles/catmull/
			// Internally using doubles not to lose precission
			double amountSquared = amount * amount;
			double amountCubed = amountSquared * amount;
			return (double)(0.5 * (2.0 * value2 +
				 (value3 - value1) * amount +
				 (2.0 * value1 - 5.0 * value2 + 4.0 * value3 - value4) * amountSquared +
				 (3.0 * value2 - value1 - 3.0 * value3 + value4) * amountCubed));
		}

		public static float CatmullRom(float value1, float value2, float value3, float value4, float amount)
		{
			// Using formula from http://www.mvps.org/directx/articles/catmull/
			// Internally using doubles not to lose precission
			double amountSquared = amount * amount;
			double amountCubed = amountSquared * amount;
			return (float)(0.5 * (2.0 * value2 +
				 (value3 - value1) * amount +
				 (2.0 * value1 - 5.0 * value2 + 4.0 * value3 - value4) * amountSquared +
				 (3.0 * value2 - value1 - 3.0 * value3 + value4) * amountCubed));
		}

		//public static double Clamp(double value, double min, double max)
		//{
		//	// First we check to see if we're greater than the max
		//	value = (value > max) ? max : value;

		//	// Then we check to see if we're less than the min.
		//	value = (value < min) ? min : value;

		//	// There's no check to see if min > max.
		//	return value;
		//}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Distance(double value1, double value2)
		{
			return Math.Abs(value1 - value2);
		}

		public static double Hermite(double value1, double tangent1, double value2, double tangent2, double amount)
		{
			// All transformed to double not to lose precission
			// Otherwise, for high numbers of param:amount the result is NaN instead of Infinity
			double v1 = value1, v2 = value2, t1 = tangent1, t2 = tangent2, s = amount, result;
			double sCubed = s * s * s;
			double sSquared = s * s;

			if (amount == 0f)
				result = value1;
			else if (amount == 1f)
				result = value2;
			else
				result = (2 * v1 - 2 * v2 + t2 + t1) * sCubed +
					 (3 * v2 - 3 * v1 - 2 * t1 - t2) * sSquared +
					 t1 * s +
					 v1;
			return (double)result;
		}

		public static float Hermite(float value1, float tangent1, float value2, float tangent2, float amount)
		{
			// All transformed to double not to lose precission
			// Otherwise, for high numbers of param:amount the result is NaN instead of Infinity
			float v1 = value1, v2 = value2, t1 = tangent1, t2 = tangent2, s = amount, result;
			float sCubed = s * s * s;
			float sSquared = s * s;

			if (amount == 0f)
				result = value1;
			else if (amount == 1f)
				result = value2;
			else
				result = (2 * v1 - 2 * v2 + t2 + t1) * sCubed +
					 (3 * v2 - 3 * v1 - 2 * t1 - t2) * sSquared +
					 t1 * s +
					 v1;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Max(double value1, double value2)
		{
			return Math.Max(value1, value2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Min(double value1, double value2)
		{
			return Math.Min(value1, value2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Max(float value1, float value2)
		{
			return Math.Max(value1, value2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Min(float value1, float value2)
		{
			return Math.Min(value1, value2);
		}

		public static double SmoothStep(double value1, double value2, double amount)
		{
			// It is expected that 0 < amount < 1
			// If amount < 0, return value1
			// If amount > 1, return value2
#if (USE_FARSEER)
				double result = SilverSpriteMathHelper.Clamp(amount, 0f, 1f);
				result = SilverSpriteMathHelper.Hermite(value1, 0f, value2, 0f, result);
#else
			double result = MathHelper.Clamp(amount, 0f, 1f);
			result = MathHelper.Hermite(value1, 0f, value2, 0f, result);
#endif
			return result;
		}

		public static float SmoothStep(float value1, float value2, float amount)
		{
			// It is expected that 0 < amount < 1
			// If amount < 0, return value1
			// If amount > 1, return value2
#if (USE_FARSEER)
				float result = SilverSpriteMathHelper.Clamp(amount, 0f, 1f);
				result = SilverSpriteMathHelper.Hermite(value1, 0f, value2, 0f, result);
#else
			float result = MathHelper.Clamp(amount, 0f, 1f);
			result = MathHelper.Hermite(value1, 0f, value2, 0f, result);
#endif
			return result;
		}

		public static double ToDegrees(double radians)
		{
			// This method uses double precission internally,
			// though it returns single double
			// Factor = 180 / pi
			return (double)(radians * 57.295779513082320876798154814105);
		}

		public static double ToRadians(double degrees)
		{
			// This method uses double precission internally,
			// though it returns single double
			// Factor = pi / 180
			return (double)(degrees * 0.017453292519943295769236907684886);
		}

		public static double WrapAngle(double angle)
		{
			angle = (double)Math.IEEERemainder((double)angle, 6.2831854820251465);
			if (angle <= -3.14159274f)
			{
				angle += 6.28318548f;
			}
			else
			{
				if (angle > 3.14159274f)
				{
					angle -= 6.28318548f;
				}
			}
			return angle;
		}

		public static bool IsPowerOfTwo(int value)
		{
			return (value > 0) && ((value & (value - 1)) == 0);
		}

		/// <summary>
		/// A tiny floating point value used in comparisons
		/// </summary>
		public const float Epsilonf = 0.00001f;
		// Degrees-to-radians conversion constant (RO).
		public const float Deg2Radf = 3.14159274f * 2F / 360F;
		/// <summary>
		/// Square root of 0.5
		/// </summary>
		public const float Sqrt05f = 0.7071067811865475244f;
		/// <summary>
		/// Square root of 2
		/// </summary>
		public const float Sqrt2f = 1.4142135623730950488f;
		/// <summary>
		/// Square root of 5
		/// </summary>
		public const float Sqrt5f = 2.2360679774997896964f;
		/// <summary>
		/// Golden angle in radians
		/// </summary>
		public const float GoldenAnglef = 3.14159274f * (3f - Sqrt5f);

		/// <summary>
		/// A tiny floating point value used in comparisons
		/// </summary>
		public const double Epsilond = 0.00001f;
		// Degrees-to-radians conversion constant (RO).
		public const double Deg2Radd = Math.PI * 2F / 360d;
		/// <summary>
		/// Square root of 0.5
		/// </summary>
		public const double Sqrt05d = 0.7071067811865475244;
		/// <summary>
		/// Square root of 2
		/// </summary>
		public const double Sqrt2d = 1.4142135623730950488;
		/// <summary>
		/// Square root of 5
		/// </summary>
		public const double Sqrt5d = 2.2360679774997896964;
		/// <summary>
		/// Golden angle in radians
		/// </summary>
		public const double GoldenAngled = Math.PI * (3 - Sqrt5d);

		// Clamps a value between a minimum float and maximum float value.
		public static float Clamp(float value, float min, float max)
		{
			if (value < min || Single.IsNaN(value))
				value = min;
			else if (value > max)
				value = max;
			return value;
		}

		// Clamps value between min and max and returns value.
		// Set the position of the transform to be that of the time
		// but never less than 1 or more than 3
		//
		public static int Clamp(int value, int min, int max)
		{
			if (value < min)
				value = min;
			else if (value > max)
				value = max;
			return value;
		}

		// Loops the value t, so that it is never larger than length and never smaller than 0.
		public static float Repeat(float t, float length)
		{
			return Clamp(t - (float)Math.Floor(t / length) * length, 0.0f, length);
		}

		// Clamps value between 0 and 1 and returns value
		public static float Clamp01(float value)
		{
			if (value < 0F)
				return 0F;
			else if (value > 1F)
				return 1F;
			else
				return value;
		}

		// Calculates the shortest difference between two given angles.
		public static float DeltaAngle(float current, float target)
		{
			float delta = Repeat((target - current), 360.0F);
			if (delta > 180.0F)
				delta -= 360.0F;
			return delta;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		// Interpolates between /a/ and /b/ by /t/. /t/ is clamped between 0 and 1.
		public static float Lerp(float a, float b, float t)
		{
			return a + (b - a) * Clamp01(t);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		// Interpolates between /a/ and /b/ by /t/ without clamping the interpolant.
		public static float LerpUnclamped(float a, float b, float t)
		{
			return a + (b - a) * t;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		// Same as ::ref::Lerp but makes sure the values interpolate correctly when they wrap around 360 degrees.
		public static float LerpAngle(float a, float b, float t)
		{
			float delta = Repeat((b - a), 360);
			if (delta > 180)
				delta -= 360;
			return a + delta * Clamp01(t);
		}

		// Moves a value /current/ towards /target/.
		static public float MoveTowards(float current, float target, float maxDelta)
		{
			if (Math.Abs(target - current) <= maxDelta)
				return target;
			return current + Math.Sign(target - current) * maxDelta;
		}

		// Same as ::ref::MoveTowards but makes sure the values interpolate correctly when they wrap around 360 degrees.
		static public float MoveTowardsAngle(float current, float target, float maxDelta)
		{
			float deltaAngle = DeltaAngle(current, target);
			if (-maxDelta < deltaAngle && deltaAngle < maxDelta)
				return target;
			target = current + deltaAngle;
			return MoveTowards(current, target, maxDelta);
		}

		// Calculates the ::ref::Lerp parameter between of two values.
		public static float InverseLerp(float a, float b, float value)
		{
			if (a != b)
				return Clamp01((value - a) / (b - a));
			else
				return 0.0f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		//<summary>Compares two floating point values if they are similar.</summary>
		public static bool Approximately(Half a, Half b)
		{
			return Math.Abs(b - a) < 0.00001;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		// Compares two floating point values if they are similar.
		public static bool Approximately(float a, float b)
		{
			//return MathF.Abs(a) - MathF.Abs(b) < Single.Epsilon * 8f;
			// If a or b is zero, compare that the other is less or equal to epsilon.
			// If neither a or b are 0, then find an epsilon that is good for
			// comparing numbers at the maximum magnitude of a and b.
			// Floating points have about 7 significant digits, so
			// 1.000001f can be represented while 1.0000001f is rounded to zero,
			// thus we could use an epsilon of 0.000001f for comparing values close to 1.
			// We multiply this epsilon by the biggest magnitude of a and b.
			return Math.Abs(b - a) < Math.Max(0.000001f * Math.Max(Math.Abs(a), Math.Abs(b)), Epsilonf * 8);
		}

		// Clamps a value between a minimum float and maximum float value.
		public static double Clamp(double value, double min, double max)
		{
			if (value < min)
				value = min;
			else if (value > max)
				value = max;
			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		// Loops the value t, so that it is never larger than length and never smaller than 0.
		public static double Repeat(double t, double length)
		{
			return Clamp(t - Math.Floor(t / length) * length, 0.0f, length);
		}

		// Clamps value between 0 and 1 and returns value
		public static double Clamp01(double value)
		{
			if (value < 0F)
				return 0F;
			else if (value > 1F)
				return 1F;
			else
				return value;
		}

		// Calculates the shortest difference between two given angles.
		public static double DeltaAngle(double current, double target)
		{
			double delta = Repeat((target - current), 360.0F);
			if (delta > 180.0F)
				delta -= 360.0F;
			return delta;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		// Interpolates between /a/ and /b/ by /t/. /t/ is clamped between 0 and 1.
		public static double Lerp(double a, double b, double t)
		{
			return a + (b - a) * Clamp01(t);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		// Interpolates between /a/ and /b/ by /t/ without clamping the interpolant.
		public static double LerpUnclamped(double a, double b, double t)
		{
			return a + (b - a) * t;
		}

		// Same as ::ref::Lerp but makes sure the values interpolate correctly when they wrap around 360 degrees.
		public static double LerpAngle(double a, double b, double t)
		{
			double delta = Repeat((b - a), 360);
			if (delta > 180)
				delta -= 360;
			return a + delta * Clamp01(t);
		}

		// Moves a value /current/ towards /target/.
		static public double MoveTowards(double current, double target, double maxDelta)
		{
			if (Math.Abs(target - current) <= maxDelta)
				return target;
			return current + Math.Sign(target - current) * maxDelta;
		}

		// Same as ::ref::MoveTowards but makes sure the values interpolate correctly when they wrap around 360 degrees.
		static public double MoveTowardsAngle(double current, double target, double maxDelta)
		{
			double deltaAngle = DeltaAngle(current, target);
			if (-maxDelta < deltaAngle && deltaAngle < maxDelta)
				return target;
			target = current + deltaAngle;
			return MoveTowards(current, target, maxDelta);
		}

		// Calculates the ::ref::Lerp parameter between of two values.
		public static double InverseLerp(double a, double b, double value)
		{
			if (a != b)
				return Clamp01((value - a) / (b - a));
			else
				return 0.0f;
		}

		// Compares two floating point values if they are similar.
		public static bool Approximately(double a, double b)
		{
			// If a or b is zero, compare that the other is less or equal to epsilon.
			// If neither a or b are 0, then find an epsilon that is good for
			// comparing numbers at the maximum magnitude of a and b.
			// Floating points have about 15 significant digits, so
			// 1.000000000000001f can be represented while 1.0000001f is rounded to zero,
			// thus we could use an epsilon of 0.000001f for comparing values close to 1.
			// We multiply this epsilon by the biggest magnitude of a and b.
			return Math.Abs(b - a) < Math.Max(0.000000000000001f * Math.Max(Math.Abs(a), Math.Abs(b)), Epsilond * 16);
		}

		public static ushort CRC16(byte[] data)
		{
			ushort wdata, wCRC = 0;
			for (int i = 0; i < data.Length; i++)
			{
				wdata = (ushort)(data[i] << 8);

				for (int j = 0; j < 8; j++, wdata <<= 1)
				{
					var a = (wCRC ^ wdata) & 0x8000;
					if (a != 0)
					{
						var c = (wCRC << 1) ^ 0x1021;
						wCRC = (ushort)c;
					}
					else
					{
						wCRC <<= 1;
					}
				}
			}
			return wCRC;
		}
	}
}
