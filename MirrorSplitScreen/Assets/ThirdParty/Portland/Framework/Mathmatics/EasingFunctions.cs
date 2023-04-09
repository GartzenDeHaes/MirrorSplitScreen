﻿using System;

using Microsoft.VisualBasic;

namespace Portland.Mathmatics
{
	// https://easings.net/
	//
	public static class EasingFunctions
	{
		public static float Linear(float t)
		{
			return t;
		}

		public static float InQuad(float t)
		{
			return t * t;
		}

		public static float OutQuad(float t)
		{
			return 1 - InQuad(1 - t);
		}

		public static float InOutQuad(float t)
		{
			if (t < 0.5) 
				return InQuad(t * 2) / 2;
			return 1 - InQuad((1 - t) * 2) / 2;
		}

		public static float InCubic(float t)
		{
			return t * t * t;
		}

		public static float OutCubic(float t)
		{
			return 1 - InCubic(1 - t);
		}

		public static float InOutCubic(float t)
		{
			if (t < 0.5) 
				return InCubic(t * 2) / 2;
			return 1 - InCubic((1 - t) * 2) / 2;
		}

		public static float InQuart(float t)
		{
			return t * t * t * t;
		}

		public static float OutQuart(float t)
		{
			return 1 - InQuart(1 - t);
		}

		public static float InOutQuart(float x0to1)
		{
			if (x0to1 < 0.5) 
				return InQuart(x0to1 * 2) / 2;
			return 1 - InQuart((1 - x0to1) * 2) / 2;
		}

		public static float InQuint(float t)
		{
			return t * t * t * t * t;
		}

		public static float OutQuint(float t)
		{
			return 1 - InQuint(1 - t);
		}

		public static float InOutQuint(float t)
		{
			if (t < 0.5) 
				return InQuint(t * 2) / 2;
			return 1 - InQuint((1 - t) * 2) / 2;
		}

		//public static float InOutQuart(float x0to1)
		//{
		//	return x0to1 < 0.5 ? 8 * x0to1 * x0to1 * x0to1 * x0to1 : 1f - MathF.Pow(-2 * x0to1 + 2, 4) / 2;
		//}

		public static float InSine(float t)
		{
			return (float)-Math.Cos(t * Math.PI / 2);
		}

		public static float OutSine(float t)
		{
			return (float)Math.Sin(t * Math.PI / 2);
		}

		public static float InOutSine(float t)
		{
			return (float)(Math.Cos(t * Math.PI) - 1) / -2;
		}

		public static float InExpo(float t)
		{
			return (float)Math.Pow(2, 10 * (t - 1));
		}

		public static float OutExpo(float t)
		{
			return 1 - InExpo(1 - t);
		}

		public static float InOutExpo(float t)
		{
			if (t < 0.5) 
				return InExpo(t * 2) / 2;
			return 1 - InExpo((1 - t) * 2) / 2;
		}

		public static float InCirc(float t)
		{
			return -((float)Math.Sqrt(1 - t * t) - 1);
		}

		public static float OutCirc(float t)
		{
			return 1 - InCirc(1 - t);
		}

		public static float InOutCirc(float t)
		{
			if (t < 0.5) 
				return InCirc(t * 2) / 2;
			return 1 - InCirc((1 - t) * 2) / 2;
		}

		public static float InElastic(float t)
		{
			return 1 - OutElastic(1 - t);
		}

		public static float OutElastic(float t)
		{
			float p = 0.3f;
			return (float)Math.Pow(2, -10 * t) * (float)Math.Sin((t - p / 4) * (2 * Math.PI) / p) + 1;
		}

		public static float InOutElastic(float t)
		{
			if (t < 0.5) 
				return InElastic(t * 2) / 2;
			return 1 - InElastic((1 - t) * 2) / 2;
		}

		public static float InBack(float t)
		{
			float s = 1.70158f;
			return t * t * ((s + 1) * t - s);
		}

		public static float OutBack(float t)
		{
			return 1 - InBack(1 - t);
		}

		public static float InOutBack(float t)
		{
			if (t < 0.5) 
				return InBack(t * 2) / 2;
			return 1 - InBack((1 - t) * 2) / 2;
		}

		public static float InBounce(float t)
		{
			return 1 - OutBounce(1 - t);
		}

		public static float OutBounce(float t)
		{
			float div = 2.75f;
			float mult = 7.5625f;

			if (t < 1 / div)
			{
				return mult * t * t;
			}
			else if (t < 2 / div)
			{
				t -= 1.5f / div;
				return mult * t * t + 0.75f;
			}
			else if (t < 2.5 / div)
			{
				t -= 2.25f / div;
				return mult * t * t + 0.9375f;
			}
			else
			{
				t -= 2.625f / div;
				return mult * t * t + 0.984375f;
			}
		}

		public static float InOutBounce(float t)
		{
			if (t < 0.5) 
				return InBounce(t * 2) / 2;
			return 1 - InBounce((1 - t) * 2) / 2;
		}

		public static float Normal01(float t)
		{
			const float amp = 1f;
			const float mean = 0.5f;
			const float stddv = 0.34f;

			var v1 = (t - mean) / (2f * stddv * stddv);
			var v2 = (float)(-v1 * v1 / 2f);
			var v3 = amp * (float)Math.Exp(v2);

			return v3;

		}

		public static float Normal(float t, float mean, float stddev)
		{
			const float amp = 1f;
			var v1 = (t - mean) / (2f * stddev * stddev);
			var v2 = (float)(-v1 * v1 / 2f);
			var v3 = amp * (float)Math.Exp(v2);

			return v3;
		}
	}
}
