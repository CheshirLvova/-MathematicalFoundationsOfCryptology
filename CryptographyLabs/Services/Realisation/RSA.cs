using System;
using System.Text;
using System.Collections.Generic;
using System.Numerics;

namespace CryptographyLabs
{
    public partial class RSA
    {
        public byte[] Decrypt(byte[] textArray, Key privateKey)
        {
            var text = Encoding.UTF8.GetString(textArray);
            var textBlocks = text.Split('-');

            var result = new List<byte>();
            for (int i = 0; i < textBlocks.Length; i++)
            {
                var number = BigInteger.Parse(textBlocks[i]);
                var tempResult = BigInteger.ModPow(number, privateKey.SecondPart, privateKey.FirstPart);
                var byteArray = tempResult.ToByteArray();
                if (tempResult <= new BigInteger(255))
                {
                    result.Insert(result.Count, byteArray[0]);
                }
                else
                {
                    result.InsertRange(result.Count, byteArray);
                }
            }

            return result.ToArray();
        }

        public byte[] Encrypt(byte[] text, Key publicKey)
        {
            string result = "";

            for (int i = 0; i < text.Length; i++)
            {
                var blockAsNumber = new BigInteger(text[i]);
                result += BigInteger.ModPow(blockAsNumber, publicKey.SecondPart, publicKey.FirstPart).ToString() + "-";
            }

            result = result.Remove(result.Length - 1, 1);
            return Encoding.UTF8.GetBytes(result);
        }
    }
}
