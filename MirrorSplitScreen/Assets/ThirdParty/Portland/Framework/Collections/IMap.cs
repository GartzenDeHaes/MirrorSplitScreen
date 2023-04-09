using System.Collections.Generic;

namespace Portland.Collections
{
    public interface IMap<TKEY, TVALUE>
    {
        TVALUE this[TKEY key] { get; set; }

        int Count { get; }
        IEnumerable<TKEY> Keys { get; }

        bool Contains(in TKEY key);
        TVALUE Get(in TKEY key);
        void Set(in TKEY key, in TVALUE value);
        bool TryGet(in TKEY key, out TVALUE value);
        bool TrySet(in TKEY key, in TVALUE value);
    }
}