using System;
using System.Collections.Generic;
using System.Linq;
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

namespace CryptographyLabs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            OpenPage(pages.CaesarCipher);
        }

        public enum pages
        {
            CaesarCipher,
            TrithemiusCipher,
            TrithemiusCipher2,
            XORCipher,
            VigenereCipher,
            RSACipher,
            DiffieHellmanCipher
        }

        public void OpenPage(pages pages)
        {
            if (pages == pages.CaesarCipher)
            {
                frame.Navigate(new CaesarCipher(this));
            }
            else if (pages == pages.TrithemiusCipher)
            {
                frame.Navigate(new TrithemiusCipher(this));
            }
            else if (pages == pages.TrithemiusCipher2)
            {
                frame.Navigate(new TrithemiusCipher2(this));
            }
            else if (pages == pages.XORCipher)
            {
                frame.Navigate(new XORCipher(this));
            }
            else if (pages == pages.VigenereCipher)
            {
                frame.Navigate(new VigenereCipher(this));
            }
            else if (pages == pages.RSACipher)
            {
                frame.Navigate(new RSACipher(this));
            }
            else if (pages == pages.DiffieHellmanCipher)
            {
                frame.Navigate(new DiffieHellmanCipher(this));
            }
        }
    }
}
