using System;

namespace Portland.Collections
{
	/// <summary>
	/// Implement to pool object.  <see cref="ObjectPool{T}"/>
	/// </summary>
	public interface IPoolable
	{
		/// <summary>
		/// Called before being returned by <see cref="ObjectPool{T}.Get()"/>.
		/// </summary>
		void Pool_Activate();

		/// <summary>
		/// Called after being passed to <see cref="ObjectPool{T}.Release(T)"/>.
		/// </summary>
		void Pool_Deactivate();
	}
}
