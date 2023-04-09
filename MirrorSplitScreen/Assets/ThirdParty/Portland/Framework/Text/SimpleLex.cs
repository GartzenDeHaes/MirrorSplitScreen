using System;
using System.Collections.Generic;
using System.Text;

namespace Portland.Text
{
	public sealed class ParseException : Exception
	{
		public ParseException(int line)
		: base($"Lex exception on line {line}.")
		{
		}

		public ParseException(string msg)
		: base(msg)
		{
		}
	}

	/// <summary>
	/// A simple lexar that handled ID's, quoted strings, ints, and CR/LF.
	/// </summary>
	public sealed class SimpleLex : IDisposable
	{
		/// <summary></summary>
		public enum TokenType : short
		{
			/// <summary></summary>
			BOF,
			/// <summary></summary>
			ID,
			/// <summary></summary>
			STRING,
			/// <summary></summary>
			INTEGER,
			/// <summary></summary>
			FLOAT,
			/// <summary></summary>
			PUNCT,
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
		public int LineNum;
		/// <summary></summary>
		public TokenType Token;

		public bool IsEOF { get { return Token == SimpleLex.TokenType.EOF; } }
		public bool IsEOL { get { return Token == SimpleLex.TokenType.EOF || Token == SimpleLex.TokenType.CR || Token == SimpleLex.TokenType.LF; } }

		private enum State
		{
			START,
			ID,
			STRING,
			NUM,
			FLOAT
		}

		enum CharClass
		{
			WHITESPACE = 0,
			PUNCT = 1,
			NUM = 2,
			ALPHA = 3
		}

		public bool AllowDashInID;

		private string _text;
		private int _textPos = 0;

		/// <summary></summary>
		public SimpleLex(string txt)
		{
			_text = txt;

			if (String.IsNullOrWhiteSpace(txt))
			{
				Token = TokenType.EOF;
			}
		}

		public void Reset(string txt)
		{
			_text = txt;
			_textPos = 0;
			LineNum = 0;
			Token = TokenType.BOF;
		}

		public bool Match(TokenType tt)
		{
			if (tt != Token)
			{
				throw new ParseException($"Expected {tt} on line {LineNum}");
			}

			return Next();
		}

		public bool Match(string lexum)
		{
			if (! StringHelper.AreEqual(Lexum, lexum))
			{
				throw new ParseException($"Expected '{lexum}' on line {LineNum}");
			}

			return Next();
		}

		public bool MatchIgnoreCase(string lexum)
		{
			if (!StringHelper.AreEqualNoCase(Lexum, lexum))
			{
				throw new ParseException($"Expected '{lexum}' on line {LineNum}");
			}

			return Next();
		}

		public bool MatchOptionalIgnoreCase(string lexum)
		{
			if (!StringHelper.AreEqualNoCase(Lexum, lexum))
			{
				return !IsEOF;
			}

			return Next();
		}

		public void ReadToEol()
		{
			ReadToEolOrChar('\n');
		}

		public void ReadToEolOrChar(char stop)
		{
			Token = TokenType.STRING;

			char ch;
			while (_textPos < _text.Length)
			{
				ch = _text[_textPos];
				if (ch == stop || ch == '\n' || ch == '\r')
				{
					break;
				}
				Lexum.Append(ch);
				_textPos++;
			}
		}

		public void NextLine()
		{
			while (Token == TokenType.CR || Token == TokenType.LF) 
			{
				Next();
			}
			//if (Token == TokenType.CR || Token == TokenType.LF)
			//{
			//	Next();
			//}
		}

		public void SkipWhitespace()
		{
			if (IsEOL)
			{
				Lexum.Length = 0;
				char ch = _text[_textPos];

				while (Char.IsWhiteSpace(ch) && _textPos < _text.Length)
				{
					ch = _text[_textPos++];
				}

				if (_textPos < _text.Length)
				{
					_textPos--;
				}

				Next();
			}
		}

		public void SkipToNextLine()
		{
			while(! IsEOL && !IsEOF)
			{
				Next();
			}
			NextLine();
		}

