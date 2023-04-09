namespace ShocoSharp.Models
{
    public class ShocoCompatibleEnglishWordsModel : ShocoModel
    {
        public static ShocoCompatibleEnglishWordsModel Instance { get; } = new ShocoCompatibleEnglishWordsModel();

        private ShocoCompatibleEnglishWordsModel()
            : base(MinimumCharacter: 39,
                  MaximumCharacter: 122,
                  MaximumSuccessorLength: 7,
                  CharactersById: new byte[32] {
                       (byte)'e',  (byte)'a',  (byte)'i',  (byte)'o',  (byte)'t',  (byte)'h',  (byte)'n',  (byte)'r',  (byte)'s',  (byte)'l',  (byte)'u',
                       (byte)'c',  (byte)'w',  (byte)'m',  (byte)'d',  (byte)'b',  (byte)'p',  (byte)'f',  (byte)'g',  (byte)'v',  (byte)'y',  (byte)'k',
                       (byte)'-',  (byte)'H',  (byte)'M',  (byte)'T', (byte)'\'',  (byte)'B',  (byte)'x',  (byte)'I',  (byte)'W',  (byte)'L' },
                  IdsByCharacter: new byte[256] {
                      255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
                      255, 255, 255, 255, 255, 255, 255,  26, 255, 255, 255, 255, 255,  22, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
                      255, 255,  27, 255, 255, 255, 255, 255,  23,  29, 255, 255,  31,  24, 255, 255, 255, 255, 255, 255,  25, 255, 255,  30, 255, 255, 255, 255, 255, 255, 255, 255,
                      255,   1,  15,  11,  14,   0,  17,  18,   5,   2, 255,  21,   9,  13,   6,   3,  16, 255,   7,   8,   4,  10,  19,  12,  28,  20, 255, 255, 255, 255, 255, 255,
                      255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
                      255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
                      255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
                      255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255 },
                  SuccessorIdsByCharacterId: new byte[32, 32] {
                      {  7,   4,  12, 255,   6, 255,   1,   0,   3,   5, 255,   9, 255,   8,   2, 255,  15,  14, 255,  10,  11, 255, 255, 255, 255, 255, 255, 255,  13, 255, 255, 255},
                      {255, 255,   6, 255,   1, 255,   0,   3,   2,   4,  15,  11, 255,   9,   5,  10,  13, 255,  12,   8,   7,  14, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255},
                      {  9,  11, 255,   4,   2, 255,   0,   8,   1,   5, 255,   6, 255,   3,   7,  15, 255,  12,  10,  13, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255},
                      {255, 255,  14,   7,   5, 255,   1,   2,   8,   9,   0,  15,   6,   4,  11, 255,  12,   3, 255,  10, 255,  13, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255},
                      {  2,   4,   3,   1,   5,   0, 255,   6,  10,   9,   7,  12,  11, 255, 255, 255, 255,  13, 255, 255,   8, 255,  15, 255, 255, 255,  14, 255, 255, 255, 255, 255},
                      {  0,   1,   2,   3,   4, 255, 255,   5,   9,  10,   6, 255, 255,   8,  15,  11, 255,  14, 255, 255,   7, 255,  13, 255, 255, 255,  12, 255, 255, 255, 255, 255},
                      {  2,   8,   7,   4,   3, 255,   9, 255,   6,  11, 255,   5, 255, 255,   0, 255, 255,  14,   1,  15,  10,  12, 255, 255, 255, 255,  13, 255, 255, 255, 255, 255},
                      {  0,   3,   1,   2,   6, 255,   9,   8,   4,  12,  13,  10, 255,  11,   7, 255, 255,  15,  14, 255,   5, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255},
                      {  0,   6,   3,   4,   1,   2, 255, 255,   5,  10,   7,   9,  11,  12, 255, 255,   8,  14, 255, 255,  15,  13, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255},
                      {  0,   6,   2,   5,   9, 255, 255, 255,  10,   1,   8, 255,  12,  14,   4, 255,  15,   7, 255,  13,   3,  11, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255},
                      {  8,  10,   9,  15,   1, 255,   4,   0,   3,   2, 255,   6, 255,  12,  11,  13,   7,  14,   5, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255},
                      {  1,   3,   6,   0,   4,   2, 255,   7,  13,   8,   9,  11, 255, 255,  15, 255, 255, 255, 255, 255,  10,   5,  14, 255, 255, 255, 255, 255, 255, 255, 255, 255},
                      {  3,   0,   1,   4, 255,   2,   5,   6,   7,   8, 255,  14, 255, 255,   9,  15, 255,  12, 255, 255, 255,  10,  11, 255, 255, 255,  13, 255, 255, 255, 255, 255},
                      {  0,   1,   3,   2,  15, 255,  12, 255,   7,  14,   4, 255, 255,   9, 255,   8,   5,  10, 255, 255,   6, 255,  13, 255, 255, 255,  11, 255, 255, 255, 255, 255},
                      {  0,   3,   1,   2, 255, 255,  12,   6,   4,   9,   7, 255, 255,  14,   8, 255, 255,  15,  11,  13,   5, 255,  10, 255, 255, 255, 255, 255, 255, 255, 255, 255},
                      {  0,   5,   7,   2,  10,  13, 255,   6,   8,   1,   3, 255, 255,  14,  15,  11, 255, 255, 255,  12,   4, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255},
                      {  0,   2,   6,   3,   7,  10, 255,   1,   9,   4,   8, 255, 255,  15, 255,  12,   5, 255, 255, 255,  11, 255,  13, 255, 255, 255,  14, 255, 255, 255, 255, 255},
                      {  1,   3,   4,   0,   7, 255,  12,   2,  11,   8,   6,  13, 255, 255, 255, 255, 255,   5, 255, 255,  10,  15,   9, 255, 255, 255,  14, 255, 255, 255, 255, 255},
                      {  1,   3,   5,   2,  13,   0,   9,   4,   7,   6,   8, 255, 255,  15, 255,  11, 255, 255,  10, 255,  14, 255,  12, 255, 255, 255, 255, 255, 255, 255, 255, 255},
                      {  0,   2,   1,   3, 255, 255, 255,   6, 255, 255,   5, 255, 255, 255, 255, 255, 255, 255, 255, 255,   4, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255},
                      {  1,  11,   4,   0,   3, 255,  13,  12,   2,   7, 255, 255,  15,  10,   5,   8,  14, 255, 255, 255, 255, 255,   9, 255, 255, 255,   6, 255, 255, 255, 255, 255},
                      {  0,   9,   2,  14,  15,   4,   1,  13,   3,   5, 255, 255,  10, 255, 255, 255, 255,   6,  12, 255,   7, 255,   8, 255, 255, 255,  11, 255, 255, 255, 255, 255},
                      {255,   2,  14, 255,   1,   5,   8,   7,   4,  12, 255,   6,   9,  11,  13,   3,  10,  15, 255, 255, 255, 255,   0, 255, 255, 255, 255, 255, 255, 255, 255, 255},
                      {  0,   1,   3,   2, 255, 255, 255, 255, 255, 255,   4, 255, 255, 255, 255, 255, 255, 255, 255, 255,   6, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255},
                      {  4,   3,   1,   5, 255, 255, 255,   0, 255, 255,   6, 255, 255, 255, 255, 255, 255, 255, 255, 255,   2, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255},
                      {  2,   8,   4,   1, 255,   0, 255,   6, 255, 255,   5, 255,   7, 255, 255, 255, 255, 255, 255, 255,  10, 255, 255,   9, 255, 255, 255, 255, 255, 255, 255, 255},
                      { 12,   5, 255, 255,   1, 255, 255,   7,   0,   3, 255,   2, 255,   4,   6, 255, 255, 255, 255,   8, 255, 255,  15, 255,  13,   9, 255, 255, 255, 255, 255,  11},
                      {  1,   3,   2,   4, 255, 255, 255,   5, 255,   7,   0, 255, 255, 255, 255, 255, 255, 255, 255, 255,   6, 255, 255, 255, 255, 255, 255, 255, 255,   8, 255, 255},
                      {  5,   3,   4,  12,   1,   6, 255, 255, 255, 255,   8,   2, 255, 255, 255, 255,   0,   9, 255, 255,  11, 255,  10, 255, 255, 255, 255, 255, 255, 255, 255, 255},
                      {255, 255, 255, 255,   0, 255,   1,  12,   3, 255, 255, 255, 255,   5, 255, 255, 255,   2, 255, 255, 255, 255, 255, 255, 255, 255,   4, 255, 255,   6, 255,  10},
                      {  2,   3,   1,   4, 255,   0, 255,   5, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,   7, 255, 255, 255, 255, 255, 255, 255, 255,   6, 255, 255},
                      {  5,   1,   3,   0, 255, 255, 255, 255, 255, 255,   4, 255, 255, 255, 255, 255, 255, 255, 255, 255,   2, 255, 255, 255, 255, 255,   9, 255, 255,   6, 255,   7} },
                  CharactersBySuccessorId: new byte[83, 16] {
                      { (byte)'s',  (byte)'t',  (byte)'c',  (byte)'l',  (byte)'m',  (byte)'a',  (byte)'d',  (byte)'r',  (byte)'v',  (byte)'T',  (byte)'A',  (byte)'L',  (byte)'e',  (byte)'M',  (byte)'Y',  (byte)'-'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'-',  (byte)'t',  (byte)'a',  (byte)'b',  (byte)'s',  (byte)'h',  (byte)'c',  (byte)'r',  (byte)'n',  (byte)'w',  (byte)'p',  (byte)'m',  (byte)'l',  (byte)'d',  (byte)'i',  (byte)'f'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'u',  (byte)'e',  (byte)'i',  (byte)'a',  (byte)'o',  (byte)'r',  (byte)'y',  (byte)'l',  (byte)'I',  (byte)'E',  (byte)'R',       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'e',  (byte)'a',  (byte)'o',  (byte)'i',  (byte)'u',  (byte)'A',  (byte)'y',  (byte)'E',       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'t',  (byte)'n',  (byte)'f',  (byte)'s', (byte)'\'',  (byte)'m',  (byte)'I',  (byte)'N',  (byte)'A',  (byte)'E',  (byte)'L',  (byte)'Z',  (byte)'r',  (byte)'V',  (byte)'R',  (byte)'C'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'o',  (byte)'a',  (byte)'y',  (byte)'i',  (byte)'u',  (byte)'e',  (byte)'I',  (byte)'L',  (byte)'D', (byte)'\'',  (byte)'E',  (byte)'Y',       0x00,       0x00,       0x00,       0x00},
                      { (byte)'r',  (byte)'i',  (byte)'y',  (byte)'a',  (byte)'e',  (byte)'o',  (byte)'u',  (byte)'Y',       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'h',  (byte)'o',  (byte)'e',  (byte)'E',  (byte)'i',  (byte)'u',  (byte)'r',  (byte)'w',  (byte)'a',  (byte)'H',  (byte)'y',  (byte)'R',  (byte)'Z',       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'h',  (byte)'i',  (byte)'e',  (byte)'a',  (byte)'o',  (byte)'r',  (byte)'I',  (byte)'y',       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'n',  (byte)'t',  (byte)'s',  (byte)'r',  (byte)'l',  (byte)'d',  (byte)'i',  (byte)'y',  (byte)'v',  (byte)'m',  (byte)'b',  (byte)'c',  (byte)'g',  (byte)'p',  (byte)'k',  (byte)'u'},
                      { (byte)'e',  (byte)'l',  (byte)'o',  (byte)'u',  (byte)'y',  (byte)'a',  (byte)'r',  (byte)'i',  (byte)'s',  (byte)'j',  (byte)'t',  (byte)'b',  (byte)'v',  (byte)'h',  (byte)'m',  (byte)'d'},
                      { (byte)'o',  (byte)'e',  (byte)'h',  (byte)'a',  (byte)'t',  (byte)'k',  (byte)'i',  (byte)'r',  (byte)'l',  (byte)'u',  (byte)'y',  (byte)'c',  (byte)'q',  (byte)'s',  (byte)'-',  (byte)'d'},
                      { (byte)'e',  (byte)'i',  (byte)'o',  (byte)'a',  (byte)'s',  (byte)'y',  (byte)'r',  (byte)'u',  (byte)'d',  (byte)'l',  (byte)'-',  (byte)'g',  (byte)'n',  (byte)'v',  (byte)'m',  (byte)'f'},
                      { (byte)'r',  (byte)'n',  (byte)'d',  (byte)'s',  (byte)'a',  (byte)'l',  (byte)'t',  (byte)'e',  (byte)'m',  (byte)'c',  (byte)'v',  (byte)'y',  (byte)'i',  (byte)'x',  (byte)'f',  (byte)'p'},
                      { (byte)'o',  (byte)'e',  (byte)'r',  (byte)'a',  (byte)'i',  (byte)'f',  (byte)'u',  (byte)'t',  (byte)'l',  (byte)'-',  (byte)'y',  (byte)'s',  (byte)'n',  (byte)'c', (byte)'\'',  (byte)'k'},
                      { (byte)'h',  (byte)'e',  (byte)'o',  (byte)'a',  (byte)'r',  (byte)'i',  (byte)'l',  (byte)'s',  (byte)'u',  (byte)'n',  (byte)'g',  (byte)'b',  (byte)'-',  (byte)'t',  (byte)'y',  (byte)'m'},
                      { (byte)'e',  (byte)'a',  (byte)'i',  (byte)'o',  (byte)'t',  (byte)'r',  (byte)'u',  (byte)'y',  (byte)'m',  (byte)'s',  (byte)'l',  (byte)'b', (byte)'\'',  (byte)'-',  (byte)'f',  (byte)'d'},
                      { (byte)'n',  (byte)'s',  (byte)'t',  (byte)'m',  (byte)'o',  (byte)'l',  (byte)'c',  (byte)'d',  (byte)'r',  (byte)'e',  (byte)'g',  (byte)'a',  (byte)'f',  (byte)'v',  (byte)'z',  (byte)'b'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'e',  (byte)'n',  (byte)'i',  (byte)'s',  (byte)'h',  (byte)'l',  (byte)'f',  (byte)'y',  (byte)'-',  (byte)'a',  (byte)'w', (byte)'\'',  (byte)'g',  (byte)'r',  (byte)'o',  (byte)'t'},
                      { (byte)'e',  (byte)'l',  (byte)'i',  (byte)'y',  (byte)'d',  (byte)'o',  (byte)'a',  (byte)'f',  (byte)'u',  (byte)'t',  (byte)'s',  (byte)'k',  (byte)'w',  (byte)'v',  (byte)'m',  (byte)'p'},
                      { (byte)'e',  (byte)'a',  (byte)'o',  (byte)'i',  (byte)'u',  (byte)'p',  (byte)'y',  (byte)'s',  (byte)'b',  (byte)'m',  (byte)'f', (byte)'\'',  (byte)'n',  (byte)'-',  (byte)'l',  (byte)'t'},
                      { (byte)'d',  (byte)'g',  (byte)'e',  (byte)'t',  (byte)'o',  (byte)'c',  (byte)'s',  (byte)'i',  (byte)'a',  (byte)'n',  (byte)'y',  (byte)'l',  (byte)'k', (byte)'\'',  (byte)'f',  (byte)'v'},
                      { (byte)'u',  (byte)'n',  (byte)'r',  (byte)'f',  (byte)'m',  (byte)'t',  (byte)'w',  (byte)'o',  (byte)'s',  (byte)'l',  (byte)'v',  (byte)'d',  (byte)'p',  (byte)'k',  (byte)'i',  (byte)'c'},
                      { (byte)'e',  (byte)'r',  (byte)'a',  (byte)'o',  (byte)'l',  (byte)'p',  (byte)'i',  (byte)'t',  (byte)'u',  (byte)'s',  (byte)'h',  (byte)'y',  (byte)'b',  (byte)'-', (byte)'\'',  (byte)'m'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'e',  (byte)'i',  (byte)'o',  (byte)'a',  (byte)'s',  (byte)'y',  (byte)'t',  (byte)'d',  (byte)'r',  (byte)'n',  (byte)'c',  (byte)'m',  (byte)'l',  (byte)'u',  (byte)'g',  (byte)'f'},
                      { (byte)'e',  (byte)'t',  (byte)'h',  (byte)'i',  (byte)'o',  (byte)'s',  (byte)'a',  (byte)'u',  (byte)'p',  (byte)'c',  (byte)'l',  (byte)'w',  (byte)'m',  (byte)'k',  (byte)'f',  (byte)'y'},
                      { (byte)'h',  (byte)'o',  (byte)'e',  (byte)'i',  (byte)'a',  (byte)'t',  (byte)'r',  (byte)'u',  (byte)'y',  (byte)'l',  (byte)'s',  (byte)'w',  (byte)'c',  (byte)'f', (byte)'\'',  (byte)'-'},
                      { (byte)'r',  (byte)'t',  (byte)'l',  (byte)'s',  (byte)'n',  (byte)'g',  (byte)'c',  (byte)'p',  (byte)'e',  (byte)'i',  (byte)'a',  (byte)'d',  (byte)'m',  (byte)'b',  (byte)'f',  (byte)'o'},
                      { (byte)'e',  (byte)'i',  (byte)'a',  (byte)'o',  (byte)'y',  (byte)'u',  (byte)'r',       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'a',  (byte)'i',  (byte)'h',  (byte)'e',  (byte)'o',  (byte)'n',  (byte)'r',  (byte)'s',  (byte)'l',  (byte)'d',  (byte)'k',  (byte)'-',  (byte)'f', (byte)'\'',  (byte)'c',  (byte)'b'},
                      { (byte)'p',  (byte)'t',  (byte)'c',  (byte)'a',  (byte)'i',  (byte)'e',  (byte)'h',  (byte)'q',  (byte)'u',  (byte)'f',  (byte)'-',  (byte)'y',  (byte)'o',       0x00,       0x00,       0x00},
                      { (byte)'o',  (byte)'e',  (byte)'s',  (byte)'t',  (byte)'i',  (byte)'d', (byte)'\'',  (byte)'l',  (byte)'b',  (byte)'-',  (byte)'m',  (byte)'a',  (byte)'r',  (byte)'n',  (byte)'p',  (byte)'w'} },
                  Packs: new ShocoPack[] {
                      new ShocoPack(0x80, 1, 2, new int[] { 26, 24, 24, 24, 24, 24, 24, 24 }, new int[] { 15, 3, 0, 0, 0, 0, 0, 0 }),
                      new ShocoPack(0xC0, 2, 4, new int[] { 25, 22, 19, 16, 16, 16, 16, 16 }, new int[] { 15, 7, 7, 7, 0, 0, 0, 0 }),
                      new ShocoPack(0xE0, 4, 8, new int[] { 23, 19, 15, 11, 8, 5, 2, 0 }, new int[] { 31, 15, 15, 15, 7, 7, 7, 3 }) })
        { }

    }
}