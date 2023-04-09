using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Portland.Mathmatics;

public static class EnumerableExtensions
{
	/// <summary>
	/// Returns a random item in the IEnumerable
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="input"></param>
	/// <returns></returns>
	public static T RandomItem<T>(this IEnumerable<T> input)
	{
		return input.ElementAt(MathHelper.RandomNext(input.Count()));
	}

	/// <summary>
	/// Each value in the list is a discrete probability in the range [0,1) and the sum should be 1.0f.
	/// </summary>
	/// <param name="input">A list of probabilities in the range [0,1)</param>
	/// <returns>Index of the selected probability</returns>
	public static int RandomDistribution(this IEnumerable<float> input)
	{
		var rnd = MathHelper.RandomNextFloat();
		float sum = 0f;
		int count = input.Count();

		for (int i = 0; i < count; i++)
		{
			var p = input.ElementAt(i);
			Debug.Assert(p >= 0 && p <= 1.0);
			sum += p;
			if (sum >= rnd)
			{
				return i;
			}
		}
#if DEBUG
		throw new Exception($"EnumerableExtensions.RandomDistribution error sum={sum} count={count}");
#else
		return MathHelper.RandomNext(count);
#endif
	}

	/// <summary>
	/// Each value in the list is a discrete probability in the range [0,1) and the sum should be 1.0f.
	/// </summary>
	/// <param name="input">A list of probabilities in the range [0,1)</param>
	/// <returns>Index of the selected probability</returns>
	public static int RandomDistribution(this IEnumerable<double> input)
	{
		var rnd = MathHelper.RandomNextFloat();
		double sum = 0;
		int count = input.Count();

		for (int i = 0; i < count; i++)
		{
			var p = input.ElementAt(i);
			Debug.Assert(p >= 0 && p < 1.0);
			sum += p;
			if (sum >= rnd)
			{
				return i;
			}
		}
#if DEBUG
		throw new Exception($"EnumerableExtensions.RandomDistribution error sum={sum} count={count}");
#else
		return MathHelper.RandomNext(count);
#endif
	}

	/// <summary>
	/// Each value in the list is a discrete probability in the range [0,1).
	/// A random number is generated and compared to each element.
	/// A random element is returned if not item is selected after #trials loops.
	/// </summary>
	/// <param name="input">A list of probabilities in the range [0,1)</param>
	/// <param name="trials">Number of times to loop through the list.</param>
	/// <returns>Index of the selected probability</returns>
	public static int RandomTest(this IEnumerable<float> input, int trials = 1)
	{
		int count = input.Count();
		float rnd;

		for (int t = 0; t < trials; t++)
		{
			for (int i = 0; i < count; i++)
			{
				var p = input.ElementAt(i);
				Debug.Assert(p >= 0 && p < 1.0);

				rnd = MathHelper.RandomNextFloat();
				if (rnd < p)
				{
					return i;
				}
			}
		}
		return MathHelper.RandomNext(count);
	}
}
