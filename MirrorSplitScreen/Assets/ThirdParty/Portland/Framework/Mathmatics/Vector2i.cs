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

using System;
using System.Globalization;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Portland.Mathmatics
{
	[Serializable]
	public struct Vector2i //: IEquatable<Vector2Int>
	{
		#region Private Fields

		private static Vector2i zeroVector = new Vector2i(0, 0);
		private static Vector2i unitVector = new Vector2i(1, 1);
		private static Vector2i unitXVector = new Vector2i(1, 0);
		private static Vector2i unitYVector = new Vector2i(0, 1);

		#endregion Private Fields

		#region Public Fields

		public int X;
		public int Y;

		#endregion Public Fields

		#region Properties

		public static Vector2i Zero
		{
			get { return zeroVector; }
		}

		public static Vector2i One
		{
			get { return unitVector; }
		}

		public static Vector2i UnitX
		{
			get { return unitXVector; }
		}

		public static Vector2i UnitY
		{
			get { return unitYVector; }
		}

		#endregion Properties

		#region Constructors

		public Vector2i(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		public Vector2i(float x, float y)
		{
			this.X = (int)x;
			this.Y = (int)y;
		}

		#endregion Constructors

		#region Public Methods

		public static Vector2i Add(Vector2i value1, Vector2i value2)
		{
			value1.X += value2.X;
			value1.Y += value2.Y;
			return value1;
		}

		public static void Add(ref Vector2i value1, ref Vector2i value2, out Vector2i result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
		}

		public static Vector2i Clamp(Vector2i value1, Vector2i min, Vector2i max)
		{
			return new Vector2i(
				 MathHelper.Clamp(value1.X, min.X, max.X),
				 MathHelper.Clamp(value1.Y, min.Y, max.Y));
		}

		public static void Clamp(ref Vector2i value1, ref Vector2i min, ref Vector2i max, out Vector2i result)
		{
			result = new Vector2i(
				 MathHelper.Clamp(value1.X, min.X, max.X),
				 MathHelper.Clamp(value1.Y, min.Y, max.Y));
		}

		public static double Distance(Vector2i value1, Vector2i value2)
		{
			double v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			return (double)Math.Sqrt((v1 * v1) + (v2 * v2));
		}

		public static void Distance(ref Vector2i value1, ref Vector2i value2, out float result)
		{
			float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			result = (float)Math.Sqrt((v1 * v1) + (v2 * v2));
		}

		public static double DistanceSquared(Vector2i value1, Vector2i value2)
		{
			float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			return (v1 * v1) + (v2 * v2);
		}

		public static void DistanceSquared(ref Vector2i value1, ref Vector2i value2, out float result)
		{
			float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			result = (v1 * v1) + (v2 * v2);
		}

		public static Vector2i Divide(Vector2i value1, Vector2i value2)
		{
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			return value1;
		}

		public static void Divide(ref Vector2i value1, ref Vector2i value2, out Vector2i result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
		}

		public static Vector2i Divide(Vector2i value1, int divider)
		{
			value1.X /= divider;
			value1.Y /= divider;
			return value1;
		}

		public static void Divide(ref Vector2i value1, float divider, out Vector2i result)
		{
			result.X = (int)(value1.X / divider);
			result.Y = (int)(value1.Y / divider);
		}

		public static float Dot(Vector2i value1, Vector2i value2)
		{
			return (value1.X * value2.X) + (value1.Y * value2.Y);
		}

		public static void Dot(ref Vector2i value1, ref Vector2i value2, out float result)
		{
			result = (value1.X * value2.X) + (value1.Y * value2.Y);
		}

		public override bool Equals(object obj)
		{
			if (obj is Vector2i)
			{
				return Equals((Vector2i)this);
			}

			return false;
		}

		public bool Equals(Vector2i other)
		{
			return (X == other.X) && (Y == other.Y);
		}

		public static Vector2i Reflect(Vector2i vector, Vector2i normal)
		{
			Vector2i result;
			int val = 2 * ((vector.X * normal.X) + (vector.Y * normal.Y));
			result.X = vector.X - (normal.X * val);
			result.Y = vector.Y - (normal.Y * val);
			return result;
		}

		public static void Reflect(ref Vector2i vector, ref Vector2i normal, out Vector2i result)
		{
			int val = 2 * ((vector.X * normal.X) + (vector.Y * normal.Y));
			result.X = vector.X - (normal.X * val);
			result.Y = vector.Y - (normal.Y * val);
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() + Y.GetHashCode();
		}

		//public static Vector2 Hermite(Vector2Int value1, Vector2Int tangent1, Vector2Int value2, Vector2Int tangent2, float amount)
		//{
		//	Vector2 result = new Vector2();
		//	Hermite(ref value1, ref tangent1, ref value2, ref tangent2, amount, out result);
		//	return result;
		//}

		//public static void Hermite(ref Vector2 value1, ref Vector2 tangent1, ref Vector2Int value2, ref Vector2Int tangent2, float amount, out Vector2 result)
		//{
		//	result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
		//	result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
		//}

		public float Length()
		{
			return (float)Math.Sqrt((X * X) + (Y * Y));
		}

		public float LengthSquared()
		{
			return (X * X) + (Y * Y);
		}

		public static Vector2i Lerp(Vector2i value1, Vector2i value2, float amount)
		{
			return new Vector2i(
				 MathHelper.Lerp(value1.X, value2.X, amount),
				 MathHelper.Lerp(value1.Y, value2.Y, amount));
		}

		public static void Lerp(ref Vector2i value1, ref Vector2i value2, float amount, out Vector2i result)
		{
			result = new Vector2i(
				 MathHelper.Lerp(value1.X, value2.X, amount),
				 MathHelper.Lerp(value1.Y, value2.Y, amount));
		}

		public static Vector2i Max(Vector2 value1, Vector2 value2)
		{
			return new Vector2i(value1.x > value2.x ? (float)value1.x : (float)value2.x,
								 value1.y > value2.y ? (float)value1.y : (float)value2.y);
		}

		public static void Max(ref Vector2i value1, ref Vector2i value2, out Vector2i result)
		{
			result.X = value1.X > value2.X ? value1.X : value2.X;
			result.Y = value1.Y > value2.Y ? value1.Y : value2.Y;
		}

		public static Vector2i Min(Vector2i value1, Vector2i value2)
		{
			return new Vector2i(value1.X < value2.X ? value1.X : value2.X,
								 value1.Y < value2.Y ? value1.Y : value2.Y);
		}

		public static void Min(ref Vector2i value1, ref Vector2i value2, out Vector2i result)
		{
			result.X = value1.X < value2.X ? value1.X : value2.X;
			result.Y = value1.Y < value2.Y ? value1.Y : value2.Y;
		}

		public static Vector2i Multiply(Vector2i value1, Vector2i value2)
		{
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			return value1;
		}

		public static Vector2i Multiply(Vector2i value1, int scaleFactor)
		{
			value1.X *= scaleFactor;
			value1.Y *= scaleFactor;
			return value1;
		}

		public static void Multiply(ref Vector2i value1, float scaleFactor, out Vector2i result)
		{
			result.X = (int)(value1.X * scaleFactor);
			result.Y = (int)(value1.Y * scaleFactor);
		}

		public static void Multiply(ref Vector2i value1, ref Vector2i value2, out Vector2i result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
		}

		public static Vector2i Negate(Vector2i value)
		{
			value.X = -value.X;
			value.Y = -value.Y;
			return value;
		}

		public static void Negate(ref Vector2i value, out Vector2i result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
		}

		public void Normalize()
		{
			int val = (int)(1.0f / Math.Sqrt((X * X) + (Y * Y)));
			X *= val;
			Y *= val;
		}

		public static Vector2i Normalize(Vector2i value)
		{
			int val = (int)(1.0f / Math.Sqrt((value.X * value.X) + (value.Y * value.Y)));
			value.X *= val;
			value.Y *= val;
			return value;
		}

		public static void Normalize(ref Vector2i value, out Vector2i result)
		{
			int val = (int)(1.0f / Math.Sqrt((value.X * value.X) + (value.Y * value.Y)));
			result.X = value.X * val;
			result.Y = value.Y * val;
		}

		public static Vector2i SmoothStep(Vector2i value1, Vector2i value2, float amount)
		{
			return new Vector2i(
				 MathHelper.SmoothStep(value1.X, value2.X, amount),
				 MathHelper.SmoothStep(value1.Y, value2.Y, amount));
		}

		public static void SmoothStep(ref Vector2i value1, ref Vector2i value2, float amount, out Vector2i result)
		{
			result = new Vector2i(
				 MathHelper.SmoothStep(value1.X, value2.X, amount),
				 MathHelper.SmoothStep(value1.Y, value2.Y, amount));
		}

		public static Vector2i Subtract(Vector2i value1, Vector2i value2)
		{
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			return value1;
		}

		public static void Subtract(ref Vector2i value1, ref Vector2i value2, out Vector2i result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
		}

		//public static Vector2Int Transform(Vector2Int position, Matrix matrix)
		//{
		//	Transform(ref position, ref matrix, out position);
		//	return position;
		//}

		//public static void Transform(ref Vector2Int position, ref Matrix matrix, out Vector2Int result)
		//{
		//	result = new Vector2((position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41,
		//								(position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42);
		//}

		//public static Vector2 Transform(Vector2 position, Quaternion quat)
		//{
		//	Transform(ref position, ref quat, out position);
		//	return position;
		//}

		//public static void Transform(ref Vector2 position, ref Quaternion quat, out Vector2 result)
		//{
		//	Quaternion v = new Quaternion(position.X, position.Y, 0, 0), i, t;
		//	Quaternion.Inverse(ref quat, out i);
		//	Quaternion.Multiply(ref quat, ref v, out t);
		//	Quaternion.Multiply(ref t, ref i, out v);

		//	result = new Vector2(v.X, v.Y);
		//}

		//public static void Transform(
		//	Vector2[] sourceArray,
		//	ref Matrix matrix,
		//	Vector2[] destinationArray)
		//{
		//	Transform(sourceArray, 0, ref matrix, destinationArray, 0, sourceArray.Length);
		//}


		//public static void Transform(
		//	Vector2[] sourceArray,
		//	int sourceIndex,
		//	ref Matrix matrix,
		//	Vector2[] destinationArray,
		//	int destinationIndex,
		//	int length)
		//{
		//	for (int x = 0; x < length; x++)
		//	{
		//		var position = sourceArray[sourceIndex + x];
		//		var destination = destinationArray[destinationIndex + x];
		//		destination.X = (position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41;
		//		destination.Y = (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42;
		//		destinationArray[destinationIndex + x] = destination;
		//	}
		//}

		//public static Vector2 TransformNormal(Vector2 normal, Matrix matrix)
		//{
		//	Vector2.TransformNormal(ref normal, ref matrix, out normal);
		//	return normal;
		//}

		//public static void TransformNormal(ref Vector2 normal, ref Matrix matrix, out Vector2 result)
		//{
		//	result = new Vector2((normal.X * matrix.M11) + (normal.Y * matrix.M21),
		//								(normal.X * matrix.M12) + (normal.Y * matrix.M22));
		//}

		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{X:{0} Y:{1}}}", new object[] {
				this.X.ToString(currentCulture), this.Y.ToString(currentCulture) });
		}

		#endregion Public Methods

		#region Operators

		public static Vector2i operator -(Vector2i value)
		{
			value.X = -value.X;
			value.Y = -value.Y;
			return value;
		}

		public static bool operator ==(Vector2i value1, Vector2i value2)
		{
			return value1.X == value2.X && value1.Y == value2.Y;
		}

		public static bool operator !=(Vector2i value1, Vector2i value2)
		{
			return value1.X != value2.X || value1.Y != value2.Y;
		}

		public static Vector2i operator +(Vector2i value1, Vector2i value2)
		{
			value1.X += value2.X;
			value1.Y += value2.Y;
			return value1;
		}

		public static Vector2i operator -(Vector2i value1, Vector2i value2)
		{
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			return value1;
		}

		public static Vector2i operator *(Vector2i value1, Vector2i value2)
		{
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			return value1;
		}

		public static Vector2i operator *(Vector2i value, int scaleFactor)
		{
			value.X *= scaleFactor;
			value.Y *= scaleFactor;
			return value;
		}

		public static Vector2i operator *(int scaleFactor, Vector2i value)
		{
			value.X *= scaleFactor;
			value.Y *= scaleFactor;
			return value;
		}

		public static Vector2i operator /(Vector2i value1, Vector2i value2)
		{
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			return value1;
		}

		public static Vector2i operator /(Vector2i value1, int divider)
		{
			value1.X /= divider;
			value1.Y /= divider;
			return value1;
		}

		#endregion Operators

		/// <summary>
		/// Returns a new vector with zero Y component
		/// </summary>
		public Vector2i ToVector2X()
		{
			return new Vector2i(X, 0);
		}

		/// <summary>
		/// Returns a new vector with zero X component
		/// </summary>
		public Vector2i ToVector2Y()
		{
			return new Vector2i(0, Y);
		}

		/// <summary>
		/// Projects the vector onto the three dimensional XY plane
		/// </summary>
		public Vector3 ToVector3XY()
		{
			return new Vector3(X, Y, 0);
		}

		/// <summary>
		/// Projects the vector onto the three dimensional XZ plane
		/// </summary>
		public Vector3 ToVector3XZ()
		{
			return new Vector3(X, 0, Y);
		}

		/// <summary>
		/// Projects the vector onto the three dimensional YZ plane
		/// </summary>
		public Vector3 ToVector3YZ()
		{
			return new Vector3(0, X, Y);
		}

		/// <summary>
		/// Returns true if vectors lie on the same line, false otherwise
		/// </summary>
		public bool IsCollinear(Vector2i other)
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
		public Vector2i Perp()
		{
			return new Vector2i(-Y, X);
		}

		/// <summary>
		/// Returns a perp dot product of vectors
		/// </summary>
		/// <remarks>
		/// Hill, F. S. Jr. "The Pleasures of 'Perp Dot' Products."
		/// Ch. II.5 in Graphics Gems IV (Ed. P. S. Heckbert). San Diego: Academic Press, pp. 138-148, 1994
		/// </remarks>
		public static float PerpDot(Vector2i a, Vector2i b)
		{
			return a.X * b.Y - a.Y * b.X;
		}

		/// <summary>
		/// Returns a signed clockwise angle in degrees [-180, 180] between from and to
		/// </summary>
		/// <param name="from">The angle extends round from this vector</param>
		/// <param name="to">The angle extends round to this vector</param>
		public static float SignedAngle(Vector2i from, Vector2i to)
		{
			return (float)Math.Atan2(PerpDot(to, from), Vector2i.Dot(to, from)) * MathHelper.Rad2Degf;
		}

		/// <summary>
		/// Returns a clockwise angle in degrees [0, 360] between from and to
		/// </summary>
		/// <param name="from">The angle extends round from this vector</param>
		/// <param name="to">The angle extends round to this vector</param>
		public static float Angle360(Vector2i from, Vector2i to)
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
		public static Vector2i InverseLerp(Vector2i a, Vector2i b, Vector2i value)
		{
			return new Vector2i(
				 MathHelper.InverseLerp(a.X, b.X, value.X),
				 MathHelper.InverseLerp(a.Y, b.Y, value.Y));
		}

		/// <summary>
		/// Returns a new vector rotated clockwise by the specified angle
		/// </summary>
		public Vector2i RotateCW(float degrees)
		{
			float radians = degrees * MathHelper.Deg2Radf;
			float sin = (float)Math.Sin(radians);
			float cos = (float)Math.Cos(radians);
			return new Vector2i(
				 X * cos + Y * sin,
				 Y * cos - X * sin);
		}

		/// <summary>
		/// Returns a new vector rotated counterclockwise by the specified angle
		/// </summary>
		public Vector2i RotateCCW(float degrees)
		{
			float radians = degrees * MathHelper.Deg2Radf;
			float sin = (float)Math.Sin(radians);
			float cos = (float)Math.Cos(radians);
			return new Vector2i(
				 X * cos - Y * sin,
				 Y * cos + X * sin);
		}

		/// <summary>
		/// Returns a new vector rotated clockwise by 45°
		/// </summary>
		public Vector2i RotateCW45()
		{
			return new Vector2i((X + Y) * MathHelper.Sqrt05f, (Y - X) * MathHelper.Sqrt05f);
		}

		/// <summary>
		/// Returns a new vector rotated counterclockwise by 45°
		/// </summary>
		public Vector2i RotateCCW45()
		{
			return new Vector2i((X - Y) * MathHelper.Sqrt05f, (Y + X) * MathHelper.Sqrt05f);
		}

		/// <summary>
		/// Returns a new vector rotated clockwise by 90°
		/// </summary>
		public Vector2i RotateCW90()
		{
			return new Vector2i(Y, -X);
		}

		/// <summary>
		/// Returns a new vector rotated counterclockwise by 90°
		/// </summary>
		public Vector2i RotateCCW90()
		{
			return new Vector2i(-Y, X);
		}

		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("({0}, {1})", X.ToString(format, formatProvider), Y.ToString(format, formatProvider));
		}
	}
}
