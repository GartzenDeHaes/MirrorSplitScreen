using System;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;

namespace Portland.Text
{
	// TODO: <!--#include file="filename.html"-->
	public sealed class XmlLex : IDisposable
	{
		public enum XmlLexToken
		{
			BOF,
			STRING,
			TAG,
			CLOSE,
			EQUAL,
			EOF,
			COMMENT,
			TAG_START,
			TAG_END,
			CODE_START,
			TEXT,
		}

		private readonly string _text;
		private int _textPos = 0;
		private int _lineNum = 1;

		public readonly StringBuilder Lexum = new StringBuilder();

		private enum State
		{
			START,
			ID,
			STRING_TICK,
			STRING_QUOTE,
			COMMENT,
			COMMENT2,
			COMMENT3,
			HEADER,
			OPEN,
			CLOSE,
		}

		public XmlLexToken Token;

		public bool IsEOF
		{
			get { return Token == XmlLexToken.EOF; }
		}

		public int LineNum;

		public bool SkipComments;

		public XmlLex(string txt, bool skipComments = false)
		{
			_text = txt;
			SkipComments = skipComments;

			Next();
		}

		private char NextCh()
		{
			if (_textPos >= _text.Length)
			{
				return '\0';
			}

			char ch = _text[_textPos++];
			if (ch == '\n')
			{
				_lineNum++;
			}
			return ch;
		}

		private void UnGetCh()
		{
			_textPos--;
		}

		private void SkipWs()
		{
			while (_textPos < _text.Length && Char.IsWhiteSpace(_text[_textPos]))
			{
				if (_text[_textPos] == '\n')
				{
					_lineNum++;
				}
				_textPos++;
			}
		}

		public bool Match(XmlLexToken token)
		{
			if (Token != token)
			{
				throw new Exception("Expected " + token + " found " + Token + " on line " + LineNum);
			}

			return Next();
		}

		public bool Match(string stringToken)
		{
			if (Token != XmlLexToken.STRING)
			{
				throw new Exception("Expected " + stringToken + " found " + Token + " on line " + LineNum);
			}

			if (! Lexum.IsEqualTo(stringToken))
			{
				throw new Exception("Expected " + stringToken + " found " + Lexum.ToString() + " on line " + LineNum);
			}

			return Next();
		}

		/// <summary>
		/// Match <name
		/// </summary>
		public bool MatchTagStart(string name)
		{
			if (Token != XmlLexToken.TAG_START)
			{
				throw new Exception("Expected TAG_START found " + Token + " on line " + LineNum);
			}

			if (!Lexum.IsEqualTo(name))
			{
				throw new Exception("Expected " + name + " found " + Lexum.ToString() + " on line " + LineNum);
			}

			return Next();
		}

		/// <summary>
		/// Match <name>
		/// </summary>
		public bool MatchTag(string name)
		{
			if (Token != XmlLexToken.TAG)
			{
				throw new Exception("Expected TAG found " + Token + " on line " + LineNum);
			}

			if (!Lexum.IsEqualTo(name))
			{
				throw new Exception("Expected <" + name + "> found " + Lexum.ToString() + " on line " + LineNum);
			}

			return Next();
		}

		/// <summary>
		/// Match </name>
		/// </summary>
		public bool MatchTagClose(string name)
		{
			if (!Lexum.IsEqualTo(name))
			{
				throw new Exception("Expected </" + name + "> found " + Lexum.ToString() + " on line " + LineNum);
			}

			return Match(XmlLexToken.CLOSE);
		}

		/// <summary>
		/// Match />
		/// </summary>
		public bool MatchTagClose()
		{
			if (Lexum.Length != 0)
			{
				throw new Exception("Expected '/>' found " + Lexum.ToString() + " on line " + LineNum);
			}

			return Match(XmlLexToken.CLOSE);
		}

		/// <summary>
		/// Match >
		/// </summary>
		public bool MatchTagEnd()
		{
			return Match(XmlLexToken.TAG_END);
		}

		public string MatchProperty(string name)
		{
			Match(name);
			Match(XmlLexToken.EQUAL);
			string retVal = Lexum.ToString();

			if (Token != XmlLexToken.TAG_END)
			{
				Match(XmlLexToken.STRING);
			}

			return retVal;
		}

		/// <summary>
		/// Read until <
		/// </summary>
		public void NextText()
		{
			Token = XmlLexToken.TEXT;

			while (_textPos < _text.Length)
			{
				char ch = NextCh();
				if (ch == '<')
				{
					UnGetCh();
					return;
				}
				Lexum.Append(ch);
			}
		}

