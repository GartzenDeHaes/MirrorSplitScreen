// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

#if !UNITY_5_3_OR_NEWER

using System;
using System.Diagnostics;
using System.Text;
using System.Runtime.Serialization;

namespace Portland.Mathmatics
{
	/// <summary>
	/// Describes a 3D-vector.
	/// </summary>
	[DataContract]
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	public struct Vector3 //: IEquatable<Vector3>
	{
		#region Static Fields

		public static readonly Vector3 zero = new Vector3(0f, 0f, 0f);
		public static readonly Vector3 one = new Vector3(1f, 1f, 1f);
		public static readonly Vector3 UnitX = new Vector3(1f, 0f, 0f);
		public static readonly Vector3 UnitY = new Vector3(0f, 1f, 0f);
		public static readonly Vector3 UnitZ = new Vector3(0f, 0f, 1f);
		public static readonly Vector3 up = new Vector3(0f, 1f, 0f);
		public static readonly Vector3 down = new Vector3(0f, -1f, 0f);
		public static readonly Vector3 right = new Vector3(1f, 0f, 0f);
		public static readonly Vector3 left = new Vector3(-1f, 0f, 0f);
		public static readonly Vector3 forward = new Vector3(0f, 0f, 1f);
		public static readonly Vector3 Backward = new Vector3(0f, 0f, -1f);
		public static readonly Vector3 East = new Vector3(1, 0, 0);
		public static readonly Vector3 West = new Vector3(-1, 0, 0);
		public static readonly Vector3 North = new Vector3(0, 0, -1);
		public static readonly Vector3 South = new Vector3(0, 0, 1);

		#endregion

		#region Public Fields

		/// <summary>
		/// The x coordinate of this <see cref="Vector3 />.
		/// </summary>
		[DataMember]
		public float x;

		/// <summary>
		/// The y coordinate of this <see cref="Vector3 />.
		/// </summary>
		[DataMember]
		public float y;

		/// <summary>
		/// The z coordinate of this <see cref="Vector3 />.
		/// </summary>
		[DataMember]
		public float z;

		#endregion

		#region Internal Properties

		internal string DebugDisplayString
		{
			get
			{
				return string.Concat(
					 this.x.ToString(), "  ",
					 this.y.ToString(), "  ",
					 this.z.ToString()
				);
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructs a 3d vector with X, Y and Z from three values.
		/// </summary>
		/// <param name="x">The x coordinate in 3d-space.</param>
		/// <param name="y">The y coordinate in 3d-space.</param>
		/// <param name="z">The z coordinate in 3d-space.</param>
		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		/// <summary>
		/// Constructs a 3d vector with X, Y and Z set to the same value.
		/// </summary>
		/// <param name="value">The x, y and z coordinates in 3d-space.</param>
		public Vector3(float value)
		{
			this.x = value;
			this.y = value;
			this.z = value;
		}

		/// <summary>
		/// Constructs a 3d vector with X, Y from <see cref="Vector2"/> and Z from a scalar.
		/// </summary>
		/// <param name="value">The x and y coordinates in 3d-space.</param>
		/// <param name="z">The z coordinate in 3d-space.</param>
		public Vector3(in Vector2 value, float z)
		{
			this.x = (float)value.x;
			this.y = (float)value.y;
			this.z = z;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Performs vector addition on <paramref name="value1"/> and <paramref name="value2"/>.
		/// </summary>
		/// <param name="value1">The first vector to add.</param>
		/// <param name="value2">The second vector to add.</param>
		/// <returns>The result of the vector addition.</returns>
		public static Vector3 Add(in Vector3 value1, in Vector3 value2)
		{
			Vector3 ret = value1;
			ret.x += value2.x;
			ret.y += value2.y;
			ret.z += value2.z;
			return ret;
		}

		/// <summary>
		/// Performs vector addition on <paramref name="value1"/> and
		/// <paramref name="value2"/>, storing the result of the
		/// addition in <paramref name="result"/>.
		/// </summary>
		/// <param name="value1">The first vector to add.</param>
		/// <param name="value2">The second vector to add.</param>
		/// <param name="result">The result of the vector addition.</param>
		public static void Add(in Vector3 value1, in Vector3 value2, out Vector3 result)
		{
			result.x = value1.x + value2.x;
			result.y = value1.y + value2.y;
			result.z = value1.z + value2.z;
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains the cartesian coordinates of a vector specified in barycentric coordinates and relative to 3d-triangle.
		/// </summary>
		/// <param name="value1">The first vector of 3d-triangle.</param>
		/// <param name="value2">The second vector of 3d-triangle.</param>
		/// <param name="value3">The third vector of 3d-triangle.</param>
		/// <param name="amount1">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of 3d-triangle.</param>
		/// <param name="amount2">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of 3d-triangle.</param>
		/// <returns>The cartesian translation of barycentric coordinates.</returns>
		public static Vector3 Barycentric(in Vector3 value1, in Vector3 value2, in Vector3 value3, float amount1, float amount2)
		{
			return new Vector3(
				 MathHelper.Barycentric(value1.x, value2.x, value3.x, amount1, amount2),
				 MathHelper.Barycentric(value1.y, value2.y, value3.y, amount1, amount2),
				 MathHelper.Barycentric(value1.z, value2.z, value3.z, amount1, amount2));
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains the cartesian coordinates of a vector specified in barycentric coordinates and relative to 3d-triangle.
		/// </summary>
		/// <param name="value1">The first vector of 3d-triangle.</param>
		/// <param name="value2">The second vector of 3d-triangle.</param>
		/// <param name="value3">The third vector of 3d-triangle.</param>
		/// <param name="amount1">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of 3d-triangle.</param>
		/// <param name="amount2">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of 3d-triangle.</param>
		/// <param name="result">The cartesian translation of barycentric coordinates as an output parameter.</param>
		public static void Barycentric(in Vector3 value1, in Vector3 value2, in Vector3 value3, float amount1, float amount2, out Vector3 result)
		{
			result.x = MathHelper.Barycentric(value1.x, value2.x, value3.x, amount1, amount2);
			result.y = MathHelper.Barycentric(value1.y, value2.y, value3.y, amount1, amount2);
			result.z = MathHelper.Barycentric(value1.z, value2.z, value3.z, amount1, amount2);
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains CatmullRom interpolation of the specified vectors.
		/// </summary>
		/// <param name="value1">The first vector in interpolation.</param>
		/// <param name="value2">The second vector in interpolation.</param>
		/// <param name="value3">The third vector in interpolation.</param>
		/// <param name="value4">The fourth vector in interpolation.</param>
		/// <param name="amount">Weighting factor.</param>
		/// <returns>The result of CatmullRom interpolation.</returns>
		public static Vector3 CatmullRom(in Vector3 value1, in Vector3 value2, in Vector3 value3, in Vector3 value4, float amount)
		{
			return new Vector3(
				 MathHelper.CatmullRom(value1.x, value2.x, value3.x, value4.x, amount),
				 MathHelper.CatmullRom(value1.y, value2.y, value3.y, value4.y, amount),
				 MathHelper.CatmullRom(value1.z, value2.z, value3.z, value4.z, amount));
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains CatmullRom interpolation of the specified vectors.
		/// </summary>
		/// <param name="value1">The first vector in interpolation.</param>
		/// <param name="value2">The second vector in interpolation.</param>
		/// <param name="value3">The third vector in interpolation.</param>
		/// <param name="value4">The fourth vector in interpolation.</param>
		/// <param name="amount">Weighting factor.</param>
		/// <param name="result">The result of CatmullRom interpolation as an output parameter.</param>
		public static void CatmullRom(in Vector3 value1, in Vector3 value2, in Vector3 value3, in Vector3 value4, float amount, out Vector3 result)
		{
			result.x = MathHelper.CatmullRom(value1.x, value2.x, value3.x, value4.x, amount);
			result.y = MathHelper.CatmullRom(value1.y, value2.y, value3.y, value4.y, amount);
			result.z = MathHelper.CatmullRom(value1.z, value2.z, value3.z, value4.z, amount);
		}

		/// <summary>
		/// Round the members of this <see cref="Vector3 /> towards positive infinity.
		/// </summary>
		public void Ceiling()
		{
			x = MathF.Ceiling(x);
			y = MathF.Ceiling(y);
			z = MathF.Ceiling(z);
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains members from another vector rounded towards positive infinity.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3 />.</param>
		/// <returns>The rounded <see cref="Vector3 />.</returns>
		public static Vector3 Ceiling(in Vector3 value)
		{
			return new Vector3(MathF.Ceiling(value.x), MathF.Ceiling(value.y), MathF.Ceiling(value.z));
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains members from another vector rounded towards positive infinity.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3 />.</param>
		/// <param name="result">The rounded <see cref="Vector3 />.</param>
		public static void Ceiling(in Vector3 value, out Vector3 result)
		{
			result.x = MathF.Ceiling(value.x);
			result.y = MathF.Ceiling(value.y);
			result.z = MathF.Ceiling(value.z);
		}

		/// <summary>
		/// Clamps the specified value within a range.
		/// </summary>
		/// <param name="value1">The value to clamp.</param>
		/// <param name="min">The min value.</param>
		/// <param name="max">The max value.</param>
		/// <returns>The clamped value.</returns>
		public static Vector3 Clamp(in Vector3 value1, in Vector3 min, in Vector3 max)
		{
			return new Vector3(
				 MathHelper.Clamp(value1.x, min.x, max.x),
				 MathHelper.Clamp(value1.y, min.y, max.y),
				 MathHelper.Clamp(value1.z, min.z, max.z));
		}

		public void Clamp(float value)
		{
			if (MathF.Abs(x) > value)
				x = (float)(value * (x < 0 ? -1 : 1));
			if (MathF.Abs(y) > value)
				y = (float)(value * (y < 0 ? -1 : 1));
			if (MathF.Abs(z) > value)
				z = (float)(value * (z < 0 ? -1 : 1));
		}

		/// <summary>
		/// Clamps the specified value within a range.
		/// </summary>
		/// <param name="value1">The value to clamp.</param>
		/// <param name="min">The min value.</param>
		/// <param name="max">The max value.</param>
		/// <param name="result">The clamped value as an output parameter.</param>
		public static void Clamp(in Vector3 value1, in Vector3 min, in Vector3 max, out Vector3 result)
		{
			result.x = MathHelper.Clamp(value1.x, min.x, max.x);
			result.y = MathHelper.Clamp(value1.y, min.y, max.y);
			result.z = MathHelper.Clamp(value1.z, min.z, max.z);
		}

		/// <summary>
		/// Computes the cross product of two vectors.
		/// </summary>
		/// <param name="vector1">The first vector.</param>
		/// <param name="vector2">The second vector.</param>
		/// <returns>The cross product of two vectors.</returns>
		public static Vector3 Cross(in Vector3 vector1, in Vector3 vector2)
		{
			Cross(vector1, vector2, out var vr);
			return vr;
		}

		/// <summary>
		/// Computes the cross product of two vectors.
		/// </summary>
		/// <param name="vector1">The first vector.</param>
		/// <param name="vector2">The second vector.</param>
		/// <param name="result">The cross product of two vectors as an output parameter.</param>
		public static void Cross(in Vector3 vector1, in Vector3 vector2, out Vector3 result)
		{
			var x = vector1.y * (double)vector2.z - vector2.y * (double)vector1.z;
			var y = -(vector1.x * (double)vector2.z - vector2.x * (double)vector1.z);
			var z = vector1.x * (double)vector2.y - vector2.x * (double)vector1.y;
			result.x = (float)x;
			result.y = (float)y;
			result.z = (float)z;
		}

		public float DistanceTo(in Vector3 other)
		{
			return (float)Math.Sqrt((other.x - x) * (other.x - x) +
								  (other.y - y) * (other.y - y) +
								  (other.z - z) * (other.z - z));
		}

		/// <summary>
		/// Returns the distance between two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The distance between two vectors.</returns>
		public static float Distance(in Vector3 value1, in Vector3 value2)
		{
			float result;
			DistanceSquared(value1, value2, out result);
			return MathF.Sqrt(result);
		}

		/// <summary>
		/// Returns the distance between two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="result">The distance between two vectors as an output parameter.</param>
		public static void Distance(in Vector3 value1, in Vector3 value2, out float result)
		{
			DistanceSquared(value1, value2, out result);
			result = MathF.Sqrt(result);
		}

		/// <summary>
		/// Returns the squared distance between two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The squared distance between two vectors.</returns>
		public static float DistanceSquared(in Vector3 value1, in Vector3 value2)
		{
			return (value1.x - value2.x) * (value1.x - value2.x) +
					  (value1.y - value2.y) * (value1.y - value2.y) +
					  (value1.z - value2.z) * (value1.z - value2.z);
		}

		/// <summary>
		/// Returns the squared distance between two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="result">The squared distance between two vectors as an output parameter.</param>
		public static void DistanceSquared(in Vector3 value1, in Vector3 value2, out float result)
		{
			result = (value1.x - value2.x) * (value1.x - value2.x) +
						(value1.y - value2.y) * (value1.y - value2.y) +
						(value1.z - value2.z) * (value1.z - value2.z);
		}

		/// <summary>
		/// Divides the components of a <see cref="Vector3 /> by the components of another <see cref="Vector3 />.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector3 />.</param>
		/// <param name="value2">Divisor <see cref="Vector3 />.</param>
		/// <returns>The result of dividing the vectors.</returns>
		public static Vector3 Divide(in Vector3 value1, in Vector3 value2)
		{
			Vector3 ret = value1;
			ret.x /= value2.x;
			ret.y /= value2.y;
			ret.z /= value2.z;
			return ret;
		}

		/// <summary>
		/// Divides the components of a <see cref="Vector3 /> by a scalar.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector3 />.</param>
		/// <param name="divider">Divisor scalar.</param>
		/// <returns>The result of dividing a vector by a scalar.</returns>
		public static Vector3 Divide(Vector3 value1, float divider)
		{
			float factor = 1 / divider;
			value1.x *= factor;
			value1.y *= factor;
			value1.z *= factor;
			return value1;
		}

		/// <summary>
		/// Divides the components of a <see cref="Vector3 /> by a scalar.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector3 />.</param>
		/// <param name="divider">Divisor scalar.</param>
		/// <param name="result">The result of dividing a vector by a scalar as an output parameter.</param>
		public static void Divide(in Vector3 value1, float divider, out Vector3 result)
		{
			float factor = 1 / divider;
			result.x = value1.x * factor;
			result.y = value1.y * factor;
			result.z = value1.z * factor;
		}

		/// <summary>
		/// Divides the components of a <see cref="Vector3 /> by the components of another <see cref="Vector3 />.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector3 />.</param>
		/// <param name="value2">Divisor <see cref="Vector3 />.</param>
		/// <param name="result">The result of dividing the vectors as an output parameter.</param>
		public static void Divide(in Vector3 value1, in Vector3 value2, out Vector3 result)
		{
			result.x = value1.x / value2.x;
			result.y = value1.y / value2.y;
			result.z = value1.z / value2.z;
		}

		/// <summary>
		/// Returns a dot product of two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The dot product of two vectors.</returns>
		public static float Dot(in Vector3 value1, in Vector3 value2)
		{
			return (float)(value1.x * (double)value2.x + value1.y * (double)value2.y + value1.z * (double)value2.z);
		}

		/// <summary>
		/// Returns a dot product of two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="result">The dot product of two vectors as an output parameter.</param>
		public static void Dot(in Vector3 value1, in Vector3 value2, out float result)
		{
			result = (float)(value1.x * (double)value2.x + value1.y * (double)value2.y + value1.z * (double)value2.z);
		}

		/// <summary>
		/// Compares whether current instance is equal to specified <see cref="Object"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is Vector3))
				return false;

			var other = (Vector3)obj;
			return x == other.x &&
					  y == other.y &&
					  z == other.z;
		}

		/// <summary>
		/// Compares whether current instance is equal to specified <see cref="Vector3 />.
		/// </summary>
		/// <param name="other">The <see cref="Vector3 /> to compare.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		public bool Equals(Vector3 other)
		{
			return x == other.x &&
					  y == other.y &&
					  z == other.z;
		}

		/// <summary>
		/// Round the members of this <see cref="Vector3 /> towards negative infinity.
		/// </summary>
		public void Floor()
		{
			x = MathF.Floor(x);
			y = MathF.Floor(y);
			z = MathF.Floor(z);
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains members from another vector rounded towards negative infinity.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3 />.</param>
		/// <returns>The rounded <see cref="Vector3 />.</returns>
		public static Vector3 Floor(Vector3 value)
		{
			value.x = MathF.Floor(value.x);
			value.y = MathF.Floor(value.y);
			value.z = MathF.Floor(value.z);
			return value;
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains members from another vector rounded towards negative infinity.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3 />.</param>
		/// <param name="result">The rounded <see cref="Vector3 />.</param>
		public static void Floor(in Vector3 value, out Vector3 result)
		{
			result.x = MathF.Floor(value.x);
			result.y = MathF.Floor(value.y);
			result.z = MathF.Floor(value.z);
		}

		/// <summary>
		/// Gets the hash code of this <see cref="Vector3 />.
		/// </summary>
		/// <returns>Hash code of this <see cref="Vector3 />.</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = x.GetHashCode();
				hashCode = (hashCode * 397) ^ y.GetHashCode();
				hashCode = (hashCode * 397) ^ z.GetHashCode();
				return hashCode;
			}
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains hermite spline interpolation.
		/// </summary>
		/// <param name="value1">The first position vector.</param>
		/// <param name="tangent1">The first tangent vector.</param>
		/// <param name="value2">The second position vector.</param>
		/// <param name="tangent2">The second tangent vector.</param>
		/// <param name="amount">Weighting factor.</param>
		/// <returns>The hermite spline interpolation vector.</returns>
		public static Vector3 Hermite(Vector3 value1, Vector3 tangent1, Vector3 value2, Vector3 tangent2, float amount)
		{
			return new Vector3(MathHelper.Hermite(value1.x, tangent1.x, value2.x, tangent2.x, amount),
									 MathHelper.Hermite(value1.y, tangent1.y, value2.y, tangent2.y, amount),
									 MathHelper.Hermite(value1.z, tangent1.z, value2.z, tangent2.z, amount));
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains hermite spline interpolation.
		/// </summary>
		/// <param name="value1">The first position vector.</param>
		/// <param name="tangent1">The first tangent vector.</param>
		/// <param name="value2">The second position vector.</param>
		/// <param name="tangent2">The second tangent vector.</param>
		/// <param name="amount">Weighting factor.</param>
		/// <param name="result">The hermite spline interpolation vector as an output parameter.</param>
		public static void Hermite(in Vector3 value1, in Vector3 tangent1, in Vector3 value2, in Vector3 tangent2, float amount, out Vector3 result)
		{
			result.x = MathHelper.Hermite(value1.x, tangent1.x, value2.x, tangent2.x, amount);
			result.y = MathHelper.Hermite(value1.y, tangent1.y, value2.y, tangent2.y, amount);
			result.z = MathHelper.Hermite(value1.z, tangent1.z, value2.z, tangent2.z, amount);
		}

		/// <summary>
		/// Returns the length of this <see cref="Vector3 />.
		/// </summary>
		/// <returns>The length of this <see cref="Vector3 />.</returns>
		public float Magnitude
		{
			get { return MathF.Sqrt((x * x) + (y * y) + (z * z)); }
		}

		public float magnitude
		{
			get { return Magnitude; }
		}

		/// <summary>
		/// Returns the squared length of this <see cref="Vector3 />.
		/// </summary>
		/// <returns>The squared length of this <see cref="Vector3 />.</returns>
		public float SqrMagnitude
		{
			get { return (x * x) + (y * y) + (z * z); }
		}

		public float sqrMagnitude
		{
			get { return SqrMagnitude; }
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains linear interpolation of the specified vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
		/// <returns>The result of linear interpolation of the specified vectors.</returns>
		public static Vector3 Lerp(in Vector3 value1, in Vector3 value2, float amount)
		{
			return new Vector3(
				 MathHelper.Lerp(value1.x, value2.x, amount),
				 MathHelper.Lerp(value1.y, value2.y, amount),
				 MathHelper.Lerp(value1.z, value2.z, amount));
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains linear interpolation of the specified vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
		/// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
		public static void Lerp(in Vector3 value1, in Vector3 value2, float amount, out Vector3 result)
		{
			result.x = MathHelper.Lerp(value1.x, value2.x, amount);
			result.y = MathHelper.Lerp(value1.y, value2.y, amount);
			result.z = MathHelper.Lerp(value1.z, value2.z, amount);
		}

		///// <summary>
		///// Creates a new <see cref="Vector3"/> that contains linear interpolation of the specified vectors.
		///// Uses <see cref="MathHelper.LerpPrecise"/> on MathHelper for the interpolation.
		///// Less efficient but more precise compared to <see cref="Vector3.Lerp(Vector3, Vector3, float)"/>.
		///// See remarks section of <see cref="MathHelper.LerpPrecise"/> on MathHelper for more info.
		///// </summary>
		///// <param name="value1">The first vector.</param>
		///// <param name="value2">The second vector.</param>
		///// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
		///// <returns>The result of linear interpolation of the specified vectors.</returns>
		//public static Vector3 LerpPrecise(Vector3 value1, Vector3 value2, float amount)
		//{
		//	return new Vector3(
		//		 MathHelper.LerpPrecise(value1.X, value2.X, amount),
		//		 MathHelper.LerpPrecise(value1.Y, value2.Y, amount),
		//		 MathHelper.LerpPrecise(value1.Z, value2.Z, amount));
		//}

		///// <summary>
		///// Creates a new <see cref="Vector3"/> that contains linear interpolation of the specified vectors.
		///// Uses <see cref="MathHelper.LerpPrecise"/> on MathHelper for the interpolation.
		///// Less efficient but more precise compared to <see cref="Vector3.Lerp(ref Vector3, ref Vector3, float, out Vector3)"/>.
		///// See remarks section of <see cref="MathHelper.LerpPrecise"/> on MathHelper for more info.
		///// </summary>
		///// <param name="value1">The first vector.</param>
		///// <param name="value2">The second vector.</param>
		///// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
		///// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
		//public static void LerpPrecise(ref Vector3 value1, ref Vector3 value2, float amount, out Vector3 result)
		//{
		//	result.X = MathHelper.LerpPrecise(value1.X, value2.X, amount);
		//	result.Y = MathHelper.LerpPrecise(value1.Y, value2.Y, amount);
		//	result.Z = MathHelper.LerpPrecise(value1.Z, value2.Z, amount);
		//}

		/// <summary>
		/// Creates a new <see cref="Vector3"/> that contains a maximal values from the two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The <see cref="Vector3"/> with maximal values from the two vectors.</returns>
		public static Vector3 Max(in Vector3 value1, in Vector3 value2)
		{
			return new Vector3(
				 MathHelper.Max(value1.x, value2.x),
				 MathHelper.Max(value1.y, value2.y),
				 MathHelper.Max(value1.z, value2.z));
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains a maximal values from the two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="result">The <see cref="Vector3 /> with maximal values from the two vectors as an output parameter.</param>
		public static void Max(in Vector3 value1, in Vector3 value2, out Vector3 result)
		{
			result.x = MathHelper.Max(value1.x, value2.x);
			result.y = MathHelper.Max(value1.y, value2.y);
			result.z = MathHelper.Max(value1.z, value2.z);
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains a minimal values from the two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The <see cref="Vector3 /> with minimal values from the two vectors.</returns>
		public static Vector3 Min(in Vector3 value1, in Vector3 value2)
		{
			return new Vector3(
				 MathHelper.Min(value1.x, value2.x),
				 MathHelper.Min(value1.y, value2.y),
				 MathHelper.Min(value1.z, value2.z));
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains a minimal values from the two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="result">The <see cref="Vector3 /> with minimal values from the two vectors as an output parameter.</param>
		public static void Min(in Vector3 value1, in Vector3 value2, out Vector3 result)
		{
			result.x = MathHelper.Min(value1.x, value2.x);
			result.y = MathHelper.Min(value1.y, value2.y);
			result.z = MathHelper.Min(value1.z, value2.z);
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains a multiplication of two vectors.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector3 />.</param>
		/// <param name="value2">Source <see cref="Vector3 />.</param>
		/// <returns>The result of the vector multiplication.</returns>
		public static Vector3 Multiply(in Vector3 value1, in Vector3 value2)
		{
			Vector3 ret = value1;
			ret.x *= value2.x;
			ret.y *= value2.y;
			ret.z *= value2.z;
			return ret;
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains a multiplication of <see cref="Vector3 /> and a scalar.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector3 />.</param>
		/// <param name="scaleFactor">Scalar value.</param>
		/// <returns>The result of the vector multiplication with a scalar.</returns>
		public static Vector3 Multiply(in Vector3 value1, float scaleFactor)
		{
			Vector3 ret = value1;
			ret.x *= scaleFactor;
			ret.y *= scaleFactor;
			ret.z *= scaleFactor;
			return ret;
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains a multiplication of <see cref="Vector3 /> and a scalar.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector3 />.</param>
		/// <param name="scaleFactor">Scalar value.</param>
		/// <param name="result">The result of the multiplication with a scalar as an output parameter.</param>
		public static void Multiply(in Vector3 value1, float scaleFactor, out Vector3 result)
		{
			result.x = value1.x * scaleFactor;
			result.y = value1.y * scaleFactor;
			result.z = value1.z * scaleFactor;
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains a multiplication of two vectors.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector3 />.</param>
		/// <param name="value2">Source <see cref="Vector3 />.</param>
		/// <param name="result">The result of the vector multiplication as an output parameter.</param>
		public static void Multiply(in Vector3 value1, in Vector3 value2, out Vector3 result)
		{
			result.x = value1.x * value2.x;
			result.y = value1.y * value2.y;
			result.z = value1.z * value2.z;
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains the specified vector inversion.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3 />.</param>
		/// <returns>The result of the vector inversion.</returns>
		public static Vector3 Negate(in Vector3 value)
		{
			return new Vector3(-value.x, -value.y, -value.z);
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains the specified vector inversion.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3 />.</param>
		/// <param name="result">The result of the vector inversion as an output parameter.</param>
		public static void Negate(in Vector3 value, out Vector3 result)
		{
			result.x = -value.x;
			result.y = -value.y;
			result.z = -value.z;
		}

		public Vector3 Normalized
		{
			get { return Normalize(this); }
		}

		/// <summary>
		/// Turns this <see cref="Vector3 /> to a unit vector with the same direction.
		/// </summary>
		public void Normalize()
		{
			float factor = MathF.Sqrt((x * x) + (y * y) + (z * z));
			if (factor == 0f)
			{
				return;
			}
			factor = 1f / factor;
			x *= factor;
			y *= factor;
			z *= factor;
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains a normalized values from another vector.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3 />.</param>
		/// <returns>Unit vector.</returns>
		public static Vector3 Normalize(in Vector3 value)
		{
			float factor = MathF.Sqrt((value.x * value.x) + (value.y * value.y) + (value.z * value.z));
			if (factor == 0f)
			{
				return Vector3.zero;
			}
			factor = 1f / factor;
			return new Vector3(value.x * factor, value.y * factor, value.z * factor);
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains a normalized values from another vector.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3 />.</param>
		/// <param name="result">Unit vector as an output parameter.</param>
		public static void Normalize(in Vector3 value, out Vector3 result)
		{
			float factor = MathF.Sqrt((value.x * value.x) + (value.y * value.y) + (value.z * value.z));
			if (factor == 0f)
			{
				result = Vector3.zero;
				return;
			}
			factor = 1f / factor;
			result.x = value.x * factor;
			result.y = value.y * factor;
			result.z = value.z * factor;
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains reflect vector of the given vector and normal.
		/// </summary>
		/// <param name="vector">Source <see cref="Vector3 />.</param>
		/// <param name="normal">Reflection normal.</param>
		/// <returns>Reflected vector.</returns>
		public static Vector3 Reflect(Vector3 vector, Vector3 normal)
		{
			// I is the original array
			// N is the normal of the incident plane
			// R = I - (2 * N * ( DotProduct[ I,N] ))
			Vector3 reflectedVector;
			// inline the dotProduct here instead of calling method
			float dotProduct = ((vector.x * normal.x) + (vector.y * normal.y)) + (vector.z * normal.z);
			reflectedVector.x = vector.x - (2.0f * normal.x) * dotProduct;
			reflectedVector.y = vector.y - (2.0f * normal.y) * dotProduct;
			reflectedVector.z = vector.z - (2.0f * normal.z) * dotProduct;

			return reflectedVector;
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains reflect vector of the given vector and normal.
		/// </summary>
		/// <param name="vector">Source <see cref="Vector3 />.</param>
		/// <param name="normal">Reflection normal.</param>
		/// <param name="result">Reflected vector as an output parameter.</param>
		public static void Reflect(ref Vector3 vector, ref Vector3 normal, out Vector3 result)
		{
			// I is the original array
			// N is the normal of the incident plane
			// R = I - (2 * N * ( DotProduct[ I,N] ))

			// inline the dotProduct here instead of calling method
			float dotProduct = ((vector.x * normal.x) + (vector.y * normal.y)) + (vector.z * normal.z);
			result.x = vector.x - (2.0f * normal.x) * dotProduct;
			result.y = vector.y - (2.0f * normal.y) * dotProduct;
			result.z = vector.z - (2.0f * normal.z) * dotProduct;
		}

		/// <summary>
		/// Round the members of this <see cref="Vector3 /> towards the nearest integer value.
		/// </summary>
		public void Round()
		{
			x = MathF.Round(x);
			y = MathF.Round(y);
			z = MathF.Round(z);
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains members from another vector rounded to the nearest integer value.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3 />.</param>
		/// <returns>The rounded <see cref="Vector3 />.</returns>
		public static Vector3 Round(Vector3 value)
		{
			value.x = MathF.Round(value.x);
			value.y = MathF.Round(value.y);
			value.z = MathF.Round(value.z);
			return value;
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains members from another vector rounded to the nearest integer value.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3 />.</param>
		/// <param name="result">The rounded <see cref="Vector3 />.</param>
		public static void Round(ref Vector3 value, out Vector3 result)
		{
			result.x = MathF.Round(value.x);
			result.y = MathF.Round(value.y);
			result.z = MathF.Round(value.z);
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains cubic interpolation of the specified vectors.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector3 />.</param>
		/// <param name="value2">Source <see cref="Vector3 />.</param>
		/// <param name="amount">Weighting value.</param>
		/// <returns>Cubic interpolation of the specified vectors.</returns>
		public static Vector3 SmoothStep(Vector3 value1, Vector3 value2, float amount)
		{
			return new Vector3(
				 MathHelper.SmoothStep(value1.x, value2.x, amount),
				 MathHelper.SmoothStep(value1.y, value2.y, amount),
				 MathHelper.SmoothStep(value1.z, value2.z, amount));
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains cubic interpolation of the specified vectors.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector3 />.</param>
		/// <param name="value2">Source <see cref="Vector3 />.</param>
		/// <param name="amount">Weighting value.</param>
		/// <param name="result">Cubic interpolation of the specified vectors as an output parameter.</param>
		public static void SmoothStep(ref Vector3 value1, ref Vector3 value2, float amount, out Vector3 result)
		{
			result.x = MathHelper.SmoothStep(value1.x, value2.x, amount);
			result.y = MathHelper.SmoothStep(value1.y, value2.y, amount);
			result.z = MathHelper.SmoothStep(value1.z, value2.z, amount);
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains subtraction of on <see cref="Vector3 /> from a another.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector3 />.</param>
		/// <param name="value2">Source <see cref="Vector3 />.</param>
		/// <returns>The result of the vector subtraction.</returns>
		public static Vector3 Subtract(Vector3 value1, Vector3 value2)
		{
			value1.x -= value2.x;
			value1.y -= value2.y;
			value1.z -= value2.z;
			return value1;
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains subtraction of on <see cref="Vector3 /> from a another.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector3 />.</param>
		/// <param name="value2">Source <see cref="Vector3 />.</param>
		/// <param name="result">The result of the vector subtraction as an output parameter.</param>
		public static void Subtract(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.x = value1.x - value2.x;
			result.y = value1.y - value2.y;
			result.z = value1.z - value2.z;
		}

		/// <summary>
		/// Returns a <see cref="String"/> representation of this <see cref="Vector3 /> in the format:
		/// {X:[<see cref="x"/>] Y:[<see cref="y"/>] Z:[<see cref="z"/>]}
		/// </summary>
		/// <returns>A <see cref="String"/> representation of this <see cref="Vector3 />.</returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder(32);
			sb.Append("(");
			sb.Append(this.x);
			sb.Append(",");
			sb.Append(this.y);
			sb.Append(",");
			sb.Append(this.z);
			sb.Append(")");
			return sb.ToString();
		}

		#region Transform

		public Vector3 Transform(in Matrix4x4 matrix)
		{
			return Transform(this, matrix);
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains a transformation of 3d-vector by the specified <see cref="Matrix4x4"/>.
		/// </summary>
		/// <param name="position">Source <see cref="Vector3 />.</param>
		/// <param name="matrix">The transformation <see cref="Matrix4x4"/>.</param>
		/// <returns>Transformed <see cref="Vector3 />.</returns>
		public static Vector3 Transform(in Vector3 position, in Matrix4x4 matrix)
		{
			Transform(position, matrix, out var ret);
			return ret;
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains a transformation of 3d-vector by the specified <see cref="Matrix4x4"/>.
		/// </summary>
		/// <param name="position">Source <see cref="Vector3 />.</param>
		/// <param name="matrix">The transformation <see cref="Matrix4x4"/>.</param>
		/// <param name="result">Transformed <see cref="Vector3 /> as an output parameter.</param>
		public static void Transform(in Vector3 position, in Matrix4x4 matrix, out Vector3 result)
		{
			var x = (position.x * matrix.m11) + (position.y * matrix.m21) + (position.z * matrix.m31) + matrix.m41;
			var y = (position.x * matrix.m12) + (position.y * matrix.m22) + (position.z * matrix.m32) + matrix.m42;
			var z = (position.x * matrix.m13) + (position.y * matrix.m23) + (position.z * matrix.m33) + matrix.m43;
			result.x = x;
			result.y = y;
			result.z = z;
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains a transformation of 3d-vector by the specified <see cref="Quaternion"/>, representing the rotation.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3 />.</param>
		/// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
		/// <returns>Transformed <see cref="Vector3 />.</returns>
		public static Vector3 Transform(in Vector3 value, in Quaternion rotation)
		{
			Vector3 result;
			Transform(value, rotation, out result);
			return result;
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains a transformation of 3d-vector by the specified <see cref="Quaternion"/>, representing the rotation.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3 />.</param>
		/// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
		/// <param name="result">Transformed <see cref="Vector3 /> as an output parameter.</param>
		public static void Transform(in Vector3 value, in Quaternion rotation, out Vector3 result)
		{
			float x = 2 * (rotation.y * value.z - rotation.z * value.y);
			float y = 2 * (rotation.z * value.x - rotation.x * value.z);
			float z = 2 * (rotation.x * value.y - rotation.y * value.x);

			result.x = value.x + x * rotation.w + (rotation.y * z - rotation.z * y);
			result.y = value.y + y * rotation.w + (rotation.z * x - rotation.x * z);
			result.z = value.z + z * rotation.w + (rotation.x * y - rotation.y * x);
		}

		/// <summary>
		/// Apply transformation on vectors within array of <see cref="Vector3 /> by the specified <see cref="Matrix4x4"/> and places the results in an another array.
		/// </summary>
		/// <param name="sourceArray">Source array.</param>
		/// <param name="sourceIndex">The starting index of transformation in the source array.</param>
		/// <param name="matrix">The transformation <see cref="Matrix4x4"/>.</param>
		/// <param name="destinationArray">Destination array.</param>
		/// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector3 /> should be written.</param>
		/// <param name="length">The number of vectors to be transformed.</param>
		public static void Transform(Vector3[] sourceArray, int sourceIndex, in Matrix4x4 matrix, Vector3[] destinationArray, int destinationIndex, int length)
		{
			if (sourceArray == null)
				throw new ArgumentNullException("sourceArray");
			if (destinationArray == null)
				throw new ArgumentNullException("destinationArray");
			if (sourceArray.Length < sourceIndex + length)
				throw new ArgumentException("Source array length is lesser than sourceIndex + length");
			if (destinationArray.Length < destinationIndex + length)
				throw new ArgumentException("Destination array length is lesser than destinationIndex + length");

			// TODO: Are there options on some platforms to implement a vectorized version of this?

			for (var i = 0; i < length; i++)
			{
				var position = sourceArray[sourceIndex + i];
				destinationArray[destinationIndex + i] =
					 new Vector3(
						  (position.x * matrix.m11) + (position.y * matrix.m21) + (position.z * matrix.m31) + matrix.m41,
						  (position.x * matrix.m12) + (position.y * matrix.m22) + (position.z * matrix.m32) + matrix.m42,
						  (position.x * matrix.m13) + (position.y * matrix.m23) + (position.z * matrix.m33) + matrix.m43);
			}
		}

		/// <summary>
		/// Apply transformation on vectors within array of <see cref="Vector3 /> by the specified <see cref="Quaternion"/> and places the results in an another array.
		/// </summary>
		/// <param name="sourceArray">Source array.</param>
		/// <param name="sourceIndex">The starting index of transformation in the source array.</param>
		/// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
		/// <param name="destinationArray">Destination array.</param>
		/// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector3 /> should be written.</param>
		/// <param name="length">The number of vectors to be transformed.</param>
		public static void Transform(Vector3[] sourceArray, int sourceIndex, in Quaternion rotation, Vector3[] destinationArray, int destinationIndex, int length)
		{
			if (sourceArray == null)
				throw new ArgumentNullException("sourceArray");
			if (destinationArray == null)
				throw new ArgumentNullException("destinationArray");
			if (sourceArray.Length < sourceIndex + length)
				throw new ArgumentException("Source array length is lesser than sourceIndex + length");
			if (destinationArray.Length < destinationIndex + length)
				throw new ArgumentException("Destination array length is lesser than destinationIndex + length");

			// TODO: Are there options on some platforms to implement a vectorized version of this?

			for (var i = 0; i < length; i++)
			{
				var position = sourceArray[sourceIndex + i];

				float x = 2 * (rotation.y * position.z - rotation.z * position.y);
				float y = 2 * (rotation.z * position.x - rotation.x * position.z);
				float z = 2 * (rotation.x * position.y - rotation.y * position.x);

				destinationArray[destinationIndex + i] =
					 new Vector3(
						  position.x + x * rotation.w + (rotation.y * z - rotation.z * y),
						  position.y + y * rotation.w + (rotation.z * x - rotation.x * z),
						  position.z + z * rotation.w + (rotation.x * y - rotation.y * x));
			}
		}

		/// <summary>
		/// Apply transformation on all vectors within array of <see cref="Vector3 /> by the specified <see cref="Matrix4x4"/> and places the results in an another array.
		/// </summary>
		/// <param name="sourceArray">Source array.</param>
		/// <param name="matrix">The transformation <see cref="Matrix4x4"/>.</param>
		/// <param name="destinationArray">Destination array.</param>
		public static void Transform(Vector3[] sourceArray, in Matrix4x4 matrix, Vector3[] destinationArray)
		{
			if (sourceArray == null)
				throw new ArgumentNullException("sourceArray");
			if (destinationArray == null)
				throw new ArgumentNullException("destinationArray");
			if (destinationArray.Length < sourceArray.Length)
				throw new ArgumentException("Destination array length is lesser than source array length");

			// TODO: Are there options on some platforms to implement a vectorized version of this?

			for (var i = 0; i < sourceArray.Length; i++)
			{
				var position = sourceArray[i];
				destinationArray[i] =
					 new Vector3(
						  (position.x * matrix.m11) + (position.y * matrix.m21) + (position.z * matrix.m31) + matrix.m41,
						  (position.x * matrix.m12) + (position.y * matrix.m22) + (position.z * matrix.m32) + matrix.m42,
						  (position.x * matrix.m13) + (position.y * matrix.m23) + (position.z * matrix.m33) + matrix.m43);
			}
		}

		/// <summary>
		/// Apply transformation on all vectors within array of <see cref="Vector3 /> by the specified <see cref="Quaternion"/> and places the results in an another array.
		/// </summary>
		/// <param name="sourceArray">Source array.</param>
		/// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
		/// <param name="destinationArray">Destination array.</param>
		public static void Transform(Vector3[] sourceArray, in Quaternion rotation, Vector3[] destinationArray)
		{
			if (sourceArray == null)
				throw new ArgumentNullException("sourceArray");
			if (destinationArray == null)
				throw new ArgumentNullException("destinationArray");
			if (destinationArray.Length < sourceArray.Length)
				throw new ArgumentException("Destination array length is lesser than source array length");

			// TODO: Are there options on some platforms to implement a vectorized version of this?

			for (var i = 0; i < sourceArray.Length; i++)
			{
				var position = sourceArray[i];

				float x = 2 * (rotation.y * position.z - rotation.z * position.y);
				float y = 2 * (rotation.z * position.x - rotation.x * position.z);
				float z = 2 * (rotation.x * position.y - rotation.y * position.x);

				destinationArray[i] =
					 new Vector3(
						  position.x + x * rotation.w + (rotation.y * z - rotation.z * y),
						  position.y + y * rotation.w + (rotation.z * x - rotation.x * z),
						  position.z + z * rotation.w + (rotation.x * y - rotation.y * x));
			}
		}

		#endregion

		#region TransformNormal

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains a transformation of the specified normal by the specified <see cref="Matrix4x4"/>.
		/// </summary>
		/// <param name="normal">Source <see cref="Vector3 /> which represents a normal vector.</param>
		/// <param name="matrix">The transformation <see cref="Matrix4x4"/>.</param>
		/// <returns>Transformed normal.</returns>
		public static Vector3 TransformNormal(Vector3 normal, in Matrix4x4 matrix)
		{
			TransformNormal(normal, matrix, out normal);
			return normal;
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains a transformation of the specified normal by the specified <see cref="Matrix4x4"/>.
		/// </summary>
		/// <param name="normal">Source <see cref="Vector3 /> which represents a normal vector.</param>
		/// <param name="matrix">The transformation <see cref="Matrix4x4"/>.</param>
		/// <param name="result">Transformed normal as an output parameter.</param>
		public static void TransformNormal(in Vector3 normal, in Matrix4x4 matrix, out Vector3 result)
		{
			var x = (normal.x * matrix.m11) + (normal.y * matrix.m21) + (normal.z * matrix.m31);
			var y = (normal.x * matrix.m12) + (normal.y * matrix.m22) + (normal.z * matrix.m32);
			var z = (normal.x * matrix.m13) + (normal.y * matrix.m23) + (normal.z * matrix.m33);
			result.x = x;
			result.y = y;
			result.z = z;
		}

		/// <summary>
		/// Apply transformation on normals within array of <see cref="Vector3 /> by the specified <see cref="Matrix4x4"/> and places the results in an another array.
		/// </summary>
		/// <param name="sourceArray">Source array.</param>
		/// <param name="sourceIndex">The starting index of transformation in the source array.</param>
		/// <param name="matrix">The transformation <see cref="Matrix4x4"/>.</param>
		/// <param name="destinationArray">Destination array.</param>
		/// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector3 /> should be written.</param>
		/// <param name="length">The number of normals to be transformed.</param>
		public static void TransformNormal(Vector3[] sourceArray,
		 int sourceIndex,
		 in Matrix4x4 matrix,
		 Vector3[] destinationArray,
		 int destinationIndex,
		 int length)
		{
			if (sourceArray == null)
				throw new ArgumentNullException("sourceArray");
			if (destinationArray == null)
				throw new ArgumentNullException("destinationArray");
			if (sourceArray.Length < sourceIndex + length)
				throw new ArgumentException("Source array length is lesser than sourceIndex + length");
			if (destinationArray.Length < destinationIndex + length)
				throw new ArgumentException("Destination array length is lesser than destinationIndex + length");

			for (int x = 0; x < length; x++)
			{
				var normal = sourceArray[sourceIndex + x];

				destinationArray[destinationIndex + x] =
					  new Vector3(
						  (normal.x * matrix.m11) + (normal.y * matrix.m21) + (normal.z * matrix.m31),
						  (normal.x * matrix.m12) + (normal.y * matrix.m22) + (normal.z * matrix.m32),
						  (normal.x * matrix.m13) + (normal.y * matrix.m23) + (normal.z * matrix.m33));
			}
		}

		/// <summary>
		/// Apply transformation on all normals within array of <see cref="Vector3 /> by the specified <see cref="Matrix4x4"/> and places the results in an another array.
		/// </summary>
		/// <param name="sourceArray">Source array.</param>
		/// <param name="matrix">The transformation <see cref="Matrix4x4"/>.</param>
		/// <param name="destinationArray">Destination array.</param>
		public static void TransformNormal(Vector3[] sourceArray, in Matrix4x4 matrix, Vector3[] destinationArray)
		{
			if (sourceArray == null)
				throw new ArgumentNullException("sourceArray");
			if (destinationArray == null)
				throw new ArgumentNullException("destinationArray");
			if (destinationArray.Length < sourceArray.Length)
				throw new ArgumentException("Destination array length is lesser than source array length");

			for (var i = 0; i < sourceArray.Length; i++)
			{
				var normal = sourceArray[i];

				destinationArray[i] =
					 new Vector3(
						  (normal.x * matrix.m11) + (normal.y * matrix.m21) + (normal.z * matrix.m31),
						  (normal.x * matrix.m12) + (normal.y * matrix.m22) + (normal.z * matrix.m32),
						  (normal.x * matrix.m13) + (normal.y * matrix.m23) + (normal.z * matrix.m33));
			}
		}

		#endregion

		/// <summary>
		/// Deconstruction method for <see cref="Vector3 />.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public void Deconstruct(out float x, out float y, out float z)
		{
			x = this.x;
			y = this.y;
			z = this.z;
		}

		/// <summary>
		/// Returns a <see cref="System.Numerics.Vector3"/>.
		/// </summary>
		//public System.Numerics.Vector3 ToNumerics()
		//{
		//	return new System.Numerics.Vector3(this.X, this.Y, this.Z);
		//}

		#endregion

		#region Operators

		///// <summary>
		///// Converts a <see cref="System.Numerics.Vector3"/> to a <see cref="Vector3 />.
		///// </summary>
		///// <param name="value">The converted value.</param>
		//public static implicit operator Vector3 System.Numerics.Vector3 value)
		//{
		//	return new Vector3 value.X, value.Y, value.Z);
		//}

		/// <summary>
		/// Compares whether two <see cref="Vector3 /> instances are equal.
		/// </summary>
		/// <param name="value1"><see cref="Vector3 /> instance on the left of the equal sign.</param>
		/// <param name="value2"><see cref="Vector3 /> instance on the right of the equal sign.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		public static bool operator ==(in Vector3 value1, in Vector3 value2)
		{
			return value1.x == value2.x
				 && value1.y == value2.y
				 && value1.z == value2.z;
		}

		/// <summary>
		/// Compares whether two <see cref="Vector3 /> instances are not equal.
		/// </summary>
		/// <param name="value1"><see cref="Vector3 /> instance on the left of the not equal sign.</param>
		/// <param name="value2"><see cref="Vector3 /> instance on the right of the not equal sign.</param>
		/// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>	
		public static bool operator !=(in Vector3 value1, in Vector3 value2)
		{
			return !(value1 == value2);
		}

		/// <summary>
		/// Adds two vectors.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector3 /> on the left of the add sign.</param>
		/// <param name="value2">Source <see cref="Vector3 /> on the right of the add sign.</param>
		/// <returns>Sum of the vectors.</returns>
		public static Vector3 operator +(Vector3 value1, in Vector3 value2)
		{
			value1.x += value2.x;
			value1.y += value2.y;
			value1.z += value2.z;
			return value1;
		}

		public static Vector3 operator +(in Vector3 a, in Size b)
		{
			return new Vector3(
				 (float)(a.x + b.Width),
				 (float)(a.y + b.Height),
				 (float)(a.z + b.Depth));
		}

		public static Vector3 operator +(in Vector3 a, float b)
		{
			return new Vector3(
				 a.x + b,
				 a.y + b,
				 a.z + b);
		}

		public static Vector3 operator -(in Vector3 a, in Size b)
		{
			return new Vector3(
				 a.x - b.Width,
				 a.y - b.Height,
				 a.z - b.Depth);
		}

		public static Vector3 operator -(in Vector3 a, float b)
		{
			return new Vector3(
				 a.x - b,
				 a.y - b,
				 a.z - b);
		}

		/// <summary>
		/// Inverts values in the specified <see cref="Vector3 />.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3 /> on the right of the sub sign.</param>
		/// <returns>Result of the inversion.</returns>
		public static Vector3 operator -(in Vector3 value)
		{
			var ret = new Vector3(-value.x, -value.y, -value.z);
			return ret;
		}

		/// <summary>
		/// Subtracts a <see cref="Vector3 /> from a <see cref="Vector3 />.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector3 /> on the left of the sub sign.</param>
		/// <param name="value2">Source <see cref="Vector3 /> on the right of the sub sign.</param>
		/// <returns>Result of the vector subtraction.</returns>
		public static Vector3 operator -(Vector3 value1, in Vector3 value2)
		{
			value1.x -= value2.x;
			value1.y -= value2.y;
			value1.z -= value2.z;
			return value1;
		}

		/// <summary>
		/// Multiplies the components of two vectors by each other.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector3 /> on the left of the mul sign.</param>
		/// <param name="value2">Source <see cref="Vector3 /> on the right of the mul sign.</param>
		/// <returns>Result of the vector multiplication.</returns>
		public static Vector3 operator *(Vector3 value1, in Vector3 value2)
		{
			value1.x *= value2.x;
			value1.y *= value2.y;
			value1.z *= value2.z;
			return value1;
		}

		/// <summary>
		/// Multiplies the components of vector by a scalar.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3 /> on the left of the mul sign.</param>
		/// <param name="scaleFactor">Scalar value on the right of the mul sign.</param>
		/// <returns>Result of the vector multiplication with a scalar.</returns>
		public static Vector3 operator *(Vector3 value, float scaleFactor)
		{
			value.x *= scaleFactor;
			value.y *= scaleFactor;
			value.z *= scaleFactor;
			return value;
		}

		/// <summary>
		/// Multiplies the components of vector by a scalar.
		/// </summary>
		/// <param name="scaleFactor">Scalar value on the left of the mul sign.</param>
		/// <param name="value">Source <see cref="Vector3 /> on the right of the mul sign.</param>
		/// <returns>Result of the vector multiplication with a scalar.</returns>
		public static Vector3 operator *(float scaleFactor, Vector3 value)
		{
			value.x *= scaleFactor;
			value.y *= scaleFactor;
			value.z *= scaleFactor;
			return value;
		}

		/// <summary>
		/// Divides the components of a <see cref="Vector3 /> by the components of another <see cref="Vector3 />.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector3 /> on the left of the div sign.</param>
		/// <param name="value2">Divisor <see cref="Vector3 /> on the right of the div sign.</param>
		/// <returns>The result of dividing the vectors.</returns>
		public static Vector3 operator /(Vector3 value1, in Vector3 value2)
		{
			value1.x /= value2.x;
			value1.y /= value2.y;
			value1.z /= value2.z;
			return value1;
		}

		/// <summary>
		/// Divides the components of a <see cref="Vector3 /> by a scalar.
		/// </summary>
		/// <param name="value1">Source <see cref="Vector3 /> on the left of the div sign.</param>
		/// <param name="divider">Divisor scalar on the right of the div sign.</param>
		/// <returns>The result of dividing a vector by a scalar.</returns>
		public static Vector3 operator /(Vector3 value1, float divider)
		{
			float factor = 1 / divider;
			value1.x *= factor;
			value1.y *= factor;
			value1.z *= factor;
			return value1;
		}

		#endregion

		#region Procedural Toolkit

		// Access the x, y, z components using [0], [1], [2] respectively.
		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return x;
					case 1: return y;
					case 2: return z;
					default:
						throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}

			set
			{
				switch (index)
				{
					case 0: x = value; break;
					case 1: y = value; break;
					case 2: z = value; break;
					default:
						throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
		}

		/// <summary>
		/// Convert two linearly distributed numbers between 0 and 1 to a point on a unit sphere (radius = 1)
		/// </summary>
		/// <param name="random1">Linearly distributed random number between 0 and 1</param>
		/// <param name="random2">Linearly distributed random number between 0 and 1</param>
		/// <returns>A cartesian point on the unit sphere</returns>
		public static Vector3 OnUnitSphere(float random1, float random2)
		{
			var theta = random1 * 2 * MathF.PI;
			var phi = MathF.Acos((2 * random2) - 1);

			// Convert from spherical coordinates to Cartesian
			var sinPhi = MathF.Sin(phi);

			var x = sinPhi * MathF.Cos(theta);
			var y = sinPhi * MathF.Sin(theta);
			var z = MathF.Cos(phi);

			return new Vector3(x, y, z);
		}

		public static Vector3 InsideUnitSphere()
		{
			float theta = 2f * MathF.PI * MathHelper.RandomNextFloat();
			float phi = MathF.Acos(1f - 2f * MathHelper.RandomNextFloat());
			Vector3 v;
			v.x = MathF.Sin(phi) * MathF.Cos(theta);
			v.y = MathF.Sin(phi) * MathF.Sin(theta);
			v.z = MathF.Cos(phi);

			return v;
		}

		public Vector2 ToVector2()
		{
			Vector2 v2;
			v2.x = x;
			v2.y = y;
			return v2;
		}

		public static Vector3 LerpUnclamped(in Vector3 value1, in Vector3 value2, float amount)
		{
			return new Vector3(
				 MathHelper.LerpUnclamped(value1.x, value2.x, amount),
				 MathHelper.LerpUnclamped(value1.y, value2.y, amount),
				 MathHelper.LerpUnclamped(value1.z, value2.z, amount));
		}

		#endregion
	}
}

#endif
