using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portland.Text
{
	public sealed class CsvParser
	{
		public bool IsEOF
		{
			get { return TypeIs == TokenType.EOF; }
		}

		public bool IsEOL
		{
			get { return IsEOF || TypeIs == TokenType.LF || TypeIs == TokenType.CR; }
}

		public CsvParser(string text)
{
			_text = text;

			if (String.IsNullOrWhiteSpace(text))
			{
				TypeIs = TokenType.EOF;
			}
			else
			{
				NextRow();
			}
		}

		private void CheckForComment()
		{
			if (TypeIs == TokenType.ID && Lexum[0] == '#')
			{
				while (!IsEOL)
				{
					Next();
				}

				NextRow();
			}
		}

		public string NextColumn()
		{
			string ret = String.Empty;
			if (!IsEOL && TypeIs != TokenType.COMMA)
			{
				ret = Lexum.ToString().Trim();
				Next();
			}
			if (!IsEOL)
			{
				if (TypeIs != TokenType.COMMA)
				{
					throw new Exception("Expected comma on line " + LineNumber + " pos " + _textPos);
				}
				Next();
			}

			return ret;
		}

		public bool NextRow()
		{
			bool okRow = false;
			if (TypeIs == TokenType.CR)
			{
				okRow = true;
				Next();
			}
			if (TypeIs == TokenType.LF)
			{
				okRow = true;
				Next();
			}
			if (TypeIs == TokenType.BOF)
			{
				okRow = true;
				Next();
			}
			if (IsEOF)
			{
				return false;
			}
			if (!okRow)
			{
				throw new Exception("Expected end of line on line " + LineNumber);
			}

			LineNumber++;
			CheckForComment();
			return !IsEOL;
		}

		/// <summary></summary>
		enum TokenType : short
		{
			/// <summary></summary>
			BOF,
			/// <summary></summary>
			ID,
			/// <summary></summary>
			STRING,
			/// <summary></summary>
			COMMA,
			/// <summary></summary>
			CR,
			/// <summary></summary>
			LF,
			/// <summary></summary>
			EOF
		}

		/// <summary></summary>
		public StringBuilder Lexum = new StringBuilder();
		/// <summary></summary>
		public int LineNumber;
		/// <summary></summary>
		private TokenType TypeIs;

		private enum State
		{
			START,
			ID,
			STRING
		}

		private string _text;
		private int _textPos = 0;

		/// <summary>Return false on EOF</summary>
		bool Next()
		{
			Lexum.Length = 0;

			if (TypeIs == TokenType.EOF)
			{
				return false;
			}

			TypeIs = TokenType.EOF;

			if (_textPos >= _text.Length)
			{
				return false;
			}

			State state = State.START;
			char lastch = '\0';
			char ch;

			while (_textPos < _text.Length)
			{
				ch = _text[_textPos++];

				switch (state)
				{
					case State.START:
						switch (ch)
						{
							case '\n':
								TypeIs = TokenType.LF;
								return true;
							case '\r':
								TypeIs = TokenType.CR;
								return true;
							case '"':
								state = State.STRING;
								TypeIs = TokenType.STRING;
								break;
							case ',':
								TypeIs = TokenType.COMMA;
								return true;
							default:
								if (ch < 33)
								{
									continue;
								}
								Lexum.Append(ch);
								state = State.ID;
								TypeIs = TokenType.ID;
								break;
						}

						break;
					case State.ID:
						if (ch != ',' && ch != '\n' && ch != '\r')
						{
							Lexum.Append(ch);
							break;
						}
						else
						{
							_textPos--;
							TypeIs = TokenType.ID;
							return true;
						}
					case State.STRING:
						if (ch == '"')
						{
							if (lastch == '\\')
							{
								lastch = ch;
								Lexum.Append(ch);
							}
							else
							{
								return true;
							}
						}
						else
						{
							lastch = ch;
							Lexum.Append(ch);
						}
						break;
				}
			}

			return TypeIs != TokenType.EOF;
		}

		/// <summary></summary>
		public void Dispose()
		{
			_text = null;
			Lexum = null;
		}
	}
}
