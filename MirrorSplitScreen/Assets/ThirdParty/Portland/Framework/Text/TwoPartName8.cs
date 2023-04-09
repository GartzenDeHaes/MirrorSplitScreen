//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Portland.Text;

//namespace Portland.Text
//{
//	[Serializable]
//	public struct TwoPartName8 : IEquatable<TwoPartName8>
//	{
//		public AsciiId4 Topic;
//		public AsciiId4 Evnt;

//		public TwoPartName8(AsciiId4 topic, AsciiId4 evnt)
//		{
//			Topic = topic;
//			Evnt = evnt;
//		}

//		public bool Equals(TwoPartName8 mgs)
//		{
//			return Topic == mgs.Topic && Evnt == mgs.Evnt;
//		}

//		public override bool Equals(object obj)
//		{
//			if (Object.ReferenceEquals(null, obj))
//			{
//				return false;
//			}
//			if (obj is TwoPartName8 mgs)
//			{
//				return Topic == mgs.Topic && Evnt == mgs.Evnt;
//			}

//			return false;
//		}

//		public override int GetHashCode()
//		{
//			return Topic.GetHashCode() ^ Evnt.GetHashCode();
//		}

//		public override string ToString()
//		{
//			return Topic + "." + Evnt.ToString();
//		}

//		public static bool operator ==(in TwoPartName8 n1, in TwoPartName8 n2)
//		{
//			return n1.Topic == n2.Topic && n1.Evnt == n2.Evnt;
//		}

//		public static bool operator !=(in TwoPartName8 n1, in TwoPartName8 n2)
//		{
//			return n1.Topic != n2.Topic || n1.Evnt != n2.Evnt;
//		}
//	}
//}
