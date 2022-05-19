using System;
using Encryption;

namespace CryptographyInDotNet
{
    class Program
    {
        static void ShowEncryptionAndDecryption(string text)
        { 
            byte[] key = { 23, 123, 11, 45, 90, 23, 123, 93, 10, 53, 43, 26, 40, 18, 145, 140};
            byte[] iv = { 25, 125, 13, 47, 92, 25, 125, 95, 15, 55, 45, 28, 42, 20, 147, 142};
            string originalData = text;
            string encryptedText = SymmetricEncryption.Encrypt(originalData, key, iv);
            string compressedEncryptedText = SymmetricEncryptionWithCompression.Encrypt(originalData, key, iv);
            Console.WriteLine("Encrypted text: " + encryptedText);
            Console.WriteLine("Compressed Encrypted text: " + compressedEncryptedText);
            
            string decryptedText = SymmetricEncryption.Decrypt(key, iv, encryptedText);;
            string compressedDecryptedText = SymmetricEncryptionWithCompression.Decrypt(key, iv, compressedEncryptedText);
            Console.WriteLine("Original data: " + decryptedText);
            Console.WriteLine("Compressed Original data: " + compressedDecryptedText);
        }
        static void Main(string[] args)
        {
        }
    }
}