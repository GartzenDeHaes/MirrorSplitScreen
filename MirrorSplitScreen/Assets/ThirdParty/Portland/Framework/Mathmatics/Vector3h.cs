using System;
using System.Runtime.CompilerServices;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Portland.Mathmatics
{
	/// <summary>
	/// A 6-byte Vector3 with half values;S
	/// </summary>
	public struct Vector3h
	{
		public static Vector3h Zero = new Vector3h();
		public static Vector3h Back = new Vector3h(0f, 0f, -1f);
		public static Vector3h Down = new Vector3h(0f, -1f, 0f);
		public static Vector3h Forward = new Vector3h(0f, 0f, 1);
		public static Vector3h Left = new Vector3h(-1, 0f, 0f);
		public static Vector3h NegativeInfinity = new Vector3h(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
		public static Vector3h One = new Vector3h(1f, 1f, 1f);
		public static Vector3h PositiveInfinity = new Vector3h(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		public static Vector3h Right = new Vector3h(1, 0f, 0f);
		public static Vector3h Up = new Vector3h(0f, 1, 0f);

		public Half X;
		public Half Y;
		public Half Z;

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3h(float ax, float ay, float az)
		{
			X = new Half(ax);
			Y = new Half(ay);
			Z = new Half(az);
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3h(Half ax, Half ay, Half az)
		{
			X = ax;
			Y = ay;
			Z = az;
		}

#if UNITY_2019_4_OR_NEWER
		public Vector3h(Vector3 v)
		{
			X = new Half(v.x);
			Y = new Half(v.y);
			Z = new Half(v.z);
		}
#else
		public Vector3h(Vector3 v)
		{
			X = new Half(v.x);
			Y = new Half(v.y);
			Z = new Half(v.z);
		}
#endif

		/// <summary></summary>
		public float sqrMagnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return X * X + Z * Z + Y * Y; }
		}

		/// <summary></summary>
		public float magnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return (float)Math.Sqrt(sqrMagnitude); }
		}

		public Vector3h normalized
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				var nv = sqrMagnitude;
				return new Vector3h(X / nv, Y / nv, Z / nv);
			}
		}

		public void Set(float ax, float ay, float az)
		{
			X = new Half(ax);
			Y = new Half(ay);
			Z = new Half(az);
		}

		public override bool Equals(object obj)
		{
			if (obj is Vector3h v1)
			{
				return this == v1;
			}

#if UNITY_2019_OR_NEWER
			if (obj is Vector3 v2)
			{
				return this == v2;
			}
#endif
			return false;
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
		}

		public override string ToString()
		{
			return "(" + X + "," + Y + "," + Z + ")";
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3h operator +(in Vector3h va, in Vector3h vb)
		{
			return new Vector3h(va.X + vb.X, va.Y + vb.Y, va.Z + vb.Z);
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3h operator -(in Vector3h va, in Vector3h vb)
		{
			return new Vector3h(va.X - vb.X, va.Y - vb.Y, va.Z - vb.Z);
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3h operator *(in Vector3h va, float f)
		{
			return new Vector3h(va.X * f, va.Y * f, va.Z * f);
		}

		/// <summary></summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3h operator /(in Vector3h va, float f)
		{
			return new Vector3h(va.X / f, va.Y / f, va.Z / f);
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in Vector3h va, in Vector3h vb)
		{
			return va.X == vb.X && va.Y == vb.Y && va.Z == vb.Z;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in Vector3h va, in Vector3h vb)
		{
			return va.X != vb.X || va.Y != vb.Y || va.Z != vb.Z;
		}

#if UNITY_2019_1_OR_NEWER
		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in Vector3h va, in Vector3 vb)
		{
			return va.X == vb.x && va.Z == vb.z && (float)va.Y == vb.y;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in Vector3h va, in Vector3 vb)
		{
			return va.X != vb.x || va.Z != vb.z || (float)va.Y != vb.y;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector3(in Vector3h v)
		{
			return new Vector3(v.X, (float)v.Y, v.Z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector3h(in Vector3 v)
		{
			return new Vector3h(v.x, v.y, v.z);
		}
#else
		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in Vector3h va, in Vector3 vb)
		{
			return va.X == vb.x && va.Z == vb.z && (float)va.Y == vb.y;
		}

		/// <summary>==</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in Vector3h va, in Vector3 vb)
		{
			return va.X != vb.x || va.Z != vb.z || (float)va.Y != vb.y;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector3(in Vector3h v)
		{
			return new Vector3(v.X, (float)v.Y, v.Z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector3h(in Vector3 v)
		{
			return new Vector3h(v.x, v.y, v.z);
		}
#endif
	}
}
