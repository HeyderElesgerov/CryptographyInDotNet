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
            byte[] encryptedData = Crypt(original, encryptor);
            return Convert.ToBase64String(encryptedData);
        }

        public static string Decrypt(byte[] key, byte[] iv, string text)
        {
            using var symmetricAlg = Aes.Create();
            using var decrypt = symmetricAlg.CreateDecryptor(key, iv);
            byte[] encryptedBytes = Convert.FromBase64String(text);
            byte[] originalData = Crypt(encryptedBytes, decrypt);
            return Encoding.UTF8.GetString(originalData);
        }

        public static byte[] Crypt(byte[] data, ICryptoTransform transform)
        {
            string path = Path.Combine(Environment.CurrentDirectory, Guid.NewGuid() + ".txt");
            using (var fs = new FileStream(path, FileMode.Create))
            {
                using (var cs = new CryptoStream(fs, transform, CryptoStreamMode.Write))
                {
                    cs.Write(data);
                }
            }

            byte[] buffer;
            using (var fs = new FileStream(path, FileMode.Open))
            {
                buffer = new byte[fs.Length];
                fs.Read(buffer);
            }
            File.Delete(path);
            return buffer;
        }
    }
}