using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portland.Text
{
	/// <summary>
	/// Simple text table formater
	/// </summary>
	public class TableFormatter
	{
		private string _title;
		private int _numCols;
		private string[] _headers;
		private int _width;
		private int[] _colWidths;

		private List<string[]> _rows = new List<string[]>();

		/// <summary></summary>
		public TableFormatter(string title, int numCols, int widthInChars = 80)
		{
			_title = title;
			_numCols = numCols;
			_width = widthInChars;
			_headers = new string[numCols];
			_colWidths = new int[numCols];

			int sum = 0;
			for (int x = 0; x < _numCols; x++)
			{
				_colWidths[x] = widthInChars / _numCols;
				sum += _colWidths[x];
			}

			if (sum != widthInChars)
			{
				_colWidths[_colWidths.Length - 1]++;
			}
		}

		/// <summary></summary>
		public void SetHeader(int col, string txt)
		{
			_headers[col] = txt;
		}

		/// <summary></summary>
		public void AddRow()
		{
			_rows.Add(new string[_numCols]);
		}

		/// <summary></summary>
		public void SetCol(int num, string txt)
		{
			_rows[_rows.Count - 1][num] = txt;
		}

		/// <summary></summary>
		public override string ToString()
		{
			StringBuilder buf = new StringBuilder(_width * 2 * _rows.Count + _rows.Count);
			char[] cbuf = new char[_width];

			buf.Append('+');
			Write(buf, '-', _width - 2);
			buf.Append('+');
			buf.Append('\n');

			if (_title.Length > 0)
			{
				buf.Append('|');
				PadCenter(buf, _title, _width - 2);
				buf.Append('|');
				buf.Append('\n');

				buf.Append('+');
				Write(buf, '-', _width - 2);
				buf.Append('+');
				buf.Append('\n');
			}

			SetTo(cbuf, ' ', _width);

			int start = 0;

			for (int x = 0; x < _headers.Length; x++)
			{
				int padpos = (_colWidths[x] / 2) - (_headers[x].Length / 2);
				SetAt(cbuf, _headers[x], padpos + start);
				cbuf[start] = '|';
				start += _colWidths[x];
			}
			cbuf[_width - 1] = '|';
			buf.Append(cbuf);
			buf.Append('\n');

			buf.Append('+');
			Write(buf, '-', _width - 2);
			buf.Append('+');
			buf.Append('\n');

			for (int rowNum = 0; rowNum < _rows.Count; rowNum++)
			{
				var row = _rows[rowNum];
				start = 0;

				SetTo(cbuf, ' ', _width);

				for (int x = 0; x < row.Length; x++)
				{
					SetAt(cbuf, row[x], start + 1);
					cbuf[start] = '|';
					start += _colWidths[x];
				}
				cbuf[_width - 1] = '|';
				buf.Append(cbuf);
				buf.Append('\n');
				buf.Append('+');
				Write(buf, '-', _width - 2);
				buf.Append('+');
				buf.Append('\n');
			}

			return buf.ToString();
		}

		private void SetTo(char[] buf, char ch, int len)
		{
			for (int x = 0; x < len; x++)
			{
				buf[x] = ch;
			}
		}

		private void SetAt(char[] buf, string txt, int pos)
		{
			for (int x = 0; x < txt.Length && x + pos < buf.Length; x++)
			{
				buf[x + pos] = txt[x];
			}
		}

		private void PadCenter(StringBuilder buf, string txt, int len)
		{
			int totalspace = len - txt.Length;
			if (totalspace == 0)
			{
				buf.Append(txt);
				return;
			}
			if (totalspace < 0)
			{
				buf.Append(txt.Substring(0, len));
				return;
			}

			int left = totalspace / 2;
			int right = len - (left + txt.Length);

			Write(buf, ' ', left);
			buf.Append(txt);
			Write(buf, ' ', right);
		}

		private void Write(StringBuilder buf, char ch, int len)
		{
			for (int x = 0; x < len; x++)
			{
				buf.Append(ch);
			}
		}
	}
}
