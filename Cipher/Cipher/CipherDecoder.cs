using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cipher {
    abstract class CipherDecoder {
        protected readonly string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";


        public abstract string GetKey(string plainText, string cypheredText);


        protected int GetShiftDistance(char letter1, char letter2) {
            letter1 = char.ToUpper(letter1);
            letter2 = char.ToUpper(letter2);
            int distance = 0;
            int firstIndex = Alphabet.IndexOf(letter1);

            while (Alphabet[firstIndex] != letter2) {
                distance++;
                firstIndex++;

                if (firstIndex > 25) {
                    firstIndex = 0;
                }
            }

            return distance;
        }


        protected char ShiftCharacter(char c, int shift) {
            shift = Math.Abs(shift);
            int alphaIndex = Alphabet.IndexOf(char.ToUpper(c));
            int shiftedIndex = (alphaIndex + shift) % 26;

            return Alphabet[shiftedIndex];
        }


        protected char UnshiftCharacter(char c, int shift) {
            shift = Math.Abs(shift) % 26;
            int alphaIndex = Alphabet.IndexOf(c);
            int tmpIndex = alphaIndex - shift;
            int unshiftedIndex = (tmpIndex >= 0) ? tmpIndex : 26 + tmpIndex;

            return Alphabet[unshiftedIndex];
        }
    }
}
