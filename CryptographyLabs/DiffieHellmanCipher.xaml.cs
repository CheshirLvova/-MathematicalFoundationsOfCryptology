using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Numerics;

namespace CryptographyLabs
{
    /// <summary>
    /// Interaction logic for VigenereCipher.xaml
    /// </summary>
    
    public partial class DiffieHellmanCipher : Page
    {
        public BigInteger P { get; set; }
        public BigInteger G { get; set; }

        public BigInteger BobPublic { get; set; }
        public BigInteger BobPrivate { get; set; }

        public BigInteger AlicePublic { get; set; }
        public BigInteger AlicePrivate { get; set; }

        public MainWindow mainWindow;
        public IDialogService dialogService;
        public XORMachine MachineENG;
        public XORMachineDec MachineUKR;
    
        public DiffieHellmanCipher(MainWindow _mainWindow)
        {
            InitializeComponent();
            dialogService = new DefaultDialogService();
            MachineENG = new XORMachine();
            MachineUKR = new XORMachineDec();
            mainWindow = _mainWindow;
        }

        public void menuOpenFile_Click(object sender, RoutedEventArgs e)
        {
            if (dialogService.OpenFileDialog())
            {
                keyTextBox0.Text = File.ReadAllLines(dialogService.FilePath).First();
                keyTextBox.Text = File.ReadAllLines(dialogService.FilePath).Skip(1).First();
                keyTextBoxAlice.Text = File.ReadAllLines(dialogService.FilePath).Skip(2).First();
                keyTextBoxBob.Text = File.ReadAllLines(dialogService.FilePath).Skip(3).First();
            }
        }

        public void menuSaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(dialogService.FilePath))
            {
                File.WriteAllText(dialogService.FilePath, keyTextBox0.Text + "\n" + keyTextBox.Text + "\n" + keyTextBoxAlice.Text + "\n" + keyTextBoxBob.Text);
            }
            else
            {
                menuSaveFileAs_Click(sender, e);
            }
        }

        public void menuSaveFileAs_Click(object sender, RoutedEventArgs e)
        {
            if (dialogService.SaveFileDialog())
            {
                File.WriteAllText(dialogService.FilePath, outputTextBlock.Text + "\n" + keyTextBox.Text);
                //File.WriteAllText(dialogService.FilePath, inputTextBox.Text + "\n" + outputTextBlock.Text + "\n");
            }
        }

        public void CaesarCipher_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.CaesarCipher);
        }

        public void TrithemiusCipher_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.TrithemiusCipher);
        }
        public void TrithemiusCipher2_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.TrithemiusCipher2);
        }
        public void XORCipher_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.XORCipher);
        }
        public void VigenereCipher_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.VigenereCipher);
        }
        public void RSACipher_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.RSACipher);
        }
        public void DiffieHellmanCipher_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.DiffieHellmanCipher);
        }
        public void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("                          Лабораторна робота 6\n" +
                "Тема: протокол обміну ключами Діффі-Гелмана.\n" +
                "Мета: Реалізувати протокол обміну ключами Діффі-Гелмана.\n", "DIFFIE-HELLMAN CIPHER (by Sofia Kovalchuk, PMI-34)");
        }


        public void menuNewFile_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void GenerateButton0_Click(object sender, RoutedEventArgs e)
        {
            P = BigInteger.Parse(keyTextBox.Text);
            G = BigInteger.Parse(keyTextBox0.Text);
            AlicePrivate = BigInteger.Parse(keyTextBoxAlice.Text);
            int d = int.Parse(keyTextBox.Text) - 1;
            while (d % 2 == 0)
                d /= 2;
            int a = int.Parse(keyTextBox0.Text) - 1;
            while (a % 2 == 0)
                a /= 2;
            if (miillerTest(d, int.Parse(keyTextBox.Text)) && miillerTest(a, int.Parse(keyTextBox0.Text)))
            {
                AlicePublic = BigInteger.ModPow(G, AlicePrivate, P);
                outputTextBlock0.Text = AlicePublic.ToString();
            }
            else
            {
                MessageBox.Show("P або G не просте!");
            }
        }

        private void GenerateButton1_Click(object sender, RoutedEventArgs e)
        {
            P = BigInteger.Parse(keyTextBox.Text);
            G = BigInteger.Parse(keyTextBox0.Text);
            BobPrivate = BigInteger.Parse(keyTextBoxBob.Text);
            int d = int.Parse(keyTextBox.Text) - 1;
            while (d % 2 == 0)
                d /= 2;
            int a = int.Parse(keyTextBox0.Text) - 1;
            while (a % 2 == 0)
                a /= 2;
            if (miillerTest(d, int.Parse(keyTextBox.Text)) && miillerTest(a, int.Parse(keyTextBox0.Text)))
            {
                BobPublic = BigInteger.ModPow(G, BobPrivate, P);
                outputTextBlock1.Text = BobPublic.ToString();
            }
            else
            {
                MessageBox.Show("P або G не просте!");
            }
        }
        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            var sAlice = BigInteger.ModPow(BobPublic, AlicePrivate, P);
            var sBob = BigInteger.ModPow(AlicePublic, BobPrivate, P);

            outputTextBlock.Text = $"Публічний ключ Аліси = {sAlice}\n" +
                $"Публічний ключ Боба = {sBob}\n";

            if (sAlice == sBob)
            {
                outputTextBlock.Text += "Приватні ключі співпадають!";
            }
            else
            {
                outputTextBlock.Text += "Приватні ключі не співпадають :(";
            }

        }

        static int power(int x, int y, int p)
        {

            int res = 1; // Initialize result 

            // Update x if it is more than  
            // or equal to p 
            x = x % p;

            while (y > 0)
            {

                // If y is odd, multiply x with result 
                if ((y & 1) == 1)
                    res = (res * x) % p;

                // y must be even now 
                y = y >> 1; // y = y/2 
                x = (x * x) % p;
            }

            return res;
        }

        static bool miillerTest(int d, int n)
        {

            // Pick a random number in [2..n-2] 
            // Corner cases make sure that n > 4 
            Random r = new Random();
            int a = 2 + (int)(r.Next() % (n - 4));

            // Compute a^d % n 
            int x = power(a, d, n);

            if (x == 1 || x == n - 1)
                return true;

            // Keep squaring x while one of the 
            // following doesn't happen 
            // (i) d does not reach n-1 
            // (ii) (x^2) % n is not 1 
            // (iii) (x^2) % n is not n-1 
            while (d != n - 1)
            {
                x = (x * x) % n;
                d *= 2;

                if (x == 1)
                    return false;
                if (x == n - 1)
                    return true;
            }

            // Return composite 
            return false;
        }

        // It returns false if n is composite  
        // and returns true if n is probably  
        // prime. k is an input parameter that  
        // determines accuracy level. Higher  
        // value of k indicates more accuracy. 

        
    }
}
