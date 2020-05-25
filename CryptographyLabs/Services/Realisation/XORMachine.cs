using System;
using System.Collections.Generic;
using System.Text;

namespace CryptographyLabs
{
    public class XORMachine
    {
        public string EngAlphabet { get; } = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public string UkrAlphabet { get; } = " абвгґдеєжзиіїйклмнопрстуфхцчшщьюяАБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ"; //1

        public string Encrypt(string text, string secretKey, int type = 0, int language = 0)
        {
            switch (type)
            {
                case 0:
                    text = Cipher(text, secretKey, language);
                    break;
            }

            return text;
        }
        //генератор повторюваної послідовності
        private string GetRepeatKey(string s, int n)
        {
            var r = s;
            while (r.Length < n)
            {
                r += r;
            }

            return r.Substring(0, n);
        }

        //метод шифрування/дешифрування
        private string Cipher(string text, string secretKey, int language=0)
        {
            string alphabet = EngAlphabet;
            switch (language)
            {
                case 0:
                    alphabet = EngAlphabet;
                    break;
                case 1:
                    alphabet = UkrAlphabet;
                    break;
            }
            string newText = "";
            var currentKey = GetRepeatKey(secretKey, text.Length);
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                var index = alphabet.IndexOf(c);
                if (index < 0)
                {
                    newText += text[i].ToString();
                }
                else
                {
                    newText += alphabet[(alphabet.Length + alphabet.IndexOf(text[i]) + currentKey[i]) % alphabet.Length].ToString();
                }
            }

            return newText;
        }
    }
}
