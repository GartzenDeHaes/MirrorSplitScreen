using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portland.Text.Generate
{
	public static class Lists
	{
		public const string CommonEnglishLetters = "etaionshr";

		public static string[] EnglishConsonants;
		public static string[] EnglishVowels;
		public static string[] EnglishVowelsExt;

		// Glyphs and digraphs in common English use. This doesn't represent all common phonemes.
		public static string[] CommonEnglishPhonemes;

		// Vowel glyphs and digraphs in common English use.
		public static string[] CommonEnglishVowels;

		public static string[] CommonEnglishNonWordSyllables;

		static Lists()
		{
			EnglishConsonants = new string[] { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z" };
			EnglishVowels = new string[] { "a", "e", "i", "o", "u", "y" };
			//EnglishVowelsExt = new string[] { "a", "e", "i", "o", "u", "y", "ee", "aa", "ij", "oe" };
			EnglishVowelsExt = new string[] { "a", "e", "i", "o", "u", "y",
				"ai", "ay", "aw", "ae",
				"ee", "ea", "ei", "ey",
				"ie",
				"oa", "oe", "oi", "oo", "ou", "oy", "ow", 
				"ui", "ue" 
			};

			CommonEnglishPhonemes = new string[] {
				// Digraphs
				"ae", "ch", "ng", "ph", "sh", "th", "zh",
				// ISO basic Latin monographs
				"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m",
				"n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
			};

			CommonEnglishVowels = new string[] {
				// Digraphs
				"ae",
				// ISO basic Latin monographs
				"a", "e", "i", "o", "u", "y"
			};

			CommonEnglishNonWordSyllables = new string[] {
				"ing","er","ter","al","ed","es","tion","re","oth","ry","de","ver","ex","en",
				"di","bout","com","ple","con","per","un","der","tle","ber","ty","num","peo",
				"ble","af","ers","mer","wa","ment","pro","ar","ma","ri","sen","ture","fer",
				"dif","pa","tions","ther","fore","est","fa","la","ei","not","si","ent","ven",
				"ev","ac","ca","fol","ful","na","tain","ning","col","par","dis","ern","ny","cit",
				"po","cal","mu","moth","pic","im","coun","mon","pe","lar","por","fi","bers","sec",
				"ap","stud","ad","tween","gan","bod","tence","ward","hap","nev","ure","mem",
				"ters","cov","ger","nit"
			};
		}

		public static Dictionary<int, string> CreateSymbolLookup(Func<string, int> hasher)
		{
			var dict = new Dictionary<int, string>();

			Parallel.ForEach(CommonEnglishNonWordSyllables, txt =>
			{
				dict.Add(hasher.Invoke(txt), txt);
			});

			Parallel.ForEach(EnglishConsonants, txt =>
			{
				dict.Add(hasher(txt), txt);
			});

			for (int x = 0; x < EnglishVowels.Length; x++)
			{
				dict.Add(hasher(EnglishVowels[x]), EnglishVowels[x]);
			}

			return dict;
		}
	}
}
