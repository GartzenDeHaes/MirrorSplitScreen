using System;
using System.Collections.Generic;
using System.Text;

namespace Portland.Collections
{
	public sealed class ArrayFlat2D<T>
	{
		T[] _cells;
		int _width;
		int _height;

		public T this[int x, int y]
		{
			get { return _cells[y * _width + x]; }
			set { _cells[y * _width + x] = value; }
		}

		public T this[int i]
		{
			get { return _cells[i]; }
			set { _cells[i] = value; }
		}

		public int Count { get { return _width * _height; } }

		public int GetLength(int dim)
		{
			return dim == 0 ? _width : _height;
		}

		public ArrayFlat2D(int width, int height)
		{
			_width = width;
			_height = height;
			_cells = new T[width * height];
		}
	}
}
