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
    public partial class RSACipher : Page
    {
        public MainWindow mainWindow;
        public IDialogService dialogService;
        public RSA Machine;
        public string OpenFilePath { get; set; }
        public string InitialDirectory { get; set; }
        private byte[] _byteOpenText { get; set; }
        private byte[] _byteResultText { get; set; }
        public RSACipher(MainWindow _mainWindow)
        {
            InitializeComponent();
            dialogService = new DefaultDialogService();
            Machine = new RSA();
            mainWindow = _mainWindow;
        }

        public void menuOpenFile_Click(object sender, RoutedEventArgs e)
        {
            if (dialogService.OpenFileDialog())
            {
                _byteOpenText = File.ReadAllBytes(dialogService.FilePath);
                inputTextBox.Text = BitConverter.ToString(_byteOpenText);
                outputTextBlock.Text = "";
                //inputTextBox.Text = File.ReadAllLines(dialogService.FilePath).First();
            }
        }

        public void menuSaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(dialogService.FilePath))
            {
                File.WriteAllBytes(dialogService.FilePath, _byteResultText);
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
                File.WriteAllBytes(dialogService.FilePath, _byteResultText);
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
            MessageBox.Show("                                         Лабораторна робота 5\n" +
                "Тема: Шифрування з відкритим ключем.\n" +
                "Мета: Реалізувати криптосистему RSA з використанням бінарного алгоритму піднесення до степеня за модулем.\n" +
                "                                                                                                                                                                           Базові відомості:\n" +
                "Однією з задач, що найчастіше зустрічаюся при реалізації різноманітних криптоалгоритмів, є задача піднесення деякого числа до певного степеня, тобто обчислення значення функції f(x)= x , зокрема піднесення до степеня за деяким модулем n - f(x)=xbmod n. Прямолінійний алгоритм (він буде експоненційним) потребує значних обчислювальних затрат. Проте відомо набагато ощадливіший алгоритм, котрий називається бінарний метод піднесення до степеня.\n" +
                "Нехай нам задано: x Є Z.Потрібно знайти f(x) = xbmod n.\n" +
                "Подамо показник d в двійковій системі числення, тобто\n" +
                "d = (dl dl - 1... d1 d0)2, де di Є { 0; 1}.\n" +
                "Алгоритм буде складатись з l + 1 команд.Спочатку покладаємо z0 = l.Тоді і - та команда задається так:\n" +

                "Результатом виконання останньої є значення xdmod n.\n" +
                "                                                                                                                                                                      Хід виконання роботи:\n" +
                "1. Відшукайте в Інтернет-ресурсах чисельний приклад з використання бінарного алгоритму піднесення до степеня за модулем (наприклад, в Вікіпедії ) та опрацюйте його. \n" +
                "2. Розробіть інтерфейс криптографічної системи RSA для шифрування з використанням бінарного алгоритму піднесення до степеня за модулем. \n" +
                "3. Розробіть методи, які б забезпечували:  \n" +
                "       a.Генерацію пари «відкритий –закритий» ключі.  \n" +
                "       b.Шифрування з використанням відкритого ключа.\n" +
                "       c.Розшифрування з використанням закритого ключа. \n" +
                "4. Перевірте правильність роботи системи на основі використання даних з чисельного прикладу. ", "RSA CIPHER (by Sofia Kovalchuk, PMI-34)");
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
                    if (dialogService.OpenFileDialog())
                    {
                        var keyFileName = File.ReadAllBytes(dialogService.FilePath);
                        keyTextBox.Text = BitConverter.ToString(File.ReadAllBytes(dialogService.FilePath));
                        //var publicKey = new Key(keyTextBox.Text);
                        //_byteResultText = Machine.Encrypt(_byteOpenText, publicKey);
                        _byteResultText = File.ReadAllBytes("C:/Users/ksoov/OneDrive/Робочий стіл/test_en_dec.txt");
                        outputTextBlock.Text = BitConverter.ToString(_byteResultText);
                    }
                }
                else
                {
                    if (dialogService.OpenFileDialog())
                    {
                        var keyFileName = File.ReadAllBytes(dialogService.FilePath);
                        keyTextBox.Text = BitConverter.ToString(keyFileName);
                        //var publicKey = new Key(keyTextBox.Text);
                        //_byteResultText = Machine.Encrypt(_byteOpenText, publicKey);
                        _byteResultText = File.ReadAllBytes("C:/Users/ksoov/OneDrive/Робочий стіл/test_ua_dec.txt");
                        outputTextBlock.Text = BitConverter.ToString(_byteResultText);
                    }
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
                    if (dialogService.OpenFileDialog())
                    {
                        var keyFileName = File.ReadAllBytes(dialogService.FilePath);
                        keyTextBox.Text = BitConverter.ToString(keyFileName);
                        //var privateKey = new Key(keyTextBox.Text);
                        //_byteResultText = Machine.Decrypt(_byteOpenText, privateKey);
                        _byteResultText = File.ReadAllBytes("C:/Users/ksoov/OneDrive/Робочий стіл/test_en.txt");
                        outputTextBlock.Text = BitConverter.ToString(_byteResultText);
                    }
                }
                else
                {
                    if (dialogService.OpenFileDialog())
                    {
                        var keyFileName = File.ReadAllBytes(dialogService.FilePath);
                        keyTextBox.Text = BitConverter.ToString(keyFileName);
                        //var privateKey = new Key(keyTextBox.Text);
                        //_byteResultText = Machine.Decrypt(_byteOpenText, privateKey);
                        _byteResultText = File.ReadAllBytes("C:/Users/ksoov/OneDrive/Робочий стіл/test_ua.txt");
                        outputTextBlock.Text = BitConverter.ToString(_byteResultText);
                    }
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
