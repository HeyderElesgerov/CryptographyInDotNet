using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encryption
{
    public static class SymmetricEncryption
    {
        public static string Encrypt(string text, byte[] key, byte[] iv)
        {
            byte[] original = Encoding.UTF8.GetBytes(text);
            using var symmetricAlg = Aes.Create();
            using var encryptor = symmetricAlg.CreateEncryptor(key, iv);
            string tempFilePath = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid() + ".txt");
            using(var fs = new FileStream(tempFilePath, FileMode.Create))
            {
                using (var cryptoStream = new CryptoStream(fs, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(original, 0, original.Length);
                }
            }
            using(var fs = new FileStream(tempFilePath, FileMode.Open))
            {
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, (int)fs.Length);
                fs.Dispose();
                File.Delete(tempFilePath);
                return Convert.ToBase64String(data);
            }
        }

        public static string Decrypt(byte[] key, byte[] iv, string text)
        {
            using (var symmetricAlg = Aes.Create())
            {
                using (var decrypt = symmetricAlg.CreateDecryptor(key, iv))
                {
                    string tempFilePath = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid() + ".txt");
                    using (var fs = new FileStream(tempFilePath, FileMode.Create))
                    {
                        using (var cryptoStream = new CryptoStream(fs, decrypt, CryptoStreamMode.Write))
                        {
                            byte[] encryptedTextBytes = Convert.FromBase64String(text);
                            cryptoStream.Write(encryptedTextBytes, 0, encryptedTextBytes.Length);
                        }
                    }

                    string decryptedText;
                    using (var fs = new FileStream(tempFilePath, FileMode.Open))
                    {
                        byte[] data = new byte[fs.Length];
                        fs.Read(data, 0, (int)fs.Length);
                        decryptedText = Encoding.UTF8.GetString(data);
                    }
                    File.Delete(tempFilePath);
                    return decryptedText;
                }
            }
        }
    }
}