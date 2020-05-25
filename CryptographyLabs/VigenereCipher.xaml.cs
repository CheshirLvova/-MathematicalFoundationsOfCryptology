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

namespace CryptographyLabs
{
    /// <summary>
    /// Interaction logic for VigenereCipher.xaml
    /// </summary>
    public partial class VigenereCipher : Page
    {
        public MainWindow mainWindow;
        public IDialogService dialogService;
        public VigenereMachine vigenereMachineENG;
        public VigenereMachineUkr vigenereMachineUKR;
    
        public VigenereCipher(MainWindow _mainWindow)
        {
            InitializeComponent();
            dialogService = new DefaultDialogService();
            vigenereMachineENG = new VigenereMachine();
            vigenereMachineUKR = new VigenereMachineUkr(" абвгґдеєжзиіїйклмнопрстуфхцчшщьюя");
            mainWindow = _mainWindow;
        }

        public void menuOpenFile_Click(object sender, RoutedEventArgs e)
        {
            if (dialogService.OpenFileDialog())
            {
                inputTextBox.Text = File.ReadAllLines(dialogService.FilePath).First();
                keyTextBox.Text = File.ReadAllLines(dialogService.FilePath).Skip(1).First();
            }
        }

        public void menuSaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(dialogService.FilePath))
            {
                File.WriteAllText(dialogService.FilePath, outputTextBlock.Text + "\n" + keyTextBox.Text + "\n");
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
                File.WriteAllText(dialogService.FilePath, outputTextBlock.Text + "\n" + keyTextBox.Text + "\n");
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
            MessageBox.Show("                                               Лабораторна робота 4\n" +
                "Тема: шифр Віженера.\n" +
                "Мета: Розробити криптосистему на основі шифру Віженера для латинського {_, a,b,...,z} та українського {_,а,б,в,г,ґ,..., я} алфавітів.\n" +
                "                                               Хід виконання роботи:\n" +
                "1. Розробіть інтерфейс криптографічної системи для реалізації шифрування. \n" +
                "2. Доповніть систему класів з попередніх лабораторних робіт класами та методами, необхідними для шифрування і розшифрування шифром Віженера. \n" +
                "3. Виконайте тестування роботи системи. ", "VIGENERE CIPHER (by Sofia Kovalchuk, PMI-34)");
        }

        public void inputTextBox_TextChanged(object sender, RoutedEventArgs e)
        {

        }

        public void menuNewFile_Click(object sender, RoutedEventArgs e)
        {
            inputTextBox.Text = "";
        }

        private void inputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void inputTextBox_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }

        private void inputTextBox_TargetUpdated(object sender, DataTransferEventArgs e)
        {

        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                    if (comboBoxLanguage.SelectedIndex == 0)
                    {
                            outputTextBlock.Text += vigenereMachineENG.Encrypt(inputTextBox.Text, keyTextBox.Text);
                            outputTextBlock.Text += "\n\n\n";
                    }
                    else
                    {
                        outputTextBlock.Text += vigenereMachineUKR.Encrypt(inputTextBox.Text, keyTextBox.Text);
                        outputTextBlock.Text += "\n\n\n";
                    }
                
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message);
            }
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                    if (comboBoxLanguage.SelectedIndex == 0)
                    {
                            outputTextBlock.Text += vigenereMachineENG.Decrypt(inputTextBox.Text, keyTextBox.Text);
                            outputTextBlock.Text += "\n\n\n";
                    }
                    else
                    {
                            outputTextBlock.Text += vigenereMachineUKR.Decrypt(inputTextBox.Text, keyTextBox.Text);
                            outputTextBlock.Text += "\n\n\n";
                    }
                
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message);
            }
        }

        private void ComboBoxEncryptionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
