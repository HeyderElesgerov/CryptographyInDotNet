using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encryption
{
    public static class SymmetricEncryption
    {
        public static string Encrypt(string password, string text, byte[] iv)
        {
            byte[] key = GetKeyFromPassword(password);
            byte[] original = Encoding.UTF8.GetBytes(text);
            using var aes = new AesCryptoServiceProvider();
            using var encryptor = aes.CreateEncryptor(key, iv);
            byte[] encryptedData = Crypt(original, encryptor);
            return Convert.ToBase64String(encryptedData);
        }

        public static string Decrypt(string password, byte[] iv, string text)
        {
            byte[] key = GetKeyFromPassword(password);
            using var aes = new AesCryptoServiceProvider();
            using var decrypt = aes.CreateDecryptor(key, iv);
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

        private static byte[] GetKeyFromPassword(string password)
        {
            using var pbkdf = new Rfc2898DeriveBytes(password, new byte[8], 5000, HashAlgorithmName.SHA256);
            return pbkdf.GetBytes(16);
        }
    }
}