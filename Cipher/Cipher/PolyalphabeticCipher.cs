using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cipher {
    class PolyalphabeticCipher : CipherDecoder, ICipher {
        private List<int> Shifts = new List<int>();


        public string Name { get { return "Polyalphabetic"; } }


        public string KeyName { get { return "Shift Word"; } }


        public string Encode(string input, string key) {
            input = input.ToUpper();
            StringBuilder ciphered = new StringBuilder(input.Length);

            foreach (char c in key) {
                Shifts.Add(Alphabet.IndexOf(char.ToUpper(c)) + 1);
            }

            for (int i = 0; i < input.Length; ++i) {
                char c = input[i];

                if (char.IsLetter(c)) {
                    ciphered.Append(ShiftCharacter(c, Shifts[i % Shifts.Count]));
                }
                else {
                    ciphered.Append(c);
                }
            }

            Shifts.Clear();
            return ciphered.ToString();
        }


        public string Decode(string input, string key) {
            input = input.ToUpper();
            StringBuilder deciphered = new StringBuilder(input.Length);

            foreach (char c in key) {
                Shifts.Add(Alphabet.IndexOf(char.ToUpper(c)) + 1);
            }

            for (int i = 0; i < input.Length; ++i) {
                char c = input[i];

                if (char.IsLetter(c)) {
                    deciphered.Append(UnshiftCharacter(c, Shifts[i % Shifts.Count]));
                }
                else {
                    deciphered.Append(c);
                }
            }

            Shifts.Clear();
            return deciphered.ToString();
        }


        public void KeyFilter(object sender, System.Windows.Input.KeyEventArgs e) {
            // allow tabbing over to other controls
            if (e.Key == Key.Tab) {
                return;
            }

            // suppress non-alphabetic chars
            string keyString = e.Key.ToString();
            if (!(keyString.Length == 1 && char.IsLetter(keyString[0]))) {
                e.Handled = true;
            }
        }


        public override string ToString() {
            return Name;
        }


        public override string GetKey(string plainText, string cypheredText) {
            return ""; // TODO
        }
    }
}
