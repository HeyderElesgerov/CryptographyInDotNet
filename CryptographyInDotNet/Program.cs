using System;
using System.Security.Cryptography;
using System.Text;
using Encryption;

namespace CryptographyInDotNet
{
    class Program
    {
        static void ShowEncryptionAndDecryption(string text)
        {
            byte[] key = new byte[16];
            byte[] iv = new byte[16];
            RandomNumberGenerator.Fill(key);
            RandomNumberGenerator.Fill(iv);
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

        static void ShowAsymmetricEncryptionAndDecryption(string text)
        {
            PublicKeyEncryption.GenerateKeys(out byte[] privateKey, out byte[] publicKey);
            Console.WriteLine("Private key: " + Convert.ToBase64String(privateKey));
            Console.WriteLine("Public key: " + Convert.ToBase64String(publicKey));
            byte[] encryptData = PublicKeyEncryption.EncryptData(Encoding.UTF8.GetBytes(text), publicKey);
            Console.WriteLine("Encrypted data: " + Convert.ToBase64String(encryptData));
            byte[] decryptData = PublicKeyEncryption.DecryptData(encryptData, privateKey);
            Console.WriteLine("Original data: " + Encoding.UTF8.GetString(decryptData));
        }

        static void ShowDigitalSigning()
        {
            PublicKeyEncryption.GenerateKeys(out byte[] privateKey, out byte[] publicKey);
            
            byte[] randomData = new byte[100];
            RandomNumberGenerator.Fill(randomData);
            
            byte[] digitalSign = PublicKeyEncryption.SignData(randomData, privateKey, HashAlgorithmName.SHA256);
            Console.WriteLine("Digital sign: " + Convert.ToBase64String(digitalSign));
            
            Console.WriteLine("Verifying sign...");
            
            bool signVerification = PublicKeyEncryption.VerifySign(randomData, digitalSign, publicKey, HashAlgorithmName.SHA256);
            
            if(signVerification)
                Console.WriteLine("Correct sign");
            else
                Console.WriteLine("Invalid sign");
        }
        
        static void Main(string[] args)
        {
            ShowDigitalSigning();
        }
    }
}