using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace Encryption
{
    public class SymmetricEncryptionWithCompression
    {
        public static string Encrypt(string text, byte[] key, byte[] iv)
        {
            byte[] original = Encoding.UTF8.GetBytes(text);
            using var symmetricAlg = Aes.Create();
            using var encryptor = symmetricAlg.CreateEncryptor(key, iv);
            string path = GetRandomFilePath();
            using (var fs = new FileStream(path, FileMode.Create))
            {
                using (var cs = new CryptoStream(fs, encryptor, CryptoStreamMode.Write))
                {
                    using (var ds = new DeflateStream(cs, CompressionMode.Compress))
                    {
                        ds.Write(original);   
                    }
                }
            }

            byte[] buffer = File.ReadAllBytes(path);
            File.Delete(path);
            return Convert.ToBase64String(buffer);
        }

        public static string Decrypt(byte[] key, byte[] iv, string text)
        {
            using var symmetricAlg = Aes.Create();
            using var decrypt = symmetricAlg.CreateDecryptor(key, iv);
            byte[] encryptedBytes = Convert.FromBase64String(text);
            string path = GetRandomFilePath();
            File.WriteAllBytes(path, encryptedBytes);
            string originalData;
            using (var fs = new FileStream(path, FileMode.Open))
            {
                using (var cs = new CryptoStream(fs, decrypt, CryptoStreamMode.Read))
                {
                    using (var ds = new DeflateStream(cs, CompressionMode.Decompress))
                    {
                        using (var sr = new StreamReader(ds))
                        {
                            originalData = sr.ReadToEnd();
                        }
                    }
                }
            }
            File.Delete(path);
            return originalData;
        }
        private static string GetRandomFilePath() => Path.Combine(Environment.CurrentDirectory, Guid.NewGuid() + ".txt");
    }
}