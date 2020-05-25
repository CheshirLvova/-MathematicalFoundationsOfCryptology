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
    public partial class XORCipher : Page
    {
        public MainWindow mainWindow;
        public IDialogService dialogService;
        public XORMachine MachineENG;
        public XORMachineDec MachineUKR;
    
        public XORCipher(MainWindow _mainWindow)
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
                inputTextBox.Text = File.ReadAllLines(dialogService.FilePath).First();
                keyTextBox.Text = File.ReadAllLines(dialogService.FilePath).Skip(1).First();
            }
        }

        public void menuSaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(dialogService.FilePath))
            {
                File.WriteAllText(dialogService.FilePath, outputTextBlock.Text + "\n" + keyTextBox.Text);
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
            MessageBox.Show("                                                                                                                                     Лабораторна робота 3\n" +
                "Тема: шифр гамування .\n" +
                "Мета: Розробити криптосистему на основі шифру гамування.\n" +
                "                                                                                                                                         Базові відомості:\n" +
                "Метод полягає в тому, що символи тексту, який шифрується, послідовно складаються з символами деякої спеціальної послідовності, яка називається гамою. Іноді такий метод представляють як накладення гами на вхідний текст, тому він отримав назву «гамування». При цьому символи вихідного тексту і гамми замінюються цифровими еквівалентами, які потім складаються по модулю n, де n - число символів в алфавіті, тобто шифрування і розшифрування для шифру гамування можна виразити наступними рівняннями: \n" +
                "                                                                                                                       y=(x+g) mod n		x=(y+n−(g mod n)) mod n,\n" +
                "де x - символ відкритого тексту, y -символ шифрованого тексту, g – символ гами. \n" +
                "Найбільш часто на практиці зустрічається двійкове гамування.При цьому використовується двійковий алфавіт, а складання здійснюється за модулем два: \n"+
                "                                                                                                                                     z = x + g(mod 2) = x XOR g.\n" +
                "Операція складання по модулю два в алгебрі логіки називається також \"виключне АБО\" або по-англійськи XOR.Операція XOR дуже швидко виконується на комп'ютері (на відміну від багатьох інших арифметичних операцій), тому накладення гами навіть на дуже великий відкритий текст виконується практично миттєво.\n"+
                "Цю ж саму операцію використовують і для розшифрування.\n"+ 
                "При використанні методу гамування ключем є послідовність, з якою проводиться складання -гамма.Якщо гамма коротше, ніж повідомлення, призначене для шифрування, гамма повторюється необхідну кількість разів.Чим довше ключ, тим надійніше шифрування методом гамування.\n"+
                "Розрізняють два різновиди гамування -з кінцевою і нескінченною гамами. При хороших статистичних властивостях гами якість шифрування визначається тільки довжиною періоду гами. При цьому, якщо довжина періоду гами перевищує довжину шифротексту, то такий шифр є абсолютно стійким, тобто його не можна розкрити за допомогою статистичної обробки зашифрованого тексту.При шифруванні за допомогою ЕОМ послідовність гами може формуватися за допомогою генератора псевдовипадкових чисел(ПВЧ). \n" +
                "                                                                                                                                      Хід виконання роботи:\n" +
                "1. Адаптуйте інтерфейс криптографічної системи симетричного шифрування з лабораторної роботи №1 або №2 для реалізації шифрування методом гамування.  \n" +
                "2. Доповніть систему класів з попередніх лабораторних робіт класами та методами, необхідними для: \n" +
                "       a.генерації гами, період якої перевищує довжину вхідного тексту; \n" +
                "       b.реалізації симетричного шифрування методом гамування.\n" +
                "3. Виконайте тестування роботи системи.  \n" +
                "3. Модифікуйте розроблену систему, забезпечивши можливість шифрування і розшифрування за допомогою шифроблокноту, як це передбачено в шифрі Вернама.", "GAMMA CIPHER (by Sofia Kovalchuk, PMI-34)");
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
                            outputTextBlock.Text += MachineENG.Encrypt(inputTextBox.Text, keyTextBox.Text, 0, 0);
                            outputTextBlock.Text += "\n\n\n";
                    }
                    else
                    {
                        outputTextBlock.Text += MachineENG.Encrypt(inputTextBox.Text, keyTextBox.Text, 0, 1);
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
                            outputTextBlock.Text += MachineUKR.Decrypt(inputTextBox.Text, keyTextBox.Text, 0, 0);
                            outputTextBlock.Text += "\n\n\n";
                    }
                    else
                    {
                            outputTextBlock.Text += MachineUKR.Decrypt(inputTextBox.Text, keyTextBox.Text, 0, 1);
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
