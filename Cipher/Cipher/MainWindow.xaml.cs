using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cipher {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly ICipher[] Ciphers = { new CaesarCipher(), new PolyalphabeticCipher() };
        private const int DefaultCipherIndex = 0;
        private ICipher CurrentCipher;
        private KeyEventHandler lastKeyFilter;

        public MainWindow() {
            InitializeComponent();

            foreach (ICipher c in Ciphers) {
                cbCiphers.Items.Add(c);
            }
            cbCiphers.SelectionChanged += cbCiphers_SelectionChanged;
            cbCiphers.SelectedIndex = DefaultCipherIndex;

            btnEncrypt.Click += btnEncrypt_Click;
            btnDecrypt.Click += btnDecrypt_Click;
            btnGetKey.Click += btnGetKey_Click;
            btnSwap.Click += btnSwap_Click;
            tbInput.TextChanged += tb_TextChanged;
            tbOutput.TextChanged += tb_TextChanged;
        }


        private void tb_TextChanged(object sender, TextChangedEventArgs e) {
            if (tbInput.Text.Length > 0 && tbOutput.Text.Length > 0) {
                btnGetKey.IsEnabled = true;
            }
            else {
                btnGetKey.IsEnabled = false;
            }
        }

        private void tb_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key.ToString().Length == 1 && "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(e.Key.ToString().ToUpper()[0])) {
                if (sender == tbInput) {
                    btnEncrypt_Click(sender, e);
                }
                else if (sender == tbOutput) {
                    btnDecrypt_Click(sender, e);
                }
            }
        }

        void cbCiphers_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            int i = cbCiphers.SelectedIndex;

            CurrentCipher = Ciphers[i];
            lblkey.Content = CurrentCipher.KeyName;

            if (lastKeyFilter != null) {
                tbKey.KeyDown -= lastKeyFilter;
            }
            lastKeyFilter = Ciphers[i].KeyFilter;
            tbKey.KeyDown += Ciphers[i].KeyFilter;
            tbKey.Clear();
            tbKey.Focus();
        }

        void btnSwap_Click(object sender, RoutedEventArgs e) {
            string tmp = tbOutput.Text;
            tbOutput.Text = tbInput.Text;
            tbInput.Text = tmp;
        }

        private void btnGetKey_Click(object sender, RoutedEventArgs e) {
            if (tbInput.Text.Length > 0 && tbOutput.Text.Length > 0) {
                tbKey.Text = (CurrentCipher as CipherDecoder).GetKey(tbInput.Text, tbOutput.Text);
            }
        }

        void btnDecrypt_Click(object sender, RoutedEventArgs e) {
            if (tbOutput.Text.Length > 0 && tbKey.Text.Length > 0) {
                tbInput.Text = CurrentCipher.Decode(tbOutput.Text, tbKey.Text);
            }
        }

        void btnEncrypt_Click(object sender, RoutedEventArgs e) {
            if (tbInput.Text.Length > 0 && tbKey.Text.Length > 0) {
                tbOutput.Text = CurrentCipher.Encode(tbInput.Text, tbKey.Text);
            }
        }
    }
}
