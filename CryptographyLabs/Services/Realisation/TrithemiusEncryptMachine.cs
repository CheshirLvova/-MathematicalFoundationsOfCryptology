using System;
using System.Collections.Generic;
using System.Text;

namespace CryptographyLabs
{
    public class TrithemiusEncryptMachine
    {
        public string EngAlphabet { get; } = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";       //0
        public string UkrAlphabet { get; } = " абвгґдеєжзиіїйклмнопрстуфхцчшщьюяАБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ"; //1

        public string Encrypt(string text, string keyOne, string keyTwo, int type = 0, int language = 0)
        {
            switch (type)
            {
                case 0:
                    text = TrithemiusOne(ref text, Int32.Parse(keyOne), Int32.Parse(keyTwo), language);
                    break;
            }

            return text;
        }

        public string TrithemiusOne(ref string text, int keyOne, int keyTwo, int language)
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
                    newText += alphabet[(alphabet.Length + alphabet.IndexOf(text[i]) + keyOne*i + keyTwo) % alphabet.Length].ToString();
                }
            }

            return newText;
        }
    }
}

