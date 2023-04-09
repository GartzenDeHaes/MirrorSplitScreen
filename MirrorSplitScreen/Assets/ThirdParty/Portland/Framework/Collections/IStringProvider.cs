using System.Text;

using Portland.Text;

namespace Portland.Collections
{
	public interface IStringProvider<TTOKEN>
	{
		bool Contains(string lexum);
		bool Contains(StringBuilder buf);
		StringTableToken Get(int index);
		StringTableToken Get(string lexum);
		StringTableToken Get(StringBuilder buf);
		string GetString(in TTOKEN token);
		string GetString(StringBuilder buf);
		int GetHashCode(in TTOKEN token);
		bool TryGet(string lexum, out TTOKEN ret);
		bool TryGet(StringBuilder buf, out TTOKEN ret);
		bool AreEqual(in TTOKEN a, in TTOKEN b);
		StringView<TTOKEN> CreateStringView(in StringTableToken tok);
		StringView<TTOKEN> CreateStringView(string lexum);
		StringView<TTOKEN> CreateStringView(StringBuilder lexum);
	}
}
