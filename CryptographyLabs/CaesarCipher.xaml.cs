using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CryptographyLabs
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class CaesarCipher : Page
{
    public MainWindow mainWindow;
    public IDialogService dialogService;
    public EncryptMachine encryptMachine;
    public DecryptMachine decryptMachine;

    public CaesarCipher(MainWindow _mainWindow)
    {
        InitializeComponent();
        dialogService = new DefaultDialogService();
        encryptMachine = new EncryptMachine();
        decryptMachine = new DecryptMachine();
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
        MessageBox.Show("                                               Лабораторна робота 1\n" +
            "Тема: Шифр  зсуву для латинського { _, a,b,...,z} та українського { _,а,б,в,г,ґ,..., я} алфавітів.\n" +
            "Мета: Розробити криптосистему на основі шифрів зсуву.\n" +
            "                                               Хід виконання роботи:\n" +
            "1. Розробіть інтерфейс криптографічної системи симетричного шифрування, передбачивши в ньому використання меню та/ або панелі інструментів для виконання таких команд: \n" +
            "    a. створення, відкривання, збереження, друкування файлів,\n" +
            "    b. шифрування і розшифрування файлів українською та англійською " +
            "                  мовами,\n" +
            "    c. виведення відомостей про розробника та\n" +
            "    d. виходу з системи.\n" +
            "2. Розробіть систему класів для реалізації симетричного шифрування, передбачивши в них методи валідації ключа, валідації, шифрування і розшифрування даних. \n" +
            "3. Виконайте тестування роботи системи.\n" +
            "4. Доповніть розроблену систему модулем для атаки на шифр методом «грубої сили» (перебору).\n" +
            "5. Розширте можливості системи, забезпечивши можливість шифрування даних в будь - якому форматі, а не тільки текстових. ", "CAESAR CIPHER (by Sofia Kovalchuk, PMI-34)");
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

    private void CheatButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            dialogService.OpenFileDialog();
            inputTextBox.Text = File.ReadAllLines(dialogService.FilePath).First();
            keyTextBox.Text = "";

            if (comboBoxLanguage.SelectedIndex == 0)
            {
                outputTextBlock.Text = "";
                for (int i = 0; i < 27; i++)
                {
                    outputTextBlock.Text += decryptMachine.Decrypt(inputTextBox.Text, i.ToString(), 0, comboBoxLanguage.SelectedIndex);
                    outputTextBlock.Text += "\n\n\n";
                }

            }
            else
            {
                outputTextBlock.Text = "";
                for (int i = 0; i < 34; i++)
                {
                    outputTextBlock.Text += decryptMachine.Decrypt(inputTextBox.Text, i.ToString(), 0, comboBoxLanguage.SelectedIndex);
                    outputTextBlock.Text += "\n\n\n";
                }
            }
        }
        catch (Exception ex)
        {
            dialogService.ShowMessage(ex.Message);
        }

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
                        outputTextBlock.Text += encryptMachine.Encrypt(inputTextBox.Text, i.ToString(), 0, comboBoxLanguage.SelectedIndex);
                        outputTextBlock.Text += "\n\n\n";
                    }

                }
                else
                {
                    outputTextBlock.Text = "";
                    for (int i = 0; i < 34; i++)
                    {
                        outputTextBlock.Text += encryptMachine.Encrypt(inputTextBox.Text, i.ToString(), 0, comboBoxLanguage.SelectedIndex);
                        outputTextBlock.Text += "\n\n\n";
                    }
                }
            }
            else
            {
                outputTextBlock.Text = encryptMachine.Encrypt(inputTextBox.Text, keyTextBox.Text, 0, comboBoxLanguage.SelectedIndex);
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
                        outputTextBlock.Text += decryptMachine.Decrypt(inputTextBox.Text, i.ToString(), 0, comboBoxLanguage.SelectedIndex);
                        outputTextBlock.Text += "\n\n\n";
                    }

                }
                else
                {
                    outputTextBlock.Text = "";
                    for (int i = 0; i < 34; i++)
                    {
                        outputTextBlock.Text += decryptMachine.Decrypt(inputTextBox.Text, i.ToString(), 0, comboBoxLanguage.SelectedIndex);
                        outputTextBlock.Text += "\n\n\n";
                    }
                }
            }
            else
            {
                outputTextBlock.Text = decryptMachine.Decrypt(inputTextBox.Text, keyTextBox.Text, /*comboBoxEncryptionType.SelectedIndex*/0, comboBoxLanguage.SelectedIndex);
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