		/// <summary>Return false on EOF</summary>
		public bool Next()
		{
			Lexum.Length = 0;

			if (Token == TokenType.EOF)
			{
				return false;
			}

			Token = TokenType.EOF;

			if (_textPos >= _text.Length)
			{
				return false;
			}

			State state = State.START;
			char delim = '\0';
			char lastch = '\0';
			char ch;
			int chcls;

			while (_textPos < _text.Length)
			{
				ch = _text[_textPos++];

				//			// 0 whitespace
				//			// 1 punct
				//			// 2 num
				//			// 3 alpha
				chcls = ((ch > 128) ? 3 : (ch - 122) > 0 ? 1 : ((ch - 96) > 0 ? 3 : ((ch - 90) > 0 ? 1 : ((ch - 64) > 0 ? 3 : ((ch - 57) > 0 ? 1 : ((ch - 47) > 0 ? 2 : ((ch - 32) > 0 ? 1 : 0)))))));

				switch (state)
				{
					case State.START:
						switch ((CharClass)chcls)
						{
							case CharClass.WHITESPACE:
								if (ch == '\n')
								{
									Token = TokenType.LF;
									LineNum++;
									return true;
								}
								if (ch == '\r')
								{
									Token = TokenType.CR;
									return true;
								}
								break;
							case CharClass.PUNCT:
								if (ch == '\'')
								{
									state = State.STRING;
									Token = TokenType.STRING;
									delim = ch;
									break; ;
								}
								if (ch == '"')
								{
									state = State.STRING;
									Token = TokenType.STRING;
									delim = ch;
									break;
								}
								if (ch == '_')
								{
									Lexum.Append(ch);
									state = State.ID;
									break;
								}
								if (ch == '-')
								{
									Lexum.Append(ch);
									state = State.NUM;
									break;
								}
								Lexum.Append(ch);
								Token = TokenType.PUNCT;
								return true;
							case CharClass.NUM:
								state = State.NUM;
								Lexum.Append(ch);
								Token = TokenType.INTEGER;
								break;
							case CharClass.ALPHA:
								Lexum.Append(ch);
								state = State.ID;
								Token = TokenType.ID;
								break;
							default:
								throw new ParseException("Internal error in SimpleLex " + _text);
						}

						break;
					case State.ID:
						if (chcls > 1 || ch == '_' || (ch == '-' && AllowDashInID))
						{
							Lexum.Append(ch);
							break;
						}
						else
						{
							_textPos--;
							Token = TokenType.ID;
							return true;
						}
					case State.NUM:
						if ((CharClass)chcls == CharClass.NUM)
						{
							Lexum.Append(ch);
							break;
						}
						else if (ch == '.')
						{
							Lexum.Append(ch);
							state = State.FLOAT;
							Token = TokenType.FLOAT;
							break;
						}
						else if (Lexum.Length == 1 && Lexum[0] == '-')
						{
							_textPos--;
							Token = TokenType.PUNCT;
							return true;
						}
						else
						{
							_textPos--;
							Token = TokenType.INTEGER;
							return true;
						}
					case State.FLOAT:
						if ((CharClass)chcls == CharClass.NUM)
						{
							Lexum.Append(ch);
							break;
						}
						else
						{
							_textPos--;
							return true;
						}
					case State.STRING:
						if (ch == delim)
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

			return Token != TokenType.EOF;
		}

		/// <summary></summary>
		public void Dispose()
		{
			_text = null;
			Lexum = null;
		}
	}  
	
	///// <summary>
		///// A simple lexar that handled ID's, quoted strings, ints, and CR/LF.
		///// </summary>
		//public class SimpleLex : IDisposable
		//{
		//	/// <summary></summary>
		//	public enum TokenType : short
		//	{
		//		/// <summary></summary>
		//		BOF,
		//		/// <summary></summary>
		//		ID,
		//		/// <summary></summary>
		//		STRING,
		//		/// <summary></summary>
		//		INTEGER,
		//		/// <summary></summary>
		//		PUNCT,
		//		/// <summary></summary>
		//		CR,
		//		/// <summary></summary>
		//		LF,
		//		/// <summary></summary>
		//		EOF
		//	}

	//	/// <summary></summary>
	//	public StringBuilder Lexum = new StringBuilder();
	//	/// <summary></summary>
	//	public short LineNum;
	//	/// <summary></summary>
	//	public TokenType TypeIs;

	//	private enum State
	//	{
	//		START,
	//		ID,
	//		STRING,
	//		NUM,
	//	}

	//	private string _text;
	//	private int _textPos = 0;

	//	/// <summary></summary>
	//	public SimpleLex(string txt)
	//	{
	//		_text = txt;

	//		if (String.IsNullOrWhiteSpace(txt))
	//		{
	//			TypeIs = TokenType.EOF;
	//		}
	//	}

	//	/// <summary>Return false on EOF</summary>
	//	public bool Next()
	//	{
	//		Lexum.Length = 0;

	//		if (TypeIs == TokenType.EOF)
	//		{
	//			return false;
	//		}

	//		TypeIs = TokenType.EOF;

	//		if (_textPos >= _text.Length)
	//		{
	//			return false;
	//		}

	//		State state = State.START;
	//		TokenType delimToken = TokenType.LF;
	//		char delim = '\0';
	//		char lastch = '\0';
	//		char ch;
	//		int chcls;

	//		while (_textPos < _text.Length)
	//		{
	//			ch = _text[_textPos++];
	//			//			// 0 whitespace
	//			//			// 1 punct
	//			//			// 2 num
	//			//			// 3 alpha

	//			chcls = ((ch > 128) ? 3 : (ch - 122) > 0 ? 1 : ((ch - 96) > 0 ? 3 : ((ch - 90) > 0 ? 1 : ((ch - 64) > 0 ? 3 : ((ch - 57) > 0 ? 1 : ((ch - 47) > 0 ? 2 : ((ch - 32) > 0 ? 1 : 0)))))));

	//			switch (state)
	//			{
	//				case State.START:
	//					if (/*Char.IsDigit(ch)*/ chcls == 2)
	//					{
	//						state = State.NUM;
	//						Lexum.Append(ch);
	//						break;
	//					}
	//					switch (ch)
	//					{
	//						case '\'':
	//							state = State.STRING;
	//							delimToken = TokenType.STRING;
	//							delim = ch;
	//							continue;
	//						case '"':
	//							state = State.STRING;
	//							delimToken = TokenType.STRING;
	//							delim = ch;
	//							continue;
	//						case '\n':
	//							TypeIs = TokenType.CR;
	//							LineNum++;
	//							return true;
	//						case '\r':
	//							TypeIs = TokenType.LF;
	//							return true;
	//						case '\0':
	//							TypeIs = TokenType.EOF;
	//							return true;
	//						case '_':
	//							Lexum.Append(ch);
	//							state = State.ID;
	//							continue;
	//					}
	//					if (StringHelper.IsProgrammingLanguagePunct(ch))
	//					{
	//						Lexum.Append(ch);
	//						TypeIs = TokenType.PUNCT;
	//						return true;
	//					}
	//					if (Char.IsControl(ch) || Char.IsWhiteSpace(ch))
	//					{
	//						continue;
	//					}
	//					if (state == State.START)
	//					{
	//						Lexum.Append(ch);
	//						state = State.ID;
	//					}

	//					break;
	//				case State.ID:
	//					if (Char.IsLetterOrDigit(ch) || ch == '_')
	//					{
	//						Lexum.Append(ch);
	//						break;
	//					}
	//					else
	//					{
	//						_textPos--;
	//						TypeIs = TokenType.ID;
	//						return true;
	//					}
	//				case State.NUM:
	//					if (Char.IsDigit(ch))
	//					{
	//						Lexum.Append(ch);
	//						break;
	//					}
	//					else
	//					{
	//						_textPos--;
	//						TypeIs = TokenType.INTEGER;
	//						return true;
	//					}
	//				case State.STRING:
	//					if (ch == '\0' || ch == delim)
	//					{
	//						if (lastch == '\\')
	//						{
	//							lastch = ch;
	//							Lexum.Append(ch);
	//						}
	//						else
	//						{
	//							TypeIs = delimToken;
	//							return true;
	//						}
	//					}
	//					else
	//					{
	//						lastch = ch;
	//						Lexum.Append(ch);
	//					}
	//					break;
	//			}
	//		}

	//		switch (state)
	//		{
	//			case State.ID:
	//				TypeIs = TokenType.ID;
	//				break;
	//			case State.NUM:
	//				TypeIs = TokenType.INTEGER;
	//				break;
	//			case State.STRING:
	//				TypeIs = delimToken;
	//				break;
	//		}

	//		return TypeIs != TokenType.EOF;
	//	}

	//	/// <summary></summary>
	//	public void Dispose()
	//	{
	//		_text = null;
	//		Lexum = null;
	//	}
	//}
	///// <summary>
	///// A simple lexar that handled ID's, quoted strings, ints, and CR/LF.
	///// </summary>
	//public class SimpleLex : IDisposable
	//{
	//	/// <summary></summary>
	//	public enum TokenType : short
	//	{
	//		/// <summary></summary>
	//		ID,
	//		/// <summary></summary>
	//		STRING,
	//		/// <summary></summary>
	//		INTEGER,
	//		/// <summary></summary>
	//		PUNCT,
	//		/// <summary></summary>
	//		CR,
	//		/// <summary></summary>
	//		LF,
	//		/// <summary></summary>
	//		EOF
	//	}

	//	/// <summary>
	//	/// A token, but you knew this already.
	//	/// </summary>
	//	public struct Token
	//	{
	//		/// <summary></summary>
	//		public string Lexum;
	//		/// <summary></summary>
	//		public short LineNum;
	//		/// <summary></summary>
	//		public TokenType TypeIs;

	//		/// <summary></summary>
	//		public static Token From(char ch, short line, TokenType tt)
	//		{
	//			Token t;
	//			t.Lexum = ch.ToString();
	//			t.LineNum = line;
	//			t.TypeIs = tt;
	//			return t;
	//		}

	//		/// <summary></summary>
	//		public static Token From(StringBuilder buf, short line, TokenType tt)
	//		{
	//			Token t;
	//			t.Lexum = buf.ToString();
	//			t.LineNum = line;
	//			t.TypeIs = tt;
	//			return t;
	//		}

	//		/// <summary></summary>
	//		public static Token From(short line, TokenType tt)
	//		{
	//			Token t;
	//			t.Lexum = String.Empty;
	//			t.LineNum = line;
	//			t.TypeIs = tt;
	//			return t;
	//		}
	//	}

	//	private enum State
	//	{
	//		START,
	//		ID,
	//		STRING,
	//		NUM,
	//	}

	//	private Token[] _tokens = new Token[8];
	//	private int _tokenPos;

	//	/// <summary></summary>
	//	public Token Current;
	//	private Token _unget;

	//	private string _text;
	//	private int _textPos = 0;
	//	private short lineNo = 1;
	//	private StringBuilder _lexum = new StringBuilder();

	//	/// <summary></summary>
	//	public SimpleLex(string txt)
	//	{
	//		_text = txt;

	//		if (String.IsNullOrWhiteSpace(txt))
	//		{
	//			_tokens[0] = Token.From(lineNo, TokenType.EOF);
	//		}
	//		else
	//		{
	//			Fill();
	//		}

	//		Current = _tokens[0];
	//		_unget.TypeIs = TokenType.EOF;
	//	}

	//	/// <summary>Return false on EOF</summary>
	//	public bool Next()
	//	{
	//		if (_unget.TypeIs != TokenType.EOF)
	//		{
	//			Current = _unget;
	//			_unget.TypeIs = TokenType.EOF;
	//			return true;
	//		}

	//		Current = _tokens[_tokenPos];

	//		if (Current.TypeIs == TokenType.EOF)
	//		{
	//			return false;
	//		}

	//		_tokenPos++;

	//		if (_tokenPos >= _tokens.Length)
	//		{
	//			_tokenPos = 0;
	//			Fill();
	//		}

	//		return true;
	//	}

	//	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	//	public void UnGet()
	//	{
	//		_unget = Current;
	//	}

	//	private void Fill()
	//	{
	//		int _pos = 0;

	//		for (int x = 0; x < _tokens.Length && _textPos < _text.Length; x++)
	//		{
	//			_lexum.Length = 0;

	//			ScanOne(ref _pos);
	//		}

	//		if (_textPos >= _text.Length && _pos < _tokens.Length)
	//		{
	//			_tokens[_pos] = Token.From(lineNo, TokenType.EOF);
	//		}
	//	}

	//	private void ScanOne(ref int _pos)
	//	{
	//		State state = State.START;
	//		TokenType delimToken = TokenType.LF;
	//		char delim = '\0';
	//		char lastch = '\0';

	//		while (_textPos < _text.Length)
	//		{
	//			char ch = (_textPos >= _text.Length) ? '\0' : _text[_textPos++];

	//			switch (state)
	//			{
	//				case State.START:
	//					if (Char.IsDigit(ch))
	//					{
	//						state = State.NUM;
	//						_lexum.Append(ch);
	//						break;
	//					}
	//					switch (ch)
	//					{
	//						case '\'':
	//							state = State.STRING;
	//							delimToken = TokenType.STRING;
	//							delim = ch;
	//							continue;
	//						case '"':
	//							state = State.STRING;
	//							delimToken = TokenType.STRING;
	//							delim = ch;
	//							continue;
	//						case '\n':
	//							_tokens[_pos++] = Token.From(lineNo, TokenType.CR);
	//							return;
	//						case '\r':
	//							_tokens[_pos++] = Token.From(lineNo, TokenType.LF);
	//							return;
	//						case '\0':
	//							_tokens[_pos++] = Token.From(lineNo, TokenType.EOF);
	//							return;
	//						case '_':
	//							_lexum.Append(ch);
	//							state = State.ID;
	//							continue;
	//					}
	//					if (StringHelper.IsProgrammingLanguagePunct(ch))
	//					{
	//						_tokens[_pos++] = Token.From(ch, lineNo, TokenType.PUNCT);
	//						return;
	//					}
	//					if (Char.IsControl(ch) || Char.IsWhiteSpace(ch))
	//					{
	//						continue;
	//					}
	//					if (state == State.START)
	//					{
	//						_lexum.Append(ch);
	//						state = State.ID;
	//					}

	//					break;
	//				case State.ID:
	//					if (Char.IsLetterOrDigit(ch) || ch == '_')
	//					{
	//						_lexum.Append(ch);
	//						break;
	//					}
	//					else
	//					{
	//						_textPos--;
	//						_tokens[_pos++] = Token.From(_lexum, lineNo, TokenType.ID);
	//						return;
	//					}
	//				case State.NUM:
	//					if (Char.IsDigit(ch))
	//					{
	//						_lexum.Append(ch);
	//						break;
	//					}
	//					else
	//					{
	//						_textPos--;
	//						_tokens[_pos++] = Token.From(_lexum, lineNo, TokenType.INTEGER);
	//						return;
	//					}
	//				case State.STRING:
	//					if (ch == '\0' || ch == delim)
	//					{
	//						if (lastch == '\\')
	//						{
	//							lastch = ch;
	//							_lexum.Append(ch);
	//						}
	//						else
	//						{
	//							_tokens[_pos++] = Token.From(_lexum, lineNo, delimToken);
	//							return;
	//						}
	//					}
	//					else
	//					{
	//						lastch = ch;
	//						_lexum.Append(ch);
	//					}
	//					break;
	//			}
	//		}

	//		switch (state)
	//		{
	//			case State.ID:
	//				_tokens[_pos++] = Token.From(_lexum, lineNo, TokenType.ID);
	//				break;
	//			case State.NUM:
	//				_tokens[_pos++] = Token.From(_lexum, lineNo, TokenType.INTEGER);
	//				break;
	//			case State.STRING:
	//				_tokens[_pos++] = Token.From(_lexum, lineNo, delimToken);
	//				break;
	//		}
	//	}

	//	/// <summary></summary>
	//	public void Dispose()
	//	{
	//		_text = null;
	//		_tokens = null;
	//		_lexum = null;
	//	}
	//}
}
