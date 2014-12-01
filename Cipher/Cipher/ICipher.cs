using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cipher {
    interface ICipher {
        string Name { get; }

        string KeyName { get; }

        string Encode(string input, string key);

        string Decode(string input, string key);

        void KeyFilter(object sender, KeyEventArgs e);
    }
}
