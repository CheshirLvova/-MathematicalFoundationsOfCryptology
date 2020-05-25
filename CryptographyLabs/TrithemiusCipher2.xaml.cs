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
    /// Interaction logic for TrithemiusCipher2.xaml
    /// </summary>
    public partial class TrithemiusCipher2 : Page
    {
        public MainWindow mainWindow;
        public IDialogService dialogService;
        public TrithemiusEncryptMachineTwo trithemiusEncMachineOne;
        public TrithemiusDecryptMachineTwo trithemiusDecMachineOne;
        public TrithemiusCipher2(MainWindow _mainWindow)
        {
            InitializeComponent();
            dialogService = new DefaultDialogService();
            trithemiusEncMachineOne = new TrithemiusEncryptMachineTwo();
            trithemiusDecMachineOne = new TrithemiusDecryptMachineTwo();
            mainWindow = _mainWindow;
        }

        public void menuOpenFile_Click(object sender, RoutedEventArgs e)
        {
            if (dialogService.OpenFileDialog())
            {
                inputTextBox.Text = File.ReadAllLines(dialogService.FilePath).First();
                keyTextBox.Text = File.ReadAllLines(dialogService.FilePath).Skip(1).First();
                keyTextBox1.Text = File.ReadAllLines(dialogService.FilePath).Skip(2).First();
                keyTextBox2.Text = File.ReadAllLines(dialogService.FilePath).Skip(3).First();
            }
        }

        public void menuSaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(dialogService.FilePath))
            {
                File.WriteAllText(dialogService.FilePath, outputTextBlock.Text + "\n" + keyTextBox.Text + "\n" + keyTextBox1.Text + "\n" + keyTextBox2.Text + "\n");
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
                File.WriteAllText(dialogService.FilePath, outputTextBlock.Text + "\n" + keyTextBox.Text + "\n" + keyTextBox1.Text + "\n" + keyTextBox2.Text + "\n");
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
            MessageBox.Show("                                            Лабораторна робота 2\n" +
                "Тема: Шифр Тритеміуса.\n" +
                "Мета: Розробити криптосистему на основі шифру Тритеміуса.\n" +

                "                                                Базові відомості:\n" +
                "Шифр Тритеміуса - вдосконалений шифр Цезаря, в якому кожен символ повідомлення зміщується на символ, який відстає від даного на деякий крок. Але крок зміщення робиться змінним, тобто залежним від будь-яких додаткових чинників. Наприклад, можна задати закон зміщення у вигляді лінійної функції позиції літери, що шифрується, або за допомогою використання гасла – текстового рядка, який багаторазово записується під текстом повідомленням. \n" +
                "Таким чином, шифрування і розшифрування для шифру Тритеміуса можна виразити наступними рівняннями:\n" +
                "                       y = (x + k) mod n       x = (y + n−(k mod n)) mod n,\n" +
                "        де x - символ відкритого тексту, y -символ шифрованого тексту, n -потужність алфавіту.\n" +
                "Крок зміщення k розраховується: \n" +
                "       •	за лінійним рівнянням k = Ap + B; \n" +
                "       •	за нелінійним рівнянням k = A2 + Bp + C; \n" +
                "       •	за гаслом. \n" +
                "Тут p -позиція букви в повідомленні.Ключем шифрування виступають відповідно коефіцієнти вказаних рівнянь та гасло.\n" +
                "                                       Хід виконання роботи:\n" +
                "Модифікуйте інтерфейс криптографічної системи симетричного шифрування з лабораторної роботи №1, забезпечивши можливість використання в якості ключа:\n" +
                "    a. 2 - вимірного вектору для зберігання коефіцієнтів лінійного рівняння шифрування,\n" +
                "    b. 3 - вимірного вектору для зберігання коефіцієнтів лінійного рівняння шифрування,\n" +
                "    c.Текстового рядка(гасла).\n" +
                "2. Доповніть систему класів з лабораторної роботи №1 класами та методами, необхідними для реалізації симетричного шифрування методом Тритеміуса, передбачивши в них методи валідації ключа, валідації шифрування і розшифрування даних.\n" +
                "3. Виконайте тестування роботи системи.\n" +
                "4. Побудувати частотні таблиці для української та англійської мов.\n" +
                "5. Доповніть систему модулем активної атаки на шифр Тритеміуса, який би забезпечував знаходження ключа шифрування у випадку, коли зловмиснику вдалось отримати пару повідомлень «незашифроване –зашифроване».", "TRITHEMIUS CIPHER (by Sofia Kovalchuk, PMI-34)");

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
                if (keyTextBox.Text == "")
                {
                    if (comboBoxLanguage.SelectedIndex == 0)
                    {
                        outputTextBlock.Text = "";
                        for (int i = 0; i < 27; i++)
                        {
                            outputTextBlock.Text += trithemiusEncMachineOne.Encrypt(inputTextBox.Text, i.ToString(), i.ToString(), i.ToString(), 0, comboBoxLanguage.SelectedIndex);
                            outputTextBlock.Text += "\n\n\n";
                        }

                    }
                    else
                    {
                        outputTextBlock.Text = "";
                        for (int i = 0; i < 34; i++)
                        {
                            outputTextBlock.Text += trithemiusEncMachineOne.Encrypt(inputTextBox.Text, i.ToString(), i.ToString(), i.ToString(), 0, comboBoxLanguage.SelectedIndex);
                            outputTextBlock.Text += "\n\n\n";
                        }
                    }
                }
                else
                {
                    outputTextBlock.Text = trithemiusEncMachineOne.Encrypt(inputTextBox.Text, keyTextBox.Text, keyTextBox1.Text, keyTextBox2.Text, 0, comboBoxLanguage.SelectedIndex);
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
                if (keyTextBox.Text == "")
                {
                    if (comboBoxLanguage.SelectedIndex == 0)
                    {
                        outputTextBlock.Text = "";
                        for (int i = 0; i < 27; i++)
                        {
                            outputTextBlock.Text += trithemiusDecMachineOne.Decrypt(inputTextBox.Text, i.ToString(), i.ToString(), i.ToString(), 0, comboBoxLanguage.SelectedIndex);
                            outputTextBlock.Text += "\n\n\n";
                        }

                    }
                    else
                    {
                        outputTextBlock.Text = "";
                        for (int i = 0; i < 34; i++)
                        {
                            outputTextBlock.Text += trithemiusDecMachineOne.Decrypt(inputTextBox.Text, i.ToString(), i.ToString(), i.ToString(), 0, comboBoxLanguage.SelectedIndex);
                            outputTextBlock.Text += "\n\n\n";
                        }
                    }
                }
                else
                {
                    outputTextBlock.Text = trithemiusDecMachineOne.Decrypt(inputTextBox.Text, keyTextBox.Text, keyTextBox1.Text, keyTextBox2.Text,/*comboBoxEncryptionType.SelectedIndex*/0, comboBoxLanguage.SelectedIndex);
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
