using Portland.Mathmatics;

using System.Threading;

namespace RogueSharp.Random
{
	/// <summary>
	/// The Singleton class is a public static class that holds the DefaultRandom generator.
	/// </summary>
	public static class RandomDefault
	{
		/// <summary>
		/// The DefaultRandom generator is DotNetRandom from System.Random
		/// </summary>
		private static ThreadLocal<IRandom> _random = new ThreadLocal<IRandom>(() => new DotNetRandom());

		public static IRandom Rand
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
	}
}
