using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Portland.Mathmatics;

namespace Portland.Collections
{
	[Serializable]
	public sealed class Vector<T> : IIndexed<T>, IEnumerable<T>, IList<T>
	{
		T[] m_data;
		int m_used;

		public bool IsReadOnly { get { return false; } }

		public int Count
		{
			get { return m_used; }
		}

		public Vector()
		{
			m_data = new T[10];
		}

		public Vector(int size)
		{
			m_data = new T[size];
		}

		public Vector(IEnumerable<T> values)
		{
			m_data = values.ToArray();
			m_used = m_data.Length;
		}

		public T Pop()
		{
			if (m_used > 0)
			{
				return m_data[--m_used];
			}
			else
			{
				return default(T);
			}
		}

		/// <summary>
		/// Bottom of stack, first element in list
		/// </summary>
		public T Head()
		{
			return m_data[0];
		}

		/// <summary>
		/// Top of stack, last element in list
		/// </summary>
		public T Tail()
		{
			return m_data[m_used - 1];
		}

		public int Capacity()
		{
			return m_data.Length;
		}

		public int AddElement(in T o)
		{
			Extend();
			m_data[m_used] = o;
			return m_used++;
		}

		public void Add(T o)
		{
			Extend();
			m_data[m_used++] = o;
		}

		public int AddAndGetIndex(in T o)
		{
			Extend();
			m_data[m_used++] = o;
			return m_used - 1;
		}

		public void Add(T[] ta)
		{
			for (int x = 0; x < ta.Length; x++)
			{
				Add(ta[x]);
			}
		}

		public void AddRangeWithPad(T[] a, int count)
		{
			for (int x = 0; x < count; x++)
			{
				if (x < a.Length)
				{
					Add(a[x]);
				}
				else
				{
					Add(default(T));
				}
			}
		}

		public void Extend()
		{
			if (m_used >= m_data.Length)
			{
				int space = m_data.Length;
				while (m_used >= space)
				{
					//space <<= 1;
					space += space / 3;
				}
				T[] array2 = new T[space];
				int i;
				for (i = 0; (i < m_used); i++)
				{
					array2[i] = m_data[i];
				}
				m_data = array2;
			}
		}

		public void SetSize(int s)
		{
			if (m_data.Length < s)
			{
				m_data = new T[s];
			}
			m_used = s;
		}

		public void Clear()
		{
			m_used = 0;
		}

		public T ElementAt(int at)
		{
			return m_data[at];
		}

		public ref T ElementAtRef(int at)
		{
			return ref m_data[at];
		}

		public T this[int idx]
		{
			get { return ElementAt(idx); }
			set { SetElementAt(idx, value); }
		}

		public void SetElementAt(int at, in T o)
		{
			m_data[at] = o;
		}

		public T FirstElement()
		{
			if (m_used > 0)
			{
				return m_data[0];
			}
			else
			{
				return default(T);
			}
		}

		public T LastElement()
		{
			if (m_used > 0)
			{
				return m_data[m_used - 1];
			}
			else
			{
				return default(T);
			}
		}

		public ref T LastElementRef()
		{
			if (m_used > 0)
			{
				return ref m_data[m_used - 1];
			}
			else
			{
				throw new Exception("Vector empty");
			}
		}

		public bool Remove(T o)
		{
			int i;
			for (i = 0; (i < m_used); i++)
			{
				if (m_data[i].Equals(o))
				{
					RemoveAt(i);
					return true;
				}
			}

			return false;
		}

		public void RemoveAt(int at)
		{
			int i;
			for (i = at; (i < (m_used - 1)); i++)
			{
				m_data[i] = m_data[i + 1];
			}
			m_used--;
		}

		public void RemoveWhen(Func<T, bool> test)
		{
			int i = 0;
			while (i < m_used)
			{
				if (test(m_data[i]))
				{
					RemoveAt(i);
					continue;
				}
				i++;
			}
		}

		public bool Contains(T item)
		{
			return IndexOf(item) >= 0;
		}

		public int IndexOf(T o)
		{
			int i;
			for (i = 0; (i < m_used); i++)
			{
				if (m_data[i].Equals(o))
				{
					return i;
				}
			}
			return -1;
		}

		public void Insert(int at, T o)
		{
			int i;
			Extend();
			for (i = m_used; (i > at); i--)
			{
				m_data[i] = m_data[i - 1];
			}
			m_data[at] = o;
			m_used++;
		}

		public void Copy(Vector<T> v)
		{
			Clear();
			for (int i = 0; i < v.m_used; i++)
			{
				AddElement(v.ElementAt(i));
			}
		}

		public void InsertBefore(in T oNew, in T oExisting)
		{
			int at = IndexOf(oExisting);
			if (at < 0)
			{
				throw new Exception("insertBefore can't find before");
				//addElement(oNew);
				//return;
			}
			Insert(at, oNew);
		}

		public bool IsEmpty()
		{
			return m_used == 0;
		}

		public T[] ToArray()
		{
			T[] ta = new T[Count];
			for (int x = 0; x < ta.Length; x++)
			{
				ta[x] = ElementAt(x);
			}
			return ta;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			for (int x = 0; x < m_data.Length; x++)
			{
				array[x + arrayIndex] = ElementAt(x);
			}
		}

		public T[] Data()
		{
			return m_data;
		}

		public T RandomElement()
		{
			return ElementAt(MathHelper.RandomRange(0, Count-1));
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new VectorEnumerator(m_data, m_used);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new VectorEnumerator(m_data, m_used);
		}

		class VectorEnumerator : IEnumerator<T>
		{
			T[] m_data;
			readonly int m_len;
			int m_pos;
			public T Current { get { return m_pos >= m_len ? default(T) : m_data[m_pos]; } }
			object IEnumerator.Current { get { return m_pos >= m_len ? default(T) : m_data[m_pos]; } }

			public VectorEnumerator(T[] data, int len)
			{
				m_data = data;
				m_len = len;
				m_pos = -1;
			}

			public bool MoveNext()
			{
				return ++m_pos < m_len;
			}

			public void Dispose()
			{
				m_data = null;
			}

			public void Reset()
			{
				m_pos = -1;
			}
		}
	}
}
