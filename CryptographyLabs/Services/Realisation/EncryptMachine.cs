using System;
using System.Collections.Generic;
using System.Text;

namespace CryptographyLabs
{
    public class EncryptMachine
    {
        public string EngAlphabet { get; } = " abcdefghijklmnopqrstuvwxyz";       //0
        public string UkrAlphabet { get; } = " абвгґдеєжзиіїйклмнопрстуфхцчшщьюя"; //1

        public string Encrypt(string text, string key, int type = 0, int language = 0)
        {
            switch (type)
            {
                case 0:
                    text = Caesarus(ref text, Int32.Parse(key), language);
                    break;
            }

            return text;
        }

        public string Caesarus(ref string text, int key, int language)
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

            key = key % alphabet.Length;

            string newText = "";

            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                var index = alphabet.IndexOf(c);
                if (index < 0)
                {
                    newText += text[i].ToString();
                }
                else //if (text[i] != ' '&& alphabet.Contains(text[i].ToString().ToLower()))
                {
                    newText += alphabet[(alphabet.Length + alphabet.IndexOf(text[i]) + key) % alphabet.Length].ToString();
                }
                /*else if (text[i] == ' ')
                {
                    newText += " ";
                }
                else
                {
                    throw (new Exception("Invalid character was typed in input field!"));
                }*/
            }

            return newText;
        }
    }
}

