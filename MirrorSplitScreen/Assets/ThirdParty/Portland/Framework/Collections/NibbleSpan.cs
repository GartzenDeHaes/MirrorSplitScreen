using System;
using System.Collections.ObjectModel;

namespace Portland.Collections
{
	/// <summary>
	/// Represents a slice of an array of 4-bit values.
	/// </summary>
	public sealed class NibbleSpan
	{
		/// <summary>
		/// The data in the nibble array. Each byte contains
		/// two nibbles, stored in big-endian.
		/// </summary>
		public byte[] Data { get; private set; }
		public int Offset { get; private set; }
		public int Length { get; private set; }

		public NibbleSpan(byte[] data, int offset, int length)
		{
			Data = data;
			Offset = offset;
			Length = length;
		}

		/// <summary>
		/// Gets or sets a nibble at the given index.
		/// </summary>
		public byte this[int index]
		{
			get { return (byte)(Data[Offset + index / 2] >> (index % 2 * 4) & 0xF); }
			set
			{
				value &= 0xF;
				Data[Offset + index / 2] &= (byte)(~(0xF << (index % 2 * 4)));
				Data[Offset + index / 2] |= (byte)(value << (index % 2 * 4));
			}
		}

		public byte[] ToArray()
		{
			byte[] array = new byte[Length];
			Buffer.BlockCopy(Data, Offset, array, 0, Length);
			return array;
		}
	}

	public class ReadOnlyNibbleArray
	{
		private NibbleSpan NibbleArray { get; set; }

		public ReadOnlyNibbleArray(NibbleSpan array)
		{
			NibbleArray = array;
		}

		public byte this[int index]
		{
			get { return NibbleArray[index]; }
		}

		public ReadOnlyCollection<byte> Data
		{
			get { return Array.AsReadOnly(NibbleArray.Data); }
		}
	}
}