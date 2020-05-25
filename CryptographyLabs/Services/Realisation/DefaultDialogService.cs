using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Windows;

namespace CryptographyLabs
{
    public class DefaultDialogService : IDialogService
    {
        public string FilePath { get; set; }

        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "output"; // Default file name
            saveFileDialog.DefaultExt = ".txt"; // Default file extension
            saveFileDialog.Filter = "Text documents (.txt)|*.txt" + "|Office Files (.doc; .xls; .ppt)|*.doc;*.xls;*.ppt" + "|All Files|*.*"; // Filter files by extension
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
