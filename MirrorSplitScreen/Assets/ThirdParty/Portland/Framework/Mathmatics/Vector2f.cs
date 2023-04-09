#region License
/*
MIT License
Copyright © 2006 The Mono.Xna Team

All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion License

#if !UNITY_5_3_OR_NEWER

using System;
using System.Text;
using System.Globalization;

namespace Portland.Mathmatics
{
	[Serializable]
	public struct Vector2 //: IEquatable<Vector2>
	{
		#region Static Fields

		private static readonly Vector2 zeroVector = new Vector2(0f, 0f);
		private static readonly Vector2 unitVector = new Vector2(1f, 1f);
		private static readonly Vector2 unitXVector = new Vector2(1f, 0f);
		private static readonly Vector2 unitYVector = new Vector2(0f, 1f);
		public static readonly Vector2 Up = new Vector2(0f, 1f);
		public static readonly Vector2 Down = new Vector2(0f, -1f);
		public static readonly Vector2 Right = new Vector2(1f, 0f);
		public static readonly Vector2 Left = new Vector2(-1f, 0f);

		#endregion Private Fields

		#region Public Fields

		public float x;
		public float y;

		#endregion Public Fields

		#region Properties

		public static Vector2 Zero
		{
			get { return zeroVector; }
		}

		public static Vector2 One
		{
			get { return unitVector; }
		}

		public static Vector2 UnitX
		{
			get { return unitXVector; }
		}

		public static Vector2 UnitY
		{
			get { return unitYVector; }
		}

		#endregion Properties

		#region Constructors

		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public Vector2(double x, double y)
		{
			this.x = (float)x;
			this.y = (float)y;
		}

		public Vector2(double value)
		{
			this.x = (float)value;
			this.y = (float)value;
		}

		#endregion Constructors

		#region Public Methods

		public static Vector2 Add(Vector2 value1, Vector2 value2)
		{
			value1.x += value2.x;
			value1.y += value2.y;
			return value1;
		}

		public static void Add(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.x = value1.x + value2.x;
			result.y = value1.y + value2.y;
		}

		public static Vector2 Barycentric(Vector2 value1, Vector2 value2, Vector2 value3, float amount1, float amount2)
		{
			return new Vector2(
				 MathHelper.Barycentric(value1.x, value2.x, value3.x, amount1, amount2),
				 MathHelper.Barycentric(value1.y, value2.y, value3.y, amount1, amount2));
		}

		public static void Barycentric(ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, float amount1, float amount2, out Vector2 result)
		{
			result = new Vector2(
				 MathHelper.Barycentric(value1.x, value2.x, value3.x, amount1, amount2),
				 MathHelper.Barycentric(value1.y, value2.y, value3.y, amount1, amount2));
		}

		public static Vector2 CatmullRom(Vector2 value1, Vector2 value2, Vector2 value3, Vector2 value4, float amount)
		{
			return new Vector2(
				 MathHelper.CatmullRom(value1.x, value2.x, value3.x, value4.x, amount),
				 MathHelper.CatmullRom(value1.y, value2.y, value3.y, value4.y, amount));
		}

		public static void CatmullRom(ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, ref Vector2 value4, float amount, out Vector2 result)
		{
			result = new Vector2(
				 MathHelper.CatmullRom(value1.x, value2.x, value3.x, value4.x, amount),
				 MathHelper.CatmullRom(value1.y, value2.y, value3.y, value4.y, amount));
		}

		public static Vector2 Clamp(Vector2 value1, Vector2 min, Vector2 max)
		{
			return new Vector2(
				 MathHelper.Clamp(value1.x, min.x, max.x),
				 MathHelper.Clamp(value1.y, min.y, max.y));
		}

		public static void Clamp(ref Vector2 value1, ref Vector2 min, ref Vector2 max, out Vector2 result)
		{
			result = new Vector2(
				 MathHelper.Clamp(value1.x, min.x, max.x),
				 MathHelper.Clamp(value1.y, min.y, max.y));
		}

		public static float Distance(Vector2 value1, Vector2 value2)
		{
			float v1 = value1.x - value2.x, v2 = value1.y - value2.y;
			return MathF.Sqrt((v1 * v1) + (v2 * v2));
		}

		public static void Distance(ref Vector2 value1, ref Vector2 value2, out float result)
		{
			float v1 = value1.x - value2.x, v2 = value1.y - value2.y;
			result = MathF.Sqrt((v1 * v1) + (v2 * v2));
		}

		public static float DistanceSquared(Vector2 value1, Vector2 value2)
		{
			float v1 = value1.x - value2.x, v2 = value1.y - value2.y;
			return (v1 * v1) + (v2 * v2);
		}

		public static void DistanceSquared(ref Vector2 value1, ref Vector2 value2, out float result)
		{
			float v1 = value1.x - value2.x, v2 = value1.y - value2.y;
			result = (v1 * v1) + (v2 * v2);
		}

		public static Vector2 Divide(Vector2 value1, Vector2 value2)
		{
			value1.x /= value2.x;
			value1.y /= value2.y;
			return value1;
		}

		public static void Divide(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.x = value1.x / value2.x;
			result.y = value1.y / value2.y;
		}

		public static Vector2 Divide(Vector2 value1, float divider)
		{
			float factor = 1 / divider;
			value1.x *= factor;
			value1.y *= factor;
			return value1;
		}

		public static void Divide(ref Vector2 value1, float divider, out Vector2 result)
		{
			float factor = 1 / divider;
			result.x = value1.x * factor;
			result.y = value1.y * factor;
		}

		public static float Dot(Vector2 value1, Vector2 value2)
		{
			return (value1.x * value2.x) + (value1.y * value2.y);
		}

		public static void Dot(ref Vector2 value1, ref Vector2 value2, out float result)
		{
			result = (value1.x * value2.x) + (value1.y * value2.y);
		}

		public override bool Equals(object obj)
		{
			if (obj is Vector2)
			{
				return Equals((Vector2)this);
			}

			return false;
		}

		public bool Equals(Vector2 other)
		{
			return (x == other.x) && (y == other.y);
		}

		public static Vector2 Reflect(Vector2 vector, Vector2 normal)
		{
			Vector2 result;
			float val = 2.0f * ((vector.x * normal.x) + (vector.y * normal.y));
			result.x = vector.x - (normal.x * val);
			result.y = vector.y - (normal.y * val);
			return result;
		}

		public static void Reflect(ref Vector2 vector, ref Vector2 normal, out Vector2 result)
		{
			float val = 2.0f * ((vector.x * normal.x) + (vector.y * normal.y));
			result.x = vector.x - (normal.x * val);
			result.y = vector.y - (normal.y * val);
		}

		public override int GetHashCode()
		{
			return (int)((x + y) * (x + y + 1) / 2 + y);
		}

		public static Vector2 Hermite(Vector2 value1, Vector2 tangent1, Vector2 value2, Vector2 tangent2, float amount)
		{
			Vector2 result = new Vector2();
			Hermite(ref value1, ref tangent1, ref value2, ref tangent2, amount, out result);
			return result;
		}

		public static void Hermite(ref Vector2 value1, ref Vector2 tangent1, ref Vector2 value2, ref Vector2 tangent2, float amount, out Vector2 result)
		{
			result.x = MathHelper.Hermite(value1.x, tangent1.x, value2.x, tangent2.x, amount);
			result.y = MathHelper.Hermite(value1.y, tangent1.y, value2.y, tangent2.y, amount);
		}

		public float Magnitude
		{
			get { return MathF.Sqrt((x * x) + (y * y)); }
		}

		public float magnitude
		{
			get { return Magnitude; }
		}

		public float SqrMagnitude
		{
			get { return (x * x) + (y * y); }
		}

		public float Length()
		{
			return MathF.Sqrt((x * x) + (y * y));
		}

		public float LengthSquared()
		{
			return (x * x) + (y * y);
		}

		public static Vector2 Lerp(Vector2 value1, Vector2 value2, float amount)
		{
			return new Vector2(
				 MathHelper.Lerp(value1.x, value2.x, amount),
				 MathHelper.Lerp(value1.y, value2.y, amount));
		}

		public static void Lerp(ref Vector2 value1, ref Vector2 value2, float amount, out Vector2 result)
		{
			result = new Vector2(
				 MathHelper.Lerp(value1.x, value2.x, amount),
				 MathHelper.Lerp(value1.y, value2.y, amount));
		}

		public static Vector2 Max(Vector2 value1, Vector2 value2)
		{
			return new Vector2(value1.x > value2.x ? value1.x : value2.x,
								 value1.y > value2.y ? value1.y : value2.y);
		}

		public static void Max(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.x = value1.x > value2.x ? value1.x : value2.x;
			result.y = value1.y > value2.y ? value1.y : value2.y;
		}

		public static Vector2 Min(Vector2 value1, Vector2 value2)
		{
			return new Vector2(value1.x < value2.x ? value1.x : value2.x,
								 value1.y < value2.y ? value1.y : value2.y);
		}

		public static void Min(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.x = value1.x < value2.x ? value1.x : value2.x;
			result.y = value1.y < value2.y ? value1.y : value2.y;
		}

		public static Vector2 Multiply(Vector2 value1, Vector2 value2)
		{
			value1.x *= value2.x;
			value1.y *= value2.y;
			return value1;
		}

		public static Vector2 Multiply(Vector2 value1, float scaleFactor)
		{
			value1.x *= scaleFactor;
			value1.y *= scaleFactor;
			return value1;
		}

		public static void Multiply(ref Vector2 value1, float scaleFactor, out Vector2 result)
		{
			result.x = value1.x * scaleFactor;
			result.y = value1.y * scaleFactor;
		}

		public static void Multiply(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.x = value1.x * value2.x;
			result.y = value1.y * value2.y;
		}

		public static Vector2 Negate(Vector2 value)
		{
			value.x = -value.x;
			value.y = -value.y;
			return value;
		}

		public static void Negate(ref Vector2 value, out Vector2 result)
		{
			result.x = -value.x;
			result.y = -value.y;
		}

		public void Normalize()
		{
			float val = 1.0f / (float)Math.Sqrt((x * x) + (y * y));
			x *= val;
			y *= val;
		}

		public Vector2 Normalized
		{
			get
			{
				Vector2 ret;
				Normalize(ref this, out ret);
				return ret;
			}
		}

		public Vector2 normalized
		{
			get
			{
				return Normalized;
			}
		}

		public static Vector2 Normalize(Vector2 value)
		{
			var sqrt = MathF.Sqrt((value.x * value.x) + (value.y * value.y));
			if (sqrt == 0)
			{
				return Vector2.Zero;
			}
			float val = 1.0f / sqrt;
			value.x *= val;
			value.y *= val;
			return value;
		}

		public static void Normalize(ref Vector2 value, out Vector2 result)
		{
			var sqrt = MathF.Sqrt((value.x * value.x) + (value.y * value.y));
			if (sqrt == 0)
			{
				result = Zero;
			}
			else
			{
				float val = 1.0f / sqrt;
				result.x = value.x * val;
				result.y = value.y * val;
			}
		}

		public static Vector2 SmoothStep(in Vector2 value1, in Vector2 value2, float amount)
		{
			return new Vector2(
				 MathHelper.SmoothStep(value1.x, value2.x, amount),
				 MathHelper.SmoothStep(value1.y, value2.y, amount));
		}

		public static void SmoothStep(in Vector2 value1, in Vector2 value2, float amount, out Vector2 result)
		{
			result = new Vector2(
				 MathHelper.SmoothStep(value1.x, value2.x, amount),
				 MathHelper.SmoothStep(value1.y, value2.y, amount));
		}

		public static Vector2 Subtract(Vector2 value1, in Vector2 value2)
		{
			value1.x -= value2.x;
			value1.y -= value2.y;
			return value1;
		}

		public static void Subtract(in Vector2 value1, in Vector2 value2, out Vector2 result)
		{
			result.x = value1.x - value2.x;
			result.y = value1.y - value2.y;
		}

		public static Vector2 Transform(in Vector2 position, in Matrix4x4 matrix)
		{
			Transform(position, matrix, out var ret);
			return ret;
		}

		public static void Transform(in Vector2 position, in Matrix4x4 matrix, out Vector2 result)
		{
			result = new Vector2((position.x * matrix.m11) + (position.y * matrix.m21) + matrix.m41,
										(position.x * matrix.m12) + (position.y * matrix.m22) + matrix.m42);
		}

		public static Vector2 Transform(in Vector2 position, in Quaternion quat)
		{
			Transform(position, quat, out var ret);
			return ret;
		}

		public static void Transform(in Vector2 position, in Quaternion quat, out Vector2 result)
		{
			Quaternion v = new Quaternion((float)position.x, (float)position.y, 0, 0), i, t;
			Quaternion.Inverse(quat, out i);
			Quaternion.Multiply(quat, v, out t);
			Quaternion.Multiply(t, i, out v);

			result = new Vector2(v.x, v.y);
		}

		public static void Transform(
			Vector2[] sourceArray,
			in Matrix4x4 matrix,
			Vector2[] destinationArray)
		{
			Transform(sourceArray, 0, matrix, destinationArray, 0, sourceArray.Length);
		}

		public static void Transform(
			Vector2[] sourceArray,
			int sourceIndex,
			in Matrix4x4 matrix,
			Vector2[] destinationArray,
			int destinationIndex,
			int length)
		{
			for (int x = 0; x < length; x++)
			{
				var position = sourceArray[sourceIndex + x];
				var destination = destinationArray[destinationIndex + x];
				destination.x = (position.x * matrix.m11) + (position.y * matrix.m21) + matrix.m41;
				destination.y = (position.x * matrix.m12) + (position.y * matrix.m22) + matrix.m42;
				destinationArray[destinationIndex + x] = destination;
			}
		}

		public static Vector2 TransformNormal(in Vector2 normal, in Matrix4x4 matrix)
		{
			Vector2.TransformNormal(normal, matrix, out var ret);
			return ret;
		}

		public static void TransformNormal(in Vector2 normal, in Matrix4x4 matrix, out Vector2 result)
		{
			result = new Vector2((normal.x * matrix.m11) + (normal.y * matrix.m21),
										(normal.x * matrix.m12) + (normal.y * matrix.m22));
		}

		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{X:{0} Y:{1}}}", new object[] {
				this.x.ToString(currentCulture), this.y.ToString(currentCulture) });
		}

		#endregion Public Methods

		#region Operators

		public static Vector2 operator -(Vector2 value)
		{
			value.x = -value.x;
			value.y = -value.y;
			return value;
		}

		public static bool operator ==(Vector2 value1, Vector2 value2)
		{
			return value1.x == value2.x && value1.y == value2.y;
		}

		public static bool operator !=(Vector2 value1, Vector2 value2)
		{
			return value1.x != value2.x || value1.y != value2.y;
		}

		public static Vector2 operator +(Vector2 value1, Vector2 value2)
		{
			value1.x += value2.x;
			value1.y += value2.y;
			return value1;
		}

		public static Vector2 operator -(Vector2 value1, Vector2 value2)
		{
			value1.x -= value2.x;
			value1.y -= value2.y;
			return value1;
		}

		public static Vector2 operator *(Vector2 value1, Vector2 value2)
		{
			value1.x *= value2.x;
			value1.y *= value2.y;
			return value1;
		}

		public static Vector2 operator *(Vector2 value, float scaleFactor)
		{
			value.x *= scaleFactor;
			value.y *= scaleFactor;
			return value;
		}

		public static Vector2 operator *(float scaleFactor, Vector2 value)
		{
			value.x *= scaleFactor;
			value.y *= scaleFactor;
			return value;
		}

		public static Vector2 operator /(Vector2 value1, Vector2 value2)
		{
			value1.x /= value2.x;
			value1.y /= value2.y;
			return value1;
		}

		public static Vector2 operator /(Vector2 value1, float divider)
		{
			float factor = 1 / divider;
			value1.x *= factor;
			value1.y *= factor;
			return value1;
		}

		#endregion Operators

		/// <summary>
		/// Returns a new vector with zero Y component
		/// </summary>
		public Vector2 ToVector2X()
		{
			return new Vector2(x, 0);
		}

		/// <summary>
		/// Returns a new vector with zero X component
		/// </summary>
		public Vector2 ToVector2Y()
		{
			return new Vector2(0, y);
		}

		/// <summary>
		/// Projects the vector onto the three dimensional XY plane
		/// </summary>
		public Vector3 ToVector3XY()
		{
			return new Vector3(x, y, 0);
		}

		/// <summary>
		/// Projects the vector onto the three dimensional XZ plane
		/// </summary>
		public Vector3 ToVector3XZ()
		{
			return new Vector3(x, 0, y);
		}

		/// <summary>
		/// Projects the vector onto the three dimensional YZ plane
		/// </summary>
		public Vector3 ToVector3YZ()
		{
			return new Vector3(0, x, y);
		}

		/// <summary>
		/// Returns true if vectors lie on the same line, false otherwise
		/// </summary>
		public bool IsCollinear(Vector2 other)
		{
			return Math.Abs(PerpDot(this, other)) < MathHelper.Epsilonf;
		}

		/// <summary>
		/// Returns a new vector rotated counterclockwise by 90°
		/// </summary>
		/// <remarks>
		/// Hill, F. S. Jr. "The Pleasures of 'Perp Dot' Products."
		/// Ch. II.5 in Graphics Gems IV (Ed. P. S. Heckbert). San Diego: Academic Press, pp. 138-148, 1994
		/// </remarks>
		public Vector2 Perp()
		{
			return new Vector2(-y, x);
		}

		/// <summary>
		/// Returns a perp dot product of vectors
		/// </summary>
		/// <remarks>
		/// Hill, F. S. Jr. "The Pleasures of 'Perp Dot' Products."
		/// Ch. II.5 in Graphics Gems IV (Ed. P. S. Heckbert). San Diego: Academic Press, pp. 138-148, 1994
		/// </remarks>
		public static float PerpDot(Vector2 a, Vector2 b)
		{
			return a.x * b.y - a.y * b.x;
		}

		/// <summary>
		/// Returns a signed clockwise angle in degrees [-180, 180] between from and to
		/// </summary>
		/// <param name="from">The angle extends round from this vector</param>
		/// <param name="to">The angle extends round to this vector</param>
		public static float SignedAngle(Vector2 from, Vector2 to)
		{
			return (float)Math.Atan2(PerpDot(to, from), Vector2.Dot(to, from)) * MathHelper.Rad2Degf;
		}

		/// <summary>
		/// Returns a clockwise angle in degrees [0, 360] between from and to
		/// </summary>
		/// <param name="from">The angle extends round from this vector</param>
		/// <param name="to">The angle extends round to this vector</param>
		public static float Angle360(Vector2 from, Vector2 to)
		{
			float angle = SignedAngle(from, to);
			while (angle < 0)
			{
				angle += 360;
			}
			return angle;
		}

		/// <summary>
		/// Calculates the linear parameter t that produces the interpolant value within the range [a, b].
		/// </summary>
		public static Vector2 InverseLerp(Vector2 a, Vector2 b, Vector2 value)
		{
			return new Vector2(
				 MathHelper.InverseLerp(a.x, b.x, value.x),
				 MathHelper.InverseLerp(a.y, b.y, value.y));
		}

		/// <summary>
		/// Returns a new vector rotated clockwise by the specified angle
		/// </summary>
		public Vector2 RotateCW(float degrees)
		{
			float radians = degrees * MathHelper.Deg2Radf;
			float sin = MathF.Sin(radians);
			float cos = MathF.Cos(radians);
			return new Vector2(
				 x * cos + y * sin,
				 y * cos - x * sin);
		}

		/// <summary>
		/// Returns a new vector rotated counterclockwise by the specified angle
		/// </summary>
		public Vector2 RotateCCW(float degrees)
		{
			float radians = degrees * MathHelper.Deg2Radf;
			float sin = MathF.Sin(radians);
			float cos = MathF.Cos(radians);
			return new Vector2(
				 x * cos - y * sin,
				 y * cos + x * sin);
		}

		/// <summary>
		/// Returns a new vector rotated clockwise by 45°
		/// </summary>
		public Vector2 RotateCW45()
		{
			return new Vector2((x + y) * MathHelper.Sqrt05f, (y - x) * MathHelper.Sqrt05f);
		}

		/// <summary>
		/// Returns a new vector rotated counterclockwise by 45°
		/// </summary>
		public Vector2 RotateCCW45()
		{
			return new Vector2((x - y) * MathHelper.Sqrt05f, (y + x) * MathHelper.Sqrt05f);
		}

		/// <summary>
		/// Returns a new vector rotated clockwise by 90°
		/// </summary>
		public Vector2 RotateCW90()
		{
			return new Vector2(y, -x);
		}

		/// <summary>
		/// Returns a new vector rotated counterclockwise by 90°
		/// </summary>
		public Vector2 RotateCCW90()
		{
			return new Vector2(-y, x);
		}

		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("({0}, {1})", x.ToString(format, formatProvider), y.ToString(format, formatProvider));
		}

		public Vector3 ToVector3()
		{
			Vector3 v;
			v.x = x;
			v.y = y;
			v.z = 0f;

			return v;
		}

		/// <summary>
		/// Round the members of this <see cref="Vector3 /> towards negative infinity.
		/// </summary>
		public void Floor()
		{
			x = MathF.Floor(x);
			y = MathF.Floor(y);
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains members from another vector rounded towards negative infinity.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3 />.</param>
		/// <returns>The rounded <see cref="Vector3 />.</returns>
		public static Vector2 Floor(Vector2 value)
		{
			value.x = MathF.Floor(value.x);
			value.y = MathF.Floor(value.y);
			return value;
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains members from another vector rounded towards negative infinity.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3 />.</param>
		/// <param name="result">The rounded <see cref="Vector3 />.</param>
		public static void Floor(in Vector2 value, out Vector2 result)
		{
			result.x = MathF.Floor(value.x);
			result.y = MathF.Floor(value.y);
		}

		/// <summary>
		/// Round the members of this <see cref="Vector3 /> towards positive infinity.
		/// </summary>
		public void Ceiling()
		{
			x = MathF.Ceiling(x);
			y = MathF.Ceiling(y);
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains members from another vector rounded towards positive infinity.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3 />.</param>
		/// <returns>The rounded <see cref="Vector3 />.</returns>
		public static Vector2 Ceiling(in Vector2 value)
		{
			return new Vector2(MathF.Ceiling(value.x), MathF.Ceiling(value.y));
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains members from another vector rounded towards positive infinity.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3 />.</param>
		/// <param name="result">The rounded <see cref="Vector3 />.</param>
		public static void Ceiling(in Vector2 value, out Vector2 result)
		{
			result.x = MathF.Ceiling(value.x);
			result.y = MathF.Ceiling(value.y);
		}

		/// <summary>
		/// Round the members of this <see cref="Vector3 /> towards the nearest integer value.
		/// </summary>
		public void Round()
		{
			x = MathF.Round(x);
			y = MathF.Round(y);
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains members from another vector rounded to the nearest integer value.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3 />.</param>
		/// <returns>The rounded <see cref="Vector3 />.</returns>
		public static Vector2 Round(Vector2 value)
		{
			value.x = MathF.Round(value.x);
			value.y = MathF.Round(value.y);
			return value;
		}

		/// <summary>
		/// Creates a new <see cref="Vector3 /> that contains members from another vector rounded to the nearest integer value.
		/// </summary>
		/// <param name="value">Source <see cref="Vector3 />.</param>
		/// <param name="result">The rounded <see cref="Vector3 />.</param>
		public static void Round(in Vector2 value, out Vector2 result)
		{
			result.x = MathF.Round(value.x);
			result.y = MathF.Round(value.y);
		}
	}
}

#endif
