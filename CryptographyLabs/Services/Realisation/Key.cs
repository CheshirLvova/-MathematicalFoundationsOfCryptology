using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.IO;

namespace CryptographyLabs
{
    public class Key
    {
        public Key() { }
        public Key(string fileName)
        {
            try
            {
                var file = File.ReadAllLines(fileName);

                FirstPart = BigInteger.Parse(file[1]);
                SecondPart = BigInteger.Parse(file[2]);

                if (file[0].Contains("Private key"))
                {
                    IsPrivate = true;
                }
                else
                {
                    IsPrivate = false;
                }
            }
            catch
            {
                FirstPart = new BigInteger();
                SecondPart = new BigInteger();
                IsPrivate = false;

                throw new ArgumentException("Wrong keys!");
            }
        }

        public BigInteger FirstPart { get; set; }

        public BigInteger SecondPart { get; set; }

        public bool IsPrivate { get; set; }

        public override string ToString()
        {
            return IsPrivate ? $"---Private key---\n{FirstPart}\n{SecondPart}" : $"---Public key---\n{FirstPart}\n{SecondPart}";
        }
    }
}
