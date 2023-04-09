// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.
#if !UNITY_5_3_OR_NEWER

using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Portland.Mathmatics
{
	/// <summary>
	/// An efficient mathematical representation for three dimensional rotations.
	/// </summary>
	[DataContract]
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	public struct Quaternion //: IEquatable<Quaternion>
	{
		#region Private Fields

		private static readonly Quaternion _identity = new Quaternion(0, 0, 0, 1);

		#endregion

		#region Public Fields

		/// <summary>
		/// The x coordinate of this <see cref="Quaternion"/>.
		/// </summary>
		[DataMember]
		public float x;

		/// <summary>
		/// The y coordinate of this <see cref="Quaternion"/>.
		/// </summary>
		[DataMember]
		public float y;

		/// <summary>
		/// The z coordinate of this <see cref="Quaternion"/>.
		/// </summary>
		[DataMember]
		public float z;

		/// <summary>
		/// The rotation component of this <see cref="Quaternion"/>.
		/// </summary>
		[DataMember]
		public float w;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructs a quaternion with X, Y, Z and W from four values.
		/// </summary>
		/// <param name="x">The x coordinate in 3d-space.</param>
		/// <param name="y">The y coordinate in 3d-space.</param>
		/// <param name="z">The z coordinate in 3d-space.</param>
		/// <param name="w">The rotation component.</param>
		public Quaternion(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		/// <summary>
		/// Constructs a quaternion with X, Y, Z from <see cref="Vector3"/> and rotation component from a scalar.
		/// </summary>
		/// <param name="value">The x, y, z coordinates in 3d-space.</param>
		/// <param name="w">The rotation component.</param>
		public Quaternion(in Vector3 value, float w)
		{
			this.x = value.x;
			this.y = value.y;
			this.z = value.z;
			this.w = w;
		}

		/// <summary>
		/// Constructs a quaternion from <see cref="Vector4"/>.
		/// </summary>
		/// <param name="value">The x, y, z coordinates in 3d-space and the rotation component.</param>
		public Quaternion(in Vector4 value)
		{
			this.x = (float)value.x;
			this.y = (float)value.y;
			this.z = (float)value.z;
			this.w = (float)value.w;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Returns a quaternion representing no rotation.
		/// </summary>
		public static Quaternion Identity
		{
			get { return _identity; }
		}

		#endregion

		#region Internal Properties

		internal string DebugDisplayString
		{
			get
			{
				if (this == Quaternion._identity)
				{
					return "Identity";
				}

				return string.Concat(
					 this.x.ToString(), " ",
					 this.y.ToString(), " ",
					 this.z.ToString(), " ",
					 this.w.ToString()
				);
			}
		}

		#endregion

		#region Public Methods

		#region Add

		/// <summary>
		/// Creates a new <see cref="Quaternion"/> that contains the sum of two quaternions.
		/// </summary>
		/// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
		/// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
		/// <returns>The result of the quaternion addition.</returns>
		public static Quaternion Add(in Quaternion quaternion1, in Quaternion quaternion2)
		{
			Quaternion quaternion;
			quaternion.x = quaternion1.x + quaternion2.x;
			quaternion.y = quaternion1.y + quaternion2.y;
			quaternion.z = quaternion1.z + quaternion2.z;
			quaternion.w = quaternion1.w + quaternion2.w;
			return quaternion;
		}

		/// <summary>
		/// Creates a new <see cref="Quaternion"/> that contains the sum of two quaternions.
		/// </summary>
		/// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
		/// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
		/// <param name="result">The result of the quaternion addition as an output parameter.</param>
		public static void Add(in Quaternion quaternion1, in Quaternion quaternion2, out Quaternion result)
		{
			result.x = quaternion1.x + quaternion2.x;
			result.y = quaternion1.y + quaternion2.y;
			result.z = quaternion1.z + quaternion2.z;
			result.w = quaternion1.w + quaternion2.w;
		}

		#endregion

		#region Concatenate

		/// <summary>
		/// Creates a new <see cref="Quaternion"/> that contains concatenation between two quaternion.
		/// </summary>
		/// <param name="value1">The first <see cref="Quaternion"/> to concatenate.</param>
		/// <param name="value2">The second <see cref="Quaternion"/> to concatenate.</param>
		/// <returns>The result of rotation of <paramref name="value1"/> followed by <paramref name="value2"/> rotation.</returns>
		public static Quaternion Concatenate(in Quaternion value1, in Quaternion value2)
		{
			Quaternion quaternion;

			float x1 = value1.x;
			float y1 = value1.y;
			float z1 = value1.z;
			float w1 = value1.w;

			float x2 = value2.x;
			float y2 = value2.y;
			float z2 = value2.z;
			float w2 = value2.w;

			quaternion.x = ((x2 * w1) + (x1 * w2)) + ((y2 * z1) - (z2 * y1));
			quaternion.y = ((y2 * w1) + (y1 * w2)) + ((z2 * x1) - (x2 * z1));
			quaternion.z = ((z2 * w1) + (z1 * w2)) + ((x2 * y1) - (y2 * x1));
			quaternion.w = (w2 * w1) - (((x2 * x1) + (y2 * y1)) + (z2 * z1));

			return quaternion;
		}

		/// <summary>
		/// Creates a new <see cref="Quaternion"/> that contains concatenation between two quaternion.
		/// </summary>
		/// <param name="value1">The first <see cref="Quaternion"/> to concatenate.</param>
		/// <param name="value2">The second <see cref="Quaternion"/> to concatenate.</param>
		/// <param name="result">The result of rotation of <paramref name="value1"/> followed by <paramref name="value2"/> rotation as an output parameter.</param>
		public static void Concatenate(in Quaternion value1, in Quaternion value2, out Quaternion result)
		{
			float x1 = value1.x;
			float y1 = value1.y;
			float z1 = value1.z;
			float w1 = value1.w;

			float x2 = value2.x;
			float y2 = value2.y;
			float z2 = value2.z;
			float w2 = value2.w;

			result.x = ((x2 * w1) + (x1 * w2)) + ((y2 * z1) - (z2 * y1));
			result.y = ((y2 * w1) + (y1 * w2)) + ((z2 * x1) - (x2 * z1));
			result.z = ((z2 * w1) + (z1 * w2)) + ((x2 * y1) - (y2 * x1));
			result.w = (w2 * w1) - (((x2 * x1) + (y2 * y1)) + (z2 * z1));
		}

		#endregion

		#region Conjugate

		/// <summary>
		/// Transforms this quaternion into its conjugated version.
		/// </summary>
		public void Conjugate()
		{
			x = -x;
			y = -y;
			z = -z;
		}

		/// <summary>
		/// Creates a new <see cref="Quaternion"/> that contains conjugated version of the specified quaternion.
		/// </summary>
		/// <param name="value">The quaternion which values will be used to create the conjugated version.</param>
		/// <returns>The conjugate version of the specified quaternion.</returns>
		public static Quaternion Conjugate(in Quaternion value)
		{
			return new Quaternion(-value.x, -value.y, -value.z, value.w);
		}

		/// <summary>
		/// Creates a new <see cref="Quaternion"/> that contains conjugated version of the specified quaternion.
		/// </summary>
		/// <param name="value">The quaternion which values will be used to create the conjugated version.</param>
		/// <param name="result">The conjugated version of the specified quaternion as an output parameter.</param>
		public static void Conjugate(in Quaternion value, out Quaternion result)
		{
			result.x = -value.x;
			result.y = -value.y;
			result.z = -value.z;
			result.w = value.w;
		}

		#endregion

		#region CreateFromAxisAngle

		/// <summary>
		/// Creates a new <see cref="Quaternion"/> from the specified axis and angle.
		/// </summary>
		/// <param name="axis">The axis of rotation.</param>
		/// <param name="angle">The angle in radians.</param>
		/// <returns>The new quaternion builded from axis and angle.</returns>
		public static Quaternion CreateFromAxisAngle(in Vector3 axis, float angle)
		{
			float half = angle * 0.5f;
			float sin = MathF.Sin(half);
			float cos = MathF.Cos(half);
			return new Quaternion(axis.x * sin, axis.y * sin, axis.z * sin, cos);
		}

		/// <summary>
		/// Creates a new <see cref="Quaternion"/> from the specified axis and angle.
		/// </summary>
		/// <param name="axis">The axis of rotation.</param>
		/// <param name="angle">The angle in radians.</param>
		/// <param name="result">The new quaternion builded from axis and angle as an output parameter.</param>
		public static void CreateFromAxisAngle(in Vector3 axis, float angle, out Quaternion result)
		{
			float half = angle * 0.5f;
			float sin = MathF.Sin(half);
			float cos = MathF.Cos(half);
			result.x = axis.x * sin;
			result.y = axis.y * sin;
			result.z = axis.z * sin;
			result.w = cos;
		}

		public static Quaternion AngleAxis(float angle, in Vector3 axis)
		{
			float half = angle * 0.5f;
			float sin = MathF.Sin(half);
			float cos = MathF.Cos(half);
			Quaternion result;
			result.x = axis.x * sin;
			result.y = axis.y * sin;
			result.z = axis.z * sin;
			result.w = cos;
			return result;
		}

		#endregion

		#region CreateFromRotationMatrix

		/// <summary>
		/// Creates a new <see cref="Quaternion"/> from the specified <see cref="Matrix4x4"/>.
		/// </summary>
		/// <param name="matrix">The rotation matrix.</param>
		/// <returns>A quaternion composed from the rotation part of the matrix.</returns>
		public static Quaternion CreateFromRotationMatrix(in Matrix4x4 matrix)
		{
			Quaternion quaternion;
			float sqrt;
			float half;
			float scale = matrix.m11 + matrix.m22 + matrix.m33;

			if (scale > 0.0f)
			{
				sqrt = MathF.Sqrt(scale + 1.0f);
				quaternion.w = sqrt * 0.5f;
				sqrt = 0.5f / sqrt;

				quaternion.x = (matrix.m23 - matrix.m32) * sqrt;
				quaternion.y = (matrix.m31 - matrix.m13) * sqrt;
				quaternion.z = (matrix.m12 - matrix.m21) * sqrt;

				return quaternion;
			}
			if ((matrix.m11 >= matrix.m22) && (matrix.m11 >= matrix.m33))
			{
				sqrt = MathF.Sqrt(1.0f + matrix.m11 - matrix.m22 - matrix.m33);
				half = 0.5f / sqrt;

				quaternion.x = 0.5f * sqrt;
				quaternion.y = (matrix.m12 + matrix.m21) * half;
				quaternion.z = (matrix.m13 + matrix.m31) * half;
				quaternion.w = (matrix.m23 - matrix.m32) * half;

				return quaternion;
			}
			if (matrix.m22 > matrix.m33)
			{
				sqrt = MathF.Sqrt(1.0f + matrix.m22 - matrix.m11 - matrix.m33);
				half = 0.5f / sqrt;

				quaternion.x = (matrix.m21 + matrix.m12) * half;
				quaternion.y = 0.5f * sqrt;
				quaternion.z = (matrix.m32 + matrix.m23) * half;
				quaternion.w = (matrix.m31 - matrix.m13) * half;

				return quaternion;
			}
			sqrt = MathF.Sqrt(1.0f + matrix.m33 - matrix.m11 - matrix.m22);
			half = 0.5f / sqrt;

			quaternion.x = (matrix.m31 + matrix.m13) * half;
			quaternion.y = (matrix.m32 + matrix.m23) * half;
			quaternion.z = 0.5f * sqrt;
			quaternion.w = (matrix.m12 - matrix.m21) * half;

			return quaternion;
		}

		/// <summary>
		/// Creates a new <see cref="Quaternion"/> from the specified <see cref="Matrix4x4"/>.
		/// </summary>
		/// <param name="matrix">The rotation matrix.</param>
		/// <param name="result">A quaternion composed from the rotation part of the matrix as an output parameter.</param>
		public static void CreateFromRotationMatrix(in Matrix4x4 matrix, out Quaternion result)
		{
			float sqrt;
			float half;
			float scale = matrix.m11 + matrix.m22 + matrix.m33;

			if (scale > 0.0f)
			{
				sqrt = MathF.Sqrt(scale + 1.0f);
				result.w = sqrt * 0.5f;
				sqrt = 0.5f / sqrt;

				result.x = (matrix.m23 - matrix.m32) * sqrt;
				result.y = (matrix.m31 - matrix.m13) * sqrt;
				result.z = (matrix.m12 - matrix.m21) * sqrt;
			}
			else
			if ((matrix.m11 >= matrix.m22) && (matrix.m11 >= matrix.m33))
			{
				sqrt = MathF.Sqrt(1.0f + matrix.m11 - matrix.m22 - matrix.m33);
				half = 0.5f / sqrt;

				result.x = 0.5f * sqrt;
				result.y = (matrix.m12 + matrix.m21) * half;
				result.z = (matrix.m13 + matrix.m31) * half;
				result.w = (matrix.m23 - matrix.m32) * half;
			}
			else if (matrix.m22 > matrix.m33)
			{
				sqrt = MathF.Sqrt(1.0f + matrix.m22 - matrix.m11 - matrix.m33);
				half = 0.5f / sqrt;

				result.x = (matrix.m21 + matrix.m12) * half;
				result.y = 0.5f * sqrt;
				result.z = (matrix.m32 + matrix.m23) * half;
				result.w = (matrix.m31 - matrix.m13) * half;
			}
			else
			{
				sqrt = MathF.Sqrt(1.0f + matrix.m33 - matrix.m11 - matrix.m22);
				half = 0.5f / sqrt;

				result.x = (matrix.m31 + matrix.m13) * half;
				result.y = (matrix.m32 + matrix.m23) * half;
				result.z = 0.5f * sqrt;
				result.w = (matrix.m12 - matrix.m21) * half;
			}
		}

		#endregion

		#region CreateFromYawPitchRoll

		/// <summary>
		/// Creates a new <see cref="Quaternion"/> from the specified yaw, pitch and roll angles.
		/// </summary>
		/// <param name="yaw">Yaw around the y axis in radians.</param>
		/// <param name="pitch">Pitch around the x axis in radians.</param>
		/// <param name="roll">Roll around the z axis in radians.</param>
		/// <returns>A new quaternion from the concatenated yaw, pitch, and roll angles.</returns>
		public static Quaternion CreateFromYawPitchRoll(float yaw, float pitch, float roll)
		{
			float halfRoll = roll * 0.5f;
			float halfPitch = pitch * 0.5f;
			float halfYaw = yaw * 0.5f;

			float sinRoll = MathF.Sin(halfRoll);
			float cosRoll = MathF.Cos(halfRoll);
			float sinPitch = MathF.Sin(halfPitch);
			float cosPitch = MathF.Cos(halfPitch);
			float sinYaw = MathF.Sin(halfYaw);
			float cosYaw = MathF.Cos(halfYaw);

			return new Quaternion((cosYaw * sinPitch * cosRoll) + (sinYaw * cosPitch * sinRoll),
										 (sinYaw * cosPitch * cosRoll) - (cosYaw * sinPitch * sinRoll),
										 (cosYaw * cosPitch * sinRoll) - (sinYaw * sinPitch * cosRoll),
										 (cosYaw * cosPitch * cosRoll) + (sinYaw * sinPitch * sinRoll));
		}

		/// <summary>
		/// Creates a new <see cref="Quaternion"/> from the specified yaw, pitch and roll angles.
		/// </summary>
		/// <param name="yaw">Yaw around the y axis in radians.</param>
		/// <param name="pitch">Pitch around the x axis in radians.</param>
		/// <param name="roll">Roll around the z axis in radians.</param>
		/// <param name="result">A new quaternion from the concatenated yaw, pitch, and roll angles as an output parameter.</param>
		public static void CreateFromYawPitchRoll(float yaw, float pitch, float roll, out Quaternion result)
		{
			float halfRoll = roll * 0.5f;
			float halfPitch = pitch * 0.5f;
			float halfYaw = yaw * 0.5f;

			float sinRoll = MathF.Sin(halfRoll);
			float cosRoll = MathF.Cos(halfRoll);
			float sinPitch = MathF.Sin(halfPitch);
			float cosPitch = MathF.Cos(halfPitch);
			float sinYaw = MathF.Sin(halfYaw);
			float cosYaw = MathF.Cos(halfYaw);

			result.x = (cosYaw * sinPitch * cosRoll) + (sinYaw * cosPitch * sinRoll);
			result.y = (sinYaw * cosPitch * cosRoll) - (cosYaw * sinPitch * sinRoll);
			result.z = (cosYaw * cosPitch * sinRoll) - (sinYaw * sinPitch * cosRoll);
			result.w = (cosYaw * cosPitch * cosRoll) + (sinYaw * sinPitch * sinRoll);
		}

		#endregion

		#region Divide

		/// <summary>
		/// Divides a <see cref="Quaternion"/> by the other <see cref="Quaternion"/>.
		/// </summary>
		/// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
		/// <param name="quaternion2">Divisor <see cref="Quaternion"/>.</param>
		/// <returns>The result of dividing the quaternions.</returns>
		public static Quaternion Divide(in Quaternion quaternion1, in Quaternion quaternion2)
		{
			Quaternion quaternion;
			float x = quaternion1.x;
			float y = quaternion1.y;
			float z = quaternion1.z;
			float w = quaternion1.w;
			float num14 = (((quaternion2.x * quaternion2.x) + (quaternion2.y * quaternion2.y)) + (quaternion2.z * quaternion2.z)) + (quaternion2.w * quaternion2.w);
			float num5 = 1f / num14;
			float num4 = -quaternion2.x * num5;
			float num3 = -quaternion2.y * num5;
			float num2 = -quaternion2.z * num5;
			float num = quaternion2.w * num5;
			float num13 = (y * num2) - (z * num3);
			float num12 = (z * num4) - (x * num2);
			float num11 = (x * num3) - (y * num4);
			float num10 = ((x * num4) + (y * num3)) + (z * num2);
			quaternion.x = ((x * num) + (num4 * w)) + num13;
			quaternion.y = ((y * num) + (num3 * w)) + num12;
			quaternion.z = ((z * num) + (num2 * w)) + num11;
			quaternion.w = (w * num) - num10;
			return quaternion;
		}

		/// <summary>
		/// Divides a <see cref="Quaternion"/> by the other <see cref="Quaternion"/>.
		/// </summary>
		/// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
		/// <param name="quaternion2">Divisor <see cref="Quaternion"/>.</param>
		/// <param name="result">The result of dividing the quaternions as an output parameter.</param>
		public static void Divide(in Quaternion quaternion1, in Quaternion quaternion2, out Quaternion result)
		{
			float x = quaternion1.x;
			float y = quaternion1.y;
			float z = quaternion1.z;
			float w = quaternion1.w;
			float num14 = (((quaternion2.x * quaternion2.x) + (quaternion2.y * quaternion2.y)) + (quaternion2.z * quaternion2.z)) + (quaternion2.w * quaternion2.w);
			float num5 = 1f / num14;
			float num4 = -quaternion2.x * num5;
			float num3 = -quaternion2.y * num5;
			float num2 = -quaternion2.z * num5;
			float num = quaternion2.w * num5;
			float num13 = (y * num2) - (z * num3);
			float num12 = (z * num4) - (x * num2);
			float num11 = (x * num3) - (y * num4);
			float num10 = ((x * num4) + (y * num3)) + (z * num2);
			result.x = ((x * num) + (num4 * w)) + num13;
			result.y = ((y * num) + (num3 * w)) + num12;
			result.z = ((z * num) + (num2 * w)) + num11;
			result.w = (w * num) - num10;
		}

		#endregion

		#region Dot

		/// <summary>
		/// Returns a dot product of two quaternions.
		/// </summary>
		/// <param name="quaternion1">The first quaternion.</param>
		/// <param name="quaternion2">The second quaternion.</param>
		/// <returns>The dot product of two quaternions.</returns>
		public static float Dot(in Quaternion quaternion1, in Quaternion quaternion2)
		{
			return ((((quaternion1.x * quaternion2.x) + (quaternion1.y * quaternion2.y)) + (quaternion1.z * quaternion2.z)) + (quaternion1.w * quaternion2.w));
		}

		/// <summary>
		/// Returns a dot product of two quaternions.
		/// </summary>
		/// <param name="quaternion1">The first quaternion.</param>
		/// <param name="quaternion2">The second quaternion.</param>
		/// <param name="result">The dot product of two quaternions as an output parameter.</param>
		public static void Dot(in Quaternion quaternion1, in Quaternion quaternion2, out float result)
		{
			result = (((quaternion1.x * quaternion2.x) + (quaternion1.y * quaternion2.y)) + (quaternion1.z * quaternion2.z)) + (quaternion1.w * quaternion2.w);
		}

		#endregion

		#region Equals

		/// <summary>
		/// Compares whether current instance is equal to specified <see cref="Object"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Quaternion)
				return Equals((Quaternion)obj);
			return false;
		}

		/// <summary>
		/// Compares whether current instance is equal to specified <see cref="Quaternion"/>.
		/// </summary>
		/// <param name="other">The <see cref="Quaternion"/> to compare.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		public bool Equals(Quaternion other)
		{
			return x == other.x &&
						 y == other.y &&
						 z == other.z &&
						 w == other.w;
		}

		#endregion

		/// <summary>
		/// Gets the hash code of this <see cref="Quaternion"/>.
		/// </summary>
		/// <returns>Hash code of this <see cref="Quaternion"/>.</returns>
		public override int GetHashCode()
		{
			return x.GetHashCode() + y.GetHashCode() + z.GetHashCode() + w.GetHashCode();
		}

		#region Inverse

		/// <summary>
		/// Returns the inverse quaternion which represents the opposite rotation.
		/// </summary>
		/// <param name="quaternion">Source <see cref="Quaternion"/>.</param>
		/// <returns>The inverse quaternion.</returns>
		public static Quaternion Inverse(in Quaternion quaternion)
		{
			Quaternion quaternion2;
			float num2 = (((quaternion.x * quaternion.x) + (quaternion.y * quaternion.y)) + (quaternion.z * quaternion.z)) + (quaternion.w * quaternion.w);
			float num = 1f / num2;
			quaternion2.x = -quaternion.x * num;
			quaternion2.y = -quaternion.y * num;
			quaternion2.z = -quaternion.z * num;
			quaternion2.w = quaternion.w * num;
			return quaternion2;
		}

		/// <summary>
		/// Returns the inverse quaternion which represents the opposite rotation.
		/// </summary>
		/// <param name="quaternion">Source <see cref="Quaternion"/>.</param>
		/// <param name="result">The inverse quaternion as an output parameter.</param>
		public static void Inverse(in Quaternion quaternion, out Quaternion result)
		{
			float num2 = (((quaternion.x * quaternion.x) + (quaternion.y * quaternion.y)) + (quaternion.z * quaternion.z)) + (quaternion.w * quaternion.w);
			float num = 1f / num2;
			result.x = -quaternion.x * num;
			result.y = -quaternion.y * num;
			result.z = -quaternion.z * num;
			result.w = quaternion.w * num;
		}

		#endregion

		/// <summary>
		/// Returns the magnitude of the quaternion components.
		/// </summary>
		/// <returns>The magnitude of the quaternion components.</returns>
		public float Length()
		{
			return MathF.Sqrt((x * x) + (y * y) + (z * z) + (w * w));
		}

		/// <summary>
		/// Returns the squared magnitude of the quaternion components.
		/// </summary>
		/// <returns>The squared magnitude of the quaternion components.</returns>
		public float LengthSquared()
		{
			return (x * x) + (y * y) + (z * z) + (w * w);
		}

		#region Lerp

		/// <summary>
		/// Performs a linear blend between two quaternions.
		/// </summary>
		/// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
		/// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
		/// <param name="amount">The blend amount where 0 returns <paramref name="quaternion1"/> and 1 <paramref name="quaternion2"/>.</param>
		/// <returns>The result of linear blending between two quaternions.</returns>
		public static Quaternion Lerp(in Quaternion quaternion1, in Quaternion quaternion2, float amount)
		{
			float num = amount;
			float num2 = 1f - num;
			Quaternion quaternion = new Quaternion();
			float num5 = (((quaternion1.x * quaternion2.x) + (quaternion1.y * quaternion2.y)) + (quaternion1.z * quaternion2.z)) + (quaternion1.w * quaternion2.w);
			if (num5 >= 0f)
			{
				quaternion.x = (num2 * quaternion1.x) + (num * quaternion2.x);
				quaternion.y = (num2 * quaternion1.y) + (num * quaternion2.y);
				quaternion.z = (num2 * quaternion1.z) + (num * quaternion2.z);
				quaternion.w = (num2 * quaternion1.w) + (num * quaternion2.w);
			}
			else
			{
				quaternion.x = (num2 * quaternion1.x) - (num * quaternion2.x);
				quaternion.y = (num2 * quaternion1.y) - (num * quaternion2.y);
				quaternion.z = (num2 * quaternion1.z) - (num * quaternion2.z);
				quaternion.w = (num2 * quaternion1.w) - (num * quaternion2.w);
			}
			float num4 = (((quaternion.x * quaternion.x) + (quaternion.y * quaternion.y)) + (quaternion.z * quaternion.z)) + (quaternion.w * quaternion.w);
			float num3 = 1f / MathF.Sqrt(num4);
			quaternion.x *= num3;
			quaternion.y *= num3;
			quaternion.z *= num3;
			quaternion.w *= num3;
			return quaternion;
		}

		/// <summary>
		/// Performs a linear blend between two quaternions.
		/// </summary>
		/// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
		/// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
		/// <param name="amount">The blend amount where 0 returns <paramref name="quaternion1"/> and 1 <paramref name="quaternion2"/>.</param>
		/// <param name="result">The result of linear blending between two quaternions as an output parameter.</param>
		public static void Lerp(in Quaternion quaternion1, in Quaternion quaternion2, float amount, out Quaternion result)
		{
			float num = amount;
			float num2 = 1f - num;
			float num5 = (((quaternion1.x * quaternion2.x) + (quaternion1.y * quaternion2.y)) + (quaternion1.z * quaternion2.z)) + (quaternion1.w * quaternion2.w);
			if (num5 >= 0f)
			{
				result.x = (num2 * quaternion1.x) + (num * quaternion2.x);
				result.y = (num2 * quaternion1.y) + (num * quaternion2.y);
				result.z = (num2 * quaternion1.z) + (num * quaternion2.z);
				result.w = (num2 * quaternion1.w) + (num * quaternion2.w);
			}
			else
			{
				result.x = (num2 * quaternion1.x) - (num * quaternion2.x);
				result.y = (num2 * quaternion1.y) - (num * quaternion2.y);
				result.z = (num2 * quaternion1.z) - (num * quaternion2.z);
				result.w = (num2 * quaternion1.w) - (num * quaternion2.w);
			}
			float num4 = (((result.x * result.x) + (result.y * result.y)) + (result.z * result.z)) + (result.w * result.w);
			float num3 = 1f / MathF.Sqrt(num4);
			result.x *= num3;
			result.y *= num3;
			result.z *= num3;
			result.w *= num3;

		}

		#endregion

		#region Slerp

		/// <summary>
		/// Performs a spherical linear blend between two quaternions.
		/// </summary>
		/// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
		/// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
		/// <param name="amount">The blend amount where 0 returns <paramref name="quaternion1"/> and 1 <paramref name="quaternion2"/>.</param>
		/// <returns>The result of spherical linear blending between two quaternions.</returns>
		public static Quaternion Slerp(in Quaternion quaternion1, in Quaternion quaternion2, float amount)
		{
			float num2;
			float num3;
			Quaternion quaternion;
			float num = amount;
			float num4 = (((quaternion1.x * quaternion2.x) + (quaternion1.y * quaternion2.y)) + (quaternion1.z * quaternion2.z)) + (quaternion1.w * quaternion2.w);
			bool flag = false;
			if (num4 < 0f)
			{
				flag = true;
				num4 = -num4;
			}
			if (num4 > 0.999999f)
			{
				num3 = 1f - num;
				num2 = flag ? -num : num;
			}
			else
			{
				float num5 = MathF.Acos(num4);
				float num6 = (float)(1.0 / Math.Sin((double)num5));
				num3 = MathF.Sin((1f - num) * num5) * num6;
				num2 = flag ? (-MathF.Sin(num * num5) * num6) : (MathF.Sin(num * num5) * num6);
			}
			quaternion.x = (num3 * quaternion1.x) + (num2 * quaternion2.x);
			quaternion.y = (num3 * quaternion1.y) + (num2 * quaternion2.y);
			quaternion.z = (num3 * quaternion1.z) + (num2 * quaternion2.z);
			quaternion.w = (num3 * quaternion1.w) + (num2 * quaternion2.w);
			return quaternion;
		}

		/// <summary>
		/// Performs a spherical linear blend between two quaternions.
		/// </summary>
		/// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
		/// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
		/// <param name="amount">The blend amount where 0 returns <paramref name="quaternion1"/> and 1 <paramref name="quaternion2"/>.</param>
		/// <param name="result">The result of spherical linear blending between two quaternions as an output parameter.</param>
		public static void Slerp(in Quaternion quaternion1, in Quaternion quaternion2, float amount, out Quaternion result)
		{
			float num2;
			float num3;
			float num = amount;
			float num4 = (((quaternion1.x * quaternion2.x) + (quaternion1.y * quaternion2.y)) + (quaternion1.z * quaternion2.z)) + (quaternion1.w * quaternion2.w);
			bool flag = false;
			if (num4 < 0f)
			{
				flag = true;
				num4 = -num4;
			}
			if (num4 > 0.999999f)
			{
				num3 = 1f - num;
				num2 = flag ? -num : num;
			}
			else
			{
				float num5 = MathF.Acos(num4);
				float num6 = (float)(1.0 / Math.Sin((double)num5));
				num3 = MathF.Sin((1f - num) * num5) * num6;
				num2 = flag ? (-MathF.Sin(num * num5) * num6) : (MathF.Sin(num * num5) * num6);
			}
			result.x = (num3 * quaternion1.x) + (num2 * quaternion2.x);
			result.y = (num3 * quaternion1.y) + (num2 * quaternion2.y);
			result.z = (num3 * quaternion1.z) + (num2 * quaternion2.z);
			result.w = (num3 * quaternion1.w) + (num2 * quaternion2.w);
		}

		#endregion

		#region Subtract

		/// <summary>
		/// Creates a new <see cref="Quaternion"/> that contains subtraction of one <see cref="Quaternion"/> from another.
		/// </summary>
		/// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
		/// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
		/// <returns>The result of the quaternion subtraction.</returns>
		public static Quaternion Subtract(in Quaternion quaternion1, in Quaternion quaternion2)
		{
			Quaternion quaternion;
			quaternion.x = quaternion1.x - quaternion2.x;
			quaternion.y = quaternion1.y - quaternion2.y;
			quaternion.z = quaternion1.z - quaternion2.z;
			quaternion.w = quaternion1.w - quaternion2.w;
			return quaternion;
		}

		/// <summary>
		/// Creates a new <see cref="Quaternion"/> that contains subtraction of one <see cref="Quaternion"/> from another.
		/// </summary>
		/// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
		/// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
		/// <param name="result">The result of the quaternion subtraction as an output parameter.</param>
		public static void Subtract(in Quaternion quaternion1, in Quaternion quaternion2, out Quaternion result)
		{
			result.x = quaternion1.x - quaternion2.x;
			result.y = quaternion1.y - quaternion2.y;
			result.z = quaternion1.z - quaternion2.z;
			result.w = quaternion1.w - quaternion2.w;
		}

		#endregion

		#region Multiply

		/// <summary>
		/// Creates a new <see cref="Quaternion"/> that contains a multiplication of two quaternions.
		/// </summary>
		/// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
		/// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
		/// <returns>The result of the quaternion multiplication.</returns>
		public static Quaternion Multiply(in Quaternion quaternion1, in Quaternion quaternion2)
		{
			Quaternion quaternion;
			float x = quaternion1.x;
			float y = quaternion1.y;
			float z = quaternion1.z;
			float w = quaternion1.w;
			float num4 = quaternion2.x;
			float num3 = quaternion2.y;
			float num2 = quaternion2.z;
			float num = quaternion2.w;
			float num12 = (y * num2) - (z * num3);
			float num11 = (z * num4) - (x * num2);
			float num10 = (x * num3) - (y * num4);
			float num9 = ((x * num4) + (y * num3)) + (z * num2);
			quaternion.x = ((x * num) + (num4 * w)) + num12;
			quaternion.y = ((y * num) + (num3 * w)) + num11;
			quaternion.z = ((z * num) + (num2 * w)) + num10;
			quaternion.w = (w * num) - num9;
			return quaternion;
		}

		/// <summary>
		/// Creates a new <see cref="Quaternion"/> that contains a multiplication of <see cref="Quaternion"/> and a scalar.
		/// </summary>
		/// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
		/// <param name="scaleFactor">Scalar value.</param>
		/// <returns>The result of the quaternion multiplication with a scalar.</returns>
		public static Quaternion Multiply(in Quaternion quaternion1, float scaleFactor)
		{
			Quaternion quaternion;
			quaternion.x = quaternion1.x * scaleFactor;
			quaternion.y = quaternion1.y * scaleFactor;
			quaternion.z = quaternion1.z * scaleFactor;
			quaternion.w = quaternion1.w * scaleFactor;
			return quaternion;
		}

		/// <summary>
		/// Creates a new <see cref="Quaternion"/> that contains a multiplication of <see cref="Quaternion"/> and a scalar.
		/// </summary>
		/// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
		/// <param name="scaleFactor">Scalar value.</param>
		/// <param name="result">The result of the quaternion multiplication with a scalar as an output parameter.</param>
		public static void Multiply(in Quaternion quaternion1, float scaleFactor, out Quaternion result)
		{
			result.x = quaternion1.x * scaleFactor;
			result.y = quaternion1.y * scaleFactor;
			result.z = quaternion1.z * scaleFactor;
			result.w = quaternion1.w * scaleFactor;
		}

		/// <summary>
		/// Creates a new <see cref="Quaternion"/> that contains a multiplication of two quaternions.
		/// </summary>
		/// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
		/// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
		/// <param name="result">The result of the quaternion multiplication as an output parameter.</param>
		public static void Multiply(in Quaternion quaternion1, in Quaternion quaternion2, out Quaternion result)
		{
			float x = quaternion1.x;
			float y = quaternion1.y;
			float z = quaternion1.z;
			float w = quaternion1.w;
			float num4 = quaternion2.x;
			float num3 = quaternion2.y;
			float num2 = quaternion2.z;
			float num = quaternion2.w;
			float num12 = (y * num2) - (z * num3);
			float num11 = (z * num4) - (x * num2);
			float num10 = (x * num3) - (y * num4);
			float num9 = ((x * num4) + (y * num3)) + (z * num2);
			result.x = ((x * num) + (num4 * w)) + num12;
			result.y = ((y * num) + (num3 * w)) + num11;
			result.z = ((z * num) + (num2 * w)) + num10;
			result.w = (w * num) - num9;
		}

		#endregion

		#region Negate

		/// <summary>
		/// Flips the sign of the all the quaternion components.
		/// </summary>
		/// <param name="quaternion">Source <see cref="Quaternion"/>.</param>
		/// <returns>The result of the quaternion negation.</returns>
		public static Quaternion Negate(in Quaternion quaternion)
		{
			return new Quaternion(-quaternion.x, -quaternion.y, -quaternion.z, -quaternion.w);
		}

		/// <summary>
		/// Flips the sign of the all the quaternion components.
		/// </summary>
		/// <param name="quaternion">Source <see cref="Quaternion"/>.</param>
		/// <param name="result">The result of the quaternion negation as an output parameter.</param>
		public static void Negate(in Quaternion quaternion, out Quaternion result)
		{
			result.x = -quaternion.x;
			result.y = -quaternion.y;
			result.z = -quaternion.z;
			result.w = -quaternion.w;
		}

		#endregion

		#region Normalize

		/// <summary>
		/// Scales the quaternion magnitude to unit length.
		/// </summary>
		public void Normalize()
		{
			float num = 1f / MathF.Sqrt((x * x) + (y * y) + (z * z) + (w * w));
			x *= num;
			y *= num;
			z *= num;
			w *= num;
		}

		/// <summary>
		/// Scales the quaternion magnitude to unit length.
		/// </summary>
		/// <param name="quaternion">Source <see cref="Quaternion"/>.</param>
		/// <returns>The unit length quaternion.</returns>
		public static Quaternion Normalize(in Quaternion quaternion)
		{
			Quaternion result;
			float num = 1f / MathF.Sqrt((quaternion.x * quaternion.x) + (quaternion.y * quaternion.y) + (quaternion.z * quaternion.z) + (quaternion.w * quaternion.w));
			result.x = quaternion.x * num;
			result.y = quaternion.y * num;
			result.z = quaternion.z * num;
			result.w = quaternion.w * num;
			return result;
		}

		/// <summary>
		/// Scales the quaternion magnitude to unit length.
		/// </summary>
		/// <param name="quaternion">Source <see cref="Quaternion"/>.</param>
		/// <param name="result">The unit length quaternion an output parameter.</param>
		public static void Normalize(in Quaternion quaternion, out Quaternion result)
		{
			float num = 1f / MathF.Sqrt((quaternion.x * quaternion.x) + (quaternion.y * quaternion.y) + (quaternion.z * quaternion.z) + (quaternion.w * quaternion.w));
			result.x = quaternion.x * num;
			result.y = quaternion.y * num;
			result.z = quaternion.z * num;
			result.w = quaternion.w * num;
		}

		#endregion

		/// <summary>
		/// Returns a <see cref="String"/> representation of this <see cref="Quaternion"/> in the format:
		/// {X:[<see cref="x"/>] Y:[<see cref="y"/>] Z:[<see cref="z"/>] W:[<see cref="w"/>]}
		/// </summary>
		/// <returns>A <see cref="String"/> representation of this <see cref="Quaternion"/>.</returns>
		public override string ToString()
		{
			return "{X:" + x + " Y:" + y + " Z:" + z + " W:" + w + "}";
		}

		/// <summary>
		/// Gets a <see cref="Vector4"/> representation for this object.
		/// </summary>
		/// <returns>A <see cref="Vector4"/> representation for this object.</returns>
		public Vector4 ToVector4()
		{
			return new Vector4(x, y, z, w);
		}

		public void Deconstruct(out float x, out float y, out float z, out float w)
		{
			x = this.x;
			y = this.y;
			z = this.z;
			w = this.w;
		}

		///// <summary>
		///// Returns a <see cref="System.Numerics.Quaternion"/>.
		///// </summary>
		//public System.Numerics.Quaternion ToNumerics()
		//{
		//	return new System.Numerics.Quaternion(this.X, this.Y, this.Z, this.W);
		//}

		#endregion

		#region Operators

		///// <summary>
		///// Converts a <see cref="System.Numerics.Quaternion"/> to a <see cref="Quaternion"/>.
		///// </summary>
		///// <param name="value">The converted value.</param>
		//public static implicit operator Quaternion(System.Numerics.Quaternion value)
		//{
		//	return new Quaternion(value.X, value.Y, value.Z, value.W);
		//}

		/// <summary>
		/// Adds two quaternions.
		/// </summary>
		/// <param name="quaternion1">Source <see cref="Quaternion"/> on the left of the add sign.</param>
		/// <param name="quaternion2">Source <see cref="Quaternion"/> on the right of the add sign.</param>
		/// <returns>Sum of the vectors.</returns>
		public static Quaternion operator +(in Quaternion quaternion1, in Quaternion quaternion2)
		{
			Quaternion quaternion;
			quaternion.x = quaternion1.x + quaternion2.x;
			quaternion.y = quaternion1.y + quaternion2.y;
			quaternion.z = quaternion1.z + quaternion2.z;
			quaternion.w = quaternion1.w + quaternion2.w;
			return quaternion;
		}

		/// <summary>
		/// Divides a <see cref="Quaternion"/> by the other <see cref="Quaternion"/>.
		/// </summary>
		/// <param name="quaternion1">Source <see cref="Quaternion"/> on the left of the div sign.</param>
		/// <param name="quaternion2">Divisor <see cref="Quaternion"/> on the right of the div sign.</param>
		/// <returns>The result of dividing the quaternions.</returns>
		public static Quaternion operator /(in Quaternion quaternion1, in Quaternion quaternion2)
		{
			Quaternion quaternion;
			float x = quaternion1.x;
			float y = quaternion1.y;
			float z = quaternion1.z;
			float w = quaternion1.w;
			float num14 = (((quaternion2.x * quaternion2.x) + (quaternion2.y * quaternion2.y)) + (quaternion2.z * quaternion2.z)) + (quaternion2.w * quaternion2.w);
			float num5 = 1f / num14;
			float num4 = -quaternion2.x * num5;
			float num3 = -quaternion2.y * num5;
			float num2 = -quaternion2.z * num5;
			float num = quaternion2.w * num5;
			float num13 = (y * num2) - (z * num3);
			float num12 = (z * num4) - (x * num2);
			float num11 = (x * num3) - (y * num4);
			float num10 = ((x * num4) + (y * num3)) + (z * num2);
			quaternion.x = ((x * num) + (num4 * w)) + num13;
			quaternion.y = ((y * num) + (num3 * w)) + num12;
			quaternion.z = ((z * num) + (num2 * w)) + num11;
			quaternion.w = (w * num) - num10;
			return quaternion;
		}

		/// <summary>
		/// Compares whether two <see cref="Quaternion"/> instances are equal.
		/// </summary>
		/// <param name="quaternion1"><see cref="Quaternion"/> instance on the left of the equal sign.</param>
		/// <param name="quaternion2"><see cref="Quaternion"/> instance on the right of the equal sign.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		public static bool operator ==(in Quaternion quaternion1, in Quaternion quaternion2)
		{
			return ((((quaternion1.x == quaternion2.x) && (quaternion1.y == quaternion2.y)) && (quaternion1.z == quaternion2.z)) && (quaternion1.w == quaternion2.w));
		}

		/// <summary>
		/// Compares whether two <see cref="Quaternion"/> instances are not equal.
		/// </summary>
		/// <param name="quaternion1"><see cref="Quaternion"/> instance on the left of the not equal sign.</param>
		/// <param name="quaternion2"><see cref="Quaternion"/> instance on the right of the not equal sign.</param>
		/// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>
		public static bool operator !=(in Quaternion quaternion1, in Quaternion quaternion2)
		{
			if (((quaternion1.x == quaternion2.x) && (quaternion1.y == quaternion2.y)) && (quaternion1.z == quaternion2.z))
			{
				return (quaternion1.w != quaternion2.w);
			}
			return true;
		}

		/// <summary>
		/// Multiplies two quaternions.
		/// </summary>
		/// <param name="quaternion1">Source <see cref="Quaternion"/> on the left of the mul sign.</param>
		/// <param name="quaternion2">Source <see cref="Quaternion"/> on the right of the mul sign.</param>
		/// <returns>Result of the quaternions multiplication.</returns>
		public static Quaternion operator *(in Quaternion quaternion1, in Quaternion quaternion2)
		{
			Quaternion quaternion;
			float x = quaternion1.x;
			float y = quaternion1.y;
			float z = quaternion1.z;
			float w = quaternion1.w;
			float num4 = quaternion2.x;
			float num3 = quaternion2.y;
			float num2 = quaternion2.z;
			float num = quaternion2.w;
			float num12 = (y * num2) - (z * num3);
			float num11 = (z * num4) - (x * num2);
			float num10 = (x * num3) - (y * num4);
			float num9 = ((x * num4) + (y * num3)) + (z * num2);
			quaternion.x = ((x * num) + (num4 * w)) + num12;
			quaternion.y = ((y * num) + (num3 * w)) + num11;
			quaternion.z = ((z * num) + (num2 * w)) + num10;
			quaternion.w = (w * num) - num9;
			return quaternion;
		}

		/// <summary>
		/// Multiplies the components of quaternion by a scalar.
		/// </summary>
		/// <param name="quaternion1">Source <see cref="Vector3"/> on the left of the mul sign.</param>
		/// <param name="scaleFactor">Scalar value on the right of the mul sign.</param>
		/// <returns>Result of the quaternion multiplication with a scalar.</returns>
		public static Quaternion operator *(in Quaternion quaternion1, float scaleFactor)
		{
			Quaternion quaternion;
			quaternion.x = quaternion1.x * scaleFactor;
			quaternion.y = quaternion1.y * scaleFactor;
			quaternion.z = quaternion1.z * scaleFactor;
			quaternion.w = quaternion1.w * scaleFactor;
			return quaternion;
		}

		/// <summary>
		/// Subtracts a <see cref="Quaternion"/> from a <see cref="Quaternion"/>.
		/// </summary>
		/// <param name="quaternion1">Source <see cref="Vector3"/> on the left of the sub sign.</param>
		/// <param name="quaternion2">Source <see cref="Vector3"/> on the right of the sub sign.</param>
		/// <returns>Result of the quaternion subtraction.</returns>
		public static Quaternion operator -(in Quaternion quaternion1, in Quaternion quaternion2)
		{
			Quaternion quaternion;
			quaternion.x = quaternion1.x - quaternion2.x;
			quaternion.y = quaternion1.y - quaternion2.y;
			quaternion.z = quaternion1.z - quaternion2.z;
			quaternion.w = quaternion1.w - quaternion2.w;
			return quaternion;

		}

		/// <summary>
		/// Flips the sign of the all the quaternion components.
		/// </summary>
		/// <param name="quaternion">Source <see cref="Quaternion"/> on the right of the sub sign.</param>
		/// <returns>The result of the quaternion negation.</returns>
		public static Quaternion operator -(in Quaternion quaternion)
		{
			Quaternion quaternion2;
			quaternion2.x = -quaternion.x;
			quaternion2.y = -quaternion.y;
			quaternion2.z = -quaternion.z;
			quaternion2.w = -quaternion.w;
			return quaternion2;
		}

		#endregion

		public static Quaternion Euler(float pitch, float yaw, float roll)
		{
			yaw *= 0.5f;
			pitch *= 0.5f;
			roll *= 0.5f;

			float c1 = (float)Math.Cos(yaw);
			float c2 = (float)Math.Cos(pitch);
			float c3 = (float)Math.Cos(roll);
			float s1 = (float)Math.Sin(yaw);
			float s2 = (float)Math.Sin(pitch);
			float s3 = (float)Math.Sin(roll);

			Quaternion q;
			q.x = s1 * s2 * c3 + c1 * c2 * s3;
			q.y = s1 * c2 * c3 + c1 * s2 * s3;
			q.z = c1 * s2 * c3 - s1 * c2 * s3;
			q.w = c1 * c2 * c3 - s1 * s2 * s3;

			return q;
		}

		// Rotates the point /point/ with /rotation/.
		public static Vector3 operator *(in Quaternion rotation, in Vector3 point)
		{
			float x = rotation.x * 2F;
			float y = rotation.y * 2F;
			float z = rotation.z * 2F;
			float xx = rotation.x * x;
			float yy = rotation.y * y;
			float zz = rotation.z * z;
			float xy = rotation.x * y;
			float xz = rotation.x * z;
			float yz = rotation.y * z;
			float wx = rotation.w * x;
			float wy = rotation.w * y;
			float wz = rotation.w * z;

			Vector3 res;
			res.x = (float)((1F - (yy + zz)) * point.x + (xy - wz) * point.y + (xz + wy) * point.z);
			res.y = (float)((xy + wz) * point.x + (1F - (xx + zz)) * point.y + (yz - wx) * point.z);
			res.z = (float)((xz - wy) * point.x + (yz + wx) * point.y + (1F - (xx + yy)) * point.z);
			return res;
		}
	}
}

#endif
