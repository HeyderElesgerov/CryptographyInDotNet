using System;
using Encryption;

namespace CryptographyInDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] key = { 23, 123, 11, 45, 90, 23, 123, 93, 10, 53, 43, 26, 40, 18, 145, 140};
            byte[] iv = { 25, 125, 13, 47, 92, 25, 125, 95, 15, 55, 45, 28, 42, 20, 147, 142};
            string encryptedText = SymmetricEncryption.Encrypt("Salam", key, iv);
            Console.WriteLine("Encrypted text: " + encryptedText);
            string decryptedText = SymmetricEncryption.Decrypt(key, iv, encryptedText);
            Console.WriteLine("Original data: " + decryptedText);
        }
    }
}