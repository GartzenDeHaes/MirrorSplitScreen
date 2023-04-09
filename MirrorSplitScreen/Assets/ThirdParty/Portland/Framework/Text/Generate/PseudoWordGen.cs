
//"""A Random Word Generator that uses a built-in algorithm that follows the syllable rules
//in the English language. Useful for finding a creative name for a business or app."""

using System;

public class PseudoRandomWord
{
	//char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
	char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
	char[] consensnts = new char[] { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };
	//char[] starting_letters = new char[] { 'c', 'd', 'g', 'h', 'i', 'j', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'y', 'z' };

	int maxlength;
	char[] start = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'v', 'w', 'y', 'z' };
	string word = "";

	char[] preceed_Vowel_letters = new char[] { 'b', 'd', 'j', 'w', 'z', 'q' };
	char[] a = new char[] { 'b', 'c', 'd', 'e', 'f', 'g', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };

	char[] e = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

	char[] i = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z' };

	char[] o = new char[] { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'z' };

	char[] u = new char[] { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z' };

	char[] y = new char[] { 'o', 'a', 'e', 'u' };

	char[] cons_next = new char[] { 'a', 'i', 'e', 'o', 'u', 't', 'r' };

	Random random;

	private bool In(char[] ar, char ch)
	{
		for (int x = 0; x < ar.Length; x++)
		{
			if (ar[x] == ch)
			{
				return true;
			}
		}

		return false;
	}

	public PseudoRandomWord(int maxlength, Random random)
	{
		this.random = random;
		this.maxlength = maxlength;
		this.word += this.start[random.Next(this.start.Length)];
	}

	string construct_word()
	{
		while (this.word.Length < this.maxlength)
		{
			if (this.word.Length >= 2)
			{
				if (In(vowels, this.word[word.Length-2]) && In(vowels, this.word[word.Length - 1]))
							this.word += consensnts[random.Next(consensnts.Length)];
				if (In(consensnts, this.word[this.word.Length-2]) && In(consensnts, this.word[this.word.Length-1]))
							this.word += vowels[random.Next(vowels.Length)];
			}
			if (In(vowels, this.word[word.Length-1]))
			{
				if (this.word[word.Length - 1] == 'a')
					this.word += this.a[random.Next(this.a.Length)];
				if (this.word[word.Length - 1] == 'e')
							this.word += this.e[random.Next(this.e.Length)];
				if (this.word[word.Length - 1] == 'i')
					this.word += this.i[random.Next(this.i.Length)];
				if (this.word[word.Length - 1] == 'o')
					this.word += this.o[random.Next(this.o.Length)];
				if (this.word[word.Length - 1] == 'u')
					this.word += this.u[random.Next(this.u.Length)];
			}
			if (In(consensnts, this.word[word.Length - 1]))
			{
				if (In(this.preceed_Vowel_letters, this.word[word.Length - 1]))
					this.word += vowels[random.Next(vowels.Length)];
				else if (this.word[word.Length - 1] == 'y')
					this.word += this.y[random.Next(this.y.Length)];
				else
					this.word += this.cons_next[random.Next(this.cons_next.Length)];
			}
		}

		//this.word = this.word[0:this.maxlength];
		this.word = this.word.Substring(0, this.maxlength);
		return this.word;
	}

	public static string Random_Word(int maxlength, Random random)   //: #returns a single random word
	{
		var rd = new PseudoRandomWord(maxlength, random);
		return rd.construct_word();
	}
}


