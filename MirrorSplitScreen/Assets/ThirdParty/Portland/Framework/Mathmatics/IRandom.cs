
namespace Portland.Mathmatics
{
	public interface IRandom
	{
		int Next();
		int Next(int maxExclusive);
		bool NextBool();
		byte NextByte();
		byte[] NextBytes(int count);
		double NextDouble();
		double NextDouble(double maxInclusive);
		double NextDouble(double minInclusive, double maxInclusive);
		float NextFloat();
		float NextFloat(float maxInclusive);
		uint NextUInt();
		uint NextUInt(uint maxExclusive);
		float Range(float minInclusive, float maxInclusive);
		int Range(int minInclusive, int maxExclusive);
		uint Range(uint minInclusive, uint maxExclusive);
	}
}