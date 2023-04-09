namespace ShocoSharp.Models
{
    public class ShocoTextModel : ShocoModel
    {
        public static ShocoTextModel Instance { get; } = new ShocoTextModel();

        private ShocoTextModel()
            : base(MinimumCharacter: 32,
                  MaximumCharacter: 119,
                  MaximumSuccessorLength: 7,
                  CharactersById: new byte[32] {
                       (byte)'<',  (byte)'>',  (byte)' ',  (byte)'/',  (byte)'e',  (byte)'t',  (byte)'a',  (byte)'E',  (byte)'i',  (byte)'n',  (byte)'s',
                       (byte)'l',  (byte)'r',  (byte)'S',  (byte)'o',  (byte)'T',  (byte)'I',  (byte)'A',  (byte)'R',  (byte)'C',  (byte)'N',  (byte)'d',
                       (byte)'p',  (byte)'L',  (byte)'v',  (byte)'"',  (byte)'_',  (byte)'m',  (byte)'P',  (byte)'c',  (byte)'h',  (byte)'g' },
                  IdsByCharacter: new byte[256] {
                      255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
                        2, 255,  25, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,   3, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,   0, 255,   1, 255,
                      255,  17, 255,  19, 255,   7, 255, 255, 255,  16, 255, 255,  23, 255,  20, 255,  28, 255,  18,  13,  15, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,  26,
                      255,   6, 255,  29,  21,   4, 255,  31,  30,   8, 255, 255,  11,  27,   9,  14,  22, 255,  12,  10,   5, 255,  24, 255, 255, 255, 255, 255, 255, 255, 255, 255,
                      255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
                      255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
                      255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
                      255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255 },
                  SuccessorIdsByCharacterId: new byte[32, 32] {
                      {255, 255, 255,   0, 255, 255,  10,   7,   4,  13,   5,   3, 255,   2, 255,  15, 255, 255,   8,   1, 255, 255, 255,   9,  11, 255, 255, 255,   6, 255, 255, 255},
                      {  0, 255,   5, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,  10, 255,   7, 255,   9,  13,  14, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255},
                      {  2, 255,   0, 255,  15,   3,   5, 255,   4,  13,   6, 255, 255, 255,   7, 255, 255, 255, 255, 255, 255,  12, 255, 255,   1, 255, 255,  10, 255,   9,   8, 255},
                      {255,   0, 255, 255, 255,  15,   8,   5, 255,  10, 255,   3, 255,   2, 255,  11, 255, 255,   6,   1, 255, 255,  14,   7, 255, 255, 255, 255,   4, 255, 255, 255},
                      {255,   0,   1, 255,  14,  13,  11, 255, 255,   4,   3,   5,   2, 255, 255, 255, 255, 255, 255, 255, 255,   8, 255, 255, 255, 255, 255,  12, 255,  10, 255,   6},
                      { 14,   0,   1, 255,   2,   9,   6, 255,   5, 255,  12, 255,   3, 255,   8, 255, 255, 255, 255, 255, 255,  10, 255, 255, 255, 255, 255, 255, 255,  13,   4, 255},
                      {255,  14,  10, 255, 255,   3, 255, 255,  12,   2,   8,   0,   1, 255, 255, 255, 255, 255, 255, 255, 255,  11,   4, 255, 255, 255, 255,   6, 255,   7, 255,  15},
                      {255,   0, 255, 255, 255, 255,   7,  10, 255, 255,   6, 255, 255,   1, 255,  11, 255,   3,   2,   8,   9, 255, 255,  14, 255, 255,   4, 255, 255,  13, 255, 255},
                      {255,   5, 255, 255,   7,   2,  15, 255, 255,   0,   1,   9,  14, 255,   6, 255, 255, 255, 255, 255, 255,   4, 255, 255,  11, 255, 255,   3, 255,   8, 255,  13},
                      { 13,   6,   5, 255,   4,   0,   7, 255,  11, 255,   8, 255,  15, 255,   3, 255, 255, 255, 255, 255, 255,   2, 255, 255, 255, 255, 255, 255, 255,  10, 255,   1},
                      { 11,   1,   2, 255,   4,   0,  12, 255,   6, 255,   8,  15, 255, 255,   7, 255, 255, 255, 255, 255, 255, 255,   3, 255, 255, 255, 255, 255, 255,  10,   5,  13},
                      {255,   2,   9, 255,   5,  10,   3, 255,   0, 255,  12,   4, 255, 255,   7, 255, 255, 255, 255, 255, 255,   8, 255, 255, 255, 255,  15,  11, 255, 255, 255, 255},
                      { 12,   0,   2, 255,   3,   9,   4, 255,   1,  14,   7, 255,  11, 255,   6, 255, 255, 255, 255, 255, 255,  13, 255, 255, 255,  15, 255,  10, 255, 255, 255,   8},
                      { 12,   0, 255, 255, 255,   7, 255,   8, 255, 255, 255,  15, 255,  10, 255,   1,   4,   3, 255, 255, 255, 255, 255, 255, 255, 255,   6, 255,   2, 255, 255, 255},
                      {255, 255,   7, 255, 255,   9, 255, 255, 255,   0,  13,   8,   1, 255,  14, 255, 255, 255, 255, 255, 255,   2,  11, 255,   4, 255, 255,   5, 255,  10, 255, 255},
                      {255,   9, 255, 255, 255, 255,   4,   0, 255, 255, 255, 255, 255,   2,  12,   7,   5,   6,   3,   8, 255, 255, 255, 255, 255, 255,   1, 255, 255, 255,  10, 255},
                      {255,   6,  11, 255, 255, 255, 255,   8, 255,  13, 255, 255, 255, 255, 255,   1, 255,  15,  12,   2,   0, 255, 255,   9, 255, 255, 255,  10,   7, 255, 255, 255},
                      { 15,  10, 255, 255, 255, 255, 255, 255, 255,  11, 255, 255,  12, 255, 255,   1, 255, 255,   3,   5,  14,   7, 255,   2, 255, 255, 255, 255, 255, 255, 255, 255},
                      { 15,   0, 255, 255,   2, 255, 255,  14, 255, 255, 255, 255, 255,   8,  13,   5,   3,   7,  10, 255,   4, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255},
                      {255, 255, 255, 255, 255, 255,   3,   4, 255, 255, 255,   7, 255, 255,   2, 255, 255,  11,   6, 255, 255, 255,  12, 255,   1, 255, 255,  15, 255,  13,  10, 255},
                      { 12,   5,  13, 255, 255, 255,  10,   0, 255, 255, 255, 255, 255,   1,  11,   7,   6,   2, 255, 255,   9, 255, 255, 255, 255, 255,   3, 255, 255, 255, 255, 255},
                      { 11,   1,   2, 255,   0,   9,  10, 255,   4, 255,  13, 255,   6, 255,   7, 255, 255, 255, 255, 255, 255,   5, 255, 255, 255,  15, 255, 255, 255, 255, 255, 255},
                      {255,   0,  14, 255,   4,   6,   5, 255,   8, 255,  15,   3,   2, 255,   9, 255, 255, 255, 255, 255, 255, 255,   1, 255, 255, 255, 255, 255, 255, 255,  12, 255},
                      { 15,  11,   8,  13,  12, 255, 255,   1,   7, 255, 255, 255, 255,   4, 255,  10,   0,   3, 255, 255, 255, 255, 255,  14, 255, 255, 255, 255, 255, 255, 255, 255},
                      {255,   1,   7, 255,   2,  15,   0, 255,   3, 255, 255, 255, 255, 255,   6, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,  12, 255, 255, 255,   8, 255, 255},
                      {255,   1,   6,   0,   2, 255, 255, 255, 255, 255,   4, 255, 255, 255, 255, 255, 255, 255, 255, 255,  14,  10, 255, 255, 255, 255,   7, 255,  11, 255, 255, 255},
                      {255, 255, 255, 255, 255,  14, 255, 255, 255, 255, 255, 255, 255,   1, 255, 255, 255, 255,   3,  11,   5, 255, 255, 255, 255, 255,   9, 255,   4,  10, 255,  13},
                      { 14,   1,   7, 255,   0, 255,   4, 255,   5, 255,   9,   8,  11, 255,   6, 255, 255, 255, 255, 255, 255,  15,   2, 255, 255, 255, 255,   3, 255, 255, 255, 255},
                      {255,  14, 255,  15, 255, 255,  11,   0,   9, 255, 255, 255,   1, 255,   7, 255,   4,  12, 255,   6, 255, 255, 255,   2, 255, 255, 255, 255, 255, 255, 255, 255},
                      {255,   8,  14, 255,   1,   4,   6, 255,  11, 255,  12,   5,  13, 255,   0, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,  10,   2, 255},
                      { 13,   3,   4, 255,   0,   7,   1, 255,   2, 255,  10, 255,   6, 255,   5, 255, 255, 255, 255, 255, 255, 255,   9, 255, 255, 255, 255, 255, 255, 255, 255,  15},
                      { 14,   1,   0,   9,   2,  15,  13, 255,   7,   6,  12,   5,   4, 255,   8, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,  11, 255, 255, 255, 255,   3, 255} },
                  CharactersBySuccessorId: new byte[87, 16] {
                      { (byte)' ',  (byte)'v',  (byte)'<',  (byte)'t',  (byte)'i',  (byte)'a',  (byte)'s',  (byte)'o',  (byte)'h',  (byte)'c',  (byte)'m',  (byte)'w',  (byte)'d',  (byte)'n',  (byte)'b',  (byte)'e'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'/',  (byte)'>',  (byte)'e',  (byte)'0',  (byte)'s',  (byte)'1',  (byte)' ',  (byte)'_',  (byte)'#',  (byte)'?',  (byte)'d',  (byte)'P',  (byte)'4',  (byte)'8',  (byte)'N',  (byte)'2'},
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
                      { (byte)'>',  (byte)'C',  (byte)'S',  (byte)'l',  (byte)'P',  (byte)'E',  (byte)'R',  (byte)'L',  (byte)'a',  (byte)'G',  (byte)'n',  (byte)'T',  (byte)'H',  (byte)'W',  (byte)'p',  (byte)'t'},
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
                      { (byte)'/',  (byte)'C',  (byte)'S',  (byte)'l',  (byte)'i',  (byte)'s',  (byte)'P',  (byte)'E',  (byte)'R',  (byte)'L',  (byte)'a',  (byte)'v',  (byte)'G',  (byte)'n',  (byte)'H',  (byte)'T'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'<',  (byte)'0',  (byte)'1',  (byte)'2',  (byte)'3',  (byte)' ',  (byte)'4',  (byte)'T',  (byte)'5',  (byte)'A',  (byte)'S',  (byte)'6',  (byte)'7',  (byte)'R',  (byte)'C',  (byte)'O'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'M',  (byte)'T',  (byte)'L',  (byte)'R',  (byte)'K',  (byte)'C',  (byte)'Y',  (byte)'d',  (byte)'U',  (byte)'V',  (byte)'>',  (byte)'n',  (byte)'r',  (byte)'G',  (byte)'N',  (byte)'<'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'H',  (byte)'v',  (byte)'o',  (byte)'a',  (byte)'E',  (byte)'K',  (byte)'R',  (byte)'l',  (byte)'f',  (byte)'O',  (byte)'h',  (byte)'A',  (byte)'p',  (byte)'c',  (byte)'D',  (byte)'m'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'>',  (byte)'S',  (byte)'R',  (byte)'A',  (byte)'_',  (byte)'D',  (byte)'s',  (byte)'a',  (byte)'C',  (byte)'N',  (byte)'E',  (byte)'T',  (byte)'b',  (byte)'c',  (byte)'L',  (byte)'q'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'N',  (byte)'T',  (byte)'C',  (byte)'O',  (byte)'V',  (byte)'F',  (byte)'>',  (byte)'P',  (byte)'E',  (byte)'L',  (byte)'m',  (byte)' ',  (byte)'R',  (byte)'n',  (byte)'U',  (byte)'A'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'I',  (byte)'E',  (byte)'K',  (byte)'A',  (byte)'S',  (byte)'O',  (byte)'D',  (byte)'i',  (byte)' ',  (byte)'U',  (byte)'T',  (byte)'>',  (byte)'e',  (byte)'/',  (byte)'L',  (byte)'<'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'E',  (byte)'S',  (byte)'A',  (byte)'_',  (byte)'G',  (byte)'>',  (byte)'I',  (byte)'T',  (byte)'U',  (byte)'N',  (byte)'a',  (byte)'o',  (byte)'<',  (byte)' ',  (byte)'O',  (byte)'D'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'E',  (byte)'r',  (byte)'L',  (byte)'O',  (byte)'I',  (byte)'u',  (byte)'C',  (byte)'o',  (byte)'B',  (byte)'i',  (byte)'K',  (byte)'a',  (byte)'A',  (byte)'H',  (byte)'>',  (byte)'/'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'>',  (byte)'U',  (byte)'e',  (byte)'I',  (byte)'N',  (byte)'T',  (byte)'O',  (byte)'A',  (byte)'S',  (byte)'B',  (byte)'R',  (byte)'D',  (byte)'F',  (byte)'o',  (byte)'E',  (byte)'<'},
                      { (byte)'>',  (byte)'T',  (byte)'P',  (byte)'A',  (byte)'I',  (byte)'U',  (byte)'_',  (byte)'t',  (byte)'E',  (byte)'H',  (byte)'S',  (byte)'M',  (byte)'<',  (byte)'D',  (byte)'K',  (byte)'l'},
                      { (byte)'E',  (byte)'_',  (byte)'S',  (byte)'R',  (byte)'a',  (byte)'I',  (byte)'A',  (byte)'T',  (byte)'C',  (byte)'>',  (byte)'h',  (byte)'-',  (byte)'o',  (byte)'H',  (byte)'O',  (byte)'Y'},
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
                      { (byte)'B',  (byte)'S',  (byte)'O',  (byte)'R',  (byte)'P',  (byte)'N',  (byte)'F',  (byte)'H',  (byte)'G',  (byte)'_',  (byte)'c',  (byte)'C',  (byte)'z',  (byte)'g',  (byte)'t',  (byte)'?'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'l',  (byte)'r',  (byte)'n',  (byte)'t',  (byte)'p',  (byte)'x',  (byte)'m',  (byte)'c',  (byte)'s',  (byte)'b',  (byte)' ',  (byte)'d',  (byte)'i',  (byte)'y',  (byte)'>',  (byte)'g'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'o',  (byte)'e',  (byte)'h',  (byte)'k',  (byte)'t',  (byte)'l',  (byte)'a',  (byte)'x',  (byte)'>',  (byte)'u',  (byte)'c',  (byte)'i',  (byte)'s',  (byte)'r',  (byte)' ',  (byte)'f'},
                      { (byte)'e',  (byte)'>',  (byte)' ',  (byte)'=',  (byte)'i',  (byte)'d',  (byte)'r',  (byte)'o',  (byte)'W',  (byte)'t',  (byte)'a',  (byte)'<',  (byte)',',  (byte)'s',  (byte)'.',  (byte)'"'},
                      { (byte)'>',  (byte)' ',  (byte)'r',  (byte)'s',  (byte)'n',  (byte)'l',  (byte)'g',  (byte)'q',  (byte)'d',  (byte)'f',  (byte)'c',  (byte)'a',  (byte)'m',  (byte)'t',  (byte)'e',  (byte)'y'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)' ',  (byte)'>',  (byte)'e',  (byte)'h',  (byte)'r',  (byte)'l',  (byte)'n',  (byte)'i',  (byte)'o',  (byte)'/',  (byte)'u',  (byte)'"',  (byte)'s',  (byte)'a',  (byte)'<',  (byte)'t'},
                      { (byte)'e',  (byte)'a',  (byte)'i',  (byte)'>',  (byte)' ',  (byte)'o',  (byte)'r',  (byte)'t',  (byte)'B',  (byte)'p',  (byte)'s',  (byte)'y',  (byte)'u',  (byte)'<',  (byte)',',  (byte)'g'},
                      { (byte)'n',  (byte)'s',  (byte)'t',  (byte)'m',  (byte)'d',  (byte)'>',  (byte)'o',  (byte)'e',  (byte)'c',  (byte)'l',  (byte)'f',  (byte)'v',  (byte)'b',  (byte)'g',  (byte)'r',  (byte)'a'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'i',  (byte)'=',  (byte)'>',  (byte)'a',  (byte)'l',  (byte)'e',  (byte)'u',  (byte)'o',  (byte)'d',  (byte)' ',  (byte)'t',  (byte)'m',  (byte)'s',  (byte)'y',  (byte)':',  (byte)'_'},
                      { (byte)'e',  (byte)'>',  (byte)'p',  (byte)'m',  (byte)'a',  (byte)'i',  (byte)'o',  (byte)' ',  (byte)'l',  (byte)'s',  (byte)'y',  (byte)'r',  (byte)'u',  (byte)'b',  (byte)'<',  (byte)'d'},
                      { (byte)'t',  (byte)'g',  (byte)'d',  (byte)'o',  (byte)'e',  (byte)' ',  (byte)'>',  (byte)'a',  (byte)'s',  (byte)'b',  (byte)'c',  (byte)'i',  (byte)'u',  (byte)'<',  (byte)'y',  (byte)'r'},
                      { (byte)'n',  (byte)'r',  (byte)'d',  (byte)'u',  (byte)'v',  (byte)'m',  (byte)'f',  (byte)' ',  (byte)'l',  (byte)'t',  (byte)'c',  (byte)'p',  (byte)'w',  (byte)'s',  (byte)'o',  (byte)'b'},
                      { (byte)'>',  (byte)'p',  (byte)'r',  (byte)'l',  (byte)'e',  (byte)'a',  (byte)'t',  (byte)';',  (byte)'i',  (byte)'o',  (byte)'f',  (byte)'u',  (byte)'h',  (byte)':',  (byte)' ',  (byte)'s'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'>',  (byte)'i',  (byte)' ',  (byte)'e',  (byte)'a',  (byte)'j',  (byte)'o',  (byte)'s',  (byte)'g',  (byte)'t',  (byte)'m',  (byte)'r',  (byte)'<',  (byte)'d',  (byte)'n',  (byte)'"'},
                      { (byte)'t',  (byte)'>',  (byte)' ',  (byte)'p',  (byte)'e',  (byte)'h',  (byte)'i',  (byte)'o',  (byte)'s',  (byte)'u',  (byte)'c',  (byte)'<',  (byte)'a',  (byte)'g',  (byte)',',  (byte)'l'},
                      { (byte)'>',  (byte)' ',  (byte)'e',  (byte)'r',  (byte)'h',  (byte)'i',  (byte)'a',  (byte)'y',  (byte)'o',  (byte)'t',  (byte)'d',  (byte)'u',  (byte)'s',  (byte)'c',  (byte)'<',  (byte)'-'},
                      {      0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00,       0x00},
                      { (byte)'a',  (byte)'>',  (byte)'e',  (byte)'i',  (byte)'2',  (byte)'?',  (byte)'o',  (byte)' ',  (byte)'c',  (byte)'G',  (byte)'1',  (byte)'3',  (byte)'"',  (byte)'y',  (byte)'.',  (byte)'t'} },
                  Packs: new ShocoPack[] {
                      new ShocoPack(0x80, 1, 2, new int[] { 26, 24, 24, 24, 24, 24, 24, 24 }, new int[] { 15, 3, 0, 0, 0, 0, 0, 0 }),
                      new ShocoPack(0xC0, 2, 4, new int[] { 25, 21, 18, 16, 16, 16, 16, 16 }, new int[] { 15, 15, 7, 3, 0, 0, 0, 0 }),
                      new ShocoPack(0xE0, 4, 8, new int[] { 24, 20, 16, 12, 8, 5, 2, 0 }, new int[] { 15, 15, 15, 15, 15, 7, 7, 3 }) })
        { }

    }
}