//using System;
//using System.Runtime.CompilerServices;

//#if UNITY_5_3_OR_NEWER
//using UnityEngine;
//#endif

//namespace Portland.Mathmatics
//{
//#if UNITY_5_3_OR_NEWER || !NETCOREAPP
//	public static class MathF
//	{
//#if UNITY_5_3_OR_NEWER
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Abs(float d) { return UnityEngine.Mathf.Abs(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Atan(float d) { return UnityEngine.Mathf.Atan(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Asin(float d) { return UnityEngine.Mathf.Asin(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Cos(float d) { return UnityEngine.Mathf.Cos(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static int Ceiling(float d) { return UnityEngine.Mathf.CeilToInt(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Round(float d) { return UnityEngine.Mathf.Round(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Log(float d) { return UnityEngine.Mathf.Log(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Pow(float d, float p) { return UnityEngine.Mathf.Pow(d, p); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Exp(float d) { return UnityEngine.Mathf.Exp(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Tan(float d) { return UnityEngine.Mathf.Tan(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Sqrt(float d) { return UnityEngine.Mathf.Sqrt(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Sin(float d) { return UnityEngine.Mathf.Sin(d); }
//		public const float PI = UnityEngine.Mathf.PI;
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Max(float a, float b) { return UnityEngine.Mathf.Max(a, b); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Min(float a, float b) { return UnityEngine.Mathf.Min(a, b); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Floor(float d) { return UnityEngine.Mathf.Floor(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Sign(float d) { return UnityEngine.Mathf.Sign(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Acos(float d) { return UnityEngine.Mathf.Acos(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Atan2(float d1, float d2) { return UnityEngine.Mathf.Atan2(d1, d2); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Clamp01(float f) { return UnityEngine.Mathf.Clamp01(f); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Clamp(float value, float min, float max) { return UnityEngine.Mathf.Clamp(value, min, max); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static int RandomRange(int low, int high) { return UnityEngine.Random.Range(low, high); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float RandomRange(float low, float high) { return UnityEngine.Random.Range(low, high); }
//#elif !NETCOREAPP
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Abs(float d) { return Math.Abs(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Atan(float d) { return (float)Math.Atan(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Cos(float d) { return (float)Math.Cos(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static int Ceiling(float d) { return (int)Math.Ceiling(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Log(float d) { return (float)Math.Log(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Exp(float d) { return (float)Math.Exp(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Tan(float d) { return (float)Math.Tan(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Sqrt(float d) { return (float)Math.Sqrt(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Sin(float d) { return (float)Math.Sin(d); }
//		public const float PI = (float)Math.PI;
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Max(float a, float b) { return Math.Max(a, b); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Min(float a, float b) { return Math.Min(a, b); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Floor(float d) { return (float)Math.Floor(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Sign(float d) { return (float)Math.Sign(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Acos(float d) { return (float)Math.Acos(d); }
//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		public static float Atan2(float d1, float d2) { return (float)Math.Atan2(d1, d2); }

//		// Clamps a value between a minimum float and maximum float value.
//		public static float Clamp(float value, float min, float max)
//		{
//			if (value < min)
//				value = min;
//			else if (value > max)
//				value = max;
//			return value;
//		}

//		// Clamps value between min and max and returns value.
//		// Set the position of the transform to be that of the time
//		// but never less than 1 or more than 3
//		//
//		public static int Clamp(int value, int min, int max)
//		{
//			if (value < min)
//				value = min;
//			else if (value > max)
//				value = max;
//			return value;
//		}

//		// Clamps value between 0 and 1 and returns value
//		public static float Clamp01(float value)
//		{
//			if (value < 0F)
//				return 0F;
//			else if (value > 1F)
//				return 1F;
//			else
//				return value;
//		}

//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		// Interpolates between /a/ and /b/ by /t/. /t/ is clamped between 0 and 1.
//		public static float Lerp(float a, float b, float t)
//		{
//			return a + (b - a) * Clamp01(t);
//		}

//		[MethodImpl(MethodImplOptions.AggressiveInlining)]
//		// Interpolates between /a/ and /b/ by /t/ without clamping the interpolant.
//		public static float LerpUnclamped(float a, float b, float t)
//		{
//			return a + (b - a) * t;
//		}

//		// Moves a value /current/ towards /target/.
//		static public float MoveTowards(float current, float target, float maxDelta)
//		{
//			if (Math.Abs(target - current) <= maxDelta)
//				return target;
//			return current + Math.Sign(target - current) * maxDelta;
//		}
//#endif
//	}
//#endif
//}
