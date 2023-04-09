using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portland.Collections
{
	public sealed class Association<TLEFT, TRIGHT> : IEquatable<Association<TLEFT, TRIGHT>>
	{
		public TLEFT A;
		public TRIGHT B;

		public TLEFT Key { get { return A; } }
		public TRIGHT Value { get { return B; } }

		public Association()
		{
		}

		public Association(TLEFT a, TRIGHT b)
		{
			A = a;
			B = b;
		}

		public override bool Equals(object obj)
		{
			if (obj is Association<TLEFT, TRIGHT> asc)
			{
				return A.Equals(asc.A) && B.Equals(asc.B);
			}

			return false;
		}

		public bool Equals(Association<TLEFT, TRIGHT> other)
		{
			return A.Equals(other.A) && B.Equals(other.B);
		}

		public override int GetHashCode()
		{
			return A.GetHashCode() ^ B.GetHashCode();
		}

		public override string ToString()
		{
			return A.ToString() + ":" + B.ToString();
		}
	}
}
