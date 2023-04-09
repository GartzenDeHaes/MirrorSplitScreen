using System;
using System.Linq;

namespace Portland.Text
{
	public class StringTokenizer
	{
		public StringTokenizer(string s)
		{
			_s = s;
			_position = 0;
		}

		private readonly string _s;
		private int _position;

		bool IsSubstringEqual(in string s, in int position, string token)
		{
			int i;
			for (i = 0; i < s.Length && i < token.Length; i++)
			{
				if (s[i + position] != token[i])
				{
					return false;
				}
			}

			return i == token.Length;
		}

		public bool TryRead(string token)
		{
			//if (_position + token.Length > _s.Length || _s.Substring(_position, token.Length) != token)
			if (_position + token.Length > _s.Length || !IsSubstringEqual(_s, _position, token))
			{
				return false;
			}

			_position += token.Length;

			return true;
		}

		public void Match(string token)
		{
			if (!TryRead(token))
			{
				throw new InvalidOperationException($"String '{_s}' at position {_position} does not contain '{token}'!");
			}
		}

		public char ReadChar()
		{
			if (_position >= _s.Length)
			{
				throw new InvalidOperationException("String read to end already!");
			}

			return _s[_position++];
		}

		public char PeekChar()
		{
			if (_position >= _s.Length)
			{
				throw new InvalidOperationException("String read to end already!");
			}

			return _s[_position];
		}

		public void SkipWhitespace()
		{
			while (! IsEOF && Char.IsWhiteSpace(PeekChar()))
			{
				_position++;
			}
		}

		public string ReadToAny(params char[] tokens)
		{
			string token;
			var i = _s.IndexOfAny(tokens, _position);
			if (i < 0)
			{
				//throw new InvalidOperationException($"String '{_s}' after position {_position} does not contain {string.Join(", ", tokens.Select(t => "'" + t + "'"))}!");
				token = _s.Substring(_position);
				_position = _s.Length;
				return token;
			}

			token = _s.Substring(_position, i - _position);
			_position += token.Length;
			return token;
		}

		public bool IsEOF { get { return _position >= _s.Length; } }

		public void MatchEOF()
		{
			if (_position < _s.Length)
			{
				throw new InvalidOperationException($"String '{_s}' does not end at position {_position}!");
			} 
		}

		public string ReadAny(params string[] tokens)
		{
			for (int x = 0; x < tokens.Length; x++)
			{
				var token = tokens[x];

				if (_position + token.Length <= _s.Length && _s.Substring(_position, token.Length) == token)
				{
					_position += token.Length;
					return token;
				}
			}

			throw new InvalidOperationException($"String '{_s}' at position {_position} does not contain '{string.Join(" or ", tokens.Select(x => "'" + x + "'"))}'!");
		}

		public int ReadNumber()
		{
			if (!char.IsDigit(_s[_position]))
			{
				throw new InvalidOperationException($"String '{_s}' at position {_position} does not contain digit!");
			}

			var digitsCount = 1;

			while (_position + digitsCount < _s.Length && char.IsDigit(_s[_position + digitsCount]))
			{
				digitsCount++;
			}

			var result = Int32.Parse(_s.Substring(_position, digitsCount));

			_position += digitsCount;

			return result;
		}
	}
}
