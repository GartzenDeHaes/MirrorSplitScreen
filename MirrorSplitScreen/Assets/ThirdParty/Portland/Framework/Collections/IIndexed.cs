using System;
using System.Collections.Generic;
using System.Text;

namespace Portland.Collections
{
	public interface IIndexed<T>
	{
		int Count { get; }
		T this[int index] { get; set; }

		//void ForEach(Action<T> dothis);
		//T FirstThat(Func<T, bool> matchesTest);
	}

	public interface IIndexed2D<T>
	{
		T this[int x, int y] { get; set; }

		int GetLength(int ord);
		//void ForEach(Action<T> dothis);
		//T FirstThat(Func<T, bool> matchesTest);
	}

	public class IndexedList<T> : List<T>, IIndexed<T>
	{
	}

	public class IndexedArray<T> : IIndexed<T>
	{
		protected T[] _array;

		public T this[int index] { get { return _array[index]; } set { _array[index] = value; } }

		public int Count { get; }

		public IndexedArray(int count)
		{
			_array = new T[count];
			Count = count;
		}
	}

	public class IndexedArrayWrap<T> : IIndexed<T>
	{
		protected T[] _array;

		public T this[int index] 
		{ 
			get { return _array[WrapIndex(index)]; } 
			set { _array[WrapIndex(index)] = value; }
		}

		public int Count { get; }

		public IndexedArrayWrap(int count)
		{
			_array = new T[count];
			Count = count;
		}

		int WrapIndex(int idx)
		{
			int m = _array.Length;
			int r = idx % m;
			return r < 0 ? r + m : r;
		}
	}

	public class IndexedArrayWrapped<T> : IIndexed<T>
	{
		protected IIndexed<T> _array;

		public T this[int index]
		{
			get { return _array[WrapIndex(index)]; }
			set { _array[WrapIndex(index)] = value; }
		}

		public int Count { get { return _array.Count; } }

		public IndexedArrayWrapped(IIndexed<T> arr)
		{
			_array = arr;
		}

		int WrapIndex(int idx)
		{
			int m = _array.Count;
			int r = idx % m;
			return r < 0 ? r + m : r;
		}
	}

	public class IndexedArray2D<T> : IIndexed2D<T>
	{
		protected T[,] _array;

		public T this[int x, int y] { get { return _array[x, y]; } set { _array[x, y] = value; } }

		public IndexedArray2D(int countx, int county)
		{
			_array = new T[countx, county];
		}

		public int GetLength(int ord)
		{
			return _array.GetLength(ord);
		}
	}

	public abstract class IIndexedArray2DWrap<T> : IIndexed2D<T>
	{
		public abstract T this[int x, int y]
		{
			get;
			set;
		}

		public abstract int GetLength(int dim);

		protected int WrapIndex(int idx, int dim)
		{
			int m = GetLength(dim);
			int r = idx % m;
			return r < 0 ? r + m : r;
		}
	}

	public class IndexedArray2DWrap<T> : IIndexedArray2DWrap<T>
	{
		protected T[,] _array;

		public override T this[int x, int y]
		{
			get { return _array[WrapIndex(x, 0), WrapIndex(y, 1)]; }
			set { _array[WrapIndex(x, 0), WrapIndex(y, 1)] = value; }
		}

		public IndexedArray2DWrap(int lenx, int leny)
		{
			_array = new T[lenx, leny];
		}

		public override int GetLength(int dim)
		{
			return _array.GetLength(dim);
		}
	}

	public class IndexedArray2DWrapped<T> : IIndexedArray2DWrap<T>
	{
		protected IIndexed2D<T> _array;

		public override T this[int x, int y]
		{
			get { return _array[WrapIndex(x, 0), WrapIndex(y, 1)]; }
			set { _array[WrapIndex(x, 0), WrapIndex(y, 1)] = value; }
		}

		public IndexedArray2DWrapped(IIndexed2D<T> arr)
		{
			_array = arr;
		}

		public override int GetLength(int dim)
		{
			return _array.GetLength(dim);
		}
	}
}
