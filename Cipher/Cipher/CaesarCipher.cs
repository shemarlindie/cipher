using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cipher {
    class CaesarCipher : CipherDecoder, ICipher {
        public string Name { get { return "Caesar"; } }


        public string KeyName { get { return "Shift Number"; } }


        public string Encode(string input, string shift) {
            int shiftNumber = int.Parse(shift);
            input = input.ToUpper();
            StringBuilder ciphered = new StringBuilder(input.Length);

            foreach (char c in input) {
                if (char.IsLetter(c)) {
                    ciphered.Append(ShiftCharacter(c, shiftNumber));
                }
                else {
                    ciphered.Append(c);
                }
            }

            return ciphered.ToString();
        }


        public string Decode(string input, string shift) {
            int shiftNumber = int.Parse(shift);
            input = input.ToUpper();
            StringBuilder deciphered = new StringBuilder(input.Length);

            foreach (char c in input) {
                if (char.IsLetter(c)) {
                    deciphered.Append(UnshiftCharacter(c, shiftNumber));
                }
                else {
                    deciphered.Append(c);
                }
            }

            return deciphered.ToString();
        }


        public void KeyFilter(object sender, KeyEventArgs e) {
            // allow tabbing over to other controls
            if (e.Key == Key.Tab) {
                return;
            }

            // suppress non-numeric characters
            string keyString = e.Key.ToString();
            if (!(keyString.Length == 2 && keyString[0] == 'D' && char.IsDigit(keyString[1]))) {
                e.Handled = true;
            }
        }


        public override string ToString() {
            return Name;
        }


        public override string GetKey(string plainText, string cypheredText) {
            return GetShiftDistance(plainText[0], cypheredText[0]).ToString();
        }
    }
}
