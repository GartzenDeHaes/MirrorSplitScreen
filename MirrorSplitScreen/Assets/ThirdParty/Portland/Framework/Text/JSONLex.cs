using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Portland.Text
{
	/// <summary></summary>
	public enum JSONLexToken
	{
		/// <summary></summary>
		BOF,
		/// <summary></summary>
		STRING,
		/// <summary></summary>
		LBRACE,
		/// <summary></summary>
		RBRACE,
		/// <summary></summary>
		LBRACK,
		/// <summary></summary>
		RBRACK,
		/// <summary></summary>
		COLON,
		/// <summary></summary>
		COMMA,
		/// <summary></summary>
		EOF
	}

	/// <summary>
	/// Simple lex for JSON
	/// </summary>
	public class JSONLex : IDisposable
	{
		private string _text;
		private int _textPos = 0;

		private StringBuilder _lexum = new StringBuilder();

		private enum State
		{
			START,
			ID,
			STRING_TICK,
			STRING_QUOTE
		}

		/// <summary></summary>
		public string Lexum
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return _lexum.ToString(); }
		}

		/// <summary></summary>
		public JSONLexToken Token
		{
			get; private set;
		}

		/// <summary></summary>
		public JSONLex(string txt)
		{
			_text = txt;
		}

		/// <summary></summary>
		public void Dispose()
		{
			_text = null;
			_lexum.Clear();
			_lexum = null;
		}

		/// <summary></summary>
		public void Match(JSONLexToken token)
		{
			if (Token != token)
			{
				throw new Exception("JSON parse error");
			}

			Next();
		}

		private char NextCh()
		{
			if (_textPos >= _text.Length)
			{
				return '\0';
			}

			return _text[_textPos++];
		}

		private void SkipWs()
		{
			while (_textPos < _text.Length && Char.IsWhiteSpace(_text[_textPos]))
			{
				_textPos++;
			}
		}

		/// <summary></summary>
		public bool Next()
		{
			_lexum.Length = 0;

			if (_text.Length == 0)
			{
				Token = JSONLexToken.EOF;
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
							case '[':
								Token = JSONLexToken.LBRACK;
								return true;
							case ']':
								Token = JSONLexToken.RBRACK;
								return true;
							case '\'':
								state = State.STRING_TICK;
								Token = JSONLexToken.STRING;
								break;
							case '"':
								state = State.STRING_QUOTE;
								Token = JSONLexToken.STRING;
								break;
							case '\n':
								continue;
							case '\0':
								Token = JSONLexToken.EOF;
								return false;
							case ',':
								Token = JSONLexToken.COMMA;
								return true;
							case '{':
								Token = JSONLexToken.LBRACE;
								return true;
							case '}':
								Token = JSONLexToken.RBRACE;
								return true;
							case ':':
								Token = JSONLexToken.COLON;
								return true;
						}

						if (Char.IsControl(ch) || Char.IsWhiteSpace(ch))
						{
							continue;
						}

						if (state == State.START)
						{
							_lexum.Append(ch);

							state = State.ID;
							Token = JSONLexToken.STRING;
						}

						break;
					case State.ID:
						if (Char.IsLetterOrDigit(ch) || ch == '_' || ch == '-')
						{
							_lexum.Append(ch);
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
							_lexum.Append(ch);
						}
						break;
					case State.STRING_TICK:
						if (ch == '\0' || ch == '\'')
						{
							return true;
						}
						else
						{
							_lexum.Append(ch);
						}
						break;
				}
			}

			_textPos = 0;
			Token = JSONLexToken.EOF;

			return _lexum.Length > 0;
		}
	}
}