		public bool Next()
		{
			Lexum.Length = 0;

			if (_text.Length == 0)
			{
				Token = XmlLexToken.EOF;
				return false;
			}

			SkipWs();

			State state = State.START;

			while (_textPos < _text.Length)
			{
				char ch = NextCh();

				switch (state)
				{
					case State.START:
						switch (ch)
						{
							case '\'':
								state = State.STRING_TICK;
								Token = XmlLexToken.STRING;
								break;
							case '"':
								state = State.STRING_QUOTE;
								Token = XmlLexToken.STRING;
								break;
							case '\n':
								_lineNum++;
								continue;
							case '\0':
								Token = XmlLexToken.EOF;
								return false;
							case '=':
								Token = XmlLexToken.EQUAL;
								return true;
							case '<':
								ch = NextCh();
								if (ch == '!')
								{
									if (NextCh() != '-')
									{
										throw new Exception("Expected comment on line " + _lineNum);
									}
									if (NextCh() != '-')
									{
										throw new Exception("Expected comment on line " + _lineNum);
									}
									state = State.COMMENT;
									Token = XmlLexToken.COMMENT;
									break;
								}
								else if (ch == '?')
								{
									state = State.HEADER;
									break;
								}
								else if (ch == '/')
								{
									state = State.CLOSE;
									Token = XmlLexToken.CLOSE;
									break;
								}
								else if (ch == '%')
								{
									Token = XmlLexToken.CODE_START;
									return true;
								}
								else
								{
									Lexum.Append(ch);
									state = State.OPEN;
									Token = XmlLexToken.TAG;
									break;
								}
							case '>':
								Token = XmlLexToken.CLOSE;
								return true;
							case '/':
								if (NextCh() == '>')
								{
									Token = XmlLexToken.TAG_END;
									return true;
								}
								throw new Exception("Expected '/>' on line " + _lineNum);
						}

						if (state == State.START)
						{
							if (Char.IsControl(ch) || Char.IsWhiteSpace(ch))
							{
								continue;
							}

							Lexum.Append(ch);

							state = State.ID;
							Token = XmlLexToken.STRING;
						}

						break;
					case State.ID:
						if (Char.IsLetterOrDigit(ch) || ch == '_' || ch == '-' || ch == '.')
						{
							Lexum.Append(ch);
						}
						else
						{
							_textPos--;
							return true;
						}
						break;
					case State.STRING_QUOTE:
						if (ch == '\0' || ch == '"')
						{
							return true;
						}
						else
						{
							Lexum.Append(ch);
						}
						break;
					case State.STRING_TICK:
						if (ch == '\0' || ch == '\'')
						{
							return true;
						}
						else
						{
							Lexum.Append(ch);
						}
						break;
					case State.COMMENT:
						if (ch == '-')
						{
							state = State.COMMENT2;
						}
						else
						{
							Lexum.Append(ch);
						}
						break;
					case State.COMMENT2:
						if (ch == '-')
						{
							state = State.COMMENT3;
						}
						else
						{
							Lexum.Append('-');
							Lexum.Append(ch);
							state = State.COMMENT;
						}
						break;
					case State.COMMENT3:
						if (ch == '>')
						{
							if (SkipComments)
							{
								Lexum.Length = 0;
								state = State.START;
							}
							else
							{
								return true;
							}
						}
						else
						{
							Lexum.Append('-');
							Lexum.Append('-');
							Lexum.Append(ch);
							state = State.COMMENT;
						}
						break;
					case State.HEADER:
						if (ch == '?')
						{
							NextCh();
							state = State.START;
						}
						break;
					case State.OPEN:
						if (Char.IsLetterOrDigit(ch) || ch == '_' || ch == '-' || ch == '.')
						{
							Lexum.Append(ch);
						}
						else if (ch == '>')
						{
							Token = XmlLexToken.TAG;
							return true;
						}
						else
						{
							Token = XmlLexToken.TAG_START;
							return true;
						}
						break;
					case State.CLOSE:
						if (ch == '>')
						{
							return true;
						}
						else
						{
							Lexum.Append(ch);
						}
						break;
				}
			}

			_textPos = 0;
			Token = XmlLexToken.EOF;

			return Lexum.Length > 0;
		}

		public void Dispose()
		{
		}
	}
}
