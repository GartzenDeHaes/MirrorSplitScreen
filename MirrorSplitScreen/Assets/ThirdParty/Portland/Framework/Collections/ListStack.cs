using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Portland.Collections
{
	/// <summary>
	/// Stack interface for List.
	/// </summary>
	public sealed class ListStack<T> : List<T>
	{
		/// <summary>
		/// 1 if Bottom is 0 and Top is N.  -1 if Bottom is N and Top is 0.
		/// </summary>
		public const int GrowthDirection = 1;

		/// <summary>
		/// Last element of the stack.
		/// </summary>
		public const int Bottom = 0;

		/// <summary>
		/// Top element of the stack.
		/// </summary>
		public int Top
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return Count - 1; }
		}

		/// <summary>
		/// Pop the top element off
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Pop()
		{
			base.RemoveAt(Count - 1);
		}

		/// <summary>
		/// Peek the top element.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T Peek()
		{
			return base[Count - 1];
		}

		/// <summary>
		/// Push.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Push(T item)
		{
			base.Add(item);
		}
	}
}
